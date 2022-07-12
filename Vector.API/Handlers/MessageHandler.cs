using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using Vector.Common.BusinessLayer;
using Vector.Common.DataLayer;
using Vector.Common.Entities;

namespace Vector.API.Handlers
{
    public abstract class MessageHandler : DelegatingHandler
    {
        private const string HeaderTokenName = "rstoken";
        private const string VersionTokenName = "appversion";

        private const string specialCharacters = "<>#%{}|\\^~[]'@$!";
        private const string PAGEINDEX = "PAGEINDEX";
        private const string PAGESIZE = "PAGESIZE";
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpContext.Current.Items["RequestMessage"] = request;

            var credentials = ParseAuthorizationHeader(request);
            if (credentials != null)
            {
                var identity = new BasicAuthenticationIdentity(credentials.LoginId, credentials);
                var principal = new GenericPrincipal(identity, null);

                Thread.CurrentPrincipal = principal;
                if (HttpContext.Current != null)
                    HttpContext.Current.User = principal;
            }
            /* Code used for generating attributes for EMSLoggingHandler Request*/
            var corrId = string.Format("{0}{1}", DateTime.Now.Ticks, Thread.CurrentThread.ManagedThreadId);
            var requestInfo = string.Format("{0} {1}", request.Method, request.RequestUri);
            if (!HttpContext.Current.Request.ContentType.Contains("multipart/form-data;"))
            {
                var requestMessage = new byte[100];
                if (!HttpContext.Current.Request.HttpMethod.Contains("POST"))
                {
                    requestMessage = await request.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                }
                else
                    requestMessage = Encoding.UTF8.GetBytes("POST Data not logged");
                IncomingMessageAsync(corrId, requestInfo, requestMessage);
            }

            /*Check for special characters in the request URL*/
            string requestURI = HttpUtility.UrlDecode(request.RequestUri.PathAndQuery);
            if (!(request.RequestUri.AbsolutePath.ToUpper().Contains("/AUTHENTICATE/VALIDATEUSER")))
            {
                if (requestURI.Split('?')[0].IndexOfAny(specialCharacters.ToCharArray()) >= default(int))
                {
                    var errorResponse = request.CreateResponse<VectorResponse<Error>>(HttpStatusCode.BadRequest, new VectorResponse<Error> { Error = new ErrorManager().SetErrorResponseWithNoSpecialCharsAllowed() });
                    OutgoingMessageAsync(corrId, requestInfo, errorResponse.Content.ReadAsByteArrayAsync().Result);
                    return errorResponse;
                }
            }
            var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
            return response;
        }

        protected virtual UserDetailsModel ParseAuthorizationHeader(HttpRequestMessage request)
        {

            IEnumerable<string> tokens = null;
            IEnumerable<string> verisontokens = null;
            request.Headers.TryGetValues(VersionTokenName, out verisontokens);
            if (request.Headers.TryGetValues(HeaderTokenName, out tokens))
            {
                using (var objUserAuthenticationDA = new UserAuthenticationDL(new VectorDataConnection()))
                {
                    string userToken = tokens.First();
                    DataSet resultValidate = objUserAuthenticationDA.UserTokenAuthentication(userToken);
                    if (!DataManager.IsNullOrEmptyDataSet(resultValidate))
                    {
                        return new UserDetailsModel()
                        {
                            UserId = Convert.ToString(resultValidate.Tables[0].Rows[0]["UserId"]) != null? Convert.ToInt64(resultValidate.Tables[0].Rows[0]["UserId"]) : 0,
                            LoginId = Convert.ToString(resultValidate.Tables[0].Rows[0]["LoginId"]),
                            VectorToken = Convert.ToString(resultValidate.Tables[0].Rows[0]["UserToken"]),
                            VectorVersion = verisontokens != null ? verisontokens.First() : string.Empty,
                            UserGuid = Guid.NewGuid().ToString()
                        };
                    }
                    else
                        return null;
                }

            }
            else
                return null;

        }

        protected abstract void IncomingMessageAsync(string correlationId, string requestInfo, byte[] message);
        protected abstract void OutgoingMessageAsync(string correlationId, string requestInfo, byte[] message);

    }

    public class BasicAuthenticationIdentity : GenericIdentity
    {
        public BasicAuthenticationIdentity(string name, UserDetailsModel udm)
            : base(name, "Basic")
        {
            this.UserId = udm.UserId;
            this.UserGuid = udm.UserGuid;
            this.VectorToken = udm.VectorToken;
            this.LoginId = udm.LoginId;
            this.VectorVersion = udm.VectorVersion;
        }

        /// <summary>
        /// Basic Auth Password for custom authentication
        /// </summary>
        public Int64 UserId { get; set; }
        public string LoginId { get; set; }
        public string UserGuid { get; set; }
        public string VectorToken { get; set; }

        public string VectorVersion { get; set; }
    }
    public class VectorActionAutorizationFilter : AuthorizeAttribute
    {
        private const string vectorClientVersion = "VectorClientVersion";

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            bool isAuthorised = false;
            var identity = Thread.CurrentPrincipal.Identity;
            if (!string.IsNullOrEmpty(identity.AuthenticationType))
            {
                var vectorVersion = ((Vector.API.Handlers.BasicAuthenticationIdentity)HttpContext.Current.User.Identity).VectorVersion;

                if (identity == null && HttpContext.Current != null)
                    identity = HttpContext.Current.User.Identity;

                if (identity != null && identity.IsAuthenticated &&
                    ((!string.IsNullOrEmpty(vectorVersion) && checkVerisonMisMatch(vectorVersion)) || string.IsNullOrEmpty(vectorVersion) ))
                {
                    isAuthorised = true;
                }
                return isAuthorised;
            }
            else
            {
                return isAuthorised;
            }
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            VectorResponse<Error> objError;
            //bool isEMSVersionMismatch;
            var identity = Thread.CurrentPrincipal.Identity;
            if (!string.IsNullOrEmpty(identity.AuthenticationType))
            {
                var vectorClientVersionFromApp = ((Vector.API.Handlers.BasicAuthenticationIdentity)((System.Security.Principal.GenericPrincipal)HttpContext.Current.User).Identity).VectorVersion;

                if (!string.IsNullOrEmpty(vectorClientVersionFromApp) && !checkVerisonMisMatch(vectorClientVersionFromApp))
                    objError = new VectorResponse<Error>() { Error = new Error() { ErrorCode = "404", ErrorDescription = "You are using old Version of Vector" } };
                else
                    objError = new VectorResponse<Error>() { Error = new Error() { ErrorCode = "401", ErrorDescription = "Not authorized for requested action .. Kindly contact administrator" } };
                String jsonResult = String.Empty;
                var a = actionContext.Request.CreateResponse(HttpStatusCode.InternalServerError, objError);
                actionContext.Response = a;
            }
            else
            {
                objError = new VectorResponse<Error>() { Error = new Error() { ErrorCode = "401", ErrorDescription = "Not authorized for requested action .. Kindly contact administrator" } };
                // String jsonResult = String.Empty;
                var a = actionContext.Request.CreateResponse(HttpStatusCode.InternalServerError, objError);
                actionContext.Response = a;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="EmsClientVersionFromApp"></param>
        /// <returns></returns>
        public bool checkVerisonMisMatch(string vectorClientVersionFromApp)
        {
            bool isMatched = false;

            string vectorClientVersionConfig = SecurityManager.GetConfigValue(vectorClientVersion);
            if (!string.IsNullOrEmpty(vectorClientVersionFromApp))
            {
                string[] appKey = vectorClientVersionFromApp.Split('=');

                if (appKey[1] == "22121985")
                    return true;

                switch (appKey[0])
                {
                    case "vectorVersion":
                        isMatched = vectorClientVersionConfig.Equals(appKey[1]);
                        break;
                    default:
                        break;
                }
            }
            return isMatched;
        }
    }

    public class UserDetailsModel
    {
        public Int64 UserId { get; set; }
        public string LoginId { get; set; }
        public string UserGuid { get; set; }
        public string VectorToken { get; set; }
        public string VectorVersion { get; set; }
    }
}

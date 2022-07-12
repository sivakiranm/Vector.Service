namespace Vector.Common.BusinessLayer
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;
    using static Vector.Common.BusinessLayer.VectorEnums;

    /// <summary>
    /// Submit data to service using web request
    /// </summary>
    /// <param name="requestUrl">Request URL</param>
    /// <param name="requestData">Request Data</param>
    /// <param name="requestMethod">Request Method</param>
    /// <returns>Success or Error</returns>
    public static class WebRequestManager
    {
        private const string ContentLength = "content-length";

        #region Methods

        /// <summary>
        /// Submit data to service using web request
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="requestData"></param>
        /// <param name="requestMethod"></param>
        /// <param name="responseStatusCode"></param>
        /// <param name="contenttype"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="isEncodingRequire"></param>
        /// <param name="header"></param>
        /// <param name="isProjectBPD"></param>
        /// <param name="isVectorToken"></param>
        /// <param name="webRequestTimeout"></param>
        /// <returns></returns>
        public static string SubmitRequest(string requestUrl, string requestData, string requestMethod, ref string responseStatusCode,
                                            string contenttype = "application/x-www-form-urlencoded",
                                            string userName = "", string password = "",
                                            bool isEncodingRequire = true, string header = "", bool isProjectBPD = false, bool isVectorToken = true,
                                            int webRequestTimeout = 100000)  //PM-Metrics: score
        {
            string responseFromServer = string.Empty;

            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                ////Create a web request instance 
                WebRequest webRequest = WebRequest.Create(requestUrl);

                //isVectorToken is using only for IntacctTool
                if (isVectorToken)
                {
                    if (!string.IsNullOrEmpty(header))
                    {
                        string[] arr = header.Split('|');
                        if (!string.IsNullOrEmpty(arr[0]))
                        {
                            string[] arr2 = arr[0].Split(':');
                            if (!string.IsNullOrEmpty(arr2[1]))
                                webRequest.Headers.Add(arr2[0], arr2[1]);
                        }

                        if (!string.IsNullOrEmpty(arr[1]))
                        {
                            string[] arr3 = arr[1].Split(':');
                            if (!string.IsNullOrEmpty(arr3[1]))
                                webRequest.Headers.Add(arr3[0], arr3[1]);
                        }
                    }
                }

                //set auto
                if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
                {
                    SetBasicAuthHeader(webRequest, userName, password, isProjectBPD);
                }

                ////Set the Method property of the request to POST.
                webRequest.Method = requestMethod;

                //// Set the ContentType property of the WebRequest.
                webRequest.ContentType = contenttype;


                if (!string.IsNullOrEmpty(requestData) &&
                    StringManager.IsNotEqual(requestMethod, RequestMethod.GET.ToString()))
                {
                    //Encode Data
                    byte[] byteArray;
                    if (isEncodingRequire)
                    {
                        byteArray = Encoding.UTF8.GetBytes("xml=" + System.Web.HttpUtility.UrlEncode(requestData));
                    }
                    else
                    {
                        byteArray = Encoding.UTF8.GetBytes(requestData);
                    }

                    //// Set the ContentLength property of the WebRequest.
                    webRequest.ContentLength = byteArray.Length;

                    //// Get the request stream.
                    using (Stream dataStream = webRequest.GetRequestStream())
                    {
                        //// Write the data to the request stream.
                        dataStream.Write(byteArray, 0, byteArray.Length);

                        //// Close the Stream object.
                    }
                }

                webRequest.Timeout = webRequestTimeout;

                //// Get the response.
                using (WebResponse response = webRequest.GetResponse())
                {
                    ////Get the stream containing content returned by the server.
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            //// Read the content.
                            responseFromServer = reader.ReadToEnd();
                            responseStatusCode = "OK";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                responseStatusCode = "Error";
                responseFromServer = ex.Message;
            }
            return responseFromServer;
        }

        public static void SetBasicAuthHeader(WebRequest request, String userName, String userPassword, bool isProjectBPD = false)
        {
            string authInfo = string.Empty;
            if (isProjectBPD == false)
            {
                authInfo = userName + ":" + userPassword;
                authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
                request.Headers["Authorization"] = "Basic" + " " + authInfo;
            }
            else if (isProjectBPD == true)
            {
                authInfo = userPassword;
                request.Headers["Authorization"] = authInfo;
            }
        }


        #endregion
    }
}

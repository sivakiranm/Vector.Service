using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RingCentral;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Vector.Common.BusinessLayer;
using Vector.Common.DataLayer;
using Vector.Common.Entities;
using Vector.Workbench.DataLayer;

namespace Vector.Workbench.BusinessLayer
{
    public class RingCentralBL : DisposeLogic
    {
        private VectorDataConnection objVectorDB;
        public RingCentralBL(VectorDataConnection objVectorCon)
        {
            this.objVectorDB = objVectorCon;
        }


        public VectorResponse<object> GetRingCentralDetails(Int64 userId)
        {
            //DataSet dsRingCentralDataInfo = new DataSet();
            //DataTable dtData = new DataTable();


            //dtData.Columns.Add("action", typeof(string));
            //dtData.Columns.Add("billing", typeof(string));
            //dtData.Columns.Add("deleted", typeof(string));
            //dtData.Columns.Add("direction", typeof(string));
            //dtData.Columns.Add("duration", typeof(string));
            //dtData.Columns.Add("extension", typeof(string));
            //dtData.Columns.Add("from", typeof(string));
            //dtData.Columns.Add("id", typeof(string));
            //dtData.Columns.Add("internalType", typeof(string));
            //dtData.Columns.Add("lastModifiedTime", typeof(string));
            //dtData.Columns.Add("legs", typeof(string));
            //dtData.Columns.Add("message", typeof(string));
            //dtData.Columns.Add("reason", typeof(string));
            //dtData.Columns.Add("reasonDescription", typeof(string));
            //dtData.Columns.Add("recording", typeof(string));
            //dtData.Columns.Add("result", typeof(string));
            //dtData.Columns.Add("sessionId", typeof(string));
            //dtData.Columns.Add("shortRecording", typeof(string));
            //dtData.Columns.Add("startTime", typeof(string));
            //dtData.Columns.Add("telephonySessionId", typeof(string));
            //dtData.Columns.Add("to", typeof(string));
            //dtData.Columns.Add("transport", typeof(string));
            //dtData.Columns.Add("type", typeof(string));
            //dtData.Columns.Add("uri", typeof(string));

            DataSet dsUserData = new DataSet(); 
            using (var objRingCentralDL = new RingCentralDL(objVectorDB))
            {
                dsUserData = objRingCentralDL.GetRingCentralDetails(userId);
                string ringCentralClientId = SecurityManager.GetConfigValue("RingCentralClientId");
                string ringCentralClientSecret = SecurityManager.GetConfigValue("RingCentralClientSecret");
                Boolean ringCentralIsProduction = false;

                if (StringManager.IsEqual(SecurityManager.GetConfigValue("RingCentralIsProduction"), "true"))
                    ringCentralIsProduction = true;
                else
                    ringCentralIsProduction = false;

                var userRingCentralInfo = (from c in dsUserData.Tables[0].AsEnumerable()
                                           select new
                                           {
                                               ringCentralUserName = c.Field<string>("RingCentralUserId"),
                                               ringCentralUserPhone = c.Field<string>("RingCentralUserPhone"),
                                               ringCentralUserPassword = c.Field<string>("RingCentralUserPassword"),
                                               ringCentralUserPhoneExtension = c.Field<string>("RingCentralUserPhoneExtension")
                                           }).ToList().FirstOrDefault(); 

                try
                {
                    //string result = GetRingCentralResponse("restapi/oauth/token", userRingCentralInfo.ringCentralUserName, userRingCentralInfo.ringCentralUserPassword, "",
                    //    ringCentralClientId, ringCentralClientSecret);
                    //var tokenResult =  JsonConvert.DeserializeObject<TokenInfo>(result);

                    string callLog = GetRingCentralResponse("restapi/v1.0/account/~/call-log", userRingCentralInfo.ringCentralUserName, userRingCentralInfo.ringCentralUserPassword, "",
                        ringCentralClientId, ringCentralClientSecret);
                    var callLogInfo = JsonConvert.DeserializeObject(callLog);

                    return new VectorResponse<object>() { ResponseData = callLogInfo };
                }
                catch (Exception ex)
                {
                    string val = ex.Message;
                } 


            }


            return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

             

        }

        public string GetRingCentralResponse(string url,string userName,string Password,string type,string clientId,string clientSecretKey)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://platform.devtest.ringcentral.com");
                var credentials = Encoding.ASCII.GetBytes(String.Format("{0}:{1}", clientId, clientSecretKey));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(credentials));

                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("username", userName),
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("access_token_ttl", "3600"),
                    new KeyValuePair<string, string>("refresh_token_ttl", "604800"),
                    new KeyValuePair<string, string>("password", Password)
                      });
                //"restapi/oauth/token"
                var response =
                    client.PostAsync(url, formContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                   // var token = JsonConvert.DeserializeObject<TokenInfo>(result);

                    return result;

                    //var response1 =
                   //client.PostAsync("restapi/v1.0/account/~/call-log", formContent).Result;
                   // var result1 = response.Content.ReadAsStringAsync().Result;
                    //var resp = JsonConvert.DeserializeObject(result1);


                    //if (resp != null)
                    //{
                    //    return new VectorResponse<object>() { ResponseData = resp };
                    //}
                    //else
                    //{
                    //    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                    //}

                }
            }

            return "";
        }

    }
}

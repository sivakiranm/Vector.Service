using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Runtime.Serialization;
using Vector.Common.BusinessLayer;
using Vector.Intacct.BusinessLogic;

namespace Vector.Intacct.APIAccess
{
    public class IntacctAPIAccessLayer : BaseLogic
    {
        string statusCode = string.Empty;
        public object GetData<T>(string requestType, string requestURL, string vectorToken, string vectorVersion,
                                 string requestBody = null, string contentType = "application/json", bool isVectorResponseOutput = true)
        {
            List<T> objFinalData = new List<T>();
            try
            {
                string url = string.Empty;
                url = Convert.ToString(Convert.ToString(ConfigurationManager.AppSettings["APIURL"] + requestURL));


                int counter = default(int);
                List<T> objDeserializeObj = new List<T>();

                var objResponse = (object)null;

                objResponse = FetchDataFromVectorAPI(url, requestType, contentType, vectorToken, vectorVersion, requestData: requestBody, isEncodingRequire: false);

                if (isVectorResponseOutput)
                    return getVectorResponse<T>(objResponse.ToString());
                else
                {
                    objDeserializeObj = getResponse<T>(objResponse.ToString()) as List<T>;
                    objDeserializeObj.ForEach(x => objFinalData.Add(x));
                    counter++;
                }

                return DataManager.ToDataTable<T>(objFinalData);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private object getVectorResponse<T>(string objResponse)
        {
            if (typeof(T) == typeof(DataTable))
            {
                DataTable dt = (DataTable)JsonConvert.DeserializeObject((JsonConvert.DeserializeObject<VectorResponse<object>>(objResponse.ToString())).ResponseData.ToString(), (typeof(DataTable)));
                return new VectorResponse<DataTable>() { ResponseData = dt };
            }
            else if (typeof(T) == typeof(DataSet))
            {
                DataSet ds = (DataSet)JsonConvert.DeserializeObject((JsonConvert.DeserializeObject<VectorResponse<object>>(objResponse.ToString())).ResponseData.ToString(), (typeof(DataSet)));
                if (!DataManager.IsNullOrEmptyDataSet(ds))
                {
                    return new VectorResponse<DataSet>() { ResponseData = ds };
                }
                return null;
            }
            else if (typeof(T) == typeof(object))
            {
                var objResponseData = JsonConvert.DeserializeObject<VectorResponse<T>>(objResponse.ToString()).ResponseData;
                if (objResponseData != null)
                {
                    return new VectorResponse<T>()
                    {
                        ResponseData = objResponseData,
                        Response = JsonConvert.DeserializeObject<VectorResponse<T>>(objResponse.ToString()).Response?.ToString(),
                        ResponseType = JsonConvert.DeserializeObject<VectorResponse<T>>(objResponse.ToString()).ResponseType?.ToString()
                    };
                }
                return null;
            }

            return JsonConvert.DeserializeObject<T>(objResponse);
        }

        private List<T> getResponse<T>(string objResponse)
        {
            //var a =  JsonConvert.DeserializeObject<EMSResponse<List<T>>>(objResponse);
            //return null;
            var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(objResponse);
            if (dict["error"] == null)
            {
                var response = dict["response"].ToString();
                return JsonConvert.DeserializeObject<List<T>>(JObject.Parse(response).First.First.ToString());
            }

            return null;
        }

        private string FetchDataFromVectorAPI(string requestURL, string requestType, string contentType, string token, string appVersion,
                                            string requestData = null, bool isEncodingRequire = false)
        {
            return WebRequestManager.SubmitRequest(requestURL, requestData, requestType, ref statusCode, contenttype: contentType,
                                                    header: "rstoken:" + token + "|appversion:" + "vectorVersion=" + appVersion, isEncodingRequire: isEncodingRequire);
        }
    }

    public class VectorResponse<T> : BaseLogic
    {
        [DataMember(Order = 1)]
        public string ResponseType
        {
            get
            {
                return typeof(T).Name;
            }
            set { }
        }

        [DataMember(Order = 2)]
        public string Response { get; set; }


        [DataMember(Order = 3)]
        public string ResponseMessage { get; set; }

        [DataMember(Order = 4)]
        public string ResponseCode { get; set; }

        [DataMember(Order = 3)]
        public T ResponseData { get; set; }

        [DataMember(Order = 6)]
        public Error Error { get; set; }
    }

    public class Error
    {
        [DataMember(Order = 1)]
        public string ErrorCode { get; set; }
        [DataMember(Order = 2)]
        public string ErrorDescription { get; set; }
    }
}

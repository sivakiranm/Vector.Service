using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Vector.Common.BusinessLayer;
using Vector.Common.Entities;
using static Vector.Common.BusinessLayer.VectorEnums;

namespace Vector.WindowsServiceManager
{
    public class ServiceConfiguration
    {
        private const string FileDirectory = "FileDirectory";

        enum ConfigurationXMLNode
        {
            Service,
            Status,
            Active,
            Name,
            Services,
            Client,
            ExecutionTime,
            Occurrence,
            ClientId,
            ClientCode,
            EntityId,
            EntityType,
            EntityName
        }
        enum ServiceOccurrence
        {
            Daily,
            Monthly
        }

        enum WindowsServiceName
        {

        }
        public static XDocument ReadConfigurationXML()
        {
            return XDocument.Load(AppDomain.CurrentDomain.BaseDirectory.Replace(SecurityManager.GetConfigValue(FileDirectory).ToString(), VectorConstants.Empty) + VectorConstants.ServiceConfigurationxml);
        }

        public static DataTable GetServiceDetailsFromXML(XDocument configDocument)
        {
            if (configDocument.DescendantNodes().ToList().Count > 0)
            {
                return GetServiceDetailsFromDocument(configDocument);
            }
            else
            {
                return GetServiceDetailsFromDocument(ReadConfigurationXML());
            }
        }

        private static DataTable GetServiceDetailsFromDocument(XDocument configDocument)
        {
            var activeServices = (from r in configDocument.Descendants(ConfigurationXMLNode.Service.ToString()).Where
                                 (r => (string)r.Attribute(ConfigurationXMLNode.Status.ToString()) == ConfigurationXMLNode.Active.ToString())
                                  select new
                                  {
                                      Name = r.Attribute(ConfigurationXMLNode.Name.ToString()).Value,
                                  }).ToList();
            DataSet services = DataManager.ListToDataSet(activeServices);
            if (!DataManager.IsNullOrEmptyDataSet(services))
            {
                return services.Tables[VectorConstants.Zero];
            }
            return null;
        }

        public static void ExecuteDailyServices(XDocument configurationDocument, DataTable serviceDetails, System.DateTime tickDateTime)
        {
            if (!DataManager.IsNullOrEmptyDataTable(serviceDetails))
            {
                foreach (DataRow serviceRow in serviceDetails.Rows)
                {
                    string serviceName = serviceRow[ConfigurationXMLNode.Name.ToString()].ToString();
                    DataTable entityDetails = GetServiceDetailsFromXML(configurationDocument, serviceName, tickDateTime, ServiceOccurrence.Daily.ToString());
                    if (!DataManager.IsNullOrEmptyDataTable(entityDetails))
                    {
                        Thread SrviceLevelThread = new Thread(() => RunDailyService(serviceName, entityDetails));
                        SrviceLevelThread.Name = serviceName + DateManager.GetTimestamp(System.DateTime.Now).ToString();
                        SrviceLevelThread.Start();
                    }
                }
            }
        }

        private static DataTable GetServiceDetailsFromXML(XDocument configurationDocument, string serviceName, DateTime tickDateTime, string Occurrence)
        {
            if (configurationDocument.DescendantNodes().ToList().Count > 0)
            {
                return GetServiceDetailsFromXMLDocumnet(configurationDocument, serviceName, tickDateTime, Occurrence);
            }
            else
            {
                return GetServiceDetailsFromXMLDocumnet(ReadConfigurationXML(), serviceName, tickDateTime, Occurrence);
            }
        }

        public static DataTable GetServiceDetailsFromXMLDocumnet(XDocument configurationDocument, string serviceName, DateTime tickDateTime, string Occurrence)
        {

            DataSet serviceDetails = null;
            var activeServices = (from c in configurationDocument.Elements(ConfigurationXMLNode.Services.ToString())
                                     .Elements(ConfigurationXMLNode.Service.ToString()).Elements(ConfigurationXMLNode.Client.ToString())
                                  where ((string)c.Parent.Attribute(ConfigurationXMLNode.Name.ToString()) == serviceName)
                                   && ((string)c.Element(ConfigurationXMLNode.Status.ToString()).Value == ConfigurationXMLNode.Active.ToString())
                                   && ((string)c.Element(ConfigurationXMLNode.ExecutionTime.ToString()).Value == GetExecutionTimeForDailyService(tickDateTime, Occurrence))
                                    && ((string)c.Element(ConfigurationXMLNode.Occurrence.ToString()).Value == Occurrence)
                                  select new
                                  {
                                      EntityId = c.Element(ConfigurationXMLNode.EntityId.ToString()).Value,
                                      EntityType = c.Element(ConfigurationXMLNode.EntityType.ToString()).Value,
                                      EntityName = c.Element(ConfigurationXMLNode.EntityName.ToString()).Value,
                                      Occurrence = c.Element(ConfigurationXMLNode.Occurrence.ToString()).Value,
                                      ExecutionTime = c.Element(ConfigurationXMLNode.ExecutionTime.ToString()).Value

                                  }).ToList();
            if (activeServices.Count > 0)
                serviceDetails = DataManager.ListToDataSet(activeServices);

            if (!DataManager.IsNullOrEmptyDataSet(serviceDetails))
            {
                return serviceDetails.Tables[VectorConstants.Zero];
            }
            return null;
        }

        private static string GetExecutionTimeForDailyService(DateTime tickDateTime, string OccurrenceValue)
        {
            if (StringManager.IsEqual(OccurrenceValue, ServiceOccurrence.Daily.ToString()))
            {
                return tickDateTime.Hour.ToString() + VectorConstants.Colon + tickDateTime.Minute.ToString();
                // return "10:30";
            }
            else if (StringManager.IsEqual(OccurrenceValue, ServiceOccurrence.Monthly.ToString()))
            {
                return tickDateTime.Day.ToString() + VectorConstants.Pipe + tickDateTime.Hour.ToString() + VectorConstants.Colon + tickDateTime.Minute.ToString();
                // return "2|19:30";
            }
            else
            {
                return string.Empty;
            }
        }

        public static void RunDailyService(string serviceName, DataTable entityDetails)
        {
            if (!DataManager.IsNullOrEmptyDataTable(entityDetails))
            {
                if (StringManager.IsEqual(serviceName, ServiceName.NegotiationRevisitEmail.ToString()))
                {
                    string status = string.Empty;

                    foreach (DataRow item in entityDetails.Rows)
                    {
                        try
                        {
                            ServiceManagerCommon.WriteExceptionToLogFile(null, ServiceName.NegotiationRevisitEmail.ToString() + VectorConstants.Started);

                            string responseCode = string.Empty;

                            string response = WebRequestManager.SubmitRequest((SecurityManager.GetConfigValue(ConfigValue.VectorAPIUrl.ToString()) +
                                                                        SecurityManager.GetConfigValue(ConfigValue.NegotiationRevisitEmailUrl.ToString())),
                                                                    string.Empty, VectorEnums.RequestMethod.GET.ToString(), ref responseCode, VectorConstants.ContentType,
                                                                    string.Empty, string.Empty, false, string.Empty, webRequestTimeout: 300000);


                            var obj = JsonConvert.DeserializeObject<VectorResponse<EmailResult>>(response);

                            if (obj.Error != null)
                            {
                                Exception ex = new Exception(obj.Error.ErrorDescription);
                                ServiceManagerCommon.WriteExceptionToLogFile(ex);
                            }
                            else
                            {
                                if (obj.ResponseData != null)
                                {
                                    if (!obj.ResponseData.Result)
                                    {
                                        Exception ex = new Exception(obj.ResponseData.ResultDesc);
                                        ServiceManagerCommon.WriteExceptionToLogFile(ex);
                                    }
                                }
                            }

                            ServiceManagerCommon.WriteExceptionToLogFile(null, ServiceName.NegotiationRevisitEmail.ToString() + VectorConstants.Ended);

                        }
                        catch (Exception ex)
                        {
                            ServiceManagerCommon.WriteExceptionToLogFile(ex);
                        }
                    }
                }
                if (StringManager.IsEqual(serviceName, ServiceName.VectorServiceManager.ToString()))
                {
                    string status = string.Empty;

                    foreach (DataRow item in entityDetails.Rows)
                    {
                        try
                        {
                            ServiceManagerCommon.WriteExceptionToLogFile(null, ServiceName.VectorServiceManager.ToString() + VectorConstants.Started);

                            string responseCode = string.Empty;

                            string response = WebRequestManager.SubmitRequest((SecurityManager.GetConfigValue(ConfigValue.VectorAPIUrl.ToString()) +
                                                                        SecurityManager.GetConfigValue(ConfigValue.VectorServiceManagerUrl.ToString())),
                                                                    string.Empty, VectorEnums.RequestMethod.GET.ToString(), ref responseCode, VectorConstants.ContentType,
                                                                    string.Empty, string.Empty, false, string.Empty, webRequestTimeout: 300000);


                            var obj = JsonConvert.DeserializeObject<VectorResponse<EmailResult>>(response);

                            if (obj.Error != null)
                            {
                                Exception ex = new Exception(obj.Error.ErrorDescription);
                                ServiceManagerCommon.WriteExceptionToLogFile(ex);
                            }
                            else
                            {
                                if (obj.ResponseData != null)
                                {
                                    if (!obj.ResponseData.Result)
                                    {
                                        Exception ex = new Exception(obj.ResponseData.ResultDesc);
                                        ServiceManagerCommon.WriteExceptionToLogFile(ex);
                                    }
                                }
                            }

                            ServiceManagerCommon.WriteExceptionToLogFile(null, ServiceName.VectorServiceManager.ToString() + VectorConstants.Ended);

                        }
                        catch (Exception ex)
                        {
                            ServiceManagerCommon.WriteExceptionToLogFile(ex);
                        }
                    }
                }

            }
        }

        public static void RunService()
        {
            string responseCode = string.Empty;
            string response = WebRequestManager.SubmitRequest((SecurityManager.GetConfigValue(ConfigValue.VectorAPIUrl.ToString()) +
                                                                    SecurityManager.GetConfigValue(ConfigValue.NegotiationRevisitEmailUrl.ToString())),
                                                                    string.Empty, VectorEnums.RequestMethod.GET.ToString(), ref responseCode, VectorConstants.ContentType,
                                                                    string.Empty, string.Empty, false, string.Empty, isVectorToken: false, webRequestTimeout: 300000);


            var obj = JsonConvert.DeserializeObject<VectorResponse<EmailResult>>(response);

            if (obj.Error != null)
            {
                Exception ex = new Exception(obj.Error.ErrorDescription);
                ServiceManagerCommon.WriteExceptionToLogFile(ex);
            }
            else
            {
                if (obj.ResponseData != null)
                {
                    if (!obj.ResponseData.Result)
                    {
                        Exception ex = new Exception(obj.ResponseMessage);
                        ServiceManagerCommon.WriteExceptionToLogFile(ex);
                    }
                }
            }
        }

    }
}

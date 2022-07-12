using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Vector.Common.BusinessLayer;
using Vector.Common.DataLayer;
using Vector.Common.Entities;
using Vector.Garage.DataLayer;
using Vector.Garage.Entities;
using static Vector.Common.BusinessLayer.EmailTemplateEnums;

namespace Vector.Garage.BusinessLayer
{
    public class ExceptionsBL : DisposeLogic
    {
        private VectorDataConnection objVectorDB;
        public ExceptionsBL(VectorDataConnection objVectorCon)
        {
            this.objVectorDB = objVectorCon;
        }

        public VectorResponse<object> GetExceptions(ExceptionsSearch objExceptionsSearch)
        {
            using (var objExceptionsDL = new ExceptionsDL(objVectorDB))
            {
                var result = objExceptionsDL.GetExceptions(objExceptionsSearch);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }

        //public VectorResponse<object> CreateException(CreateException objCreateException)
        //{
        //    using (var objExceptionsDL = new ExceptionsDL(objVectorDB))
        //    {
        //        Tuple<int, string, string> result = objExceptionsDL.CreateException(objCreateException);

        //        if (result.Item1 > 0)
        //        {
        //            //Success
        //            return new VectorResponse<object>() { ResponseType = "Success", Response = "Success", ResponseMessage = "Exception Raised Successfully!" };
        //        }
        //        else if (result.Item1 == -1)
        //        {
        //            return new VectorResponse<object>() { ResponseType = "Error", Response = "Error", ResponseMessage = "Please Enter Valid Property In the Selected Client!" };
        //        }
        //        else if (result.Item1 == -2)
        //            return SendMailWhenExceptionReopen(result.Item2, objCreateException,"Exception Raised Successfully");
        //        else
        //        {
        //            return new VectorResponse<object>() { ResponseType = "Error", Response = "Error", ResponseMessage = "Error occured. Please contact Administrator" };
        //        }
        //    }
        //}

        public VectorResponse<object> RaiseException(CreateException objCreateException)
        {
            using (var objExceptionsDL = new ExceptionsDL(objVectorDB))
            {
                DataSet result = objExceptionsDL.RaiseException(objCreateException);

                if (!DataManager.IsNullOrEmptyDataSet(result))
                {
                    ExceptionResult resultInfo = (from c in result.Tables[0].AsEnumerable()
                                      select new ExceptionResult
                                      {
                                          result = c.Field<bool>("Result"),
                                          message = c.Field<string>("ResultMessage"),
                                          errorlog = c.Field<string>("ErrorMessageToLog"),
                                          emails = c.Field<string>("Emails"),
                                      }).ToList().FirstOrDefault();

                    if (!string.IsNullOrEmpty(resultInfo.emails))
                    {
                        return SendMailWhenExceptionReopen(resultInfo.emails, objCreateException, resultInfo.message, result);
                    }

                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { ResponseType = "Error", Response = "Error", ResponseMessage = "Error occured. Please contact Administrator" };
                } 
            }
        }

        private VectorResponse<object> SendMailWhenExceptionReopen(String emailId, CreateException objCreateException, string message, DataSet resultInfo)
        {
            string finalResultMessage = "";

            if (string.IsNullOrEmpty(emailId))
                emailId = SecurityManager.GetConfigValue("ReOpenExceptionEmailTo");

            if (!string.IsNullOrEmpty(emailId))
            {
                EmailTemplate objEmailTemplate = EmailTemplateManager.GetEmailTemplate(AppDomain.CurrentDomain.BaseDirectory, EmailTemplates.Exceptions, EmailTemplateGUIDs.ReOpenException);

                string emailBody = string.Format(objEmailTemplate.EmailBody, objCreateException.AcctNbr, objCreateException.InvoiceNbr, objCreateException.ExceptionType, objCreateException.ExceptionDesc, objCreateException.Comments, SecurityManager.GetConfigValue("VectorURL"), SecurityManager.GetConfigValue("VectorWebSite"));

                string subject = string.Format(objEmailTemplate.Subject, objCreateException.ProPertyName, objCreateException.AcctNbr);

                string logoPath = HttpContext.Current.Server.MapPath(VectorConstants.TildeSeparator.ToString());
                logoPath += objEmailTemplate.EmailImagesList[default(int)].Path;

                bool emailResult = EmailManager.SendEmailWithMutipleAttachments(emailId, SecurityManager.GetConfigValue("FromEmail"), subject,
                                                                    emailBody, "", "", null, "", false, logoPath);
                if (emailResult)
                    return new VectorResponse<object>() { ResponseData = resultInfo };
                else
                {
                    finalResultMessage = Convert.ToString(resultInfo.Tables[0].Rows[0]["ResultMessage"]);
                    finalResultMessage = finalResultMessage + ", Unable to send Notification";
                    resultInfo.Tables[0].Rows[0]["ResultMessage"] = finalResultMessage;

                    return new VectorResponse<object>() { ResponseData = resultInfo };
                }
            }
            else
                return new VectorResponse<object>() { ResponseData = resultInfo };
        }



        public VectorResponse<object> CreateExceptionTicket(ExceptionTicketEntity objCreateException,Int64 userId)
        {
            using (var objExceptionsDL = new ExceptionsDL(objVectorDB))
            {
                DataSet result = objExceptionsDL.CreateExceptionTicket(objCreateException,userId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
                //if (!DataManager.IsNullOrEmptyDataSet(result))
                //{
                //    ExceptionResult resultInfo = (from c in result.Tables[0].AsEnumerable()
                //                                  select new ExceptionResult
                //                                  {
                //                                      result = c.Field<bool>("Result"),
                //                                      message = c.Field<string>("ResultMessage"),
                //                                      errorlog = c.Field<string>("ErrorMessageToLog"),
                //                                      emails = c.Field<string>("Emails"),
                //                                  }).ToList().FirstOrDefault();

                //    //if (!string.IsNullOrEmpty(resultInfo.emails))
                //    //{
                //    //    return SendMailWhenExceptionReopen(resultInfo.emails, objCreateException, resultInfo.message, result);
                //    //}

                //    return new VectorResponse<object>() { ResponseData = result };
                //}
                //else
                //{
                //    return new VectorResponse<object>() { ResponseType = "Error", Response = "Error", ResponseMessage = "Error occured. Please contact Administrator" };
                //}
            }
        }


        public VectorResponse<object> GetExceptionAnalyticsData(string fromDate,string toDate,Int64 userId)
        {
            using (var objExceptionsDL = new ExceptionsDL(objVectorDB))
            {
                var result = objExceptionsDL.GetExceptionAnalyticsData(fromDate, toDate, userId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }

        public VectorResponse<object> GetExceptionHistory(Int64 exceptionId, Int64 userId)
        {
            using (var objExceptionsDL = new ExceptionsDL(objVectorDB))
            {
                var result = objExceptionsDL.GetExceptionHistory(exceptionId, userId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }


    }
}

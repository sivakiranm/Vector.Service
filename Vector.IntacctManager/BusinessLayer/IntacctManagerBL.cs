using System;
using System.Data;
using System.IO;
using Vector.Common.BusinessLayer;
using Vector.Common.DataLayer;
using Vector.Common.Entities;
using Vector.IntacctManager.DataLayer;

namespace Vector.IntacctManager.BusinessLayer
{
    public class IntacctManagerBL : DisposeLogic
    {
        private VectorDataConnection objVectorDB;
        public IntacctManagerBL(VectorDataConnection objVectorCon)
        {
            this.objVectorDB = objVectorCon;
        }

        public VectorResponse<object> GetReportDataByType(string dataType, string fromDate, string toDate, string type = null)
        {
            try
            {
                DataSet reportData = new DataSet();
                switch (dataType)
                {
                    case "Customer":
                        reportData = GetCustomerData(fromDate, toDate);
                        break;
                    case "Property":
                        reportData = GetPropertyData(fromDate, toDate, type);
                        break;
                    case "Invoice":
                        reportData = GetInvoiceData(fromDate, toDate, type);
                        break;
                }

                if (DataValidationLayer.isDataSetNotNull(reportData))
                {
                    StringWriter sw = new StringWriter();
                    reportData.WriteXml(sw, XmlWriteMode.WriteSchema);
                    return new VectorResponse<object>() { ResponseData = reportData, ResponseType = dataType, Response = Convert.ToString(sw) };
                }
                else
                {
                    return new VectorResponse<object>() { ResponseType = "Exception", Error = new Error() { ErrorDescription = "No Data Available!" } };
                }
            }
            catch (Exception ex)
            {
                return new VectorResponse<object>() { ResponseType = "Exception", Error = new Error() { ErrorDescription = ex.Message } };
            }
        }

        public DataSet GetCustomerData(string fromDate, string toDate)
        {
            using (IntacctManagerDL objMasterDataDA = new IntacctManagerDL(objVectorDB))
            {
                return objMasterDataDA.GetCustomerData(fromDate, toDate);
            }
        }

        public DataSet GetPropertyData(string fromDate, string toDate, string syncType = null)
        {
            using (IntacctManagerDL objMasterDataDA = new IntacctManagerDL(objVectorDB))
            {
                return objMasterDataDA.GetPropertyData(fromDate, toDate, syncType);
            }
        }

        public DataSet GetInvoiceData(string fromDate, string toDate,string type)
        {
            using (IntacctManagerDL objMasterDataDA = new IntacctManagerDL(objVectorDB))
            {
                return objMasterDataDA.GetInvoiceData(fromDate, toDate, type);
            }
        }

        public VectorResponse<object> UpdateReportDataByType(string qBDataType, string customerKey, string qBID,string keyNo, string intacctId = null)
        {
            try
            {
                switch (qBDataType)
                {
                    case "Customer":
                        UpdateCustomerData(customerKey, qBID, intacctId,keyNo);
                        break;
                    case "Property":
                        UpdatePropertyData(customerKey, qBID, keyNo, intacctId);
                        break;
                    case "Hauler":
                        UpdateHaulerData(customerKey, qBID, keyNo);
                        break;
                }
                return new VectorResponse<object>() { ResponseData = "Success", ResponseType = "Success", ResponseMessage = "Success",ResponseCode="200", Response = Convert.ToString(qBID) };
            }
            catch (Exception ex)
            {
                return new VectorResponse<object>() { ResponseType = "Exception", ResponseMessage = "Failure", ResponseCode = "201", Error = new Error() { ErrorDescription = ex.Message } };
            }
        }

        public void UpdateCustomerData(string customerKey, string qBID,string KeyNo, string intacctId = null)
        {
            using (IntacctManagerDL objMasterDataDA = new IntacctManagerDL(objVectorDB))
            {
                objMasterDataDA.UpdateCustomerData(customerKey, qBID, KeyNo, intacctId);
            }
        }

        public void UpdatePropertyData(string customerKey, string qBID, string KeyNo, string intacctId = null)
        {
            using (IntacctManagerDL objMasterDataDA = new IntacctManagerDL(objVectorDB))
            {
                objMasterDataDA.UpdatePropertyData(customerKey, qBID, KeyNo, intacctId);
            }
        }

        public void UpdateHaulerData(string customerKey, string qBID, string KeyNo)
        {
            using (IntacctManagerDL objMasterDataDA = new IntacctManagerDL(objVectorDB))
            {
                objMasterDataDA.UpdateHaulerData(customerKey, qBID, KeyNo);
            }
        }

        public VectorResponse<object> UpdateIntacctSyncId(string intacctType, string customerKey, string intacctId)
        {

            try
            {
                using (IntacctManagerDL objMasterDataDA = new IntacctManagerDL(objVectorDB))
                {
                    int result = 0;
                    result = objMasterDataDA.UpdateIntacctData(customerKey, intacctId, intacctType);
                    return new VectorResponse<object>() { ResponseData = "Success", ResponseType = "Success", Response = Convert.ToString(result) };
                }
            }
            catch (Exception ex)
            {
                return new VectorResponse<object>() { ResponseType = "Exception", Error = new Error() { ErrorDescription = ex.Message } };
            }
        }

        public VectorResponse<object> UpdateInvoice(string vectorInvoiceNo, string qBID, string type)
        {
            try
            {
                using (IntacctManagerDL objMasterDataDA = new IntacctManagerDL(objVectorDB))
                {
                    objMasterDataDA.UpdateInvoiceData(vectorInvoiceNo, qBID, type);
                    return new VectorResponse<object>() { ResponseData = "Success", ResponseType = "Success", ResponseMessage = "Success", ResponseCode = "200", Response = Convert.ToString(qBID) };
                }
            }
            catch (Exception ex)
            {
                return new VectorResponse<object>() { ResponseType = "Exception", ResponseMessage = "Failure", ResponseCode = "201", Error = new Error() { ErrorDescription = ex.Message } };
            }
        }

        public VectorResponse<object> UpdateBillData(string invoiceNumber, string qBID, string type)
        {
            try
            {
                using (IntacctManagerDL objMasterDataDA = new IntacctManagerDL(objVectorDB))
                {
                    objMasterDataDA.UpdateInvoicesBillData(invoiceNumber, qBID, type);
                    return new VectorResponse<object>() { ResponseData = "Success", ResponseType = "Success", ResponseMessage = "Success", ResponseCode = "200", Response = Convert.ToString(qBID) };

                }
            }
            catch (Exception ex)
            {
                return new VectorResponse<object>() { ResponseType = "Exception", ResponseMessage = "Failure", ResponseCode = "201", Error = new Error() { ErrorDescription = ex.Message } };
            }
        }

        public VectorResponse<object> UpdateReceiptPayment(string paymentQBID, string Comments,
                                                               string paymentType, DateTime DateUpdated,
                                                               Double TotalAmount, string invoiceDetailsXML, string intacctId)
        {
            try
            {
                //using (UserBO objUserBO = new UserBO())
                //{
                //    string refuseConnection = objUserBO.ValidateUserAndGetRefuseConnection(loginid, password);
                //    if (!string.IsNullOrEmpty(refuseConnection))
                //    {
                //        DataSet resultData = UpdateReceiptPaymentDetails(refuseConnection, paymentQBID, Comments,
                //                                                paymentType, DateUpdated,
                //                                                TotalAmount, invoiceDetailsXML, loginid, intacctId);
                //        res.Response = (!DataManager.IsNullOrEmptyDataSet(resultData)) ? GetStatus(Convert.ToInt16(resultData.Tables[0].Rows[0]["Result"])) : ReceiptPaymentResult.UnknownError;
                //        res.ResposeType = typeof(ReceiptPaymentResult).ToString();
                //        res.Error = null;
                //    }
                //    else
                //        ErrorUtility.ReturnAuthenticationError(res);
                //}

                return new VectorResponse<object>() { ResponseType = "Exception", Error = new Error() { ErrorCode = "404", ErrorDescription = "Notification : Payments Sync Service is Temporarly Stopped, Please contact Administrator." } };
            }
            catch (Exception ex)
            {
                return new VectorResponse<object>() { ResponseType = "Exception", Error = new Error() { ErrorDescription = ex.Message } };
                //ErrorLogger.WriteToLogFile1("**********************************************");
                //ErrorLogger.WriteToLogFile1("Error Message:" + ex.Message + ex.StackTrace + System.Environment.NewLine + "Comments:" + Comments + System.Environment.NewLine + "data:" + invoiceDetailsXML);
            }
        }

        public VectorResponse<object> LogException(string customerKey, string intacct, string entityType, string errorDescription, string intacctSession,
                         string systemIp, string errorType, string xml, string userId)
        {
            using (IntacctManagerDL objMasterDataDA = new IntacctManagerDL(objVectorDB))
            {
                objMasterDataDA.LogException(customerKey, intacct, entityType, errorDescription, intacctSession, systemIp, errorType, xml, userId);
                return new VectorResponse<object>() { Response = "Success" };
            }
        }
    }
}

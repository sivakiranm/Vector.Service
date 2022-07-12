using Newtonsoft.Json;
using Vector.Intacct.APIAccess;
using Vector.Intacct.BusinessLogic;
using Vector.Intacct.Entities;
using static Vector.Common.BusinessLayer.VectorEnums;

namespace Vector.Intacct.DataAccess
{
    public class IntacctDALayer : BaseLogic
    {
        #region Login

        internal object ValidateUser(string loginId, string password)

        {
            using (IntacctAPIAccessLayer objIntacctAPIAccessLayer = new IntacctAPIAccessLayer())
            {
                return objIntacctAPIAccessLayer.GetData<object>(
                             RequestMethod.GET.ToString(), string.Format(IntacctAPIURL.ValidateUser, loginId, password),
                             SecurityContext.Instance.VectorToken, SecurityContext.Instance.AppVersion, null, "application/json", true);
            }
        }

        internal object GetCustomer(string fromDate, string toDate)

        {
            using (IntacctAPIAccessLayer objIntacctAPIAccessLayer = new IntacctAPIAccessLayer())
            {
                return objIntacctAPIAccessLayer.GetData<object>(
                             RequestMethod.GET.ToString(), string.Format(IntacctAPIURL.GetCustomer, fromDate, toDate),
                             SecurityContext.Instance.VectorToken, SecurityContext.Instance.AppVersion, null, "application/json", true);
            }
        }

        internal object GetProperty(string fromDate, string toDate, string syncType)

        {
            using (IntacctAPIAccessLayer objIntacctAPIAccessLayer = new IntacctAPIAccessLayer())
            {
                return objIntacctAPIAccessLayer.GetData<object>(
                             RequestMethod.GET.ToString(), string.Format(IntacctAPIURL.GetProperty, fromDate, toDate, syncType),
                             SecurityContext.Instance.VectorToken, SecurityContext.Instance.AppVersion, null, "application/json", true);
            }
        }

        public object GetInvoice(string fromDate, string toDate, string type)
        {
            using (IntacctAPIAccessLayer objIntacctAPIAccessLayer = new IntacctAPIAccessLayer())
            {
                return objIntacctAPIAccessLayer.GetData<object>(
                             RequestMethod.GET.ToString(), string.Format(IntacctAPIURL.GetInvoice, fromDate, toDate, type),
                             SecurityContext.Instance.VectorToken, SecurityContext.Instance.AppVersion, null, "application/json", true);
            }
        }

        public object LogException(IntacctException objIntacctException)
        {
            using (IntacctAPIAccessLayer objIntacctAPIAccessLayer = new IntacctAPIAccessLayer())
            {
                return objIntacctAPIAccessLayer.GetData<object>(
                             RequestMethod.POST.ToString(), IntacctAPIURL.LogException,
                             SecurityContext.Instance.VectorToken, SecurityContext.Instance.AppVersion, JsonConvert.SerializeObject(objIntacctException),
                             "application/json", true);
            }
        }

        public object UpdateReportDataByEntity(string qBDataType, string customerKey, string qBID,string keyNo, string intacctId = null)
        {
            using (IntacctAPIAccessLayer objIntacctAPIAccessLayer = new IntacctAPIAccessLayer())
            {
                return objIntacctAPIAccessLayer.GetData<object>(
                             RequestMethod.GET.ToString(), string.Format(IntacctAPIURL.UpdateReportDataByEntity, qBDataType, customerKey, qBID, keyNo, intacctId),
                             SecurityContext.Instance.VectorToken, SecurityContext.Instance.AppVersion, null, "application/json", true);
            }
        }

        public object UpdateIntacctId(string intacctType, string entityId, string intacctId)
        {
            using (IntacctAPIAccessLayer objIntacctAPIAccessLayer = new IntacctAPIAccessLayer())
            {
                return objIntacctAPIAccessLayer.GetData<object>(
                             RequestMethod.GET.ToString(), string.Format(IntacctAPIURL.UpdateIntacctId, intacctType, entityId, intacctId),
                             SecurityContext.Instance.VectorToken, SecurityContext.Instance.AppVersion, null, "application/json", true);
            }
        }

        public object UpdateInvoice(string invoiceNumber, string qBID, string type)
        {
            using (IntacctAPIAccessLayer objIntacctAPIAccessLayer = new IntacctAPIAccessLayer())
            {
                return objIntacctAPIAccessLayer.GetData<object>(
                             RequestMethod.GET.ToString(), string.Format(IntacctAPIURL.UpdateInvoice, invoiceNumber, qBID, type),
                             SecurityContext.Instance.VectorToken, SecurityContext.Instance.AppVersion, null, "application/json", true);
            }
        }

        public object UpdateBillData(string invoiceNumber, string qBID, string type)
        {
            using (IntacctAPIAccessLayer objIntacctAPIAccessLayer = new IntacctAPIAccessLayer())
            {
                return objIntacctAPIAccessLayer.GetData<object>(
                             RequestMethod.GET.ToString(), string.Format(IntacctAPIURL.UpdateBillData, invoiceNumber, qBID, type),
                             SecurityContext.Instance.VectorToken, SecurityContext.Instance.AppVersion, null, "application/json", true);
            }
        }

        public object UpdateReceiptPayment(string paymentQBID, string comments, string paymentType, System.DateTime dateUpdated, double totalAmount,
                                            string invoiceDetailsXML, string id)
        {
            using (IntacctAPIAccessLayer objIntacctAPIAccessLayer = new IntacctAPIAccessLayer())
            {
                using (Vector.Intacct.Entities.PaymentReceipt objPaymentReceipt = new Vector.Intacct.Entities.PaymentReceipt())
                {
                    objPaymentReceipt.PaymentQBID = paymentQBID;
                    objPaymentReceipt.Comments = comments;

                    objPaymentReceipt.PaymentType = paymentType;
                    objPaymentReceipt.DateUpdated = dateUpdated;
                    objPaymentReceipt.TotalAmount = totalAmount;
                    objPaymentReceipt.InvoiceDetailsXML = invoiceDetailsXML;
                    objPaymentReceipt.Id = id;

                    return objIntacctAPIAccessLayer.GetData<object>(
                            RequestMethod.POST.ToString(), IntacctAPIURL.UpdateReceiptPayment,
                            SecurityContext.Instance.VectorToken, SecurityContext.Instance.AppVersion, JsonConvert.SerializeObject(objPaymentReceipt), "application/json", true);
                }
            }
        }

        #endregion
    }
}

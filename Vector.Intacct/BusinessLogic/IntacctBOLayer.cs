using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Vector.Intacct.APIAccess;
using Vector.Intacct.DataAccess;
using Vector.Intacct.Entities;

namespace Vector.Intacct.BusinessLogic
{
    public class IntacctBOLayer : BaseLogic
    {
        #region Login

        internal object ValidateUser(string loginId, string password)
        {
            using (IntacctDALayer objIntacctDALayer = new IntacctDALayer())
            {
                return objIntacctDALayer.ValidateUser(loginId, password);
            }
        }

        #endregion

        #region Common

        internal object GetCustomer(string fromDate, string toDate)
        {
            using (IntacctDALayer objIntacctDALayer = new IntacctDALayer())
            {
                return objIntacctDALayer.GetCustomer(fromDate, toDate);
            }
        }

        internal object GetProperty(string fromDate, string toDate, string syncType)
        {
            using (IntacctDALayer objIntacctDALayer = new IntacctDALayer())
            {
                return objIntacctDALayer.GetProperty(fromDate, toDate, syncType);
            }
        }

        public object GetInvoice(string fromDate, string toDate, string type)
        {
            using (IntacctDALayer objIntacctDALayer = new IntacctDALayer())
            {
                return objIntacctDALayer.GetInvoice(fromDate, toDate, type);
            }
        }

        internal object UpdateReportDataByEntity(string qBDataType, string customerKey, string qBID,string keyNo, string intacctId = null)
        {
            using (IntacctDALayer objIntacctDALayer = new IntacctDALayer())
            {
                return objIntacctDALayer.UpdateReportDataByEntity(qBDataType, customerKey, qBID, keyNo, intacctId);
            }
        }

        public object UpdateIntacctId(string intacctType, string entityId, string intacctId)
        {
            using (IntacctDALayer objIntacctDALayer = new IntacctDALayer())
                return objIntacctDALayer.UpdateIntacctId(intacctType, entityId, intacctId);
        }

        public object UpdateInvoice(string invoiceNumber, string qBID, string type)
        {
            using (IntacctDALayer objIntacctDALayer = new IntacctDALayer())
                return objIntacctDALayer.UpdateInvoice(invoiceNumber, qBID, type);
        }

        public object UpdateIntacctIdForBill(string invoiceNumber, string qBID, string type)
        {
            using (IntacctDALayer objIntacctDALayer = new IntacctDALayer())
                return objIntacctDALayer.UpdateBillData(invoiceNumber, qBID, type);
        }


        public object LogException(IntacctException objIntacctException)
        {
            using (IntacctDALayer objIntacctDALayer = new IntacctDALayer())
            {
                return objIntacctDALayer.LogException(objIntacctException);
            }
        }

        public object UpdateReceiptPayment(string paymentQBID, string Comments, string paymentType, System.DateTime DateUpdated, double TotalAmount,
                                           string invoiceDetailsXML, string id)
        {
            using (IntacctDALayer objIntacctDALayer = new IntacctDALayer())
            {
                return objIntacctDALayer.UpdateReceiptPayment(paymentQBID, Comments,
                                                                paymentType, DateUpdated,
                                                                TotalAmount, invoiceDetailsXML, id);
            }
        }

        #endregion
    }
}

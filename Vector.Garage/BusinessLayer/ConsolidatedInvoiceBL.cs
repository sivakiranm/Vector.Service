using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vector.Common.BusinessLayer;
using Vector.Common.DataLayer;
using Vector.Common.Entities;
using Vector.Garage.DataLayer;
using Vector.Garage.Entities;

namespace Vector.Garage.BusinessLayer
{
    public class ConsolidatedInvoiceBL : DisposeLogic 
    {
        private VectorDataConnection objVectorDB;
        public ConsolidatedInvoiceBL(VectorDataConnection objVectorCon)
        {
            this.objVectorDB = objVectorCon;
        }
  
        public VectorResponse<object> CreateConsolidatedInvoice(ConsolidatedInvoiceInfo objConsolidatedInvoice)
        {
            using (var objConsolidatedInvoiceDL = new ConsolidatedInvoiceDL(objVectorDB))
            {
                var result = objConsolidatedInvoiceDL.CreateConsolidatedInvoice(objConsolidatedInvoice);

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

        public VectorResponse<object> ConsolidatedInvoiceSearch(ConsolidatedInvoiceSearch objConsolidatedInvoice)
        {
            using (var objConsolidatedInvoiceDL = new ConsolidatedInvoiceDL(objVectorDB))
            {
                var result = objConsolidatedInvoiceDL.ConsolidatedInvoiceSearch(objConsolidatedInvoice);

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

        public VectorResponse<object> ApproveFundCI(FundApproveCI objFundApproveCI)
        {
            using (var objConsolidatedInvoiceDL = new ConsolidatedInvoiceDL(objVectorDB))
            {
                var result = objConsolidatedInvoiceDL.ApproveFundCI(objFundApproveCI);

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

        public VectorResponse<object> ReporcessInvoice(FundApproveCI objInvoiceInfo)
        {
            using (var objConsolidatedInvoiceDL = new ConsolidatedInvoiceDL(objVectorDB))
            {
                var result = objConsolidatedInvoiceDL.ReporcessInvoice(objInvoiceInfo);

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

        public VectorResponse<object> RejectCITransactions(RejectCITransactions objInvoiceInfo)
        {
            using (var objConsolidatedInvoiceDL = new ConsolidatedInvoiceDL(objVectorDB))
            {
                var result = objConsolidatedInvoiceDL.RejectCITransactions(objInvoiceInfo);

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

        public VectorResponse<object> PartiallyFundCITransactions(PartiallyFundCITransactions objInvoiceInfo)
        {
            using (var objConsolidatedInvoiceDL = new ConsolidatedInvoiceDL(objVectorDB))
            {
                var result = objConsolidatedInvoiceDL.PartiallyFundCITransactions(objInvoiceInfo);

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

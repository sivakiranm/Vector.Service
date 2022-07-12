using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vector.Common.BusinessLayer;
using Vector.Common.DataLayer;
using Vector.Garage.Entities;

namespace Vector.Garage.DataLayer
{

    public class ReportDL : DisposeLogic
    {

        private Database objVectorConnection;
        private VectorDataConnection objVectorDB;

        DataSet dsData = new DataSet();

        public ReportDL(VectorDataConnection objVectorDB)
        {
            this.objVectorDB = objVectorDB;
        }

        private Database GetVectorDBInstance()
        {
            return objVectorDB.GetVectorDBConnection();
        }

        public DataSet GetProcessAndFlowMonitoringConsoleInfo(ConsoleEntity objConsoleEntity)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetMonitoringConsoleInfo.ToString(),
                                                  objConsoleEntity.Action,
                                                  objConsoleEntity.ManifestId,
                                                  objConsoleEntity.ManifestName,
                                                  objConsoleEntity.FeatureName,
                                                  objConsoleEntity.FeatureId,
                                                  objConsoleEntity.UserId);
            return res;
        }


        public DataSet GetClientListReportInfo(SearchEntity objSearchEntity, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetClientListReport.ToString(),
                                                  objSearchEntity.action,
                                                  objSearchEntity.clientName,
                                                  objSearchEntity.city,
                                                  objSearchEntity.state,
                                                  objSearchEntity.propertyName,
                                                  objSearchEntity.accountExecutiveId,
                                                  objSearchEntity.salesPerson,
                                                  objSearchEntity.billingType,
                                                  objSearchEntity.clientStatus,
                                                     userId);
            return res;
        }

        public DataSet GetVendorOverchargeReportInfo(SearchEntity objSearchEntity, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetVendorOverChargeReportInfo.ToString(),
                                               objSearchEntity.action,
                                                  objSearchEntity.clientId,
                                                  objSearchEntity.propertyId,
                                                  objSearchEntity.vendorId,
                                                  objSearchEntity.vendorCorporateId,
                                                  objSearchEntity.city,
                                                  objSearchEntity.state,
                                                  objSearchEntity.zip,
                                                  objSearchEntity.accountId,
                                                  objSearchEntity.vendorInvoiceNumber,
                                                  objSearchEntity.vectorInvoiceNumber,
                                                  objSearchEntity.invoiceFromDate,
                                                  objSearchEntity.invoiceToDate,
                                                  objSearchEntity.auditInvoiceFromDate,
                                                  objSearchEntity.auditInvoiceToDate,
                                                  objSearchEntity.accountExecutiveId,
                                                  objSearchEntity.accountStatus,
                                                  objSearchEntity.serviceCategory,
                                                  objSearchEntity.ticketStatus,
                                                     userId);

            return res;
        }

        public DataSet GetVendorListReportInfo(SearchEntity objSearchEntity, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetVendorListReportInfo.ToString(),
                                                  objSearchEntity.action,
                                                  objSearchEntity.vendorCorporateId,
                                                  objSearchEntity.vendorId,
                                                  objSearchEntity.accountId,
                                                  objSearchEntity.city,
                                                  objSearchEntity.state,
                                                  objSearchEntity.zip,
                                                  objSearchEntity.vendorStatus,
                                                     userId);
            return res;
        }

        public DataSet GetVendorCorporateListReportInfo(SearchEntity objSearchEntity, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetVendorCorporateListReportInfo.ToString(),
                                                  objSearchEntity.action,
                                                  objSearchEntity.vendorCorporateId,
                                                  objSearchEntity.accountId,
                                                  objSearchEntity.state,
                                                  objSearchEntity.city,
                                                  objSearchEntity.zip,
                                                  objSearchEntity.status,
                                                     userId);
            return res;
        }
        public DataSet GetDownloadInvoicesReportInfo(SearchEntity objSearchEntity, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetDownloadInvoicesReportInfo.ToString(),
                                                  objSearchEntity.action,
                                                  objSearchEntity.clientId,
                                                  objSearchEntity.propertyId,
                                                  objSearchEntity.vendorId,
                                                  objSearchEntity.vendorCorporateId,
                                                  objSearchEntity.InvoiceId,
                                                  objSearchEntity.accountId,
                                                  objSearchEntity.batchId,
                                                  objSearchEntity.DispatchStatus,
                                                  objSearchEntity.auditInvoiceFromDate,
                                                  objSearchEntity.auditInvoiceToDate,
                                                  userId);
            return res;
        }

        public DataSet DownloadInvoices(DownloadInvoices objDonwloadInvoices, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageDownloadInvoices.ToString(),
                                                 objDonwloadInvoices.invoiceXml,
                                                     userId);
            return res;
        }


        public DataSet ManageMissingInvoiceInfo(MarkAsNotMissing objMarkAsNotMissing, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageMissingInvoiceInfo.ToString(),
                                                 objMarkAsNotMissing.Action,
                                                 objMarkAsNotMissing.AccountId,
                                                 objMarkAsNotMissing.InvoiceDate,
                                                     userId);
            return res;
        }


        public DataSet UploadedMissingInviceInfo(MarkAsNotMissing objMarkAsNotMissing, Int64 userId, DataSet readExcel)
        {

            string finalData = readExcel.GetXml();

            finalData = finalData.Replace("_x0020__x0023_", string.Empty);
            finalData = finalData.Replace("_x0020_", string.Empty);

            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorUploadedMissingInviceInfo.ToString(),
                                                 objMarkAsNotMissing.Action,
                                                 objMarkAsNotMissing.fileName,
                                                 objMarkAsNotMissing.folderName,
                                                finalData,
                                                     userId);
            return res;
        }



        public DataSet GetTaskStatus(TaskSearch objTaskSearch, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorTaskStatusReprot.ToString(),
                                                 objTaskSearch.Action,
                                                 objTaskSearch.clientId,
                                                 objTaskSearch.propertyId,
                                                 objTaskSearch.taskCreatedFromDate,
                                                 objTaskSearch.taskCreatedToDate,
                                                 objTaskSearch.taskCompletedFromDate,
                                                 objTaskSearch.taskCompletedToDate,
                                                 objTaskSearch.ticketId,
                                                 objTaskSearch.taskId,
                                                     userId);
            return res;
        }


        public DataSet GetProcessReport(SearchEntity objSearchEntity, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorDailyProcessReport.ToString(),
                                                 objSearchEntity.action,
                                                 objSearchEntity.fromDate,
                                                 objSearchEntity.toDate,
                                                     userId);
            return res;
        }


        public DataSet GetLineitemComments(string type, Int64 lineitemId, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetLineitemComments.ToString(),
                                                 type,
                                                 lineitemId,
                                                     userId);
            return res;
        }


        public DataSet VectorMissingInvoiceHistory(string action, Int64 Accountid, string irpUniqueCode,Int64 contractId, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetMissingInvoiceReportHistory.ToString(),
                                                 action,
                                                 Accountid,
                                                 irpUniqueCode,
                                                 contractId,
                                                     userId);
            return res;
        }

        public DataSet VectorGetAccountAddEditReport(string action, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetAccountAddEditReport.ToString(),
                                                 action,
                                                     userId);
            return res;
        }


    }
}

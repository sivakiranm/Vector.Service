using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Data;
using Vector.Common.BusinessLayer;
using Vector.Common.DataLayer;
using Vector.Garage.Entities;

namespace Vector.Garage.DataLayer
{
    public class FinancialReportsDL : DisposeLogic
    {
        private Database objVectorConnection;
        private VectorDataConnection objVectorDB;
        DataSet dsData = new DataSet();

        public FinancialReportsDL(VectorDataConnection objVectorDB)
        {
            this.objVectorDB = objVectorDB;
        }

        private Database GetVectorDBInstance()
        {
            return objVectorDB.GetVectorDBConnection();
        }

        public DataSet GetRsAgingInfo(SearchEntity objSearchEntity, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("VectorRsAgingDetailsReport",
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
                objSearchEntity.contractId,
                objSearchEntity.salesPerson,
                objSearchEntity.negotiator,
                userId
                );
        }

        public DataSet GetHaulerAgingInfo(SearchEntity objSearchEntity, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("VectorHaulerAgingDetailsReport",
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
                objSearchEntity.contractId,
                objSearchEntity.salesPerson,
                objSearchEntity.negotiator,
                userId
                );
        }


        public DataSet GetInvoiceSummaryInfo(SearchEntity objSearchEntity, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();            
                 return objVectorConnection.ExecuteDataSet("VectorGetInvoiceSummaryReport",
                objSearchEntity.action,
                objSearchEntity.clientId,
                objSearchEntity.propertyId,
                objSearchEntity.vendorId,
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
                objSearchEntity.contractId,
                objSearchEntity.salesPerson,
                objSearchEntity.negotiator,
                userId
                );
        }
        public DataSet GetSavingSummaryInfo(SearchEntity objSearchEntity, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("VectorGetSavingSummaryReport",
           objSearchEntity.action,
           objSearchEntity.clientId,
           objSearchEntity.propertyId,
           objSearchEntity.vendorId,
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
           objSearchEntity.contractId,
           objSearchEntity.salesPerson,
           objSearchEntity.negotiator,
           userId
           );
        }

        public DataSet GetInvoiceLineItemInfo(SearchEntity objSearchEntity, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("VectorGetInvoiceLineItemReport",
                objSearchEntity.action,
                objSearchEntity.clientId,
                objSearchEntity.propertyId,
                objSearchEntity.vendorId,
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
                objSearchEntity.contractId,
                objSearchEntity.salesPerson,
                objSearchEntity.negotiator,
                objSearchEntity.serviceCategory,
                userId
                );
        }


        public DataSet GetNegotiationStatusInfo(SearchEntity searchReport, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("VectorGetNegotiationStatusReport", 
                searchReport.action,
                searchReport.clientId,
                searchReport.propertyId,                
                searchReport.city,
                searchReport.state,
                searchReport.zip,
                searchReport.negotiator,
                searchReport.negotiationId,
                searchReport.vendorId,
                searchReport.vendorCorporateId,
                searchReport.salesPerson,
                searchReport.accountExecutiveId,
                searchReport.negotiationStatus,
                searchReport.negotiationReasons,
                searchReport.contractEffectiveFromDate,
                searchReport.contractEffectiveToDate,
                searchReport.negotiationBeginDate,
                searchReport.negotiationEndDate,
                searchReport.contractEffectiveFromDateRangeFrom,
                searchReport.contractEffectiveFromDateRangeTo,
                searchReport.contractEffectiveToDateRangeFrom,
                searchReport.contractEffectiveToDateRangeTo,
                searchReport.negotiationStartDateRangeFrom,
                searchReport.negotiationStartDateRangeTo,
                searchReport.negotiationEndDateRangeFrom,
                searchReport.negotiationEndDateRangeTo,
                searchReport.negotiationAwardedDateRangeFrom,
                searchReport.negotiationAwardedDateRangeTo,
                userId
                );
        }

        public DataSet GetContractInfo(SearchEntity searchReport, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("VectorGetContractListReport",
                searchReport.action,
                searchReport.clientId,
                searchReport.propertyId,
                searchReport.city,
                searchReport.state,
                searchReport.zip,
                searchReport.negotiator,
                searchReport.negotiationId,
                searchReport.vendorId,
                searchReport.vendorCorporateId,
                searchReport.salesPerson,
                searchReport.accountExecutiveId,
                searchReport.negotiationStatus,                
                searchReport.contractEffectiveFromDate,
                searchReport.contractEffectiveToDate,
                searchReport.contractId,                
                userId
                );
        }

        public DataSet GetPropertyInfo(SearchEntity searchReport, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("VectorGetPropertyListReport",
                searchReport.action,
                searchReport.clientId,
                searchReport.propertyId,
                searchReport.city,
                searchReport.state,
                searchReport.zip,
                searchReport.accountId,
                searchReport.accountExecutiveId,
                searchReport.salesPerson,
                searchReport.vendorId,
                searchReport.vendorCorporateId,
                searchReport.propertyStatus,
                userId
                );
        }

        public DataSet GetMissingInvoiceReport(SearchEntity searchReport, Int64 userId)
          {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("VectorGetMissingInvoiceReportInfo", searchReport.action,
                searchReport.clientId,
                searchReport.propertyId,
                searchReport.vendorId, 
                searchReport.accountStatus,
                searchReport.accountId, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value,searchReport.accountExecutiveId,
                searchReport.billingAnalyst,
                searchReport.city,searchReport.state,searchReport.zip,
                userId
                );
        }

        public DataSet GetServiceLevelByPropertyInfo(SearchEntity searchReport, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("VectorServiceLevelByPropertyReport",
                searchReport.action,
                searchReport.clientId,
                searchReport.propertyId,
                searchReport.vendorId,
                searchReport.vendorCorporateId,
                searchReport.accountId,
                searchReport.serviceCategory,
                searchReport.propertyStatus,
                searchReport.city,
                searchReport.state,
                searchReport.zip,
                userId
                );
        }

        public DataSet GetBillGapReport(SearchEntity searchReport, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("VectorBillGapReport", searchReport.action,
                searchReport.clientId,
                searchReport.clientName, 
                searchReport.propertyId,
                searchReport.propertyName,
                searchReport.vendorName,
                searchReport.vendorId,
                searchReport.accountNumber,
                searchReport.accountId,
                searchReport.accountMode,
                searchReport.accountType,
                searchReport.contractEffectiveFromDate,
                searchReport.contractEffectiveToDate,
                searchReport.accountStatus, 
                userId
                );
        }


        public DataSet VectorGetStagingInvoices(SearchEntity searchReport, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("VectorGetStagingInvoices", searchReport.action,
                searchReport.clientId,
                searchReport.propertyId,
                searchReport.vendorId,
                searchReport.accountStatus,
                searchReport.accountId, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, searchReport.accountExecutiveId,
                searchReport.billingAnalyst,
                searchReport.city, searchReport.state, searchReport.zip,
                userId
                );
        }

    }
}

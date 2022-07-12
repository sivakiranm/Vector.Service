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
using Vector.Workbench.Entities;

namespace Vector.Workbench.DataLayer
{
    public class WidgetsDL : DisposeLogic
    {
        #region Constructor

        private Database objVectorConnection;
        private VectorDataConnection objVectorDB;

        public WidgetsDL(VectorDataConnection objVectorDB)
        {
            this.objVectorDB = objVectorDB;
        }

        private Database GetVectorDBInstance()
        {
            return objVectorDB.GetVectorDBConnection();
        }

        #endregion

        public DataSet ClientSummaryInfo(ClientSummaryInfo objClientSummaryInfo, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorClientSummaryWidget.ToString()
                                    , objClientSummaryInfo.PropertyId
                                    , objClientSummaryInfo.FromMonth
                                    , objClientSummaryInfo.FromYear
                                    , objClientSummaryInfo.ToMonth
                                    , objClientSummaryInfo.ToYear
                                    , userId);
        }


        public DataSet ClientMetrics(Int64? clientId, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorClientMetricsWidget.ToString()
                                    , clientId
                                    , userId);
        }

        public DataSet ServiceSummary(Int64? propertyId, string propertyName, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorServiceSummaryWidget.ToString()
                                    , propertyId
                                    ,propertyName
                                    , userId);
        }

        internal DataSet GetInvoiceExplorerDL(SearchInvoice objSearch, Int64 userId)
        {
            
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetInvoiceExplorerData.ToString(),
                                                        objSearch.VendorAccountId   ,
                                                        objSearch.PropertyId,
                                                        objSearch.InvoiceFromDate,
                                                        objSearch.InvoiceToDate,
                                                       userId);
        }

        public DataSet TicketByServiceType(Int64? propertyId,  Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetTicketByServiceType.ToString()
                                    , propertyId 
                                    , userId);
        }


        public DataSet VectorOpenInvoiceByClient(Int64? clientId, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorOpenInvoiceByClient.ToString()
                                    , clientId
                                    , userId);
        }

        public DataSet ManageActions(Actions objActions, Int64 userId, string supportFileDocs)
        {

            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageActions.ToString()
                                    , objActions.Action
                                    , objActions.ActionName
                                    , objActions.ActionDescription
                                    , objActions.DueDate
                                    , objActions.ActionStatus
                                    , supportFileDocs
                                    , userId);
        }

        public DataSet GetActions(Actions objActions, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetActions.ToString()
                                    , objActions.Action
                                    , objActions.ActionId 
                                    , userId);
        }

        public DataSet GetNegotiationthreesixtyReport(SearchEntity objSearch, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();

            if(string.Equals(objSearch.action, "ClientWiseInfo"))
            {
                return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorNegotiationThreeSixtyWidgetClientPropertyView.ToString()
                                    , objSearch.action
                                    , objSearch.clientId
                                    , objSearch.clientName
                                    , objSearch.propertyId
                                    , objSearch.propertyName
                                    , objSearch.vendorCorporateId
                                    , objSearch.vendorCoporateName
                                    , objSearch.vendorId
                                    , objSearch.vendorName
                                    , objSearch.city
                                    , objSearch.state
                                    , objSearch.zip
                                    , objSearch.negotiator
                                    , objSearch.propertyStatus
                                    , objSearch.propertyAddress
                                    , objSearch.negotiationId
                                    , objSearch.negotiationStatus
                                    , objSearch.contractEffectiveFromDate
                                    , objSearch.contractEffectiveToDate
                                    , objSearch.negotiationBeginDate
                                    , objSearch.negotiationEndDate
                                    , userId);
            }
            else
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorNegotiationThreeSixtyWidget.ToString()
                                    , objSearch.action
                                    , objSearch.clientId
                                    , objSearch.clientName
                                    , objSearch.propertyId
                                    , objSearch.propertyName
                                    , objSearch.vendorCorporateId
                                    , objSearch.vendorCoporateName
                                    , objSearch.vendorId
                                    , objSearch.vendorName
                                    , objSearch.city
                                    , objSearch.state
                                    , objSearch.zip
                                    , objSearch.negotiator
                                    , objSearch.propertyStatus
                                    , objSearch.propertyAddress
                                    , objSearch.negotiationId
                                    , objSearch.negotiationStatus
                                    , objSearch.contractEffectiveFromDate
                                    , objSearch.contractEffectiveToDate
                                    , objSearch.negotiationBeginDate
                                    , objSearch.negotiationEndDate
                                    , userId);
        }

        public DataSet GetNegotaitionStatusWidgetDetails(SearchEntity objSearch, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.GetNegotaitionStatusWidgetDetails.ToString()
                                    , objSearch.action
                                    , objSearch.clientId
                                    , objSearch.clientName
                                    , objSearch.propertyId
                                    , objSearch.propertyName
                                    , objSearch.vendorCorporateId
                                    , objSearch.vendorCoporateName
                                    , objSearch.vendorId
                                    , objSearch.vendorName
                                    , objSearch.city
                                    , objSearch.state
                                    , objSearch.zip
                                    , objSearch.negotiator
                                    , objSearch.propertyStatus
                                    , objSearch.propertyAddress
                                    , objSearch.negotiationId
                                    , objSearch.negotiationStatus
                                    , objSearch.contractEffectiveFromDate
                                    , objSearch.contractEffectiveToDate
                                    , objSearch.negotiationBeginDate
                                    , objSearch.negotiationEndDate
                                    ,objSearch.IsOnOff
                                    , userId);
        }



        public DataSet UpdatePersonalizedSettings(PersonalizeSettings objSearch, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.ManageWidgetFeaturePersonalization.ToString()
                                    , objSearch.Action
                                    , objSearch.Type
                                    , objSearch.FeatureId
                                    , objSearch.widgetId
                                    , objSearch.clientIds
                                    , objSearch.propertyIds
                                    , userId);
        }

        public DataSet GetWorkTrackerInfo(WorkEntity objSearch, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetWorkTrackerInfo.ToString()
                                    , objSearch.action
                                    , objSearch.userId
                                    , objSearch.userName
                                    , objSearch.workStatus
                                    , objSearch.functionalArea
                                    , objSearch.issueType
                                    , objSearch.trackingNo
                                    , objSearch.releaseId
                                    , objSearch.workId
                                    , userId);
        }

        public DataSet ManageWorkTracker(WorkEntity objSearch, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageWorkTraker.ToString()
                                    , objSearch.action
                                    , objSearch.workId
                                    , objSearch.issueType
                                    , objSearch.shortDescription
                                    , objSearch.longDescription
                                    , objSearch.functionalArea
                                    , objSearch.workStatus
                                    , objSearch.mergerWorkId
                                    , objSearch.comments
                                    , objSearch.documentXml
                                    , objSearch.releaseId
                                    , userId);
        }

        public DataSet GetProductivityMetrics(string action,string reportBy, Int64? reporterId, Int64? clientId, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetProductivityMetricsByUser.ToString()
                                    , action
                                    , reportBy
                                    , reporterId
                                    , clientId
                                    ,userId);
        }

    }
}
    
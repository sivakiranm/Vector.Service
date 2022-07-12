using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Data;
using Vector.Common.BusinessLayer;
using Vector.Common.DataLayer;
using Vector.Dashboard.Entities;

namespace Vector.Dashboard.DataLayer
{
    public class DashboardDL : DisposeLogic
    {
        private Database objVectorConnection;
        private VectorDataConnection objVectorDB;
        DataSet dsData = new DataSet();
        enum Action
        {
            DeleteDashboardWidget,
            DeleteDashboard,
            AddDashbordWidgets
        }
        public DashboardDL(VectorDataConnection objVectorDB)
        {
            this.objVectorDB = objVectorDB;
        }

        private Database GetVectorDBInstance()
        {
            return objVectorDB.GetVectorDBConnection();
        }

        public DataSet GetDashboardByUSer(string userId, string userName)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("GetVactorMasterData", "GetDashboardByUser", userId, 1, 1, "");
        }
        public DataSet GetWidgets(string userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("GetVactorMasterData", "GetWidgets", userId, 0, 0, "");
        }
        public DataSet GetWidgetsByDashboard(string dashboardId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("GetVactorMasterData", "GetWidgetsByDashboard", 1, dashboardId, 1, "");
        }

        public int AddDashbord(Vector.Dashboard.Entities.Dashboard dashborad)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteNonQuery("VectorDashboardManagement", "Create", dashborad.userId, dashborad.title, dashborad.description, dashborad.layout,
                dashborad.dashboardIcon, dashborad.dashboardColor, dashborad.widgets, 0, dashborad.isPinned, 0, 0);
        }
        public int PinUnPinDashboard(Int64 userId, Int64 dashboardId, bool isPin)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteNonQuery("VectorDashboardManagement", "PinUnPinDashboard", userId, "", "", "", "", "", "", dashboardId, isPin, 0, -1);
        }

        public DataSet GetNumbersDL()
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetDashboardNumbersData.ToString());
        }

        internal DataSet GetRevenueBySalesPersonsDL()
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetDashboardRevenueBySalesPersonsData.ToString());
        }

        internal DataSet GetHighGrowthCustomersDL()
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetDashboardHighGrowthCustomersData.ToString());
        }

        internal DataSet GetDormantPropertiesBySalespersonDL()
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetDashboardDormantPropertiesBySalespersonData.ToString());
        }

        internal DataSet GetProductiveMetricsByCustomerServiceDL()
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetProductiveMetricsByCustomerServiceData.ToString());
        }

        internal DataSet GetTaskPerformanceMetricsCustomerServiceDL()
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetTaskPerformanceMetricsCustomerServiceData.ToString());
        }

        internal DataSet GetBenchmarkingDL(Int64 propertyId, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetBenchmarkingData.ToString(),
                propertyId, userId);
        }

        internal DataSet GetProductiveMetricsByBADL()
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetProductiveMetricsByBAData.ToString());
        }

        internal DataSet GetTaskPerfomranceMetricsBADL()
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetTaskPerfomranceMetricsBAData.ToString());
        }

        internal DataSet GetOpenFundsByAccountExecutiveDL()
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetOpenFundsByAccountExecutiveData.ToString());
        }

        internal DataSet GetVectorOpenInvoiceBySalespersonDL()
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetVectorOpenInvoiceBySalespersonData.ToString());
        }

        internal DataSet GetAgingByServiceTypeDL()
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetAgingByServiceTypeData.ToString());
        }

        internal DataSet GetLeadLifetimeReportDL()
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetLeadLifetimeReportData.ToString());
        }

        internal DataSet GetSalesFunnelDL()
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetSalesFunnelData.ToString());
        }

        internal DataSet GetCampaignSummaryDL()
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetCampaignSummaryData.ToString());
        }

        internal DataSet GetExceptionByBillingAnalystDL()
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetExceptionByBillingAnalystData.ToString());
        }

        internal DataSet GetPropertiesWithoutInvoicesBySalespersonsDL()
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetPropertiesWithoutInvoicesBySalespersonsData.ToString());
        }



        internal DataSet GetVendorPastDueByBillingAnalystDL()
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetVendorPastDueByBillingAnalystData.ToString());
        }

        internal DataSet GetTaskPerformanceMetricsByAEDL()
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetTaskPerformanceMetricsByAEData.ToString());
        }

        internal DataSet GetMissingBillByBillingAnalystDL()
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetMissingBillByBillingAnalystData.ToString());
        }

        internal DataSet GetProductiveMetricsByAEDL()
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetProductiveMetricsByAEData.ToString());
        }

        internal DataSet GetInvoiceSummaryDL()
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetInvoiceSummaryData.ToString());
        }

        internal DataSet GetSavingsSummaryDL()
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetSavingsSummaryData.ToString());
        }

        internal DataSet GetAuditSummaryDL()
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetAuditSummaryData.ToString());
        }

        internal DataSet GetMissingBillDL(string receipientType, string clientName, string account, string vendor)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetMissingBillData.ToString(), receipientType, clientName, account, vendor);
        }

        internal DataSet GetVendorPendingCreditsDL(string clientName, string account, string vendor, string accountStatus)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetVendorPendingCreditsData.ToString(), clientName, account, vendor, accountStatus);
        }

        internal DataSet GetExceptionAgingDL(string clientName, string vendor, string exceptionType, string account, string fromDate, string toDate)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetExceptionAgingData.ToString(),
                                                    clientName, vendor, account, exceptionType, fromDate, toDate);
        }

        internal DataSet GetMyNumbersDL()
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetMyNumbersData.ToString());
        }

        internal DataSet GetNumberofContractsByStatusDL(string clientName, string salesPerson, Int64 propertyId, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetNumberofContractsByStatusData.ToString(),
                clientName, salesPerson, propertyId, userId);
        }

        internal DataSet GetUpcomingRenewalTrackerDL(string salesPerson, string fromDate, string toDate, string vendor)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetUpcomingRenewalTrackerData.ToString(), salesPerson, fromDate, toDate, vendor);
        }

        internal DataSet GetArchiveContractRequestDL()
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetArchiveContractRequestData.ToString());
        }

        internal DataSet GetClientVendorContractDocumentsDL(string clientName, string vendor)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorClientVendorContractDocumentsData.ToString(), clientName, vendor);
        }

        internal DataSet GetSearchForContractDL()
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetSearchForContractData.ToString());
        }

        internal DataSet GetVendorPastDueByAEDL(Int64? vendor, string accountExecutive, Int64? account, Int64? client, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetVendorPastDueByAEData.ToString(), vendor, accountExecutive, account, client, userId);
        }

        internal DataSet GetMyClientsDL(string userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetMyClientsData.ToString(), userId);
        }

        internal DataSet GetOveragesByClientDL(string client, string vendor)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetOveragesByClientData.ToString(), client, vendor);
        }

        internal DataSet GetTaskPerformanceMetricsByClientDL(Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetTaskPerformanceMetricsByClientData.ToString(), userId);
        }

        internal DataSet GetOpenTasksByServiceTypeDL(string serviceType, string client)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetOpenTasksByServiceTypeData.ToString(), serviceType, client);
        }

        internal DataSet GetUpcomingClientContractRenewalsDL(string salesPerson, Int64? clientId, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetUpcomingClientContractRenewalsData.ToString(), salesPerson, clientId,
                userId);
        }

        internal DataSet GetContractsPendingForClientApprovalDL(string salesPerson, string client)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetContractsPendingForClientApprovalData.ToString(), salesPerson, client);
        }

        internal DataSet GetV97OpenInvoicesDL(string salesPerson, string client)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetV97OpenInvoicesData.ToString(), salesPerson, client);
        }

        internal DataSet GetConsolidatedFilesFundRequestDL(string salesPerson, Int64? clientID, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetConsolidatedFilesFundRequestData.ToString(), salesPerson, clientID, userId);
        }

        internal DataSet GetQuickLinks(Int64? userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetQuickLinksInfo.ToString(), userId);
        }

        internal DataSet ManageQuickLinks(QuickLinks objQuickLinks)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageQuickLinksInfo.ToString(),
                objQuickLinks.Action,
                objQuickLinks.FeatureId,
                objQuickLinks.ManifestId,
                objQuickLinks.Type,
                objQuickLinks.UserId);
        }

        public int DeleteDashboardWidget(Int64 userId, Int64 dashboardId, Int64 widgetId, Int64 dashboardWidgetMappingId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteNonQuery(VectorEnums.StoredProcedures.VectorDashboardManagement.ToString(), Action.DeleteDashboardWidget.ToString(),
                                                        userId, "", "", "", "", "", widgetId.ToString(), dashboardId, false, dashboardWidgetMappingId, -1);
        }

        internal int DeleteDashbord(Int64 userId, Int64 dashboardId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteNonQuery(VectorEnums.StoredProcedures.VectorDashboardManagement.ToString(), Action.DeleteDashboard.ToString(),
                                                        userId, "", "", "", "", "", 0, dashboardId, false, 0, -1);
        }

        internal int AddDashbordWidgets(Entities.Dashboard objDashboard)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteNonQuery(VectorEnums.StoredProcedures.VectorDashboardManagement.ToString(), Action.AddDashbordWidgets.ToString(),
                                                        objDashboard.userId, "", "", "", "", "", objDashboard.widgets, objDashboard.dashboardId, false, 0, 0);
        }

        internal DataSet GetWorkforceUsers(string action, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetWorkforceUsers.ToString(), action, userId);
        }

        internal DataSet GetWorkforceUsersProductivityDetails(string action, string selectedUsers, Int64 loginUserId)
        {
            objVectorConnection = GetVectorDBInstance();
            if (StringManager.IsEqual(action, "Category"))
                return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetWorkforceCategory.ToString(), action, selectedUsers, loginUserId);
            else if (StringManager.IsEqual(action, "SLAUsers"))
                return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetWorkforceSLAUsers.ToString(), action, selectedUsers, loginUserId);
            else if (StringManager.IsEqual(action, "SLA"))
                return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetWorkforceSLA.ToString(), action, selectedUsers, loginUserId);
            else if (StringManager.IsEqual(action, "UnAssignedInfo"))
                return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetWorkforceUnassingedInfo.ToString(), action, selectedUsers, loginUserId);
            else return null;
        }


        internal DataSet GetWorkforceTasks(Int64 ticketId, string type, Int64? userID, Int64? manifestId, Int64? flowdetailId, string categoryType, string categoryId, string assignedUsers)
        {
            objVectorConnection = GetVectorDBInstance();
            
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetWorkforceTasks.ToString(), type,
                                        ticketId,
                                        userID,
                                        manifestId,
                                        flowdetailId,
                                        categoryType,
                                        categoryId,
                                        assignedUsers);
            
        }
    }
}

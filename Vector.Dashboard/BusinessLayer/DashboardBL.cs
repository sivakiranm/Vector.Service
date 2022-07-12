using System;
using System.Collections.Generic;
using System.Data;
using Vector.Common.BusinessLayer;
using Vector.Common.DataLayer;
using Vector.Common.Entities;
using Vector.Dashboard.DataLayer;
using Vector.Dashboard.Entities;

namespace Vector.Dashboard.BusinessLayer
{
    public class DashboardBL : DisposeLogic
    {
        private VectorDataConnection objVectorDB;
        public DashboardBL(VectorDataConnection objVectorCon)
        {
            this.objVectorDB = objVectorCon;
        }



        public VectorResponse<object> GetUserDashboards(string userId, string userName)
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                var result = dashboardDL.GetDashboardByUSer(userId, userName);
                if (DataValidationLayer.isDataSetNotNull(result))
                {

                    return new VectorResponse<object>() { ResponseData = result.Tables[0] };

                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }
        public VectorResponse<object> GetWidgets(string userId)
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                var result = dashboardDL.GetWidgets(userId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {

                    return new VectorResponse<object>() { ResponseData = result.Tables[0] };

                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }
        public VectorResponse<object> GetDashboardWidgets(string dashboardId)
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                var result = dashboardDL.GetWidgetsByDashboard(dashboardId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {

                    return new VectorResponse<object>() { ResponseData = result.Tables[0] };

                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }
        //public Dashboards GetUserDashboards(string userId, string userName)
        //{

        //    Dashboards dashboards = null;

        //    try
        //    {
        //        dashboards = new Dashboards();

        //        if (userName.Equals("ClientUser")) //Client Property Manager
        //        {
        //            dashboards.userDashboards = GetClientPropertyManagerDashboardInfo();
        //        }
        //        else if (userName.Equals("CSPUser")) //Customer Support User
        //        {
        //            dashboards.userDashboards = GetCustomerSupportUserDashboardInfo();
        //        }
        //        else 
        //        {
        //            dashboards.userDashboards = GetStakeHolderDashboardInfo();
        //        }


        //        //dashboards.StakeHolder = GetStakeHolderDashboardInfo();
        //        //dashboards.ClientPropertyManager = GetClientPropertyManagerDashboardInfo();
        //        //dashboards.CustomerSupportUser = GetCustomerSupportUserDashboardInfo();

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return dashboards;

        //}

        private List<DashboardInfo> GetStakeHolderDashboardInfo()
        {

            List<DashboardInfo> StakeHolderInfo = null;
            DashboardInfo dashboardinfo = null;
            List<Widget> widgetsInfo = null;
            Widget widget = null;

            try
            {

                StakeHolderInfo = new List<DashboardInfo>();
                dashboardinfo = new DashboardInfo();

                dashboardinfo.colorCode = "overview-box-1";
                dashboardinfo.dashboardFormat = "layouttwobytwo";
                dashboardinfo.dashboardId = 1;
                dashboardinfo.dashboardTitle = "Dashboard A";
                dashboardinfo.icon = "pi pi-id-card";
                dashboardinfo.isActive = true;
                dashboardinfo.isPinned = false;

                widgetsInfo = new List<Widget>();
                widget = new Widget();
                widget.widgetId = 1;
                widget.widgetTitle = "Top Five Properties";
                widgetsInfo.Add(widget);

                widget = new Widget();
                widget.widgetId = 2;
                widget.widgetTitle = "Bottom Five Properties";
                widgetsInfo.Add(widget);

                widget = new Widget();
                widget.widgetId = 3;
                widget.widgetTitle = "Savings Summary";
                widgetsInfo.Add(widget);

                widget = new Widget();
                widget.widgetId = 4;
                widget.widgetTitle = "Property Carousel View";
                widgetsInfo.Add(widget);

                dashboardinfo.widgets = widgetsInfo;

                StakeHolderInfo.Add(dashboardinfo);

                dashboardinfo = new DashboardInfo();
                dashboardinfo.colorCode = "overview-box-2";
                dashboardinfo.dashboardFormat = "inprogress";
                dashboardinfo.dashboardId = 2;
                dashboardinfo.dashboardTitle = "Dashboard B";
                dashboardinfo.icon = "pi pi-id-card";
                dashboardinfo.isActive = false;
                dashboardinfo.isPinned = false;

                widgetsInfo = new List<Widget>();
                widget = new Widget();
                widget.widgetId = 5;
                widget.widgetTitle = "Calls to Actions";
                widgetsInfo.Add(widget);

                widget = new Widget();
                widget.widgetId = 6;
                widget.widgetTitle = "Hauler Payables";
                widgetsInfo.Add(widget);

                widget = new Widget();
                widget.widgetId = 7;
                widget.widgetTitle = "Rs Payables";
                widgetsInfo.Add(widget);

                dashboardinfo.widgets = widgetsInfo;

                StakeHolderInfo.Add(dashboardinfo);

                dashboardinfo = new DashboardInfo();
                dashboardinfo.colorCode = "overview-box-3";
                dashboardinfo.dashboardFormat = "inprogress";
                dashboardinfo.dashboardId = 3;
                dashboardinfo.dashboardTitle = "Dashboard C";
                dashboardinfo.icon = "pi pi-id-card";
                dashboardinfo.isActive = false;
                dashboardinfo.isPinned = false;

                widgetsInfo = new List<Widget>();
                widget = new Widget();
                widget.widgetId = 8;
                widget.widgetTitle = "BaseLine Track";
                widgetsInfo.Add(widget);

                widget = new Widget();
                widget.widgetId = 9;
                widget.widgetTitle = "Service Savings";
                widgetsInfo.Add(widget);

                widget = new Widget();
                widget.widgetId = 10;
                widget.widgetTitle = "Equipment Savings";
                widgetsInfo.Add(widget);

                widget = new Widget();
                widget.widgetId = 11;
                widget.widgetTitle = "Management Fee";
                widgetsInfo.Add(widget);

                dashboardinfo.widgets = widgetsInfo;

                StakeHolderInfo.Add(dashboardinfo);

                dashboardinfo = new DashboardInfo();
                dashboardinfo.colorCode = "overview-box-4";
                dashboardinfo.dashboardFormat = "inprogress";
                dashboardinfo.dashboardId = 4;
                dashboardinfo.dashboardTitle = "Dashboard D";
                dashboardinfo.icon = "pi pi-id-card";
                dashboardinfo.isActive = false;
                dashboardinfo.isPinned = false;

                widgetsInfo = new List<Widget>();
                widget = new Widget();
                widget.widgetId = 8;
                widget.widgetTitle = "Service Requiest ";
                widgetsInfo.Add(widget);

                widget = new Widget();
                widget.widgetId = 9;
                widget.widgetTitle = "Tickets";
                widgetsInfo.Add(widget);

                widget = new Widget();
                widget.widgetId = 10;
                widget.widgetTitle = "Invoices";
                widgetsInfo.Add(widget);

                widget = new Widget();
                widget.widgetId = 11;
                widget.widgetTitle = "Payments";
                widgetsInfo.Add(widget);

                dashboardinfo.widgets = widgetsInfo;

                StakeHolderInfo.Add(dashboardinfo);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return StakeHolderInfo;
        }

        private List<DashboardInfo> GetClientPropertyManagerDashboardInfo()
        {

            List<DashboardInfo> ClientPropertyManagerInfo = null;
            DashboardInfo dashboardinfo = null;
            List<Widget> widgetsInfo = null;
            Widget widget = null;

            try
            {

                ClientPropertyManagerInfo = new List<DashboardInfo>();
                dashboardinfo = new DashboardInfo();

                dashboardinfo.colorCode = "overview-box-1";
                dashboardinfo.dashboardFormat = "layouttwobyone";
                dashboardinfo.dashboardId = 1;
                dashboardinfo.dashboardTitle = "Property View";
                dashboardinfo.icon = "pi pi-id-card";
                dashboardinfo.isActive = true;
                dashboardinfo.isPinned = false;

                widgetsInfo = new List<Widget>();
                widget = new Widget();
                widget.widgetId = 1;
                widget.widgetTitle = "Property Carousel View";
                widgetsInfo.Add(widget);

                widget = new Widget();
                widget.widgetId = 2;
                widget.widgetTitle = "Service Summary";
                widgetsInfo.Add(widget);

                widget = new Widget();
                widget.widgetId = 3;
                widget.widgetTitle = "Calls to Actions";
                widgetsInfo.Add(widget);

                dashboardinfo.widgets = widgetsInfo;

                ClientPropertyManagerInfo.Add(dashboardinfo);

                dashboardinfo = new DashboardInfo();
                dashboardinfo.colorCode = "overview-box-1";
                dashboardinfo.dashboardFormat = "inprogress";
                dashboardinfo.dashboardId = 2;
                dashboardinfo.dashboardTitle = "Benchmark";
                dashboardinfo.icon = "pi pi-id-card";
                dashboardinfo.isActive = false;
                dashboardinfo.isPinned = false;

                widgetsInfo = new List<Widget>();
                widget = new Widget();
                widget.widgetId = 5;
                widget.widgetTitle = "Portfolio rank";
                widgetsInfo.Add(widget);

                widget = new Widget();
                widget.widgetId = 6;
                widget.widgetTitle = "Industary Benchmark";
                widgetsInfo.Add(widget);

                widget = new Widget();
                widget.widgetId = 7;
                widget.widgetTitle = "Tips";
                widgetsInfo.Add(widget);

                dashboardinfo.widgets = widgetsInfo;

                ClientPropertyManagerInfo.Add(dashboardinfo);

                dashboardinfo = new DashboardInfo();
                dashboardinfo.colorCode = "overview-box-3";
                dashboardinfo.dashboardFormat = "inprogress";
                dashboardinfo.dashboardId = 3;
                dashboardinfo.dashboardTitle = "Payables";
                dashboardinfo.icon = "pi pi-id-card";
                dashboardinfo.isActive = false;
                dashboardinfo.isPinned = false;

                widgetsInfo = new List<Widget>();
                widget = new Widget();
                widget.widgetId = 5;
                widget.widgetTitle = "Calls to Actions";
                widgetsInfo.Add(widget);

                widget = new Widget();
                widget.widgetId = 6;
                widget.widgetTitle = "Hauler Payables";
                widgetsInfo.Add(widget);

                widget = new Widget();
                widget.widgetId = 7;
                widget.widgetTitle = "Rs Payables";
                widgetsInfo.Add(widget);

                dashboardinfo.widgets = widgetsInfo;

                ClientPropertyManagerInfo.Add(dashboardinfo);

                dashboardinfo = new DashboardInfo();
                dashboardinfo.colorCode = "overview-box-3";
                dashboardinfo.dashboardFormat = "inprogress";
                dashboardinfo.dashboardId = 4;
                dashboardinfo.dashboardTitle = "Ops SnapShot";
                dashboardinfo.icon = "pi pi-id-card";
                dashboardinfo.isActive = false;
                dashboardinfo.isPinned = false;

                widgetsInfo = new List<Widget>();
                widget = new Widget();
                widget.widgetId = 8;
                widget.widgetTitle = "Service Requiest ";
                widgetsInfo.Add(widget);

                widget = new Widget();
                widget.widgetId = 9;
                widget.widgetTitle = "Tickets";
                widgetsInfo.Add(widget);

                widget = new Widget();
                widget.widgetId = 10;
                widget.widgetTitle = "Invoices";
                widgetsInfo.Add(widget);

                widget = new Widget();
                widget.widgetId = 11;
                widget.widgetTitle = "Payments";
                widgetsInfo.Add(widget);

                dashboardinfo.widgets = widgetsInfo;

                ClientPropertyManagerInfo.Add(dashboardinfo);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ClientPropertyManagerInfo;
        }

        private List<DashboardInfo> GetCustomerSupportUserDashboardInfo()
        {

            List<DashboardInfo> CustomerSupportUserInfo = null;
            DashboardInfo dashboardinfo = null;
            List<Widget> widgetsInfo = null;
            Widget widget = null;

            try
            {

                CustomerSupportUserInfo = new List<DashboardInfo>();
                dashboardinfo = new DashboardInfo();

                dashboardinfo.colorCode = "overview-box-2";
                dashboardinfo.dashboardFormat = "layoutonebytwo";
                dashboardinfo.dashboardId = 1;
                dashboardinfo.dashboardTitle = "Tickets";
                dashboardinfo.icon = "pi pi-id-card";
                dashboardinfo.isActive = true;
                dashboardinfo.isPinned = false;

                widgetsInfo = new List<Widget>();
                widget = new Widget();
                widget.widgetId = 1;
                widget.widgetTitle = "Metrics";
                widgetsInfo.Add(widget);

                widget = new Widget();
                widget.widgetId = 2;
                widget.widgetTitle = "Queues";
                widgetsInfo.Add(widget);

                dashboardinfo.widgets = widgetsInfo;

                CustomerSupportUserInfo.Add(dashboardinfo);

                dashboardinfo = new DashboardInfo();
                dashboardinfo.colorCode = "overview-box-2";
                dashboardinfo.dashboardFormat = "inprogress";
                dashboardinfo.dashboardId = 2;
                dashboardinfo.dashboardTitle = "Engagement Snapshot";
                dashboardinfo.icon = "pi pi-id-card";
                dashboardinfo.isActive = false;
                dashboardinfo.isPinned = false;

                widgetsInfo = new List<Widget>();
                widget = new Widget();
                widget.widgetId = 5;
                widget.widgetTitle = "Savings Summary";
                widgetsInfo.Add(widget);

                widget = new Widget();
                widget.widgetId = 6;
                widget.widgetTitle = "Service Summary";
                widgetsInfo.Add(widget);

                widget = new Widget();
                widget.widgetId = 7;
                widget.widgetTitle = "Enguagement Snapshot";
                widgetsInfo.Add(widget);

                dashboardinfo.widgets = widgetsInfo;

                CustomerSupportUserInfo.Add(dashboardinfo);

                dashboardinfo = new DashboardInfo();
                dashboardinfo.colorCode = "overview-box-4";
                dashboardinfo.dashboardFormat = "inprogress";
                dashboardinfo.dashboardId = 3;
                dashboardinfo.dashboardTitle = "Implementations";
                dashboardinfo.icon = "pi pi-id-card";
                dashboardinfo.isActive = false;
                dashboardinfo.isPinned = false;

                widgetsInfo = new List<Widget>();
                widget = new Widget();
                widget.widgetId = 5;
                widget.widgetTitle = "Open Items";
                widgetsInfo.Add(widget);

                widget = new Widget();
                widget.widgetId = 6;
                widget.widgetTitle = "Work In Progress";
                widgetsInfo.Add(widget);

                widget = new Widget();
                widget.widgetId = 7;
                widget.widgetTitle = "Completed";
                widgetsInfo.Add(widget);

                dashboardinfo.widgets = widgetsInfo;

                CustomerSupportUserInfo.Add(dashboardinfo);

                dashboardinfo = new DashboardInfo();
                dashboardinfo.colorCode = "overview-box-1";
                dashboardinfo.dashboardFormat = "inprogress";
                dashboardinfo.dashboardId = 4;
                dashboardinfo.dashboardTitle = "Tickets";
                dashboardinfo.icon = "pi pi-id-card";
                dashboardinfo.isActive = false;
                dashboardinfo.isPinned = false;

                widgetsInfo = new List<Widget>();
                widget = new Widget();
                widget.widgetId = 8;
                widget.widgetTitle = "Hauler Pending Credits";
                widgetsInfo.Add(widget);

                widget = new Widget();
                widget.widgetId = 9;
                widget.widgetTitle = "Hauler Past Due";
                widgetsInfo.Add(widget);

                widget = new Widget();
                widget.widgetId = 10;
                widget.widgetTitle = "RS Past Due";
                widgetsInfo.Add(widget);

                dashboardinfo.widgets = widgetsInfo;

                CustomerSupportUserInfo.Add(dashboardinfo);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return CustomerSupportUserInfo;
        }

        public VectorResponse<object> AddDashbord(Vector.Dashboard.Entities.Dashboard objDashboard)
        {
            try
            {

                //calling the db save or update method, if successfully inserted or updated then return true other wise return false
                //return true;

                using (var dashboardDL = new DashboardDL(objVectorDB))
                {
                    int result = dashboardDL.AddDashbord(objDashboard);
                    if (result > 0)
                    {

                        return new VectorResponse<object>() { ResponseMessage = "Dashboard Added Successfully." };

                    }
                    else
                    {
                        return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "Unable to Add new Dashboard, Please try again." } };

                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            //  return true;
        }

        public VectorResponse<object> PinUnPinDashboard(Int64 userId, Int64 dashboardId, bool isPin)
        {
            try
            {

                //calling the db save or update method, if successfully inserted or updated then return true other wise return false
                //related to the pin and unpin functioning
                //return true;

                using (var dashboardDL = new DashboardDL(objVectorDB))
                {
                    int result = dashboardDL.PinUnPinDashboard(userId, dashboardId, isPin);
                    if (result > 0)
                    {

                        return new VectorResponse<object>() { ResponseMessage = "Dashboard Pinned/unPinned Successfully." };

                    }
                    else
                    {
                        return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "Unable to Pin/UnPin Dashboard, Please try again." } };

                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            //  return true;
        }
        public VectorResponse<object> GetNumbersBL()
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                var result = dashboardDL.GetNumbersDL();
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    DataTable dtData = DataManager.Pivot(result.Tables[0], result.Tables[0].Columns["AttributeDates"], result.Tables[0].Columns["AttributeValue"]);
                    return new VectorResponse<object>() { ResponseData = dtData };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }

        public VectorResponse<object> GetRevenueBySalesPersonsBL()
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                var result = dashboardDL.GetRevenueBySalesPersonsDL();
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

        public VectorResponse<object> GetHighGrowthCustomersBL()
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                var result = dashboardDL.GetHighGrowthCustomersDL();
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
        public VectorResponse<object> GetDormantPropertiesBySalespersonBL()
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                var result = dashboardDL.GetDormantPropertiesBySalespersonDL();
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

        public VectorResponse<object> GetProductiveMetricsByCustomerServiceBL()
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                var result = dashboardDL.GetProductiveMetricsByCustomerServiceDL();
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

        public VectorResponse<object> GetTaskPerformanceMetricsCustomerServiceBL()
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                var result = dashboardDL.GetTaskPerformanceMetricsCustomerServiceDL();
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

        public VectorResponse<object> GetBenchmarkingBL(Int64 propertyId, Int64 userId)
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                var result = dashboardDL.GetBenchmarkingDL(propertyId, userId);
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

        public VectorResponse<object> GetProductiveMetricsByBABL()
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                var result = dashboardDL.GetProductiveMetricsByBADL();
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

        public VectorResponse<object> GetTaskPerfomranceMetricsBABL()
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                var result = dashboardDL.GetTaskPerfomranceMetricsBADL();
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

        public VectorResponse<object> GetOpenFundsByAccountExecutiveBL()
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                var result = dashboardDL.GetOpenFundsByAccountExecutiveDL();
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

        public VectorResponse<object> GetVectorOpenInvoiceBySalespersonBL()
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                var result = dashboardDL.GetVectorOpenInvoiceBySalespersonDL();
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

        public VectorResponse<object> GetAgingByServiceTypeBL()
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                var result = dashboardDL.GetAgingByServiceTypeDL();
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

        public VectorResponse<object> GetLeadLifetimeReportBL()
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                var result = dashboardDL.GetLeadLifetimeReportDL();
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

        public VectorResponse<object> GetSalesFunnelBL()
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                var result = dashboardDL.GetSalesFunnelDL();
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

        public VectorResponse<object> GetCampaignSummaryBL()
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                var result = dashboardDL.GetCampaignSummaryDL();
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

        public VectorResponse<object> GetExceptionByBillingAnalystBL()
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                var result = dashboardDL.GetExceptionByBillingAnalystDL();
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

        public VectorResponse<object> GetPropertiesWithoutInvoicesBySalespersonsBL()
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                var result = dashboardDL.GetPropertiesWithoutInvoicesBySalespersonsDL();
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



        public VectorResponse<object> GetVendorPastDueByBillingAnalystBL()
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                var result = dashboardDL.GetVendorPastDueByBillingAnalystDL();
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

        public VectorResponse<object> GetTaskPerformanceMetricsByAEBL()
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                var result = dashboardDL.GetTaskPerformanceMetricsByAEDL();
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

        public VectorResponse<object> GetMissingBillByBillingAnalystBL()
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                var result = dashboardDL.GetMissingBillByBillingAnalystDL();
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

        public VectorResponse<object> GetProductiveMetricsByAEBL()
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                var result = dashboardDL.GetProductiveMetricsByAEDL();
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

        public VectorResponse<object> GetInvoiceSummaryBL()
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                var result = dashboardDL.GetInvoiceSummaryDL();
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    //DataTable auditData = result.Tables[0].Copy();
                    //DataTable throughOutputData = result.Tables[0].Copy();
                    //DataTable pivotedAudit = DataManager.Pivot(DataManager.DeleteColumn(auditData, "ThroughOutput"), result.Tables[0].Columns["InvoiceDates"], result.Tables[0].Columns["AuditedInvoice"]);

                    //DataTable pivotedThroughOutput = DataManager.Pivot(DataManager.DeleteColumn(throughOutputData, "AuditedInvoice"), result.Tables[0].Columns["InvoiceDates"], result.Tables[0].Columns["ThroughOutput"]);
                    //DataTable mergeddata = DataManager.MergeDataTables(RenameTableColumnName(pivotedAudit, "Audited"), RenameTableColumnName(pivotedThroughOutput, "ThroughOutput"), "UniqueId").Copy();
                    //mergeddata.PrimaryKey = null;
                    //DataManager.DeleteColumn(mergeddata, "UniqueId");
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }

        private DataTable RenameTableColumnName(DataTable data, string columnName)
        {
            foreach (DataColumn item in data.Columns)
            {
                if (item.ColumnName != "UniqueId")
                    item.ColumnName = item.ColumnName + "(" + columnName + ")";
            }

            return data;
        }

        public VectorResponse<object> GetSavingsSummaryBL()
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                var result = dashboardDL.GetSavingsSummaryDL();
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

        public VectorResponse<object> GetAuditSummaryBL()
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                var result = dashboardDL.GetAuditSummaryDL();
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

        public VectorResponse<object> GetMissingBillBL(string receipientType, string clientName, string account, string vendor)
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                var result = dashboardDL.GetMissingBillDL(receipientType, clientName, account, vendor);
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

        public VectorResponse<object> GetVendorPendingCreditsBL(string clientName, string account, string vendor, string accountStatus)
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                var result = dashboardDL.GetVendorPendingCreditsDL(clientName, account, vendor, accountStatus);
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

        public VectorResponse<object> GetExceptionAgingBL(string clientName, string vendor, string exceptionType, string account, string fromDate, string toDate)
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                var result = dashboardDL.GetExceptionAgingDL(clientName, vendor, account, exceptionType, fromDate, toDate);
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

        public VectorResponse<object> GetMyNumbersBL()
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                var result = dashboardDL.GetMyNumbersDL();
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

        public VectorResponse<object> GetNumberofContractsByStatusBL(string clientName, string salesPerson, Int64 propertyId, Int64 userId)
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                var result = dashboardDL.GetNumberofContractsByStatusDL(clientName, salesPerson, propertyId, userId);
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
        public VectorResponse<object> GetUpcomingRenewalTrackerBL(string salesPerson, string fromDate, string toDate, string vendor)
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                var result = dashboardDL.GetUpcomingRenewalTrackerDL(salesPerson, fromDate, toDate, vendor);
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

        public VectorResponse<object> GetArchiveContractRequestBL()
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                var result = dashboardDL.GetArchiveContractRequestDL();
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

        public VectorResponse<object> GetClientVendorContractDocumentsBL(string clientName, string vendor)
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                var result = dashboardDL.GetClientVendorContractDocumentsDL(clientName, vendor);
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

        public VectorResponse<object> GetSearchForContractBL()
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                var result = dashboardDL.GetSearchForContractDL();
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

        public VectorResponse<object> GetVendorPastDueByAEBL(Int64? vendor, string accountExecutive, Int64? account, Int64? client, Int64 userId)
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                var result = dashboardDL.GetVendorPastDueByAEDL(vendor, accountExecutive, account, client, userId);
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

        public VectorResponse<object> GetMyClientsBL(string userId)
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                var result = dashboardDL.GetMyClientsDL(userId);
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

        public VectorResponse<object> GetOveragesByClientBL(string client, string vendor)
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                var result = dashboardDL.GetOveragesByClientDL(client, vendor);
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

        public VectorResponse<object> GetTaskPerformanceMetricsByClientBL(Int64 userId)
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                var result = dashboardDL.GetTaskPerformanceMetricsByClientDL(userId);
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

        public VectorResponse<object> GetOpenTasksByServiceTypeBL(string serviceType, string client)
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                var result = dashboardDL.GetOpenTasksByServiceTypeDL(serviceType, client);
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

        public VectorResponse<object> GetUpcomingClientContractRenewalsBL(string salesPerson, Int64? clientId, Int64 userId)
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                var result = dashboardDL.GetUpcomingClientContractRenewalsDL(salesPerson, clientId, userId);
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

        public VectorResponse<object> GetContractsPendingForClientApprovalBL(string salesPerson, string client)
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                var result = dashboardDL.GetContractsPendingForClientApprovalDL(salesPerson, client);
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

        public VectorResponse<object> GetV97OpenInvoicesBL(string salesPerson, string client)
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                var result = dashboardDL.GetV97OpenInvoicesDL(salesPerson, client);
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

        public VectorResponse<object> GetConsolidatedFilesFundRequestBL(string salesPerson, Int64? clientId, Int64 userId)
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                var result = dashboardDL.GetConsolidatedFilesFundRequestDL(salesPerson, clientId, userId);
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

        public VectorResponse<object> GetQuickLinks(Int64? userId)
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                var result = dashboardDL.GetQuickLinks(userId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[0].TableName = "quickInfo";
                    result.Tables[1].TableName = "processManifest";

                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }

        public VectorResponse<object> ManageQuickLinks(QuickLinks objQuickLinks)
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                var result = dashboardDL.ManageQuickLinks(objQuickLinks);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[0].TableName = "result";
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }

        public VectorResponse<object> DeleteDashbord(Int64 userId, Int64 dashboardId)
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                int result = dashboardDL.DeleteDashbord(userId, dashboardId);
                if (result > 0)
                {
                    return new VectorResponse<object>() { ResponseMessage = "Dashboard deleted Successfully." };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "Unable to delete Dashboard, Please try again." } };
                }
            }
        }

        public VectorResponse<object> DeleteDashboardWidget(Int64 userId, Int64 dashboardId, Int64 widgetId, Int64 dashboardWidgetMappingId)
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                int result = dashboardDL.DeleteDashboardWidget(userId, dashboardId, widgetId, dashboardWidgetMappingId);
                if (result > 0)
                {
                    return new VectorResponse<object>() { ResponseMessage = "Widget deleted Successfully." };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "Unable to delete Widget, Please try again." } };
                }
            }
        }
        public VectorResponse<object> AddDashbordWidgets(Entities.Dashboard objDashboard)
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                int result = dashboardDL.AddDashbordWidgets(objDashboard);
                if (result == 1)
                {
                    return new VectorResponse<object>() { ResponseMessage = "Widget(s) Added Successfully." };
                }
                else
                {
                    return new VectorResponse<object>()
                    {
                        Error = new Error()
                        {
                            ErrorDescription = result == 2 ? "Widget(s) already Included." : "Unable to add Widget(s), Please try again."
                        }
                    };
                }
            }
        }

        public VectorResponse<object> GetWorkforceUsers(Int64 userId)
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                var result = dashboardDL.GetWorkforceUsers("GetUsers", userId);
                if (!DataManager.IsNullOrEmptyDataSet(result))
                {
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }

        public VectorResponse<object> GetWorkforceUsersProductivityDetails(string action, string selectedUsers, Int64 loginUserId)
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                var result = dashboardDL.GetWorkforceUsersProductivityDetails(action, selectedUsers, loginUserId);
                if (!DataManager.IsNullOrEmptyDataSet(result))
                {
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }

        public VectorResponse<object> GetWorkforceTasks(Int64 ticketId, string type,Int64? userID, Int64? manifestId, Int64? flowdetailId, string categoryType, string categoryId, string assignedUsers)
        {
            using (var dashboardDL = new DashboardDL(objVectorDB))
            {
                var result = dashboardDL.GetWorkforceTasks(ticketId, type, userID, manifestId, flowdetailId, categoryType, categoryId, assignedUsers);
                if (!DataManager.IsNullOrEmptyDataSet(result))
                {
                    result.Tables[VectorConstants.Zero].TableName = "summary";
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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vector.API.Handlers;
using Vector.Common.DataLayer;
using Vector.Common.Entities;
using Vector.Dashboard.BusinessLayer;
using Vector.Garage.BusinessLayer;
using Vector.Garage.Entities;
using Vector.Master.Entities;
using Vector.Master.BusinessLayer;
using Vector.UserManagement.ClientInfo;
using Vector.Dashboard.Entities;

namespace Vector.API.Controllers
{
    [RoutePrefix("api/dashboard")]
    [VectorActionAutorizationFilter]
    public class DashboardController : ApiController
    {


        #region "METHOD TO RETURN THE DATA IN THE FORM OF JSON OBJECT "

        [HttpGet]
        [Route("GetUserDashboards")]

        public IHttpActionResult GetUserDashboards(string userId, string userName)
        {

            //if (clientinformation == null)
            //{
            //    return BadRequest("Invalid Data.");
            //}

            //Dashboards dashboards = null;
            // DashboardBL dashboardbl = new DashboardBL();
            // dashboards = dashboardbl.GetUserDashboards(userId, userName);
            using (var dashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(dashboardBL.GetUserDashboards(userId, userName));

            }

            //var model = new
            //{
            //    response = "Success",
            //    responseMessage = "Login Success, Kindly Secure Token",
            //    responseCode = "" + "200" + "",
            //    responseData = dashboards,

            //};
            //return Ok(model);
            // return VectorResponseHandler.GetVectorResponse(dashboards);
            // return VectorResponseHandler.GetVectorResponse(new VectorResponse<Object>() { ResponseData = dashboards,});

        }

        #endregion
        #region "METHOD TO RETURN THE DATA IN THE FORM OF JSON OBJECT "

        [HttpGet]
        [Route("GetWidgets")]

        public IHttpActionResult GetWidgets(string userId)
        {

            using (var dashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(dashboardBL.GetWidgets(userId));

            }

        }

        #endregion
        #region "METHOD TO RETURN THE DATA IN THE FORM OF JSON OBJECT "

        [HttpGet]
        [Route("GetDashboardWidgets")]

        public IHttpActionResult GetDashboardWidgets(string dashboardId)
        {

            //if (clientinformation == null)
            //{
            //    return BadRequest("Invalid Data.");
            //}

            //Dashboards dashboards = null;
            // DashboardBL dashboardbl = new DashboardBL();
            // dashboards = dashboardbl.GetUserDashboards(userId, userName);
            using (var dashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(dashboardBL.GetDashboardWidgets(dashboardId));

            }

            //var model = new
            //{
            //    response = "Success",
            //    responseMessage = "Login Success, Kindly Secure Token",
            //    responseCode = "" + "200" + "",
            //    responseData = dashboards,

            //};
            //return Ok(model);
            // return VectorResponseHandler.GetVectorResponse(dashboards);
            // return VectorResponseHandler.GetVectorResponse(new VectorResponse<Object>() { ResponseData = dashboards,});

        }

        #endregion

        #region"                METHOD TO INSERT OR UPDATE THE DASHBOARD INFORMATION                "

        [HttpPost]
        [Route("AddDashbord")]

        public IHttpActionResult AddDashbord([FromBody] Vector.Dashboard.Entities.Dashboard objDashboard)
        {

            //if (clientinformation == null)
            //{
            //    return BadRequest("Invalid Data.");
            //}
            using (var dashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(dashboardBL.AddDashbord(objDashboard));

            }
            //bool blnSaveorUpdateStatus = false;
            //DashboardBL dashboardbl = new DashboardBL(new VectorDataConnection());
            //blnSaveorUpdateStatus = dashboardbl.AddDashbord(objDashboard);

            //if(blnSaveorUpdateStatus)
            //{
            //    var model = new
            //    {
            //        response = "Success",
            //        responseMessage = "Dashboard Added Successfully.",
            //        responseCode = "" + "200" + "",
            //        responseData = "[]",
            //    };
            //    return Ok(model);
            //}
            //else
            //{
            //    var model = new
            //    {
            //        response = "Failure",
            //        responseMessage = "Unable to Add new Dashboard, Please try again.",
            //        responseCode =  "404",
            //        responseData = "[]",
            //    };
            //    return Ok(model);
            //}

        }

        #endregion

        #region"                METHOD TO INSERT OR UPDATE THE PIN OR UNPIN DASHBOARD INFORMATION                "

        [HttpPost]
        [Route("PinUnPinDashboard")]

        public IHttpActionResult PinUnPinDashboard(Int64 userId, Int64 dashboardId, bool isPin)
        {
            //TODO : Implement Using in all methods
            using (var dashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(dashboardBL.PinUnPinDashboard(userId, dashboardId, isPin));

            }
            //bool blnSaveorUpdateStatus = false;
            //DashboardBL dashboardbl = new DashboardBL(new VectorDataConnection());
            //blnSaveorUpdateStatus = dashboardbl.PinUnPinDashboard(userId, dashboardId, isPin);

            //if (blnSaveorUpdateStatus)
            //{
            //    var model = new
            //    {
            //        response = "Success",
            //        responseMessage = "Dashboard Pinned/unPinned Successfully.",
            //        responseCode = "" + "200" + "",
            //        responseData = "[]",
            //    };
            //    return Ok(model);
            //}
            //else
            //{
            //    var model = new
            //    {
            //        response = "Failure",
            //        responseMessage = "Unable to Pin/UnPin Dashboard, Please try again.",
            //        responseCode = "" + "404" + "",
            //        responseData = "[]",
            //    };
            //    return Ok(model);
            //}

        }

        #endregion        

        #region"                METHOD TO BOTTOM FIVE PROPERTY RS COST THE DASHBOARD INFORMATION             "
        [HttpGet]
        [Route("BottomFiveRSCOST")]

        public IHttpActionResult Getbottomfiverscostproperty()
        {

            //if (clientinformation == null)
            //{
            //    return BadRequest("Invalid Data.");
            //}

            bottomfivepropertiesInformation dashboards = null;
            bottomfivepropertiesbl bottomfivepropertiesbl = new bottomfivepropertiesbl();
            dashboards = bottomfivepropertiesbl.Getbottomfiverscostproperty();

            var model = new
            {
                response = "Success",
                responseMessage = "Login Success, Kindly Secure Token",
                responseCode = "" + "200" + "",
                responseData = dashboards,
            };
            return Ok(model);

        }
        #endregion

        #region  "                METHOD TO BOTTOM FIVE PROPERTY HAULER COST THE DASHBOARD INFORMATION                "
        [HttpGet]
        [Route("BottomFiveHAULERCOST")]

        public IHttpActionResult Getbottomfivehaulercostproperty()
        {

            //if (clientinformation == null)
            //{
            //    return BadRequest("Invalid Data.");
            //}

            bottomfivepropertiesInformation dashboards = null;
            bottomfivepropertiesbl bottomfivepropertiesbl = new bottomfivepropertiesbl();
            dashboards = bottomfivepropertiesbl.Getbottomfivehaulercostproperty();

            var model = new
            {
                response = "Success",
                responseMessage = "Login Success, Kindly Secure Token",
                responseCode = "" + "200" + "",
                responseData = dashboards,
            };
            return Ok(model);

        }
        #endregion

        #region  "                METHOD TO TOP FIVE PROPERTY RS COST THE DASHBOARD INFORMATION                "
        [HttpGet]
        [Route("TopFiveRSCOST")]

        public IHttpActionResult Gettopfiverscostproperty()
        {

            //if (clientinformation == null)
            //{
            //    return BadRequest("Invalid Data.");
            //}

            bottomfivepropertiesInformation dashboards = null;
            bottomfivepropertiesbl bottomfivepropertiesbl = new bottomfivepropertiesbl();
            dashboards = bottomfivepropertiesbl.Gettopfiverscostproperty();

            var model = new
            {
                response = "Success",
                responseMessage = "Login Success, Kindly Secure Token",
                responseCode = "" + "200" + "",
                responseData = dashboards,
            };
            return Ok(model);

        }
        #endregion

        #region  "                METHOD TO TOP FIVE PROPERTY HAULER COST THE DASHBOARD INFORMATION                "
        [HttpGet]
        [Route("TopFiveHAULERCOST")]

        public IHttpActionResult Gettopfivehaulercostproperty()
        {

            //if (clientinformation == null)
            //{
            //    return BadRequest("Invalid Data.");
            //}

            bottomfivepropertiesInformation dashboards = null;
            bottomfivepropertiesbl bottomfivepropertiesbl = new bottomfivepropertiesbl();
            dashboards = bottomfivepropertiesbl.Gettopfivehaulercostproperty();

            var model = new
            {
                response = "Success",
                responseMessage = "Login Success, Kindly Secure Token",
                responseCode = "" + "200" + "",
                responseData = dashboards,
            };
            return Ok(model);

        }
        #endregion

        #region"                METHOD TO CALLS TO ACTION COMPLETED  THE DASHBOARD INFORMATION               " 
        [HttpGet]
        [Route("CallstoactionCompleted")]

        public IHttpActionResult GetCallstoactionCompletedInfoProperty(string Userid, string Username, string Searchinfo)
        {

            //if (clientinformation == null)
            //{
            //    return BadRequest("Invalid Data.");
            //}

            CallstoactionInfo dashboards = null;
            CallstoactionBL Callstoactionbl = new CallstoactionBL();
            dashboards = Callstoactionbl.GetCallstoactionCompletedInfoProperty(Userid, Username, Searchinfo);

            var model = new
            {
                response = "Success",
                responseMessage = "Login Success, Kindly Secure Token",
                responseCode = "" + "200" + "",
                responseData = dashboards,
            };
            return Ok(model);

        }
        #endregion

        #region"                METHOD TO CALLS TO ACTION  TO DO THE DASHBOARD INFORMATION                   " 
        [HttpGet]
        [Route("CallstoactionToDo")]

        public IHttpActionResult GetCallstoactionToDoInfoProperty(string Userid, string Username, string Searchinfo)
        {

            //if (clientinformation == null)
            //{
            //    return BadRequest("Invalid Data.");
            //}

            CallstoactionInfo dashboards = null;
            CallstoactionBL Callstoactionbl = new CallstoactionBL();
            dashboards = Callstoactionbl.GetCallstoactionToDoInfoProperty(Userid, Username, Searchinfo);

            var model = new
            {
                response = "Success",
                responseMessage = "Login Success, Kindly Secure Token",
                responseCode = "" + "200" + "",
                responseData = dashboards,
            };
            return Ok(model);

        }
        #endregion

        #region  "                METHOD TO SERVICES THE DASHBOARD INFORMATION                          "  
        [HttpGet]
        [Route("Services")]

        public IHttpActionResult GetServicesProperty()
        {

            //if (clientinformation == null)
            //{
            //    return BadRequest("Invalid Data.");
            //}

            ServicesInfo dashboards = null;
            Servicesbl Servicesblref = new Servicesbl();
            dashboards = Servicesblref.GetServicesProperty();

            var model = new
            {
                response = "Success",
                responseMessage = "Login Success, Kindly Secure Token",
                responseCode = "" + "200" + "",
                responseData = dashboards,
            };
            return Ok(model);

        }
        #endregion

        #region "                METHOD TO SERVICE SSUMMARY THE DASHBOARD INFORMATION                   " 
        [HttpGet]
        [Route("ServicesSummary")]

        public IHttpActionResult GetServicesSummaryProperty(string month, string year)
        {

            //if (clientinformation == null)
            //{
            //    return BadRequest("Invalid Data.");
            //}

            ServicesInfo dashboards = null;
            Servicesbl Servicesblref = new Servicesbl();
            dashboards = Servicesblref.GetServicesSummaryProperty(month, year);

            var model = new
            {
                response = "Success",
                responseMessage = "Login Success, Kindly Secure Token",
                responseCode = "" + "200" + "",
                responseData = dashboards,
            };
            return Ok(model);

        }
        #endregion

        #region   "                METHOD TO METRICS THE DASHBOARD INFORMATION                           "
        [HttpGet]
        [Route("Metrics")]

        public IHttpActionResult GetMetrics()
        {

            //if (clientinformation == null)
            //{
            //    return BadRequest("Invalid Data.");
            //}

            MeticsQueues dashboards = null;
            MetricsQueuesbl Servicesblref = new MetricsQueuesbl();
            dashboards = Servicesblref.GetMetrics();

            var model = new
            {
                response = "Success",
                responseMessage = "Login Success, Kindly Secure Token",
                responseCode = "" + "200" + "",
                responseData = dashboards,
            };
            return Ok(model);

        }
        #endregion

        #region   "                METHOD TO QUEUES OPENTICKETS THE DASHBOARD INFORMATION                    "
        [HttpGet]
        [Route("QueuesOpentickets")]

        public IHttpActionResult GetQueuesOpentickets(string Userid, string Username, string Searchinfo)
        {

            //if (clientinformation == null)
            //{
            //    return BadRequest("Invalid Data.");
            //}

            MeticsQueues dashboards = null;
            MetricsQueuesbl Servicesblref = new MetricsQueuesbl();
            dashboards = Servicesblref.GetQueuesOpentickets(Userid, Username, Searchinfo);

            var model = new
            {
                response = "Success",
                responseMessage = "Login Success, Kindly Secure Token",
                responseCode = "" + "200" + "",
                responseData = dashboards,
            };
            return Ok(model);

        }
        #endregion

        #region   "                METHOD TO QUEUES RESOLVEDTICKETS THE DASHBOARD INFORMATION               "
        [HttpGet]
        [Route("QueuesResolvedtickets")]

        public IHttpActionResult GetQueuesResolvedtickets(string Userid, string Username, string Searchinfo)
        {

            //if (clientinformation == null)
            //{
            //    return BadRequest("Invalid Data.");
            //}

            MeticsQueues dashboards = null;
            MetricsQueuesbl Servicesblref = new MetricsQueuesbl();
            dashboards = Servicesblref.GetQueuesResolvedtickets(Userid, Username, Searchinfo);

            var model = new
            {
                response = "Success",
                responseMessage = "Login Success, Kindly Secure Token",
                responseCode = "" + "200" + "",
                responseData = dashboards,
            };
            return Ok(model);

        }
        #endregion

        [HttpGet]
        [Route("Numbers")]
        public IHttpActionResult GetNumbers()
        {
            using (var objDashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objDashboardBL.GetNumbersBL());
            }
        }

        [HttpGet]
        [Route("RevenueBySalesPersons")]
        public IHttpActionResult GetRevenueBySalesPersons()
        {
            using (var objDashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objDashboardBL.GetRevenueBySalesPersonsBL());
            }
        }

        [HttpGet]
        [Route("HighGrowthCustomers")]
        public IHttpActionResult GetHighGrowthCustomers()
        {
            using (var objDashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objDashboardBL.GetHighGrowthCustomersBL());
            }
        }

        [HttpGet]
        [Route("DormantPropertiesBySalesperson")]
        public IHttpActionResult GetDormantPropertiesBySalesperson()
        {
            using (var objDashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objDashboardBL.GetDormantPropertiesBySalespersonBL());
            }
        }

        [HttpGet]
        [Route("PropertiesWithoutInvoicesBySalespersons")]
        public IHttpActionResult GetPropertiesWithoutInvoicesBySalespersons()
        {
            using (var objDashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objDashboardBL.GetPropertiesWithoutInvoicesBySalespersonsBL());
            }
        }

        [HttpGet]
        [Route("CampaignSummary")]
        public IHttpActionResult GetCampaignSummary()
        {
            using (var objDashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objDashboardBL.GetCampaignSummaryBL());
            }
        }

        [HttpGet]
        [Route("SalesFunnel")]
        public IHttpActionResult GetSalesFunnel()
        {
            using (var objDashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objDashboardBL.GetSalesFunnelBL());
            }
        }

        [HttpGet]
        [Route("LeadLifetimeReport")]
        public IHttpActionResult GetLeadLifetimeReport()
        {
            using (var objDashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objDashboardBL.GetLeadLifetimeReportBL());
            }
        }

        [HttpGet]
        [Route("ExceptionByBillingAnalyst")]
        public IHttpActionResult GetExceptionByBillingAnalyst()
        {
            using (var objDashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objDashboardBL.GetExceptionByBillingAnalystBL());
            }
        }

        [HttpGet]
        [Route("AgingByServiceType")]
        public IHttpActionResult GetAgingByServiceType()
        {
            using (var objDashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objDashboardBL.GetAgingByServiceTypeBL());
            }
        }

        [HttpGet]
        [Route("OpenFundsByAccountExecutive")]
        public IHttpActionResult GetOpenFundsByAccountExecutive()
        {
            using (var objDashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objDashboardBL.GetOpenFundsByAccountExecutiveBL());
            }
        }

        [HttpGet]
        [Route("VectorOpenInvoiceBySalesperson")]
        public IHttpActionResult GetVectorOpenInvoiceBySalesperson()
        {
            using (var objDashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objDashboardBL.GetVectorOpenInvoiceBySalespersonBL());
            }
        }

        [HttpGet]
        [Route("MissingBillByBillingAnalyst")]
        public IHttpActionResult GetMissingBillByBillingAnalyst()
        {
            using (var objDashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objDashboardBL.GetMissingBillByBillingAnalystBL());
            }
        }

        [HttpGet]
        [Route("VendorPastDueByBillingAnalyst")]
        public IHttpActionResult GetVendorPastDueByBillingAnalyst()
        {
            using (var objDashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objDashboardBL.GetVendorPastDueByBillingAnalystBL());
            }
        }

        [HttpGet]
        [Route("TaskPerformanceMetricsByAE")]
        public IHttpActionResult GetTaskPerformanceMetricsByAE()
        {
            using (var objDashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objDashboardBL.GetTaskPerformanceMetricsByAEBL());
            }
        }

        [HttpGet]
        [Route("ProductiveMetricsByAE")]
        public IHttpActionResult GetProductiveMetricsByAE()
        {
            using (var objDashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objDashboardBL.GetProductiveMetricsByAEBL());
            }
        }

        [HttpGet]
        [Route("TaskPerfomranceMetricsBA")]
        public IHttpActionResult GetTaskPerfomranceMetricsBA()
        {
            using (var objDashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objDashboardBL.GetTaskPerfomranceMetricsBABL());
            }
        }

        [HttpGet]
        [Route("ProductiveMetricsByBA")]
        public IHttpActionResult GetProductiveMetricsByBA()
        {
            using (var objDashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objDashboardBL.GetProductiveMetricsByBABL());
            }
        }

        [HttpGet]
        [Route("TaskPerformanceMetricsCustomerService")]
        public IHttpActionResult GetTaskPerformanceMetricsCustomerService()
        {
            using (var objDashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objDashboardBL.GetTaskPerformanceMetricsCustomerServiceBL());
            }
        }

        [HttpGet]
        [Route("ProductiveMetricsByCustomerService")]
        public IHttpActionResult GetProductiveMetricsByCustomerService()
        {
            using (var objDashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objDashboardBL.GetProductiveMetricsByCustomerServiceBL());
            }
        }

        [HttpGet]
        [Route("Benchmarking")]
        public IHttpActionResult GetBenchmarking(Int64 propertyId)
        {
            using (var objDashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objDashboardBL.GetBenchmarkingBL(propertyId, VectorAPIContext.Current.UserId));
            }
        }



        [HttpGet]
        [Route("InvoiceSummary")]
        public IHttpActionResult GetInvoiceSummary()
        {
            using (var objDashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objDashboardBL.GetInvoiceSummaryBL());
            }
        }

        [HttpGet]
        [Route("SavingsSummary")]
        public IHttpActionResult GetSavingsSummary()
        {
            using (var objDashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objDashboardBL.GetSavingsSummaryBL());
            }
        }

        [HttpGet]
        [Route("AuditSummary")]
        public IHttpActionResult GetAuditSummary()
        {
            using (var objDashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objDashboardBL.GetAuditSummaryBL());
            }
        }

        [HttpGet]
        [Route("MissingBill")]
        public IHttpActionResult GetMissingBill(string receipientType, string clientName, string account, string vendor)
        {
            using (var objDashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objDashboardBL.GetMissingBillBL(receipientType, clientName, account, vendor));
            }
        }

        [HttpGet]
        [Route("VendorPendingCredits")]
        public IHttpActionResult GetVendorPendingCredits(string clientName, string account, string vendor, string accountStatus)
        {
            using (var objDashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objDashboardBL.GetVendorPendingCreditsBL(clientName, account, vendor, accountStatus));
            }
        }

        [HttpGet]
        [Route("ExceptionAging")]
        public IHttpActionResult GetExceptionAging(string clientName, string vendor, string exceptionType, string account, string fromDate, string toDate)
        {
            using (var objDashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objDashboardBL.GetExceptionAgingBL(clientName, vendor, account, exceptionType, fromDate, toDate));
            }
        }

        [HttpGet]
        [Route("MyNumbers")]
        public IHttpActionResult GetMyNumbers()
        {
            using (var objDashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objDashboardBL.GetMyNumbersBL());
            }
        }

        [HttpGet]
        [Route("NumberofContractsByStatus")]
        public IHttpActionResult GetNumberofContractsByStatus(string clientName, string salesPerson, Int64 propertyId)
        {
            using (var objDashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objDashboardBL.GetNumberofContractsByStatusBL(clientName, salesPerson, propertyId, VectorAPIContext.Current.UserId));
            }
        }

        [HttpGet]
        [Route("UpcomingRenewalTracker")]
        public IHttpActionResult GetUpcomingRenewalTracker(string salesPerson, string fromDate, string toDate, string vendor)
        {
            using (var objDashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objDashboardBL.GetUpcomingRenewalTrackerBL(salesPerson, fromDate, toDate, vendor));
            }
        }

        [HttpGet]
        [Route("ArchiveContractRequest")]
        public IHttpActionResult GetArchiveContractRequest()
        {
            using (var objDashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objDashboardBL.GetArchiveContractRequestBL());
            }
        }

        [HttpGet]
        [Route("ClientVendorContractDocuments")]
        public IHttpActionResult GetClientVendorContractDocuments(string clientName, string vendor)
        {
            using (var objDashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objDashboardBL.GetClientVendorContractDocumentsBL(clientName, vendor));
            }
        }

        [HttpGet]
        [Route("SearchForContract")]
        public IHttpActionResult GetSearchForContract()
        {
            using (var objDashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objDashboardBL.GetSearchForContractBL());
            }
        }

        [HttpGet]
        [Route("VendorPastDueByAE")]
        public IHttpActionResult GetVendorPastDueByAE(Int64? vendorId, string accountExecutive, Int64? accountId, Int64? clientId)
        {
            using (var objDashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objDashboardBL.GetVendorPastDueByAEBL(vendorId, accountExecutive, accountId, clientId, VectorAPIContext.Current.UserId));
            }
        }

        [HttpGet]
        [Route("MyClients")]
        public IHttpActionResult GetMyClients()
        {
            using (var objDashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objDashboardBL.GetMyClientsBL(VectorAPIContext.Current.UserId.ToString()));
            }
        }

        [HttpGet]
        [Route("OveragesByClient")]
        public IHttpActionResult GetOveragesByClient(string client, string vendor)
        {
            using (var objDashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objDashboardBL.GetOveragesByClientBL(client, vendor));
            }
        }

        [HttpGet]
        [Route("TaskPerformanceMetricsByClient")]
        public IHttpActionResult GetTaskPerformanceMetricsByClient()
        {
            using (var objDashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objDashboardBL.GetTaskPerformanceMetricsByClientBL(VectorAPIContext.Current.UserId));
            }
        }

        [HttpGet]
        [Route("OpenTasksByServiceType")]
        public IHttpActionResult GetOpenTasksByServiceType(string serviceType, string client)
        {
            using (var objDashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objDashboardBL.GetOpenTasksByServiceTypeBL(serviceType, client));
            }
        }

        [HttpGet]
        [Route("UpcomingClientContractRenewals")]
        public IHttpActionResult GetUpcomingClientContractRenewals(string salesPerson, Int64? clientId)
        {
            using (var objDashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objDashboardBL.GetUpcomingClientContractRenewalsBL(salesPerson, clientId, VectorAPIContext.Current.UserId));
            }
        }

        [HttpGet]
        [Route("ContractsPendingForClientApproval")]
        public IHttpActionResult GetContractsPendingForClientApproval(string salesPerson, string client)
        {
            using (var objDashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objDashboardBL.GetContractsPendingForClientApprovalBL(salesPerson, client));
            }
        }

        [HttpGet]
        [Route("V97OpenInvoices")]
        public IHttpActionResult GetV97OpenInvoices(string salesPerson, string client)
        {
            using (var objDashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objDashboardBL.GetV97OpenInvoicesBL(salesPerson, client));
            }
        }


        [HttpGet]
        [Route("ConsolidatedFilesFundRequest")]
        public IHttpActionResult GetConsolidatedFilesFundRequest(string salesPerson, Int64? clientId)
        {
            using (var objDashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objDashboardBL.GetConsolidatedFilesFundRequestBL(salesPerson, clientId, VectorAPIContext.Current.UserId));
            }
        }


        [HttpGet]
        [Route("GetQuickLinks")]
        public IHttpActionResult GetQuickLinks(Int64? userID)
        {
            using (var objDashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objDashboardBL.GetQuickLinks(userID));
            }
        }

        [HttpPost]
        [Route("ManageQuickLinks")]
        public IHttpActionResult ManageQuickLinks(QuickLinks objQuickLinks)
        {
            using (var objDashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objDashboardBL.ManageQuickLinks(objQuickLinks));
            }
        }

        [HttpDelete]
        [Route("DeleteDashboardWidget")]
        public IHttpActionResult DeleteDashboardWidget(Int64 userId, Int64 dashboardId, Int64 widgetId, Int64 dashboardWidgetMappingId)
        {
            //TODO : Implement Using in all methods
            using (var dashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(dashboardBL.DeleteDashboardWidget(userId, dashboardId, widgetId, dashboardWidgetMappingId));

            }
        }

        [HttpDelete]
        [Route("DeleteDashboard")]
        public IHttpActionResult DeleteDashboard(Int64 userId, Int64 dashboardId)
        {
            using (var dashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(dashboardBL.DeleteDashbord(userId, dashboardId));
            }
        }

        [HttpPost]
        [Route("AddWidgets")]
        public IHttpActionResult AddDashbordWidgets(Vector.Dashboard.Entities.Dashboard objDashboard)
        {
            using (var dashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(dashboardBL.AddDashbordWidgets(objDashboard));

            }
        }

        [HttpGet]
        [Route("WorkforceUsers")]
        public IHttpActionResult GetWorkforceUsers()
        {
            using (var objDashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objDashboardBL.GetWorkforceUsers(VectorAPIContext.Current.UserId));
            }
        }

        [HttpPost]
        [Route("UsersProductivityDetails")]
        public IHttpActionResult GetWorkforceUsersProductivityDetails(string action, Vector.Dashboard.Entities.WorkforceUsers objWorkforceUsers)
        {
            using (var objDashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objDashboardBL.GetWorkforceUsersProductivityDetails(action, objWorkforceUsers.workforceUsers != null ?
                    (!string.IsNullOrEmpty(objWorkforceUsers.workforceUsers) ? objWorkforceUsers.workforceUsers.ToString() : "") : "", VectorAPIContext.Current.UserId));
            }
        }

        [HttpGet]
        [Route("GetWorkforceTasks")]
        public IHttpActionResult GetWorkforceTasks(Int64 ticketId, string type, Int64? manifestId, Int64? flowdetailId, string categoryType, string categoryId, string assignedUsers)
        {
            using (var objDashboardBL = new DashboardBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objDashboardBL.GetWorkforceTasks(ticketId, type, VectorAPIContext.Current.UserId, manifestId, flowdetailId, categoryType, categoryId, assignedUsers));
            }
        }
    }
}

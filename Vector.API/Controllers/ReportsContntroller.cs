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
using Vector.Garage.BusinessLayer;
using Vector.Garage.Entities;
using Vector.UserManagement.ClientInfo;

namespace Vector.API.Controllers
{
    [RoutePrefix("api/Garage")]
    [VectorActionAutorizationFilter]
    public class ReportsController : ApiController
    {

        #region Client Reports

        [HttpPost]
        [Route("GetRsAgingInfo")]
        
        public IHttpActionResult GetRsAgingInfo([FromBody] SearchEntity reportSearch)
        {
            using (var objFinancialBL = new FinancialReportsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objFinancialBL.GetRsAgingInfo(reportSearch, VectorAPIContext.Current.UserId));

            }
        }

        [HttpPost]
        [Route("GetHaulerAgingReportInfo")]
        
        public IHttpActionResult GetHaulerAgingInfo([FromBody] SearchEntity reportSearch)
        {
            using (var objFinancialBL = new FinancialReportsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objFinancialBL.GetHaulerAgingInfo(reportSearch, VectorAPIContext.Current.UserId));
            }
        }

        [HttpPost]
        [Route("GetInvoiceSummaryInfo")]
        
        public IHttpActionResult GetInvoiceSummaryInfo([FromBody] SearchEntity reportSearch)
        {
            using (var objFinancialBL = new FinancialReportsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objFinancialBL.GetInvoiceSummaryInfo(reportSearch, VectorAPIContext.Current.UserId));
            }
        }
        [HttpPost]
        [Route("GetSavingSummaryInfo")]

        public IHttpActionResult GetSavingSummaryInfo([FromBody] SearchEntity reportSearch)
        {
            using (var objFinancialBL = new FinancialReportsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objFinancialBL.GetSavingSummaryInfo(reportSearch, VectorAPIContext.Current.UserId));
            }
        }
        [HttpPost]
        [Route("GetInvoiceLineItemInfo")]
        
        public IHttpActionResult GetInvoiceLineItemInfo(SearchEntity reportSearch)
        {
            using (var objFinancialBL = new FinancialReportsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objFinancialBL.GetInvoiceLineItemInfo(reportSearch, VectorAPIContext.Current.UserId));
            }
        }

        [HttpPost]
        [Route("GetNegotiationStatusInfo")]
        
        public IHttpActionResult GetNegotiationStatusInfo([FromBody] SearchEntity reportSearch)
        {
            using (var objFinancialBL = new FinancialReportsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objFinancialBL.GetNegotiationStatusInfo(reportSearch, VectorAPIContext.Current.UserId));
            }
        }

        [HttpPost]
        [Route("GetContractListReportInfo")]
        
        public IHttpActionResult GetContractInfo([FromBody] SearchEntity reportSearch)
        {
            using (var objFinancialBL = new FinancialReportsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objFinancialBL.GetContractInfo(reportSearch, VectorAPIContext.Current.UserId));
            }
        }


        [HttpPost]
        [Route("GetPropertyListInfo")]
        
        public IHttpActionResult GetPropertyInfo(SearchEntity reportSearch)
        {
            using (var objFinancialBL = new FinancialReportsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objFinancialBL.GetPropertyInfo(reportSearch, VectorAPIContext.Current.UserId));
            }
        }

        [HttpPost]
        [Route("GetMissingInvoiceReport")]

        public IHttpActionResult GetMissingInvoiceReport([FromBody] SearchEntity reportSearch)
        {
            using (var objFinancialBL = new FinancialReportsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objFinancialBL.GetMissingInvoiceReport(reportSearch, VectorAPIContext.Current.UserId));
            }
        }

        [HttpPost]
        [Route("GetBillGapReport")]

        public IHttpActionResult GetBillGapReport([FromBody] SearchEntity reportSearch)
        {
            using (var objFinancialBL = new FinancialReportsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objFinancialBL.GetBillGapReport(reportSearch, VectorAPIContext.Current.UserId));
            }
        }

        [HttpPost]
        [Route("GetServiceLevelByPropertyInfo")]
        
        public IHttpActionResult GetServiceLevelByPropertyInfo(SearchEntity reportSearch)
        {
            using (var objFinancialBL = new FinancialReportsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objFinancialBL.GetServiceLevelByPropertyInfo(reportSearch, VectorAPIContext.Current.UserId));
            }
        }

        #endregion

        #region Baseline

        [HttpGet]
        [Route("GetBaselineInfo")]
        
        public IHttpActionResult GetBaselineInfo(string action, int baselineId, string baselineNo)
        {
            using (var objThreeSixtyDegreesBL = new ThreeSixtyDegreesBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objThreeSixtyDegreesBL.GetBaselineInfo(action, baselineId, baselineNo));
            }
        }


        #endregion

        #region Property
        [HttpGet]
        [Route("GetPropertyInfo")]
        
        public IHttpActionResult GetProeprtyInfo(string action, int propertyId, string propertyName)
        {
            using (var objThreeSixtyDegreesBL = new ThreeSixtyDegreesBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objThreeSixtyDegreesBL.GetPropertyInfo(action, propertyId, propertyName));
            }
        }

        #endregion

        #region Hauler
        [HttpGet]
        [Route("GetHaulerInfo")]
        
        public IHttpActionResult GetHaulerInfo(string action, int haulerId, string haulerName)
        {
            using (var objThreeSixtyDegreesBL = new ThreeSixtyDegreesBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objThreeSixtyDegreesBL.GetHaulerInfo(action, haulerId, haulerName));
            }
        }
        #endregion

        #region Account
        [HttpGet]
        [Route("GetAccountInfo")]
        
        public IHttpActionResult GetAccountInfo(string action, int accountId, string accountName)
        {
            using (var objThreeSixtyDegreesBL = new ThreeSixtyDegreesBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objThreeSixtyDegreesBL.GetAccountInfo(action, accountId, accountName));
            }
        }
        #endregion

        #region "METHOD TO RETURN  Client Information"

        [HttpGet]
        [Route("ClientInformation")]
        public IHttpActionResult GetClientInformation(string action,string clientName,Int64 clientId)
        {

            using (var objThreeSixtyDegreesBL = new ThreeSixtyDegreesBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objThreeSixtyDegreesBL.GetClientInformation(action,clientName, clientId,VectorAPIContext.Current.UserId));

            }


        }

        #endregion
        #region "METHOD TO RETURN  Client Properties"

        [HttpGet]
        [Route("ClientProperties")]
        public IHttpActionResult GetClientProperties(string clientName = "Schulte Hospitality")
        {

            using (var objThreeSixtyDegreesBL = new ThreeSixtyDegreesBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objThreeSixtyDegreesBL.GetClientProperties(clientName,VectorAPIContext.Current.UserId));

            }


        }
        #endregion
        #region "METHOD TO RETURN  Client Metrics"

        
        #endregion

        [HttpGet]
        [Route("SavingsCostInfo")]
        public IHttpActionResult GetSavingsCostInfo(string searchBy, Int64 searchId, string searchText, string year, Int64 userId)
        {

            using (var objThreeSixtyDegreesBL = new ThreeSixtyDegreesBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objThreeSixtyDegreesBL.SavingsCostInfo(searchBy, searchId, searchText, year, VectorAPIContext.Current.UserId));

            }


        }


        #region "METHOD TO RETURN  Client Information"

        [HttpGet]
        [Route("ClientSearch")]
        public IHttpActionResult GetClientSearch(string clientName)
        {

            using (var objThreeSixtyDegreesBL = new ThreeSixtyDegreesBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objThreeSixtyDegreesBL.GetClientSearch(clientName,VectorAPIContext.Current.UserId));

            }


        }

        #endregion

        #region Forms->Master Data



        #endregion

        #region Monitoring Console


        [HttpPost]
        [Route("GetMonitoringConsoleInfo")]
        public IHttpActionResult GetMonitoringConsoleInfo(ConsoleEntity objConsoleEntity)
        {

            using (var objReportBL = new ReportBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objReportBL.GetProcessAndFlowMonitoringConsoleInfo(objConsoleEntity));

            }


        }
        #endregion

        [HttpPost]
        [Route("GetClientListReportInfo")]
        public IHttpActionResult GetClientListReportInfo(SearchEntity objSearchEntity)
        {

            using (var objReportBL = new ReportBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objReportBL.GetClientListReportInfo(objSearchEntity, VectorAPIContext.Current.UserId));

            }

        }

        [HttpPost]
        [Route("GetVendorOverchargeReportInfo")]
        public IHttpActionResult GetVendorOverchargeReportInfo(SearchEntity objSearchEntity)
       {

            using (var objReportBL = new ReportBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objReportBL.GetVendorOverchargeReportInfo(objSearchEntity, VectorAPIContext.Current.UserId));

            }

        }

        [HttpPost]
        [Route("GetVendorListReportInfo")]
        public IHttpActionResult GetVendorListReportInfo(SearchEntity objSearchEntity)
        {

            using (var objReportBL = new ReportBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objReportBL.GetVendorListReportInfo(objSearchEntity, VectorAPIContext.Current.UserId));

            }

        }

        [HttpPost]
        [Route("GetVendorCorporateListReportInfo")]
        public IHttpActionResult GetVendorCorporateListReportInfo(SearchEntity objSearchEntity)
        {

            using (var objReportBL = new ReportBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objReportBL.GetVendorCorporateListReportInfo(objSearchEntity, VectorAPIContext.Current.UserId));

            }

        }

        [HttpPost]
        [Route("GetDownloadInvoicesReportInfo")]
        public IHttpActionResult GetDownloadInvoicesReportInfo(SearchEntity objSearchEntity)
        {

            using (var objReportBL = new ReportBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objReportBL.GetDownloadInvoicesReportInfo(objSearchEntity, VectorAPIContext.Current.UserId));

            }

        }


        [HttpPost]
        [Route("DownloadInvoices")] 
        public IHttpActionResult DownloadInvoices(DownloadInvoices objDonwloadInvoices)
        {

            using (var objReportBL = new ReportBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objReportBL.DownloadInvoices(objDonwloadInvoices, VectorAPIContext.Current.UserId));

            }


        }
        [HttpPost]
        [Route("ManageMissingInvoiceInfo")]
        public IHttpActionResult ManageMissingInvoiceInfo(MarkAsNotMissing objMarkAsNotMissing)
        {

            using (var objReportBL = new ReportBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objReportBL.ManageMissingInvoiceInfo(objMarkAsNotMissing, VectorAPIContext.Current.UserId));

            }


        }

        [HttpPost]
        [Route("UploadedMissingInviceInfo")]
        public IHttpActionResult UploadedMissingInviceInfo(MarkAsNotMissing objMarkAsNotMissing)
        {

            using (var objReportBL = new ReportBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objReportBL.UploadedMissingInviceInfo(objMarkAsNotMissing, VectorAPIContext.Current.UserId));

            }


        }


        [HttpPost]
        [Route("GetTaskStatus")]
        public IHttpActionResult GetTaskStatus(TaskSearch objTaskSearch)
        {

            using (var objReportBL = new ReportBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objReportBL.GetTaskStatus(objTaskSearch, VectorAPIContext.Current.UserId));

            }


        }

        [HttpPost]
        [Route("GetProcessReport")]
        public IHttpActionResult GetProcessReport(SearchEntity objSearch)
        {

            using (var objReportBL = new ReportBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objReportBL.GetProcessReport(objSearch, VectorAPIContext.Current.UserId));

            }


        }




        [HttpGet]
        [Route("LineitemComments")]
        public IHttpActionResult GetLineitemComments(string type,Int64 lineitemId)
        {

            using (var objReportBL = new ReportBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objReportBL.GetLineitemComments(type, lineitemId, VectorAPIContext.Current.UserId));

            }


        }




        [HttpPost]
        [Route("VectorGetStagingInvoices")]

        public IHttpActionResult VectorGetStagingInvoices([FromBody] SearchEntity reportSearch)
        {
            using (var objFinancialBL = new FinancialReportsBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objFinancialBL.VectorGetStagingInvoices(reportSearch, VectorAPIContext.Current.UserId));
            }
        }


        [HttpGet]
        [Route("VectorMissingInvoiceHistory")]
        public IHttpActionResult VectorMissingInvoiceHistory(string action, Int64 accountId,string irpUniqueCode,Int64 contractId)
        {

            using (var objReportBL = new ReportBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objReportBL.VectorMissingInvoiceHistory(action, accountId, irpUniqueCode, contractId, VectorAPIContext.Current.UserId));

            }


        }

        [HttpGet]
        [Route("VectorGetAccountAddEditReport")]
        public IHttpActionResult VectorGetAccountAddEditReport(string action)
        {

            using (var objReportBL = new ReportBL(new VectorDataConnection()))
            {
                return VectorResponseHandler.GetVectorResponse(objReportBL.VectorGetAccountAddEditReport(action, VectorAPIContext.Current.UserId));

            }


        }
    }
}

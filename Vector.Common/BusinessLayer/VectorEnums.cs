using System;
using System.ComponentModel;
using System.Reflection;

namespace Vector.Common.BusinessLayer
{
    public static class VectorEnums
    {
        public enum StoredProcedures
        {
            VectorGetUserManagementInfo,
            VectorGetServiceRequestInfo,
            GetVactorMasterData,
            VectorGetWorkManifestInfo,
            VectorGetToolsFeaturesInfo,
            VectorManageWorkManifestInfo,
            VectorManagePersonaFeatures,
            GetFeaturesByPerosna,
            VectorManageUserInfo,
            VectorGetFeatures,
            VectorManageClientInvoicePreferences,
            VectorGetClientInvoicePreferences,
            VectorManageClientAddressInfo,
            VectorGetClientContactInfo,
            VectorManageClientContactInfo,
            VectorManageClientContractInfo,
            VectorManageClientRole,
            VectorManagePropertyAddressInfo,
            VectorViewClient,
            VectorGetClientContractInfo,
            VectorManagePropertyInvoicePreferences,
            VectorGetPropertyInvoicePreferences,
            VectorGetPropertyContactInfo,
            VectorManagePropertyContactInfo,
            VectorManagePropertyMiscellaneousInfo,
            VectorGetPropertyMiscellaneousInfo,
            VectorGetPropertyContractInfo,
            VectorManagePropertyContractInfo,
            VectorManageVendorCorporateContactInfo,
            VectorGetVendorCorporateContactInfo,
            VectorManageVendorCorporateInfo,
            VectorGetVendorCorporateInfo,
            VectorGetVendorCorporateAddressInfo,
            VectorManageVendorCorporateAddressInfo,
            VectorGetVendorContactInfo,
            VectorManageVendorContactInfo,
            VectorManageVendorAddressInfo,
            VectorGetVendorAddressInfo,
            VectorGetVendorPaymentInfo,
            VectorManageVendorPaymentInfo,
            VectorManageVendorCorporatePaymentInfo,
            VectorGetVendorCorporatePaymentInfo,
            VectorViewVendor,
            VectorManageRegion,
            VectorManageVerticals,
            VectorGetClientAgreementInfo,
            VectorManageClientAgreementInfo,
            VectorManagePropertyInfo,
            VectorGetVendorInfo,
            VectorManageVendorInfo,
            VectorViewProperty,
            VectorGetPropertyDetails,
            VectorManageBaselineInfo,
            VectorGetBaselineInfo,
            VectorGetMapCatalogLineItemInfo,
            VectorManageMapCatalogLineItemInfo,
            VectorGetApproveBaseLineInfo,
            VectorManageApproveBaseLineInfo,
            VectorGetBaselineMainInfo,
            VectorManageBaselineMainInfo,
            VectorGetServiceLevelCatalog,
            VectorManageServiceLevelCatalog,
            VectorManageServiceRequestInfo,
            VectorGetBaselinSupportingFilesInfo,
            VectorManageBaselinSupportingFilesInfo,
            VectorAddUpdateNegotiationsInfo,
            VectorGetNegotiationsInfo,
            VectorGetSearchNegotiationsData,
            VectorGetNegotiationsBidSheetInfo,
            VectorGetDraftNegotiationsInfo,
            VectorGetNegotiationDetails,
            VectorAddUpdateNegotiationBidValues,
            VectorAddUpdateDraftNegotiationsInfo,
            VectorManageNegotiationBidSheetInfo,
            VectorAddUpdateNegotiationLineItems,
            VectorAddUpdateBaselineNegotiationLineItems,
            VectorGenerateVendorBidLink,
            GetVectorInventoryMasterData,
            VectorManageNegotiationsInfo,
            VectorManageDraftNegotiationsInfo,
            VectorManageAccountInfo,
            VectorGetAccountInfo,
            VectorGetAccountRemitInfo,
            VectorManageAccountRemitInfo,
            VectorGetBatchInfo,
            VectorManageBatchInfo,
            VectorManageFinalizeBatchInfo,
            VectorGetInvoiceHeaderInfoSearch,
            VectorManageUploadInvoicesInf,
            VectorManageInvoiceHeaderInfo,
            VectorManageUploadInvoicesInfo,
            VectorGetContractsInfo,
            VectorGetInvoiceLineItemsInfo,
            VectorGetInvoiceHeaderLineItemsInfo,
            VectorManageInvoiceAddLineItemInfo,
            VectorGetExceptions,
            VectorManageEnteredException,
            VectorGetVerifyInfo,
            VectorGetInvoicesForProcessing,
            VectorGetInvoicesForAudit,
            VectorGetInvoiceDetails,
            GetVectorContractDetailsForDocAndEmail,
            VectorGenerateInvoice,
            VectorGetInvoiceDetailsForInvoiceGeneration,
            VectorManageContractTransmitInfo,
            VectorManageRenewArchiveContractInfo,
            VectorUpdateContractInfo,
            VectorGetExceptionDetails,
            VectorManageExceptionInfo,
            VectorManageContractApproveAnnualIncreaseLineItemsInfo,
            VectorAddUpdateContractLineItems,
            VectorCreateException,
            VectorGetDashboardNumbersData,
            VectorGetDashboardRevenueBySalesPersonsData,
            VectorGetDashboardHighGrowthCustomersData,
            VectorGetDashboardDormantPropertiesBySalespersonData,
            VectorGetProductiveMetricsByCustomerServiceData,
            VectorGetTaskPerformanceMetricsCustomerServiceData,
            VectorGetBenchmarkingData,
            VectorGetProductiveMetricsByBAData,
            VectorGetTaskPerfomranceMetricsBAData,
            VectorGetOpenFundsByAccountExecutiveData,
            VectorGetVectorOpenInvoiceBySalespersonData,
            VectorGetAgingByServiceTypeData,
            VectorGetLeadLifetimeReportData,
            VectorGetSalesFunnelData,
            VectorGetCampaignSummaryData,
            VectorGetExceptionByBillingAnalystData,
            VectorGetPropertiesWithoutInvoicesBySalespersonsData,
            VectorGetInvoiceExplorerData,
            VectorGetVendorPastDueByBillingAnalystData,
            VectorGetTaskPerformanceMetricsByAEData,
            VectorGetMissingBillByBillingAnalystData,
            VectorGetProductiveMetricsByAEData,
            VectorGetInvoiceSummaryData,
            VectorGetSavingsSummaryData,
            VectorGetAuditSummaryData,
            VectorCreateConsolidatedInvoiceInfo,
            VectorSearchConsolidatedInvoice,
            VectorManageConsolidatedInvoice,
            VectorGetConsolidatedRejectedInvoices,
            VectorManageReprocessConsolidatedInvoice,
            VectorManageRejectConsolidatedInvoice,
            VectorGetClientGetConfigurationInfo,
            VectorManageClientPayAccoutInfo,
            VectorGetConsolidatedInvoiceForPayFile,
            VectorManagePayFileGeneration,
            VectorGetPayFiles,
            VectorManagePayFile,
            VectorGetSearchProperties,
            ManageElectronicPayFileGeneration,
            VectorManageEletronicPayFile,
            VectorRejectPayFileTransactions,
            VectorManageClientDealPackage,
            VectorManageInitiateOwnerShipTransgerInfo,
            GetOwnershipTransfers,
            VectorManageOwnershipTransferClientApproval,
            VectorManageOwnershipTransferApprove,
            VectorGetMissingBillData,
            VectorGetVendorPendingCreditsData,
            VectorGetExceptionAgingData,
            VectorGetMyNumbersData,
            VectorGetNumberofContractsByStatusData,
            VectorGetUpcomingRenewalTrackerData,
            VectorGetArchiveContractRequestData,
            VectorClientVendorContractDocumentsData,
            VectorGetSearchForContractData,
            VectorGetVendorPastDueByAEData,
            VectorGetMyClientsData,
            VectorGetOveragesByClientData,
            VectorGetTaskPerformanceMetricsByClientData,
            VectorGetOpenTasksByServiceTypeData,
            VectorGetContractsPendingForClientApprovalData,
            VectorGetUpcomingClientContractRenewalsData,
            VectorGetV97OpenInvoicesData,
            VectorGetConsolidatedFilesFundRequestData,
            VectorManageTicketAndTasks,
            VectorGetQuickLinksInfo,
            VectorManageQuickLinksInfo,
            VectorGetMonitoringConsoleInfo,
            VectorGetInvoiceLookUp,
            VectorGetDispatchInvoice,
            VectorGetInvoiceDetailsForDispatch,
            VectorManageDispatchInvoice,
            VectorGetPayFileInfoforXML,
            VectorGetClientDealPackageInfo,
            VectorGetPropertyBaselineInfo,
            VectorManagePropertyBaselineInfo,
            VectorGetTasksInfo,
            VectorDashboardManagement,
            VectorManageMyQueue,
            VectorGetMyQueue,
            VectorManageTasks,
            VectorManageVendorPendingCreditsInfo,
            VectorGetVendorPendingCreditsInfo,
            VectorManageVendorPastDueInfo,
            VectorGetVendorPastDueInfo,
            VectorManageVendorEmailDataInfo,
            VectorManageContractInfo,
            VectorAddLineitemToInvoiceFromAccount,
            VectorManageUserProfileInfo,
            VectorGetClientListReport,
            VectorGetVendorOverChargeReportInfo,
            VectorGetVendorListReportInfo,
            VectorGetVendorCorporateListReportInfo,
            VectorGetDownloadInvoicesReportInfo,
            VectorManageDownloadInvoices,
            VectorManageMissingInvoiceInfo,
            VectorUploadedMissingInviceInfo,
            VectorManagePreBidRequest,
            VectorAddUpdateNegotiationEmailUniqueId,
            GetVectorDocuments,
            VectorDocuments,
            VectorUploadPropertyBaselineInfo,
            VectorSearchAccounts,
            VectorSearchBaselines,
            GetNegotiationRevisitEmailDetails,
            GetNegotiationEmailDetails,
            VectorGetServiceManagerDetails,
            VectorClientSummaryWidget,
            VectorClientMetricsWidget,
            VectorServiceSummaryWidget,
            VectorGetTicketByServiceType,
            VectorCompleteDealPackage,
            VectorOpenInvoiceByClient,
            VectorManageActions,
            VectorGetActions,
            VectorTaskStatusReprot,
            VectorGetToDownloadFromPR,
            VectorViewBaseLine,
            VectorDailyProcessReport,
            VectorManagePartiallyFundConsolidatedInvoice,
            VectorGetNegotationBaselineLineitemInfo,
            GetVectorVendorBidRequestEmailInfo,
            VectorOwnerShipTransfer,
            VectorManageProcessUpdates,
            VectorManageTicketRequestorDetails,
            VectorCreateTicketForException,
            VectorSearchAccountsExceptions,
            VectorGetAccountLineitemInfo,
            VectorManageAccountLineItemInfo,
            VectorGetMonthlyInvoicesForProcessing,
            VectorGetMonthlyInvoiceDetailsForInvoiceGeneration,
            VectorSearchVendorCorporate,
            VectorRunCalculationServiceForInvoice,
            VectorGetLineitemComments,
            VectorGetBackLogTasks,
            VectorGetPropertiesByClient,
            VectorGetPropertyBaselines,
            VectorGetPropertyNegotiations,
            VectorGetPropertyContracts,
            VectorGetPropertyAccounts,
            VectorGetPropertyDocuments,
            VectorManageNegotiationDocuments,
            VectorGetNegotiationDocuments,
            VectorManageNegotiationLineitem,
            VectorGetRingCentralInfo,
            VectorSearchVendors,
            VectorGetNegotiaitonWidget,
            VectorGetVendorContactsForBaseline,
            VectorAddBaseline,
            VectorManageBaseline,
            VectorGetBaseline,
            VectorGetContractExpiryInfo,
            VectorManageBillGapReport,
            VectorManageBaselineLineitemState,
            VectorManageBidEmailServices,
            VectorNegotiationThreeSixtyWidget,
            GetNegotaitionStatusWidgetDetails,
            GetNegotaitionStatusWidgetDetailsWithClientProperty,
            VectorNegotiationThreeSixtyWidgetClientPropertyView,
            VectorGetNegotiaitons,
            ManageNegotiaionLineitemState,
            GetInvoiceNotificaitonsInfo,
            VectorGetNegotiationVendorLineitems,
            VectorGetAccountLineitems,
            VectoGetLowestBidInfo,
            ManageWidgetFeaturePersonalization,
            VectorGetWorkTrackerInfo,
            VectorManageWorkTraker,
            VectorManageMissingInvoice,
            VectorManagePlaceholderInvoice,
            VectorManageInvoicelineitem,
            VectorManageAssignments,
            VectorGetAwardedVendors,
            VectorGetPropertyContacts,
            VectorGetProductivityMetricsByUser,
            VectorGetMissingInvoiceReportHistory,
            VectorGetAccountAddEditReport,
            VectorGetAccountComments,
            VectorArchiveAccountVerificationRecord,
            VectorGetWorkforceUsers,
            VectorGetWorkforceCategory,
            VectorGetWorkforceSLAUsers,
            VectorGetWorkforceSLA,
            VectorGetAccountListReport,
            VectorGetResources,
            VectorManageResources,
            VectorGetInvoiceReceiptData,
            VectorManageIRPDocuments,
            VectorGetIRPDocuments,
            ManageUserFeatureNavigationLog,
            GetVectorCommonFromInventory,
            VectorManageCommonTask,
            ManageUserLog,
            VectorUserChangePassword,
            VectorGetWorkforceTasks,
            VectorGetWorkforceUnassingedInfo,
            VectorGetCommonTaskInfo,
            VectorNegotiationCloneLineitems,
            VectorGetInvoiceVerification,
            VectorGetInvoiceVerificationDetails,
            ManageInvoiceVerificationDetails,
            VectorGetInvoicesForShortPayNotification,
            VectorGetMasterDataTrueUpPanelData,
            VectorManageMasteTrueupDataLineitem,
            VectorGetMasterDataTrueupAccountLineitemInfo,
            VectorGetTrueupPanelContractApprovalData,
            VectorGetMasterDataTrueupContractLineitemInfo,
            VectorGetInvoiceReceiptDataAnalytics,
            VectorGetExceptionAnalyticsData,
            VectorGetExceptionHistoryData,
            VectorManageEmailTicket
        }

        public enum Actions
        {
            GetFeaturesByPerosna,
            UserFeatures,
            GetUserProfileDetails
        }

        public enum Filter
        {
            Top10,
            Top20,
            Others,
            All,
            Take,
            Skip,
            SkipAndTake
        }
        public enum EmailType
        {
            CC,
            Bcc,
        }

        public enum Column
        {
            FGuid,
            Month,
            Year,
            MonthYear,
            VendorId,
            MessageId,
            UIDL,
            NegotiationNo,
        }

        public enum Format
        {
            MMDDYYYYHHMMSS,
            MMddyyyyHHmmssffff,
            MMDDYYYY,
            MMddyyyy,
            yyyyMMdd,
            MMDDYY,
            ddMMyy,
            DDMMYYYY,
            YYYYDDMM,
            MMYYYY,
            MMyyyy,
            YYYYMMDD_HHMMSS,
            HHMMSS,
            YYYYMMDD,
            MMDDYYYY_HHMMSS,
            YYYYMMDDHHMMSS,
            DD_MM_YY
        }

        public enum RequestMethod
        {
            GET,
            POST,
            PUT,
            DELETE,
            OPTION
        }

        public enum ConfigValue
        {
            FileDirectory,
            DailyServiceIntervel,
            MonthlyServiceIntervel,
            VectorAPIUrl,
            EmailServer,
            EmailId,
            Password,
            ServerProtocol,
            NegotiationRevisitEmailUrl,
            VectorServiceManagerUrl,
        }

        public enum ExcelMessage
        {
            [Description("Invalid Date")]
            InvalidDate,
            [Description("Date must be a valid date")]
            ExcelInvalidDate,
            [Description("Please enter date here")]
            ExcelDatePrompt,
            [Description("In Valid Data")]
            InvalidData,
            [Description("value data must be a integer and must be between 0 and 1000000000")]
            IntegerDecimalErrorMessage,
            [Description("Enter value here")]
            IntegerDecimalPromptMsg
        }

        public enum ServiceName
        {
            NegotiationRevisitEmail,
            VectorServiceManager
        }
    }

    public static class EnumMgr
    {
        #region Enum Methods

        public static string Desc(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    DescriptionAttribute attr = Attribute.GetCustomAttribute(field,
                             typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Compares the enum values of same enum type 
        /// </summary>
        /// <typeparam name="T">enum</typeparam>
        /// <param name="obj1">First object of enum</param>
        /// <param name="obj2">Second object of enum</param>
        /// <returns>bool</returns>
        public static bool IsEqual<T>(object obj1, object obj2)
        {
            if (obj1 is T && obj2 is T)
            {
                if (((T)obj1).Equals((T)obj2))
                    return true;
                else
                    return false;
            }
            else
                return false;

        }

        public static T GetEnum<T>(string enumValue)
        {
            return (T)Enum.Parse(typeof(T), enumValue);
        }

        public static T GetEnumFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }
            return default(T);
        }
        #endregion
    }
}

using System.ComponentModel;

namespace Vector.Intacct.BusinessLogic
{
    public class EnumManager
    {
        public enum Columns
        {
            CUSTOMERID,
            CustomerName,
            IntacctIdFromRefuse,
            CompanyKey,
            Name,
            PrintableName,
            Inactive,
            Fax,
            Phone,
            Email,
            ContactName,
            PostalCode,
            State,
            City,
            Street2,
            Street1,
            Parent_Name,
            Parent_QBID,
            QBID,
            DateAdded,
            DateChanged,
            InvoiceNo,
            PropertyManagementName,
            PropertyName,
            InvoiceDate,
            HaulerName,
            HaulerInvoiceNo,
            AmountWR,
            CreateDate,
            DueDate,
            Invoice_QBID,
            Property_QBID,
            TotalCredits,
            IsActive,
            Customer_ListID,
            ListID,
            EditSequence,
            AccountNumber,
            CompanyName,
            BillAddress_Addr1,
            BillAddress_Addr2,
            BillAddress_City,
            BillAddress_State,
            BillAddress_PostalCode,
            Contact,
            Parent_FullName,
            Parent_ListID,
            Vendor_Addr1,
            Vendor_Addr2,
            Vendor_City,
            Vendor_State,
            Vendor_PostalCode,
            HaulerInvoiceDate,
            ContractID,
            TotalCommission,
            HaulerLocn,
            AmountSaved,
            AccountNO,
            HaulerPhone,
            BILL_QBID,
            RefNumber,
            TxnDate,
            Customer_FullName,
            Memo,
            _NetAmount,
            TxnID,
            AmountDue,
            TimeModified,
            _IsPaid,
            BalanceRemaining,
            _InvoiceNo,
            _InvoiceDate,
            SubTotal,
            INTACCT_ID,
            IntacctID,
            GroupNbr,
            state,
            totalamount,
            UniqueContactName,
            ContactFax,
            ContactEmail,
            ContactPhone,
            NAME,
            GroupStatus,
            FrachiseOrOpenMarket,
            PropertyId,
            PROJECTTYPE,
            STATUS,
            PROJECTID,
            AuditedDate,
            Item,
            GlAccountNumber,
            RECORDID,
            invoiceno,
            contactname,
            amount,
            memo,
            year,
            day,
            month,
            accountlabel,
            ponumber,
            description,
            customerid,
            key,
            totaldue,
            Key,
            recordid,
            PropertyID,
            BillingType,
            HaulerAmount,
            IntacctBillId,
            Currency
        }

        public enum ServieEndPointName
        {
            [Description("BasicHttpBinding_ILoginService")]
            LoginService,
            [Description("BasicHttpBinding_IMasterService")]
            MasterService,
            [Description("BasicHttpBinding_IInvoiceService")]
            InvoiceService,
            [Description("BasicHttpBinding_ILogExceptionService")]
            LogExceptionService
        }


        public enum Messages
        {
            [Description("Please enter valid Username & Password..!")]
            ValidUserNameAndPassword,

            [Description("Please enter credentials")]
            PleaseEnterCredentials
        }

        public enum IntacctType
        {
            Customer,
            Project,
            Property,
            Hauler,
            Invoice,
            ArInvoice,
            [Description("Repair RS Invoice QBIS's")]
            RepairInvoice,
            [Description("QB Dup Invoice Checker")]
            MarkDuplicateInvoice,
            [Description("Download Payments")]
            DownloadPayments,
            arpayment
        }

        public enum XMLNodes
        {
            [Description("\\Files\\XML\\Configuration.xml")]
            ConfigurationXML,
            [Description("//Configuration/CompanyFilePath")]
            CompanyFilePath,
            [Description("//Configuration/LogFilePath")]
            LogFilePath,
            [Description("//Configuration/AsiUtilities")]
            AsiUtilities,
            [Description("//Configuration/ServicePath")]
            ServicePath,
            [Description("//Configuration/RefuseAppName")]
            RefuseAppName,
            Configuration
        }

        public enum ProcessOptions
        {
            [Description(" - Update Run")]
            Update,
            [Description(" - Compare Only Run")]
            Compare,
            New
        }

        public enum LoggingOptions
        {
            [Description("All")]
            GoodError,
            [Description("ErrorsOnly")]
            ErrorOnly
        }
    }
}

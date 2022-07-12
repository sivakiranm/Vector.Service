using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Vector.Common.BusinessLayer;
using Vector.Intacct.APIAccess;
using Vector.Intacct.Entities;

namespace Vector.Intacct.BusinessLogic
{
    public class SyncEachRecord : BaseLogic
    {
        public static string PaymentMethod = "Payment Method";
        public static string DepositToAccount = "Deposit To Account";
        public static string constUndepositedfund = "Undeposited Funds";
        public static string ARAccount = "AR Account";
        public static string Customer = "Customer";
        public static string CustomerId = "Customer Id";
        public static string Date = "Date";
        public static string PaymentAmount = "Payment Amount";
        public static string ChkNo = "ChkNo.";
        public static string AppliedtoInvoiceRef = "Applied to Invoice Ref";
        public static string constBankid = "Citynational--City National";
        public static string constCustomerid = "C0035";
        public static string constUndepoisted = "1600--Software capitalized";
        public static string constinvoicekey = "99";
        public static string conUndepfun = "1130--Other Accounts Receivable";
        public static string constInvoice = "RECORDNO";
        public static string constARInvoice = " AR Invoice Payment";
        public static string invoiceDtls = "AR Payment Details";
        public static string AREntity = "AR Payments";
        public static string ARPAYMENT = "arpayment";
        public static string DateReceived = "datereceived";
        public static string ARPaymentItems = "arpaymentitems";
        public static string ARPaymentItem = "arpaymentitem";
        public static string ARPaymentDetails = "arpaymentdetails";
        public static string ARPaymentDetail = "arpaymentdetail";
        public static string LineItems = "lineitems";
        public static string LineItem = "lineitem";
        public static string ARPaymentMethod = "paymentmethod";
        public static string ARPaymentKey = "key";
        public static string ARPaymentAmount = "paymentapplied";
        public static string InvoiceKey = "invoicekey";
        public static string Amount = "amount";
        public static string LineItemId = "lineitem_Id";
        public static string ARPaymentId = "arpayment_Id";
        public static string whenModified = "whenmodified";
        public static string batchTitle = "batchtitle";
        public static string responseSuccessMessage = "Updated Successfully for Invoice No ";
        public static string responseErrorMessage = "Unable to Update Payment data for Invoice No ";
        public static string Paymentrefid = "refid";
        public static string ExchangeRate = "ExchangeRate";
        public static string PaymentDate = "PaymentDate";
        public static string AppliedtoInvoiceAmount = "Applied to Invoice Amount";
        public int arpaymentKey;
        public StringBuilder logFileContent = new StringBuilder();
        public StringBuilder SuccesslogFileContent = new StringBuilder();
        public StringBuilder FailurelogFileContent = new StringBuilder();
        public StringBuilder logRespose = new StringBuilder();
        public static string Currency = "Currency";

        public bool RunProcessForEachRecordForTemplate(DataTable dtTemplateData, IntacctLogin session, string logFilePath)
        {
            string headerDetails = string.Empty;
            bool retunvalue = false;
            GenerateLogFile(logFileContent, logFilePath);
            try
            {
                foreach (DataRow row in dtTemplateData.Rows)
                {
                    function objFunction = new function();
                    try
                    {
                        ARPaymentProcess(session, null, row, objFunction);
                    }
                    catch (Exception ex)
                    {
                        FailurelogFileContent.AppendLine(Constants.Error + ex.Message + Environment.NewLine + Constants.SkippingRemaining + invoiceDtls);

                        ErrorLog.GenerateErrorDetails(ex, string.Empty, string.Empty, string.Empty, Constants.TechnicalError);

                        ErrorLog.LogIntacctExceptions(string.Empty, SecurityContext.Instance.LogInUserId,
                                                                         SecurityContext.Instance.LogInPassword,
                                                                         AREntity,
                                                                         string.Empty,
                                                                         AREntity,
                                                                         ex.Message,
                                                                         session.objConnectionEntity.SessionId,
                                                                         SecurityManager.GetIPAddress.ToString(),
                                                                         Constants.TechnicalError,
                                                                         string.Empty);

                    }
                }

                retunvalue = true;
                if ((!string.IsNullOrEmpty(logFilePath)) && (!string.IsNullOrEmpty(logFileContent.ToString())))
                {
                    if (!string.IsNullOrEmpty(FailurelogFileContent.ToString()))
                    {
                        logFileContent.AppendLine(FailurelogFileContent.ToString());
                    }
                    SaveTextToFile(logFilePath, logFileContent.ToString());
                }
                if (!string.IsNullOrEmpty(FailurelogFileContent.ToString()))
                {
                    ErrorLog.LogIntacctExceptions(string.Empty, SecurityContext.Instance.LogInUserId,
                                                                    SecurityContext.Instance.LogInPassword,
                                                                    AREntity,
                                                                    string.Empty,
                                                                    AREntity,
                                                                    FailurelogFileContent.ToString(),
                                                                    string.Empty,
                                                                    SecurityManager.GetIPAddress.ToString(),
                                                                    Constants.FunctionalError,
                                                                    string.Empty);
                }
            }
            catch (Exception ex)
            {
                FailurelogFileContent.AppendLine(Constants.Error + ex.Message + Environment.NewLine + Constants.SkippingRemaining + invoiceDtls);
                ErrorLog.GenerateErrorDetails(ex, string.Empty, string.Empty, string.Empty, Constants.TechnicalError);

            }

            return retunvalue;
        }

        private void ARPaymentProcess(IntacctLogin session, DataTable InvoiceDt, DataRow row, function objFunction)
        {
            List<function> objFunctionList = new List<function>();
            create_arpayment newArPayment = new create_arpayment();
            ARPYMT newMultiCurrencyPayment;
            create createPayment = new create();

            if (!string.IsNullOrEmpty(row[Currency].ToString()))
            {
                if (StringManager.IsEqual(row[Currency].ToString(), "USD"))
                {
                    newArPayment = CreateARPaymentRecord(row, InvoiceDt, session, ref FailurelogFileContent);
                    restOfPaymentProcess(session, row, objFunction, objFunctionList, newArPayment);
                }
                else
                {
                    newMultiCurrencyPayment = CreateARPaymentRecordForCAD(row, InvoiceDt, session, ref FailurelogFileContent);
                    createPayment.ARPYMT = newMultiCurrencyPayment;
                    restOfPaymentProcessCAD(session, row, objFunction, objFunctionList, createPayment);
                }
            }
        }

        private void restOfPaymentProcess(IntacctLogin session, DataRow row, function objFunction, List<function> objFunctionList, create_arpayment newArPayment)
        {
            if (newArPayment != null)
            {
                if (!string.IsNullOrEmpty(row[Customer].ToString()) && !string.IsNullOrEmpty(row[AppliedtoInvoiceRef].ToString()))
                {
                    objFunction.controlid = "Customer: " + BusinessLayer.EscapeSpecailCharactersInHTML(row[Customer].ToString()) + ", Invoice No: " + row[AppliedtoInvoiceRef].ToString();
                }

                objFunction.ItemElementName = ItemChoiceType2.create_arpayment;
                objFunction.Item = newArPayment;
                objFunctionList.Add(objFunction);

                string response = string.Empty;

                if (objFunctionList != null && session != null)
                {
                    response = IntacctConnector.CreateUpdate(session, objFunctionList);
                }

                if (!string.IsNullOrEmpty(response))
                {
                    string responseMessageFormat = Constants.Success + ": AR Payment key {0} generated for  {1} transaction. ";
                    logRespose.AppendLine(Environment.NewLine + IntacctBusinessLogic.processUpdateResults(response, ref logFileContent, EnumManager.IntacctType.ArInvoice,false, Constants.FindSelectedNode, Constants.FindControlId, responseMessageFormat));
                }
            }
            else
            {
                FailurelogFileContent.AppendLine(Environment.NewLine + Constants.Failure + ": Provided Data is Invalid Data ");
                ErrorLog.LogIntacctExceptions(string.Empty, SecurityContext.Instance.LogInUserId,
                                                                                               SecurityContext.Instance.LogInPassword,
                                                                                               Constants.ArEntity,
                                                                                               string.Empty,
                                                                                               Constants.ArEntity,
                                                                                                "Provided Data is Invalid Data",
                                                                                               session.objConnectionEntity.SessionId,
                                                                                               SecurityManager.GetIPAddress.ToString(),
                                                                                               Constants.FunctionalError,
                                                                                               string.Empty);
            }
        }
        private void restOfPaymentProcessCAD(IntacctLogin session, DataRow row, function objFunction, List<function> objFunctionList, create newArPayment)
        {
            if (newArPayment != null)
            {
                if (!string.IsNullOrEmpty(row[Customer].ToString()) && !string.IsNullOrEmpty(row[AppliedtoInvoiceRef].ToString()))
                {
                    objFunction.controlid = "Customer: " + BusinessLayer.EscapeSpecailCharactersInHTML(row[Customer].ToString()) + ", Invoice No: " + row[AppliedtoInvoiceRef].ToString();
                }

                objFunction.ItemElementName = ItemChoiceType2.create;
                objFunction.Item = newArPayment;
                objFunctionList.Add(objFunction);
                string response = string.Empty;

                if (objFunctionList != null && session != null)
                {
                    response = IntacctConnector.CreateUpdate(session, objFunctionList);
                }

                if (!string.IsNullOrEmpty(response))
                {
                    string responseMessageFormat = Constants.Success + ": AR Payment key {0} generated for  {1} transaction. ";
                    logRespose.AppendLine(Environment.NewLine + IntacctBusinessLogic.processUpdateResults(response, ref logFileContent, EnumManager.IntacctType.ArInvoice, false, Constants.FindSelectedNode, Constants.FindControlId, responseMessageFormat));
                }
            }
            else
            {
                FailurelogFileContent.AppendLine(Environment.NewLine + Constants.Failure + ": Provided Data is Invalid Data ");
                ErrorLog.LogIntacctExceptions(string.Empty, SecurityContext.Instance.LogInUserId,
                                                                                               SecurityContext.Instance.LogInPassword,
                                                                                               Constants.ArEntity,
                                                                                               string.Empty,
                                                                                               Constants.ArEntity,
                                                                                                "Provided Data is Invalid Data",
                                                                                               session.objConnectionEntity.SessionId,
                                                                                               SecurityManager.GetIPAddress.ToString(),
                                                                                               Constants.FunctionalError,
                                                                                               string.Empty);
            }
        }

        public static create_arpayment CreateARPaymentRecord(DataRow row, DataTable dtInvoice, IntacctLogin session, ref StringBuilder failureLog)
        {
            create_arpayment arpayment = new create_arpayment();
            try
            {
                //PaymentMethod
                paymentmethod paymethod = new paymentmethod();
                if (!string.IsNullOrEmpty(row[PaymentMethod].ToString()))
                {
                    paymethod.Text = Common.AssignText(row[PaymentMethod].ToString());
                    arpayment.paymentmethod = paymethod;
                }

                //AccountType
                accounttype AcctType = new accounttype();
                if (!string.IsNullOrEmpty(row[DepositToAccount].ToString()))
                {
                    AcctType.Text = Common.AssignText(row[DepositToAccount].ToString());
                }

                //Customer
                //Insteadof Customer we need to send CustomerId
                string custid = string.Empty;
                if (!string.IsNullOrEmpty(row[CustomerId].ToString()))
                {
                    customerid customer = new customerid();


                    //Get customer id from uploaded excel and find it in intacct
                    custid = GetCustomerID(row[CustomerId].ToString(), session, ref failureLog);

                    if (!string.IsNullOrEmpty(custid))
                        customer.Text = Common.AssignText(custid);
                    else
                        return null;
                    arpayment.customerid = customer;
                }

                //RecieptDate -- instead we get datereceived
                if (!string.IsNullOrEmpty(row[Date].ToString()))
                {
                    datereceived recieptdate = new datereceived();

                    year yr = new year();
                    //string stryr=Convert.ToDateTime(row[Date].ToString()).Year.ToString();
                    yr.Text = Common.AssignText(Convert.ToDateTime(row[Date].ToString()).Year.ToString());
                    recieptdate.year = yr;

                    month mnth = new month();
                    mnth.Text = Common.AssignText(Convert.ToDateTime(row[Date].ToString()).Month.ToString());
                    recieptdate.month = mnth;

                    day dy = new day();
                    dy.Text = Common.AssignText(Convert.ToDateTime(row[Date].ToString()).Day.ToString());
                    recieptdate.day = dy;

                    arpayment.datereceived = recieptdate;
                }

                //DateOnCheck  Tobe filled in arpayment
                if (!string.IsNullOrEmpty(row[Date].ToString()))
                {
                    checkdate dtCheck = new checkdate();

                    year yr = new year();
                    //string stryr=Convert.ToDateTime(row[Date].ToString()).Year.ToString();
                    yr.Text = Common.AssignText(Convert.ToDateTime(row[Date].ToString()).Year.ToString());
                    dtCheck.year = yr;

                    month mnth = new month();
                    mnth.Text = Common.AssignText(Convert.ToDateTime(row[Date].ToString()).Month.ToString());
                    dtCheck.month = mnth;

                    day dy = new day();
                    dy.Text = Common.AssignText(Convert.ToDateTime(row[Date].ToString()).Day.ToString());
                    dtCheck.day = dy;
                }

                //CheckAmount as PaymentAmount
                if (!string.IsNullOrEmpty(row[PaymentAmount].ToString()))
                {
                    paymentamount payamt = new paymentamount();
                    payamt.Text = Common.AssignText(row[PaymentAmount].ToString());
                    arpayment.paymentamount = payamt;

                }

                //Check#  Tobe filled in arpayment
                if (!string.IsNullOrEmpty(row[ChkNo].ToString()))
                {
                    //checkno chknbr = new checkno();
                    refid refid = new refid();
                    refid.Text = Common.AssignText(row[ChkNo].ToString());
                    arpayment.refid = refid;

                }

                //Invoice key
                if (!string.IsNullOrEmpty(row[AppliedtoInvoiceRef].ToString()))
                {
                    List<arpaymentitem> lstpayitem = new List<arpaymentitem>();
                    arpaymentitem payitem = new arpaymentitem();
                    invoicekey key = new invoicekey();
                    string keyid = string.Empty;

                    keyid = GetInvoiceKeyBasedOnInvoiceNo(row[AppliedtoInvoiceRef].ToString(), custid, session);
                    if (!string.IsNullOrEmpty(keyid))
                        key.Text = Common.AssignText(keyid);
                    else
                        return null;

                    payitem.invoicekey = key;
                    amount amt = new amount();
                    amt.Text = Common.AssignText(row[PaymentAmount].ToString());
                    payitem.amount = amt;
                    lstpayitem.Add(payitem);
                    arpayment.arpaymentitem = lstpayitem.ToArray();
                }

                if (!string.IsNullOrEmpty(row[DepositToAccount].ToString()))
                {
                    undepfundsacct undepfund = new undepfundsacct();
                    undepfund.Text = Common.AssignText(row[DepositToAccount].ToString());
                    arpayment.Item = undepfund;
                }

            }
            catch (Exception ex)
            {
                failureLog.AppendLine(Constants.Error + ex.Message + Environment.NewLine + Constants.SkippingRemaining + invoiceDtls);
                ErrorLog.GenerateErrorDetails(ex, string.Empty, string.Empty, string.Empty, Constants.TechnicalError);
                ErrorLog.LogIntacctExceptions(ex.Message, SecurityContext.Instance.LogInUserId,
                                                                                               SecurityContext.Instance.LogInPassword,
                                                                                               Constants.ArEntity,
                                                                                               string.Empty,
                                                                                               Constants.ArEntity,
                                                                                               ex.Message,
                                                                                               session.objConnectionEntity.SessionId,
                                                                                               SecurityManager.GetIPAddress.ToString(),
                                                                                               Constants.TechnicalError,
                                                                                               string.Empty);
            }
            return arpayment;
        }
        public static ARPYMT CreateARPaymentRecordForCAD(DataRow row, DataTable dtInvoice, IntacctLogin session, ref StringBuilder failureLog)
        {
            ARPYMT arpayment = new ARPYMT();
            try
            {
                //Undeposited funds account 
                if (!string.IsNullOrEmpty(row[DepositToAccount].ToString()))
                {
                    undepositedaccountno undepfund = new undepositedaccountno();
                    undepfund.Text = Common.AssignText(row[DepositToAccount].ToString());
                    arpayment.undepositedaccountno = undepfund;
                }

                //Customer
                string custid = string.Empty;
                if (!string.IsNullOrEmpty(row[CustomerId].ToString()))
                {
                    customeridcad customer = new customeridcad();
                    //Get customer id from uploaded excel and find it in intacct
                    custid = GetCustomerID(row[CustomerId].ToString(), session, ref failureLog);

                    if (!string.IsNullOrEmpty(custid))
                        customer.Text = Common.AssignText(custid);
                    else
                        return null;
                    arpayment.customerid = customer;
                }

                //PaymentMethod
                paymentmethodCAD paymethod = new paymentmethodCAD();
                if (!string.IsNullOrEmpty(row[PaymentMethod].ToString()))
                {
                    paymethod.Text = Common.AssignText(row[PaymentMethod].ToString());
                    arpayment.paymentmethod = paymethod;

                }

                //intacct exchangerate Type
                exch_rate_type_id intacctExchangeRateType = new exch_rate_type_id();
                if (!string.IsNullOrEmpty(row[ExchangeRate].ToString()))
                {
                    intacctExchangeRateType.Text = Common.AssignText(row[ExchangeRate].ToString());
                    arpayment.exch_rate_type_id = intacctExchangeRateType;
                }
                //Receipt Date 
                receiptdate receiptdate = new receiptdate();
                if (!string.IsNullOrEmpty(row[Date].ToString()))
                {
                    receiptdate.Text = Common.AssignText(row[Date].ToString());
                    arpayment.receiptdate = receiptdate;
                }

                //Payment Date
                paymentdatecad paymentdate = new paymentdatecad();
                if (!string.IsNullOrEmpty(row[Date].ToString()))
                {
                    paymentdate.Text = Common.AssignText(row[Date].ToString());
                    arpayment.paymentdate = paymentdate;

                }

                //amounttoPay bank account
                if (!string.IsNullOrEmpty(row[PaymentAmount].ToString()))
                {
                    amounttopay payamt = new amounttopay();
                    payamt.Text = Common.AssignText(row[PaymentAmount].ToString());
                    arpayment.amounttopay = payamt;

                }

                //transaction amounttoPay
                trx_amounttopay trxpayamt = new trx_amounttopay();
                trxpayamt.Text = Common.AssignText("0");
                arpayment.trx_amounttopay = trxpayamt;

                //Base Currency
                basecur basecurrency = new basecur();
                basecurrency.Text = Common.AssignText("USD");
                arpayment.basecurr = basecurrency;

                //Invoice key
                if (!string.IsNullOrEmpty(row[AppliedtoInvoiceRef].ToString()))
                {
                    arpymtdetails arpaymentDetailss = new arpymtdetails();
                    List<arpymtdetail> lstpayitem = new List<arpymtdetail>();
                    arpymtdetail payitem = new arpymtdetail();
                    recordkey key = new recordkey();
                    string keyid = string.Empty;

                    keyid = GetInvoiceKeyBasedOnInvoiceNo(row[AppliedtoInvoiceRef].ToString(), custid, session);
                    if (!string.IsNullOrEmpty(keyid))
                        key.Text = Common.AssignText(keyid);
                    else
                        return null;

                    payitem.recordkey = key;
                    trx_paymentamount amt = new trx_paymentamount();
                    if (!string.IsNullOrEmpty(row[AppliedtoInvoiceAmount].ToString()))
                    {
                        amt.Text = Common.AssignText(row[AppliedtoInvoiceAmount].ToString());
                    }
                    payitem.trx_paymentamount = amt;
                    lstpayitem.Add(payitem);
                    arpaymentDetailss.arpymtdetail = payitem;
                    arpayment.arpymtdetails = arpaymentDetailss;
                }
            }
            catch (Exception ex)
            {
                failureLog.AppendLine(Constants.Error + ex.Message + Environment.NewLine + Constants.SkippingRemaining + invoiceDtls);
                ErrorLog.GenerateErrorDetails(ex, string.Empty, string.Empty, string.Empty, Constants.TechnicalError);
                ErrorLog.LogIntacctExceptions(ex.Message, SecurityContext.Instance.LogInUserId,
                                                                                               SecurityContext.Instance.LogInPassword,
                                                                                               Constants.ArEntity,
                                                                                               string.Empty,
                                                                                               Constants.ArEntity,
                                                                                               ex.Message,
                                                                                               session.objConnectionEntity.SessionId,
                                                                                               SecurityManager.GetIPAddress.ToString(),
                                                                                               Constants.TechnicalError,
                                                                                               string.Empty);
            }
            return arpayment;
        }

        //To get customerid by sending customer name to the intacct api using readbyquery
        public static string GetCustomerID(string CustomerId, IntacctLogin session, ref StringBuilder failureLog)
        {
            string custName = string.Empty;
            try
            {
                //DataRow[] foundRows;
                DataSet customerData = IntacctConnector.read("CUSTOMER", session, ItemChoiceType2.read, type: "readByName", key: CustomerId);
                if (!DataManager.IsNullOrEmptyDataSet(customerData))
                {
                    //it means data is in intacct,return customer id.
                    return CustomerId;
                }
                else
                {
                    failureLog.AppendLine(Constants.Error + " " + custName + " is not Found");
                }
            }
            catch (Exception ex)
            {
                failureLog.AppendLine(Constants.Error + ex.Message + Environment.NewLine + Constants.SkippingRemaining + invoiceDtls);
                ErrorLog.GenerateErrorDetails(ex, string.Empty, string.Empty, string.Empty, Constants.TechnicalError);
                ErrorLog.LogIntacctExceptions(ex.Message, SecurityContext.Instance.LogInUserId,
                                                                                               SecurityContext.Instance.LogInPassword,
                                                                                               Constants.ArEntity,
                                                                                               string.Empty,
                                                                                               Constants.ArEntity,
                                                                                               ex.Message,
                                                                                               session.objConnectionEntity.SessionId,
                                                                                               SecurityManager.GetIPAddress.ToString(),
                                                                                               Constants.TechnicalError,
                                                                                               string.Empty);
            }
            return CustomerId;

        }

        public DataTable GetCustomerDetails(IntacctLogin session, ref StringBuilder failureLog)
        {
            DataTable dtCust = new DataTable();
            try
            {
                DataSet IntacctCustds = IntacctConnector.read("CUSTOMER", session, ItemChoiceType2.read, type: "readByQuery");
                if (IntacctCustds != null && IntacctCustds.Tables.Count > 0)
                    return IntacctCustds.Tables["customer"];
            }
            catch (Exception ex)
            {

                failureLog.AppendLine(Constants.Error + ex.Message + Environment.NewLine + Constants.SkippingRemaining + invoiceDtls);
                ErrorLog.GenerateErrorDetails(ex, string.Empty, string.Empty, string.Empty, Constants.TechnicalError);
                ErrorLog.LogIntacctExceptions(ex.Message, SecurityContext.Instance.LogInUserId,
                                                                                               SecurityContext.Instance.LogInPassword,
                                                                                               Constants.ArEntity,
                                                                                               string.Empty,
                                                                                               Constants.ArEntity,
                                                                                               ex.Message,
                                                                                               session.objConnectionEntity.SessionId,
                                                                                               SecurityManager.GetIPAddress.ToString(),
                                                                                               Constants.TechnicalError,
                                                                                               string.Empty);
            }

            return dtCust;
        }

        public static void GenerateLogFile(StringBuilder logFileContent, string logfilePath)
        {
            logFileContent.AppendLine(Common.GetLogHeader(string.Empty, string.Empty, constARInvoice, logfilePath));
            logFileContent.AppendLine(Constants.DashLine);
        }

        public static void SaveTextToFile(string logFilePath, string strData)
        {
            using (FileStream fs = new FileStream(logFilePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(strData);
                }
            }
        }

        public static string GetInvoiceKeyBasedOnInvoiceNo(string InvoiceNo, string CustomerId, IntacctLogin session, string type = "", string compareString = "")
        {
            string invkey = string.Empty;
            DataSet IntacctInvoiceds = null;
            if (StringManager.IsEqual(type, "GetbillKey"))
            {
                IntacctInvoiceds = IntacctConnector.read(Constants.Bill, session, ItemChoiceType2.get_list, type: "get_list", expValue1: InvoiceNo, field1: Constants.BillNo);
                if (!DataManager.IsNullOrEmptyDataSet(IntacctInvoiceds))
                {
                    invkey = IntacctInvoiceds.Tables["bill"].Rows[0]["key"].ToString();
                }
                return invkey;
            }
            else
            {
                IntacctInvoiceds = IntacctConnector.read(Constants.Invoice, session, ItemChoiceType2.get_list, type: "get_list", expValue1: InvoiceNo, field1: Constants.InvoiceNo);
            }
            if (IntacctInvoiceds == null)
            {
                return Constants.SessionError;
            }
            if (IntacctInvoiceds != null && IntacctInvoiceds.Tables.Count > 0)
            {
                if (StringManager.IsNotEqual(type, "compare"))
                {
                    invkey = IntacctInvoiceds.Tables["invoice"].Rows[0]["key"].ToString();
                }
                else
                {
                    if (!DataManager.IsNullOrEmptyDataSet(IntacctInvoiceds))
                    {
                        DataRow[] invoiceRow;
                        invoiceRow = IntacctInvoiceds.Tables["Invoice"].Select();
                        DataRow invoiceSelectedRow = invoiceRow[Constants.Zero];

                        string state = Convert.ToString(invoiceSelectedRow[EnumManager.Columns.state.ToString()]);
                        string totalamount = Convert.ToString(invoiceSelectedRow[EnumManager.Columns.totalamount.ToString()]);
                        if (StringManager.IsNotEqual(compareString, Constants.Yes) && StringManager.IsNotEqual(state, Constants.Posted) && StringManager.IsNotEqual(totalamount, Constants.Zero.ToString()))
                        {
                            return Constants.Skip;
                        }
                        else
                        {
                            DataRow[] billToRow;
                            billToRow = IntacctInvoiceds.Tables["billto"].Select();
                            DataRow billToSelectedRow = billToRow[Constants.Zero];

                            DataRow[] dueDateRow;
                            dueDateRow = IntacctInvoiceds.Tables["datedue"].Select();
                            DataRow dueDateSelectedRow = dueDateRow[Constants.Zero];
                            string month = null;
                            string day = null;
                            string year = null;
                            foreach (DataRow dr in dueDateRow)
                            {
                                month = dr["month"].ToString();
                                day = dr["day"].ToString();
                                year = dr["year"].ToString();

                            }
                            string dueDate = null;
                            if (!string.IsNullOrEmpty(month))
                                dueDate = month + "/" + day + "/" + year;

                            DataRow[] linitemRow;
                            linitemRow = IntacctInvoiceds.Tables["lineitem"].Select();
                            DataRow lineItemSelectedRow = linitemRow[Constants.Zero];
                            string amount = Convert.ToString(lineItemSelectedRow[EnumManager.Columns.amount.ToString()]).Trim();

                            string amountTotalInDecimal = string.IsNullOrEmpty(amount) ? string.Empty : Convert.ToString(String.Format("{0:0.00}", Math.Round(Convert.ToDecimal(amount), 2)));

                            DataRow[] datepaidRow;
                            datepaidRow = IntacctInvoiceds.Tables["datecreated"].Select();
                            DataRow datepaidSelectedRow = datepaidRow[Constants.Zero];
                            month = null; day = null; year = null;

                            foreach (DataRow dr in datepaidRow)
                            {
                                month = dr["month"].ToString();
                                day = dr["day"].ToString();
                                year = dr["year"].ToString();

                            }
                            string auditedDate = null;
                            if (!string.IsNullOrEmpty(month))
                                auditedDate = month + "/" + day + "/" + year;

                            DataRow reportRow = invoiceRow[0];
                            return string.Format(Constants.UniqueInvoiceString,
                                                                                   Convert.ToString(invoiceSelectedRow[EnumManager.Columns.invoiceno.ToString()]).Trim(),
                                                                                    amountTotalInDecimal,
                                                                                   Convert.ToString(lineItemSelectedRow[EnumManager.Columns.memo.ToString()]).Trim(),
                                                                                   auditedDate,
                                                                                   dueDate,
                                                                                   Convert.ToString(invoiceSelectedRow[EnumManager.Columns.customerid.ToString()]).Trim(),
                                                                                   Convert.ToString(lineItemSelectedRow[EnumManager.Columns.accountlabel.ToString()]).Trim(),
                                                                                   Convert.ToString(invoiceSelectedRow[EnumManager.Columns.ponumber.ToString()]).Trim(),
                                                                                   Convert.ToString(invoiceSelectedRow[EnumManager.Columns.description.ToString()]).Trim(),
                                                                                   Convert.ToString(invoiceSelectedRow[EnumManager.Columns.key.ToString()]).Trim(),
                                                                                   Convert.ToString(invoiceSelectedRow[EnumManager.Columns.customerid.ToString()]).Trim());

                        }
                    }
                }

            }
            return invkey;
        }

        //To get invoice between from and to date range
        public static string GetInvoiceDetailsQueryStringForRange(string FromDate, string ToDate)
        {
            string replacingQuery = "<![CDATA[(WHENCREATED >= '" + FromDate + "'  AND WHENCREATED <= '" + ToDate + "')]]>";
            return replacingQuery;
        }

        public static string replacevalue1 = "<![CDATA[>=]]>";
        public static string replacevalue2 = "<![CDATA[<=]]>";
        public static DataSet GetInvoiceDetailsByQuery(IntacctLogin session, string expVal1, string expVal2)
        {
            DataSet IntacctInvoiceds = IntacctConnector.read("ARPAYMENT", session, ItemChoiceType2.get_list, type: "get_list", expValue1: expVal1, expValue2: expVal2, replaceString1: "QueryValue1", replaceString2: "QueryValue2", replaceWithString1: replacevalue1, replaceWithString2: replacevalue2);
            return IntacctInvoiceds;
        }


        public static string PaymentReverseSync(DataSet IntacctInvoiceds, string logfilePath, string headerdetails, IntacctLogin session, SelectionEntity objSelectionEntity)
        {
            StringBuilder PaymentlogFileContent = new StringBuilder();
            PaymentlogFileContent.AppendLine(Constants.DashLine);
            PaymentlogFileContent.AppendLine(Environment.NewLine + Constants.SyncReportForEntity + " AR Payments");
            if (IntacctInvoiceds != null && IntacctInvoiceds.Tables.Count > 0)
            {
                GenerateXmlFileForPaymentUpdateInRefuse(IntacctInvoiceds, ref PaymentlogFileContent, session, objSelectionEntity);
            }
            return PaymentlogFileContent.ToString();
        }


        public static void GenerateXmlFileForPaymentUpdateInRefuse(DataSet dsInvoicedata, ref StringBuilder PaymentlogFileContent, IntacctLogin session, SelectionEntity objSelectionEntity)
        {
            StringBuilder logfile = new StringBuilder();
            int lineItemcount = dsInvoicedata.Tables[ARPaymentItem].Rows.Count;
            int count = dsInvoicedata.Tables["lineitem"].Rows.Count;

            DataTable dtARPymentItem = new DataTable();
            dtARPymentItem.Columns.Add("invoiceKey");
            dtARPymentItem.Columns.Add("amount");
            dtARPymentItem.Columns.Add("arpaymentitems_Id");

            DataTable dtLineItem = new DataTable();
            dtLineItem.Columns.Add("amount");
            dtLineItem.Columns.Add("Key");
            dtLineItem.Columns.Add("lineitem_Id");

            string invoicedtls = string.Empty;
            for (int i = 0; i < lineItemcount; i++)
            {
                Vector.Intacct.Entities.ReceiveARPaymentDetails ARPmntDtls = new Vector.Intacct.Entities.ReceiveARPaymentDetails();
                string arpaymentKey = string.Empty;
                string arPaymentamount = string.Empty;
                string invoicekey = string.Empty;
                string comments = string.Empty;
                string lineitemxml = string.Empty;
                string paymentmethod = string.Empty;
                string lineitemamount = string.Empty;
                string arPayment_Id = string.Empty;
                string recievedDate = string.Empty;
                //DateTime recievedDt;
                string whenmodifiedValue = string.Empty;

                StringBuilder xmlARPayments = new StringBuilder();
                List<ReceiveARPaymentsLineItem> listLineItems = new List<ReceiveARPaymentsLineItem>();
                StringBuilder invDetls = new StringBuilder();

                DataRow dtInvoiceKey = dsInvoicedata.Tables["arpaymentitem"].Rows[i];

                DataRow dtlineItemDetails = dsInvoicedata.Tables[LineItem].Rows[i];

                int paymentid = (from c in dsInvoicedata.Tables["lineitems"].AsEnumerable()
                                 where c.Field<int>("lineitems_id") == dtlineItemDetails.Field<int>("lineitems_id")
                                 select c.Field<int>("arpayment_Id")).FirstOrDefault();

                var Arpayment = (from c in dsInvoicedata.Tables["arpayment"].AsEnumerable()
                                 where c.Field<int>("arpayment_Id") == paymentid
                                 select c).FirstOrDefault();

                if (dtInvoiceKey != null)
                {

                    arPayment_Id = Convert.ToString(paymentid);

                    if (!string.IsNullOrEmpty(dtInvoiceKey["invoicekey"].ToString()))
                    {
                        ARPmntDtls.PaymentIntacctID = dtInvoiceKey["invoicekey"].ToString();
                    }
                    if (!string.IsNullOrEmpty(Arpayment[ARPaymentMethod].ToString()))
                    {
                        ARPmntDtls.PaymentMethod = Arpayment[ARPaymentMethod].ToString();
                    }
                    if (!string.IsNullOrEmpty(Arpayment[ARPaymentAmount].ToString()))
                    {
                        ARPmntDtls.TotalAmount = Convert.ToDouble(Arpayment[ARPaymentAmount].ToString());
                    }
                    if (!string.IsNullOrEmpty(Arpayment[whenModified].ToString()))
                    {
                        ARPmntDtls.WhenModified = Convert.ToDateTime(Arpayment[whenModified].ToString());
                    }
                    if (!string.IsNullOrEmpty(Arpayment[batchTitle].ToString()))
                    {

                        ARPmntDtls.Comments = Arpayment[batchTitle].ToString();
                    }
                    if (!string.IsNullOrEmpty(Arpayment[Paymentrefid].ToString()))
                    {
                        ARPmntDtls.Refid = Arpayment[Paymentrefid].ToString();
                    }

                }

                DataSet PaymentLineItemDetails = GetInvoiceLineItemDetailsByKey(session, dtInvoiceKey.Field<string>("invoicekey"));
                if (!DataManager.IsNullOrEmptyDataSet(PaymentLineItemDetails))
                {
                    whenmodifiedValue = PaymentLineItemDetails.Tables["invoice"].Rows[0]["whenmodified"].ToString();


                    DataSet IntacctInvKeyds = new DataSet();

                    foreach (DataRow dr in PaymentLineItemDetails.Tables["lineitem"].Rows)
                    {
                        ReceiveARPaymentsLineItem lineARItem = new ReceiveARPaymentsLineItem();

                        lineARItem.InvoiceKey = dtInvoiceKey["invoicekey"].ToString();

                        //To find the Invoice no based on invoiceKey to display in the log file
                        if (!string.IsNullOrEmpty(lineARItem.InvoiceKey))
                        {
                            IntacctInvKeyds = IntacctConnector.read(Constants.Invoice, session, ItemChoiceType2.get_list, type: "get_list", expValue1: lineARItem.InvoiceKey, field1: "key");
                            if (IntacctInvKeyds != null && IntacctInvKeyds.Tables.Count > 0)
                            {
                                invoicedtls = IntacctInvKeyds.Tables[0].Rows[0]["invoiceno"].ToString();
                            }
                        }

                        if (!string.IsNullOrEmpty(dr["totalpaid"].ToString()))
                        {
                            lineARItem.Amount = dr["totalpaid"].ToString();
                        }

                        lineARItem.WhenModified = Convert.ToDateTime(whenmodifiedValue);

                        lineARItem.BalanceRemaining = dr["totaldue"].ToString();

                        lineARItem.LineItemKey = dr["key"].ToString();

                        listLineItems.Add(lineARItem);

                        ARPmntDtls.LineItems = listLineItems;
                    }

                    //To  Build and Send RecievePayment Object

                    if (dtARPymentItem != null)
                    {
                        string generatedXml = GetReceiptPaymentLineItemsXML(ARPmntDtls.LineItems);
                        try
                        {
                            using (IntacctBOLayer objMasterService = new IntacctBOLayer())
                            {
                                var response = (VectorResponse<object>)objMasterService.UpdateReceiptPayment(string.Empty, ARPmntDtls.Comments, ARPmntDtls.PaymentMethod, ARPmntDtls.WhenModified, ARPmntDtls.TotalAmount, generatedXml, ARPmntDtls.PaymentIntacctID);
                                if (ValidateResponse(response))
                                {
                                    if (string.Equals(response.Response.ToString().ToLower(), Constants.Success))
                                    {
                                        PaymentlogFileContent.AppendLine(Environment.NewLine + "Payment Amount " + ARPmntDtls.LineItems[0].Amount + " is " + responseSuccessMessage + invoicedtls);
                                    }
                                    else
                                    {
                                        PaymentlogFileContent.AppendLine(Environment.NewLine + responseErrorMessage + invoicedtls + ". " + response.Response.ToString());
                                    }
                                }
                                else
                                {
                                    ErrorLog.GenerateErrorDetails(response.Response.ToString(), Constants.ArEntity, string.Empty, string.Empty, Constants.TechnicalError);
                                    ErrorLog.LogIntacctExceptions(response.Response.ToString(), SecurityContext.Instance.LogInUserId,
                                                                                             SecurityContext.Instance.LogInPassword,
                                                                                             Constants.ArEntity,
                                                                                             string.Empty,
                                                                                             Constants.ArEntity,
                                                                                             response.Response.ToString(),
                                                                                             session.objConnectionEntity.SessionId,
                                                                                             SecurityManager.GetIPAddress.ToString(),
                                                                                             Constants.FunctionalError,
                                                                                             string.Empty);

                                }


                            }
                        }

                        catch (Exception ex)
                        {
                            PaymentlogFileContent.AppendLine(Constants.Error + ex.Message + Environment.NewLine + Constants.SkippingRemaining + invoiceDtls);
                            ErrorLog.GenerateErrorDetails(ex, string.Empty, string.Empty, string.Empty, Constants.TechnicalError);
                            ErrorLog.LogIntacctExceptions(string.Empty, SecurityContext.Instance.LogInUserId,
                                                                                     SecurityContext.Instance.LogInPassword,
                                                                                     AREntity,
                                                                                     string.Empty,
                                                                                     AREntity,
                                                                                     ex.Message,
                                                                                     string.Empty,
                                                                                     SecurityManager.GetIPAddress.ToString(),
                                                                                     Constants.TechnicalError,
                                                                                     string.Empty);
                        }
                    }
                }
                else
                {
                    string msg = "Invoice is not Present for this Payment Reference : " + ARPmntDtls.PaymentIntacctID;

                    PaymentlogFileContent.AppendLine(Environment.NewLine + Constants.Error + msg + Environment.NewLine);
                    ErrorLog.GenerateErrorDetails(msg, string.Empty, string.Empty, string.Empty, Constants.TechnicalError);
                    ErrorLog.LogIntacctExceptions(msg, SecurityContext.Instance.LogInUserId,
                                                                             SecurityContext.Instance.LogInPassword,
                                                                             Constants.ArEntity,
                                                                             string.Empty,
                                                                             Constants.ArEntity,
                                                                             msg,
                                                                             session.objConnectionEntity.SessionId,
                                                                             SecurityManager.GetIPAddress.ToString(),
                                                                             Constants.FunctionalError,
                                                                             string.Empty);
                }
                IntacctBusinessLogic.SetRowProcessCounts(Convert.ToString(lineItemcount),
                                         Convert.ToString(i + 1),
                                          Convert.ToString(lineItemcount - 1 - i),
                               objSelectionEntity.lblArPaymentToProcess, objSelectionEntity.lblArPaymentProcessed, objSelectionEntity.lblArPaymentRemaning);

            }
        }

        public static DataSet GetInvoiceLineItemDetailsByKey(IntacctLogin session, string Key)
        {
            DataSet LineItemDetails = IntacctConnector.read("invoice", session, ItemChoiceType2.get_invoice, type: "get_invoice", key: Key);
            return LineItemDetails;
        }



        private enum LineItemsEnum
        {
            ReceivePaymentsLineItems,
            ReceivePaymentsLineItem,
            InvoiceKey,
            Amount,
            BalanceRemaining,
            WhenModified,
            LineItemKey
        }
        private static string GetReceiptPaymentLineItemsXML(List<ReceiveARPaymentsLineItem> lineitems)
        {
            XDocument lineitemXML = new XDocument(new XElement(LineItemsEnum.ReceivePaymentsLineItems.ToString()));
            lineitems.ForEach(item => lineitemXML.Element(LineItemsEnum.ReceivePaymentsLineItems.ToString())
                                    .Add(new XElement(LineItemsEnum.ReceivePaymentsLineItem.ToString(),
                                                        new XElement(LineItemsEnum.InvoiceKey.ToString(), item.InvoiceKey),
                                                        new XElement(LineItemsEnum.Amount.ToString(), item.Amount),
                                                        new XElement(LineItemsEnum.BalanceRemaining.ToString(), item.BalanceRemaining),
                                                         new XElement(LineItemsEnum.WhenModified.ToString(), item.WhenModified),
                                                        new XElement(LineItemsEnum.LineItemKey.ToString(), item.LineItemKey)
                                                        )
                                                        ));
            return lineitemXML.ToString();

        }
        private static string GenerateXmlFromTable(DataTable dataTable)
        {
            string xmlARPayment = string.Empty;
            using (StringWriter sw = new StringWriter())
            {
                dataTable.WriteXml(sw);
                xmlARPayment = sw.ToString();
            }

            return xmlARPayment;

        }

        public static bool ValidateResponse(VectorResponse<object> res)
        {
            if (res != null && StringManager.IsNotEqual(res.ResponseType, Constants.Exception))
            {
                if (!string.IsNullOrEmpty(res.Response.ToString()))
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
            return false;
        }
    }
}

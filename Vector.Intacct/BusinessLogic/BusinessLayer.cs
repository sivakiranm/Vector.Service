using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Xml;
using Vector.Common.BusinessLayer;
using Vector.Intacct.Entities;

namespace Vector.Intacct.BusinessLogic
{
    public class BusinessLayer
    {
        /// <summary>
        /// It is used to assign text in string array fow assigning to baseclass objects
        /// </summary>
        /// <param name="text">text to be assigned</param>
        /// <returns>returns the string array text</returns>
        public static string[] AddText(string text)
        {
            string[] objValues = new string[1];
            objValues[0] = text;
            return objValues;
        }

        /// <summary>
        /// It is used to add multiple functions to function object to post the data
        /// </summary>
        /// <param name="objFunctionList">List of function used</param>
        /// <returns>returns baseclass function object</returns>
        public static function[] AddFunctions(List<function> objFunctionList)
        {
            if (objFunctionList != null && objFunctionList.Count > 0)
            {
                function[] objFunctionValues = new function[objFunctionList.Count];
                int i = 0;
                foreach (function objFunction in objFunctionList)
                {
                    objFunctionValues[i] = objFunction;
                    i++;
                }
                return objFunctionValues;
            }
            else
                return null;
        }

        /// <summary>
        /// Add List of contents to baseclass content object
        /// </summary>
        /// <param name="objContentList">List of content objects</param>
        /// <returns>returns baseclass content object</returns>
        public static content[] AddContents(content objContentList)
        {
            if (objContentList != null)
            {
                content[] objContentValues = new content[1];
                objContentValues[0] = objContentList;
                return objContentValues;
            }
            else
                return null;
        }

        /// <summary>
        /// List of operations are added to operation object
        /// </summary>
        /// <param name="objOperationList">List of operations that are used to post the method</param>
        /// <returns>returns base class operation object</returns>
        public static operation[] AddOperations(operation objOperationList)
        {
            if (objOperationList != null)
            {
                operation[] objOperationValues = new operation[1];
                objOperationValues[0] = objOperationList;
                return objOperationValues;
            }
            else
                return null;
        }

        public static void createCustomerRecord(DataRow pkrmRow, string CustomerName, string GroupNbr, List<function> objFunctionList, SelectionEntity objSelectionEntity, ref StringBuilder logFileContent)
        {
            //string CustomerID = pkrmRow[Constants.IntacctIdFromRefuse].ToString();
            create_customer objCreateCustomer = new create_customer();
            function objFunction = new function();
            //CustomerID
            customerid objCustomerID = new customerid();
            string customerID = GroupNbr.ToString();
            objCustomerID.Text = new string[] { customerID };

            //Customer Name
            name objCustomerName = new name();
            string customerName = EscapeSpecailCharactersInHTML(pkrmRow[Constants.Name].ToString());
            //string customerName = System.Web.HttpUtility.UrlEncode(pkrmRow[Constants.Name].ToString());
            objCustomerName.Text = new string[] { customerName };

            //All Primary Contact Details
            contactinfo objcontactinfo = new contactinfo();

            //primary objPrimaryContactDetails = new primary();
            contact objCustomerPrimaryContact = new contact();
            mailaddress objMailAddress = new mailaddress();
            //Print As
            printas objPrintAs = new printas();
            string customerPrintAs = EscapeSpecailCharactersInHTML(pkrmRow[Constants.Name.ToString()].ToString());
            objPrintAs.Text = new string[] { customerPrintAs };
            //contact Name
            contactname objContactName = new contactname();
            string customerContactName = EscapeSpecailCharactersInHTML(pkrmRow[Constants.intacctColumnNames.UniqueContactName.ToString()].ToString());
            objContactName.Text = new string[] { customerContactName };

            //Address 1
            address1 objAddress1 = new address1();
            string customerAddress1 = EscapeSpecailCharactersInHTML(pkrmRow[Constants.intacctColumnNames.Street1.ToString()].ToString());
            objAddress1.Text = new string[] { customerAddress1 };

            //Address 2
            address2 objAddress2 = new address2();
            string customerAddress2 = EscapeSpecailCharactersInHTML(pkrmRow[Constants.intacctColumnNames.Street2.ToString()].ToString());
            objAddress2.Text = new string[] { customerAddress2 };

            //CITY
            city objCustomerCity = new city();
            string customerCity = EscapeSpecailCharactersInHTML(pkrmRow[Constants.intacctColumnNames.City.ToString()].ToString());
            objCustomerCity.Text = new string[] { customerCity };

            //State
            state objState = new state();
            string customerState = pkrmRow[Constants.intacctColumnNames.STATE.ToString()].ToString();
            objState.Text = new string[] { customerState };

            //Zip
            zip objZip = new zip();
            string customerZip = pkrmRow[Constants.intacctColumnNames.PostalCode.ToString()].ToString();
            objZip.Text = new string[] { customerZip };

            //LastName
            lastname objLastName = new lastname();
            string customerLastName = EscapeSpecailCharactersInHTML(pkrmRow[Constants.intacctColumnNames.UniqueContactName.ToString()].ToString());
            objLastName.Text = new string[] { customerLastName };

            //Fax
            fax objFax = new fax();
            string customerFaxNumber = pkrmRow[Constants.intacctColumnNames.Fax.ToString()].ToString();
            objFax.Text = new string[] { customerFaxNumber };

            //Email
            email1 objEmail = new email1();
            string customerEmail = pkrmRow[Constants.intacctColumnNames.ContactEmail.ToString()].ToString();
            objEmail.Text = new string[] { customerEmail };

            //Phone1
            phone1 objPhone = new phone1();
            string customerPhone = pkrmRow[Constants.intacctColumnNames.ContactPhone.ToString()].ToString();
            objPhone.Text = new string[] { customerPhone };

            //Filling the primary contact entity for the customer here
            objCustomerPrimaryContact.contactname = objContactName;
            objCustomerPrimaryContact.printas = objPrintAs;
            objMailAddress.address1 = objAddress1;
            objMailAddress.address2 = objAddress2;
            objMailAddress.city = objCustomerCity;
            objMailAddress.state = objState;
            objMailAddress.zip = objZip;
            objCustomerPrimaryContact.mailaddress = objMailAddress;
            objCustomerPrimaryContact.lastname = objLastName;
            objCustomerPrimaryContact.fax = objFax;
            objCustomerPrimaryContact.email1 = objEmail;
            objCustomerPrimaryContact.phone1 = objPhone;
            objcontactinfo.Item = objCustomerPrimaryContact;
            //Customer Status
            status objStatus = new status();
            string customerStatus = pkrmRow[Constants.intacctColumnNames.GroupStatus.ToString()].ToString();
            objStatus.Text = new string[] { customerStatus };

            //Assigning Updated Values 
            objCreateCustomer.customerid = objCustomerID;
            objCreateCustomer.name = objCustomerName;
            objCreateCustomer.contactinfo = objcontactinfo;
            objCreateCustomer.status = objStatus;

            objFunction.controlid = customerName.ToString();
            objFunction.ItemElementName = ItemChoiceType2.create_customer;
            objFunction.Item = objCreateCustomer;
            if (objSelectionEntity.isBulk)
            {
                objFunctionList.Add(objFunction);
            }
            else
            {
                List<function> newSeqListCreateCustomer = new List<function>();
                newSeqListCreateCustomer.Add(objFunction);
                IntacctBusinessLogic.PostDataSeQuential(objSelectionEntity, newSeqListCreateCustomer, ref logFileContent);
                newSeqListCreateCustomer.Clear();
            }
        }

        public static void createPropertyRecord(DataRow pkrmRow, List<function> objFunctionList, SelectionEntity objSelectionEntity, ref StringBuilder logFileContent)
        {
            //Create Property Record with the following 5.
            create_project objCreateProject = new create_project();
            function objFunction = new function();

            //Property Id
            projectid objPropertyId = new projectid(); 
            string propertyId = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.PropertyId.ToString()]));
            objPropertyId.Text = new string[] { propertyId };

            //Property Name
            name objPropertyName = new name();
            string propertyName = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.Name.ToString()]));
            objPropertyName.Text = new string[] { propertyName };

            //Customer Id          
            customerid objCustomerID = new customerid();
            string customerID = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.GroupNbr.ToString()]));
            objCustomerID.Text = new string[] { customerID };

            //Property Type
            projecttype objprojectType = new projecttype();
            string projectType = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.FrachiseOrOpenMarket.ToString()]));
            objprojectType.Text = new string[] { projectType };

            //Property Status

            status objStatus = new status();
            string propertyStatus = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.Inactive.ToString()]));
            objStatus.Text = new string[] { propertyStatus };

            //Property Category
            projectcategory objProjectCategory = new projectcategory();
            string projectCat = "Contract";
            objProjectCategory.Text = new string[] { projectCat };

            //property Currency
            currency objProjectCurrency = new currency();
            string projectCurrency = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.Currency.ToString()]));
            objProjectCurrency.Text = new string[] { projectCurrency };

            objCreateProject.projectid = objPropertyId;
            objCreateProject.name = objPropertyName;
            objCreateProject.customerid = objCustomerID;
            objCreateProject.projecttype = objprojectType;
            objCreateProject.status = objStatus;
            objCreateProject.projectcategory = objProjectCategory;
            objCreateProject.currency = objProjectCurrency;

            objFunction.controlid = propertyName.ToString();
            objFunction.ItemElementName = Entities.ItemChoiceType2.create_project;
            objFunction.Item = objCreateProject;

            if (objSelectionEntity.isBulk)
            {
                objFunctionList.Add(objFunction);
            }
            else
            {
                List<function> newSeqListCreateProperty = new List<function>();
                newSeqListCreateProperty.Add(objFunction);
                IntacctBusinessLogic.PostDataSeQuential(objSelectionEntity, newSeqListCreateProperty, ref logFileContent);
                newSeqListCreateProperty.Clear();
            }
        }
        public static void createInvoiceRecord(DataRow pkrmRow, List<function> objFunctionList, SelectionEntity objSelectionEntity, ref StringBuilder logFileContent)
        {
            string billingType = pkrmRow[EnumManager.Columns.BillingType.ToString()].ToString();
            function objFunction = new function();
            objFunction = BuildInvoiceEntity(pkrmRow, billingType);

            if (objSelectionEntity.isBulk)
            {
                objFunctionList.Add(objFunction);
            }
            else
            {
                List<function> newSeqListCreateInvoice = new List<function>();
                newSeqListCreateInvoice.Add(objFunction);

                string response = IntacctBusinessLogic.PostDataSeQuential(objSelectionEntity, newSeqListCreateInvoice, ref logFileContent);
                XmlDocument simpleXml = new XmlDocument();
                simpleXml.LoadXml(response);
                string status = simpleXml.SelectSingleNode(Constants.ResponseOperationResultStatus).InnerXml;
                if (!string.IsNullOrEmpty(status))
                {
                    if (status == Constants.Success)
                    {
                        if (StringManager.IsEqual(billingType, Constants.BillPayRsHauler.ToString()) || StringManager.IsEqual(billingType, Constants.BillPaySavingsShare.ToString()))
                        {
                            SyncBillForInvoice(pkrmRow, ref logFileContent, objSelectionEntity);
                        }
                    }
                }
                newSeqListCreateInvoice.Clear();
            }
        }

        public static void CreateMissingEntity(DataRow pkrmRow, List<function> objFunctionList, SelectionEntity objSelectionEntity, ref StringBuilder logFileContent, string IntacctString, string billstring)
        {
            string billingType = pkrmRow[EnumManager.Columns.BillingType.ToString()].ToString();
            function objFunction = new function();
            if (string.IsNullOrEmpty(IntacctString) && !string.IsNullOrEmpty(billstring))
            {
                objFunction = BuildInvoiceEntity(pkrmRow, billingType);
                List<function> newSeqListCreateInvoice = new List<function>();
                newSeqListCreateInvoice.Add(objFunction);
                string response = IntacctBusinessLogic.PostDataSeQuential(objSelectionEntity, newSeqListCreateInvoice, ref logFileContent);
                //Create only invoice record not Bill Record
            }
            if (string.IsNullOrEmpty(billstring) && !string.IsNullOrEmpty(IntacctString))
            {
                //Create only BillRecord record not Invoice Record
                SyncBillForInvoice(pkrmRow, ref logFileContent, objSelectionEntity);
            }
        }

        public static void SyncBillForInvoice(DataRow pkrmRow, ref StringBuilder logFileContent, SelectionEntity objSelectionEntity)
        {
            try
            {
                function objFunction = new function();
                objFunction = BuildDummyBillEntityForHaulerAmount(pkrmRow);
                List<function> newSeqListCreateInvoice = new List<function>();
                newSeqListCreateInvoice.Add(objFunction);
                IntacctBusinessLogic.PostDataSeQuential(objSelectionEntity, newSeqListCreateInvoice, ref logFileContent);
                newSeqListCreateInvoice.Clear();
            }
            catch (Exception ex)
            {
                ErrorLog.LogIntacctExceptions(string.Empty, SecurityContext.Instance.LogInUserId,
                                                                                    SecurityContext.Instance.LogInPassword,
                                                                                    "DuplicateHaulerAmountToBillsEntity",
                                                                                    string.Empty,
                                                                                    "BillCopyOfInvoice",
                                                                                    "",
                                                                                    "",
                                                                                    SecurityManager.GetIPAddress.ToString(),
                                                                                    Constants.FunctionalError,
                                                                                    string.Empty);
            }
        }

        private static function BuildInvoiceEntity(DataRow pkrmRow, string billingType)
        {
            //Create Invoice Record 

            create_invoice objCreateInvoice = new create_invoice();
            function objFunction = new function();

            //Adding CustomerId

            customerid objCustomerId = new customerid();
            string customerId = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.GroupNbr.ToString()]));
            objCustomerId.Text = new string[] { customerId };


            //Add AMOUNT to LINEITEM and that to InvoiceLineItems:
            amount objAmount = new amount();
            string amount = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.AmountDue.ToString()]));
            objAmount.Text = new string[] { amount };

            //Add HaulerAMOUNT to LINEITEM and that to InvoiceLineItems:    
            amount objAmountHauler = new amount();
            string Hauleramount = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.HaulerAmount.ToString()]));
            objAmountHauler.Text = new string[] { Hauleramount };

            lineitem objLineItem = new lineitem();
            objLineItem.amount = objAmount;

            lineitem objLineItemHaulerAmount = new lineitem();
            objLineItemHaulerAmount.amount = objAmountHauler;

            invoiceitems objInvoiceItems = new invoiceitems();
            objInvoiceItems.lineitem = new lineitem[] { objLineItem, objLineItemHaulerAmount };

            //Add HaulerName i.e memo in intacct as per BRD:
            memo objMemo = new memo();
            string memo = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.HaulerName.ToString()]));
            objMemo.Text = new string[] { memo };
            objLineItem.memo = objMemo;
            objLineItemHaulerAmount.memo = objMemo;

            projectid objProjectId = new projectid();
            string projectId = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.PropertyID.ToString()]));
            objProjectId.Text = new string[] { projectId };
            objLineItem.projectid = objProjectId;
            objLineItemHaulerAmount.projectid = objProjectId;

            //Add Invoice Audit Date ie datecreated in intacct:              
            datecreated objdateDateCreated = new datecreated();
            year objDateCreatedYear = new year();
            month objDateCreatedMonth = new month();
            day objDateCreatedDay = new day();
            string datePaid = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.AuditedDate.ToString()]));
            //TODO need to check datepaid is null or empty
            DateTime paidDate;
            paidDate = Convert.ToDateTime(datePaid);
            string paidYear = paidDate.Year.ToString();
            string paidMonth = paidDate.Month.ToString();
            string paidDay = paidDate.Day.ToString();

            objDateCreatedYear.Text = new string[] { paidYear };
            objDateCreatedMonth.Text = new string[] { paidMonth };
            objDateCreatedDay.Text = new string[] { paidDay };

            objdateDateCreated.day = objDateCreatedDay;
            objdateDateCreated.month = objDateCreatedMonth;
            objdateDateCreated.year = objDateCreatedYear;

            //Add Invoice Number:
            invoiceno objInvoiceNo = new invoiceno();
            string invoiceNo = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.InvoiceNo.ToString()]));
            objInvoiceNo.Text = new string[] { invoiceNo };

            //Add contact name as Property Name to billto:

            contactname objContactName = new contactname();
            string contactname = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.PropertyName.ToString()]));
            objContactName.Text = new string[] { contactname };
            billto objBillto = new billto();
            objBillto.Item = objContactName;

            //Add due date
            datedue objDateDue = new datedue();

            year objYear = new year();
            month objMonth = new month();
            day objDay = new day();

            string dueDay = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.AuditedDate.ToString()]));
            //Adding 30 days to the Invoice date as requested
            DateTime date = Convert.ToDateTime(dueDay);
            string year = date.AddDays(Convert.ToInt16(SecurityManager.GetConfigValue(Constants.Add30DaysToInvoiceDate))).Year.ToString();
            string month = date.AddDays(Convert.ToInt16(SecurityManager.GetConfigValue(Constants.Add30DaysToInvoiceDate))).Month.ToString();
            string day = date.AddDays(Convert.ToInt16(SecurityManager.GetConfigValue(Constants.Add30DaysToInvoiceDate))).Day.ToString();

            objYear.Text = new string[] { year };
            objMonth.Text = new string[] { month };
            objDay.Text = new string[] { day };

            objDateDue.year = objYear;
            objDateDue.month = objMonth;
            objDateDue.day = objDay;

            //Adding CustomerName or CustomerNo to Customer..???
            //Adding AccountLabel to the Item.
            accountlabel objAccountLabel = new accountlabel();
            string accountLabel = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.Item.ToString()]));
            objAccountLabel.Text = new string[] { accountLabel };

            //Adding reference number i.e hauler account number here in RS
            ponumber objPoNo = new ponumber();
            string poNumber = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.AccountNO.ToString()]));
            objPoNo.Text = new string[] { poNumber };

            //Adding Hauler Invoice# in RS ie Description in the intacct

            description objDescription = new description();
            string description = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.HaulerInvoiceNo.ToString()]));
            objDescription.Text = new string[] { description };

            //Adding currency from the property
            currency objCurrency = new currency();
            string currency = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.Currency.ToString()]));
            objCurrency.Text = new string[] { currency };


            //Adding exchangeratetype 
            exchratetype objechratetype = new exchratetype();
            string exchangeratetype = "Intacct Daily Rate";
            objechratetype.Text = new string[] { exchangeratetype };

            //Adding glAccountNumber to the intacct since it is mandatory (As of now we are using 8888)
            glaccountno objGlAccountNo = new glaccountno();
            string glAccountNo = SecurityManager.GetConfigValue(Constants.GlAccountNo).ToString();
            objGlAccountNo.Text = new string[] { glAccountNo };
            objLineItem.Item = objGlAccountNo;

            //Hauler GLAccountNumber
            glaccountno objGlHaulerAccNo = new glaccountno();
            string glAcctHaulerNo = SecurityManager.GetConfigValue(Constants.GlHaulerAcctNo).ToString();
            objGlHaulerAccNo.Text = new string[] { glAcctHaulerNo };

            objLineItemHaulerAmount.Item = objGlHaulerAccNo;

            //Creating a custom field for property name.
            customfieldname objCustomFieldName = new customfieldname();
            string propertyName = "PROPERTYNAME";
            objCustomFieldName.Text = new string[] { propertyName };

            objCreateInvoice.customerid = objCustomerId;
            objCreateInvoice.invoiceno = objInvoiceNo;
            objCreateInvoice.datedue = objDateDue;
            objCreateInvoice.description = objDescription;
            objCreateInvoice.currency = objCurrency;
            objCreateInvoice.exchratetype = objechratetype;
            if (StringManager.IsEqual(billingType, Constants.DirectPaySavingsShare))
            {
                objCreateInvoice.invoiceitems = new lineitem[] { objLineItem };

            }
            else
            {
                objCreateInvoice.invoiceitems = new lineitem[] { objLineItem, objLineItemHaulerAmount };
            }
            objCreateInvoice.ponumber = objPoNo;
            objCreateInvoice.datecreated = objdateDateCreated;
            //objCreateInvoice.customfields = new customfield[] { objCustomField };


            objFunction.controlid = objInvoiceNo.Text[Constants.Zero];
            objFunction.ItemElementName = ItemChoiceType2.create_invoice;
            objFunction.Item = objCreateInvoice;
            return objFunction;
        }
        
        private static function BuildDummyBillEntityForHaulerAmount(DataRow pkrmRow)
        {
            //Create Invoice Record 

            create_bill objCreateBill = new create_bill();
            function objFunction = new function();

            //Add vendorid from the Configuration file since they are ok with that

            vendorid objVendorId = new vendorid();
            string vendorId = SecurityManager.GetConfigValue(Constants.VendorId).ToString();
            objVendorId.Text = new string[] { vendorId };

            //Date Created 
            //Add Invoice Audit Date ie datecreated in intacct:              
            datecreated objdateDateCreated = new datecreated();
            year objDateCreatedYear = new year();
            month objDateCreatedMonth = new month();
            day objDateCreatedDay = new day();

            //take the date into stting, later assign the same to DateCreated
            string datePaid = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.AuditedDate.ToString()]));
            DateTime paidDate;
            paidDate = Convert.ToDateTime(datePaid);
            string paidYear = paidDate.Year.ToString();
            string paidMonth = paidDate.Month.ToString();
            string paidDay = paidDate.Day.ToString();

            objDateCreatedYear.Text = new string[] { paidYear };
            objDateCreatedMonth.Text = new string[] { paidMonth };
            objDateCreatedDay.Text = new string[] { paidDay };

            objdateDateCreated.day = objDateCreatedDay;
            objdateDateCreated.month = objDateCreatedMonth;
            objdateDateCreated.year = objDateCreatedYear;

            // Date Posted not posting as of now            
            //Date Due            
            datedue objDateDue = new datedue();

            year objYear = new year();
            month objMonth = new month();
            day objDay = new day();

            string dueDay = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.AuditedDate.ToString()]));
            //Adding 30 days to the Invoice date as requested
            DateTime date = Convert.ToDateTime(dueDay);
            string year = date.AddDays(Convert.ToInt16(SecurityManager.GetConfigValue(Constants.Add30DaysToInvoiceDate))).Year.ToString();
            string month = date.AddDays(Convert.ToInt16(SecurityManager.GetConfigValue(Constants.Add30DaysToInvoiceDate))).Month.ToString();
            string day = date.AddDays(Convert.ToInt16(SecurityManager.GetConfigValue(Constants.Add30DaysToInvoiceDate))).Day.ToString();

            objYear.Text = new string[] { year };
            objMonth.Text = new string[] { month };
            objDay.Text = new string[] { day };

            objDateDue.year = objYear;
            objDateDue.month = objMonth;
            objDateDue.day = objDay;

            //Adding currency from the property
            currency objCurrency = new currency();
            string currency = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.Currency.ToString()]));
            objCurrency.Text = new string[] { currency };

            //Adding exchangeratetype 
            exchratetype objechratetype = new exchratetype();
            string exchangeratetype = "Intacct Daily Rate";
            objechratetype.Text = new string[] { exchangeratetype };

            //Action N/A            
            //Bill Number
            billno objBillNo = new billno();
            string strbillNo = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.InvoiceNo.ToString()]));
            objBillNo.Text = new string[] { strbillNo };

            //Po Number
            ponumber objPoNo = new ponumber();
            string poNumber = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.AccountNO.ToString()]));
            objPoNo.Text = new string[] { poNumber };

            //Description
            description objDescription = new description();
            string description = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.HaulerInvoiceNo.ToString()]));
            objDescription.Text = new string[] { description };

            //ExternalId N/A            
            //BillItems
            amount objAmount = new amount();
            string amount = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.HaulerAmount.ToString()]));
            objAmount.Text = new string[] { amount };

            lineitem objLineItem = new lineitem();
            objLineItem.amount = objAmount;


            //Add HaulerName i.e memo in intacct as per BRD:
            memo objMemo = new memo();
            string memo = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.HaulerName.ToString()]));
            objMemo.Text = new string[] { memo };
            objLineItem.memo = objMemo;


            projectid objProjectId = new projectid();
            string projectId = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.PropertyID.ToString()]));
            objProjectId.Text = new string[] { projectId };
            objLineItem.projectid = objProjectId;

            glaccountno objGlAccountNo = new glaccountno();
            string glAccountNo = SecurityManager.GetConfigValue(Constants.GlBillNo).ToString();
            objGlAccountNo.Text = new string[] { glAccountNo };
            objLineItem.Item = objGlAccountNo;

            billitems objBillItems = new billitems();
            objBillItems.lineitem = new lineitem[] { objLineItem };

            //Finally assigning filled values to the entity
            objCreateBill.vendorid = objVendorId;
            objCreateBill.currency = objCurrency;
            objCreateBill.exchratetype = objechratetype;
            objCreateBill.datecreated = objdateDateCreated;
            objCreateBill.Items = new object[] { objDateDue };
            objCreateBill.billno = objBillNo;
            objCreateBill.description = objDescription;
            objCreateBill.billitems = new lineitem[] { objLineItem };

            objFunction.controlid = objBillNo.Text[Constants.Zero];
            objFunction.ItemElementName = ItemChoiceType2.create_bill;
            objFunction.Item = objCreateBill;

            return objFunction;
        }

        public static string EscapeSpecailCharactersInHTML(string str)
        {
            return StringManager.ReplaceSpecialCharactersWithHexa(str);
        }

        public static void UpdateCustomer(DataRow pkrmRow, IntacctLogin objLogin, List<function> objFunctionList, SelectionEntity objSelectionEntity, ref StringBuilder logFileContent)
        {
            string intacctId = pkrmRow[Constants.IntacctIdFromRefuse].ToString();

            update_customer objUpdateCustomer = new update_customer();
            function objFunction = new function();

            //Customer Name
            name objCustomerName = new name();
            string customerName = EscapeSpecailCharactersInHTML(pkrmRow[Constants.Name].ToString());
            objCustomerName.Text = new string[] { customerName };

            string customerIdPlusName = intacctId.ToString() + ':' + customerName.ToString();

            objFunction.controlid = customerIdPlusName.ToString();

            //All Primary Contact Details
            contactinfo objcontactinfo = new contactinfo();

            contact objCustomerPrimaryContact = new contact();
            mailaddress objMailAddress = new mailaddress();
            //Print As
            printas objPrintAs = new printas();
            string customerPrintAs = EscapeSpecailCharactersInHTML(pkrmRow[Constants.Name.ToString()].ToString());
            objPrintAs.Text = new string[] { customerPrintAs };
            //contact Name
            contactname objContactName = new contactname();
            string customerContactName = EscapeSpecailCharactersInHTML(pkrmRow[Constants.intacctColumnNames.UniqueContactName.ToString()].ToString());
            objContactName.Text = new string[] { customerContactName };

            //Address 1
            address1 objAddress1 = new address1();
            string customerAddress1 = EscapeSpecailCharactersInHTML(pkrmRow[Constants.intacctColumnNames.Street1.ToString()].ToString());
            objAddress1.Text = new string[] { customerAddress1 };

            //Address 2
            address2 objAddress2 = new address2();
            string customerAddress2 = EscapeSpecailCharactersInHTML(pkrmRow[Constants.intacctColumnNames.Street2.ToString()].ToString());
            objAddress2.Text = new string[] { customerAddress2 };

            //CITY
            city objCustomerCity = new city();
            string customerCity = pkrmRow[Constants.intacctColumnNames.City.ToString()].ToString();
            objCustomerCity.Text = new string[] { customerCity };


            //State
            state objState = new state();
            string customerState = pkrmRow[Constants.intacctColumnNames.STATE.ToString()].ToString();
            objState.Text = new string[] { customerState };

            //Zip
            zip objZip = new zip();
            string customerZip = pkrmRow[Constants.intacctColumnNames.PostalCode.ToString()].ToString();
            objZip.Text = new string[] { customerZip };

            //LastName
            lastname objLastName = new lastname();
            string customerLastName = EscapeSpecailCharactersInHTML(pkrmRow[Constants.intacctColumnNames.UniqueContactName.ToString()].ToString());
            objLastName.Text = new string[] { customerLastName };

            //Fax
            fax objFax = new fax();
            string customerFaxNumber = pkrmRow[Constants.intacctColumnNames.Fax.ToString()].ToString();
            objFax.Text = new string[] { customerFaxNumber };

            //Email
            email1 objEmail = new email1();
            string customerEmail = pkrmRow[Constants.intacctColumnNames.ContactEmail.ToString()].ToString();
            objEmail.Text = new string[] { customerEmail };

            //Phone1
            phone1 objPhone = new phone1();
            string customerPhone = pkrmRow[Constants.intacctColumnNames.ContactPhone.ToString()].ToString();
            objPhone.Text = new string[] { customerPhone };

            //Filling the primary contact entity for the customer here

            objCustomerPrimaryContact.contactname = objContactName;
            objCustomerPrimaryContact.printas = objPrintAs;
            objMailAddress.address1 = objAddress1;
            objMailAddress.address2 = objAddress2;
            objMailAddress.city = objCustomerCity;
            objMailAddress.state = objState;
            objMailAddress.zip = objZip;
            objCustomerPrimaryContact.mailaddress = objMailAddress;
            objCustomerPrimaryContact.lastname = objLastName;
            objCustomerPrimaryContact.fax = objFax;
            objCustomerPrimaryContact.email1 = objEmail;
            objCustomerPrimaryContact.phone1 = objPhone;
            objcontactinfo.Item = objCustomerPrimaryContact;
            //Customer Status
            status objStatus = new status();
            string customerStatus = pkrmRow[Constants.intacctColumnNames.GroupStatus.ToString()].ToString();
            objStatus.Text = new string[] { customerStatus };

            //Assigning Updated Values 
            objUpdateCustomer.customerid = intacctId;
            objUpdateCustomer.name = objCustomerName;
            objUpdateCustomer.contactinfo = objcontactinfo;
            objUpdateCustomer.status = objStatus;
            objFunction.ItemElementName = ItemChoiceType2.update_customer;
            objFunction.Item = objUpdateCustomer;

            if (objSelectionEntity.isBulk)
            {
                objFunctionList.Add(objFunction);
            }
            else
            {
                List<function> newSeqListUdpateCustomer = new List<function>();
                newSeqListUdpateCustomer.Add(objFunction);
                IntacctBusinessLogic.PostDataSeQuential(objSelectionEntity, newSeqListUdpateCustomer, ref logFileContent);
                newSeqListUdpateCustomer.Clear();
            }
        }

        public static void UpdateProperty(DataRow pkrmRow, IntacctLogin objLogin, List<function> objFunctionList, SelectionEntity objSelectionEntity, ref StringBuilder logFileContent)
        {
            update_project objUpdateProject = new update_project();

            //Property Id
            projectid objPropertyId = new projectid();
            string propertyId = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.PropertyId.ToString()]));
            objPropertyId.Text = new string[] { propertyId };

            function objFunction = new function();
            //Property Name
            name objPropertyName = new name();
            string propertyName = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.Name.ToString()]));
            objPropertyName.Text = new string[] { propertyName };

            string customerIdPlusName = propertyId.ToString() + ':' + propertyName.ToString();

            objFunction.controlid = customerIdPlusName.ToString();

            //Customer Id          
            customerid objCustomerID = new customerid();
            string customerID = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.GroupNbr.ToString()]));
            objCustomerID.Text = new string[] { customerID };

            //Property Type
            projecttype objprojectType = new projecttype();
            string projectType = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.FrachiseOrOpenMarket.ToString()]));
            objprojectType.Text = new string[] { projectType };

            //Property Status

            status objStatus = new status();
            string propertyStatus = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.Inactive.ToString()]));
            objStatus.Text = new string[] { propertyStatus };

            //Property Category
            projectcategory objProjectCategory = new projectcategory();
            string projectCat = Constants.Contract;
            objProjectCategory.Text = new string[] { projectCat };

            //property Currency
            currency objProjectCurrency = new currency();
            string projectCurrency = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.Currency.ToString()]));
            objProjectCurrency.Text = new string[] { projectCurrency };

            objUpdateProject.projectid = objPropertyId;
            objUpdateProject.name = objPropertyName;
            objUpdateProject.customerid = objCustomerID;
            objUpdateProject.projecttype = objprojectType;
            objUpdateProject.status = objStatus;
            objUpdateProject.projectcategory = objProjectCategory;
            objUpdateProject.key = propertyId.ToString();
            objUpdateProject.currency = objProjectCurrency;

            objFunction.ItemElementName = ItemChoiceType2.update_project;
            objFunction.Item = objUpdateProject;

            if (objSelectionEntity.isBulk)
            {
                objFunctionList.Add(objFunction);

            }
            else
            {
                List<function> newSeqListUdpateProperty = new List<function>();
                newSeqListUdpateProperty.Add(objFunction);
                IntacctBusinessLogic.PostDataSeQuential(objSelectionEntity, newSeqListUdpateProperty, ref logFileContent);
                newSeqListUdpateProperty.Clear();
            }

        }

        public static void UpdateInvoice(DataRow pkrmRow, IntacctLogin objLogin, List<function> objFunctionList, SelectionEntity objSelectionEntity, ref StringBuilder logFileContent)
        {
            //Update Invoice Record with the following .

            update_invoice objUpdateInvoice = new update_invoice();
            function objFunction = new function();

            key objKey = new key();
            string key = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.IntacctIdFromRefuse.ToString()]));
            objKey.Text = new string[] { key };

            //Adding CustomerId

            customerid objCustomerId = new customerid();
            string customerId = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.GroupNbr.ToString()]));
            objCustomerId.Text = new string[] { customerId };

            //datecreated is a mandatory field

            //Add Invoice Audit Date ie datecreated in intacct:              
            datecreated objdateDateCreated = new datecreated();
            year objDateCreatedYear = new year();
            month objDateCreatedMonth = new month();
            day objDateCreatedDay = new day();
            string datePaid = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.AuditedDate.ToString()]));
            DateTime paidDate = Convert.ToDateTime(datePaid);
            string paidYear = paidDate.Year.ToString();
            string paidMonth = paidDate.Month.ToString();
            string paidDay = paidDate.Day.ToString();
            objDateCreatedYear.Text = new string[] { paidYear };
            objDateCreatedMonth.Text = new string[] { paidMonth };
            objDateCreatedDay.Text = new string[] { paidDay };

            objdateDateCreated.day = objDateCreatedDay;
            objdateDateCreated.month = objDateCreatedMonth;
            objdateDateCreated.year = objDateCreatedYear;

            //Add AMOUNT to LINEITEM and that to InvoiceLineItems:
            amount objAmount = new amount();
            string amount = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.AmountDue.ToString()]));
            objAmount.Text = new string[] { amount };

            //Add HaulerAMOUNT to LINEITEM and that to InvoiceLineItems:    
            amount objAmountHauler = new amount();
            string Hauleramount = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.HaulerAmount.ToString()]));
            objAmountHauler.Text = new string[] { Hauleramount };

            //Add Invoice Number:
            invoiceno objInvoiceNo = new invoiceno();
            string invoiceNo = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.InvoiceNo.ToString()]));
            objInvoiceNo.Text = new string[] { invoiceNo };

            //Adding currency from the property
            currency objCurrency = new currency();
            string currency = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.Currency.ToString()]));
            objCurrency.Text = new string[] { currency };


            //Adding exchangeratetype 
            exchratetype objechratetype = new exchratetype();
            string exchangeratetype = "Intacct Daily Rate";
            objechratetype.Text = new string[] { exchangeratetype };


            updatelineitem objLineItem = new updatelineitem();
            objLineItem.amount = objAmount;
            objLineItem.line_num = Convert.ToString(Constants.One);

            updatelineitem objLineItemHAulerAmount = new updatelineitem();
            objLineItemHAulerAmount.amount = objAmountHauler;
            objLineItemHAulerAmount.line_num = Convert.ToString(Constants.Two);

            //Updating the project id 
            projectid objProjectId = new projectid();
            string projectId = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.PropertyID.ToString()]));
            objProjectId.Text = new string[] { projectId };
            objLineItem.projectid = objProjectId;
            objLineItemHAulerAmount.projectid = objProjectId;

            contactname objContactName = new contactname();
            string contactname = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.PropertyName.ToString()]));
            objContactName.Text = new string[] { contactname };
            billto objBillto = new billto();
            objBillto.Item = objContactName;

            //Add HaulerName i.e memo in intacct as per BRD:
            memo objMemo = new memo();
            string memo = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.HaulerName.ToString()]));
            objMemo.Text = new string[] { memo };
            objLineItem.memo = objMemo;
            objLineItemHAulerAmount.memo = objMemo;

            //Add due date
            datedue objDateDue = new datedue();

            year objYear = new year();
            month objMonth = new month();
            day objDay = new day();

            string dueDay = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.AuditedDate.ToString()]));
            DateTime date = Convert.ToDateTime(dueDay);
            //Adding 30 days to the Invoice date as requested
            string year = date.AddDays(Convert.ToInt16(SecurityManager.GetConfigValue(Constants.Add30DaysToInvoiceDate))).Year.ToString();
            string month = date.AddDays(Convert.ToInt16(SecurityManager.GetConfigValue(Constants.Add30DaysToInvoiceDate))).Month.ToString();
            string day = date.AddDays(Convert.ToInt16(SecurityManager.GetConfigValue(Constants.Add30DaysToInvoiceDate))).Day.ToString();

            objYear.Text = new string[] { year };
            objMonth.Text = new string[] { month };
            objDay.Text = new string[] { day };

            objDateDue.year = objYear;
            objDateDue.month = objMonth;
            objDateDue.day = objDay;

            //Adding AccountLabel to the Item.

            accountlabel objAccountLabel = new accountlabel();
            string accountLabel = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.Item.ToString()]));
            objAccountLabel.Text = new string[] { accountLabel };

            //Adding reference number i.e hauler account number here in RS
            ponumber objPoNo = new ponumber();
            string poNumber = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.AccountNO.ToString()]));
            objPoNo.Text = new string[] { poNumber };

            //Adding Hauler Invoice# in RS ie Description in the intacct

            description objDescription = new description();
            string description = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.HaulerInvoiceNo.ToString()].ToString()));
            objDescription.Text = new string[] { description };

            //Adding glAccountNumber to the intacct since it is mandatory (As of now we are using 8888)

            List<glaccountno> objGlAccountNo = new List<glaccountno>();
            string glAccountNo = SecurityManager.GetConfigValue(Constants.GlAccountNo).ToString();
            string glAccountNoHauler = SecurityManager.GetConfigValue(Constants.GlHaulerAcctNo).ToString();
            objGlAccountNo.Add(new glaccountno() { Text = new string[] { glAccountNo } });
            objGlAccountNo.Add(new glaccountno() { Text = new string[] { glAccountNoHauler } });
            // objGlAccountNo[1].Text = new string[] { glAccountNoHauler };
            objLineItem.Item = objGlAccountNo[0];
            objLineItemHAulerAmount.Item = objGlAccountNo[1];

            customfieldname objCustomFieldName = new customfieldname();
            string propertyName = Constants.PROPERTYNAME;
            objCustomFieldName.Text = new string[] { propertyName };

            objUpdateInvoice.customerid = objCustomerId;
            objUpdateInvoice.invoiceno = objInvoiceNo;
            objUpdateInvoice.datedue = objDateDue;
            objUpdateInvoice.description = objDescription;
            objUpdateInvoice.updateinvoiceitems = new updatelineitem[] { objLineItem, objLineItemHAulerAmount };
            objUpdateInvoice.ponumber = objPoNo;
            objUpdateInvoice.datecreated = objdateDateCreated;
            objUpdateInvoice.currency = objCurrency;
            objUpdateInvoice.exchratetype = objechratetype;
            //objUpdateInvoice.customfields = new customfield[] { objCustomField };
            objUpdateInvoice.key = key;

            objFunction.controlid = objInvoiceNo.Text[Constants.Zero];
            objFunction.ItemElementName = ItemChoiceType2.update_invoice;
            objFunction.Item = objUpdateInvoice;

            if (objSelectionEntity.isBulk)
            {
                objFunctionList.Add(objFunction);
            }
            else
            {
                List<function> newSeqListUdpateInvoice = new List<function>();
                newSeqListUdpateInvoice.Add(objFunction);
                IntacctBusinessLogic.PostDataSeQuential(objSelectionEntity, newSeqListUdpateInvoice, ref logFileContent);
                newSeqListUdpateInvoice.Clear();
            }

        }
        public static void UpdateInvoiceDirectpay(DataRow pkrmRow, IntacctLogin objLogin, List<function> objFunctionList, SelectionEntity objSelectionEntity, ref StringBuilder logFileContent)
        {
            //Update Invoice Record with the following .

            update_invoice objUpdateInvoice = new update_invoice();
            function objFunction = new function();

            key objKey = new key();
            string key = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.IntacctIdFromRefuse.ToString()]));
            objKey.Text = new string[] { key };

            //Adding CustomerId

            customerid objCustomerId = new customerid();
            string customerId = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.GroupNbr.ToString()]));
            objCustomerId.Text = new string[] { customerId };

            //datecreated is a mandatory field

            //Add Invoice Audit Date ie datecreated in intacct:              
            datecreated objdateDateCreated = new datecreated();
            year objDateCreatedYear = new year();
            month objDateCreatedMonth = new month();
            day objDateCreatedDay = new day();
            string datePaid = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.AuditedDate.ToString()]));
            DateTime paidDate = Convert.ToDateTime(datePaid);
            string paidYear = paidDate.Year.ToString();
            string paidMonth = paidDate.Month.ToString();
            string paidDay = paidDate.Day.ToString();
            objDateCreatedYear.Text = new string[] { paidYear };
            objDateCreatedMonth.Text = new string[] { paidMonth };
            objDateCreatedDay.Text = new string[] { paidDay };

            objdateDateCreated.day = objDateCreatedDay;
            objdateDateCreated.month = objDateCreatedMonth;
            objdateDateCreated.year = objDateCreatedYear;

            //Add AMOUNT to LINEITEM and that to InvoiceLineItems:
            amount objAmount = new amount();
            string amount = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.AmountDue.ToString()]));
            objAmount.Text = new string[] { amount };

            //Add Invoice Number:
            invoiceno objInvoiceNo = new invoiceno();
            string invoiceNo = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.InvoiceNo.ToString()]));
            objInvoiceNo.Text = new string[] { invoiceNo };

            //Adding currency from the property
            currency objCurrency = new currency();
            string currency = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.Currency.ToString()]));
            objCurrency.Text = new string[] { currency };

            //Adding exchangeratetype 
            exchratetype objechratetype = new exchratetype();
            string exchangeratetype = "Intacct Daily Rate";
            objechratetype.Text = new string[] { exchangeratetype };


            updatelineitem objLineItem = new updatelineitem();
            objLineItem.amount = objAmount;
            objLineItem.line_num = Convert.ToString(Constants.One);

            //Updating the project id 
            projectid objProjectId = new projectid();
            string projectId = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.PropertyID.ToString()]));
            objProjectId.Text = new string[] { projectId };
            objLineItem.projectid = objProjectId;
            //objLineItemHAulerAmount.projectid = objProjectId;

            contactname objContactName = new contactname();
            string contactname = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.PropertyName.ToString()]));
            objContactName.Text = new string[] { contactname };
            billto objBillto = new billto();
            objBillto.Item = objContactName;

            //Add HaulerName i.e memo in intacct as per BRD:
            memo objMemo = new memo();
            string memo = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.HaulerName.ToString()]));
            objMemo.Text = new string[] { memo };
            objLineItem.memo = objMemo;
            //objLineItemHAulerAmount.memo = objMemo;

            //Add due date
            datedue objDateDue = new datedue();

            year objYear = new year();
            month objMonth = new month();
            day objDay = new day();

            string dueDay = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.AuditedDate.ToString()]));
            DateTime date = Convert.ToDateTime(dueDay);
            //Adding 30 days to the Invoice date as requested
            string year = date.AddDays(Convert.ToInt16(SecurityManager.GetConfigValue(Constants.Add30DaysToInvoiceDate))).Year.ToString();
            string month = date.AddDays(Convert.ToInt16(SecurityManager.GetConfigValue(Constants.Add30DaysToInvoiceDate))).Month.ToString();
            string day = date.AddDays(Convert.ToInt16(SecurityManager.GetConfigValue(Constants.Add30DaysToInvoiceDate))).Day.ToString();

            objYear.Text = new string[] { year };
            objMonth.Text = new string[] { month };
            objDay.Text = new string[] { day };

            objDateDue.year = objYear;
            objDateDue.month = objMonth;
            objDateDue.day = objDay;

            //Adding CustomerName or CustomerNo to Customer..???
            //Adding AccountLabel to the Item.
            accountlabel objAccountLabel = new accountlabel();
            string accountLabel = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.Item.ToString()]));
            objAccountLabel.Text = new string[] { accountLabel };

            //Adding reference number i.e hauler account number here in RS
            ponumber objPoNo = new ponumber();
            string poNumber = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.AccountNO.ToString()]));
            objPoNo.Text = new string[] { poNumber };

            //Adding Hauler Invoice# in RS ie Description in the intacct

            description objDescription = new description();
            string description = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.HaulerInvoiceNo.ToString()].ToString()));
            objDescription.Text = new string[] { description };

            //Adding glAccountNumber to the intacct since it is mandatory (As of now we are using 8888)
            List<glaccountno> objGlAccountNo = new List<glaccountno>();
            string glAccountNo = SecurityManager.GetConfigValue(Constants.GlAccountNo).ToString();
            //string glAccountNoHauler = SecurityManager.GetConfigValue(Constants.GlHaulerAcctNo).ToString();
            objGlAccountNo.Add(new glaccountno() { Text = new string[] { glAccountNo } });
            //objGlAccountNo.Add(new glaccountno() { Text = new string[] { glAccountNoHauler } });
            // objGlAccountNo[1].Text = new string[] { glAccountNoHauler };
            objLineItem.Item = objGlAccountNo[0];
            //objLineItem.Item = objGlAccountNo[1];

            customfieldname objCustomFieldName = new customfieldname();
            string propertyName = Constants.PROPERTYNAME;
            objCustomFieldName.Text = new string[] { propertyName };

            objUpdateInvoice.customerid = objCustomerId;
            objUpdateInvoice.invoiceno = objInvoiceNo;
            objUpdateInvoice.datedue = objDateDue;
            objUpdateInvoice.description = objDescription;
            objUpdateInvoice.updateinvoiceitems = new updatelineitem[] { objLineItem };
            objUpdateInvoice.ponumber = objPoNo;
            objUpdateInvoice.datecreated = objdateDateCreated;
            //objUpdateInvoice.customfields = new customfield[] { objCustomField };
            objUpdateInvoice.key = key;
            objUpdateInvoice.currency = objCurrency;
            objUpdateInvoice.exchratetype = objechratetype;

            objFunction.controlid = objInvoiceNo.Text[Constants.Zero];
            objFunction.ItemElementName = ItemChoiceType2.update_invoice;
            objFunction.Item = objUpdateInvoice;

            if (objSelectionEntity.isBulk)
            {
                objFunctionList.Add(objFunction);
            }
            else
            {
                List<function> newSeqListUdpateInvoice = new List<function>();
                newSeqListUdpateInvoice.Add(objFunction);
                IntacctBusinessLogic.PostDataSeQuential(objSelectionEntity, newSeqListUdpateInvoice, ref logFileContent);
                newSeqListUdpateInvoice.Clear();
            }
        }

        public static void UpdateInvoiceBill(DataRow pkrmRow, IntacctLogin objLogin, List<function> objFunctionList, SelectionEntity objSelectionEntity, ref StringBuilder logFileContent)
        {
            update_bill objUpdateBill = new update_bill();
            function objFunction = new function();

            //Add vendorid from the Configuration file since they are ok with that

            string key = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.IntacctBillId.ToString()]));

            vendorid objVendorId = new vendorid();
            string vendorId = SecurityManager.GetConfigValue(Constants.VendorId).ToString();
            objVendorId.Text = new string[] { vendorId };

            //Date Created 
            //Add Invoice Audit Date ie datecreated in intacct:              
            datecreated objdateDateCreated = new datecreated();
            year objDateCreatedYear = new year();
            month objDateCreatedMonth = new month();
            day objDateCreatedDay = new day();

            //Adding currency from the property
            currency objCurrency = new currency();
            string currency = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.Currency.ToString()]));
            objCurrency.Text = new string[] { currency };


            //Adding exchangeratetype 
            exchratetype objechratetype = new exchratetype();
            string exchangeratetype = "Intacct Daily Rate";
            objechratetype.Text = new string[] { exchangeratetype };

            //take the date into stting, later assign the same to DateCreated
            string datePaid = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.AuditedDate.ToString()]));
            DateTime paidDate;
            paidDate = Convert.ToDateTime(datePaid);
            string paidYear = paidDate.Year.ToString();
            string paidMonth = paidDate.Month.ToString();
            string paidDay = paidDate.Day.ToString();

            objDateCreatedYear.Text = new string[] { paidYear };
            objDateCreatedMonth.Text = new string[] { paidMonth };
            objDateCreatedDay.Text = new string[] { paidDay };

            objdateDateCreated.day = objDateCreatedDay;
            objdateDateCreated.month = objDateCreatedMonth;
            objdateDateCreated.year = objDateCreatedYear;

            // Date Posted not posting as of now            
            //Date Due            
            datedue objDateDue = new datedue();

            year objYear = new year();
            month objMonth = new month();
            day objDay = new day();

            string dueDay = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.AuditedDate.ToString()]));
            //Adding 30 days to the Invoice date as requested
            DateTime date = Convert.ToDateTime(dueDay);
            string year = date.AddDays(Convert.ToInt16(SecurityManager.GetConfigValue(Constants.Add30DaysToInvoiceDate))).Year.ToString();
            string month = date.AddDays(Convert.ToInt16(SecurityManager.GetConfigValue(Constants.Add30DaysToInvoiceDate))).Month.ToString();
            string day = date.AddDays(Convert.ToInt16(SecurityManager.GetConfigValue(Constants.Add30DaysToInvoiceDate))).Day.ToString();

            objYear.Text = new string[] { year };
            objMonth.Text = new string[] { month };
            objDay.Text = new string[] { day };

            objDateDue.year = objYear;
            objDateDue.month = objMonth;
            objDateDue.day = objDay;

            //Action N/A            
            //Bill Number
            billno objBillNo = new billno();
            string strbillNo = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.InvoiceNo.ToString()]));
            objBillNo.Text = new string[] { strbillNo };

            //Po Number
            ponumber objPoNo = new ponumber();
            string poNumber = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.AccountNO.ToString()]));
            objPoNo.Text = new string[] { poNumber };

            //Description
            description objDescription = new description();
            string description = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.HaulerInvoiceNo.ToString()]));
            objDescription.Text = new string[] { description };

            //ExternalId N/A            
            //BillItems
            amount objAmount = new amount();
            string amount = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.HaulerAmount.ToString()]));
            objAmount.Text = new string[] { amount };

            updatelineitem objUpdateLineItem = new updatelineitem();
            objUpdateLineItem.amount = objAmount;


            //Add HaulerName i.e memo in intacct as per BRD:
            memo objMemo = new memo();
            string memo = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.HaulerName.ToString()]));
            objMemo.Text = new string[] { memo };
            objUpdateLineItem.memo = objMemo;


            projectid objProjectId = new projectid();
            string projectId = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.PropertyID.ToString()]));
            objProjectId.Text = new string[] { projectId };
            objUpdateLineItem.projectid = objProjectId;

            glaccountno objGlAccountNo = new glaccountno();
            string glAccountNo = SecurityManager.GetConfigValue(Constants.GlBillNo).ToString();
            objGlAccountNo.Text = new string[] { glAccountNo };
            objUpdateLineItem.Item = objGlAccountNo;

            objUpdateLineItem.line_num = Convert.ToString(Constants.One);

            //Finally assigning filled values to the entity
            objUpdateBill.vendorid = objVendorId;
            objUpdateBill.datecreated = objdateDateCreated;
            objUpdateBill.datedue = objDateDue;
            objUpdateBill.billno = objBillNo;
            objUpdateBill.description = objDescription;
            objUpdateBill.updatebillitems = new object[] { objUpdateLineItem };
            objUpdateBill.key = key;
            objUpdateBill.currency = objCurrency;
            objUpdateBill.exchratetype = objechratetype;
            objFunction.controlid = objBillNo.Text[Constants.Zero];
            objFunction.ItemElementName = ItemChoiceType2.update_bill;
            objFunction.Item = objUpdateBill;

            List<function> newSeqListUdpateInvoice = new List<function>();
            newSeqListUdpateInvoice.Add(objFunction);
            IntacctBusinessLogic.PostDataSeQuential(objSelectionEntity, newSeqListUdpateInvoice, ref logFileContent);
            newSeqListUdpateInvoice.Clear();
        }
        public static void UpdateBillRsValue(DataRow pkrmRow, IntacctLogin objLogin, List<function> objFunctionList, SelectionEntity objSelectionEntity, ref StringBuilder logFileContent)
        {
            update_bill objUpdateBill = new update_bill();
            function objFunction = new function();

            //Add vendorid from the Configuration file since they are ok with that
            string key = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.IntacctBillId.ToString()]));

            vendorid objVendorId = new vendorid();
            string vendorId = SecurityManager.GetConfigValue(Constants.VendorId).ToString();
            objVendorId.Text = new string[] { vendorId };

            //Date Created 
            //Add Invoice Audit Date ie datecreated in intacct:              
            datecreated objdateDateCreated = new datecreated();
            year objDateCreatedYear = new year();
            month objDateCreatedMonth = new month();
            day objDateCreatedDay = new day();

            //Adding currency from the property
            currency objCurrency = new currency();
            string currency = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.Currency.ToString()]));
            objCurrency.Text = new string[] { currency };

            //Adding exchangeratetype 
            exchratetype objechratetype = new exchratetype();
            string exchangeratetype = "Intacct Daily Rate";
            objechratetype.Text = new string[] { exchangeratetype };

            //take the date into stting, later assign the same to DateCreated
            string datePaid = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.AuditedDate.ToString()]));
            DateTime paidDate;
            paidDate = Convert.ToDateTime(datePaid);
            string paidYear = paidDate.Year.ToString();
            string paidMonth = paidDate.Month.ToString();
            string paidDay = paidDate.Day.ToString();

            objDateCreatedYear.Text = new string[] { paidYear };
            objDateCreatedMonth.Text = new string[] { paidMonth };
            objDateCreatedDay.Text = new string[] { paidDay };

            objdateDateCreated.day = objDateCreatedDay;
            objdateDateCreated.month = objDateCreatedMonth;
            objdateDateCreated.year = objDateCreatedYear;

            // Date Posted not posting as of now            
            //Date Due            
            datedue objDateDue = new datedue();

            year objYear = new year();
            month objMonth = new month();
            day objDay = new day();

            string dueDay = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.AuditedDate.ToString()]));
            //Adding 30 days to the Invoice date as requested
            DateTime date = Convert.ToDateTime(dueDay);
            string year = date.AddDays(Convert.ToInt16(SecurityManager.GetConfigValue(Constants.Add30DaysToInvoiceDate))).Year.ToString();
            string month = date.AddDays(Convert.ToInt16(SecurityManager.GetConfigValue(Constants.Add30DaysToInvoiceDate))).Month.ToString();
            string day = date.AddDays(Convert.ToInt16(SecurityManager.GetConfigValue(Constants.Add30DaysToInvoiceDate))).Day.ToString();

            objYear.Text = new string[] { year };
            objMonth.Text = new string[] { month };
            objDay.Text = new string[] { day };

            objDateDue.year = objYear;
            objDateDue.month = objMonth;
            objDateDue.day = objDay;

            //Action N/A            
            //Bill Number
            billno objBillNo = new billno();
            string strbillNo = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.InvoiceNo.ToString()]));
            objBillNo.Text = new string[] { strbillNo };

            //Po Number
            ponumber objPoNo = new ponumber();
            string poNumber = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.AccountNO.ToString()]));
            objPoNo.Text = new string[] { poNumber };

            //Description
            description objDescription = new description();
            string description = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.HaulerInvoiceNo.ToString()]));
            objDescription.Text = new string[] { description };

            //ExternalId N/A            
            //BillItems
            amount objAmount = new amount();
            string amount = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.AmountDue.ToString()]));
            objAmount.Text = new string[] { amount };

            updatelineitem objUpdateLineItem = new updatelineitem();
            objUpdateLineItem.amount = objAmount;

            //Add HaulerName i.e memo in intacct as per BRD:
            memo objMemo = new memo();
            string memo = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.HaulerName.ToString()]));
            objMemo.Text = new string[] { memo };
            objUpdateLineItem.memo = objMemo;

            projectid objProjectId = new projectid();
            string projectId = EscapeSpecailCharactersInHTML(Convert.ToString(pkrmRow[EnumManager.Columns.PropertyID.ToString()]));
            objProjectId.Text = new string[] { projectId };
            objUpdateLineItem.projectid = objProjectId;

            glaccountno objGlAccountNo = new glaccountno();
            string glAccountNo = SecurityManager.GetConfigValue(Constants.GlAccountNo).ToString();
            objGlAccountNo.Text = new string[] { glAccountNo };
            objUpdateLineItem.Item = objGlAccountNo;
            objUpdateLineItem.line_num = Convert.ToString(Constants.Zero);                     

            //Finally assigning filled values to the entity
            objUpdateBill.vendorid = objVendorId;
            objUpdateBill.datecreated = objdateDateCreated;
            //objUpdateBill.Items = new object[] { objDateDue };
            objUpdateBill.datedue = objDateDue;
            objUpdateBill.billno = objBillNo;
            objUpdateBill.description = objDescription;
            objUpdateBill.updatebillitems = new object[] { objUpdateLineItem };
            objUpdateBill.key = key;
            objUpdateBill.currency = objCurrency;
            objUpdateBill.exchratetype = objechratetype;
            objFunction.controlid = objBillNo.Text[Constants.Zero];
            objFunction.ItemElementName = ItemChoiceType2.update_bill;
            objFunction.Item = objUpdateBill;

            List<function> newSeqListUdpateInvoice = new List<function>();
            newSeqListUdpateInvoice.Add(objFunction);
            IntacctBusinessLogic.PostDataSeQuential(objSelectionEntity, newSeqListUdpateInvoice, ref logFileContent);
            newSeqListUdpateInvoice.Clear();
        }
    }
}

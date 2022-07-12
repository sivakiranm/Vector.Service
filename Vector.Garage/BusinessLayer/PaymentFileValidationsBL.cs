using System;
using System.Text.RegularExpressions;
using Vector.Common.BusinessLayer;

namespace Vector.Garage.BusinessLayer
{
    public class PaymentFileValidationsBL :DisposeLogic
    {

        #region Check Detail Validations

        public string ValidatingCheckDetails(string controlValue, string action, string currencyCode = "")
        {
            string result = string.Empty;
            switch (action.ToUpper())
            {
                case "DELIVERY METHOD":
                    {
                        //Required Field Validation
                        if (!string.IsNullOrEmpty(controlValue)) { }
                        else { result = @"Delivery method is required\n"; break; }

                        //LengthValidation & RegularExpression 
                        Regex objPattern = new Regex("^000$|^001$");
                        if (objPattern.IsMatch(controlValue)) { }
                        else { result = @"Delivery method allows 3 digits i.e 000 or 001 only\n"; break; }

                        break;
                    }
                case "CHECK PREFIX":
                    {
                        //Required Field Validation
                        if (!string.IsNullOrEmpty(controlValue)) { }
                        else { result = @"Check Prefix is required\n"; break; }

                        //LengthValidation & RegularExpression 
                        Regex objPattern = new Regex("^\\d{3}$");
                        if (objPattern.IsMatch(controlValue)) { }
                        else { result = @"Check Prefix allows 3 digits only\n"; break; }

                        break;
                    }
                case "CHECK NUMBER":
                    {
                        //Required Field Validation
                        if (!string.IsNullOrEmpty(controlValue)) { }
                        else { result = @"Check Number is required\n"; break; }

                        if (StringManager.IsEqual(currencyCode, "CAD"))
                        {
                            //LengthValidation & RegularExpression 
                            Regex objPattern = new Regex("^\\d{8}$");
                            if (objPattern.IsMatch(controlValue)) { }
                            else { result = @"Check Number allows 8 digits only for CAD Transactions\n"; break; }
                        }
                        else
                        {
                            //LengthValidation & RegularExpression 
                            Regex objPattern = new Regex("^\\d{10}$");
                            if (objPattern.IsMatch(controlValue)) { }
                            else { result = @"Check Number allows 10 digits only for USD Transactions\n"; break; }
                        }



                        break;
                    }
                case "CHECK DOC NUMBER":
                    {
                        //Required Field Validation
                        if (!string.IsNullOrEmpty(controlValue)) { }
                        else { result = @"Check Doc # is required\n"; break; }

                        //LengthValidation & RegularExpression 
                        Regex objPattern = new Regex("^[a-zA-Z0-9_@&/-]{1,18}$");
                        if (objPattern.IsMatch(controlValue)) { }
                        else { result = @"Check Doc # allows 1-18 length of alphanumerics with special characters (_@&/-) only\n"; break; }

                        break;
                    }

                case "LAST SEQUENCE":
                    {
                        //Required Field Validation
                        if (!string.IsNullOrEmpty(controlValue)) { }
                        else { result = @"Last Sequence is required\n"; break; }

                        //LengthValidation & RegularExpression 
                        Regex objPattern = new Regex("^\\d{1,7}$");
                        if (objPattern.IsMatch(controlValue)) { }
                        else { result = @"Last Sequence allows 1-7 length of digits only\n"; break; }

                        //Validating the value
                        //The value can't be greater than 9000000
                        if (Convert.ToInt32(controlValue) >= 9000000)
                        { result = @"Last Sequence value can not be greater than or Equal to 9000000\n"; break; }
                        else { }

                        break;
                    }
                case "NEXT SEQUENCE":
                    {
                        //Required Field Validation
                        if (!string.IsNullOrEmpty(controlValue)) { }
                        else { result = @"Next Sequence is required\n"; break; }

                        //LengthValidation & RegularExpression 
                        Regex objPattern = new Regex("^\\d{1,7}$");
                        if (objPattern.IsMatch(controlValue)) { }
                        else { result = @"Next Sequence allows 1-7 length of digits only\n"; break; }

                        //Validating the value
                        //The value can't be greater than 9000000
                        if (Convert.ToInt32(controlValue) >= 9000000)
                        { result = @"Next Sequence value can not be greater than or Equal to 9000000\n"; break; }
                        else { }

                        break;
                    }
            }
            return result;
        }

        #endregion

        #region Originator Deatils Validations

        public string ValidatingOriginatorDetails(string controlValue, string action, string currencyCode = "")
        {
            string result = string.Empty;
            switch (action.ToUpper())
            {


                case "CLIENT NAME":
                    {
                        //Required Field Validation
                        if (string.IsNullOrEmpty(controlValue)) { result = @"Originator Name is required\n"; break; }

                        //LengthValidation & RegularExpression 
                        Regex objPattern = new Regex(@"^[a-zA-Z0-9\s_@&-.]{1,50}$");
                        if (!objPattern.IsMatch(controlValue))
                        { result = @"Originator Name allows 1-50 length of  alphanumerics with special characters (_@&-.) only\n"; break; }

                        break;
                    }
                case "ABA ROUTING NUMBER":
                    {
                        //Required Field Validation
                        if (!string.IsNullOrEmpty(controlValue)) { }
                        else { result = @"ABA Routing # is required\n"; break; }

                        if (StringManager.IsEqual(currencyCode, "CAD"))
                        {
                            //LengthValidation & RegularExpression 
                            Regex objPattern = new Regex("^\\d{8}$");
                            if (objPattern.IsMatch(controlValue)) { }
                            else { result = @"ABA Routing #  allows 8 digits only for CAD transactions\n"; break; }
                        }
                        else
                        {
                            //LengthValidation & RegularExpression 
                            Regex objPattern = new Regex("^\\d{9}$");
                            if (objPattern.IsMatch(controlValue)) { }
                            else { result = @"ABA Routing #  allows 9 digits only For USD transactions\n"; break; }
                        }
                        break;
                    }
                case "ZBA DISBURSEMENT":
                    {
                        //Required Field Validation
                        if (string.IsNullOrEmpty(controlValue))
                        { result = @"Sub Account A/c # is required\n"; break; }

                        //LengthValidation & RegularExpression 
                        Regex objPattern = new Regex("^[a-zA-Z0-9_@&/-]{1,34}$");
                        if (!objPattern.IsMatch(controlValue))
                        { result = @"Sub Account A/c # allows 1-34 length of alphanumerics with special characters (_@&/-) only\n"; break; }

                        break;
                    }
                case "BANK NAME":
                    {
                        //Required Field Validation
                        if (string.IsNullOrEmpty(controlValue))
                        { result = @"Bank Name is required\n"; break; }

                        //LengthValidation & RegularExpression 
                        Regex objPattern = new Regex(@"[a-zA-Z0-9\s._()$!:;.,&/\\*@#+-]{1,60}$");
                        if (!objPattern.IsMatch(controlValue))
                        { result = @"Bank Name allows 1-60 length of  alphanumerics with special characters (_()$!:;.,&/\*@#+-) only\n"; break; }

                        break;
                    }
                case "BANK ID TYPE":
                    {
                        //Required Field Validation
                        if (string.IsNullOrEmpty(controlValue))
                        { result = @"Bank Id Type is required\n"; break; }

                        //LengthValidation & RegularExpression "^[a-zA-Z0-9_@&/-]{3}$"

                        Regex objPattern = new Regex("^ABA$");
                        if (!objPattern.IsMatch(controlValue))
                        { result = @"Bank Id Type allows 3 alphabets i.e ABA only\n"; break; }

                        break;
                    }

                case "REF ID":
                    {
                        //Required Field Validation
                        if (string.IsNullOrEmpty(controlValue))
                        { result = @"Reference Id Type is required\n"; break; }

                        //LengthValidation & RegularExpression "^[a-zA-Z0-9_@&/-]{3}$"

                        Regex objPattern = new Regex("^[a-zA-Z0-9_@&-.#,:;$()]{10}$");
                        if (!objPattern.IsMatch(controlValue))
                        { result = @"Reference Id allows length of 10 alphanumerics with special characters (_@&-.#,:;$()) only\n"; break; }

                        break;
                    }

                case "ACCOUNT CURRENCY":
                    {
                        //Required Field Validation
                        if (string.IsNullOrEmpty(controlValue))
                        { result = @"Account Currency is required\n"; break; }

                        //LengthValidation & RegularExpression Regex("^[a-zA-Z0-9_@&/-]{3}$");

                        Regex objPattern = new Regex("^USD$|^CAD$");
                        if (!objPattern.IsMatch(controlValue))
                        { result = @"Account Currency allows 3 alphabets i.e USD or CAD only\n"; break; }

                        break;
                    }

                case "ACCOUNT TYPE":
                    {
                        //Required Field Validation
                        if (string.IsNullOrEmpty(controlValue))
                        { result = @"Account Type is required\n"; break; }

                        //LengthValidation & RegularExpression  Regex("^[a-zA-Z]{1}$");

                        Regex objPattern = new Regex("^D$|^G$");

                        if (!objPattern.IsMatch(controlValue))
                        { result = @"Account Type allows only 1 alphabet i.e D or G \n"; break; }

                        break;
                    }

                case "ADDRESS1":
                    {
                        //Required Field Validation
                        if (string.IsNullOrEmpty(controlValue)) { result = @"Originator Address 1 is required\n"; break; }

                        //LengthValidation & RegularExpression 
                        Regex objPattern = new Regex(@"^[\w\. _()$!:;.\\,&*@#/-]{1,55}$");//^[a-zA-Z0-9\s_@&/,:()#.*!;-]{1,55}$
                        if (!objPattern.IsMatch(controlValue)) { result = @"Originator Address 1 allows 1-55 length of  alphanumerics with special characters(_@()&/-#,:.*!;) only\n"; break; }

                        break;
                    }

                case "ADDRESS2":
                    {
                        if (!string.IsNullOrEmpty(controlValue))
                        {
                            //LengthValidation & RegularExpression Regex("^[a-zA-Z0-9_@&/-]{1,55}$")
                            Regex objPattern = new Regex(@"^[\w\. _()$!:;.\\,&*@#/-]{1,55}$");
                            if (!objPattern.IsMatch(controlValue))
                            { result = @"Originator Address 2 allows 1-55 length of alphanumerics with special characters(_@()&/-#,:.*!;) only\n"; break; }
                        }
                        break;

                    }


                case "CITY":
                    {
                        //Required Field Validation
                        if (!string.IsNullOrEmpty(controlValue)) { }
                        else { result = @"Originator City is required\n"; break; }

                        //LengthValidation & RegularExpression 
                        Regex objPattern = new Regex(@"^[a-zA-Z\s]{1,30}$");//^[a-zA-Z\s._()$!:;.,&*@#-]{1,30}$
                        if (objPattern.IsMatch(controlValue)) { }
                        else { result = @"Originator City allows 1-30 length of alphabets with space only\n"; break; }

                        break;
                    }

                case "STATE":
                    {
                        //Required Field Validation
                        if (string.IsNullOrEmpty(controlValue))
                        {
                            result = @"Originator State is required\n"; break;
                        }

                        //LengthValidation & RegularExpression 
                        Regex objPattern = new Regex("^[a-zA-Z]{2,3}$");
                        if (!objPattern.IsMatch(controlValue)) { result = @"Originator State allows 2-3 alphabets only\n"; break; }

                        break;
                    }


                case "ZIP":
                    {
                        //Required Field Validation
                        if (string.IsNullOrEmpty(controlValue)) { result = @"Originator Zip is required\n"; break; }

                        //LengthValidation & RegularExpression 
                        Regex objPattern = new Regex(@"^\d{5}$|^\d{9}$|^[a-zA-Z\s0-9]{7}$");
                        if (!objPattern.IsMatch(controlValue.Trim()))
                        { result = @"Originator Zip allows 5 - 9 alphanumerics with space\n"; break; }

                        break;
                    }

                case "COUNTRY":
                    {
                        //Required Field Validation
                        if (string.IsNullOrEmpty(controlValue))
                        { result = @"Originator Country is required\n"; break; }

                        //LengthValidation & RegularExpression  Regex("^[a-zA-Z]{2}$");

                        Regex objPattern = new Regex("^US$|^CA$");

                        if (!objPattern.IsMatch(controlValue))
                        { result = @"Originator Country allows only 2 alphabets i.e US or CA \n"; break; }

                        break;
                    }

                case "CUSTID":
                    {
                        //Required Field Validation
                        if (string.IsNullOrEmpty(controlValue))
                        { result = @"Customer Id is required\n"; break; }
                        Regex objPattern = new Regex("^[a-zA-Z,0-9]{1,10}$");

                        if (!objPattern.IsMatch(controlValue))
                        { result = @"Customer Id allows length of 10 alphanumeric value \n"; break; }

                        break;
                    }
                case "DIVCODE":
                    {
                        //Required Field Validation
                        if (string.IsNullOrEmpty(controlValue))
                        { result = @"Division Code is required\n"; break; }

                        Regex objPattern = new Regex("^\\d{5}$");
                        if (objPattern.IsMatch(controlValue)) { }
                        else { result = @"Division Code allows 5 digits only\n"; break; }

                        break;
                    }

                case "MAN":
                    {
                        //Required Field Validation
                        if (string.IsNullOrEmpty(controlValue))
                        { result = @"MAN is required\n"; break; }

                        Regex objPattern = new Regex("^[a-zA-Z,0-9]{16}$");
                        if (objPattern.IsMatch(controlValue)) { }
                        else { result = @"MAN allows length of 16 alphanumeric value \n"; break; }

                        break;
                    }

                case "BATCHID":
                    {
                        //Required Field Validation
                        if (string.IsNullOrEmpty(controlValue))
                        { result = @"Batch Id is required\n"; break; }

                        Regex objPattern = new Regex("^\\d{1,10}$");
                        if (objPattern.IsMatch(controlValue)) { }
                        else { result = @"Batch Id allows max of 10 digits value only\n"; break; }

                        break;
                    }
                case "CONTACTNAME":
                    {
                        //Required Field Validation
                        if (string.IsNullOrEmpty(controlValue))
                        { result = @"Contact Person Name is required\n"; break; }

                        Regex objPattern = new Regex(@"^[a-zA-Z\s]{1,40}$");
                        if (!objPattern.IsMatch(controlValue.Trim()))
                        { result = @"Contact Person Name allows max 40 of characters with space\n"; break; }


                        break;
                    }
                case "CONTACTEMAIL":
                    {
                        //Required Field Validation
                        if (string.IsNullOrEmpty(controlValue))
                        { result = @"Contact Person E-mail is required\n"; break; }

                        //Regex objPattern = new Regex("/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/");
                        //if (objPattern.IsMatch(controlValue)) { }
                        //else { result = @"Contact Person E-mail should be Valid \n"; break; }

                        break;
                    }
                case "CONTACTPHONE":
                    {
                        //Required Field Validation
                        if (string.IsNullOrEmpty(controlValue))
                        { result = @"Contact Person Phone Number is required\n"; break; }
                        Regex objPattern = new Regex("^\\d{10}$");
                        if (objPattern.IsMatch(controlValue)) { }
                        else { result = @"Contact Person Phone Number should be Valid \n"; break; }

                        break;
                    }

            }
            return result;
        }

        #endregion


        #region Receiver Deatils Validations

        public string ValidatingReceiverDetails(string controlValue, string action, string currencyCode = "")
        {
            string result = string.Empty;
            switch (action.ToUpper())
            {
                case "VENDOR NAME":
                    {
                        //Required Field Validation
                        if (string.IsNullOrEmpty(controlValue))
                        { result = @"Vendor Name is required\n"; break; }

                        //LengthValidation & RegularExpression 
                        Regex objPattern = new Regex(@"^[a-zA-Z0-9\s._()$!:;.,&/\\*@#-]{1,60}$");
                        if (!objPattern.IsMatch(controlValue))
                        {
                            result = @"Vendor Name allows 1-60 length of  alphanumerics with special characters (_()$!:;.,/\&*@#-]*) only\n"; break;
                        }

                        break;
                    }
                case "VENDOR PAYEE NAME SUCC":
                    {
                        //Required Field Validation
                        if (string.IsNullOrEmpty(controlValue))
                        { result = @"Vendor Payee Name is required\n"; break; }

                        //LengthValidation & RegularExpression 
                        Regex objPattern = new Regex(@"^[a-zA-Z0-9\s._()$!:;.,&/@#-]{1,60}$");
                        if (!objPattern.IsMatch(controlValue))
                        {
                            result = @"Vendor Payee Name (SUCC) allows 1-60 length of  alphanumerics with special characters (_()$!:;.,/&@#-) only\n"; break;
                        }
                        break;
                    }
                case "VENDOR PAYEE NAME":
                    {
                        //Required Field Validation
                        if (string.IsNullOrEmpty(controlValue))
                        { result = @"Vendor Payee Name 1 is required\n"; break; }

                        //LengthValidation & RegularExpression 
                        Regex objPattern = new Regex(@"^[a-zA-Z0-9\s._()$!:;.,&/@#-]{1,35}$");
                        if (!objPattern.IsMatch(controlValue))
                        {
                            result = @"Vendor Payee Name 1 allows 1-35 length of  alphanumerics with special characters (_()$!:;.,/&@#-) only\n"; break;
                        }

                        break;
                    }
                case "VENDOR PAYEE NAME1":
                    {
                        //Required Field Validation
                        //if (string.IsNullOrEmpty(controlValue))
                        //{ result = @"Vendor Payee Name 2 is required\n"; break; }

                        //LengthValidation & RegularExpression 
                        if (!string.IsNullOrEmpty(controlValue))
                        {
                            Regex objPattern = new Regex(@"^[a-zA-Z0-9\s._()$!:;.,&/@#-]{1,35}$");
                            if (!objPattern.IsMatch(controlValue))
                            {
                                result = @"Vendor Payee Name 2 allows 1-35 length of  alphanumerics with special characters (_()$!:;.,/&@#-) only\n"; break;
                            }
                        }

                        break;
                    }

                case "VENDOR ROUTING NUMBER":
                    {
                        //Required Field Validation
                        if (string.IsNullOrEmpty(controlValue))
                        {
                            result = @"Vendor ABA Routing # is required\n"; break;
                        }

                        //LengthValidation & RegularExpression 
                        Regex objPattern = new Regex("^\\d{9}$");
                        if (!objPattern.IsMatch(controlValue))
                        { result = @"Vendor ABA Routing #  allows 9 digits only\n"; break; }

                        break;
                    }
                case "VENDOR RMT ACCT NUMBER":
                    {
                        //Required Field Validation
                        if (string.IsNullOrEmpty(controlValue))
                        {
                            result = @"Vendor Rmt Acct # is required\n"; break;
                        }

                        //LengthValidation & RegularExpression 
                        Regex objPattern = new Regex("^[a-zA-Z0-9_@&/-]{1,25}$");
                        if (!objPattern.IsMatch(controlValue))
                        { result = @"Vendor Rmt Acct # allows 1-25 length of alphanumerics with special characters (_@&/-) only\n"; break; }

                        break;
                    }
                case "BANK NAME":
                    {
                        //Required Field Validation
                        if (string.IsNullOrEmpty(controlValue))
                        { result = @"Bank Name is required\n"; break; }

                        //LengthValidation & RegularExpression 
                        Regex objPattern = new Regex(@"[a-zA-Z0-9\s._()$!:;.,&/\\*@#+-]{1,60}$");
                        if (!objPattern.IsMatch(controlValue))
                        { result = @"Bank Name allows 1-60 length of  alphanumerics with special characters (_()$!:;.,&/\*@#+-) only\n"; break; }

                        break;
                    }
                case "BANK ID TYPE":
                    {
                        //Required Field Validation
                        if (!string.IsNullOrEmpty(controlValue)) { }
                        else { result = @"Bank Id Type is required\n"; break; }

                        //LengthValidation & RegularExpression 
                        Regex objPattern = new Regex("^ABA$");
                        if (objPattern.IsMatch(controlValue)) { }
                        else { result = @"Bank Id Type allows 3 alphabets i.e ABA only\n"; break; }

                        break;
                    }

                case "ACCOUNT CURRENCY":
                    {
                        //Required Field Validation
                        if (string.IsNullOrEmpty(controlValue))
                        { result = @"Account Currency is required\n"; break; }

                        //LengthValidation & RegularExpression 
                        Regex objPattern = new Regex("^USD$|^CAD$");
                        if (!objPattern.IsMatch(controlValue))
                        { result = @"Account Currency allows 3 alphabets i.e USD or CAD only\n"; break; }

                        break;
                    }

                case "ACCOUNT TYPE":
                    {
                        //Required Field Validation
                        if (!string.IsNullOrEmpty(controlValue)) { }
                        else { result = @"Account Type is required\n"; break; }

                        //LengthValidation & RegularExpression 
                        Regex objPattern = new Regex("^D$|^G$");
                        if (objPattern.IsMatch(controlValue)) { }
                        else { result = @"Account Type allows only 1 alphabet i.e D or G \n"; break; }

                        break;
                    }

                case "ADDRESS1":
                    {
                        //Required Field Validation
                        if (!string.IsNullOrEmpty(controlValue)) { }
                        else { result = @"Address line 1 is required\n"; break; }

                        //LengthValidation & RegularExpression 
                        Regex objPattern = new Regex(@"^[\w\. _()$!:;.\\,&*@#/-]{1,55}$");
                        if (objPattern.IsMatch(controlValue)) { }
                        else { result = @"Address 1 allows 1-55 length of  alphanumerics with special characters(_@()&/-#,:.) only\n"; break; }

                        break;
                    }

                case "ADDRESS2":
                    {
                        if (!string.IsNullOrEmpty(controlValue))
                        {
                            //LengthValidation & RegularExpression 
                            Regex objPattern = new Regex(@"^[\w\. _()$!:;.\\,&*@#/-]{1,55}$");
                            if (objPattern.IsMatch(controlValue)) { }
                            else { result = @"Address 2 allows 1-55 length of  alphanumerics with special characters(_@()&/-#,:.) only\n"; break; }
                        }
                        break;

                    }


                case "CITY":
                    {
                        //Required Field Validation
                        if (!string.IsNullOrEmpty(controlValue)) { }
                        else { result = @"City is required\n"; break; }

                        //LengthValidation & RegularExpression 
                        Regex objPattern = new Regex(@"^[a-zA-Z\s]{1,30}$");
                        if (objPattern.IsMatch(controlValue)) { }
                        else { result = @"City allows 1-30 length of alphabets with space only\n"; break; }

                        break;
                    }

                case "STATE":
                    {
                        //Required Field Validation
                        if (!string.IsNullOrEmpty(controlValue)) { }
                        else
                        {
                            result = @"State is required\n"; break;
                        }

                        //LengthValidation & RegularExpression 
                        Regex objPattern = new Regex("^[a-zA-Z]{2,3}$");
                        if (objPattern.IsMatch(controlValue)) { }
                        else { result = @"State allows 2-3 alphabets only\n"; break; }

                        break;
                    }


                case "ZIP":
                    {
                        //Required Field Validation
                        if (!string.IsNullOrEmpty(controlValue)) { }
                        else { result = @"Zip is required\n"; break; }

                        //LengthValidation & RegularExpression 
                        Regex objPattern = new Regex(@"^\d{5}$|^\d{9}$|^[a-zA-Z\s0-9]{7}$");
                        if (objPattern.IsMatch(controlValue)) { }
                        else { result = @"Zip allows allows 5 - 9 alphanumerics with space\n"; break; }

                        break;
                    }

                case "BILLINGZIP":
                    {
                        //Required Field Validation
                        if (!string.IsNullOrEmpty(controlValue)) { }
                        else { result = @"Billing Zip is required\n"; break; }

                        //LengthValidation & RegularExpression 
                        Regex objPattern = new Regex(@"^\d{5,5}$");
                        if (objPattern.IsMatch(controlValue)) { }
                        else { result = @"Billing Zip allow 5 digits only \n"; break; }

                        break;
                    }

                case "COUNTRY":
                    {
                        //Required Field Validation
                        if (!string.IsNullOrEmpty(controlValue)) { }
                        else { result = @"Country is required\n"; break; }

                        //LengthValidation & RegularExpression 
                        Regex objPattern = new Regex("^US$|^CA$");
                        if (objPattern.IsMatch(controlValue)) { }
                        else { result = @"Country allows only 2 alphabets i.e US or CA \n"; break; }

                        break;
                    }
            }
            return result;
        }

        #endregion

        #region Invoice Details Validations

        public string ValidatingInvoiceDetails(string controlValue, string action)
        {
            string result = string.Empty;
            switch (action.ToUpper())
            {
                case "CLIENT NAME":
                    {
                        //Required Field Validation
                        if (!string.IsNullOrEmpty(controlValue)) { }
                        else { result = @"Client Name is required\n"; break; }

                        //LengthValidation & RegularExpression 
                        Regex objPattern = new Regex(@"^[a-zA-Z0-9\s._@&-.]{1,60}$");
                        if (objPattern.IsMatch(controlValue)) { }
                        else { result = @"Client Name allows 1-60 length of  alphanumerics with special characters (_@&-.) only\n"; break; }

                        break;
                    }

                case "ACCOUNT NUMBER":
                    {
                        //Required Field Validation
                        if (string.IsNullOrEmpty(controlValue))
                        {
                            result = @"Account # is required\n"; break;
                        }

                        //LengthValidation & RegularExpression 
                        Regex objPattern = new Regex(@"^[a-zA-Z0-9\s._#()=+@*&-.]{1,60}$");
                        if (!objPattern.IsMatch(controlValue))
                        { result = @"Account #  allows 1-60 length of  alphanumerics with special characters (_@&-.#()=+ ) only\n"; break; }

                        break;
                    }
                case "INVOICE DATE":
                    {
                        //Required Field Validation
                        if (string.IsNullOrEmpty(controlValue))
                        {
                            result = @"Invoice Date is required\n"; break;
                        }

                        //LengthValidation & RegularExpression 
                        //Regex objPattern = new Regex(@"^(((0?[1-9]|1[012])/(0?[1-9]|1\d|2[0-8])|(0?[13456789]|1[012])/(29|30)|(0?[13578]|1[02])/31)/(19|[2-9]\d)\d{2}|0?2/29/((19|[2-9]\d)(0[48]|[2468][048]|[13579][26])|(([2468][048]|[3579][26])00)))$");
                        try
                        {
                            Convert.ToDateTime(controlValue);
                        }
                        catch (Exception)
                        {
                            result = @"Invoice Date format is invalid / Please enter Date in correct format (yyyy-mm-dd) and Year should be later than 1900 and earlier than 2099\n";
                        }

                        break;
                    }
                case "SERVICE PERIOD FROM":
                    {
                        //Required Field Validation
                        if (string.IsNullOrEmpty(controlValue))
                        { result = @"Service Period From is required\n"; break; }

                        //LengthValidation & RegularExpression 
                        //Regex objPattern = new Regex(@"^(((0?[1-9]|1[012])/(0?[1-9]|1\d|2[0-8])|(0?[13456789]|1[012])/(29|30)|(0?[13578]|1[02])/31)/(19|[2-9]\d)\d{2}|0?2/29/((19|[2-9]\d)(0[48]|[2468][048]|[13579][26])|(([2468][048]|[3579][26])00)))$");

                        try
                        {
                            Convert.ToDateTime(controlValue);
                        }
                        catch (Exception)
                        {
                            result = @"Service Period From Date format is invalid / Please enter Date in correct format (yyyy-mm-dd) and Year should be later than 1900 and earlier than 2099\n"; break;
                        }

                        break;
                    }
                case "SERVICE PERIOD TO":
                    {
                        //Required Field Validation
                        if (string.IsNullOrEmpty(controlValue))
                        { result = @"Service Period From is required\n"; break; }

                        //LengthValidation & RegularExpression 
                        //Regex objPattern = new Regex(@"^(((0?[1-9]|1[012])/(0?[1-9]|1\d|2[0-8])|(0?[13456789]|1[012])/(29|30)|(0?[13578]|1[02])/31)/(19|[2-9]\d)\d{2}|0?2/29/((19|[2-9]\d)(0[48]|[2468][048]|[13579][26])|(([2468][048]|[3579][26])00)))$");

                        try
                        {
                            Convert.ToDateTime(controlValue);
                        }
                        catch (Exception)
                        {
                            result = @"Service Period To Date format is invalid / Please enter Date in correct format (yyyy-mm-dd) and Year should be later than 1900 and earlier than 2099\n"; break;
                        }

                        break;
                    }
                case "SETTLEMENT DATE":
                    {
                        //Required Field Validation
                        if (string.IsNullOrEmpty(controlValue))
                        { result = @"Settlement Date is required\n"; break; }

                        //LengthValidation & RegularExpression 
                        //Regex objPattern = new Regex(@"^(((0?[1-9]|1[012])/(0?[1-9]|1\d|2[0-8])|(0?[13456789]|1[012])/(29|30)|(0?[13578]|1[02])/31)/(19|[2-9]\d)\d{2}|0?2/29/((19|[2-9]\d)(0[48]|[2468][048]|[13579][26])|(([2468][048]|[3579][26])00)))$");

                        try
                        {
                            Convert.ToDateTime(controlValue);
                        }
                        catch (Exception)
                        {
                            result = @"Settlement Date format is invalid / Please enter Date in correct format (yyyy-mm-dd) and Year should be later than 1900 and earlier than 2099\n"; break;
                        }

                        break;
                    }

                case "BILL NUMBER":
                    {
                        //Required Field Validation
                        if (!string.IsNullOrEmpty(controlValue)) { }
                        else
                        {
                            result = @"Bill # is required\n"; break;
                        }

                        //LengthValidation & RegularExpression 
                        Regex objPattern = new Regex(@"^[a-zA-Z0-9\s_@&-.]{1,30}$");
                        if (objPattern.IsMatch(controlValue)) { }
                        else { result = @"Bill #  allows 1-30 length of  alphanumerics with special characters (_@&-.) only\n"; break; }

                        break;
                    }

                case "TRANSACTION ID":
                    {
                        //Required Field Validation
                        if (string.IsNullOrEmpty(controlValue))
                        { result = @"Transaction Id is required\n"; break; }

                        //LengthValidation & RegularExpression 
                        Regex objPattern = new Regex(@"^[a-zA-Z0-9\s_-]{1,30}$");
                        if (!objPattern.IsMatch(controlValue))
                        { result = @"Transaction Id  allows 1-30 length of  alphanumerics with special characters (_-) only\n"; break; }

                        break;
                    }

                case "PAY BY AMOUNT":
                    {
                        //Required Field Validation
                        if (string.IsNullOrEmpty(controlValue))
                        { result = @"Pay BY Amount is required\n"; break; }

                        //LengthValidation & RegularExpression 
                        Regex objPattern = new Regex(@"^[+-]?\d{1,13}.\d{2}");
                        if (!objPattern.IsMatch(controlValue))
                        { result = @"Pay BY Amount allows 1-15 length of digits with 2 decimal points only\n"; break; }

                        break;
                    }


                case "BILL AMOUNT":
                    {
                        //Required Field Validation
                        if (!string.IsNullOrEmpty(controlValue)) { }
                        else { result = @"Bill Amount is required\n"; break; }

                        //LengthValidation & RegularExpression 
                        Regex objPattern = new Regex(@"^[+-]?\d{1,13}.\d{2}");
                        if (objPattern.IsMatch(controlValue)) { }
                        else { result = @"Bill Amount allows 1-15 length of digits with 2 decimal points only\n"; break; }

                        break;
                    }

                case "CURRENT AMOUNT":
                    {
                        //Required Field Validation
                        if (!string.IsNullOrEmpty(controlValue)) { }
                        else { result = @"Current Amount is required\n"; break; }

                        //LengthValidation & RegularExpression 
                        Regex objPattern = new Regex(@"^[+-]?\d{1,13}.\d{2}");
                        if (objPattern.IsMatch(controlValue)) { }
                        else { result = @"Current Amount allows 1-15 length of digits with 2 decimal points only\n"; break; }

                        break;
                    }

            }
            return result;
        }

        #endregion

        #region Payment File Transaction Details

        public string ValidatingPFDetails(string controlValue, string action)
        {
            string result = string.Empty;
            switch (action.ToUpper())
            {
                case "PF TRANSACTION COUNT":
                    {
                        //Required Field Validation
                        if (string.IsNullOrEmpty(controlValue)) { result = @"PF Transaction Count is required\n"; break; }

                        //LengthValidation & RegularExpression 
                        Regex objPattern = new Regex(@"^\d{1,8}$");
                        if (!objPattern.IsMatch(controlValue)) { result = @"PF Transaction Count allows 1-8 length of digits\n"; break; }

                        break;
                    }

                case "PF TRANSACTION AMOUNT":
                    {
                        //Required Field Validation
                        if (string.IsNullOrEmpty(controlValue)) { result = @"PF Transaction Amount is required\n"; break; }

                        //LengthValidation & RegularExpression 
                        Regex objPattern = new Regex(@"^\d{1,13}.\d{2}");
                        if (!objPattern.IsMatch(controlValue)) { result = @"PF Transaction Amount allows 1-15 length of digits with 2 decimal points only\n"; break; }
                        break;
                    }
                case "COMPANY ID":
                    {
                        //Required Field Validation
                        if (string.IsNullOrEmpty(controlValue)) { result = @"Company Id is required\n"; break; }

                        //LengthValidation & RegularExpression 
                        Regex objPattern = new Regex(@"^[a-zA-Z0-9\s_@&-.]{1,30}$");
                        if (!objPattern.IsMatch(controlValue)) { result = @"Company Id allows 1-30 length of  alphanumerics with special characters (_@&-.) only\n"; break; }
                        break;
                    }

                case "PF DATE":
                    {
                        //Required Field Validation
                        if (string.IsNullOrEmpty(controlValue))
                        { result = @"PF Date From is required\n"; break; }

                        //LengthValidation & RegularExpression 
                        //Regex objPattern = new Regex(@"^(((0?[1-9]|1[012])/(0?[1-9]|1\d|2[0-8])|(0?[13456789]|1[012])/(29|30)|(0?[13578]|1[02])/31)/(19|[2-9]\d)\d{2}|0?2/29/((19|[2-9]\d)(0[48]|[2468][048]|[13579][26])|(([2468][048]|[3579][26])00)))$");

                        try
                        {
                            Convert.ToDateTime(controlValue);
                        }
                        catch (Exception)
                        {
                            result = @"PF Date format is invalid / Please enter Date in correct format (yyyy-mm-dd) and Year should be later than 1900 and earlier than 2099\n"; break;
                        }

                        break;
                    }

                case "SERVICE PERIOD FROM":
                    {
                        //Required Field Validation
                        if (string.IsNullOrEmpty(controlValue))
                        { result = @"Service Period From is required\n"; break; }

                        //LengthValidation & RegularExpression 
                        //Regex objPattern = new Regex(@"^(((0?[1-9]|1[012])/(0?[1-9]|1\d|2[0-8])|(0?[13456789]|1[012])/(29|30)|(0?[13578]|1[02])/31)/(19|[2-9]\d)\d{2}|0?2/29/((19|[2-9]\d)(0[48]|[2468][048]|[13579][26])|(([2468][048]|[3579][26])00)))$");

                        try
                        {
                            Convert.ToDateTime(controlValue);
                        }
                        catch (Exception)
                        {
                            result = @"Service Period From Date format is invalid / Please enter Date in correct format (yyyy-mm-dd) and Year should be later than 1900 and earlier than 2099\n"; break;
                        }

                        break;
                    }
                case "SERVICE PERIOD TO":
                    {
                        //Required Field Validation
                        if (string.IsNullOrEmpty(controlValue))
                        { result = @"Service Period From is required\n"; break; }

                        //LengthValidation & RegularExpression 
                        //Regex objPattern = new Regex(@"^(((0?[1-9]|1[012])/(0?[1-9]|1\d|2[0-8])|(0?[13456789]|1[012])/(29|30)|(0?[13578]|1[02])/31)/(19|[2-9]\d)\d{2}|0?2/29/((19|[2-9]\d)(0[48]|[2468][048]|[13579][26])|(([2468][048]|[3579][26])00)))$");

                        try
                        {
                            Convert.ToDateTime(controlValue);
                        }
                        catch (Exception)
                        {
                            result = @"Service Period To Date format is invalid / Please enter Date in correct format (yyyy-mm-dd) and Year should be later than 1900 and earlier than 2099\n"; break;
                        }

                        break;
                    }

                case "BILL NUMBER":
                    {
                        //Required Field Validation
                        if (!string.IsNullOrEmpty(controlValue)) { }
                        else
                        {
                            result = @"Bill # is required\n"; break;
                        }

                        //LengthValidation & RegularExpression 
                        Regex objPattern = new Regex(@"^[a-zA-Z0-9\s_@&-.]{1,30}$");
                        if (objPattern.IsMatch(controlValue)) { }
                        else { result = @"Bill #  allows 1-30 length of  alphanumerics with special characters (_@&-.) only\n"; break; }

                        break;
                    }

                case "TRANSACTION ID":
                    {
                        //Required Field Validation
                        if (!string.IsNullOrEmpty(controlValue)) { }
                        else { result = @"Transaction Id is required\n"; break; }

                        //LengthValidation & RegularExpression 
                        Regex objPattern = new Regex(@"^[a-zA-Z0-9\s_-]{1,30}$");
                        if (objPattern.IsMatch(controlValue)) { }
                        else { result = @"Transaction Id  allows 1-30 length of  alphanumerics with special characters (_-) only\n"; break; }

                        break;
                    }

                case "PAY BY AMOUNT":
                    {
                        //Required Field Validation
                        if (string.IsNullOrEmpty(controlValue))
                        { result = @"Pay BY Amount is required\n"; break; }

                        //LengthValidation & RegularExpression 
                        Regex objPattern = new Regex(@"^[+-]?\d{1,13}.\d{2}");
                        if (!objPattern.IsMatch(controlValue))
                        { result = @"Pay BY Amount allows 1-15 length of digits with 2 decimal points only\n"; break; }

                        break;
                    }

                case "CURRENT AMOUNT":
                    {
                        //Required Field Validation
                        if (!string.IsNullOrEmpty(controlValue)) { }
                        else { result = @"Current Amount is required\n"; break; }

                        //LengthValidation & RegularExpression 
                        Regex objPattern = new Regex(@"^[+-]?\d{1,13}.\d{2}");
                        if (objPattern.IsMatch(controlValue)) { }
                        else { result = @"Current Amount allows 1-15 length of digits with 2 decimal points only\n"; break; }

                        break;
                    }

                case "NOTE TYPE":
                    {
                        //Required Field Validation
                        if (!string.IsNullOrEmpty(controlValue)) { }
                        else
                        {
                            result = @"Note Type is required\n"; break;
                        }

                        //LengthValidation & RegularExpression 
                        Regex objPattern = new Regex(@"^[a-zA-Z0-9]{3}$");
                        if (objPattern.IsMatch(controlValue)) { }
                        else { result = @"Note Type  allows 3 alphanumerics only\n"; break; }

                        break;
                    }

                case "NOTE TEXT":
                    {
                        //Required Field Validation
                        if (!string.IsNullOrEmpty(controlValue)) { }
                        else
                        {
                            result = @"Note Text is required\n"; break;
                        }

                        //LengthValidation & RegularExpression 
                        Regex objPattern = new Regex(@"^[a-zA-Z0-9_@&-.]{1,80}$");
                        if (objPattern.IsMatch(controlValue)) { }
                        else { result = @"Note Text allows length of 1-80 alphanumerics with special characters(_@&-.) only\n"; break; }

                        break;
                    }

                case "FILE CONTROL NUMBER":
                    {
                        //Required Field Validation
                        if (string.IsNullOrEmpty(controlValue))
                        {
                            result = @"File Control # is required\n"; break;
                        }

                        //LengthValidation & RegularExpression 
                        Regex objPattern = new Regex(@"^[a-zA-Z0-9.]{22}$");
                        if (!objPattern.IsMatch(controlValue))
                        { result = @"File Control # allows length of 22 alphanumerics with special characters(.) only\n"; break; }

                        break;
                    }


                case "FILE DATE":
                    {
                        //Required Field Validation
                        if (!string.IsNullOrEmpty(controlValue)) { }
                        else { result = @"File Date is required\n"; break; }

                        //LengthValidation & RegularExpression 
                        Regex objPattern = new Regex(@"^(((0?[1-9]|1[012])/(0?[1-9]|1\d|2[0-8])|(0?[13456789]|1[012])/(29|30)|(0?[13578]|1[02])/31)/(19|[2-9]\d)\d{2}|0?2/29/((19|[2-9]\d)(0[48]|[2468][048]|[13579][26])|(([2468][048]|[3579][26])00)))$");
                        if (objPattern.IsMatch(controlValue)) { }
                        else { result = @"File Date format is invalid / Please enter Date in correct format (YYYYMMDD) and Year should be later than 1900 and earlier than 2099\n"; break; }

                        break;
                    }

            }
            return result;
        }

        #endregion
    }
}

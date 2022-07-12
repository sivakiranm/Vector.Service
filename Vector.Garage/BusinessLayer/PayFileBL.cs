using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Schema;
using Vector.Common.BusinessLayer;
using Vector.Common.DataLayer;
using Vector.Common.Entities;
using Vector.Garage.DataLayer;
using Vector.Garage.Entities;
using PaymentFile;

namespace Vector.Garage.BusinessLayer
{
    public class PayFileBL : DisposeLogic
    {
        private VectorDataConnection objVectorDB;
        PaymentFile.File[] objPFFile;
        string strValidationErrors = string.Empty;
        string strErrorMessages = string.Empty;
        string strNameSpace = String.Empty;

        private enum Info
        {
            BatchID,
            CustomerID
        }

        public PayFileBL(VectorDataConnection objVectorCon)
        {
            this.objVectorDB = objVectorCon;
        }

        public VectorResponse<object> GetClientPayConfigurationInfo(ClientPayAccountInfo objClientPayAccountInfo)
        {
            using (var objPayFileDL = new PayFileDL(objVectorDB))
            {
                var result = objPayFileDL.GetClientPayConfigurationInfo(objClientPayAccountInfo);

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

        public VectorResponse<object> ManageClientPayAccount(ClientPayAccountInfo objClientPayAccountInfo)
        {
            using (var objPayFileDL = new PayFileDL(objVectorDB))
            {
                var result = objPayFileDL.ManageClientPayAccount(objClientPayAccountInfo);

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

        public VectorResponse<object> GetConsolidatedInvoiceForPayFile(PayFileInfo objPayFileInfo)
        {
            using (var objPayFileDL = new PayFileDL(objVectorDB))
            {
                var result = objPayFileDL.GetConsolidatedInvoiceForPayFile(objPayFileInfo);

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

        public VectorResponse<object> ManagePayFileGeneration(PayFileInfo objPayFileInfo)
        {
            using (var objPayFileDL = new PayFileDL(objVectorDB))
            {
                var result = objPayFileDL.ManagePayFileGeneration(objPayFileInfo);

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

        public VectorResponse<object> ManageElectronicPayFileGeneration(PayFileInfo objPayFileInfo)
        {
            using (var objPayFileDL = new PayFileDL(objVectorDB))
            {
                var result = objPayFileDL.ManageElectronicPayFileGeneration(objPayFileInfo);

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

        public VectorResponse<object> GetPayFiles(SearchPayFile objSearchPayFile)
        {
            using (var objPayFileDL = new PayFileDL(objVectorDB))
            {
                var result = objPayFileDL.GetPayFiles(objSearchPayFile);

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

        public VectorResponse<object> ManagePayFile(ManagePayFile objManagePayFile,string payFilePath,Int64 userId)
        {
            using (var objPayFileDL = new PayFileDL(objVectorDB))
            {
                
                if (objManagePayFile.Action == "Approve")
                {

                    DataSet objPayFileResult = GenerateWFPaymentFile(objManagePayFile.PayFileId, userId, objManagePayFile.Comments, payFilePath, objManagePayFile);

                    if (DataValidationLayer.isDataSetNotNull(objPayFileResult))
                    {
                        objPayFileResult.Tables[VectorConstants.Zero].TableName = "result";
                        return new VectorResponse<object>() { ResponseData = objPayFileResult };
                    }
                    else
                    {
                        return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                    }
                }
                else
                {
                    var result = objPayFileDL.ManagePayFile(objManagePayFile); 
                    if (DataValidationLayer.isDataSetNotNull(result))
                    {
                        result.Tables[VectorConstants.Zero].TableName = "result";
                        return new VectorResponse<object>() { ResponseData = result };
                    }
                    else
                    {
                        return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                    }
                }
                 
            
            }
        }

        

        public VectorResponse<object> ManageEletronicTransaction(EletronicTransaction objEletronicTransaction)
        {
            using (var objPayFileDL = new PayFileDL(objVectorDB))
            {
                var result = objPayFileDL.ManageEletronicTransaction(objEletronicTransaction);

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

        public VectorResponse<object> RejectPayFileTransactions(ManagePayFile objManagePayFile)
        {
            using (var objPayFileDL = new PayFileDL(objVectorDB))
            {
                var result = objPayFileDL.RejectPayFileTransactions(objManagePayFile);

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

        #region Generate Payment File

        private DataSet GenerateWFPaymentFile(Int64 pfIdfr, Int64 userId, string comments, string paymentFileSourcePath, ManagePayFile objManagePayFile)
        {
            DataSet dsData = new DataSet();
            DataTable dtData = new DataTable();
            dtData.Columns.Add("Result", typeof(Int32));
            dtData.Columns.Add("ResultMessage", typeof(string));
            dtData.Columns.Add("ErrorMessageToLog", typeof(string));
            dtData.Columns.Add("PayFileXML", typeof(string));

            string errormessage = string.Empty;
            //NameValueCollection objResult = new NameValueCollection();

            string[] result = GeneratePaymentFileXMl(pfIdfr, comments.Replace("'", "").Trim(), userId,0, paymentFileSourcePath, objManagePayFile);
            if (result[0].ToString() == "FAIL")
            {
                DataRow drRow = dtData.NewRow();
                drRow["Result"] = 0;
                drRow["ResultMessage"] = "Error while generating the Pay File..";
                dtData.Rows.Add(drRow);
            }
            else if (result[0].ToString() == "INVALIDDATA")
            {
                DataRow drRow = dtData.NewRow();
                drRow["Result"] = 0;
                drRow["ResultMessage"] = "Unable to save the Pay File,Pay File Generated is not having valid Data." + result[1].Replace("\\n", Environment.NewLine).ToString();
                dtData.Rows.Add(drRow); 
            }
            else if (result[0].ToString() == "INVALIDXML")
            {
                DataRow drRow = dtData.NewRow();
                drRow["Result"] = 0;
              
             
                if (!string.IsNullOrEmpty(result[1].ToString()))
                {
                    errormessage = errormessage + result[1].ToString();
                }
                if (!string.IsNullOrEmpty(result[2].ToString()))
                {
                    errormessage = errormessage + ". " + result[1].ToString();
                }

                drRow["ResultMessage"] = errormessage;
                dtData.Rows.Add(drRow);
            }
            else if (result[0].ToString() == "XMLSAVED")
            {
                errormessage = "Pay File saved Successfully & Successfully updated Status to Approved";
                DataRow drRow = dtData.NewRow();
                drRow["Result"] =1;
                drRow["ResultMessage"] = errormessage;
                drRow["PayFileXML"] = result[1].ToString() + ".xml";
                dtData.Rows.Add(drRow); 
            }
            dsData.Tables.Add(dtData);
            return dsData;
        }

        /// <summary>
        /// Generate Payment File XMl
        /// </summary>
        /// <returns>String</returns>
        public string[] GeneratePaymentFileXMl(Int64 PfIdfr, String comments, Int64 userId, Int64 highApprovalLevelId, string paymentFileSourcePath, ManagePayFile objManagePayFile)
        {
            //Table 0 Pf header details
            //table 1 Ach
            //Table 2 Check
            //Table 3 SUCC
            bool isAchExists = false;
            bool isCheckExists = false;
            string isVerificationEnabled = ConfigurationManager.AppSettings["PayfileVerificationEnabled"].ToString(); 

            using (var objPkrmInventoryDA = new PayFileDL(objVectorDB))
            {
                DataSet dsPFXML = objPkrmInventoryDA.GetPFDetailsForXML(PfIdfr);
                //DecryptAccountDetails(ref dsPFXML);
                //Validate Payment File 
                String errorMessage = ValidatePaymentFileXMl(ref dsPFXML);
                if (String.IsNullOrEmpty(errorMessage))
                {
                    string paymentXML = string.Empty;
                    string PFFileName = string.Empty;
                    try
                    {
                        // Root file 
                        objPFFile = new PaymentFile.File[1];
                        objPFFile[0] = new PaymentFile.File();
                        if (dsPFXML.Tables.Count > 0)
                            if (dsPFXML.Tables[0].Rows.Count > 0)
                            {

                                /**************************************************************************
                                 *                 File Header  
                                 ***************************************************************************/
                                if (!string.IsNullOrEmpty(dsPFXML.Tables[0].Rows[0]["PayFileName"].ToString().Trim()))
                                    PFFileName = ReplaceReserverCharacters(dsPFXML.Tables[0].Rows[0]["PayFileName"].ToString());


                                if (!string.IsNullOrEmpty(dsPFXML.Tables[0].Rows[0]["DisbursementAccountCode"].ToString().Trim()))
                                    objPFFile[0].CompanyID = ReplaceReserverCharacters(dsPFXML.Tables[0].Rows[0]["DisbursementAccountCode"].ToString());


                                if (!string.IsNullOrEmpty(dsPFXML.Tables[0].Rows[0]["PaymentAmount"].ToString().Trim()))
                                    objPFFile[0].PmtRecTotal = dsPFXML.Tables[0].Rows[0]["PaymentAmount"].ToString();
                            }


                        if (dsPFXML.Tables[1].Rows.Count > 0 || dsPFXML.Tables[2].Rows.Count > 0 || dsPFXML.Tables[3].Rows.Count > 0)
                        {
                            int achCount = dsPFXML.Tables[1].Rows.Count;
                            int chkCount = 0;
                            int succCount = dsPFXML.Tables[3].Rows.Count;
                            DataTable dtCheckNumbers = null;
                            if (dsPFXML.Tables[2].Rows.Count > 0)
                            {
                                DataView dvCheckNumbers = new DataView(dsPFXML.Tables[2]);
                                dtCheckNumbers = dvCheckNumbers.ToTable(true, "CHECKNUMBER");
                                chkCount = dtCheckNumbers.Rows.Count;
                                isCheckExists = true;
                            }

                            //Can be multiple Payment records
                            // run a loop for payment record 
                            objPFFile[0].PmtRecCount = Convert.ToString(achCount + chkCount + succCount);
                            PmtRec[] objPmtRec = new PmtRec[achCount + chkCount + succCount];


                            /**************************************************************************
                            *                  Adding ACH Payment Records  
                            ***************************************************************************/

                            if (dsPFXML.Tables[1].Rows.Count > 0)
                            {
                                for (int i = 0; i < dsPFXML.Tables[1].Rows.Count; i++)
                                {
                                    isAchExists = true;
                                    //Fill ACH Details
                                    objPmtRec[i] = GenerateACHTransactions(ref dsPFXML, ref i, isVerificationEnabled);
                                }
                            }


                            ///End of Payment Record Loop

                            /**************************************************************************
                              *                  Adding check Payment Records  
                              ***************************************************************************/
                            if (dsPFXML.Tables[2].Rows.Count > 0)
                            {
                                for (int j = 0; j < chkCount; j++)
                                {
                                    objPmtRec[achCount + j] = GenerateCheckTransactions(ref dsPFXML, ref dtCheckNumbers, ref j, isCheckExists, isAchExists, isVerificationEnabled);
                                }
                            }
                            ///End of Payment Record Loop


                            /**************************************************************************
                              *                  Adding Succ Payment Records  
                            ***************************************************************************/
                            if (dsPFXML.Tables[3].Rows.Count > 0)
                            {
                                for (int k = 0; k < succCount; k++)
                                {
                                    objPmtRec[chkCount + achCount + k] = GenerateSUCCTransactions(ref dsPFXML, ref k, isCheckExists, isAchExists, isVerificationEnabled);
                                }
                            }

                            ///End of Payment Record Loop
                            ///
                            //Add Payment record to File 
                            objPFFile[0].PmtRec = objPmtRec;

                        }


                        /**************************************************************************
                         *                 File Footer  
                         ***************************************************************************/


                        if (dsPFXML.Tables[0].Rows.Count > 0)
                        {

                            //FileGroup
                            FileInfoGrp objFileInfoGroup = new FileInfoGrp();
                            DateTime fileDate = Convert.ToDateTime(dsPFXML.Tables[0].Rows[0]["PayFileDate"].ToString());
                            objFileInfoGroup.FileDate = string.Format("{0:yyyyMMdd}", fileDate);
                            objFileInfoGroup.FileControlNumber = dsPFXML.Tables[0].Rows[0]["PayFileName"].ToString();
                            PFFileName = dsPFXML.Tables[0].Rows[0]["PayFileName"].ToString();
                            //Add File group as footer 
                            objPFFile[0].FileInfoGrp = objFileInfoGroup;

                        }

                        ///Validate XML
                        string[] strErrors = objPFFile[0].ValidationErrors;
                        bool errorflag = objPFFile[0].ValidFlag;


                        // getting XMl
                        paymentXML = Vector.Common.BusinessLayer.Common.SerializeObject<PaymentFile.File>(objPFFile[0]);
                        paymentXML = paymentXML.Remove(0, 1);
                        Validate(paymentXML);


                        //Remove XSI XSD and Namespace
                        paymentXML = paymentXML.Replace("http://www.w3.org/2001/XMLSchema-instance", string.Empty).Replace("xmlns:xsi=", string.Empty).Replace(@"""""", string.Empty);
                        paymentXML = paymentXML.Replace("xsi:type=", string.Empty).Replace("xsd:string", string.Empty).Replace(@"""""", string.Empty);
                        paymentXML = paymentXML.Replace("http://www.w3.org/2001/XMLSchema", string.Empty).Replace("xmlns:xsd=", string.Empty).Replace(@"""""", string.Empty);
                        paymentXML = paymentXML.Replace("http://www.prokarma.com/PF.xsd", string.Empty).Replace("xmlns=", string.Empty).Replace(@"""""", string.Empty);

                        if (string.IsNullOrEmpty(strValidationErrors.Trim()) && string.IsNullOrEmpty(strErrorMessages.Trim()))
                        {
                            //save xml file to 
                            // Create the XmlDocument.
                            XmlDocument doc = new XmlDocument();
                            doc.LoadXml(paymentXML);

                            // Save the document to a file. White space is
                            // preserved (no white space).
                            doc.PreserveWhitespace = true;
                            string filePath = string.Empty;
                            string appPath = HttpContext.Current.Request.ApplicationPath;
                            string physicalPath = HttpContext.Current.Request.MapPath(appPath);

                            if (!string.IsNullOrEmpty(paymentFileSourcePath))
                                filePath = paymentFileSourcePath + PFFileName + ".xml";
                            else
                                filePath = physicalPath + "\\Files\\XML\\" + PFFileName + ".xml";

                            string[] result = new string[2];
                            if (System.IO.File.Exists(filePath))
                            {
                                //If it is not high level approval then check for this condition.
                                if (highApprovalLevelId <= 0)
                                {
                                    VectorTextLogger.LogErrortoFile("PaymentFile : High Approval : high Approval Fail", Convert.ToString("Vector"), Convert.ToString(userId), "Method : GeneratePaymentFileXMl");
                                    //EMSTextLogger.LogErrortoFile("PaymentFile : High Approval : high Approval Fail");
                                    result[0] = "FAIL";
                                    result[1] = PFFileName;
                                    return result;
                                }
                            }
                            doc.Save(filePath);

                            //Save Xml into the DB

                            System.IO.FileStream fsPF = new System.IO.FileStream(filePath, System.IO.FileMode.Open);
                            Byte[] pfXmlByte = new byte[fsPF.Length];
                            fsPF.Read(pfXmlByte, 0, (int)fsPF.Length);
                            fsPF.Close();

                            //Initially set status of updation to zero
                            int status = 0;

                            //If it is high approval then it's level will be greater than 0 and update PF status else regular PF update status
                            //if (highApprovalLevelId > 0)
                            //    status = UpdateHighApprovalPaymentFile(PfIdfr, "APPROVE", comments, userId, pfXmlByte);
                            //else
                                //status = UpdatePaymentFile(PfIdfr, "APPROVE", comments, userId, pfXmlByte);

                            using (var objPayFileDL = new PayFileDL(objVectorDB))
                            {

                                var dsResult = objPayFileDL.ManagePayFile(objManagePayFile);
                                if (DataValidationLayer.isDataSetNotNull(dsResult))
                                {
                                   if(dsResult.Tables.Count>0 && dsResult.Tables[0].Rows.Count >0)
                                    {
                                        int resultValue = (from C in dsResult.Tables[0].AsEnumerable()
                                                           where C.Field<Boolean>("Result") == true
                                                           select C).Count();
                                        if(resultValue > 0)
                                        {
                                            status = 1;
                                        }
                                    } 
                                } 
                            }

                            //If it is high approval check it's status is -2 so that failure cause will be user might be unauthorized else normal workflow
                            if (highApprovalLevelId > 0)
                            {
                                if (status == -2)
                                {
                                    result[0] = "UnAuthorized";
                                    result[1] = status.ToString();
                                    return result;
                                }
                            }

                            if (status > 0)
                            {
                                result[0] = "XMLSAVED";
                                result[1] = PFFileName;
                                return result;
                            }
                            else
                            {
                                VectorTextLogger.LogErrortoFile("PaymentFile :  Status : Fail", Convert.ToString("Vector"), Convert.ToString(userId), "Method : GeneratePaymentFileXMl");
                                //EMSTextLogger.LogErrortoFile("PaymentFile :  Status : Fail");
                                result[0] = "FAIL";
                                result[1] = PFFileName;
                                if (System.IO.File.Exists(filePath))
                                    System.IO.File.Delete(filePath);
                                return result;
                            }

                        }
                        else
                        {
                            string[] result = new string[3];
                            result[0] = "INVALIDXML";
                            result[1] = strErrorMessages;
                            result[2] = strValidationErrors;
                            return result;
                        }

                    }
                    catch (Exception ex)
                    {
                        VectorTextLogger.LogErrortoFile("PaymentFile : " + ex.Message, Convert.ToString("Vector"), Convert.ToString(userId), "Method : GeneratePaymentFileXMl");
                        //EMSTextLogger.LogErrortoFile("PaymentFile : " + ex.Message);
                        string[] result = new string[1];
                        result[0] = "FAIL";
                        return result;

                    }


                } //end of validations 
                else
                {
                    string[] result = new string[2];
                    result[0] = "INVALIDDATA";
                    result[1] = errorMessage;
                    return result;
                }
            }

        }

        /// <summary>
        ///  Event Handler for XML Validation 
        /// </summary>
        /// <param name="object"></param>
        /// <param name="ValidationEventArgs"></param>
        private void ValidationCallBack(object sender, ValidationEventArgs args)
        {
            strValidationErrors += "" + args.Message;
        }

        /// <summary>
        /// Validate xml with given xsd
        /// </summary>
        /// <param name="xml"></param>
        /// <returns>Dataset</returns>
        public void Validate(String xml)
        {


            string appPath = HttpContext.Current.Request.ApplicationPath;
            string physicalPath = HttpContext.Current.Request.MapPath(appPath);
            string xsdFilePath = physicalPath + "\\Files\\XSD\\PFXML.xsd";
            string NameSpace = "http://www.prokarma.com/PF.xsd";
            //xml = xml.Replace("<File ", "<File xmlns='" + NameSpace + "' ");
            strNameSpace = NameSpace;
            // Set the validation settings.
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            XmlSchemaSet xss = new XmlSchemaSet();
            xss.Add(NameSpace, xsdFilePath);
            settings.Schemas.Add(xss);
            settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);

            // Create the XmlReader object.
            XmlReader reader = XmlReader.Create(new System.IO.StringReader(xml), settings);

            try
            {

                while (reader.Read())
                {
                    if (String.IsNullOrEmpty(strNameSpace))
                        strNameSpace = reader.NamespaceURI;
                }

                //Check any vlidation error .if there is a validation error return null else go futher to get dataset
                if (String.IsNullOrEmpty(strValidationErrors) == false)
                {
                    return;
                }

                if (String.Compare(strNameSpace, NameSpace) == 0)
                {




                    return;

                }
                else // Incorrect namespace
                {
                    strErrorMessages = "Invalid XML Against XSD ,Check NameSpace xmlns tag";
                }

            }
            catch (System.Xml.XmlException xmlError)
            {
                strErrorMessages = xmlError.Message;
                return;
            }
            catch
            {
                strErrorMessages = "XML Document is not well-formed.";
                return;
            }
            finally
            {
                reader.Close();
            }


            return;
        }

        public int UpdatePaymentFile(Int64 pfIdfr, string action, string comments, Int64 userId, byte[] XmlContent = null)
        {
            //objPkrmInventoryDA = new PkrmInventoryDA(objCommonDA);
            //return objPkrmInventoryDA.UpdatePaymentFile(pfIdfr, action, comments, userId, XmlContent);
            return 0;
        }
        public int UpdateHighApprovalPaymentFile(Int64 pfIdfr, string action, string comments, string userId, byte[] XmlContent = null, string approvalType = null)
        {
            //objPkrmInventoryDA = new PkrmInventoryDA(objCommonDA);
            //return objPkrmInventoryDA.UpdateHighApprovalPaymentFile(pfIdfr, action, comments, userId, XmlContent, approvalType);
            return 0;
        }

        private RcvrDepAcctID EmptyReceiverPartyAcctDetails(bool isCheckExists, bool isAchExists)
        {
            // Receiver Dept Account Info : Header
            RcvrDepAcctID objRceiverDepAcctID = new RcvrDepAcctID();
            DepAcctID objRecvDepAcctID = new DepAcctID();
            // Receiver Dept Account Info : Bank Info
            BankInfo objRecvBankInfo = new BankInfo();
            // add receiver bank info
            objRecvDepAcctID.BankInfo = objRecvBankInfo;

            //Srinivas Kothapalli : Added For Verification Report, as we require this for Reading XML, only for CHECK transactions
            if (isCheckExists && !isAchExists)
            {
                RefInfo[] objRefInfo = new RefInfo[1];
                objRefInfo[0] = new RefInfo();
                objRecvBankInfo.RefInfo = objRefInfo;
            }

            //Add DeptAcctID to Receiver Dept Acct ID 
            objRceiverDepAcctID.DepAcctID = objRecvDepAcctID;

            return objRceiverDepAcctID;
        }

        /// <summary>
        /// Check
        /// </summary>
        /// <param name="dsPFXML"></param>
        /// <param name="dtCheckNumbers"></param>
        /// <param name="achCount"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        private PmtRec GenerateCheckTransactions(ref DataSet dsPFXML, ref DataTable dtCheckNumbers, ref int j, bool isCheckExists, bool isAchExists, string isPfVerificationEnabled)
        {
            DataRow[] drPF = dsPFXML.Tables[2].Select("CHECKNUMBER = '" + dtCheckNumbers.Rows[j]["CHECKNUMBER"].ToString() + "'");
            DataRow drPFDetails = drPF[0];

            PmtRec objPmtRec = new PmtRec();
            //Payment record header 
            objPmtRec.DeliveryMethodSpecified = true;

            if (drPFDetails["DeliveryMethod"].ToString() == "000")
                objPmtRec.DeliveryMethod = PmtRecDeliveryMethod.Item000;
            else if (drPFDetails["DeliveryMethod"].ToString() == "001")
                objPmtRec.DeliveryMethod = PmtRecDeliveryMethod.Item001;
            else if (drPFDetails["DeliveryMethod"].ToString().ToUpper() == "X")
                objPmtRec.DeliveryMethod = PmtRecDeliveryMethod.X;

            //Implemented for RBCA : Overnight Payee only
            // If RBCA & Overnight Payee then : SDC , for others its CHK
            string companyCode = (from c in dsPFXML.Tables[VectorConstants.Zero].AsEnumerable()
                                  select c.Field<string>("DisbursementAccountCode")).FirstOrDefault();

            //if (StringManager.IsEqual(companyCode, "RBCA") && drPFDetails["DeliveryMethod"].ToString() == "001")
            //    objPmtRec.PmtMethod = PmtRecPmtMethod.SDC;
            //else
                objPmtRec.PmtMethod = PmtRecPmtMethod.CHK;

            // These are all common for SDC and CHK
            objPmtRec.PmtCrDrSpecified = true;
            objPmtRec.PmtCrDr = PmtRecPmtCrDr.C;


            //adding the Message  CHK to the Payment file

            //Message[] objMessage = new Message[1];
            //objMessage[0] = new Message();
            //objMessage[0].MsgType = MessageMsgType.CHK;
            //objMessage[0].MsgTypeSpecified = true;
            //objMessage[0].MsgText = "For questions call: " + ReplaceReserverCharacters(drPFDetails["Message"].ToString()) 
            //                                               + "  "
            //                                               + ReplaceReserverCharacters(drPFDetails["PKRM_ACCOUNT_TYPE"].ToString())
            //                                               + " " 
            //                                               + ReplaceReserverCharacters(drPFDetails["PKRM_ACCOUNT_NUMBER"].ToString());
            //objPmtRec.Message = objMessage;

            //Take this message ReplaceReserverCharacters(drInvoice["PKRM_ACCOUNT_NUMBER"].ToString());

            // Originator Party
            OrgnrParty objOrgparty = new OrgnrParty();
            Name ObjOrgName = new Name();
            ObjOrgName.Name1 = ReplaceReserverCharacters(dsPFXML.Tables[0].Rows[0]["DisbursementAccountName"].ToString());
            PostAddr ObjOrgPostAdd = new PostAddr();
            ObjOrgPostAdd.Addr1 = ReplaceReserverCharacters(drPFDetails["OriginatingAddress1"].ToString());
            ObjOrgPostAdd.Addr2 = ReplaceReserverCharacters(drPFDetails["OriginatingAddress2"].ToString());
            ObjOrgPostAdd.City = ReplaceReserverCharacters(drPFDetails["OriginatingCity"].ToString());
            ObjOrgPostAdd.StateProv = ReplaceReserverCharacters(drPFDetails["OriginatingState"].ToString());
            ObjOrgPostAdd.PostalCode = ReplaceReserverCharacters(drPFDetails["OriginatingZip"].ToString());
            ObjOrgPostAdd.Country = ReplaceReserverCharacters(drPFDetails["OriginatingCountry"].ToString());
            objOrgparty.Name = ObjOrgName;
            objOrgparty.PostAddr = ObjOrgPostAdd;
            //Add Originator party info payment record 
            objPmtRec.OrgnrParty = objOrgparty;
            //add check details
            Check objCheck = new Check();
            objCheck.ChkDocNum = drPFDetails["CheckDocNumber"].ToString();
            objCheck.ChkNum = drPFDetails["CheckNumber"].ToString();
            objPmtRec.Check = objCheck;

            // Receiver Party
            RcvrParty objRcvrParty = new RcvrParty();
            Name ObjRecvName = new Name();
            ObjRecvName.Name1 = ReplaceReserverCharacters(drPFDetails["VendorName"].ToString());
            if (!string.IsNullOrEmpty(drPFDetails["PayeeName1"].ToString().Trim()))
                ObjRecvName.Name2 = ReplaceReserverCharacters(drPFDetails["PayeeName1"].ToString());
            PostAddr ObjRecvPostAdd = new PostAddr();
            ObjRecvPostAdd.Addr1 = ReplaceReserverCharacters(drPFDetails["RemitAddress1"].ToString());
            if (!string.IsNullOrEmpty(drPFDetails["RemitAddress2"].ToString().Trim()))
                ObjRecvPostAdd.Addr2 = ReplaceReserverCharacters(drPFDetails["RemitAddress2"].ToString());
            ObjRecvPostAdd.City = ReplaceReserverCharacters(drPFDetails["RemitCity"].ToString());
            ObjRecvPostAdd.StateProv = ReplaceReserverCharacters(drPFDetails["RemitState"].ToString());
            ObjRecvPostAdd.PostalCode = ReplaceReserverCharacters(drPFDetails["RemitZip"].ToString());
            ObjRecvPostAdd.Country = ReplaceReserverCharacters(drPFDetails["RemitCountry"].ToString());
            objRcvrParty.PostAddr = ObjRecvPostAdd;
            objRcvrParty.Name = ObjRecvName;
            //Add Receiver party info payment record 
            objPmtRec.RcvrParty = objRcvrParty;
            // Originator Dept Account Info : Header
            OrgnrDepAcctID objOrgnrDepAcctID = new OrgnrDepAcctID();
            DepAcctID objOrgDepAcctID = new DepAcctID();
            objOrgDepAcctID.AcctTypeSpecified = true;
            objOrgDepAcctID.AcctID = dsPFXML.Tables[0].Rows[0]["BankAccountNumber"].ToString();
            if (!string.IsNullOrEmpty(drPFDetails["AccountType"].ToString().Trim()))
            {
                if (drPFDetails["AccountType"].ToString().ToUpper() == "D")
                    objOrgDepAcctID.AcctType = DepAcctIDAcctType.D;
                else if (drPFDetails["AccountType"].ToString().ToUpper() == "C")
                    objOrgDepAcctID.AcctType = DepAcctIDAcctType.C;
                else if (drPFDetails["AccountType"].ToString().ToUpper() == "G")
                    objOrgDepAcctID.AcctType = DepAcctIDAcctType.G;
            }
            objOrgDepAcctID.AcctCur = dsPFXML.Tables[0].Rows[0]["DusbursementAccountCurrency"].ToString();
            // Originator Dept Account Info : Bank Info
            BankInfo objOrgBankInfo = new BankInfo();
            objOrgBankInfo.BankIDTypeSpecified = true;
            objOrgBankInfo.Name = ReplaceReserverCharacters(dsPFXML.Tables[0].Rows[0]["BankName"].ToString());
            objOrgBankInfo.BankIDType = BankInfoBankIDType.ABA;
            objOrgBankInfo.BankID = dsPFXML.Tables[0].Rows[0]["BankRoutingNumber"].ToString();

            // Originator Dept Account Info : Bank Info : Ref Type 
            RefInfo[] objRefInfo = new RefInfo[1];
            objRefInfo[0] = new RefInfo();

            objOrgBankInfo.RefInfo = objRefInfo;
            //Add Originator Bank info to Originator Dept Acct Info
            objOrgDepAcctID.BankInfo = objOrgBankInfo;
            //Add DeptAcctID to Originator Dept Acct ID 
            objOrgnrDepAcctID.DepAcctID = objOrgDepAcctID;

            //Add Originator Dept Acct ID to Originator party
            objPmtRec.OrgnrDepAcctID = objOrgnrDepAcctID;

            //Add DeptAcctID to receiver Dept Acct ID to Receiver party
            objPmtRec.RcvrDepAcctID = EmptyReceiverPartyAcctDetails(isCheckExists, isAchExists);


            //add payment details
            PmtDetail objPmtDetail = new PmtDetail();
            PaymentFile.InvoiceInfo[] objInvoiceInfo = new PaymentFile.InvoiceInfo[drPF.Length];

            DataRow drInvoice = null;
            objPmtRec.CurAmt = 0;
            for (int k = 0; k < drPF.Length; k++)
            {
                drInvoice = drPF[k];
                objInvoiceInfo[k] = new PaymentFile.InvoiceInfo();
                objInvoiceInfo[k].NetCurAmtSpecified = true;
                objInvoiceInfo[k].InvoiceTypeSpecified = true;
                objInvoiceInfo[k].ServicePeriodFromSpecified = true;
                objInvoiceInfo[k].ServicePeriodToSpecified = true;
                objInvoiceInfo[k].InvocieDateSpecified = true;
                objInvoiceInfo[k].CustomerName = ReplaceReserverCharacters(drInvoice["ClientName"].ToString());
                objInvoiceInfo[k].AccountNumber = ReplaceReserverCharacters(drInvoice["AccountNumber"].ToString());
                objInvoiceInfo[k].NetCurAmt = Math.Round(Convert.ToDecimal(drInvoice["PaymentAmount"].ToString()), 2);
                objInvoiceInfo[k].InvoiceDate = Convert.ToDateTime(drInvoice["DueDate"].ToString());
                objInvoiceInfo[k].InvoiceNum = drInvoice["InvoiceNumber"].ToString();
                objInvoiceInfo[k].TransactionID = drInvoice["VectorInvoiceNo"].ToString();
                objInvoiceInfo[k].ServicePeriodFrom = Convert.ToDateTime(drInvoice["ServiceFromDate"].ToString());
                objInvoiceInfo[k].ServicePeriodTo = Convert.ToDateTime(drInvoice["ServiceToDate"].ToString());
                objInvoiceInfo[k].InvoiceType = PaymentFile.InvoiceInfoInvoiceType.IV; 
                objInvoiceInfo[k].PaymentId = ReplaceReserverCharacters(drInvoice["VectorPaymentId"].ToString());

                Note[] objNote = new Note[1];
                objNote[0] = new Note();
                objNote[0].NoteType = "INV";
                objNote[0].NoteText = "";
                objInvoiceInfo[k].Note = objNote;
                objPmtRec.CurAmt += Math.Round(Convert.ToDecimal(drInvoice["PaymentAmount"].ToString()), 2);
            }
            objPmtDetail.InvoiceInfo = objInvoiceInfo;
            objPmtRec.PmtDetail = objPmtDetail;


            // Payment ID and Account            
            objPmtRec.PmtID = drPFDetails["VectorPaymentId"].ToString();
            objPmtRec.CurCode = dsPFXML.Tables[0].Rows[0]["Currency"].ToString();
            objPmtRec.ValueDateSpecified = true;
            if (!String.IsNullOrEmpty(drPFDetails["SettlementDate"].ToString().Trim()))
                objPmtRec.ValueDate = Convert.ToDateTime(drPFDetails["SettlementDate"].ToString());
            else
                objPmtRec.ValueDate = DateTime.Now;
            return objPmtRec;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dsPFXML"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        private PmtRec GenerateSUCCTransactions(ref DataSet dsPFXML, ref int k, bool isCheckExists, bool isAchExists, string isVerificationEnabled)
        {
            string productName = (from c in dsPFXML.Tables[VectorConstants.Zero].AsEnumerable()
                                  select c.Field<string>("DisbursementAccountCode")).FirstOrDefault().ToString();

            PmtRec objPmtRec = new PmtRec();
            //Payment record header           
            objPmtRec.PmtMethod = PmtRecPmtMethod.CCR;
            objPmtRec.PmtCrDrSpecified = true;
            objPmtRec.PmtCrDr = PmtRecPmtCrDr.C;
            //Batch Id and Consumer Id
            IDInfo[] objIDInfo = new IDInfo[2];
            IDInfo objBatchInfo = new IDInfo();
            IDInfo objCustInfo = new IDInfo();
            objBatchInfo.IDType = Info.BatchID.ToString();
            objBatchInfo.ID = dsPFXML.Tables[0].Rows[0]["BatchId"].ToString();
            objCustInfo.IDType = Info.CustomerID.ToString();
            objCustInfo.ID = dsPFXML.Tables[0].Rows[0]["CustomerId"].ToString();
            objIDInfo[0] = objBatchInfo;
            objIDInfo[1] = objCustInfo;
            objPmtRec.IDInfo = objIDInfo;
            //Message
            Message objMessage = new Message();
            objMessage.MsgTypeSpecified = true;
            objMessage.MsgType = MessageMsgType.OBI;
            objMessage.MsgText = "Payee Invoice Ref";
            Message[] objMessages = new Message[1];
            objMessages[0] = objMessage;
            objPmtRec.Message = objMessages;
            // Originator Party
            OrgnrParty objOrgparty = new OrgnrParty();
            Name ObjOrgName = new Name();
            // Originator Party/Name
            ObjOrgName.Name1 = ReplaceReserverCharacters(dsPFXML.Tables[0].Rows[0]["DisbursementAccountName"].ToString());
            objOrgparty.Name = ObjOrgName;
            // Originator Party/Postal address
            PostAddr ObjOrgPostAdd = new PostAddr();
            ObjOrgPostAdd.Addr1 = ReplaceReserverCharacters(dsPFXML.Tables[3].Rows[k]["OriginatingAddress1"].ToString());
            ObjOrgPostAdd.Addr2 = ReplaceReserverCharacters(dsPFXML.Tables[3].Rows[k]["OriginatingAddress2"].ToString());
            ObjOrgPostAdd.City = ReplaceReserverCharacters(dsPFXML.Tables[3].Rows[k]["OriginatingCity"].ToString());
            ObjOrgPostAdd.StateProv = ReplaceReserverCharacters(dsPFXML.Tables[3].Rows[k]["OriginatingState"].ToString());
            ObjOrgPostAdd.PostalCode = ReplaceReserverCharacters(dsPFXML.Tables[3].Rows[k]["OriginatingZip"].ToString());
            ObjOrgPostAdd.Country = ReplaceReserverCharacters(dsPFXML.Tables[3].Rows[k]["OriginatingCountry"].ToString());
            objOrgparty.PostAddr = ObjOrgPostAdd;
            // Originator Party/contact info
            ContactInfo objContactInfo = new ContactInfo();
            objContactInfo.Name = ReplaceReserverCharacters(dsPFXML.Tables[0].Rows[0]["VectorContactName"].ToString());
            PhoneNum objPhoneNum = new PhoneNum();
            objPhoneNum.Phone = ReplaceReserverCharacters(dsPFXML.Tables[0].Rows[0]["Phone"].ToString());
            objContactInfo.PhoneNum = objPhoneNum;
            objContactInfo.EmailAddr = ReplaceReserverCharacters(dsPFXML.Tables[0].Rows[0]["ContactEmail"].ToString());
            objOrgparty.ContactInfo = objContactInfo;
            //Add Originator party info payment record 
            objPmtRec.OrgnrParty = objOrgparty;
            // Receiver Party:Name
            RcvrParty objRcvrParty = new RcvrParty();
            Name ObjRecvName = new Name();
            ObjRecvName.Name1 = ReplaceReserverCharacters(dsPFXML.Tables[3].Rows[k]["VendorName"].ToString()); 
            objRcvrParty.Name = ObjRecvName;
            //Ref into
            RefInfo[] objRefInfo = new RefInfo[1];

            RefInfo objRef = new RefInfo();
            if (StringManager.IsEqual(productName, "REFU"))
                objRef.RefID = dsPFXML.Tables[3].Rows[k]["HaulerRefId"].ToString();

            objRefInfo[0] = objRef;
            objRcvrParty.RefInfo = objRefInfo;
            PostAddr objPostAddr = new PostAddr();
                        
            objPostAddr.PostalCode = dsPFXML.Tables[3].Rows[k]["RemitZip"].ToString(); 

            objRcvrParty.PostAddr = objPostAddr;
            ContactInfo objRecContactInfo = new ContactInfo();                       
            objRecContactInfo.EmailAddr = ReplaceReserverCharacters(dsPFXML.Tables[3].Rows[k]["PCardEmail"].ToString()); 

            objRcvrParty.ContactInfo = objRecContactInfo;
            //Add Receiver party info payment record 
            objPmtRec.RcvrParty = objRcvrParty;

            // Originator Dept Account Info : Header
            OrgnrDepAcctID objOrgnrDepAcctID = new OrgnrDepAcctID();
            DepAcctID objRecDepAcctID = new DepAcctID();
            BankInfo objRecBankInfo = new BankInfo();
            objRecDepAcctID.BankInfo = objRecBankInfo;
            objRecDepAcctID.AcctID = dsPFXML.Tables[0].Rows[0]["MAN"].ToString().Trim();
            objOrgnrDepAcctID.DepAcctID = objRecDepAcctID;

            //Add Originator Dept Acct ID to Originator party
            objPmtRec.OrgnrDepAcctID = objOrgnrDepAcctID;


            //Add DeptAcctID to receiver Dept Acct ID to Receiver party
            objPmtRec.RcvrDepAcctID = EmptyReceiverPartyAcctDetails(isCheckExists, isAchExists);

            //div code
            PmtSuppCCR objPmtSuppCCR = new PmtSuppCCR();
            objPmtSuppCCR.Division = (object)dsPFXML.Tables[0].Rows[0]["DivCode"].ToString().Trim();
            objPmtRec.PmtSuppCCR = objPmtSuppCCR;

            //Payment Details 
            PmtDetail objPmtDetail = new PmtDetail();
            //Payment Details : invoice
            PaymentFile.InvoiceInfo[] objInvoiceInfo = new PaymentFile.InvoiceInfo[1];
            objInvoiceInfo[0] = new PaymentFile.InvoiceInfo();
            objInvoiceInfo[0].NetCurAmtSpecified = true;
            objInvoiceInfo[0].InvoiceTypeSpecified = true;
            objInvoiceInfo[0].ServicePeriodFromSpecified = true;
            objInvoiceInfo[0].ServicePeriodToSpecified = true;
            objInvoiceInfo[0].InvocieDateSpecified = true;
            objInvoiceInfo[0].CustomerName = ReplaceReserverCharacters(dsPFXML.Tables[3].Rows[k]["ClientName"].ToString());
            objInvoiceInfo[0].AccountNumber = ReplaceReserverCharacters(dsPFXML.Tables[3].Rows[k]["AccountNumber"].ToString());
            objInvoiceInfo[0].NetCurAmt = Math.Round(Convert.ToDecimal(dsPFXML.Tables[3].Rows[k]["PaymentAmount"].ToString()), 2);
            objInvoiceInfo[0].InvoiceDate = Convert.ToDateTime(dsPFXML.Tables[3].Rows[k]["DueDate"].ToString());
            objInvoiceInfo[0].InvoiceNum = ReplaceReserverCharacters(dsPFXML.Tables[3].Rows[k]["InvoiceNumber"].ToString());
            objInvoiceInfo[0].TransactionID = ReplaceReserverCharacters(dsPFXML.Tables[3].Rows[k]["VectorInvoiceNo"].ToString());
            objInvoiceInfo[0].ServicePeriodFrom = Convert.ToDateTime(dsPFXML.Tables[3].Rows[k]["ServiceFromDate"].ToString());
            objInvoiceInfo[0].ServicePeriodTo = Convert.ToDateTime(dsPFXML.Tables[3].Rows[k]["ServiceToDate"].ToString());
            objInvoiceInfo[0].InvoiceType = PaymentFile.InvoiceInfoInvoiceType.IV;

            if (StringManager.IsEqual(isVerificationEnabled, "true"))
                objInvoiceInfo[0].PaymentId = ReplaceReserverCharacters(dsPFXML.Tables[3].Rows[k]["VectorPaymentId"].ToString());

            POInfo[] objAchPOInfo = new POInfo[1];
            Note[] objACHNote = new Note[1];
            objACHNote[0] = new Note();
            objACHNote[0].NoteType = "INV";

            if (StringManager.IsEqual(productName, "REFU"))
                objACHNote[0].NoteText = "Account Number : " + ReplaceReserverCharacters(dsPFXML.Tables[3].Rows[k]["AccountNumber"].ToString());
            else
                objACHNote[0].NoteText = "";

            objInvoiceInfo[0].Note = objACHNote;
            objPmtDetail.InvoiceInfo = objInvoiceInfo;
            objPmtRec.PmtDetail = objPmtDetail;

            // Payment ID and Account 
            objPmtRec.PmtID = dsPFXML.Tables[3].Rows[k]["VectorPaymentId"].ToString();
            objPmtRec.CurAmt = Math.Round(Convert.ToDecimal(dsPFXML.Tables[3].Rows[k]["PaymentAmount"].ToString()), 2);
            objPmtRec.CurCode = dsPFXML.Tables[0].Rows[0]["Currency"].ToString();

            objPmtRec.ValueDateSpecified = true;
            if (!string.IsNullOrEmpty(dsPFXML.Tables[3].Rows[k]["SettlementDate"].ToString()))
                objPmtRec.ValueDate = Convert.ToDateTime(dsPFXML.Tables[3].Rows[k]["SettlementDate"].ToString());
            else
                objPmtRec.ValueDate = DateTime.Now;
            return objPmtRec;

        }

        /// <summary>
        /// ACH
        /// </summary>
        /// <param name="dsPFXML"></param>
        /// <param name="objPmtRec"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private PmtRec GenerateACHTransactions(ref DataSet dsPFXML, ref int i, string verifyPaymentEnabled)
        {

            PmtRec objPmtRec = new PmtRec();
            //Payment record header 
            objPmtRec.PmtFormat = PmtRecPmtFormat.CTX;
            objPmtRec.PmtMethod = PmtRecPmtMethod.DAC;
            objPmtRec.PmtCrDrSpecified = true;
            objPmtRec.PmtFormatSpecified = true;
            objPmtRec.PmtCrDr = PmtRecPmtCrDr.C;

            // Originator Party
            OrgnrParty objOrgparty = new OrgnrParty();
            Name ObjOrgName = new Name();
            ObjOrgName.Name1 = ReplaceReserverCharacters(dsPFXML.Tables[0].Rows[0]["CLIENT_NAME"].ToString());
            objOrgparty.Name = ObjOrgName;
            PostAddr ObjOrgPostAdd = new PostAddr();
            ObjOrgPostAdd.Addr1 = ReplaceReserverCharacters(dsPFXML.Tables[1].Rows[i]["OADDRESS1"].ToString());
            ObjOrgPostAdd.Addr2 = ReplaceReserverCharacters(dsPFXML.Tables[1].Rows[i]["OADDRESS2"].ToString());
            ObjOrgPostAdd.City = ReplaceReserverCharacters(dsPFXML.Tables[1].Rows[i]["OCITY"].ToString());
            ObjOrgPostAdd.StateProv = ReplaceReserverCharacters(dsPFXML.Tables[1].Rows[i]["OSTATE"].ToString());
            ObjOrgPostAdd.PostalCode = ReplaceReserverCharacters(dsPFXML.Tables[1].Rows[i]["OZIP"].ToString());
            ObjOrgPostAdd.Country = ReplaceReserverCharacters(dsPFXML.Tables[1].Rows[i]["OCOUNTRY"].ToString());
            objOrgparty.PostAddr = ObjOrgPostAdd;
            //Add Originator party info payment record 
            objPmtRec.OrgnrParty = objOrgparty;
            // Receiver Party
            RcvrParty objRcvrParty = new RcvrParty();
            Name ObjRecvName = new Name();
            ObjRecvName.Name1 = ReplaceReserverCharacters(dsPFXML.Tables[1].Rows[i]["PKRM_VNDR_NAME"].ToString());
            if (!string.IsNullOrEmpty(dsPFXML.Tables[1].Rows[i]["PKRM_VNDR_NAME1"].ToString().Trim()))
                ObjRecvName.Name2 = ReplaceReserverCharacters(dsPFXML.Tables[1].Rows[i]["PKRM_VNDR_NAME1"].ToString());

            objRcvrParty.Name = ObjRecvName;
            //Add Receiver party info payment record 
            objPmtRec.RcvrParty = objRcvrParty;


            // Originator Dept Account Info : Header
            OrgnrDepAcctID objOrgnrDepAcctID = new OrgnrDepAcctID();
            DepAcctID objOrgDepAcctID = new DepAcctID();
            objOrgDepAcctID.AcctID = ReplaceReserverCharacters(dsPFXML.Tables[0].Rows[0]["BANK_ACCT_NBR"].ToString());
            objOrgDepAcctID.AcctTypeSpecified = true;
            if (!string.IsNullOrEmpty(dsPFXML.Tables[0].Rows[0]["ACCOUNTTYPE"].ToString().Trim()))
            {
                if (dsPFXML.Tables[0].Rows[0]["ACCOUNTTYPE"].ToString().ToUpper() == "D")
                    objOrgDepAcctID.AcctType = DepAcctIDAcctType.D;
                else if (dsPFXML.Tables[0].Rows[0]["ACCOUNTTYPE"].ToString().ToUpper() == "G")
                    objOrgDepAcctID.AcctType = DepAcctIDAcctType.G;
                else if (dsPFXML.Tables[0].Rows[0]["ACCOUNTTYPE"].ToString().ToUpper() == "C")
                    objOrgDepAcctID.AcctType = DepAcctIDAcctType.C;
            }

            objOrgDepAcctID.AcctCur = ReplaceReserverCharacters(dsPFXML.Tables[0].Rows[0]["ACCOUNTCURRENCY"].ToString());
            // Originator Dept Account Info : Bank Info
            BankInfo objOrgBankInfo = new BankInfo();
            objOrgBankInfo.BankIDTypeSpecified = true;
            if (IsPaymentFieldRequired(dsPFXML, "BANK_NAME", dsPFXML.Tables[1].Rows[i]["RegularVendorName"].ToString()))
                objOrgBankInfo.Name = ReplaceReserverCharacters(dsPFXML.Tables[0].Rows[0]["BANK_NAME"].ToString());
            objOrgBankInfo.BankIDType = BankInfoBankIDType.ABA;
            objOrgBankInfo.BankID = ReplaceReserverCharacters(dsPFXML.Tables[0].Rows[0]["BANK_ROUTING_NBR"].ToString());
            // Originator Dept Account Info : Bank Info : Ref Type 
            RefInfo[] objRefInfo = new RefInfo[1];
            objRefInfo[0] = new RefInfo();
            objRefInfo[0].RefType = "ACH";
            if (IsPaymentFieldRequired(dsPFXML, "REFID", dsPFXML.Tables[1].Rows[i]["RegularVendorName"].ToString()))
                objRefInfo[0].RefID = ReplaceReserverCharacters(dsPFXML.Tables[0].Rows[0]["REFID"].ToString());
            objOrgBankInfo.RefInfo = objRefInfo;

            //Add Originator Bank info to Originator Dept Acct Info
            objOrgDepAcctID.BankInfo = objOrgBankInfo;
            //Add DeptAcctID to Originator Dept Acct ID 
            objOrgnrDepAcctID.DepAcctID = objOrgDepAcctID;
            //Add Originator Dept Acct ID to Originator party
            objPmtRec.OrgnrDepAcctID = objOrgnrDepAcctID;

            // Receiver Dept Account Info : Header
            RcvrDepAcctID objRceiverDepAcctID = new RcvrDepAcctID();
            DepAcctID objRecvDepAcctID = new DepAcctID();
            objRecvDepAcctID.AcctTypeSpecified = true;
            objRecvDepAcctID.AcctID = ReplaceReserverCharacters(dsPFXML.Tables[1].Rows[i]["VENDOR_NO"].ToString());
            objRecvDepAcctID.AcctType = DepAcctIDAcctType.D;
            objRecvDepAcctID.AcctCur = ReplaceReserverCharacters(dsPFXML.Tables[1].Rows[i]["ACCOUNTCURRENCY"].ToString());
            // Receiver Dept Account Info : Bank Info
            BankInfo objRecvBankInfo = new BankInfo();
            if (IsPaymentFieldRequired(dsPFXML, "VENDOR_BANK_NAME", dsPFXML.Tables[1].Rows[i]["RegularVendorName"].ToString()))
                objRecvBankInfo.Name = ReplaceReserverCharacters(dsPFXML.Tables[1].Rows[i]["VENDOR_BANK_NAME"].ToString());
            objRecvBankInfo.BankIDTypeSpecified = true;
            objRecvBankInfo.BankIDType = BankInfoBankIDType.ABA;
            objRecvBankInfo.BankID = ReplaceReserverCharacters(dsPFXML.Tables[1].Rows[i]["VENDOR_ABA_ROUTING_NO"].ToString());

            // add receiver bank info
            objRecvDepAcctID.BankInfo = objRecvBankInfo;
            //Add DeptAcctID to Receiver Dept Acct ID 
            objRceiverDepAcctID.DepAcctID = objRecvDepAcctID;
            //Add DeptAcctID to receiver Dept Acct ID to Receiver party
            objPmtRec.RcvrDepAcctID = objRceiverDepAcctID;

            //// Receiver Dept Account Info : Postal Address
            //add payment details
            if (dsPFXML.Tables[0].Rows[0]["PF_TYPE"].ToString().ToUpper() != "PENNY TEST")
            {
                PmtDetail objACHPmtDetail = new PmtDetail();
                PaymentFile.InvoiceInfo[] objACHInvoiceInfo = new PaymentFile.InvoiceInfo[1];
                objACHInvoiceInfo[0] = new PaymentFile.InvoiceInfo();
                objACHInvoiceInfo[0].NetCurAmtSpecified = true;
                objACHInvoiceInfo[0].InvoiceTypeSpecified = true;
                objACHInvoiceInfo[0].ServicePeriodFromSpecified = true;
                objACHInvoiceInfo[0].ServicePeriodToSpecified = true;
                objACHInvoiceInfo[0].InvocieDateSpecified = true;
                objACHInvoiceInfo[0].CustomerName = ReplaceReserverCharacters(dsPFXML.Tables[1].Rows[i]["ClientName"].ToString());
                objACHInvoiceInfo[0].AccountNumber = ReplaceReserverCharacters(dsPFXML.Tables[1].Rows[i]["AccountNumber"].ToString());
                objACHInvoiceInfo[0].NetCurAmt = Math.Round(Convert.ToDecimal(dsPFXML.Tables[1].Rows[i]["PaymentAmount"].ToString()), 2);
                objACHInvoiceInfo[0].InvoiceDate = Convert.ToDateTime(dsPFXML.Tables[1].Rows[i]["DueDate"].ToString());
                //Replace transaction id with Account number for proper CTX details
                objACHInvoiceInfo[0].InvoiceNum = dsPFXML.Tables[1].Rows[i]["InvoiceNumber"].ToString();
                objACHInvoiceInfo[0].TransactionID = dsPFXML.Tables[1].Rows[i]["AccountNumber"].ToString();
                objACHInvoiceInfo[0].ServicePeriodFrom = Convert.ToDateTime(dsPFXML.Tables[1].Rows[i]["ServiceFromDate"].ToString());
                objACHInvoiceInfo[0].ServicePeriodTo = Convert.ToDateTime(dsPFXML.Tables[1].Rows[i]["ServiceToDate"].ToString());
                objACHInvoiceInfo[0].InvoiceType = PaymentFile.InvoiceInfoInvoiceType.IV;

                if (StringManager.IsEqual(verifyPaymentEnabled, "true"))
                    objACHInvoiceInfo[0].PaymentId = ReplaceReserverCharacters(dsPFXML.Tables[1].Rows[i]["VectorPaymentId"].ToString());

                POInfo[] objAchPOInfo = new POInfo[1];
                Note[] objACHNote = new Note[1];
                objACHNote[0] = new Note();
                objACHNote[0].NoteType = "INV";
                objACHNote[0].NoteText = "";
                objACHInvoiceInfo[0].Note = objACHNote;
                objACHPmtDetail.InvoiceInfo = objACHInvoiceInfo;
                objPmtRec.PmtDetail = objACHPmtDetail;
            }

            // Payment ID and Account
            objPmtRec.PmtID = dsPFXML.Tables[1].Rows[i]["VectorPaymentId"].ToString();
            objPmtRec.CurAmt = Math.Round(Convert.ToDecimal(dsPFXML.Tables[1].Rows[i]["PaymentAmount"].ToString()), 2);
            objPmtRec.CurCode = ReplaceReserverCharacters(dsPFXML.Tables[0].Rows[0]["AccountCurrencyType"].ToString());
            objPmtRec.ValueDateSpecified = true;
            if (!string.IsNullOrEmpty(dsPFXML.Tables[1].Rows[i]["SettlementDate"].ToString()))
                objPmtRec.ValueDate = Convert.ToDateTime(dsPFXML.Tables[1].Rows[i]["SettlementDate"].ToString());
            else
                objPmtRec.ValueDate = DateTime.Now;
            return objPmtRec;

        }

        /// <summary>
        /// Replace reserve character ( > < & ' " \ * ~ ^ `)
        /// </summary> 
        /// <returns>String</returns>
        public string ReplaceReserverCharacters(string Value)
        {
            if (!string.IsNullOrEmpty(Value))
            {
                Value = Value.Replace("&", "&amp;");
                Value = Value.Replace(">", "&gt;");
                Value = Value.Replace("<", "&lt;");
                Value = Value.Replace("'", string.Empty);
                Value = Value.Replace(@"""", string.Empty);
                Value = Value.Replace(@"\", string.Empty);
                Value = Value.Replace(@"*", string.Empty);
                Value = Value.Replace(@"^", string.Empty);
                Value = Value.Replace(@"~", string.Empty);
                Value = Value.Replace(@"`", string.Empty);
            }
            return Value;

        }

        /// <summary>
        /// Update account details
        /// </summary>
        /// <param name="dsPFXML"></param>
        private void DecryptAccountDetails(ref DataSet dsPFXML)
        {
            //using (PasswordBO objPasswordBO = new PasswordBO())
            //{
            //    foreach (DataRow dr in dsPFXML.Tables[0].Rows)
            //    {
            //        dr.AcceptChanges();
            //        dr.BeginEdit();
            //        if (!string.IsNullOrEmpty(dr["BANK_ACCT_NBR"].ToString()))
            //            dr["BANK_ACCT_NBR"] = ReplaceReserverCharacters(objPasswordBO.psDecrypt(dr["BANK_ACCT_NBR"].ToString()));
            //        if (!string.IsNullOrEmpty(dr["BANK_ROUTING_NBR"].ToString()))
            //            dr["BANK_ROUTING_NBR"] = ReplaceReserverCharacters(objPasswordBO.psDecrypt(dr["BANK_ROUTING_NBR"].ToString()));
            //        if (!string.IsNullOrEmpty(dr["MAN"].ToString()))
            //            dr["MAN"] = ReplaceReserverCharacters(objPasswordBO.psDecrypt(dr["MAN"].ToString()));
            //        dr.EndEdit();
            //    }
            //    if (dsPFXML.Tables[1].Rows.Count > 0)
            //    {
            //        foreach (DataRow dr in dsPFXML.Tables[1].Rows)
            //        {
            //            dr.AcceptChanges();
            //            dr.BeginEdit();
            //            if (!string.IsNullOrEmpty(dr["VENDOR_NO"].ToString()))
            //                dr["VENDOR_NO"] = objPasswordBO.psDecrypt(dr["VENDOR_NO"].ToString());
            //            if (!string.IsNullOrEmpty(dr["VENDOR_ABA_ROUTING_NO"].ToString()))
            //                dr["VENDOR_ABA_ROUTING_NO"] = objPasswordBO.psDecrypt(dr["VENDOR_ABA_ROUTING_NO"].ToString());
            //            dr.EndEdit();
            //        }
            //    }
            //}
        }

        /// <summary>
        /// Validate Payment File 
        /// </summary>
        /// <returns>String</returns>
        public string ValidatePaymentFileXMl(ref DataSet dsPFXML)
        {
            //Table 0 Pf header details
            //table 1 Ach
            //Table 2 Check
            //Table 3 SUCC

            using (PaymentFileValidationsBL ObjPFV = new PaymentFileValidationsBL())
            {
                StringBuilder errorMessage = new StringBuilder(string.Empty);
                DataTable dtCheckNumbers = null;
                int CheckCount = 0;

                try
                {
                    //Dataset should contain 4 tables 
                    if (dsPFXML.Tables.Count != 5)
                        errorMessage.Append(@" Invalid Data \n"); ;
                    if (dsPFXML.Tables.Count > 2)
                        if (dsPFXML.Tables[0].Rows.Count > 0)
                        {
                            /**************************************************************************
                             *                 File Header  
                             ***************************************************************************/

                            //File Name 
                            errorMessage.Append(ObjPFV.ValidatingPFDetails(dsPFXML.Tables[0].Rows[0]["PayFileName"].ToString(), "FILE CONTROL NUMBER"));

                            //Total amount
                            errorMessage.Append(ObjPFV.ValidatingPFDetails(dsPFXML.Tables[0].Rows[0]["PaymentAmount"].ToString(), "File Payment Amount"));

                            //Total amount
                            errorMessage.Append(ObjPFV.ValidatingPFDetails(dsPFXML.Tables[0].Rows[0]["PayFileDate"].ToString(), "Payfile Date"));

                        }
                        else { errorMessage.Append(@" Payment file header info is required \n"); }

                    /**************************************************************************
                            *           Validating Originator Party(company details)
                    ***************************************************************************/
                    StringBuilder errorMessageForOrg = new StringBuilder(string.Empty);
                    if (dsPFXML.Tables[0].Rows.Count > 0)
                    {
                        //Company ID 
                        errorMessage.Append(ObjPFV.ValidatingPFDetails(dsPFXML.Tables[0].Rows[0]["DisbursementAccountCode"].ToString(), "COMPANY ID"));
                        // Originator Party / Name
                        errorMessageForOrg.Append(ObjPFV.ValidatingOriginatorDetails(dsPFXML.Tables[0].Rows[0]["DisbursementAccountName"].ToString().Trim(), "Disbursement Client Name"));

                        //Originatior Bank Routing Number 
                        errorMessageForOrg.Append(ObjPFV.ValidatingOriginatorDetails(dsPFXML.Tables[0].Rows[0]["BankRoutingNumber"].ToString(), "ABA ROUTING NUMBER", dsPFXML.Tables[0].Rows[0]["DusbursementAccountCurrency"].ToString()));
                        //Originatior Bank ZBA/Account Number 
                        errorMessageForOrg.Append(ObjPFV.ValidatingOriginatorDetails(dsPFXML.Tables[0].Rows[0]["BankAccountNumber"].ToString().Trim(), "ZBA DISBURSEMENT"));
                        //Account Currency
                        errorMessageForOrg.Append(ObjPFV.ValidatingOriginatorDetails(dsPFXML.Tables[0].Rows[0]["DusbursementAccountCurrency"].ToString(), "Disbursement Account Currency"));
                        //Account TYPE
                        errorMessageForOrg.Append(ObjPFV.ValidatingOriginatorDetails(dsPFXML.Tables[0].Rows[0]["AccountType"].ToString(), "ACCOUNT TYPE"));

                        errorMessage.Append(ObjPFV.ValidatingOriginatorDetails(dsPFXML.Tables[0].Rows[0]["ReferenceId"].ToString(), "REF ID"));
                        // for SUCC
                        if (dsPFXML.Tables[3].Rows.Count > 0)
                        {
                            //Consumer Id
                            errorMessage.Append(ObjPFV.ValidatingOriginatorDetails(dsPFXML.Tables[0].Rows[0]["CustomerId"].ToString(), "CUSTID"));
                            //div Code
                            errorMessage.Append(ObjPFV.ValidatingOriginatorDetails(dsPFXML.Tables[0].Rows[0]["DivCode"].ToString(), "DIVCODE"));
                            //MAN
                            errorMessage.Append(ObjPFV.ValidatingOriginatorDetails(dsPFXML.Tables[0].Rows[0]["MAN"].ToString(), "MAN"));
                            //Contact Name
                            errorMessage.Append(ObjPFV.ValidatingOriginatorDetails(dsPFXML.Tables[0].Rows[0]["VectorContactName"].ToString(), "CONTACTNAME"));
                            //Contact Email
                            errorMessage.Append(ObjPFV.ValidatingOriginatorDetails(dsPFXML.Tables[0].Rows[0]["ContactEmail"].ToString(), "CONTACTEMAIL"));
                            //Contact Phone Nbr
                            errorMessage.Append(ObjPFV.ValidatingOriginatorDetails(dsPFXML.Tables[0].Rows[0]["Phone"].ToString(), "CONTACTPHONE"));
                            //Batch Id
                            errorMessage.Append(ObjPFV.ValidatingOriginatorDetails(dsPFXML.Tables[0].Rows[0]["BatchId"].ToString(), "BATCHID"));

                        }

                        if (!string.IsNullOrEmpty(errorMessageForOrg.ToString()))
                        {
                            errorMessage.Append(Environment.NewLine + "Originator Details :" + Environment.NewLine + errorMessageForOrg.ToString());
                        }

                    }
                    else { errorMessage.Append(@" Originator Details are required \n"); }


                    if (dsPFXML.Tables[1].Rows.Count > 0 || dsPFXML.Tables[2].Rows.Count > 0 || dsPFXML.Tables[3].Rows.Count > 0)
                    {
                        int achCount = dsPFXML.Tables[1].Rows.Count;
                        int chkCount = 0;
                        int SuccCount = dsPFXML.Tables[3].Rows.Count;


                        if (dsPFXML.Tables[2].Rows.Count > 0)
                        {
                            DataView dvCheckNumbers = new DataView(dsPFXML.Tables[2]);
                            dtCheckNumbers = dvCheckNumbers.ToTable(true, "CHECKNUMBER");
                            CheckCount = dtCheckNumbers.Rows.Count;
                        }

                        //Can be multiple Payment records
                        // run a loop for payment record 
                        //Payment File Transaction Count 
                        errorMessage.Append(ObjPFV.ValidatingPFDetails(Convert.ToString(achCount + chkCount + SuccCount), "PayFile TRANSACTION COUNT"));
                    }


                    /**************************************************************************
                    *                  Adding ACH Paymnet Records  Orignator and Receiver 
                    ***************************************************************************/
                    StringBuilder errorMessageForAch = new StringBuilder(string.Empty);
                    if (dsPFXML.Tables[1].Rows.Count > 0)
                        for (int i = 0; i < dsPFXML.Tables[1].Rows.Count; i++)
                        {

                            //Postal Address
                            errorMessageForOrg.Append(ObjPFV.ValidatingOriginatorDetails(dsPFXML.Tables[1].Rows[i]["OriginatingAddress1"].ToString().Trim(), "ADDRESS1"));
                            errorMessageForOrg.Append(ObjPFV.ValidatingOriginatorDetails(dsPFXML.Tables[1].Rows[i]["OriginatingAddress2"].ToString(), "ADDRESS2"));
                            errorMessageForOrg.Append(ObjPFV.ValidatingOriginatorDetails(dsPFXML.Tables[1].Rows[i]["OriginatingCity"].ToString(), "CITY"));
                            errorMessageForOrg.Append(ObjPFV.ValidatingOriginatorDetails(dsPFXML.Tables[1].Rows[i]["OriginatingState"].ToString(), "STATE"));
                            errorMessageForOrg.Append(ObjPFV.ValidatingOriginatorDetails(dsPFXML.Tables[1].Rows[i]["OriginatingZip"].ToString(), "ZIP"));
                            errorMessageForOrg.Append(ObjPFV.ValidatingOriginatorDetails(dsPFXML.Tables[1].Rows[i]["OriginatingCountry"].ToString(), "COUNTRY"));

                            //Account Type 
                            errorMessageForAch.Append(ObjPFV.ValidatingReceiverDetails(dsPFXML.Tables[1].Rows[i]["AccountType"].ToString(), "ACCOUNT TYPE"));
                            // Receiver Party
                            //Receiver Name
                            errorMessageForAch.Append(ObjPFV.ValidatingReceiverDetails(dsPFXML.Tables[1].Rows[i]["VendorName"].ToString(), "VENDOR PAYEE NAME"));
                            errorMessageForAch.Append(ObjPFV.ValidatingReceiverDetails(dsPFXML.Tables[1].Rows[i]["PayeeName1"].ToString(), "VENDOR PAYEE NAME1"));


                            //Receiver ABA Routing Number 
                            errorMessageForAch.Append(ObjPFV.ValidatingReceiverDetails(dsPFXML.Tables[1].Rows[i]["RoutingNumber"].ToString(), "VENDOR ROUTING NUMBER", dsPFXML.Tables[1].Rows[i]["ACCOUNTCURRENCY"].ToString()));


                            // Receiver Account Number 
                            errorMessageForAch.Append(ObjPFV.ValidatingReceiverDetails(dsPFXML.Tables[1].Rows[i]["VendorNo"].ToString(), "VENDOR ACCT NUMBER"));
                            // Receiver Dept Account Info : Bank Name
                            if (IsPaymentFieldRequired(dsPFXML, "VENDOR_BANK_NAME", dsPFXML.Tables[1].Rows[i]["RegularVendorName"].ToString()))
                            {
                                // Originator Dept Account Info : Bank Info
                                errorMessageForOrg.Append(ObjPFV.ValidatingOriginatorDetails(dsPFXML.Tables[0].Rows[0]["BANK_NAME"].ToString(), "BANK NAME"));
                                errorMessageForAch.Append(ObjPFV.ValidatingReceiverDetails(dsPFXML.Tables[1].Rows[i]["BankName"].ToString(), "BANK NAME"));
                            }
                            // Receiver Dept Account Info : Account currency
                            errorMessageForAch.Append(ObjPFV.ValidatingReceiverDetails(dsPFXML.Tables[1].Rows[i]["AccountCurrencyType"].ToString(), "ACCOUNT CURRENCY"));


                            /**************************************************************************
                            *                 Payment Details / Invoice information
                            ***************************************************************************/
                            //add payment details
                            //if (dsPFXML.Tables[0].Rows[0]["PF_TYPE"].ToString().ToUpper() != "PENNY TEST")
                            //{
                                errorMessageForAch.Append(ObjPFV.ValidatingInvoiceDetails(dsPFXML.Tables[1].Rows[i]["ClientName"].ToString(), "CLIENT NAME"));
                                errorMessageForAch.Append(ObjPFV.ValidatingInvoiceDetails(dsPFXML.Tables[1].Rows[i]["AccountNumber"].ToString(), "ACCOUNT NUMBER"));
                                errorMessageForAch.Append(ObjPFV.ValidatingInvoiceDetails(dsPFXML.Tables[1].Rows[i]["PaymentAmount"].ToString(), "PAY BY AMOUNT"));
                                errorMessageForAch.Append(ObjPFV.ValidatingInvoiceDetails(dsPFXML.Tables[1].Rows[i]["DueDate"].ToString(), "INVOICE DATE"));
                                errorMessageForAch.Append(ObjPFV.ValidatingInvoiceDetails(dsPFXML.Tables[1].Rows[i]["VectorInvoiceNo"].ToString(), "TRANSACTION ID"));
                                errorMessageForAch.Append(ObjPFV.ValidatingInvoiceDetails(dsPFXML.Tables[1].Rows[i]["ServiceFromDate"].ToString(), "SERVICE PERIOD FROM"));
                                errorMessageForAch.Append(ObjPFV.ValidatingInvoiceDetails(dsPFXML.Tables[1].Rows[i]["ServiceToDate"].ToString(), "SERVICE PERIOD TO"));
                            //}

                            // Payment ID and Account 
                            errorMessageForAch.Append(ObjPFV.ValidatingInvoiceDetails(dsPFXML.Tables[1].Rows[i]["VectorPaymentId"].ToString(), "TRANSACTION ID"));
                            errorMessageForAch.Append(ObjPFV.ValidatingInvoiceDetails(dsPFXML.Tables[1].Rows[i]["PaymentAmount"].ToString(), "PAY BY AMOUNT"));
                            errorMessageForAch.Append(ObjPFV.ValidatingReceiverDetails(dsPFXML.Tables[0].Rows[0]["Currency"].ToString(), "ACCOUNT CURRENCY"));
                            errorMessageForAch.Append(ObjPFV.ValidatingInvoiceDetails(dsPFXML.Tables[1].Rows[i]["SettlementDate"].ToString(), "SETTLEMENT DATE"));
                            if (!string.IsNullOrEmpty(errorMessageForAch.ToString()))
                            {
                                errorMessage.Append(Environment.NewLine + "Transaction Id :" + dsPFXML.Tables[1].Rows[i]["VectorPaymentId"].ToString() + Environment.NewLine + errorMessageForAch.ToString());
                                errorMessageForAch.Clear();
                            }

                        }  //End of Payment Record Loop


                    /**************************************************************************
                             *                  Adding check Payment Records  
                     ***************************************************************************/
                    StringBuilder errorMessageForChk = new StringBuilder(String.Empty);
                    if (dsPFXML.Tables[2].Rows.Count > 0)
                        for (int j = 0; j < CheckCount; j++)
                        {
                            DataRow[] drPF = dsPFXML.Tables[2].Select("CHECKNUMBER = '" + dtCheckNumbers.Rows[j]["CheckNumber"].ToString() + "'");
                            DataRow drPFDetails = drPF[0];


                            //Postal Address
                            errorMessageForOrg.Append(ObjPFV.ValidatingOriginatorDetails(drPFDetails["OriginatingAddress1"].ToString().Trim(), "ADDRESS1"));
                            errorMessageForOrg.Append(ObjPFV.ValidatingOriginatorDetails(drPFDetails["OriginatingAddress2"].ToString(), "ADDRESS2"));
                            errorMessageForOrg.Append(ObjPFV.ValidatingOriginatorDetails(drPFDetails["OriginatingCity"].ToString(), "CITY"));
                            errorMessageForOrg.Append(ObjPFV.ValidatingOriginatorDetails(drPFDetails["OriginatingState"].ToString(), "STATE"));
                            errorMessageForOrg.Append(ObjPFV.ValidatingOriginatorDetails(drPFDetails["OriginatingCountry"].ToString(), "ZIP"));
                            errorMessageForOrg.Append(ObjPFV.ValidatingOriginatorDetails(drPFDetails["OriginatingZip"].ToString(), "COUNTRY"));

                            // Check Detail Validations
                            errorMessageForChk.Append(ObjPFV.ValidatingCheckDetails(drPFDetails["CheckDocNumber"].ToString(), "CHECK DOC NUMBER"));

                            //Added This for Check If the Check NUmber must be 8 Digit if CAD transaction exists.(approved By Mahesh)
                            errorMessageForChk.Append(ObjPFV.ValidatingCheckDetails(drPFDetails["CheckNumber"].ToString(), "CHECK NUMBER", dsPFXML.Tables[0].Rows[0]["Currency"].ToString()));

                            errorMessageForChk.Append(ObjPFV.ValidatingCheckDetails(drPFDetails["DeliveryMethod"].ToString(), "DELIVERY METHOD"));
                            // Receiver Party
                            errorMessageForChk.Append(ObjPFV.ValidatingReceiverDetails(drPFDetails["VendorName"].ToString(), "VENDOR PAYEE NAME"));
                            errorMessageForChk.Append(ObjPFV.ValidatingReceiverDetails(drPFDetails["PayeeName1"].ToString(), "VENDOR PAYEE NAME1"));

                            //Postal Address
                            errorMessageForChk.Append(ObjPFV.ValidatingReceiverDetails(drPFDetails["RemitAddress1"].ToString().Trim(), "ADDRESS1"));
                            errorMessageForChk.Append(ObjPFV.ValidatingReceiverDetails(drPFDetails["RemitAddress2"].ToString(), "ADDRESS2"));
                            errorMessageForChk.Append(ObjPFV.ValidatingReceiverDetails(drPFDetails["RemitCity"].ToString(), "CITY"));
                            errorMessageForChk.Append(ObjPFV.ValidatingReceiverDetails(drPFDetails["RemitState"].ToString(), "STATE"));
                            errorMessageForChk.Append(ObjPFV.ValidatingReceiverDetails(drPFDetails["RemitZip"].ToString(), "ZIP"));
                            errorMessageForChk.Append(ObjPFV.ValidatingReceiverDetails(drPFDetails["RemitCountry"].ToString(), "COUNTRY"));
                            errorMessageForChk.Append(ObjPFV.ValidatingReceiverDetails(drPFDetails["AccountType"].ToString(), "ACCOUNT TYPE"));

                            // Payment ID and Account 
                            errorMessageForChk.Append(ObjPFV.ValidatingInvoiceDetails(drPFDetails["VectorPaymentId"].ToString(), "TRANSACTION ID"));
                            errorMessageForChk.Append(ObjPFV.ValidatingInvoiceDetails(drPFDetails["PaymentAmount"].ToString(), "PAY BY AMOUNT"));
                            errorMessageForChk.Append(ObjPFV.ValidatingInvoiceDetails(drPFDetails["SettlementDate"].ToString(), "SETTLEMENT DATE"));
                            errorMessageForChk.Append(ObjPFV.ValidatingReceiverDetails(dsPFXML.Tables[0].Rows[0]["Currency"].ToString(), "ACCOUNT CURRENCY"));
                            if (!string.IsNullOrEmpty(errorMessageForChk.ToString()))
                            {
                                errorMessage.Append(Environment.NewLine + "Transaction Id :" + drPFDetails["VectorPaymentId"].ToString() + Environment.NewLine + errorMessageForChk.ToString());
                                errorMessageForChk.Clear();
                            }

                            StringBuilder errorMessageForInvoice = new StringBuilder(string.Empty);
                            for (int k = 0; k < drPF.Length; k++)
                            {
                                errorMessageForInvoice.Append(ObjPFV.ValidatingInvoiceDetails(drPF[k]["AccountNumber"].ToString(), "ACCOUNT NUMBER"));
                                errorMessageForInvoice.Append(ObjPFV.ValidatingInvoiceDetails(drPF[k]["PaymentAmount"].ToString(), "PAY BY AMOUNT"));
                                errorMessageForInvoice.Append(ObjPFV.ValidatingInvoiceDetails(drPF[k]["DueDate"].ToString(), "INVOICE DATE"));
                                errorMessageForInvoice.Append(ObjPFV.ValidatingInvoiceDetails(drPF[k]["VectorInvoiceNo"].ToString(), "TRANSACTION ID"));
                                errorMessageForInvoice.Append(ObjPFV.ValidatingInvoiceDetails(drPF[k]["ServiceFromDate"].ToString(), "SERVICE PERIOD FROM"));
                                errorMessageForInvoice.Append(ObjPFV.ValidatingInvoiceDetails(drPF[k]["ServiceToDate"].ToString(), "SERVICE PERIOD TO"));

                                if (!string.IsNullOrEmpty(errorMessageForInvoice.ToString()))
                                {
                                    errorMessage.Append(Environment.NewLine + "Transaction Id :" + drPF[k]["VectorPaymentId"].ToString() + Environment.NewLine + errorMessageForInvoice.ToString());
                                    errorMessageForInvoice.Clear();

                                }
                            }
                        }

                    //Validate SUCC Details
                    StringBuilder errorMessageForSucc = new StringBuilder(String.Empty);
                    if (dsPFXML.Tables[3].Rows.Count > 0)
                        for (int i = 0; i < dsPFXML.Tables[3].Rows.Count; i++)
                        {
                            //Postal Address
                            errorMessageForOrg.Append(ObjPFV.ValidatingOriginatorDetails(dsPFXML.Tables[3].Rows[i]["OriginatingAddress1"].ToString().Trim(), "ADDRESS1"));
                            errorMessageForOrg.Append(ObjPFV.ValidatingOriginatorDetails(dsPFXML.Tables[3].Rows[i]["OriginatingAddress2"].ToString(), "ADDRESS2"));
                            errorMessageForOrg.Append(ObjPFV.ValidatingOriginatorDetails(dsPFXML.Tables[3].Rows[i]["OriginatingCity"].ToString(), "CITY"));
                            errorMessageForOrg.Append(ObjPFV.ValidatingOriginatorDetails(dsPFXML.Tables[3].Rows[i]["OriginatingState"].ToString(), "STATE"));
                            errorMessageForOrg.Append(ObjPFV.ValidatingOriginatorDetails(dsPFXML.Tables[3].Rows[i]["OriginatingZip"].ToString(), "ZIP"));
                            errorMessageForOrg.Append(ObjPFV.ValidatingOriginatorDetails(dsPFXML.Tables[3].Rows[i]["OriginatingCountry"].ToString(), "COUNTRY"));
                            //Rev Party Name
                            errorMessageForSucc.Append(ObjPFV.ValidatingReceiverDetails(dsPFXML.Tables[3].Rows[i]["VendorName"].ToString(), "VENDOR PAYEE NAME SUCC"));
                            //Invoice Details 
                            errorMessageForSucc.Append(ObjPFV.ValidatingInvoiceDetails(dsPFXML.Tables[3].Rows[i]["ClientName"].ToString(), "CLIENT NAME"));
                            errorMessageForSucc.Append(ObjPFV.ValidatingInvoiceDetails(dsPFXML.Tables[3].Rows[i]["AccountNumber"].ToString(), "ACCOUNT NUMBER"));
                            errorMessageForSucc.Append(ObjPFV.ValidatingInvoiceDetails(dsPFXML.Tables[3].Rows[i]["PaymentAmount"].ToString(), "PAY BY AMOUNT"));
                            errorMessageForSucc.Append(ObjPFV.ValidatingInvoiceDetails(dsPFXML.Tables[3].Rows[i]["DueDate"].ToString(), "INVOICE DATE"));
                            errorMessageForSucc.Append(ObjPFV.ValidatingInvoiceDetails(dsPFXML.Tables[3].Rows[i]["VectorInvoiceNo"].ToString(), "TRANSACTION ID"));
                            errorMessageForSucc.Append(ObjPFV.ValidatingInvoiceDetails(dsPFXML.Tables[3].Rows[i]["ServiceFromDate"].ToString(), "SERVICE PERIOD FROM"));
                            errorMessageForSucc.Append(ObjPFV.ValidatingInvoiceDetails(dsPFXML.Tables[3].Rows[i]["ServiceToDate"].ToString(), "SERVICE PERIOD TO"));
                            //Pymt Details                      
                            errorMessageForSucc.Append(ObjPFV.ValidatingInvoiceDetails(dsPFXML.Tables[3].Rows[i]["VectorPaymentId"].ToString(), "TRANSACTION ID"));
                            errorMessageForSucc.Append(ObjPFV.ValidatingInvoiceDetails(dsPFXML.Tables[3].Rows[i]["PaymentAmount"].ToString(), "PAY BY AMOUNT"));
                            errorMessageForSucc.Append(ObjPFV.ValidatingInvoiceDetails(dsPFXML.Tables[3].Rows[i]["SettlementDate"].ToString(), "SETTLEMENT DATE"));
                            errorMessageForSucc.Append(ObjPFV.ValidatingReceiverDetails(dsPFXML.Tables[0].Rows[0]["Currency"].ToString(), "ACCOUNT CURRENCY"));
                            //Bill Zip Code
                            errorMessageForSucc.Append(ObjPFV.ValidatingReceiverDetails(dsPFXML.Tables[3].Rows[i]["OriginatingZip"].ToString(), "BILLINGZIP"));
                            if (!string.IsNullOrEmpty(errorMessageForSucc.ToString()))
                            {
                                errorMessage.Append(Environment.NewLine + "Transaction Id :" + dsPFXML.Tables[3].Rows[i]["VectorPaymentId"].ToString() + Environment.NewLine + errorMessageForSucc.ToString());
                                errorMessageForSucc.Clear();
                            }

                        }
                    ///End of Payment Record Loop
                }
                catch (Exception)
                {
                    errorMessage.Append(" Fail ");
                }

                return errorMessage.ToString();

            }

        }

        private bool IsPaymentFieldRequired(DataSet dsPFXML, string columnName, string vendorName = "")
        {
            bool result = true;

            if (!DataManager.IsNullOrEmptyDataTable(dsPFXML.Tables[4]))
            {
                DataTable data = dsPFXML.Tables[4];

                var query = (from c in data.AsEnumerable()
                             where (c.Field<string>("VendorName") == null ? (string.Empty).Contains(vendorName) : StringManager.IsEqual(c.Field<string>("VendorName").ToUpper().Trim(), vendorName.ToUpper()))
                             && (c.Field<string>("FieldName") == null ? (string.Empty).Contains(columnName) : StringManager.IsEqual(c.Field<string>("FieldName").ToUpper().Trim(), columnName.ToUpper()))
                             select c).AsDataView();
                if (query != null && query.ToTable().Rows.Count > 0)
                    result = false;
            }

            return result;
        }

        private string SendMailOnVerifyOfFS(string PfName, ref DataTable dtMN, string proadminUrl, string userName)
        {
            if (!DataManager.IsNullOrEmptyDataTable(dtMN))
            {
                foreach (DataRow drEmailDetails in dtMN.Rows)
                {   // Store the result mesage in variables
                    return SendInvoiceToClient(drEmailDetails, PfName, proadminUrl, userName);
                }
            }
            else
            {
                return "Unable to send Payment Notification - Generation < br > Please create the Payment Notification -Generation in Manage Notification.";
            }
            return string.Empty;
        }
        private string SendInvoiceToClient(DataRow drEmail, string PfName, string proadminUrl, string userId)
        {

            //using (ProAdminCommon objCommon = new ProAdminCommon())
            //{

            //    string date = string.Empty;
            //    //To get the Invoice Date
            //    if (StringManager.IsEqual(drEmail[EnumManager.EmailColumns.DISPLAYFLAG.ToString()].ToString(), EnumManager.Keys.TRUE.ToString()))
            //        date = "( " + DateTime.Today.ToString("MM/dd/yyyy") + " )";
            //    string result = objCommon.GeneratePFEmail(PfName, drEmail[EnumManager.EmailColumns.TOADDRESS.ToString()].ToString(),
            //                        drEmail[EnumManager.EmailColumns.CCADDRESS.ToString()].ToString(), string.Empty, drEmail[EnumManager.EmailColumns.FROMADDRESS.ToString()].ToString(),
            //                        drEmail[EnumManager.EmailColumns.EMAIL_SUBJECT.ToString()].ToString() + date, drEmail[EnumManager.EmailColumns.EMAIL_BODY.ToString()].ToString(), true,
            //                        drEmail[EnumManager.EmailColumns.Greeting.ToString()].ToString(), drEmail[EnumManager.EmailColumns.Signature.ToString()].ToString(), proadminUrl, userId);

            //    if (result.ToUpper().Contains("EMAIL SENT SUCCESSFULLY"))
            //        return "Email Sent Successfully";
            //    else
            //        return "Failed to send mail";

            //}

            return "";
        }

        #endregion
    }
}

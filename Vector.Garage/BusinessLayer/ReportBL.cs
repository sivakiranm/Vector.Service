using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vector.Common.BusinessLayer;
using Vector.Common.DataLayer;
using Vector.Common.Entities;
using Vector.Garage.DataLayer;
using Vector.Garage.Entities;

namespace Vector.Garage.BusinessLayer
{
    public class ReportBL : DisposeLogic
    {
        private VectorDataConnection objVectorDB;
        public ReportBL(VectorDataConnection objVectorCon)
        {
            this.objVectorDB = objVectorCon;
        }


        public VectorResponse<object> GetProcessAndFlowMonitoringConsoleInfo(ConsoleEntity objConsoleEntity)
        {
            using (var objReportDL = new ReportDL(objVectorDB))
            {
                var result = objReportDL.GetProcessAndFlowMonitoringConsoleInfo(objConsoleEntity);
                DataSet dsData = new DataSet();
                if (objConsoleEntity.Action.Equals(VectorConstants.Process))
                    dsData = FormatMonitoringConsoleForProcess(result);
                else if (objConsoleEntity.Action.Equals(VectorConstants.Flow))
                    dsData = FormatMonitoringConsoleForFlow(result);

                if (DataValidationLayer.isDataSetNotNull(dsData))
                {
                    return new VectorResponse<object>() { ResponseData = dsData };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }

        private DataSet FormatMonitoringConsoleForProcess(DataSet result)
        {
            DataSet dsData = new DataSet();
            DataTable table = new DataTable();
            table.Columns.Add("Manifest", typeof(String));
            table.Columns.Add("ManifestId", typeof(Int64));
            table.Columns.Add("ZeroToFourHours", typeof(int));
            table.Columns.Add("FourtoTwelveHours", typeof(int));
            table.Columns.Add("MoreThanTwlelveHours", typeof(int));
            table.TableName = "Summary";

            var distinctManifest = (from c in result.Tables[0].AsEnumerable()
                                    select new
                                    {
                                        ManifestID = c.Field<Int64>("ManifestId"),
                                        ManifestName = c.Field<string>("manifestName")
                                    }).Distinct().ToList();

            foreach (var maniFId in distinctManifest)
            {
                int fisrt = (from c in result.Tables[0].AsEnumerable()
                             where c.Field<Int64>("ManifestId") == maniFId.ManifestID
                             && c.Field<Int32>("DelayHours") == 3
                             select new
                             {
                                 ManifestId = c.Field<Int64>("ManifestId")
                             }).Count();
                int Second = (from c in result.Tables[0].AsEnumerable()
                              where c.Field<Int64>("ManifestId") == maniFId.ManifestID
                              && c.Field<Int32>("DelayHours") == 2
                              select new
                              {
                                  ManifestId = c.Field<Int64>("ManifestId")
                              }).Count();
                int third = (from c in result.Tables[0].AsEnumerable()
                             where c.Field<Int64>("ManifestId") == maniFId.ManifestID
                             && c.Field<Int32>("DelayHours") == 1
                             select new
                             {
                                 ManifestId = c.Field<Int64>("ManifestId")
                             }).Count();

                DataRow dr = table.NewRow();
                dr["Manifest"] = maniFId.ManifestName;
                dr["ManifestId"] = maniFId.ManifestID;
                dr["ZeroToFourHours"] = third;
                dr["FourtoTwelveHours"] = Second;
                dr["MoreThanTwlelveHours"] = fisrt;

                table.Rows.Add(dr);
            }

            dsData.Tables.Add(table);


            return dsData;
        }
        private DataSet FormatMonitoringConsoleForFlow(DataSet result)
        {
            DataSet dsData = new DataSet();
            DataTable dtColumns = new DataTable("DisplayColumns");
            dtColumns.Columns.Add("name", typeof(String));
            dtColumns.Columns.Add("value", typeof(String));

            DataTable table = new DataTable("FlowInfo");
            table.Columns.Add("flow", typeof(String));
            table.Columns.Add("flowId", typeof(Int64));

            DataRow drColumns1 = dtColumns.NewRow();
            drColumns1["name"] = "Flow";
            drColumns1["value"] = "flow";
            dtColumns.Rows.Add(drColumns1);


            int flowDetailsCount = 1;
            string colunName = "task";
            string colunTextName = "taskName";
            string colunTextId = "taskId";
            string colunDisplayName = "Task ";
            var distinctFlows = (from c in result.Tables[0].AsEnumerable()
                                 select new
                                 {
                                     FlowId = c.Field<Int64>("FlowId"),
                                     FlowName = c.Field<string>("FlowName")
                                 }).Distinct().ToList();



            foreach (var flowInfo in distinctFlows)
            {

                DataRow dr = table.NewRow();

                var distinctFlowDetails = (from c in result.Tables[0].AsEnumerable()
                                           where c.Field<Int64>("FlowId") == flowInfo.FlowId
                                           select new
                                           {
                                               FlowDetailsId = c.Field<Int64>("FlowDetailsId"),
                                               FlowDetailName = c.Field<string>("TaskOrEventName"),
                                               TotalTasks = c.Field<Int32>("Total")
                                           }).Distinct().ToList();

                flowDetailsCount = 1;
                foreach (var objFlowDetails in distinctFlowDetails)
                {
                    string workdColumnName = "";
                    switch (flowDetailsCount)
                    {
                        case 1:
                            workdColumnName = "One";
                            break;
                        case 2:
                            workdColumnName = "Two";
                            break;
                        case 3:
                            workdColumnName = "Three";
                            break;
                        case 4:
                            workdColumnName = "Four";
                            break;
                        case 5:
                            workdColumnName = "Five";
                            break;
                        case 6:
                            workdColumnName = "Six";
                            break;
                        case 7:
                            workdColumnName = "Seven";
                            break;
                        case 8:
                            workdColumnName = "Eight";
                            break;
                        case 9:
                            workdColumnName = "Nine";
                            break;
                        case 10:
                            workdColumnName = "Ten";
                            break;
                        case 11:
                            workdColumnName = "Eleven";
                            break;
                        case 12:
                            workdColumnName = "Twelve";
                            break;
                        case 13:
                            workdColumnName = "Thirteen";
                            break;
                        case 14:
                            workdColumnName = "Fourteen";
                            break;
                        case 15:
                            workdColumnName = "Fifteen";
                            break;
                        case 16:
                            workdColumnName = "Sixteen";
                            break;
                        case 17:
                            workdColumnName = "Seventeen";
                            break;
                        case 18:
                            workdColumnName = "Eighteen";
                            break;
                        default:
                            workdColumnName = "ninteen";
                            break;
                    }

                    colunName = "task" + workdColumnName;
                    colunDisplayName = "Task " + flowDetailsCount;
                    colunTextName = "task" + workdColumnName + "Name";
                    colunTextId = "task" + workdColumnName + "Id";

                    if (table.Columns.Contains(colunName))
                    {


                        dr[colunName] = objFlowDetails.TotalTasks;
                        dr[colunTextName] = objFlowDetails.FlowDetailName;
                        dr[colunTextId] = objFlowDetails.FlowDetailsId;
                    }
                    else
                    {
                        table.Columns.Add(colunName, typeof(Int32));
                        table.Columns.Add(colunTextName, typeof(string));
                        table.Columns.Add(colunTextId, typeof(Int64));


                        //dr.Columns.Add(colunName, typeof(Int32));
                        //dr.Columns.Add(colunTextName, typeof(string));
                        //dr.Columns.Add(colunTextId, typeof(Int64));

                        DataRow drColumns = dtColumns.NewRow();
                        drColumns["value"] = colunName;
                        drColumns["name"] = colunDisplayName;
                        dtColumns.Rows.Add(drColumns);

                        dr[colunName] = objFlowDetails.TotalTasks;
                        dr[colunTextName] = objFlowDetails.FlowDetailName;
                        dr[colunTextId] = objFlowDetails.FlowDetailsId;
                    }

                    flowDetailsCount = flowDetailsCount + 1;
                }


                if (dr == null)
                    dr = table.NewRow();

                dr["flow"] = flowInfo.FlowName;
                dr["flowId"] = flowInfo.FlowId;
                table.Rows.Add(dr);

            }

            dsData.Tables.Add(table);
            dsData.Tables.Add(dtColumns);


            return dsData;
        }

        public VectorResponse<object> GetClientListReportInfo(SearchEntity searchEntity, Int64 userId)
        {
            using (var objReportDL = new ReportDL(objVectorDB))
            {
                var result = objReportDL.GetClientListReportInfo(searchEntity, userId);
   
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[0].TableName = "Summary";
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }

        public VectorResponse<object> GetVendorOverchargeReportInfo(SearchEntity objSearchEntity, Int64 userId)
        {
            using (var objReportDL = new ReportDL(objVectorDB))
            {
                var result = objReportDL.GetVendorOverchargeReportInfo(objSearchEntity, userId);

                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[0].TableName = "Summary";
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }

        public VectorResponse<object> GetVendorListReportInfo(SearchEntity objAgingEntity, Int64 userId)
        {
            using (var objReportDL = new ReportDL(objVectorDB))
            {
                var result = objReportDL.GetVendorListReportInfo(objAgingEntity, userId);

                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[0].TableName = "Summary";
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }

        public VectorResponse<object> GetVendorCorporateListReportInfo(SearchEntity objAgingEntity, Int64 userId)
        {
            using (var objReportDL = new ReportDL(objVectorDB))
            {
                var result = objReportDL.GetVendorCorporateListReportInfo(objAgingEntity, userId);

                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[0].TableName = "Summary";
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }

        public VectorResponse<object> GetDownloadInvoicesReportInfo(SearchEntity objSearchEntity, Int64 userId)
        {
            using (var objReportDL = new ReportDL(objVectorDB))
            { 
                
                var result = objReportDL.GetDownloadInvoicesReportInfo(objSearchEntity, userId);

                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[0].TableName = "Summary";
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }


        public VectorResponse<object> DownloadInvoices(DownloadInvoices objDonwloadInvoices, Int64 userId)
        {
            using (var objReportDL = new ReportDL(objVectorDB))
            {
                List<string> copyFileResult = new List<string>();
                var result = objReportDL.DownloadInvoices(objDonwloadInvoices, userId);
                string FileName = DateManager.GenerateTitleWithTimestamp("DownloadFiles");
                string zipFilePath = SecurityManager.GetConfigValue("FileServerTempPath") + "\\" + FileName;
                string zipPath = zipFilePath + ".zip";

                if(!Directory.Exists(zipFilePath))
                {
                    Directory.CreateDirectory(zipFilePath);
                }

                foreach (DataRow dr in result.Tables[1].Rows)
                {
                     
                    if (File.Exists(Convert.ToString(dr["InvoicePath"])))
                    {
                        File.Copy(Convert.ToString(dr["InvoicePath"]), zipFilePath + "//" +  Convert.ToString(dr["FileName"]));
                        copyFileResult.Add("Invoice Number : "+Convert.ToString(dr["InvoiceNumber"]) + " , Type : " + Convert.ToString(dr["Type"]) + " - Included Successfully.");
                    } 
                    else
                        copyFileResult.Add("Invoice Number : " + Convert.ToString(dr["InvoiceNumber"]) + " , Type : " + Convert.ToString(dr["Type"]) + " - File Not Found.");
                }


                //  ZipFile.CreateFromDirectory(zipFilePath, zipPath); 

                ZipFile.CreateFromDirectory(zipFilePath, zipPath, CompressionLevel.Fastest, true);
                
                result.Tables[0].Rows[0]["downloadFilePath"] = result.Tables[0].Rows[0]["downloadFilePath"]  +  "/Temp/" +FileName + ".zip" ;

                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[0].TableName = "Summary";
                    //if (Directory.Exists(zipFilePath))
                    //{
                    //    Directory.Delete(zipFilePath);
                    //}
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }


        public VectorResponse<object> ManageMissingInvoiceInfo(MarkAsNotMissing objMarkAsNotMissing, Int64 userId)
        {
            using (var objReportDL = new ReportDL(objVectorDB))
            {
                var result = objReportDL.ManageMissingInvoiceInfo(objMarkAsNotMissing, userId);

                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[0].TableName = "Summary";
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }

        public VectorResponse<object> UploadedMissingInviceInfo(MarkAsNotMissing objMarkAsNotMissing, Int64 userId)
        {
            using (var objReportDL = new ReportDL(objVectorDB))
            {
                string pathToMoveFile = "";
                pathToMoveFile = SecurityManager.GetConfigValue("FileServerPath") + "MissingInvoices";
                string newFileName =  objMarkAsNotMissing.fileName.Replace(".xlsx","") + "_"+ DateManager.GetTimestamp(DateTime.Now) + ".xlsx";


                string filePath = "";
                filePath = SecurityManager.GetConfigValue("FileServerTempPath") + objMarkAsNotMissing.folderName + "\\" + objMarkAsNotMissing.fileName;

                if (!Directory.Exists(pathToMoveFile))
                {
                    Directory.CreateDirectory(pathToMoveFile);
                  
                }
              
                DataSet readExcel = ReadmissinInvoiceFiles(objMarkAsNotMissing, filePath);

                var result = objReportDL.UploadedMissingInviceInfo(objMarkAsNotMissing, userId, readExcel);

                int updatedResult = (from c in result.Tables[0].AsEnumerable()
                                     select c.Field<Int32>("Result")).FirstOrDefault();

                if(updatedResult == 0)
                {
                    File.Delete(pathToMoveFile + "\\" + newFileName); 
                }
                else
                {
                    File.Move(filePath, pathToMoveFile + "\\" + newFileName);
                }


                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[0].TableName = "Summary";
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }

        private DataSet ReadmissinInvoiceFiles(MarkAsNotMissing objMarkAsNotMissing,string filePath)
        {
            
            DataSet fileData = ExcelToDataTable(filePath);
            return fileData;

        }

        public DataSet ExcelToDataTable(String AddendaFilePath)
        {

            using (var stream = File.Open(AddendaFilePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (data) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });


                    return result;
                }
            }

        }


        public VectorResponse<object> GetTaskStatus(TaskSearch objTaskSearch, Int64 userId)
        {
            using (var objReportDL = new ReportDL(objVectorDB))
            {
                var result = objReportDL.GetTaskStatus(objTaskSearch, userId);

                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[0].TableName = "Summary";
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }


        public VectorResponse<object> GetProcessReport(SearchEntity objSearchEntity, Int64 userId)
        {
            using (var objReportDL = new ReportDL(objVectorDB))
            {
                var result = objReportDL.GetProcessReport(objSearchEntity, userId);

                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[0].TableName = "Summary";
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }


        public VectorResponse<object> GetLineitemComments(string type, Int64 lineitemId, Int64 userId)
        {
            using (var objReportDL = new ReportDL(objVectorDB))
            {
                var result = objReportDL.GetLineitemComments(type, lineitemId, userId);

                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    if(type.ToUpper().Equals("BASELINECONTRACTBYALID"))
                    {
                        result.Tables[0].TableName = "BaselineComments";
                        result.Tables[1].TableName = "ContractComments";
                    }
                    else if (type.ToUpper().Equals("EXCEPTIONACCOUNTS") || type.ToUpper().Equals("CONTRACTBASELINE") || type.ToUpper().Equals("CONTRACTLINEITEMTRUEUPPANELCOMMENTS"))
                    {
                        result.Tables[0].TableName = "AccountComments";
                        result.Tables[1].TableName = "ContractComments";
                        result.Tables[2].TableName = "BaselineComments";
                    }
                    else 
                    result.Tables[0].TableName = "Comments";
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }


        public VectorResponse<object> VectorMissingInvoiceHistory(string action,Int64 Accountid, string irpUniqueCode,Int64 contractId, Int64 userId)
        {
            using (var objReportDL = new ReportDL(objVectorDB))
            {
                var result = objReportDL.VectorMissingInvoiceHistory(action, Accountid, irpUniqueCode,contractId, userId);

                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[0].TableName = "Summary";
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }


        public VectorResponse<object> VectorGetAccountAddEditReport(string action, Int64 userId)
        {
            using (var objReportDL = new ReportDL(objVectorDB))
            {
                var result = objReportDL.VectorGetAccountAddEditReport(action, userId);

                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[0].TableName = "Summary";
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

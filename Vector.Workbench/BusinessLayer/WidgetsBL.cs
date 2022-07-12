using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vector.Common.BusinessLayer;
using Vector.Common.DataLayer;
using Vector.Common.Entities;
using Vector.Garage.Entities;
using Vector.Workbench.DataLayer;
using Vector.Workbench.Entities;
using System.Xml.Linq;
using System.IO;

namespace Vector.Workbench.BusinessLayer
{
    public class WidgetsBL : DisposeLogic
    {

        private VectorDataConnection objVectorDB;
        public WidgetsBL(VectorDataConnection objVectorCon)
        {
            this.objVectorDB = objVectorCon;
        }


        public VectorResponse<object> ClientSummaryInfo(ClientSummaryInfo objClientSummaryInfo, Int64 userId)
        {
            using (var objWidgetsDL = new WidgetsDL(objVectorDB))
            {
                var result = PivotAndAddSummaryDetails(objWidgetsDL.ClientSummaryInfo(objClientSummaryInfo, userId));

                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[0].TableName = "Details";
                    result.Tables[1].TableName = "Summary";
                    result.Tables[2].TableName = "columns";
                    return new VectorResponse<object>() { ResponseData = result };

                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }

        private DataSet PivotAndAddSummaryDetails(DataSet dataSet)
        {
            DataTable dtData = new DataTable();
            dtData.Columns.Add("Category", typeof(string));

            DataTable dtDataColumns = new DataTable();
            dtDataColumns.Columns.Add("name", typeof(string)); 
            dtDataColumns.Columns.Add("value", typeof(string));


            DataRow dtRowColumnOld = dtDataColumns.NewRow();
            dtRowColumnOld["name"] = "category";

            dtDataColumns.Rows.Add(dtRowColumnOld);

            foreach (DataColumn dtColumns in dataSet.Tables[0].Columns)
            {
                if (dtColumns.ColumnName != "MonthName" && dtColumns.ColumnName != "InvoiceYear" && dtColumns.ColumnName != "MonthYearDisplay"
                    && dtColumns.ColumnName != "InvoiceYear" && dtColumns.ColumnName != "MonthNumber" && dtColumns.ColumnName != "MonthYear")
                {
                    DataRow dtRow = dtData.NewRow();
                    dtRow["Category"] = dtColumns.ColumnName;

                    dtData.Rows.Add(dtRow);
                }
            }

            foreach (DataRow dtRows in dataSet.Tables[0].Rows)
            {
                if (!dtData.Columns.Contains(Convert.ToString(dtRows["MonthYear"])))
                {
                    dtData.Columns.Add(Convert.ToString(dtRows["MonthYear"]), typeof(double));

                    DataRow dtRowColumn = dtDataColumns.NewRow();
                    dtRowColumn["name"] = Convert.ToString(dtRows["MonthYearDisplay"]);
                    dtRowColumn["value"] = Convert.ToString(dtRows["MonthYear"]);

                    dtDataColumns.Rows.Add(dtRowColumn);
                }

                int index = -1;
                DataRow[] rows = dtData.Select("Category = 'Old Cost'");
                if (rows.Count() > 0)
                {
                    index = dtData.Rows.IndexOf(rows[0]);
                }

                dtData.Rows[index][Convert.ToString(dtRows["MonthYear"])] = Convert.ToString(dtRows["Old Cost"]);

                int index1 = -1;
                DataRow[] rows1 = dtData.Select("Category = 'New Cost'");
                if (rows1.Count() > 0)
                {
                    index1 = dtData.Rows.IndexOf(rows1[0]);
                }
                dtData.Rows[index1][Convert.ToString(dtRows["MonthYear"])] = Convert.ToString(dtRows["New Cost"]);


                int index2 = -1;
                DataRow[] rows2 = dtData.Select("Category = 'Savings'");
                if (rows2.Count() > 0)
                {
                    index2 = dtData.Rows.IndexOf(rows2[0]);
                }
                dtData.Rows[index2][Convert.ToString(dtRows["MonthYear"])] = Convert.ToString(dtRows["Savings"]);


                int index3 = -1;
                DataRow[] rows3 = dtData.Select("Category = 'Error Caught'");
                if (rows3.Count() > 0)
                {
                    index3 = dtData.Rows.IndexOf(rows3[0]);
                }
                dtData.Rows[index3][Convert.ToString(dtRows["MonthYear"])] = Convert.ToString(dtRows["Error Caught"]);

            }




            DataSet dsData = new DataSet();
            dsData.Tables.Add(dataSet.Tables[0].Copy()); 
            dsData.Tables.Add(dtData);
            dsData.Tables.Add(dtDataColumns);
            return dsData;
        }


        public VectorResponse<object> ClientMetrics(Int64? clientId, Int64 userId)
        {
            using (var objWidgetsDL = new WidgetsDL(objVectorDB))
            {
                var result = objWidgetsDL.ClientMetrics(clientId, userId);

                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[0].TableName = "metrics";
                    return new VectorResponse<object>() { ResponseData = result };

                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }

        public VectorResponse<object> ServiceSummary(Int64? propertyId, string propertyName, Int64 userId)
        {
            using (var objWidgetsDL = new WidgetsDL(objVectorDB))
            {
                var result = objWidgetsDL.ServiceSummary(propertyId, propertyName, userId);

                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[0].TableName = "serviceSummary";
                    return new VectorResponse<object>() { ResponseData = result };

                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }

        public VectorResponse<object> GetInvoiceExplorerBL(SearchInvoice objSearch, Int64 userId)
        {
            using (var objWidgetsDL = new WidgetsDL(objVectorDB))
            {
                var result = objWidgetsDL.GetInvoiceExplorerDL(objSearch,userId);
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

        public VectorResponse<object> TicketByServiceType(Int64? propertyId,Int64 userId)
        {
            using (var objWidgetsDL = new WidgetsDL(objVectorDB))
            {
                var result = objWidgetsDL.TicketByServiceType(propertyId,  userId);

                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[0].TableName = "ticketsInfo"; 
                    result.Tables[1].TableName = "tickets";
                    return new VectorResponse<object>() { ResponseData = result };

                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }

        public VectorResponse<object> VectorOpenInvoiceByClient(Int64? clientId, Int64 userId)
        {
            using (var objWidgetsDL = new WidgetsDL(objVectorDB))
            {
                var result = objWidgetsDL.VectorOpenInvoiceByClient(clientId, userId);

                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[0].TableName = "summary";
                    result.Tables[1].TableName = "info";
                    return new VectorResponse<object>() { ResponseData = result };

                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }



        public VectorResponse<object> ManageActions(Actions objActions, Int64 userId)
        {
            using (var objWidgetsDL = new WidgetsDL(objVectorDB))
            {
                string actionDocs = string.Empty;

                if (objActions.DocumentXml != null && objActions.DocumentXml.Count > 0)
                {
                    XDocument Docs = new XDocument(
                         new XElement("ROOT",
                         from Doc in objActions.DocumentXml
                         select new XElement("File",
                                     new XElement("DocumentName", Doc.DocumentName),
                                     new XElement("DocumentType", Doc.DocumentType)

                         )));

                    actionDocs = Docs.ToString();
                }
                else
                {

                    actionDocs = "<ROOT></ROOT>";
                }


                var result = objWidgetsDL.ManageActions(objActions, userId, actionDocs.ToString());
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
        public VectorResponse<object> GetActions(Actions objActions, Int64 userId)
        {
            using (var objWidgetsDL = new WidgetsDL(objVectorDB))
            {
                var result = objWidgetsDL.GetActions(objActions, userId);
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


        public VectorResponse<object> GetNegotiationthreesixtyReport(SearchEntity objSearch, Int64 userId)
        {
            using (var objWidgetsDL = new WidgetsDL(objVectorDB))
            {
                var result = objWidgetsDL.GetNegotiationthreesixtyReport(objSearch, userId);

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

        public VectorResponse<object> GetNegotaitionStatusWidgetDetails(SearchEntity objSearch, Int64 userId)
        {
            using (var objWidgetsDL = new WidgetsDL(objVectorDB))
            {
                var result = objWidgetsDL.GetNegotaitionStatusWidgetDetails(objSearch, userId);

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


        public VectorResponse<object> UpdatePersonalizedSettings(PersonalizeSettings objSearch, Int64 userId)
        {
            using (var objWidgetsDL = new WidgetsDL(objVectorDB))
            {
                var result = objWidgetsDL.UpdatePersonalizedSettings(objSearch, userId);

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


        public VectorResponse<object> GetWorkTrackerInfo(WorkEntity objSearch, Int64 userId)
        {
            using (var objWidgetsDL = new WidgetsDL(objVectorDB))
            {
                var result = objWidgetsDL.GetWorkTrackerInfo(objSearch, userId);

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

        public VectorResponse<object> ManageWorkTracker(WorkEntity objSearch, Int64 userId)
        {
            using (var objWidgetsDL = new WidgetsDL(objVectorDB))
            {
                string tempFilePath = SecurityManager.GetConfigValue("FileServerTempPath") + objSearch.tempFolderName + "\\";
                string folderPath = SecurityManager.GetConfigValue("FileServerPath");


    var result = objWidgetsDL.ManageWorkTracker(objSearch, userId);

                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    if (objSearch.action.ToUpper().Equals("CREATEISSUEORENHANCEMENT") || objSearch.action.ToUpper().Equals("UPDATESTATUS"))
                    {
                        var resultInfo = (from c in result.Tables[0].AsEnumerable()
                                          select new
                                          {
                                              workNo = c.Field<string>("WorkNo"),
                                              WorkFolderPath = c.Field<string>("WorkFolderPath"),

                                          }).FirstOrDefault();

                        foreach (var obj in objSearch.documents)
                        {
                            if (File.Exists(tempFilePath + obj.fileName))
                            {


                                if (!Directory.Exists(folderPath + resultInfo.WorkFolderPath + resultInfo.workNo))
                                    Directory.CreateDirectory(folderPath + resultInfo.WorkFolderPath + resultInfo.workNo);

                                File.Move(tempFilePath + obj.fileName, folderPath + resultInfo.WorkFolderPath + resultInfo.workNo + "//" + obj.fileName);
                            }
                        }
                    }

                    return new VectorResponse<object>() { ResponseData = result };

                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }


        public VectorResponse<object> GetProductivityMetrics(string action,string reportBy, Int64? reporterId, Int64? clientId, Int64 userId)
        {
            using (var objWidgetsDL = new WidgetsDL(objVectorDB))
            {
                var result = objWidgetsDL.GetProductivityMetrics(action, reportBy, reporterId,clientId,userId);

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

    }



}

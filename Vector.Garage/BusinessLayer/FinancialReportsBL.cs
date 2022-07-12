using System;
using System.Data;
using Vector.Common.BusinessLayer;
using Vector.Common.DataLayer;
using Vector.Common.Entities;
using Vector.Garage.DataLayer;
using Vector.Garage.Entities;

namespace Vector.Garage.BusinessLayer
{
    public class FinancialReportsBL : DisposeLogic
    {
        private VectorDataConnection objVectorDB;
        public FinancialReportsBL(VectorDataConnection objVectorCon)
        {
            this.objVectorDB = objVectorCon;
        }



        public VectorResponse<object> GetRsAgingInfo(SearchEntity searchReport,Int64 userId)
        {
            using (var objFinancialDL = new FinancialReportsDL(objVectorDB))
            {
                var result = objFinancialDL.GetRsAgingInfo(searchReport, userId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[0].TableName = "Summary";
                    result.Tables[1].TableName = "Details";
                    return new VectorResponse<object>() { ResponseData = result };

                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }

        public VectorResponse<object> GetHaulerAgingInfo(SearchEntity searchReport, Int64 userId)
        {
            using (var objFinancialDL = new FinancialReportsDL(objVectorDB))
            {
                var result = objFinancialDL.GetHaulerAgingInfo(searchReport, userId); 
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[0].TableName="Summary";
                    result.Tables[1].TableName="Details";
                    return new VectorResponse<object>() { ResponseData = result };

                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }

        public VectorResponse<object> GetInvoiceSummaryInfo(SearchEntity searchReport, Int64 userId)
        {
            using (var objFinancialDL = new FinancialReportsDL(objVectorDB))
            {
                var result = objFinancialDL.GetInvoiceSummaryInfo(searchReport, userId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[0].TableName = "Summary";
                    result.Tables[1].TableName = "Details";
                    return new VectorResponse<object>() { ResponseData = result };

                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }

        public VectorResponse<object> GetSavingSummaryInfo(SearchEntity searchReport, Int64 userId)
        {
            using (var objFinancialDL = new FinancialReportsDL(objVectorDB))
            {
                var result = objFinancialDL.GetSavingSummaryInfo(searchReport, userId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[0].TableName = "Summary";
                    result.Tables[1].TableName = "Details";
                    return new VectorResponse<object>() { ResponseData = result };

                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }

        public VectorResponse<object> GetInvoiceLineItemInfo(SearchEntity searchReport, Int64 userId)
        {
            using (var objFinancialDL = new FinancialReportsDL(objVectorDB))
            {
                var result = objFinancialDL.GetInvoiceLineItemInfo(searchReport, userId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[0].TableName = "Summary";
                    result.Tables[1].TableName = "Details";
                    return new VectorResponse<object>() { ResponseData = result };

                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }

        public VectorResponse<object> GetNegotiationStatusInfo(SearchEntity searchReport, Int64 userId)
        {
            using (var objFinancialDL = new FinancialReportsDL(objVectorDB))
            {
                var result = objFinancialDL.GetNegotiationStatusInfo(searchReport, userId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[0].TableName = "Summary";
                    result.Tables[1].TableName = "Details";
                    return new VectorResponse<object>() { ResponseData = result };

                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }

        public VectorResponse<object> GetContractInfo(SearchEntity searchReport, Int64 userId)
        {
            using (var objFinancialDL = new FinancialReportsDL(objVectorDB))
            {
                var result = objFinancialDL.GetContractInfo(searchReport, userId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[0].TableName = "Summary";
                    result.Tables[1].TableName = "Details";
                    return new VectorResponse<object>() { ResponseData = result };

                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }

        public VectorResponse<object> GetPropertyInfo(SearchEntity searchReport, Int64 userId)
        {
            using (var objFinancialDL = new FinancialReportsDL(objVectorDB))
            {
                var result = objFinancialDL.GetPropertyInfo(searchReport, userId);
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

        public VectorResponse<object> GetMissingInvoiceReport(SearchEntity searchReport, Int64 userId)
        {
            using (var objFinancialDL = new FinancialReportsDL(objVectorDB))
            {
                var result = objFinancialDL.GetMissingInvoiceReport(searchReport, userId);
                GetAgeAndNextBillDates(ref result, searchReport.missingStatus);
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

        public VectorResponse<object> GetBillGapReport(SearchEntity searchReport, Int64 userId)
        {
            using (var objFinancialDL = new FinancialReportsDL(objVectorDB))
            {
                var result = objFinancialDL.GetBillGapReport(searchReport, userId); 
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables.Add(GetColumnsToDisplayForBillGapReport(result));
                    result.Tables[0].TableName = "Summary";
                    return new VectorResponse<object>() { ResponseData = result };

                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }

        private DataTable GetColumnsToDisplayForBillGapReport(DataSet result)
        {
            DataTable dtColumnsToDisplay = new DataTable();
            dtColumnsToDisplay.TableName = "displayColumns";
            dtColumnsToDisplay.Columns.Add("Value", typeof(string));
            dtColumnsToDisplay.Columns.Add("Name", typeof(string));
            dtColumnsToDisplay.Columns.Add("DownloadColumnName", typeof(string));
            dtColumnsToDisplay.Columns.Add("DisplayColumnName", typeof(string));
            dtColumnsToDisplay.Columns.Add("BillGapCommentsColumnName", typeof(string)); 

            foreach (DataColumn  dColumn in result.Tables[0].Columns)
            {
                if (dColumn.ColumnName.Contains("bill_"))
                {
                    DataRow dr = dtColumnsToDisplay.NewRow();
                    dr["Value"] = dColumn.ColumnName;
                    string colName = dColumn.ColumnName.Replace("bill_", "");
                    dr["Name"] = colName.Insert(colName.Length - 4, " "); 
                    dr["DownloadColumnName"] = dColumn.ColumnName;
                    dr["DisplayColumnName"] = colName.Insert(colName.Length - 4, " ");
                    dr["BillGapCommentsColumnName"] = dColumn.ColumnName.Replace("bill_", "comments_");


                    dtColumnsToDisplay.Rows.Add(dr);
                }
            }

            return dtColumnsToDisplay;
        }

        public void GetAgeAndNextBillDates(ref DataSet ds, object status)
        {

            if (!DataManager.IsNullOrEmptyDataSet(ds))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dr.BeginEdit();
                    DateTime dBilldate;
                    DateTime dNextBilldateWithoutGracePeriod;
                    DateTime dcurrentDate;
                    //int age = 0;
                    string billingCycle = string.Empty;
                    dcurrentDate = DateTime.Today;

                    if (!string.IsNullOrEmpty(dr["Invoice_Date"].ToString()))
                    {
                        if (!string.IsNullOrEmpty(dr["BillingCycle"].ToString()))
                            billingCycle = dr["BillingCycle"].ToString();

                        dBilldate = Convert.ToDateTime(dr["Invoice_Date"].ToString());
                        dBilldate = UpdateWithHaulerInvoiceDay(Convert.ToString(dr["HAULERINVOICEDAY"]), dBilldate);
                        dNextBilldateWithoutGracePeriod = dBilldate;
                        dBilldate = AddGracePeriod(Convert.ToString(dr["RECIEPTMODE"]), dBilldate);
                        switch (billingCycle.ToUpper())
                        {
                            case "BI-MONTHLY":
                                dr["NEXTBILLDATE"] = Convert.ToDateTime(dBilldate.AddMonths(2)).ToShortDateString();
                                dNextBilldateWithoutGracePeriod = dNextBilldateWithoutGracePeriod.AddMonths(2);
                                break;
                            case "MONTHLY":
                                dr["NEXTBILLDATE"] = Convert.ToDateTime(dBilldate.AddMonths(1)).ToShortDateString();
                                dNextBilldateWithoutGracePeriod = dNextBilldateWithoutGracePeriod.AddMonths(1);
                                break;
                            case "SEMI-WEEKLY":
                            case "WEEKLY":
                                dr["NEXTBILLDATE"] = Convert.ToDateTime(dBilldate.AddDays(7)).ToShortDateString();
                                dNextBilldateWithoutGracePeriod = dNextBilldateWithoutGracePeriod.AddDays(7);
                                break;
                            case "BI-WEEKLY":
                                dr["NEXTBILLDATE"] = Convert.ToDateTime(dBilldate.AddDays(14)).ToShortDateString();
                                dNextBilldateWithoutGracePeriod = dNextBilldateWithoutGracePeriod.AddDays(14);
                                break;
                            case "SEMI-MONTHLY":
                                dr["NEXTBILLDATE"] = Convert.ToDateTime(dBilldate.AddDays(15)).ToShortDateString();
                                dNextBilldateWithoutGracePeriod = dNextBilldateWithoutGracePeriod.AddDays(15);
                                break;
                            case "YEARLY":
                                dr["NEXTBILLDATE"] = Convert.ToDateTime(dBilldate.AddYears(1)).ToShortDateString();
                                dNextBilldateWithoutGracePeriod = dNextBilldateWithoutGracePeriod.AddYears(1);
                                break;
                            case "HALF YEARLY":
                                dr["NEXTBILLDATE"] = Convert.ToDateTime(dBilldate.AddMonths(6)).ToShortDateString();
                                dNextBilldateWithoutGracePeriod = dNextBilldateWithoutGracePeriod.AddMonths(6);
                                break;
                            case "QUARTERLY":
                                dr["NEXTBILLDATE"] = Convert.ToDateTime(dBilldate.AddMonths(3)).ToShortDateString();
                                dNextBilldateWithoutGracePeriod = dNextBilldateWithoutGracePeriod.AddMonths(3);
                                break;
                            case "VARIABLE":
                                dr["NEXTBILLDATE"] = DBNull.Value;
                                break;
                            default:
                                dr["NEXTBILLDATE"] = DBNull.Value;
                                break;
                        }

                        if (!string.IsNullOrEmpty(dr["NEXTBILLDATE"].ToString()))
                        {
                            int overDueDaysAfterHID = dcurrentDate.Subtract(dNextBilldateWithoutGracePeriod).Days;
                            int days = dcurrentDate.Subtract(Convert.ToDateTime(dr["NEXTBILLDATE"].ToString())).Days;
                            dr["MissingStatus"] = days > 0 ? "Missing" : (overDueDaysAfterHID > 0 ? "Anticipated" : "");
                            dr["NEXTBILLDATE"] = dNextBilldateWithoutGracePeriod.ToShortDateString();
                            dr["AGE"] = dcurrentDate.Subtract(dNextBilldateWithoutGracePeriod).Days;
                        }
                    }
                    else
                    {
                        DateTime currentDate = System.DateTime.Now;
                        dr["NEXTBILLDATE"] = Convert.ToDateTime(currentDate.Month.ToString() + "/" + GetHaulerInvoiceDay(dr["HaulerInvoiceDay"].ToString()) + "/" + currentDate.Year.ToString()).ToShortDateString();
                        DateTime billdate = Convert.ToDateTime(currentDate.Month.ToString() + "/01/" + currentDate.Year.ToString());
                        int overDueDaysAfterHID = dcurrentDate.Subtract(Convert.ToDateTime(dr["NEXTBILLDATE"].ToString())).Days;
                        int days = Convert.ToInt32(Convert.ToString(dcurrentDate.Subtract(Convert.ToDateTime(dr["NEXTBILLDATE"].ToString()).AddDays(GetNumberOfDays(Convert.ToString(dr["RecieptMode"])))).Days));
                        dr["MissingStatus"] = days > 0 ? "Missing" : (overDueDaysAfterHID > 0 ? "Anticipated" : "");
                        dr["AGE"] = dcurrentDate.Subtract(Convert.ToDateTime(dr["NEXTBILLDATE"].ToString())).Days;
                    }

                    dr.EndEdit();
                }

                //if (status != null)
                //{
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string filter = "missingstatus = ''" + (status != null ? " or missingstatus <> '" + status.ToString() + "'" : "");
                        DataRow[] rows = ds.Tables[0].Select(filter); // username is column name
                        foreach (DataRow r in rows)
                            r.Delete();
                    }
                    ds.AcceptChanges();

                //}
            }
        }

        private string GetHaulerInvoiceDay(string invoiceDay)
        {
            if (!string.IsNullOrEmpty(invoiceDay))
            {
                DateTime currentDate = System.DateTime.Now;
                if (StringManager.IsEqual(invoiceDay, "0"))
                {
                    return DateManager.GetLastDayOfMonth(currentDate.Month, currentDate.Year).ToString();
                }
                else if(currentDate.Month == 2)
                {
                    if (invoiceDay == "29" || invoiceDay == "30" || invoiceDay == "31" || invoiceDay == "32")
                    {
                        return "28";
                    }
                    else
                        return invoiceDay;
                }
            }
            else
            {
                return "01";
            }
            return invoiceDay;
        }

        private int GetNumberOfDays(string recieptMode)
        {
            if (StringManager.IsEqual(recieptMode, "Electronically"))
            {
                return 5;
            }
            else if (StringManager.IsEqual(recieptMode, "USPS"))
            {
                return 10;
            }
            return 10;
        }

        private DateTime AddGracePeriod(string recieptMode, DateTime dBilldate)
        {
            switch (recieptMode.ToUpper())
            {
                case "ELECTRONICALLY":
                    dBilldate = dBilldate.AddDays(5);
                    break;
                case "USPS":
                    dBilldate = dBilldate.AddDays(10);
                    break;
            }

            return dBilldate;


        }

        private DateTime UpdateWithHaulerInvoiceDay(string haulerInvoiceDay, DateTime dBilldate)
        {

            if (dBilldate.Month == 2)
            {
                if (haulerInvoiceDay == "31" || haulerInvoiceDay == "30" || haulerInvoiceDay == "29")
                    haulerInvoiceDay = "28";

                return string.IsNullOrEmpty(haulerInvoiceDay) ?
                               dBilldate :
                               (new DateTime(dBilldate.Year, dBilldate.Month,
                                       string.Compare(haulerInvoiceDay, "0", true) == 0 ?
                                            DateManager.GetLastDateOfMonth(dBilldate.Month, dBilldate.Year).Day :
                                           Convert.ToInt16(haulerInvoiceDay)));
            }
            else
            {
                return string.IsNullOrEmpty(haulerInvoiceDay) ?
                                dBilldate :
                                (new DateTime(dBilldate.Year, dBilldate.Month,
                                        string.Compare(haulerInvoiceDay, "0", true) == 0 ?
                                             DateManager.GetLastDateOfMonth(dBilldate.Month, dBilldate.Year).Day :
                                            Convert.ToInt16(haulerInvoiceDay)));
            }
        }




        public VectorResponse<object> GetServiceLevelByPropertyInfo(SearchEntity searchReport, Int64 userId)
        {
            using (var objFinancialDL = new FinancialReportsDL(objVectorDB))
            {
                var result = objFinancialDL.GetServiceLevelByPropertyInfo(searchReport, userId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[0].TableName = "Summary";
                    result.Tables[1].TableName = "Details";
                    return new VectorResponse<object>() { ResponseData = result };

                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }

        public VectorResponse<object> VectorGetStagingInvoices(SearchEntity searchReport, Int64 userId)
        {
            using (var objFinancialDL = new FinancialReportsDL(objVectorDB))
            {
                var result = objFinancialDL.VectorGetStagingInvoices(searchReport, userId); 
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

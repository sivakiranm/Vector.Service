using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;
using OfficeOpenXml.DataValidation;
using OfficeOpenXml.DataValidation.Contracts;
using OfficeOpenXml.Style;
using System.Collections.Specialized;

namespace Vector.Common.BusinessLayer
{
    public class ExportToExcel : DisposeLogic
    {
        #region Constants

        private const string DefaultSheetName = "Sheet1";
        private const string HeaderContent = "content-disposition";
        private const string HeaderAttachment = "attachment;  filename=";
        private const string FormatXlsx = ".xlsx";
        private const string FormatXls = ".xls";
        private const string ContentTypeXlsx = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        private const string ContentTypeXls = "application/vnd.ms-excel";
        private const string SystemString = "System.String";
        private const string SystemDateTime = "System.DateTime";
        private const string SystemDecimal = "System.Decimal";
        private const string SystemInt = "System.Int64";
        private const string CssNumber = ":cssnumber,";
        private const string CssDate = ":cssdate,";
        private const string CssCurrency = ":csscurrency,";

        #endregion

        #region Create Excel

        /// <summary>
        /// Create Excel Sheet using EPPLus.
        /// </summary>
        /// <param name="fileName">Path where the ExcelSheet to be stored</param>
        /// <param name="dsData">Data Set that contains table to Export to Excel</param>
        /// <param name="sheetName">Excel Sheet Name</param>
        /// <param name="freezPaneFrom">lockColumFrom</param>
        /// <param name="freezPaneTo">lockColumTo</param>
        /// <param name="passwordProtection">true/False</param>
        /// <param name="password">password protection string , is valid only when passwordProtection is set to "TRUE"</param>
        /// <param name="dtValidation"></param>
        /// <param name="enableDataValidations"></param>
        /// <param name="freezList">string dictionary format (fromCoulum:toColumn)</param>
        /// <param name="headerBgColor"></param>
        /// <param name="hideColumns"></param>
        /// <param name="hyperlinkColumns"></param>
        /// <param name="commaFormatRequired"></param>
        /// <param name="isExportRequired"></param>
        /// <param name="isFormatRequired"></param>
        /// <param name="isHeaderRequired"></param>
        /// <param name="isXlsx"></param>
        /// <returns></returns>
        public static bool CreateExcel(string fileName, DataSet dsData, string sheetName = DefaultSheetName,
                bool passwordProtection = false, string password = null,
                Dictionary<int, int> freezList = null, Dictionary<int, string> hideColumns = null,
                int freezPaneFrom = 0, int freezPaneTo = 0, bool enableDataValidations = false,
                DataTable dtValidation = null, bool isXlsx = true, bool isFormatRequired = false, bool headerBgColor = true,
                bool isHeaderRequired = true, bool isExportRequired = true, string[] hyperlinkColumns = null, bool commaFormatRequired = true)
        {
            using (ExcelPackage package = new ExcelPackage())
            {
                //Load the sheet with one string column, one date column and a few random numbers.
                var wsExcel = package.Workbook.Worksheets.Add(sheetName);

                //set the width of the columns in the workSheet.
                wsExcel.DefaultColWidth = VectorConstants.TwentyFive;

                //create cells based on the DataTable and Formula to a particular
                foreach (DataTable dtExcel in dsData.Tables)
                {
                    wsExcel = AddDataToExcel(wsExcel, dtExcel, enableDataValidations, dtValidation, isFormatRequired,
                                isHeaderAndBGColorReq: headerBgColor, isHeaderRequired: isHeaderRequired,
                                hyperlinkColumns: hyperlinkColumns, commaFormatRequired: commaFormatRequired);

                    if (!DataManager.IsNullOrEmptyDataTable(dtExcel))
                        //Posing the Edit settings in Excel Cells with Different Color
                        SetEditSettings(wsExcel, dtExcel, freezList);


                    //hide column
                    HideColumns(wsExcel, hideColumns);
                }

                // To set the Panes i.e borders of editable and nonEditable fields
                if (freezPaneFrom != 0 && freezPaneTo != 0)
                    wsExcel.View.FreezePanes(freezPaneFrom, freezPaneTo);

                //Set PasswordProtection
                SetPasswordProtection(wsExcel, passwordProtection, password);

                if (isExportRequired)
                {
                    //transmit the file by memory/file stream
                    TransmitExcelFile(package, fileName, isXlsx);
                }
                else
                {
                    byte[] data = package.GetAsByteArray();
                    string fileExtension = string.Empty;
                    if (isXlsx)
                    {
                        fileExtension = FormatXlsx;
                    }
                    else
                    {
                        fileExtension = FormatXls;
                    }
                    string path = fileName + fileExtension;
                    File.WriteAllBytes(path, data);
                }

                return true;
            }
        }





        public static byte[] CreateExcelForAPI(string fileName, DataSet dsData, string sheetName = DefaultSheetName,
                bool passwordProtection = false, string password = null,
                Dictionary<int, int> freezList = null, Dictionary<int, string> hideColumns = null,
                int freezPaneFrom = 0, int freezPaneTo = 0, bool enableDataValidations = false,
                DataTable dtValidation = null, bool isXlsx = true, bool isFormatRequired = false, bool headerBgColor = true,
                bool isHeaderRequired = true, bool isExportRequired = true, NameValueCollection dropdownColumns = null, int validationStartColumn = -1, bool isRate = false)
        {

            byte[] data = null;
            using (ExcelPackage package = new ExcelPackage())
            {
                //Load the sheet with one string column, one date column and a few random numbers.
                var wsExcel = package.Workbook.Worksheets.Add(sheetName);

                //set the width of the columns in the workSheet.
                wsExcel.DefaultColWidth = VectorConstants.TwentyFive;

                //create cells based on the DataTable and Formula to a particular
                foreach (DataTable dtExcel in dsData.Tables)
                {
                    wsExcel = AddDataToExcel(wsExcel, dtExcel, enableDataValidations, dtValidation, isFormatRequired,
                                  isHeaderAndBGColorReq: headerBgColor, isHeaderRequired: isHeaderRequired,
                                  dropdownColumns: dropdownColumns, validationStartColumn: validationStartColumn, isRateInventory: isRate);

                    if (!DataManager.IsNullOrEmptyDataTable(dtExcel))
                        //Posing the Edit settings in Excel Cells with Different Color
                        SetEditSettings(wsExcel, dtExcel, freezList);


                    //hide column
                    HideColumns(wsExcel, hideColumns);
                }

                // To set the Panes i.e borders of editable and nonEditable fields
                if (freezPaneFrom != 0 && freezPaneTo != 0)
                    wsExcel.View.FreezePanes(freezPaneFrom, freezPaneTo);

                //Set PasswordProtection
                SetPasswordProtection(wsExcel, passwordProtection, password);

                //if (isExportRequired)
                //{
                //    //transmit the file by memory/file stream
                //    TransmitExcelFile(package, fileName, isXlsx);
                //}
                //else
                //{
                data = package.GetAsByteArray();
                //    string fileExtension = string.Empty;
                //    if (isXlsx)
                //    {
                //        fileExtension = FormatXlsx;
                //    }
                //    else
                //    {
                //        fileExtension = FormatXls;
                //    }
                //    string path = fileName + fileExtension;
                //    File.WriteAllBytes(path, data);
                //}


                // }

            }
            return data;
        }



        public static bool CreateMultiSheetExcel(string fileName, DataSet dsData, string[] sheetName = null,
                    bool passwordProtection = false, string password = null,
                    Dictionary<int, int> freezList = null, Dictionary<int, string> hideColumns = null,
                    int freezPaneFrom = 0, int freezPaneTo = 0, bool enableDataValidations = false,
                    DataTable dtValidation = null, bool isXlsx = true, bool isFormatRequired = false, string xlsPath = "",
                    bool isExportRequired = true, string[] hyperlinkColumns = null)
        {
            using (ExcelPackage package = new ExcelPackage())
            {
                //create cells based on the DataTable and Formula to a particular
                for (int i = 0; i < dsData.Tables.Count; i++)
                {
                    if (sheetName == null || String.IsNullOrEmpty(sheetName[i]))
                    {
                        sheetName = new string[dsData.Tables.Count];
                        sheetName[i] = "Sheet" + (i + 1);
                    }
                    //Load the sheet with one string column, one date column and a few random numbers.
                    var wsExcel = package.Workbook.Worksheets.Add(sheetName[i]);

                    //set the width of the columns in the workSheet.
                    wsExcel.DefaultColWidth = VectorConstants.TwentyFive;

                    wsExcel = AddDataToExcel(wsExcel, dsData.Tables[i], enableDataValidations, dtValidation, isFormatRequired, isHeaderAndBGColorReq: false, hyperlinkColumns: hyperlinkColumns);

                    if (!DataManager.IsNullOrEmptyDataTable(dsData.Tables[i]))
                        //Posing the Edit settings in Excel Cells with Different Color
                        SetEditSettings(wsExcel, dsData.Tables[i], freezList);


                    //hide column
                    HideColumns(wsExcel, hideColumns);

                    // To set the Panes i.e borders of editable and nonEditable fields
                    if (freezPaneFrom != 0 && freezPaneTo != 0)
                        wsExcel.View.FreezePanes(freezPaneFrom, freezPaneTo);

                    //Set PasswordProtection
                    SetPasswordProtection(wsExcel, passwordProtection, password);

                }
                if (xlsPath != "")
                {
                    byte[] XlsBytes = package.GetAsByteArray();
                    File.WriteAllBytes(xlsPath, XlsBytes);

                    string Format = string.Empty, ContentType = string.Empty;
                    if (isXlsx)
                    {
                        Format = FormatXlsx;
                        ContentType = ContentTypeXlsx;
                    }
                    else
                    {
                        Format = FormatXls;
                        ContentType = ContentTypeXls;
                    }

                    if (isExportRequired)
                    {
                        var response = HttpContext.Current.Response;
                        response.Clear();
                        response.AddHeader(HeaderContent, HeaderAttachment + fileName + Format);
                        response.ContentType = ContentType;
                        response.BinaryWrite(XlsBytes);
                        response.End();
                    }
                }
                else
                {
                    //transmit the file by memory/file stream
                    TransmitExcelFile(package, fileName, isXlsx);
                }
                return true;
            }
        }

        public static byte[] CreateMultiSheetExcelAndDownload(string fileName, DataSet dsData, string[] sheetName = null,
                    bool passwordProtection = false, string password = null,
                    Dictionary<int, int> freezList = null, Dictionary<int, string> hideColumns = null,
                    int freezPaneFrom = 0, int freezPaneTo = 0, bool enableDataValidations = false,
                    DataTable dtValidation = null, bool isXlsx = true, bool isFormatRequired = false, string xlsPath = "",
                    bool isExportRequired = true, string[] hyperlinkColumns = null)
        {
            using (ExcelPackage package = new ExcelPackage())
            {
                byte[] data = null;

                //create cells based on the DataTable and Formula to a particular
                for (int i = 0; i < dsData.Tables.Count; i++)
                {
                    if (sheetName == null || String.IsNullOrEmpty(sheetName[i]))
                    {
                        sheetName = new string[dsData.Tables.Count];
                        sheetName[i] = "Sheet" + (i + 1);
                    }
                    //Load the sheet with one string column, one date column and a few random numbers.
                    var wsExcel = package.Workbook.Worksheets.Add(sheetName[i]);

                    //set the width of the columns in the workSheet.
                    wsExcel.DefaultColWidth = VectorConstants.TwentyFive;

                    wsExcel = AddDataToExcel(wsExcel, dsData.Tables[i], enableDataValidations, dtValidation, isFormatRequired, isHeaderAndBGColorReq: false, hyperlinkColumns: hyperlinkColumns);

                    if (!DataManager.IsNullOrEmptyDataTable(dsData.Tables[i]))
                        //Posing the Edit settings in Excel Cells with Different Color
                        SetEditSettings(wsExcel, dsData.Tables[i], freezList);


                    //hide column
                    HideColumns(wsExcel, hideColumns);

                    // To set the Panes i.e borders of editable and nonEditable fields
                    if (freezPaneFrom != 0 && freezPaneTo != 0)
                        wsExcel.View.FreezePanes(freezPaneFrom, freezPaneTo);

                    //Set PasswordProtection
                    SetPasswordProtection(wsExcel, passwordProtection, password);

                }
                data = package.GetAsByteArray();
                //return true;
                return data;
            }
        }

        public static void SetColumnsOrder(DataTable table, NameValueCollection columnNames)
        {
            int columnIndex = 0;
            if (columnNames != null && columnNames.Count > 0)
            {
                foreach (string key in columnNames.Keys)
                {
                    table.Columns[key].SetOrdinal(columnIndex);
                    columnIndex++;
                }
            }
        }

        /// <summary>
        /// Transmit Excel File to User through POpup
        /// </summary>
        /// <param name="path"></param>
        private static void TransmitExcelFile(ExcelPackage package, string fileName, bool isXlsx)
        {
            string Format = string.Empty, ContentType = string.Empty;
            if (isXlsx)
            {
                Format = FormatXlsx;
                ContentType = ContentTypeXlsx;
            }
            else
            {
                Format = FormatXls;
                ContentType = ContentTypeXls;
            }
            var response = HttpContext.Current.Response;
            response.Clear();
            response.AddHeader(HeaderContent, HeaderAttachment + fileName + Format);
            response.ContentType = ContentType;
            response.BinaryWrite(package.GetAsByteArray());
            response.End();
        }

        /// <summary>
        /// Set the Edit setting for Rows and Columns on Excel
        /// </summary>
        /// <param name="wsExcel"></param>
        /// <param name="dtExcel"></param>
        /// <param name="freezColumnsList"></param>
        /// <param name="freezRowsList"></param>
        private static void SetEditSettings(ExcelWorksheet wsExcel, DataTable dtExcel, Dictionary<int, int> freezColumnsList)
        {
            int rowCount = dtExcel.Rows.Count;
            int columnCount = dtExcel.Columns.Count;

            rowCount = rowCount + VectorConstants.One;

            //if freeze columns exists then 
            if (freezColumnsList != null && freezColumnsList.Count > 0)
                (from KeyValuePair<int, int> fColumn in freezColumnsList select fColumn).ToList().ForEach(item =>
                {
                    wsExcel.Cells[item.Key, item.Value, rowCount, columnCount].Style.Locked = false;
                    wsExcel.Cells[item.Key, item.Value, rowCount, columnCount].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    wsExcel.Cells[item.Key, item.Value, rowCount, columnCount].Style.Fill.BackgroundColor.SetColor(Color.White);
                });
        }

        /// <summary>
        /// Set Password Protection
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="passwordProtection"></param>
        /// <param name="password"></param>
        private static void SetPasswordProtection(ExcelWorksheet ws, bool passwordProtection, string password)
        {
            //if password protected then add protection
            if (passwordProtection)
                ws.Protection.SetPassword(password);

        }

        /// <summary>
        /// Add data to the Excel Sheet
        /// </summary>
        /// <param name="wsExcel"></param>
        /// <param name="dtData"></param>
        /// <param name="formula"></param>
        /// <param name="formulaColumn"></param>
        /// <returns></returns>
        private static ExcelWorksheet AddDataToExcel(ExcelWorksheet wsExcel, DataTable dtData,
                      bool EnableDataValidations, DataTable dtValidation = null, bool isFormatRequired = false,
                      bool isHeaderAndBGColorReq = true, bool isHeaderRequired = true,
                      NameValueCollection dropdownColumns = null, int validationStartColumn = -1,
                      string[] hyperlinkColumns = null, bool isRateInventory = false, bool commaFormatRequired = true)
        {
            //int rowsCount = dtData.Rows.Count;
            int columnCount = dtData.Columns.Count;

            if (isHeaderAndBGColorReq)
            {
                //Format all cells
                ExcelRange cols = wsExcel.Cells["A:XFD"];

                //fill the Style of the Column
                cols.Style.Fill.PatternType = ExcelFillStyle.Solid;


                Color bodyColor = ColorTranslator.FromHtml("#efefef");

                //backGround Color of the Column
                cols.Style.Fill.BackgroundColor.SetColor(bodyColor);
            }


            //multi Variable declaration.

            //use count variable to iterate through Rows
            int count = VectorConstants.One;
            bool isBold = false;
            int rangeFrom = 1, rangeTo = 0;
            /* for each row in excel sheet add data from the data table row  by columns*/
            foreach (DataRow drExcel in dtData.Rows)
            {
                if (Convert.ToString(drExcel[0]) == "Filters Applied")
                {
                    isBold = true;
                }
                else
                {
                    isBold = false;
                }

                count++;
                for (int colCount = 0; colCount < columnCount; colCount++)
                {
                    string currencyFormat = (dtData.Columns[colCount]).ExtendedProperties["currency"] != null ? (dtData.Columns[colCount]).ExtendedProperties["currency"].ToString() : null;
                    if (hyperlinkColumns == null)
                        AddCellsWithValues(wsExcel, count, colCount + VectorConstants.One, drExcel[colCount].ToString(),
                                dtData.Columns[colCount].DataType.ToString(), isFormatRequired, isBold, currencyFormat, commaFormatRequired);
                    else
                    {
                        bool valueExist = IsValueExistinList(dtData, hyperlinkColumns, colCount);
                        if (valueExist)
                        {
                            string ImageInfo = drExcel[colCount].ToString(), columnValue = string.Empty;
                            if (!string.IsNullOrEmpty(ImageInfo))
                            {
                                string[] ImageInfoArr = ImageInfo.Split(new char[] { '~' });
                                if (ImageInfoArr != null && ImageInfoArr.Length == 2)
                                {
                                    //AddCellsWithValues(wsExcel, count, colCount + VectorConstants.One, ImageInfoArr[1]);
                                    AddCellsWithValues(wsExcel, count, colCount + VectorConstants.One, ImageInfoArr[1], dtData.Columns[colCount].DataType.ToString(), isFormatRequired, isBold, currencyFormat);

                                    var cell = wsExcel.Cells[count, colCount + VectorConstants.One];
                                    cell.Hyperlink = new Uri(ImageInfoArr[0]);
                                }
                            }
                            else
                                AddCellsWithValues(wsExcel, count, colCount + VectorConstants.One, drExcel[colCount].ToString(),
                                    dtData.Columns[colCount].DataType.ToString(), isFormatRequired, isBold, currencyFormat);
                        }
                        else
                        {
                            AddCellsWithValues(wsExcel, count, colCount + VectorConstants.One, drExcel[colCount].ToString(),
                                  dtData.Columns[colCount].DataType.ToString(), isFormatRequired, isBold, currencyFormat);
                        }
                    }

                    if (!DataManager.IsNullOrEmptyDataTable(dtValidation))
                    {
                        string values = DataManager.GetRowByColumnFilter(dtValidation, "attributedesc", dtData.Columns[colCount].ColumnName, "attributeoptions");
                        CreateDropdownForCell(wsExcel, count, colCount, values, ref rangeFrom, ref rangeTo, isRateInventory);
                    }

                    if (dropdownColumns != null)
                    {
                        foreach (var column in dropdownColumns.Keys)
                        {
                            if (column.ToString() == dtData.Columns[colCount].ColumnName)
                            {
                                string values = drExcel[dropdownColumns.Get(column.ToString())].ToString();
                                CreateDropdownForCell(wsExcel, count, colCount, values, ref rangeFrom, ref rangeTo, isRateInventory);
                            }
                        }
                    }

                    //if Data Validation string is enables then only Set Data Validation in Column
                    if (EnableDataValidations && (validationStartColumn == -1 || colCount >= validationStartColumn))
                        switch (dtData.Columns[colCount].DataType.ToString())
                        {
                            case "System.Int64":
                                var validationInt = wsExcel.Cells[count, colCount + VectorConstants.One].DataValidation.AddIntegerDataValidation();
                                IntegerValidation(validationInt, dtData.Columns[colCount].ColumnName, dtValidation);
                                break;
                            case "System.Decimal":
                                var validationDecimal = wsExcel.Cells[count, colCount + VectorConstants.One].DataValidation.AddDecimalDataValidation();
                                DecimalValidation(validationDecimal, dtData.Columns[colCount].ColumnName, dtValidation);
                                break;
                            case "System.DateTime":
                                var validation = wsExcel.Cells[count, colCount + VectorConstants.One].DataValidation.AddDateTimeDataValidation();
                                DateTimeValidation(validation);
                                break;
                            case "System.Double":
                                var validationDouble = wsExcel.Cells[count, colCount + VectorConstants.One].DataValidation.AddDecimalDataValidation();
                                DecimalValidation(validationDouble, dtData.Columns[colCount].ColumnName, dtValidation);
                                break;
                        }
                }
            }

            if (isHeaderRequired)
                CreateColumnHeader(wsExcel, dtData, isHeaderColorRequired: isHeaderAndBGColorReq);
            else
            {
                wsExcel.DeleteRow(1, 1, true);
                wsExcel.Cells["A:XFD"].AutoFitColumns();
            }
            return wsExcel;
        }

        private static void CreateDropdownForCell(ExcelWorksheet wsExcel, int count, int colCount, string values, ref int rangeFrom, ref int rangeTo, bool isRateInventory = false)
        {
            if (!string.IsNullOrEmpty(values) && StringManager.IsNotEqual(values, "") && StringManager.IsNotEqual(values, "null"))
            {
                string[] dropdownValues = null;
                dropdownValues = values.Split(',');
                if (dropdownValues.Length > 0)
                {
                    CreateDropDown(wsExcel, count, (colCount + VectorConstants.One), dropdownValues, ref rangeFrom, ref rangeTo, isRateInventory);
                    wsExcel.Cells[count, (colCount + VectorConstants.One)].Style.Locked = true;
                }
            }
        }

        private static bool IsValueExistinList(DataTable dtData, string[] hyperlinkColumns, int colCount)
        {
            foreach (string item in hyperlinkColumns)
            {
                if (dtData.Columns[colCount].ColumnName == item)
                {
                    return true;
                }
            }
            return false;
        }

        private static string ApplyFormatToValue(DataRow dataRow, string columnDataType, bool isFormatRequired, int columnIndex)
        {
            string outPutValue = dataRow[columnIndex].ToString();
            if (isFormatRequired)
            {
                switch (columnDataType)
                {
                    case "System.Decimal":
                        outPutValue = string.Format(CultureInfo.CurrentCulture, "{0:F}", dataRow[columnIndex] == System.DBNull.Value ? null : (decimal?)Convert.ToDecimal(dataRow[columnIndex], CultureInfo.CurrentCulture));
                        break;
                    case "System.Double":
                        outPutValue = string.Format(CultureInfo.CurrentCulture, "{0:c}", dataRow[columnIndex] == System.DBNull.Value ? null : (Double?)dataRow[columnIndex]);
                        break;
                    case "System.DateTime":
                        outPutValue = string.Format(CultureInfo.CurrentCulture, "{0:d}", dataRow[columnIndex] == System.DBNull.Value ? null : (DateTime?)dataRow[columnIndex]);
                        break;
                }
            }

            return outPutValue;
        }


        /// <summary>
        /// ADD CELLS base on the Column and Value
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="rowcount"></param>
        /// <param name="coulumnCount"></param>
        /// <param name="value"></param>
        private static void AddCellsWithValues(ExcelWorksheet ws, int rowcount, int colCount, string value, string columnDataType,
            bool isFormatRequired = false, bool isBold = false, string currencyFormat = null, bool commaFormatRequired = true)
        {
            CreateCell(ws, rowcount, value, columnDataType, isFormatRequired, GetExcelColumnName(colCount), currencyFormat: currencyFormat, commaFormatRequired: commaFormatRequired);

        }

        /// <summary>
        /// Returns Column Name for Excel by column number..eg.,A,AA,ABA,AB
        /// </summary>
        /// <param name="columnNumber"></param>
        /// <returns></returns>
        private static string GetExcelColumnName(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName;
        }

        private static void CreateCell(ExcelWorksheet ws, int rowcount, string value, string columnDataType, bool isFormatRequired, string cellName,
            string currencyFormat = null, bool isBold = false, bool commaFormatRequired = true)
        {
            var cell = ws.Cells[cellName + rowcount];
            cell.Style.Numberformat.Format = GetFormatByCoumnDataType(columnDataType, commaFormatRequired);

            if (currencyFormat == "$")
            {
                cell.Style.Numberformat.Format = "$#,##0.00";
            }
            if (isBold)
                cell.Style.Font.Bold = true;

            if (!isFormatRequired)
                cell.Value = value;
            else
            {
                switch (columnDataType)
                {
                    case "System.Decimal":
                        if (!string.IsNullOrEmpty(value))
                            cell.Value = Convert.ToDecimal(value);
                        else
                            cell.Value = "";
                        break;
                    case "System.Double":
                        if (!string.IsNullOrEmpty(value))
                            cell.Value = Convert.ToDouble(value);
                        else
                            cell.Value = "";
                        break;
                    case "System.Int32":
                        if (!string.IsNullOrEmpty(value))
                            cell.Value = Convert.ToInt32(value);
                        else
                            cell.Value = "";
                        break;
                    case "System.UInt64":
                        if (!string.IsNullOrEmpty(value))
                            cell.Value = Convert.ToInt64(value);
                        else
                            cell.Value = "";
                        break;
                    default:
                        cell.Value = value;
                        break;
                }
            }
        }

        /// <summary>
        /// Add HeaderColumn Names in First Row
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="dtData"></param>
        private static void CreateColumnHeader(ExcelWorksheet ws, DataTable dtData, bool isHeaderColorRequired = true)
        {
            int count = 0;
            //int endRow = dtData.Rows.Count;

            Color colour = ColorTranslator.FromHtml("#2878b1");
            Color colourHeader = Color.White;


            //for each column add header
            (from DataColumn dColumn in dtData.Columns select dColumn).ToList().ForEach(item =>
            {
                count++;
                ws.Cells[VectorConstants.One, count].Value = item.ColumnName.ToString();                     //set value
                ws.Cells[VectorConstants.One, count].Style.Font.Bold = true;                                 //set style BOLD
                ws.Cells[VectorConstants.One, count].Style.Locked = true;                                    //Lock the Column headers

                //commented this line and moved to this line after the foreach by naresh for excel downlod issue                                                                                        //ws.Cells[VectorConstants.One, count].Style.Numberformat.Format = GetFormatByCoumnDataType(item.DataType.ToString());
                // ws.Cells["A:XFD"].AutoFitColumns();
                if (isHeaderColorRequired)
                {
                    ws.Cells[VectorConstants.One, count].Style.Font.Color.SetColor(colourHeader);
                    ws.Cells[VectorConstants.One, count].Style.Fill.BackgroundColor.SetColor(colour);   //Set the background color
                }
            });
            //naresh added
            ws.Cells["A:XFD"].AutoFitColumns();
        }

        private static string GetFormatByCoumnDataType(string columnDataType, bool commaFormatRequired)
        {
            string outPutValue = "General";
            switch (columnDataType)
            {
                case "System.Decimal":
                    if (commaFormatRequired)
                        outPutValue = "#,##0.00";
                    else
                        outPutValue = "0.00";
                    break;
            }


            return outPutValue;
        }


        /// <summary>
        /// Hide Columns Based on Column Numbers 
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="hideColumns"></param>
        private static void HideColumns(ExcelWorksheet ws, Dictionary<int, string> hideColumns)
        {
            if (hideColumns != null)
                if (hideColumns.Count > 0)
                    //hiding the Columns for each dictionary item
                    (from KeyValuePair<int, string> hidColumn in hideColumns select hidColumn).ToList().ForEach(item => ws.Column(item.Key).Hidden = true);
        }

        private static void CreateDropDown(ExcelWorksheet ws, int rowNumber, int columnNumber, string[] dropdownValues, ref int rangeFrom, ref int rangeTo, bool isRateInventory = false)
        {
            var addDropDown = ws.Cells[rowNumber, columnNumber].DataValidation.AddListDataValidation();
            if (!isRateInventory)
            {
                foreach (var value in dropdownValues)
                {
                    addDropDown.AllowBlank = false;
                    addDropDown.Formula.Values.Add(value);
                }
            }
            else
            {
                if (rangeFrom == 1)
                {
                    ws.Cells["O1"].Value = "AllRateNames";
                    rangeFrom++;
                    rangeTo++;
                }
                rangeTo += dropdownValues.Length;
                int indexValue = 0;
                for (int i = rangeFrom; i <= rangeTo; ++i)
                {
                    ws.Cells["O" + i].Value = dropdownValues[indexValue++];
                }
                addDropDown.AllowBlank = false;
                //addDropDown.Formula.ExcelFormula = string.Format("=OFFSET(Sheet1!${0}${1},0,0,MATCH(\" * \",Sheet1!${0}${1}:${0}{2},-1),1)","P", rangeFrom, rangeTo);
                addDropDown.Formula.ExcelFormula = "$O$" + rangeFrom + ":$O$" + rangeTo;
                rangeFrom = rangeTo + 1;
            }
        }

        public static bool CreateExcelForWindows(string pathWithFileName, DataSet dsData, string sheetName, int freezPaneFrom, int freezPaneTo, bool passwordProtection, string password)
        {
            using (ExcelPackage package = new ExcelPackage())
            {
                //Load the sheet with one string column, one date column and a few random numbers.
                var wsExcel = package.Workbook.Worksheets.Add(sheetName);

                //set the width of the columns in the workSheet.
                wsExcel.DefaultColWidth = 25;

                //create cells based on the DataTable and Formula to a particular
                foreach (DataTable dtExcel in dsData.Tables)
                {
                    wsExcel = AddDataToExcel(wsExcel, dtExcel, false);

                    //if (dtExcel.Rows.Count > 0)
                    //    //Posing the Edit settings in Excel Cells with Different Color
                    //    SetEditSettings(wsExcel, dtExcel);
                }
                if (freezPaneFrom != 0 && freezPaneTo != 0)
                    wsExcel.View.FreezePanes(freezPaneFrom, freezPaneTo);
                SetPasswordProtection(wsExcel, passwordProtection, password);

                DirectoryInfo di = new DirectoryInfo(Directory.GetParent(pathWithFileName).ToString());
                if (!di.Exists)
                    di.Create();
                File.WriteAllBytes(pathWithFileName, package.GetAsByteArray());
                //System.Diagnostics.Process.Start(pathWithFileName);
                return true;
            }
        }


        #endregion

        #region Read Excel

        /// <summary>
        /// Using WPPlus Read Excel
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static DataTable ReadExcel(string path, bool hasHeader)
        {
            //Create Package and Load package with Given Excel File
            var package = new ExcelPackage();
            package.Load(new FileInfo(path).OpenRead());



            return GetDataFromPackage(package, hasHeader);
        }

        /// <summary>
        /// Using WPPlus Read Excel
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static DataTable ReadExcelWithIndex(string path, bool hasHeader)
        {
            //Create Package and Load package with Given Excel File
            var package = new ExcelPackage();
            package.Load(new FileInfo(path).OpenRead());

            //Get the WorkSheep Instance of the Excel
            var wsExcel = package.Workbook.Worksheets.First();
            return GetdataTaleFromworkSheetWithIndex(hasHeader, wsExcel, 1);
        }

        /// <summary>
        /// Using WPPlus Read Excel
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static DataTable ReadExcelWithIndexCellLimit(string path, bool hasHeader, int validDataCell = 0)
        {
            //Create Package and Load package with Given Excel File
            var package = new ExcelPackage();
            package.Load(new FileInfo(path).OpenRead());

            //Get the WorkSheep Instance of the Excel
            var wsExcel = package.Workbook.Worksheets.First();
            return GetdataTaleFromworkSheetWithIndexAndCellLimit(hasHeader, wsExcel, 1, validDataCell);
        }

        /// <summary>
        /// Read Excel from Stream with out saving File
        /// </summary>
        /// <param name="dataStream"></param>
        /// <param name="hasHeader"></param>
        /// <returns></returns>
        public static DataTable ReadExcel(Stream dataStream, bool hasHeader)
        {
            var package = new ExcelPackage(dataStream);
            return GetDataFromPackage(package, hasHeader);
        }

        /// <summary>
        /// Read Excel from Stream with out saving File for Multiple WorkSheets
        /// </summary>
        /// <param name="dataStream"></param>
        /// <param name="hasHeader"></param>
        /// <returns></returns>
        public static DataSet ReadExcelMultipleSheets(Stream dataStream, bool hasHeader, string dataSetName = "Master", int headerRowNumber = 0)
        {
            var package = new ExcelPackage(dataStream);
            return ReadExcelMultipleWorkSheetDataFromWorkbook(package, hasHeader, dataSetName, headerRowNumber);
        }

        /// <summary>
        /// Read Excel from Stream with out saving File for Multiple WorkSheets
        /// </summary>
        /// <param name="dataStream"></param>
        /// <param name="hasHeader"></param>
        /// <returns></returns>
        public static DataSet ReadExcelMultipleSheets(string excelFilePath, bool hasHeader, string dataSetName = "Master", int headerRowNumber = 0)
        {
            var package = new ExcelPackage();
            package.Load(new FileInfo(excelFilePath).OpenRead());
            return ReadExcelMultipleWorkSheetDataFromWorkbook(package, hasHeader, dataSetName, headerRowNumber);
        }



        /// <summary>
        /// Create Data Table with the Content in Package
        /// </summary>
        /// <param name="package"></param>
        /// <param name="hasHeader"></param>
        /// <returns></returns>
        private static DataTable GetDataFromPackage(ExcelPackage package, bool hasHeader)
        {
            //Get the WorkSheep Instance of the Excel
            var wsExcel = package.Workbook.Worksheets.First();

            DataTable tblExcel = GetdataTaleFromworkSheet(hasHeader, wsExcel, 1);

            //return the excel table
            return tblExcel;
        }

        private static DataTable GetdataTaleFromworkSheet(bool hasHeader, ExcelWorksheet wsExcel, int headerRowNumber = 0)
        {
            //Create table instance to load Excel Data.
            DataTable tblExcel = new DataTable();
            tblExcel.Locale = CultureInfo.InvariantCulture;

            if (wsExcel.Dimension != null)
            {
                //using each cell of WorkSheet Create DataTable Columns.
                foreach (var firstRowCell in wsExcel.Cells[headerRowNumber, VectorConstants.One, headerRowNumber, wsExcel.Dimension.End.Column])
                {
                    //if not the header is null add Column to the Table
                    // if (!string.IsNullOrEmpty(firstRowCell.Text))
                    string header = string.Empty;
                    if (firstRowCell.Text.Contains("\r\n"))
                        header = firstRowCell.Text.Replace("\r\n", "");
                    else
                        header = firstRowCell.Text;

                    tblExcel.Columns.Add(hasHeader ? header : string.Format(CultureInfo.CurrentCulture, "Column {0}", firstRowCell.Start.Column));
                }


                //Header YES then 2 (If Excel sheet has Header then Start reading from ROW 2)
                //Header NO then 1 (IF Excel Sheet has No Header then Start reading from Row 1)
                var startRow = 0;

                if (hasHeader && headerRowNumber == 0)
                {
                    startRow = VectorConstants.Two;
                }
                else if (hasHeader && headerRowNumber != 0)
                {
                    startRow = headerRowNumber + VectorConstants.One;
                }


                //read Each row from Excel Sheet
                for (var rowNum = startRow; rowNum <= wsExcel.Dimension.End.Row; rowNum++)
                {
                    //Get Row from Excel Sheet
                    var wsRow = wsExcel.Cells[rowNum, VectorConstants.One, rowNum, wsExcel.Dimension.End.Column];

                    //Create New Row instance
                    var row = tblExcel.NewRow();

                    //Add Cell data to the above row instance
                    foreach (var cell in wsRow)
                    {
                        if (cell == null)
                            continue;
                        if (tblExcel.Columns.Count > (cell.Start.Column - VectorConstants.One))
                            //add till the header cell is not empty
                            // if (!string.IsNullOrEmpty(cell.Text))

                            cell.Value = cell.Value != null && cell.Value.GetType().Name == "DateTime" ? ((DateTime)cell.Value).ToString("MM/dd/yyyy") : cell.Value;

                        row[cell.Start.Column - VectorConstants.One] = string.IsNullOrEmpty(cell.Text) ? cell.Value : cell.Text;
                        //else
                        //    row[cell.Start.Column - VectorConstants.One] = string.Empty;
                    }
                    //Add row to the DataTable
                    tblExcel.Rows.Add(row);
                }
            }
            return tblExcel;
        }

        private static DataTable GetdataTaleFromworkSheetWithIndex(bool hasHeader, ExcelWorksheet wsExcel, int headerRowNumber = 0)
        {
            //Create table instance to load Excel Data.
            DataTable tblExcel = new DataTable();
            tblExcel.Locale = CultureInfo.InvariantCulture;

            if (wsExcel.Dimension != null)
            {
                //using each cell of WorkSheet Create DataTable Columns.
                foreach (var firstRowCell in wsExcel.Cells[headerRowNumber, VectorConstants.One, headerRowNumber, wsExcel.Dimension.End.Column])
                {
                    //if not the header is null add Column to the Table
                    // if (!string.IsNullOrEmpty(firstRowCell.Text))
                    string header = string.Empty;
                    if (firstRowCell.Text.Contains("\r\n"))
                        header = firstRowCell.Text.Replace("\r\n", "");
                    else
                        header = firstRowCell.Text;

                    tblExcel.Columns.Add(hasHeader ? header : string.Format(CultureInfo.CurrentCulture, "Column {0}", firstRowCell.Start.Column));
                }
                tblExcel.Columns.Add("Index");

                //Header YES then 2 (If Excel sheet has Header then Start reading from ROW 2)
                //Header NO then 1 (IF Excel Sheet has No Header then Start reading from Row 1)
                var startRow = 0;

                if (hasHeader && headerRowNumber == 0)
                {
                    startRow = VectorConstants.Two;
                }
                else if (hasHeader && headerRowNumber != 0)
                {
                    startRow = headerRowNumber + VectorConstants.One;
                }


                //read Each row from Excel Sheet
                for (var rowNum = startRow; rowNum <= wsExcel.Dimension.End.Row; rowNum++)
                {
                    //Get Row from Excel Sheet
                    var wsRow = wsExcel.Cells[rowNum, VectorConstants.One, rowNum, wsExcel.Dimension.End.Column];

                    //Create New Row instance
                    var row = tblExcel.NewRow();

                    //Add Cell data to the above row instance
                    foreach (var cell in wsRow)
                    {
                        if (tblExcel.Columns.Count > (cell.Start.Column - VectorConstants.One))
                            //add till the header cell is not empty
                            // if (!string.IsNullOrEmpty(cell.Text))
                            if (cell.Value != null)
                            {
                                cell.Value = cell.Value.GetType().Name == "DateTime" ? ((DateTime)cell.Value).ToString("MM/dd/yyyy") : cell.Value;
                            }

                        row[cell.Start.Column - VectorConstants.One] = string.IsNullOrEmpty(cell.Text) ? cell.Value : cell.Text;
                        //else
                        //    row[cell.Start.Column - VectorConstants.One] = string.Empty;
                    }
                    row[tblExcel.Columns.Count - VectorConstants.One] = rowNum;
                    //Add row to the DataTable
                    tblExcel.Rows.Add(row);
                }
            }
            return tblExcel;
        }

        private static DataTable GetdataTaleFromworkSheetWithIndexAndCellLimit(bool hasHeader, ExcelWorksheet wsExcel, int headerRowNumber = 0, int validDataCell = 0)
        {
            //Create table instance to load Excel Data.
            DataTable tblExcel = new DataTable();
            tblExcel.Locale = CultureInfo.InvariantCulture;

            if (wsExcel.Dimension != null)
            {
                //using each cell of WorkSheet Create DataTable Columns.
                foreach (var firstRowCell in wsExcel.Cells[headerRowNumber, VectorConstants.One, headerRowNumber, wsExcel.Dimension.End.Column])
                {
                    //if not the header is null add Column to the Table
                    // if (!string.IsNullOrEmpty(firstRowCell.Text))
                    string header = string.Empty;
                    if (firstRowCell.Text.Contains("\r\n"))
                        header = firstRowCell.Text.Replace("\r\n", "");
                    else
                        header = firstRowCell.Text;

                    tblExcel.Columns.Add(hasHeader ? header : string.Format(CultureInfo.CurrentCulture, "Column {0}", firstRowCell.Start.Column));
                }
                tblExcel.Columns.Add("Index");

                //Header YES then 2 (If Excel sheet has Header then Start reading from ROW 2)
                //Header NO then 1 (IF Excel Sheet has No Header then Start reading from Row 1)
                var startRow = 0;

                if (hasHeader && headerRowNumber == 0)
                {
                    startRow = VectorConstants.Two;
                }
                else if (hasHeader && headerRowNumber != 0)
                {
                    startRow = headerRowNumber + VectorConstants.One;
                }


                //read Each row from Excel Sheet
                for (var rowNum = startRow; rowNum <= wsExcel.Dimension.End.Row; rowNum++)
                {
                    //Get Row from Excel Sheet
                    var wsRow = wsExcel.Cells[rowNum, VectorConstants.One, rowNum, wsExcel.Dimension.End.Column];

                    //Create New Row instance
                    var row = tblExcel.NewRow();

                    //Add Cell data to the above row instance
                    foreach (var cell in wsRow)
                    {
                        if (tblExcel.Columns.Count > (cell.Start.Column - VectorConstants.One))
                            //add till the header cell is not empty
                            // if (!string.IsNullOrEmpty(cell.Text))
                            if (cell.Value != null)
                            {
                                cell.Value = cell.Value.GetType().Name == "DateTime" ? ((DateTime)cell.Value).ToString("MM/dd/yyyy") : cell.Value;
                            }
                        if (cell.Start.Column - VectorConstants.One > validDataCell)
                        {
                            row[validDataCell - VectorConstants.One] = string.IsNullOrEmpty(cell.Text) ? cell.Value : cell.Text;
                        }
                        else
                        {
                            row[cell.Start.Column - VectorConstants.One] = string.IsNullOrEmpty(cell.Text) ? cell.Value : cell.Text;
                        }
                        //else
                        //    row[cell.Start.Column - VectorConstants.One] = string.Empty;
                    }
                    row[tblExcel.Columns.Count - VectorConstants.One] = rowNum;
                    //Add row to the DataTable
                    tblExcel.Rows.Add(row);
                }
            }
            return tblExcel;
        }
        /// <summary>
        /// Will return Data set with tables belonging to each worksheet in WorkBook
        /// </summary>
        /// <param name="package"></param>
        /// <param name="hasHeader"></param>
        /// <param name="dataSetName"></param>
        /// <returns></returns>
        private static DataSet ReadExcelMultipleWorkSheetDataFromWorkbook(ExcelPackage package, bool hasHeader, string dataSetName = "MasterData", int headerRowNumber = 0)
        {
            DataSet wsData = new DataSet(dataSetName);


            foreach (ExcelWorksheet worksheet in package.Workbook.Worksheets)
            {
                wsData.Tables.Add(GetdataTaleFromworkSheet(hasHeader, worksheet, headerRowNumber));
            }

            return wsData;
        }

        #endregion

        #region MISC (CAN BE DELETED)

        ///// <summary>
        ///// Formatting cells and setting protection levels STYLE
        ///// </summary>
        ///// <param name="ws"></param>
        ///// <param name="dtData"></param>
        ///// <param name="protectionFrom"></param>
        ///// <param name="ProtectionTo"></param>
        //private void FormatCellsDataType(ExcelWorksheet ws, DataTable dtData, int protectionFrom = Zero, int ProtectionTo = Zero)
        //{
        //    int rowCount = dtData.Rows.Count;
        //    int columnCount = dtData.Columns.Count;

        //    //set the Protection Level STYLE 
        //    if ((protectionFrom == Zero && ProtectionTo != Zero) || (protectionFrom != Zero && ProtectionTo != Zero))
        //    {
        //        ws.Cells[protectionFrom, ProtectionTo, rowCount + 1, rowCount + 1].Style.Locked = false;
        //        ws.Cells[protectionFrom, ProtectionTo, rowCount + 1, rowCount + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //        ws.Cells[protectionFrom, ProtectionTo, rowCount + 1, rowCount + 1].Style.Fill.BackgroundColor.SetColor(Color.White);

        //    }
        //}

        #endregion

        #region DataValidation Methods            

        /// <summary>
        /// Validation messages for Decimal Fields
        /// </summary>
        /// <param name="validation"></param>
        private static void DecimalValidation(IExcelDataValidationDecimal validation, string columnText, DataTable dtValidation)
        {
            validation.ShowErrorMessage = true;
            validation.ErrorTitle = EnumMgr.Desc(VectorEnums.ExcelMessage.InvalidData);
            validation.Prompt = EnumMgr.Desc(VectorEnums.ExcelMessage.IntegerDecimalPromptMsg);
            if (dtValidation == null)
            {
                validation.Error = EnumMgr.Desc(VectorEnums.ExcelMessage.IntegerDecimalErrorMessage);
                validation.Formula.Value = 0;
                validation.Formula2.Value = VectorConstants.MaxValue;
            }
            else
            {
                validation.Error = DataManager.GetRowByColumnFilter(dtValidation, "Attribute_Desc", columnText, "ErrorMessage") == null ?
                  EnumMgr.Desc(VectorEnums.ExcelMessage.IntegerDecimalErrorMessage) : DataManager.GetRowByColumnFilter(dtValidation, "Attribute_Desc", columnText, "ErrorMessage");
                validation.Formula.Value = DataManager.GetRowByColumnFilter(dtValidation, "Attribute_Desc", columnText, "ErrorMessage") == null ?
                        0 : Convert.ToInt16(DataManager.GetRowByColumnFilter(dtValidation, "Attribute_Desc", columnText, "MinValue"));
                validation.Formula2.Value = DataManager.GetRowByColumnFilter(dtValidation, "Attribute_Desc", columnText, "ErrorMessage") == null ?
                        VectorConstants.MaxValue : Convert.ToInt32(DataManager.GetRowByColumnFilter(dtValidation, "Attribute_Desc", columnText, "MaxValue"));
            }
            validation.Operator = ExcelDataValidationOperator.between;
        }

        /// <summary>
        /// Validation messages for Integer Fields
        /// </summary>
        /// <param name="validation"></param>
        private static void IntegerValidation(IExcelDataValidationInt validation, string columnText, DataTable dtValidation)
        {
            validation.ShowErrorMessage = true;
            validation.ErrorTitle = EnumMgr.Desc(VectorEnums.ExcelMessage.InvalidData);
            validation.Prompt = "Enter value here";
            if (dtValidation == null)
            {
                validation.Error = EnumMgr.Desc(VectorEnums.ExcelMessage.IntegerDecimalErrorMessage);
                validation.Formula.Value = 0;
                validation.Formula2.Value = VectorConstants.MaxValue;
            }
            else
            {
                validation.Error = DataManager.GetRowByColumnFilter(dtValidation, "Attribute_Desc", columnText, "ErrorMessage") == null ?
                  EnumMgr.Desc(VectorEnums.ExcelMessage.IntegerDecimalErrorMessage) : DataManager.GetRowByColumnFilter(dtValidation, "Attribute_Desc", columnText, "ErrorMessage");
                validation.Formula.Value = DataManager.GetRowByColumnFilter(dtValidation, "Attribute_Desc", columnText, "ErrorMessage") == null ?
                        0 : Convert.ToInt16(DataManager.GetRowByColumnFilter(dtValidation, "Attribute_Desc", columnText, "MinValue"));
                validation.Formula2.Value = DataManager.GetRowByColumnFilter(dtValidation, "Attribute_Desc", columnText, "ErrorMessage") == null ?
                        VectorConstants.MaxValue : Convert.ToInt32(DataManager.GetRowByColumnFilter(dtValidation, "Attribute_Desc", columnText, "MaxValue"));
            }
            validation.Operator = ExcelDataValidationOperator.between;
        }

        /// <summary>
        /// Validation messages for DataTime Fields
        /// </summary>
        /// <param name="validation"></param>
        private static void DateTimeValidation(IExcelDataValidationDateTime validation)
        {
            validation.ShowErrorMessage = true;
            validation.ErrorTitle = EnumMgr.Desc(VectorEnums.ExcelMessage.InvalidDate);
            validation.Error = EnumMgr.Desc(VectorEnums.ExcelMessage.ExcelInvalidDate);
            validation.Prompt = EnumMgr.Desc(VectorEnums.ExcelMessage.ExcelDatePrompt);
            //validation.Formula.Value = DateTime.Parse("1900-01-01", CultureInfo.CurrentCulture);
            validation.Formula.Value = DateTime.Parse("01/01/1900", CultureInfo.CurrentCulture);
            //validation.Formula2.Value = DateTime.Parse("1900-01-01", CultureInfo.CurrentCulture);
            validation.Formula2.Value = DateTime.Parse("01/01/1900", CultureInfo.CurrentCulture);
            validation.Operator = ExcelDataValidationOperator.greaterThan;
        }

        #endregion

        #region CellProtection

        /// <summary>
        /// Returns Dictionary object which will provide the Rows and Columns that
        /// have to be given Read and  Write operation
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, int> CreateDictionaryForRemovingCellProtection(int fromRow, int fromColumn)
        {
            //Dictionary <fromRow,fromColumn>
            Dictionary<int, int> enableColumns = new Dictionary<int, int>();

            //enable protection from row 2,column 5 
            //i.e Enable editing from row2 of Column 6 to end
            //other columns i.e 1,2,3,4,5 are disabled
            enableColumns.Add(fromRow, fromColumn);

            return enableColumns;
        }


        #endregion

        #region Export Datatable to Excel

        /// <summary>
        /// Explore Data to Excel file format.
        /// </summary>
        /// <param name="dtData"></param>
        /// <param name="fileName"></param>
        /// <param name="Header"></param>
        /// <param name="HeadingInfo"></param>
        /// <param name="Mode"></param>
        /// <param name="excelformat"></param>
        /// <param name="ColumnHeadersFormat"></param>
        /// <param name="ReqColumns"></param>
        public static void ExportDatatableToExcel(DataTable dtData, string fileName, string fileExtension, string excelformat, bool headerRequired = true)
        {
            if (!DataManager.IsNullOrEmptyDataTable(dtData))
            {
                GridView gvExport = FormatingExcelGridView(dtData, excelformat);
                gvExport.HeaderRow.Visible = headerRequired;
                HttpResponse response = HttpContext.Current.Response;
                response.Cache.SetExpires(DateTime.Now.AddSeconds(1));
                response.ContentType = "application/vnd.ms-excel";
                response.Charset = "";
                response.Write("<html xmlns:x=\"urn:schemas-microsoft-com:office:excel\">");
                response.Write("\r\n");
                response.Write("<style>  .cssnumber " + "\r\n" + "{mso-number-format:\"" + @"\@" + "\"" + "; text-align:left; } " + "\r\n  .cssdate " + "\r\n" + "{mso-number-format:Short Date; } " + "\r\n" + " .csscurrency " + "\r\n" + "{mso-number-format:\"" + "0\\.00" + "\"" + "; text-align:right;} " + "\r\n" + " .cssdecimal " + "\r\n" + "{mso-number-format:\"" + @"0\.00" + "\"" + "; text-align:right;} " + "\r\n" + " .cssspecnumber " + "\r\n" + "{mso-number-format:\"" + @"0" + "\"" + "; text-align:right;} " + "\r\n" + "</style>");

                using (StringWriter swExport = new StringWriter(CultureInfo.CurrentCulture))
                {
                    using (HtmlTextWriter hwExport = new HtmlTextWriter(swExport))
                    {
                        gvExport.RenderControl(hwExport);
                        response.AppendHeader("content-disposition", "attachment;filename=\"" + fileName + fileExtension + "\"");
                        response.Write(swExport.ToString());
                    }
                }
                response.End();
            }
        }

        public static void ExportAndSaveDatatableToExcel(DataTable dtData, string fileName, string fileExtension, string excelformat)
        {
            if (!DataManager.IsNullOrEmptyDataTable(dtData))
            {

                StringWriter excelFormatWriter = new StringWriter(CultureInfo.CurrentCulture);
                excelFormatWriter.Write("<html xmlns=\"urnchemas-microsoft-comffice:excel\">");
                excelFormatWriter.Write("\r\n");
                excelFormatWriter.Write("<style> .cssnumber " + "\r\n" + "{mso-number-format:\"" + @"\@" + "\"" + "; text-align:left; } " + "\r\n .cssdate " + "\r\n" + "{mso-number-format:\"" + "Short Date" + "\"" + "; } " + "\r\n" + " .cssdecimal " + "\r\n" + "{mso-number-format:\"" + @"0\.00" + "\"" + "; text-align:right;} " + "\r\n" + "</style>");
                excelFormatWriter.Write("<div align=center><h4></h4></div>");
                excelFormatWriter.Write("<div align=left></div>");
                HtmlTextWriter htmlExcelWriter = new HtmlTextWriter(excelFormatWriter);
                GridView gvExport = FormatingExcelGridView(dtData, excelformat);
                gvExport.RenderControl(htmlExcelWriter);
                StreamWriter excelWriter = new StreamWriter(fileName + fileExtension);
                excelWriter.WriteLine("\r\n");
                excelWriter.WriteLine(htmlExcelWriter.InnerWriter.ToString());
                excelWriter.Close();
            }
        }

        /// <summary>
        /// Formatting DataTable into Excel format.
        /// </summary>
        /// <param name="dtData"></param>
        /// <param name="excelformat"></param>
        /// <param name="ColumnHeadersFormat"></param>
        /// <param name="ReqColumns"></param>
        /// <returns></returns>
        private static GridView FormatingExcelGridView(DataTable dtData, string excelformat)
        {
            //Declare Grid view which will be used to export             
            GridView gvGlobal = new GridView();

            //Step 1: Bind Data table to to grid view
            gvGlobal.DataSource = dtData;
            gvGlobal.DataBind();

            //Step 2 :  Excel Formatting
            // e.g. "0:cssnumber,1:cssdate"  here 0 is index of the gridview cell and css name seperated by , 
            char[] cellseparator = new char[] { ',' };
            char[] cssseparator = new char[] { ':' };

            //declare arraylist to hold cellindex and cssname
            ArrayList alCells = new ArrayList();
            ArrayList alCSS = new ArrayList();
            string[] strFormats;

            //if format exists
            if (string.IsNullOrEmpty(excelformat) == false)
            {
                //Split to get no of cells send for excel format 
                strFormats = excelformat.Split(cellseparator);

                foreach (string strcellformats in strFormats)
                {

                    //split each format to get index and css class name 
                    string[] strcss = strcellformats.Split(cssseparator);

                    if (strcss.Length == 2)
                    {
                        alCells.Add(strcss[0].ToString());
                        alCSS.Add(strcss[1].ToString());
                    }

                }
            }

            //Step 3: Set format(css) to gridview cells
            foreach (GridViewRow gvr in gvGlobal.Rows)
            {
                foreach (TableCell tc in gvr.Cells)
                {
                    if (alCells.Contains(Convert.ToString(gvr.Cells.GetCellIndex(tc), CultureInfo.CurrentCulture)))
                    {
                        tc.Attributes.Add("class", Convert.ToString(alCSS[alCells.IndexOf(Convert.ToString(gvr.Cells.GetCellIndex(tc), CultureInfo.CurrentCulture))], CultureInfo.CurrentCulture));
                    }
                    else
                    {
                        tc.Attributes.Add("class", "cssnumber");
                    }

                }
            }

            gvGlobal.EnableViewState = false;
            return gvGlobal;
        }

        /// <summary>
        /// OverLoad Function to Export Grid to Excel File with Cell Formating and Customize Column Names
        /// 
        /// Step 1 : Column Name Formating
        /// Step 2 : Set column Names to datatable based on column index
        /// Step 3 : Bind Datatable to gridview
        /// Step 4 : Excel Formatting
        /// Step 5 : Set format(css) to gridview cells 
        /// Step 6 : Export to Excel File 
        /// 
        /// </summary>
        /// <param name="dtData"></param>
        /// <param name="FileName"></param>
        /// <param name="Header"></param>
        /// <param name="HeadingInfo"></param>
        /// <param name="excelFormat"></param>
        /// <param name="ColumnHeadersFormat"></param>

        public static void ExportGridViewToExcel(DataTable dtData, string fileName, string header, string headingInfo, string excelFormat, string columnHeadersFormat)
        {

            //Declare Grid view which will be used to export 
            GridView gvGlobal = new GridView();


            //Step 1 : Column Name Formating
            // e.g. "0:ColumnName,1:ColumnName"  here 0 is index of the datatable and its respective column Name
            char[] columnSeparator = new char[] { ',' };
            char[] headerSeparator = new char[] { ':' };

            //declare arraylist to hold ColumnIndex and ColumnName

            ArrayList alColumns = new ArrayList();
            ArrayList alName = new ArrayList();

            //if format exists
            if (string.IsNullOrEmpty(columnHeadersFormat) == false)
            {
                //Split to get no of Columns send for Column Format 
                string[] strColumnFormats = columnHeadersFormat.Split(columnSeparator);

                foreach (string strEachColumn in strColumnFormats)
                {

                    //split each format to get index and Column Names 
                    string[] strColumns = strEachColumn.Split(headerSeparator);

                    //check of lenght 
                    if (strColumns.Length == 2)
                    {
                        alColumns.Add(strColumns[0].ToString());  // Column Index
                        alName.Add(strColumns[1].ToString());     //Column Name
                    }

                }
            }

            //Step 2: Set column Names to datatable based on column index
            for (int i = 0; i < alColumns.Count; i++)
            {
                dtData.Columns[Convert.ToInt32(alColumns[i])].ColumnName = alName[i].ToString();
            }

            //Step 3: Bind Datatable to to gridview
            gvGlobal.DataSource = dtData;
            gvGlobal.DataBind();


            //Step 4 :  Excel Formatting
            // e.g. "0:cssnumber,1:cssdate"  here 0 is index of the gridview cell and css name seperated by , 
            char[] cellseparator = new char[] { ',' };
            char[] cssseparator = new char[] { ':' };

            //declare arraylist to hold cellindex and cssname
            ArrayList alCells = new ArrayList();
            ArrayList alCSS = new ArrayList();

            //if format exists
            if (string.IsNullOrEmpty(excelFormat) == false)
            {
                //Split to get no of cells send for excel format 
                string[] strFormats = excelFormat.Split(cellseparator);

                foreach (string strcellformats in strFormats)
                {

                    //split each format to get index and css class name 
                    string[] strcss = strcellformats.Split(cssseparator);

                    if (strcss.Length == 2)
                    {
                        alCells.Add(strcss[0].ToString());
                        alCSS.Add(strcss[1].ToString());
                    }

                }
            }

            //Step 5: Set format(css) to gridview cells
            foreach (GridViewRow gvr in gvGlobal.Rows)
            {
                foreach (TableCell tc in gvr.Cells)
                {
                    if (alCells.Contains(Convert.ToString(gvr.Cells.GetCellIndex(tc))))
                    {
                        tc.Attributes.Add("class", alCSS[alCells.IndexOf(Convert.ToString(gvr.Cells.GetCellIndex(tc)))].ToString());
                    }
                    else
                    {
                        tc.Attributes.Add("class", "cssnumber");
                    }

                }
            }


            //Step 6 : Export to Excel File 

            HttpContext.Current.Response.Cache.SetExpires(DateTime.Now.AddSeconds(1));
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.Charset = "";
            gvGlobal.EnableViewState = false;
            HttpContext.Current.Response.Write("<html xmlns:x=\"urn:schemas-microsoft-com:office:excel\">");
            HttpContext.Current.Response.Write("\r\n");
            HttpContext.Current.Response.Write("<style>  .cssnumber " + "\r\n" + "{mso-number-format:\"" + @"\@" + "\"" + "; text-align:left; } " + "\r\n  .cssdate " + "\r\n" + "{mso-number-format:\"" + "Short Date" + "\"" + "; } " + "\r\n" + " .csscurrency " + "\r\n" + "{mso-number-format:\"" + "0\\.00" + "\"" + "; text-align:right;} " + "\r\n" + "</style>");
            HttpContext.Current.Response.Write("<div align=center><h4><U>" + header + "</U></h4></div>");
            HttpContext.Current.Response.Write("<div align=left>" + headingInfo + "</div>");

            StringWriter tw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(tw);

            gvGlobal.RenderControl(hw);
            HttpContext.Current.Response.AppendHeader("content-disposition", "attachment;filename=" + fileName);
            HttpContext.Current.Response.Write(tw.ToString());
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// generating the Excel Format
        /// </summary>
        /// <returns></returns>
        public static string GenerateExcelFormat(DataTable cellData)
        {
            string strExcelFormat = string.Empty;
            for (int i = 0; i < cellData.Columns.Count; i++)
            {
                if (cellData.Columns[i].DataType.ToString().Equals(SystemString))
                    strExcelFormat += i + CssNumber;
                else if (cellData.Columns[i].DataType.ToString().Equals(SystemDateTime))
                    strExcelFormat += i + CssDate;
                else if (cellData.Columns[i].DataType.ToString().Equals(SystemDecimal))
                    strExcelFormat += i + CssCurrency;
                else if (cellData.Columns[i].DataType.ToString().Equals(SystemInt))
                    strExcelFormat += i + CssNumber;
            }
            //Removing the ',' at the end of the string
            if (!string.IsNullOrEmpty(strExcelFormat))
                return strExcelFormat.Substring(0, strExcelFormat.Length - 1);

            return strExcelFormat;
        }

        /// <summary>
        /// Merge Rows
        /// </summary>
        /// <param name="path"></param>
        /// <param name="sheetName"></param>
        /// <param name="cellsToMerge"></param>
        public static void MergeRows(string path, string sheetName, string cellsToMerge)
        {
            using (ExcelPackage package = new ExcelPackage(new FileInfo(path)))
            {
                var worksheet = package.Workbook.Worksheets[sheetName];
                worksheet.Cells[cellsToMerge].Merge = true;
                package.Save();
            }
        }
        #endregion
    }
}

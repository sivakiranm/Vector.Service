using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ExcelLibrary.SpreadSheet;
using System.Xml;
using System.Xml.Linq;
using System.Reflection;

namespace Vector.Common.BusinessLayer
{
    public class ExcelLibrary : DisposeLogic
    {


        public DataTable ReadExcel(string fileName, int headerRow = 0, int dataFromRow = 0, bool lastColIndexReq = true)
        {
            // open xls file
            Workbook book = Workbook.Load(fileName);
            Worksheet sheet = book.Worksheets[0];

            DataTable dtExcelData = new DataTable();

            // traverse cells
            foreach (var cell in sheet.Cells.GetRow(headerRow))
            {
                DataColumn dtColumns = new DataColumn();

                string columnName = Convert.ToString(cell.Value);
                string header = string.Empty;

                if (columnName.Contains("\r\n"))
                    header = columnName.Replace("\r\n", "");
                else if (columnName.Contains("\n"))
                    header = columnName.Replace("\n", "");
                else
                    header = columnName;

                dtColumns.ColumnName = Convert.ToString(header);

                if (!dtExcelData.Columns.Contains(dtColumns.ColumnName))
                    dtExcelData.Columns.Add(dtColumns);

            }

            int lastRowIndex = 0;
            // traverse rows by Index
            for (int rowIndex = sheet.Cells.FirstRowIndex;
                   rowIndex <= sheet.Cells.LastRowIndex; rowIndex++)
            {
                if (rowIndex >= dataFromRow)
                {
                    DataRow dtRow = dtExcelData.NewRow();

                    Row row = sheet.Cells.GetRow(rowIndex);

                    // Get last column index based on header count
                    if (lastColIndexReq)
                        lastRowIndex = row.LastColIndex;
                    else
                        lastRowIndex = dtExcelData.Columns.Count - 1;

                    for (int colIndex = row.FirstColIndex;
                       colIndex <= lastRowIndex; colIndex++)
                    {
                        Cell cell = row.GetCell(colIndex);

                        if (GetDateFormats().Contains(cell.FormatString))
                        {
                            if (dtExcelData.Columns.Count >= colIndex)
                                dtRow[colIndex] = cell.DateTimeValue.Date;
                        }
                        else if (dtExcelData.Columns.Count >= colIndex)
                            dtRow[colIndex] = cell.Value;

                    }

                    dtExcelData.Rows.Add(dtRow);
                }
            }

            return dtExcelData;
        }


        public List<string> GetDateFormats()
        {
            List<string> dateFormats = new List<string>();
            dateFormats.Add("mm/dd/yyyy");
            dateFormats.Add("dd/mm/yyyy");
            dateFormats.Add("yyyy/dd/mm");
            dateFormats.Add("yyyy/mm/dd");
            dateFormats.Add("m/d/yy");
            dateFormats.Add("m/d/yyyy");
            dateFormats.Add("yyyy/mm/dd");
            dateFormats.Add("yyyy/dd/mm");
            dateFormats.Add("d/m/yyyy");
            return dateFormats;

        }

    }

    public static class XMLtoDataTable
    {
        const string currency = "Currency";
        const string styles = "//ss:Styles";
        const string style = "ss:Style";
        const string numberFormat = "ss:NumberFormat";
        const string Format = "ss:Format";
        const string ssID = "ss:ID";
        const string styleID = "ss:StyleID";
        const string ssIndex = "ss:Index";
        const string ssData = "ss:Data";
        const string ssType = "ss:Type";
        const string column = "Column";
        const string currencyStyle = "currency";
        const string celldata = "ss:Cell/ss:Data";
        const string ssCell = "ss:Cell";
        const string ssWorksheet = "//ss:Worksheet";
        const string ssName = "ss:Name";
        const string clientCSS = "-->";

        private static ColumnType getDefaultType()
        {
            return new ColumnType(typeof(String));
        }

        public struct ColumnType
        {
            public Type type;
            private string name;
            public ColumnType(Type type) { this.type = type; this.name = type.ToString().ToLower(); }
            public object ParseString(string input)
            {
                if (String.IsNullOrEmpty(input))
                    return DBNull.Value;
                switch (type.ToString())
                {
                    case "system.datetime":
                        return DateTime.Parse(input);
                    case "system.decimal":
                        return decimal.Parse(input);
                    case "system.boolean":
                        return bool.Parse(input);
                    case "system.int32":
                        decimal d = Convert.ToDecimal(input);
                        if ((d % 1) > 0)
                        {
                            return d; //is decimal
                        }
                        else
                        {
                            return int.Parse(input); //is int
                        }

                    default:
                        return input;
                }
            }
        }


        private static ColumnType getType(XmlNode data)
        {
            string type = null;
            if (data.Attributes[ssType] == null || data.Attributes[ssType].Value == null)
                type = "";
            else
                type = data.Attributes[ssType].Value;
            switch (type)
            {
                case "DateTime":
                    return new ColumnType(typeof(DateTime));
                case "Boolean":
                    return new ColumnType(typeof(Boolean));
                case "Number":
                    return new ColumnType(typeof(decimal)); // applying decimal format if style format not contains "0" else configured system.int32 before this method
                default://"String"
                    return new ColumnType(typeof(String));
            }
        }


        //Get Currency Format Style

        public static void GetCurrencyAndNumberFormats(XmlDocument doc, XmlNamespaceManager nsmgr, out string xlsCurrencyStyle, out string xlsNumberStyle)
        {
            xlsCurrencyStyle = "";
            xlsNumberStyle = "";
            foreach (XmlNode node in
               doc.DocumentElement.SelectNodes(styles, nsmgr))
            {
                XmlNodeList stylesList = node.SelectNodes(style, nsmgr);
                if (stylesList.Count > 0)
                {
                    for (int i = 0; i < stylesList.Count; i++)
                    {
                        XmlNodeList stylecells = stylesList[i].SelectNodes(numberFormat, nsmgr);
                        if (stylecells != null && stylecells.Count > 0 && stylecells[0].Attributes[Format].Value == currency)
                        {
                            xlsCurrencyStyle = stylesList[i].Attributes[ssID].Value;
                        }
                        if (stylecells != null && stylecells.Count > 0 && stylecells[0].Attributes[Format].Value == VectorConstants.Zero.ToString())
                        {
                            xlsNumberStyle = stylesList[i].Attributes[ssID].Value;
                        }
                    }

                }

            }
        }

        //Add Columns To Table from header row
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="rows"></param>
        /// <param name="nsmgr"></param>
        /// <param name="columns"></param>
        public static void AddColumnsToTable(DataTable dt, XmlNodeList rows, XmlNamespaceManager nsmgr, List<ColumnType> columns)
        {
            foreach (XmlNode data in rows[0].SelectNodes(celldata, nsmgr))
            {
                columns.Add(new ColumnType(typeof(string)));//default to text
                dt.Columns.Add(data.InnerText == "&nbsp" ? " " : data.InnerText, typeof(string));
            }
            // return dt;
        }

        //Update Data-Types of columns if Auto-Detecting
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="rows"></param>
        /// <param name="nsmgr"></param>
        /// <param name="columns"></param>
        /// <param name="startIndex"></param>
        /// <param name="xlsNumberStyle"></param>
        /// <param name="xlsCurrencyStyle"></param>
        /// <param name="reportTitle"></param>
        public static void UpdateDataTypesForColumns(DataTable dt, XmlNodeList rows, XmlNamespaceManager nsmgr, List<ColumnType> columns, int startIndex, string xlsNumberStyle, string xlsCurrencyStyle, string reportTitle)
        {
            XmlNodeList cells = rows[startIndex].SelectNodes(ssCell, nsmgr);
            int actualCellIndex = 0;
            for (int cellIndex = 0; cellIndex < cells.Count; cellIndex++)
            {
                XmlNode cell = cells[cellIndex];
                if (cell.Attributes[ssIndex] != null)
                    actualCellIndex =
                      int.Parse(cell.Attributes[ssIndex].Value) - 1;
                ColumnType autoDetectType = new ColumnType();
                if (cell.Attributes[styleID] != null && cell.Attributes[styleID].Value == xlsNumberStyle) //number format
                {
                    autoDetectType = new ColumnType(typeof(Int32));
                }
                else
                {
                    //Modified by Naresh - If reportTitle is empty (convert Number to String)
                    if (string.IsNullOrEmpty(reportTitle))
                    {
                        var typeOfData = cell.SelectSingleNode(ssData, nsmgr);
                        autoDetectType = typeOfData.Attributes[ssType].Value == "Number" && cell.Attributes[styleID].Value == xlsCurrencyStyle ? new ColumnType(typeof(String)) : getType(cell.SelectSingleNode(ssData, nsmgr));
                    }
                    else
                    {
                        autoDetectType = getType(cell.SelectSingleNode(ssData, nsmgr));
                    }
                }
                if (actualCellIndex >= dt.Columns.Count)
                {
                    dt.Columns.Add(column + actualCellIndex.ToString(), autoDetectType.type);
                    columns.Add(autoDetectType);
                }
                else
                {
                    dt.Columns[actualCellIndex].DataType = autoDetectType.type;
                    columns[actualCellIndex] = autoDetectType;
                    dt.Columns[actualCellIndex].AllowDBNull = true;
                }

                actualCellIndex++;
            }
        }

        //All records of data  Load to DataTable
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="rows"></param>
        /// <param name="nsmgr"></param>
        /// <param name="columns"></param>
        /// <param name="startIndex"></param>
        /// <param name="xlsCurrencyStyle"></param>
        /// <param name="reportTitle"></param>
        public static void LoadDataToDataTable(DataTable dt, XmlNodeList rows, XmlNamespaceManager nsmgr, List<ColumnType> columns, int startIndex, string xlsCurrencyStyle, string reportTitle)
        {
            var xmlnodeIndexwithCSS = rows.Cast<XmlElement>().AsEnumerable().Select((elem, index) => new { elem, index }).LastOrDefault(p => !p.elem.InnerText.Contains(clientCSS)).index;
            //var xmlnodeListwithCSS = rows.Cast<XmlElement>().AsEnumerable().Select((elem, index) => new { elem, index }).Where(p => !p.elem.InnerText.Contains(clientCSS));            

            for (int rowIndex = startIndex; rowIndex < rows.Count; rowIndex++)
            {
                var listOfData = Array.Empty<string>();  //Modified by Naresh - create empty array

                DataRow row = dt.NewRow();
                //Modified by Raja - instead of hardcoded index value as 1 changed dynamic index value xmlnodeIndexwithCSS
                XmlNodeList cells = rows[xmlnodeIndexwithCSS].SelectNodes(ssCell, nsmgr); //Modified by Naresh - fetching 1st row data with style 

                //Modified by Raja - instead of hardcoded index value as 1 changed dynamic index value xmlnodeIndexwithCSS
                if (rowIndex != xmlnodeIndexwithCSS) //Modified by Naresh - split the row of each cell data with "-->"
                {
                    listOfData = rows[rowIndex].InnerText.Split(new string[] { clientCSS }, StringSplitOptions.None);
                }
                int actualCellIndex = 0;
                for (int cellIndex = 0; cellIndex < cells.Count; cellIndex++)
                {

                    XmlNode cell = cells[cellIndex];
                    if (cell.Attributes[ssIndex] != null)
                        actualCellIndex = int.Parse(cell.Attributes[ssIndex].Value) - 1;

                    XmlNode data = cell.SelectSingleNode(ssData, nsmgr);

                    if (actualCellIndex >= dt.Columns.Count)
                    {
                        for (int ii = dt.Columns.Count; ii < actualCellIndex; ii++)
                        {
                            dt.Columns.Add(column + actualCellIndex.ToString(), typeof(string)); columns.Add(getDefaultType());
                        } // ii
                        ColumnType autoDetectType =
                           getType(cell.SelectSingleNode(ssData, nsmgr));
                        dt.Columns.Add(column + actualCellIndex.ToString(),
                                       typeof(string));
                        columns.Add(autoDetectType);
                    }
                    if (data != null)
                    {
                        if (cell.Attributes[styleID] != null && cell.Attributes[styleID].Value == xlsCurrencyStyle)
                        {
                            dt.Columns[actualCellIndex].ExtendedProperties[currencyStyle] = VectorConstants.Dollor;
                        }
                        if (dt.Columns[actualCellIndex].DataType.Name != "String" && (string.IsNullOrEmpty(data.InnerText) || (listOfData.Length > 0 && string.IsNullOrEmpty(listOfData[actualCellIndex]))))
                            row[actualCellIndex] = DBNull.Value;
                        else
                        {
                            //Modified by Naresh for Assign data , append Currency  format if reportTitle is empty and cell data type is Currency style
                            if (string.IsNullOrEmpty(reportTitle) && cell.Attributes[styleID].Value == xlsCurrencyStyle)
                            {
                                //Modified by Raja - instead of hardcoded index value as 1 changed dynamic index value xmlnodeIndexwithCSS
                                row[actualCellIndex] = rowIndex == xmlnodeIndexwithCSS ? data.InnerText : listOfData[actualCellIndex];
                                row[actualCellIndex] = dt.Columns[actualCellIndex].ExtendedProperties[currencyStyle].ToString() + row[actualCellIndex];
                            }
                            else
                            {
                                //Modified by Raja - instead of hardcoded index value as 1 changed dynamic index value xmlnodeIndexwithCSS
                                row[actualCellIndex] = rowIndex == xmlnodeIndexwithCSS ? data.InnerText : listOfData[actualCellIndex];
                            }
                        }
                    }
                    actualCellIndex++;
                }

                dt.Rows.Add(row);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="hasHeaders"></param>
        /// <param name="autoDetectColumnType"></param>
        /// <param name="reportTitle"></param>
        /// <returns></returns>
        public static DataTable ImportExcelXML(string xml, bool hasHeaders, bool autoDetectColumnType, string reportTitle)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);

            nsmgr.AddNamespace("o", "urn:schemas-microsoft-com:office:office");
            nsmgr.AddNamespace("x", "urn:schemas-microsoft-com:office:excel");
            nsmgr.AddNamespace("ss", "urn:schemas-microsoft-com:office:spreadsheet");

            //*************************
            //Get Currency Format Style
            //*************************
            string xlsCurrencyStyle = "";
            string xlsNumberStyle = "";

            GetCurrencyAndNumberFormats(doc, nsmgr, out xlsCurrencyStyle, out xlsNumberStyle);  // Modified by Naresh- Code Refactering

            DataTable dt = null;
            foreach (XmlNode node in
              doc.DocumentElement.SelectNodes(ssWorksheet, nsmgr))
            {
                dt = new DataTable(node.Attributes[ssName].Value);
                dt.ExtendedProperties.Add(currencyStyle, "");
                XmlNodeList rows = node.SelectNodes("ss:Table/ss:Row", nsmgr);
                if (rows.Count > 0)
                {

                    //*************************
                    //Add Columns To Table from header row
                    //*************************

                    List<ColumnType> columns = new List<ColumnType>();
                    int startIndex = 0;
                    if (hasHeaders)
                    {

                        AddColumnsToTable(dt, rows, nsmgr, columns); // Modified by Naresh- Code Refactering

                        startIndex++;
                    }

                    //*************************
                    //Update Data-Types of columns if Auto-Detecting
                    //*************************

                    if (autoDetectColumnType && rows.Count > 0)
                    {

                        UpdateDataTypesForColumns(dt, rows, nsmgr, columns, startIndex, xlsNumberStyle, xlsCurrencyStyle, reportTitle); // Modified by Naresh- Code Refactering
                    }

                    //*************************
                    //Load Data
                    //*************************
                    LoadDataToDataTable(dt, rows, nsmgr, columns, startIndex, xlsCurrencyStyle, reportTitle);
                }
            }
            return dt;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Xml.Linq;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace Vector.Common.BusinessLayer
{
    public class gridCoumnsCSV
    {
        public string text { get; set; }
        public string dataField { get; set; }
        public bool exportable { get; set; }
        public string cellsformat { get; set; }
    }

    public static class DataManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="gridColumns"></param>
        /// <param name="isDisplayName"></param>
        /// <returns></returns>
        public static DataTable ToDataTableByDisplayName<T>(List<T> items, List<gridCoumnsCSV> gridColumns, bool isDisplayName = false)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            //Get all the properties by using reflection   

            if (isDisplayName)
            {
                var properties = typeof(T).GetProperties().Where(p => p.IsDefined(typeof(DisplayAttribute), false))
                            .Select(p => new
                            {
                                PropertyType = p.PropertyType,
                                PropertyName = p.Name,
                                DisplayName = p.GetCustomAttributes(typeof(DisplayAttribute),
                                  false).Cast<DisplayAttribute>().FirstOrDefault()
                            });
                foreach (var item in properties)
                {
                    dataTable.Columns.Add(item.DisplayName != null && !string.IsNullOrEmpty(item.DisplayName.Name) ? item.DisplayName.Name : item.PropertyName,
                        checkPropertyByType(item.PropertyType));
                }
            }
            else
            {
                List<string> propArr = new List<string>();
                foreach (PropertyInfo prop in Props)
                {
                    //Setting column names as Property names  
                    var column = gridColumns.FirstOrDefault(x => x.dataField.ToLower() == prop.Name.ToLower() && x.exportable);

                    //else if (cell.cellsformat == "d2")
                    //{
                    //    type = typeof(Decimal);
                    //}
                    if (column != null)
                    {
                        var cell = gridColumns.FirstOrDefault(x => !string.IsNullOrEmpty(x.cellsformat) && column.dataField == x.dataField);
                        Type type = null;
                        if (cell != null && (cell.cellsformat == "c2" || cell.cellsformat == "c4" || cell.cellsformat == "d2"))
                        {
                            type = typeof(string);
                        }
                        dataTable.Columns.Add(column.text, type ?? checkPropertyType(prop));
                    }
                    else
                        propArr.Add(prop.Name);
                }

                PropertyInfo[] ColumnProperties = Props.Where(p => !propArr.Contains(p.Name)).ToArray();


                foreach (T item in items)
                {
                    var values = new object[ColumnProperties.Length];
                    for (int i = 0; i < ColumnProperties.Length; i++)
                    {
                        var cell = gridColumns.FirstOrDefault(x => !string.IsNullOrEmpty(x.cellsformat) && ColumnProperties[i].Name.ToLower() == x.dataField.ToLower());
                        values[i] = ColumnProperties[i].GetValue(item, null);
                        if (cell != null)
                        {
                            if (cell.cellsformat.ToLower() == "c2")
                            {
                                values[i] = string.Format("${0:#,0.00}", values[i]);
                                //values[i] = string.Format("{0:c}", values[i]);
                                //values[i] = Math.Round(Convert.ToDouble(values[i]), 2);
                            }
                            else if (cell.cellsformat.ToLower() == "d2")
                            {
                                values[i] = string.Format("{0:N2}", values[i]);
                            }
                            else if (cell.cellsformat.ToLower() == "c4")
                            {
                                values[i] = string.Format("${0:#,0.0000}", values[i]);
                            }
                        }
                    }
                    dataTable.Rows.Add(values);
                }
            }
            return dataTable;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            //Get all the properties by using reflection   

            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names  
                dataTable.Columns.Add(prop.Name, checkPropertyType(prop));
            }

            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {

                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }

        private static Type checkPropertyType(PropertyInfo prop)
        {
            Type t = null;
            if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                t = prop.PropertyType.GetGenericArguments()[0];
            else
                t = prop.PropertyType;

            return t;
        }

        private static Type checkPropertyByType(Type PropertyType)
        {
            Type t = null;
            if (PropertyType.IsGenericType && PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                t = PropertyType.GetGenericArguments()[0];
            else
                t = PropertyType;

            return t;
        }


        #region Constants

        private const string FormatColumn = "[{0}]";
        private const string FormatColumnWithNull = "ISNULL([{0}],0)";
        private const string SumExpressionStart = "SUM(";
        private const string SystemDecimal = "System.Decimal";
        private const string ClosingBracket = ")";
        private const string OpeningBracket = "(";
        private const string DividedBy = "/";
        private const string ClosingPercentExp = ")*100";
        private const string SumOf = "SUM(";
        private const string BraceEnd = ")";
        private const string Space = " ";
        private const string CSVValueFormat = "{0}";
        private const string DoubleQuotes = "\"";
        private const string DoubleDoubleQuotes = "\"\"";
        private const int NoColumn = -1;
        private const string SingleQuote = "'";
        private const string Period = "Period";
        private const string Id = "Id";
        private const string Name = "Name";
        private const string From = "From";
        private const string To = "To";
        private const string NoOfDays = "NoOfDays";
        private const string TabDelimeter = "\\t";
        private const string CSTabDelimeter = "\t";

        #endregion

        #region Enum

        /// <summary>
        /// List of Columns
        /// </summary>
        private enum Columns
        {
            ATTRIBUTE_NAME,
            ATTRIBUTE_VALUE
        }

        #endregion

        public static DataTable AddShortMonthName(DataTable dt, string monthColumnName, string monthTextColumnName)
        {
            if (!dt.Columns.Contains(monthColumnName))
                return dt;

            DataColumn dc = new DataColumn(monthTextColumnName, typeof(string));
            dc.Expression = "IIF(" + monthColumnName + " = 1, 'Jan', IIF(" + monthColumnName + " = 2, 'Feb', IIF(" + monthColumnName + " = 3, 'Mar', IIF(" + monthColumnName + " = 4, 'Apr', IIF(" + monthColumnName + " = 5, 'May', IIF(" + monthColumnName + " = 6, 'Jun', IIF(" + monthColumnName + " = 7, 'Jul', IIF(" + monthColumnName + " = 8, 'Aug', IIF(" + monthColumnName + " = 9, 'Sep', IIF(" + monthColumnName + " = 10, 'Oct', IIF(" + monthColumnName + " = 11, 'Nov', IIF(" + monthColumnName + " = 12, 'Dec', ''))))))))))))";
            dt.Columns.Add(dc);
            return dt;
        }

        public static DataTable AddShortMonthAndYearName(DataTable dt, string monthColumnName, string yearColumnName, string monthYearTextColumnName, string seperatorText = "-")
        {
            if (!dt.Columns.Contains(monthColumnName))
                return dt;

            DataColumn dc = new DataColumn(monthYearTextColumnName, typeof(string));
            dc.Expression = "(IIF(" + monthColumnName + " = 1, 'Jan', IIF(" + monthColumnName + " = 2, 'Feb', IIF(" + monthColumnName + " = 3, 'Mar', IIF(" + monthColumnName + " = 4, 'Apr', IIF(" + monthColumnName + " = 5, 'May', IIF(" + monthColumnName + " = 6, 'Jun', IIF(" + monthColumnName + " = 7, 'Jul', IIF(" + monthColumnName + " = 8, 'Aug', IIF(" + monthColumnName + " = 9, 'Sep', IIF(" + monthColumnName + " = 10, 'Oct', IIF(" + monthColumnName + " = 11, 'Nov', IIF(" + monthColumnName + " = 12, 'Dec', '')))))))))))))+'" + seperatorText + "'+" + yearColumnName;
            dt.Columns.Add(dc);
            return dt;
        }
        public static DataTable AddShortMonthAndYearNameWithComma(DataTable dt, string monthColumnName, string yearColumnName, string monthYearTextColumnName, string seperatorText = ",")
        {
            if (!dt.Columns.Contains(monthColumnName))
                return dt;

            DataColumn dc = new DataColumn(monthYearTextColumnName, typeof(string));
            dc.Expression = "(IIF(" + monthColumnName + " = 1, 'Jan', IIF(" + monthColumnName + " = 2, 'Feb', IIF(" + monthColumnName + " = 3, 'Mar', IIF(" + monthColumnName + " = 4, 'Apr', IIF(" + monthColumnName + " = 5, 'May', IIF(" + monthColumnName + " = 6, 'Jun', IIF(" + monthColumnName + " = 7, 'Jul', IIF(" + monthColumnName + " = 8, 'Aug', IIF(" + monthColumnName + " = 9, 'Sep', IIF(" + monthColumnName + " = 10, 'Oct', IIF(" + monthColumnName + " = 11, 'Nov', IIF(" + monthColumnName + " = 12, 'Dec', '')))))))))))))+'" + seperatorText + "'+" + yearColumnName;
            dt.Columns.Add(dc);
            return dt;
        }

        public static DataTable AddShortQuarterAndYearName(DataTable dt, string qtrColumnName, string yearColumnName, string qtrYearTextColumnName)
        {
            if (!dt.Columns.Contains(qtrColumnName))
                return dt;

            DataColumn dc = new DataColumn(qtrYearTextColumnName, typeof(string));
            dc.Expression = qtrColumnName + "+' '+" + yearColumnName;
            dt.Columns.Add(dc);
            return dt;
        }

        public static DataTable ChangeColumnDataType(DataTable dt, string columnName, Type objType)
        {
            DataTable dtCloned = dt.Clone();
            dtCloned.Columns[columnName].DataType = objType;
            foreach (DataRow row in dt.Rows)
            {
                dtCloned.ImportRow(row);
            }

            return dtCloned;
        }


        /// <summary>
        /// Gets Columns data types For Sum
        /// </summary>
        public static Collection<string> GetColumnsForSum
        {
            get
            {
                Collection<string> columnType = new Collection<string>();
                columnType.Add("System.Int32");
                columnType.Add("System.Int16");
                columnType.Add("System.Int64");
                columnType.Add("System.Decimal");
                columnType.Add("System.Float");
                columnType.Add("System.Double");
                return columnType;
            }
        }

        #region Data

        #region Validate Dataset

        /// <summary>
        /// This Method is used to check whether the data table is empty or not
        /// </summary>
        /// <param name="dataSource">Data Source</param>
        /// <returns>Boolean value</returns>
        public static bool IsNullOrEmptyDataTable(DataTable dataSource)
        {
            if ((dataSource == null) || (dataSource.Rows.Count == VectorConstants.Zero))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// This Method is used to check whether the data set is empty or not
        /// </summary>
        /// <param name="dataSource">Data Source</param>
        /// <returns>Boolean value</returns>
        public static bool IsNullOrEmptyDataSet(DataSet dataSource)
        {
            if (dataSource == null || dataSource.Tables.Count == VectorConstants.Zero || dataSource.Tables[VectorConstants.Zero].Rows.Count == VectorConstants.Zero)
            {
                if (dataSource != null && dataSource.Tables.Count > 0)
                    return false;
                else
                    return true;
            }


            return false;
        }

        /// <summary>
        /// This Method is used to check whether the data set is empty or not and table count
        /// </summary>
        /// <param name="dataSource">Data Source</param>
        /// <param name="tableCount">Number of tables count</param>
        /// <returns>Boolean value</returns>
        public static bool IsNullOrEmptyDataSet(DataSet dataSource, int tableCount)
        {
            int index = tableCount - VectorConstants.One;
            if (dataSource == null || dataSource.Tables.Count != tableCount)
            {
                if (dataSource.Tables.Count >= tableCount)
                {
                    if (dataSource.Tables[index].Rows.Count == VectorConstants.Zero)
                    {
                        return true;
                    }
                }
            }

            return false;
        }



        /// <summary>
        /// This Method is used to check whether the data set is empty or not and table count 
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static bool IsNullOrEmptyDataSet(DataSet ds, string tableName)
        {

            if (ds == null || !ds.Tables.Contains(tableName))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// This Method is used to check whether the data set is empty or not and how many table in contains
        /// </summary>
        /// <param name="dataSource">Data Source</param>
        /// <param name="tableCount">Number of tables count</param>
        /// <returns>Boolean value</returns>
        public static bool IsDataSetContainsTable(DataSet dataSource, int tableCount)
        {
            if (dataSource == null || dataSource.Tables.Count == VectorConstants.Zero
                                   || dataSource.Tables[VectorConstants.Zero].Rows.Count == VectorConstants.Zero
                                   || dataSource.Tables.Count != tableCount)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// This Method is used to check whether the data set is empty or not and table count
        /// </summary>
        /// <param name="dataSource">Data Source</param>
        /// <param name="tableCount">Number of tables count</param>
        /// <param name="checkrowsForTable">Check Rows for table number</param>
        /// <returns>Boolean value</returns>
        public static bool IsNullOrEmptyDataSet(DataSet dataSource, int tableCount, int checkrowsForTable)
        {
            if (dataSource != null)
            {
                if (dataSource.Tables.Count >= checkrowsForTable)
                {
                    int index = checkrowsForTable - VectorConstants.One;
                    if (dataSource.Tables.Count >= tableCount && dataSource.Tables[index].Rows.Count != VectorConstants.Zero)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// This Method is used to check whether the data is found for filter or not
        /// </summary>
        /// <param name="dataSource">Data Source</param>
        /// <param name="filter">filter value</param>
        /// <returns>Boolean value</returns>
        public static bool IsDataExists(DataTable dataSource, string filter)
        {
            if (!IsNullOrEmptyDataTable(dataSource))
            {
                ////find if any data exists for the filter 
                DataRow[] rowFound = dataSource.Select(filter);
                if (rowFound.Length > VectorConstants.Zero)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checking dataset having data or not
        /// </summary>
        /// <param name="dataSource">Data Source</param>
        /// <returns>Boolean value</returns>
        public static bool DataSetHasData(DataSet dataSource)
        {
            return !IsNullOrEmptyDataSet(dataSource);
        }

        /// <summary>
        /// Checking dataset table count
        /// </summary>
        /// <param name="dataSource">Data Source</param>
        /// <returns>integer value</returns>
        public static int DataSetTableCount(DataSet dataSource)
        {
            if (IsNullOrEmptyDataSet(dataSource) == true)
            {
                return 0;
            }

            return dataSource.Tables.Count;
        }

        /// <summary>
        /// Checking first data table in data set rows count
        /// </summary>
        /// <param name="dataSource">Data Source</param>
        /// <returns>Rows Count in data table</returns>
        public static int DataSetRowCount(DataSet dataSource)
        {
            if (IsNullOrEmptyDataSet(dataSource) == true)
            {
                return VectorConstants.Zero;
            }

            return dataSource.Tables[VectorConstants.Zero].Rows.Count;
        }

        /// <summary>
        /// Returns true if the data table contains the given column
        /// </summary>
        /// <param name="dataSource">Data Source</param>
        /// <param name="columnName">Column name</param>
        /// <returns>Boolean value</returns>
        public static bool IsDataTableHasColumn(DataTable dataSource, string columnName)
        {
            if (dataSource.Columns.Contains(columnName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Delete Columns from data table
        /// </summary>
        /// <param name="dataSource">Data Source</param>
        /// <param name="columnName">Column Name</param>
        /// <returns>Data Table</returns>
        public static DataTable DeleteColumn(DataTable dataSource, string columnName)
        {
            if (IsDataTableHasColumn(dataSource, columnName))
            {
                dataSource.Columns.Remove(columnName);
                return dataSource;
            }

            return dataSource;
        }

        /// <summary>
        /// Returns true if the data table contains the given column
        /// </summary>
        /// <param name="dataSource">Data Source</param>
        /// <returns>Boolean Value</returns>
        public static bool IsDataTableHasColumn(DataTable dataSource)
        {
            if (dataSource.Columns.Count > VectorConstants.Zero)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checking data row having data or not
        /// </summary>
        /// <param name="dataRow">Data Row</param>
        /// <returns>Boolean Value</returns>
        public static bool IsDataRowHasData(DataRow[] dataRow)
        {
            if (dataRow != null && dataRow.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Returns true if the data table contains the given column
        /// </summary>
        /// <param name="dataSource">Data Source</param>
        /// <param name="oldColumnName">Old Column Name</param>
        /// <param name="newColumnName">New Column Name</param>
        /// <param name="isToUpper">Is To upper or not</param>
        /// <returns>Data table with new column name</returns>
        public static DataTable RenameColumn(DataTable dataSource, string oldColumnName, string newColumnName, bool isToUpper = true, bool isMultipleColumnRename = false)
        {
            if (IsDataTableHasColumn(dataSource, oldColumnName))
            {
                if (!dataSource.Columns.Contains(newColumnName) || isMultipleColumnRename)
                {
                    int index = dataSource.Columns.IndexOf(oldColumnName);
                    dataSource.Columns[index].ColumnName = isToUpper ? newColumnName.ToUpper(CultureInfo.InvariantCulture) : newColumnName;
                    return dataSource;
                }
            }

            return dataSource;
        }

        /// <summary>
        /// If Data is null adding default value
        /// </summary>
        /// <param name="dataSource">Data Source</param>
        /// <returns>New dataSource with Default values</returns>
        public static DataTable HasNull(this DataTable dataSource)
        {
            foreach (DataRow dataRow in dataSource.Rows)
            {
                foreach (DataColumn dataColumn in dataSource.Columns)
                {
                    if (dataRow[dataColumn] == DBNull.Value || string.IsNullOrEmpty(dataRow[dataColumn].ToString()))
                    {
                        dataRow[dataColumn] = "0.00";
                        dataRow.AcceptChanges();
                    }
                }
            }

            return dataSource;
        }

        #endregion

        #region Validate Data

        /// <summary>
        /// Is valid Numeric
        /// </summary>
        /// <param name="value">numeric value</param>
        /// <returns>boolean value</returns>
        public static bool IsNumeric(string value)
        {
            int num1;
            return int.TryParse(value, out num1);
        }

        /// <summary>
        /// Is valid temperature 
        /// </summary>
        /// <param name="value">tempter value</param>
        /// <param name="min">minimum temp</param>
        /// <param name="max">maximum temp</param>
        /// <returns>boolean value</returns>
        public static bool IsValidTemperature(string value, string min, string max)
        {
            if (IsNumeric(value))
            {
                if (Convert.ToInt32(value, CultureInfo.CurrentCulture) >= Convert.ToInt32(min, CultureInfo.CurrentCulture) &&
                    Convert.ToInt32(value, CultureInfo.CurrentCulture) <= Convert.ToInt32(max, CultureInfo.CurrentCulture))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Is valid Day 
        /// </summary>
        /// <param name="value">temp value</param>
        /// <param name="min">min value</param>
        /// <param name="max">max value</param>
        /// <returns>Boolean value</returns>
        public static bool IsValidDay(string value, int min, int max)
        {
            if (IsNumeric(value))
            {
                if (Convert.ToInt32(value, CultureInfo.CurrentCulture) >= min && Convert.ToInt32(value, CultureInfo.CurrentCulture) <= max)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Validates email address format.
        /// </summary>
        /// <param name="email">email id</param>
        /// <returns>boolean value</returns>
        public static bool IsEmail(string email)
        {
            return Regex.IsMatch(email, @"^([\w\-\.'*]+)@((\[([0-9]{1,3}\.){3}[0-9]{1,3}\])|(([\w\-]+\.)+)([a-zA-Z]{2,4}))$");
        }

        /// <summary>
        /// Validates telephone number format.
        /// </summary>
        /// <param name="phone">phone number</param>
        /// <returns>boolean value</returns>
        public static bool IsPhone(string phone)
        {
            return Regex.IsMatch(phone, @"^[01]?[- ]?\(?[2-9]\d{2}\)?[-]?\d{3}[-]?\d{4}$");
        }

        /// <summary>
        /// Validates policy number format.
        /// </summary>
        /// <param name="policy">policy name</param>
        /// <returns>boolean value</returns>
        public static bool IsPolicy(string policy)
        {
            return Regex.IsMatch(policy, @"^[-A-Z0-9]+$");
        }

        /// <summary>
        /// Check Data base null value
        /// </summary>
        /// <param name="value">value parameter</param>
        /// <returns>boolean value</returns>
        public static bool IsDBNull(object value)
        {
            return value == DBNull.Value;
        }

        /// <summary>
        /// Check for date and time
        /// </summary>
        /// <param name="date">Date time</param>
        /// <returns>boolean value</returns>
        public static bool IsActive(DateTime? date)
        {
            if (date == null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// format string into phone number
        /// </summary>
        /// <param name="telephoneNumber">phone number</param>
        /// <returns>formatted phone number</returns>
        public static string FormatAsTelephoneNumber(string telephoneNumber)
        {
            if (string.IsNullOrEmpty(telephoneNumber))
            {
                return string.Empty;
            }

            if (telephoneNumber.Length != 10)
            {
                return telephoneNumber;
            }

            return string.Format(CultureInfo.CurrentCulture, "({0}){1}-{2}", telephoneNumber.Substring(0, 3), telephoneNumber.Substring(3, 3), telephoneNumber.Substring(6, 4));
        }

        /// <summary>
        /// removing special characters
        /// </summary>
        /// <param name="value">text value</param>
        /// <returns>formatted value</returns>
        public static string Safe(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            return value
                .Replace(@"\", @"\\")
                .Replace("\r", "\\r")
                .Replace("\n", "\\n")
                .Replace("'", @"\'");
        }

        /// <summary>
        /// removing special characters
        /// </summary>
        /// <param name="inputSql">input String</param>
        /// <returns>formatted string</returns>
        public static string SafeSqlLiteral(string inputSql)
        {
            if (string.IsNullOrEmpty(inputSql))
            {
                return string.Empty;
            }

            return inputSql
                .Replace(@"\", @"\\")
                .Replace("\r", "\\r")
                .Replace("\n", "\\n")
                .Replace("'", string.Empty);
        }

        /// <summary>
        /// Validate Rows
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static bool HasNullOrEmptyInRows(DataTable table)
        {
            foreach (DataColumn column in table.Columns)
            {
                if (table.Rows.OfType<DataRow>().Any(r => r.IsNull(column) || string.IsNullOrEmpty(Convert.ToString(r[column]))))
                    return true;
            }

            return false;
        }

        #endregion

        #region Data Formating

        /// <summary>
        /// format data value
        /// </summary>
        /// <param name="data">text value</param>
        /// <returns>formatted value</returns>
        public static object FormatDataType(string data)
        {
            decimal value = 0;

            if (decimal.TryParse(data, out value))
            {
                return string.Format(CultureInfo.CurrentCulture, "{0, 0:N2}", value);
            }
            else
            {
                return string.Format(CultureInfo.CurrentCulture, "{0, 0:N2}", data);
            }
        }

        /// <summary>
        /// Returns data table along with total row . 
        /// </summary>
        /// <param name="dataSource">data source</param>
        /// <param name="columnsCollection">column collection</param>
        /// <returns>data table</returns>
        public static DataTable AddTotalRowToDataTable(DataTable dataSource, NameValueCollection columnsCollection)
        {
            if (!IsNullOrEmptyDataTable(dataSource))
            {
                ////Create table with required columns 
                if (columnsCollection != null && columnsCollection.Count > VectorConstants.Zero)
                {
                    ////create columns list in string array
                    string[] columns = new string[columnsCollection.Count];
                    int i = VectorConstants.Zero;

                    DataRow dr = dataSource.NewRow();

                    ////criteria[key] old column and value is new columns
                    foreach (string key in columnsCollection.Keys)
                    {
                        columns[i] = key.ToString();
                        if (i == VectorConstants.Zero)
                        {
                            dr[i] = "Total";
                        }
                        else
                        {
                            dr[i] = Convert.ToString(Convert.ToDecimal(dataSource.Compute(SumOf + dataSource.Columns[columns[i]] + BraceEnd,
                                            string.Empty), CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
                        }

                        i++;
                    }

                    dataSource.Rows.Add(dr);
                    dataSource.AcceptChanges();
                }
            }

            return dataSource;
        }

        /// <summary>
        /// Deleting unwanted columns from data table using String Dictionary values as required columns
        /// </summary>
        /// <param name="dataSource">data Source</param>
        /// <param name="columnsCollection">columns Collection</param>
        /// <param name="isDistinctRequired">is Distinct Required</param>
        /// <returns>Data Table</returns>
        public static DataTable CreateTableWithGivenColumns(DataTable dataSource, NameValueCollection columnsCollection, bool isDistinctRequired = true, bool isMultipleColumnRename = false)
        {
            if (!IsNullOrEmptyDataTable(dataSource))
            {
                ////Create table with required columns 
                if (columnsCollection != null && columnsCollection.Count > VectorConstants.Zero)
                {
                    ////create columns list in string array
                    string[] columns = new string[columnsCollection.Count];
                    int i = VectorConstants.Zero;

                    ////criteria[key] old column and value is new columns
                    foreach (string key in columnsCollection.Keys)
                    {
                        columns[i] = key.ToString();
                        i++;
                    }

                    ////check are all columns exists in data table 
                    if (AreColumnsExistsInDataTable(dataSource, columns))
                    {
                        return RenameColumnNames(dataSource.DefaultView.ToTable(isDistinctRequired, columns), columnsCollection, isMultipleColumnRename: isMultipleColumnRename);
                    }
                }
            }

            return dataSource;
        }

        /// <summary>
        /// check column list in given data table StringDictionary will
        /// </summary>
        /// <param name="dataSource">data source</param>
        /// <param name="columns">column collection</param>
        /// <returns>boolean value</returns>
        public static bool AreColumnsExistsInDataTable(DataTable dataSource, string[] columns)
        {
            if (!IsNullOrEmptyDataTable(dataSource))
            {
                ////Create table with required columns 
                if (columns.Length > VectorConstants.Zero)
                {
                    ////key is old column and value is new columns
                    foreach (string col in columns)
                    {
                        ////check for each column exists in data table 
                        if (!dataSource.Columns.Contains(col))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>
        ///  Rename the columns using String Dictionary values
        /// </summary>
        /// <param name="dataSource">data source</param>
        /// <param name="columns">column collection</param>
        /// <returns>data table</returns>
        public static DataTable RenameColumnNames(DataTable dataSource, NameValueCollection columns, bool isMultipleColumnRename = false)
        {
            if (!IsNullOrEmptyDataTable(dataSource))
            {
                if (columns != null && columns.Count > VectorConstants.Zero)
                {
                    ////criteria[key] old column and value is new columns
                    foreach (string key in columns.Keys)
                    {
                        RenameColumn(dataSource, key.ToString().Trim(), (string)columns[key].Trim(), false, isMultipleColumnRename: isMultipleColumnRename);
                    }
                }
            }

            return dataSource;
        }

        /// <summary>
        /// Format data table of a dataset 
        /// </summary>
        /// <param name="data">data source</param>
        /// <param name="columnFormat">column collection</param>
        /// <param name="tableNumber">table number</param>
        /// <returns>data set</returns>
        public static DataSet FormatDataSet(DataSet data, NameValueCollection columnFormat, int tableNumber = 0, bool isMultipleColumnRename = false, bool isDistinctRequired = true)
        {
            if (!IsNullOrEmptyDataSet(data))
            {
                if (columnFormat != null && columnFormat.Count > VectorConstants.Zero)
                {
                    int i = VectorConstants.Zero;
                    DataSet formatedData = new DataSet();
                    formatedData.Locale = CultureInfo.InvariantCulture;
                    foreach (DataTable table in data.Tables)
                    {
                        ////given table data only formatting in the data set
                        if (i == tableNumber)
                        {
                            formatedData.Tables.Add(FormatDataTable(table.Copy(), columnFormat, isDistinctRequired, isMultipleColumnRename: isMultipleColumnRename));
                        }
                        else
                        {
                            formatedData.Tables.Add(table.Copy());
                        }

                        i++;
                    }

                    return formatedData;
                }
            }

            return data;
        }

        /// <summary>
        /// Format table 
        /// </summary>
        /// <param name="data">data source</param>
        /// <param name="columnFormat">column collection</param>
        /// <param name="isDistinctRequired">is unique column</param>
        /// <returns>data table</returns>
        public static DataTable FormatDataTable(DataTable data, NameValueCollection columnFormat, bool isDistinctRequired = true, bool isMultipleColumnRename = false)
        {
            return CreateTableWithGivenColumns(data, columnFormat, isDistinctRequired, isMultipleColumnRename: isMultipleColumnRename);
        }

        /// <summary>
        /// Get CSU columns data
        /// </summary>
        /// <param name="data">data source</param>
        /// <param name="tableData">string builder</param>
        /// <returns>formatted string with comma separated</returns>
        public static string ConvertDataToCsv(DataTable data, StringBuilder tableData = null, string delimiter = ",", bool isDoubleQuoteRequired = true, string rowEndDelimeter = "")
        {

            if (string.Equals(delimiter, TabDelimeter))
                delimiter = CSTabDelimeter;

            string doubleQuotes = DoubleQuotes;

            if (!isDoubleQuoteRequired)
                doubleQuotes = string.Empty;


            foreach (DataRow row in data.Rows)
            {
                ////read each column data of row and add comma to each column data 
                var fieldsWithQuotes = row.ItemArray.Select(fields => string.Format(CultureInfo.CurrentCulture, CSVValueFormat,
                    string.Concat(doubleQuotes, ReplaceStringwithRequired(fields)
                                                                 , doubleQuotes))).ToArray();

                ////replace "  in data to "" & enclose field with double quotes
                tableData.AppendLine(string.Join(delimiter, fieldsWithQuotes) + rowEndDelimeter);
            }

            return tableData.ToString();
        }

        private static string ReplaceStringwithRequired(object fields)
        {
            return fields.ToString().Replace(Environment.NewLine, string.Empty)
                                    .Replace(VectorConstants.NewLineRow, VectorConstants.Space)
                                    .Replace(VectorConstants.NewRow, VectorConstants.Space)
                                    .Replace(VectorConstants.NewLine, VectorConstants.Space);
            //.Replace(DoubleQuotes, DoubleDoubleQuotes);
        }

        /// <summary>
        /// delete Columns from table
        /// </summary>
        /// <param name="data">data source</param>
        /// <param name="columns">column collection</param>
        /// <returns>data table</returns>
        public static DataTable DeleteColumn(DataTable data, string[] columns)
        {
            ////Create table with required columns 
            if (columns.Length > VectorConstants.Zero)
            {
                ////key is old column and value is new columns
                foreach (string col in columns)
                {
                    if (IsDataTableHasColumn(data, col.ToString()))
                    {
                        data.Columns.Remove(col.ToString());
                    }
                }
            }

            return data;
        }

        /// <summary>
        ///  Generate Data set to display search criteria using
        /// </summary>
        /// <param name="collection">column collection</param>
        /// <returns>data set</returns>
        public static DataSet GenerateSearchCiteriaData(NameValueCollection collection)
        {
            DataSet citeriaData = new DataSet();
            citeriaData.Locale = CultureInfo.InvariantCulture;
            DataTable data = new DataTable();
            data.Locale = CultureInfo.InvariantCulture;
            data.Columns.Add(Columns.ATTRIBUTE_NAME.ToString());
            data.Columns.Add(Columns.ATTRIBUTE_VALUE.ToString());
            DataRow row;

            var items = collection.AllKeys.SelectMany(collection.GetValues, (k, v) => new { key = k, value = v });
            foreach (var item in items)
            {
                if (StringManager.IsNotEqual(item.value, string.Empty))
                {
                    row = data.NewRow();
                    row[Columns.ATTRIBUTE_NAME.ToString()] = item.value;
                    row[Columns.ATTRIBUTE_VALUE.ToString()] = item.key;
                    data.Rows.Add(row);
                }
            }

            citeriaData.Tables.Add(data);
            return citeriaData;
        }

        /// <summary>
        /// Replacing Specific Values in Data Table to Specified Values
        /// </summary>
        /// <param name="data">data source</param>
        /// <param name="columnName">column name</param>
        /// <param name="fromValue">from value</param>
        /// <param name="toValue">to value</param>
        /// <returns>data table</returns>
        public static DataTable SetEmptyDataValueForDataColum(DataTable data, string columnName, string fromValue, string toValue)
        {
            if (!IsNullOrEmptyDataTable(data) && IsDataTableHasColumn(data, columnName))
            {
                if (data.Columns[columnName].DataType != typeof(string))
                {
                    DataTable clonedata = new DataTable();
                    clonedata.Locale = CultureInfo.InvariantCulture;

                    ////Copying Schema From Source Data table
                    clonedata = data.Clone();
                    ////Changing Data type for Required Column 
                    clonedata.Columns[columnName].DataType = typeof(string);

                    ////Importing Entire Data From Source To Cloned Data Table
                    foreach (DataRow row in data.Rows)
                    {
                        clonedata.ImportRow(row);
                    }

                    ////Checking Value in Column & Replacing It With Given Value
                    data = ReplaceValuesinDataColumn(clonedata, columnName, fromValue, toValue);
                    data = new DataTable();
                    data.Locale = CultureInfo.InvariantCulture;
                    data = clonedata.Copy();
                }
                else
                {
                    data = ReplaceValuesinDataColumn(data, columnName, fromValue, toValue);
                }
            }

            return data;
        }

        /// <summary>
        /// adding table to data set
        /// </summary>
        /// <param name="dataSource">data Source</param>
        /// <param name="table">data table</param>
        /// <returns>data set</returns>
        public static DataSet AddTableToDataSet(DataSet dataSource, DataTable table)
        {
            DataSet ds = dataSource.Copy();
            ds.Tables.Add(table.Copy());

            return ds;
        }

        /// <summary>
        /// merge to dataset's
        /// </summary>
        /// <param name="dataSource">old data set</param>
        /// <param name="newDataSet">new data set</param>
        /// <returns>data set</returns>
        public static DataSet MergeDataSets(DataSet dataSource, DataSet newDataSet)
        {
            DataSet oldDataSource = new DataSet();
            oldDataSource.Locale = CultureInfo.InvariantCulture;
            if (!IsNullOrEmptyDataSet(dataSource))
            {
                oldDataSource = dataSource.Copy();
            }

            foreach (DataTable dt in newDataSet.Tables)
            {
                if (oldDataSource.Tables.Contains(dt.TableName.ToString()))
                {
                    oldDataSource.Tables.Remove(dt.TableName.ToString());
                }

                oldDataSource.Tables.Add(dt.Copy());
            }

            return oldDataSource;
        }

        /// <summary>
        /// adding decimal column
        /// </summary>
        /// <param name="columnName">column name</param>
        /// <param name="addDefaultValue">default value</param>
        /// <param name="defaultValue">another value</param>
        /// <returns>data column</returns>
        public static DataColumn AddDecimalColumn(string columnName, bool addDefaultValue = false, decimal defaultValue = 0)
        {
            DataColumn newColumn;
            newColumn = !string.IsNullOrEmpty(columnName) ? new DataColumn(columnName) : new DataColumn();
            newColumn.DataType = System.Type.GetType(SystemDecimal);
            if (addDefaultValue)
            {
                newColumn.DefaultValue = defaultValue;
            }

            return newColumn;
        }

        /// <summary>
        /// adding column to data table
        /// </summary>
        /// <param name="dataTable">data table</param>
        /// <param name="columnName">column name</param>
        /// <param name="columnType">column data type</param>
        /// <param name="defaultValue">default value</param>
        public static void AddColumnToDataTable(DataTable dataTable, string columnName, Type columnType = null, string defaultValue = "")
        {
            if (!DataManager.IsNullOrEmptyDataTable(dataTable) && !string.IsNullOrEmpty(columnName))
            {
                if (!dataTable.Columns.Contains(columnName))
                {
                    DataColumn dataColumn;
                    if (columnType != null)
                    {
                        dataColumn = new DataColumn(columnName, columnType);
                        if (!StringManager.IsEqual(defaultValue, string.Empty))
                        {
                            dataColumn.DefaultValue = Convert.ChangeType(defaultValue, columnType, CultureInfo.CurrentCulture);
                        }
                    }
                    else
                    {
                        dataColumn = new DataColumn(columnName);
                        if (!StringManager.IsEqual(defaultValue, string.Empty))
                        {
                            dataColumn.DefaultValue = defaultValue;
                        }
                    }

                    dataTable.Columns.Add(dataColumn);
                    dataTable.AcceptChanges();
                }
            }
        }

        /// <summary>
        /// add column to table column name should not be null column name should not already exists in data table 
        /// </summary>
        /// <param name="dataTable">data table</param>
        /// <param name="columnName">column name</param>
        /// <param name="columnType">column data type</param>
        /// <param name="defaultValue">default value</param>
        public static void AddColumn(DataTable dataTable, string columnName, Type columnType = null, string defaultValue = "")
        {
            if (!string.IsNullOrEmpty(columnName) && (!dataTable.Columns.Contains(columnName)))
            {
                DataColumn dataColumn;

                ////is valid data type
                if (columnType != null)
                {
                    dataColumn = new DataColumn(columnName, columnType);
                    if (!StringManager.IsEqual(defaultValue, string.Empty))
                    {
                        dataColumn.DefaultValue = Convert.ChangeType(defaultValue, columnType, CultureInfo.CurrentCulture);
                    }
                }
                else
                {
                    dataColumn = new DataColumn(columnName);
                    if (!StringManager.IsEqual(defaultValue, string.Empty))
                    {
                        dataColumn.DefaultValue = defaultValue;
                    }
                }

                ////add column to table
                dataTable.Columns.Add(dataColumn);
                dataTable.AcceptChanges();
            }
        }

        /// <summary>
        /// Returns Row Values of a given column as a delimiter separated upper case string
        /// </summary>
        /// <param name="dataTable">data table</param>
        /// <param name="columnName">column name</param>
        /// <param name="delimiter">delimiter value</param>
        /// <param name="returnUniqRowValues">is Unique Row Values</param>
        /// <returns>row value</returns>
        public static string GetRowValues(DataTable dataTable, string columnName, string delimiter, bool returnUniqRowValues = false)
        {
            if (!DataManager.IsNullOrEmptyDataTable(dataTable) && DataManager.IsDataTableHasColumn(dataTable, columnName))
            {
                StringBuilder rowValues = new StringBuilder();
                var distinctRows = returnUniqRowValues ?
                        (from DataRow dRow in dataTable.Rows
                         select dRow[columnName]).Distinct().ToArray() :
                        (from DataRow dRow in dataTable.Rows
                         select dRow[columnName]).ToArray();
                rowValues.Append(string.Join(delimiter, distinctRows));
                return rowValues.ToString().ToUpper(CultureInfo.InvariantCulture);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Convert list To DataTable
        /// </summary>
        /// <typeparam name="TSource">IEnumerable type</typeparam>
        /// <param name="source">data list</param>
        /// <returns>data table</returns>
        public static DataTable ConvertToDataTable<TSource>(IEnumerable<TSource> source)
        {
            var props = typeof(TSource).GetProperties();

            ////add column with property name and property type 
            var resultTable = new DataTable();
            resultTable.Locale = CultureInfo.InvariantCulture;
            resultTable.Columns.AddRange(props.Select(eachProp => new DataColumn(eachProp.Name, eachProp.PropertyType)).ToArray());

            ////add rows from source/collection to data table
            source.ToList().ForEach(rec => resultTable.Rows.Add(props.Select(p => p.GetValue(rec, null)).Distinct().ToArray()));

            return resultTable;
        }

        #endregion

        #region Linq

        /// <summary>
        /// Returns Columns name as delimiter separated upper case string
        /// </summary>
        /// <param name="dataTable">data table</param>
        /// <param name="delimiter">delimiter value</param>
        /// <returns>column name</returns>
        public static string GetColumnNames(DataTable dataTable, string delimiter, bool isDoubleQuoteRequired = true)
        {
            if (string.Equals(delimiter, TabDelimeter))
                delimiter = CSTabDelimeter;

            string doubleQuotes = DoubleQuotes;

            if (!isDoubleQuoteRequired)
                doubleQuotes = string.Empty;


            if (IsDataTableHasColumn(dataTable))
            {
                StringBuilder columnNames = new StringBuilder();

                var columns = dataTable.Columns.Cast<DataColumn>().Select(column =>
                                string.Concat(doubleQuotes, column.ColumnName.Replace(DoubleQuotes, DoubleDoubleQuotes), doubleQuotes)).ToArray();
                columnNames.Append(string.Join(delimiter, columns));

                return columnNames.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Checks or Uncheck the checkboxes in Checkbox list.
        /// </summary>
        /// <param name="list">list control</param>
        /// <param name="isToCheck">is To Check</param>
        public static void CheckOrUncheck(ListControl list, bool isToCheck = true)
        {
            if (list != null && list.Items.Count > 0)
            {
                (from eachCheckBox in list.Items.Cast<ListItem>()
                 select eachCheckBox).ToList().ForEach(chkBox => chkBox.Selected = isToCheck);
            }
        }

        /// <summary>
        /// Get Distinct Data Row Values
        /// </summary>
        /// <param name="dataTable">data table</param>
        /// <param name="columnName">column name</param>
        /// <returns>data row values collection</returns>
        public static string[] GetDistinctDataRowValues(DataTable dataTable, string columnName)
        {
            if (IsDataTableHasColumn(dataTable, columnName))
            {
                return dataTable.Rows.Cast<DataRow>().Select(row => row[columnName].ToString()).Distinct().ToArray();
            }
            else
            {
                return default(string[]);
            }
        }

        /// <summary>
        /// Get Distinct Data Row Values
        /// </summary>
        /// <param name="dataTable">data table</param>
        /// <param name="columnName">column name</param>
        /// <param name="delimiter">delimiter value</param>
        /// <returns>data row value</returns>
        public static string GetDistinctDataRowValues(DataTable dataTable, string columnName, string delimiter)
        {
            if (IsDataTableHasColumn(dataTable))
            {
                StringBuilder columnNames = new StringBuilder();
                var columns = dataTable.Rows.Cast<DataRow>().Select(row => string.Concat(SingleQuote, row[columnName].ToString(), SingleQuote)).Distinct().ToArray();
                columnNames.Append(string.Join(delimiter, columns));
                return columnNames.ToString().ToUpper(CultureInfo.InvariantCulture);
            }
            else
            {
                return string.Empty;
            }
        }

        #endregion

        #region Grid View

        #region GridView : Generate Grid Headr Cell

        /// <summary>
        /// Generate Grid view Header Cell
        /// </summary>
        /// <param name="text">header text</param>
        /// <param name="className">class value</param>
        /// <param name="columnSpan">column spam</param>
        /// <returns>Table Header Cell</returns>
        public static TableHeaderCell GenerateGridHeaderCell(string text, string className, int columnSpan)
        {
            TableHeaderCell headerCell = new TableHeaderCell();
            headerCell.CssClass = className;
            headerCell.Text = text;
            headerCell.ColumnSpan = columnSpan;
            return headerCell;
        }

        /// <summary>
        /// Adding Search Header
        /// </summary>
        /// <param name="tableRow">data row</param>
        /// <param name="header">header value</param>
        /// <param name="headerText">header text</param>
        /// <param name="cellPercentage">column width</param>
        /// <returns>table row</returns>
        public static TableRow AddSearchHeader(TableRow tableRow, string header, string headerText, double cellPercentage)
        {
            TableCell c1 = new TableCell();
            TableCell c2 = new TableCell();
            c1.Width = Unit.Percentage(cellPercentage);
            c1.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
            c2.Style.Add("text-align", "left");

            c1.Text = header + " :";
            c2.Text = headerText;
            c2.Font.Bold = true;

            tableRow.Cells.Add(c1);
            tableRow.Cells.Add(c2);

            return tableRow;
        }

        #endregion

        /// <summary>
        /// Get cell value from Grid view row
        /// </summary>
        /// <param name="row">table row</param>
        /// <param name="columnNumber">column number</param>
        /// <returns>column name</returns>
        public static string GetCellValue(TableRow row, int columnNumber)
        {
            string ret = row.Cells[columnNumber].Text.Trim();

            ////there is a bug that may set value to ^nbsp
            if (ret == "&nbsp;")
            {
                ret = string.Empty;
            }

            return ret;
        }

        /// <summary>
        /// Get Column Index based on grid header
        /// </summary>
        /// <param name="gridView">grid view</param>
        /// <param name="fieldName">column name</param>
        /// <returns>index of column</returns>
        public static int GetGridIndex(GridView gridView, string fieldName)
        {
            int columnCount = gridView.Columns.Count;
            for (int i = VectorConstants.Zero; i < columnCount; i++)
            {
                DataControlField field = gridView.Columns[i];

                ////Assuming accessing happens at data level, e.g with data field's name
                if (field.HeaderText.ToUpper(CultureInfo.InvariantCulture).Replace(" ", string.Empty) == fieldName.ToUpper(CultureInfo.InvariantCulture).Replace(" ", string.Empty))
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Get Grid View Field
        /// </summary>
        /// <param name="gridView">grid view control</param>
        /// <param name="fieldName">field name</param>
        /// <returns>grid view field</returns>
        public static BoundField GetGridViewField(GridView gridView, string fieldName)
        {
            int index = GetGridIndex(gridView, fieldName);
            return (index == -1) ? null : gridView.Columns[index] as BoundField;
        }

        /// <summary>
        /// Get Column Index based on grid header
        /// </summary>
        /// <param name="gridView">grid view</param>
        /// <param name="fieldName">column name</param>
        /// <returns>index of column</returns>
        public static int GetGridColumnIndex(GridView gridView, string accessibleHeaderText)
        {
            int columnCount = gridView.Columns.Count;
            for (int i = VectorConstants.Zero; i < columnCount; i++)
            {
                DataControlField field = gridView.Columns[i];

                ////Assuming accessing happens at data level, e.g with data field's name
                if (field.AccessibleHeaderText.ToUpper(CultureInfo.InvariantCulture).Replace(" ", string.Empty) == accessibleHeaderText.ToUpper(CultureInfo.InvariantCulture).Replace(" ", string.Empty))
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Get Grid View Text
        /// </summary>
        /// <param name="row">table row</param>
        /// <param name="fieldName">filed value</param>
        /// <returns>field text</returns>
        public static string GetGridViewText(TableRow row, string fieldName)
        {
            GridView grd = row.NamingContainer as GridView;
            if (grd != null)
            {
                int index = GetGridIndex(grd, fieldName);
                if (index != -1)
                {
                    return row.Cells[index].Text;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Get Cell index based on header text
        /// </summary>
        /// <param name="gridView">grid view</param>
        /// <param name="columnText">column value</param>
        /// <returns>cell index</returns>
        public static int GetIndex(GridView gridView, string columnText)
        {
            int columnCount = gridView.Columns.Count;
            for (int index = VectorConstants.Zero; index < columnCount; index++)
            {
                if (gridView.Columns[index].HeaderText.Equals(columnText))
                {
                    return index;
                }
            }

            return NoColumn;
        }

        /// <summary>
        /// Get Cell index based on header text but ignore space from header text
        /// </summary>
        /// <param name="gridView">grid view</param>
        /// <param name="columnText">column text</param>
        /// <param name="ignoreSpace">is ignore Space</param>
        /// <returns>cell index</returns>
        public static int GetIndex(GridView gridView, string columnText, bool ignoreSpace)
        {
            if (ignoreSpace)
            {
                int columnCount = gridView.Columns.Count;
                for (int index = VectorConstants.Zero; index < columnCount; index++)
                {
                    if (gridView.Columns[index].HeaderText.Replace(Space, string.Empty)
                                                    .Equals(columnText.Replace(Space, string.Empty)))
                    {
                        return index;
                    }
                }
            }

            return NoColumn;
        }

        /// <summary>
        /// Find the Row Index of a table provided column name and value returns -1 if value not found.
        /// example CommonLogic.FindRowIndexInDataTable(table, "Name", "company name")
        /// </summary>
        /// <param name="dataSource">data source</param>
        /// <param name="columnName">column name</param>
        /// <param name="value">filter value</param>
        /// <returns>row index</returns>
        public static int FindRowIndexInDataTable(DataTable dataSource, string columnName, string value)
        {
            if (DataManager.IsDataTableHasColumn(dataSource, columnName) && !string.IsNullOrEmpty(value))
            {
                DataRow[] rowArray = dataSource.Select(columnName + VectorConstants.Equal + string.Format(CultureInfo.CurrentCulture, VectorConstants.FilterValueFormat, value));
                if (rowArray.Length > VectorConstants.Zero)
                {
                    return dataSource.Rows.IndexOf(rowArray[VectorConstants.Zero]);
                }
            }

            return VectorConstants.NotFound;
        }

        /// <summary>
        /// returns the Rows of a table provided column name and value
        /// :returns null if column not found.
        /// :example CommonLogic.FindRowsInDataTable(dataSource, "Name", "Company name")
        /// </summary>
        /// <param name="dataSource">data source</param>
        /// <param name="columnName">column name</param>
        /// <param name="value">filter value</param>
        /// <returns>data row</returns>
        public static DataRow[] FindRowsInDataTable(DataTable dataSource, string columnName, string value)
        {
            DataRow[] dataRow = null;
            if (DataManager.IsDataTableHasColumn(dataSource, columnName) && !string.IsNullOrEmpty(value))
            {
                dataRow = dataSource.Select(columnName + VectorConstants.Equal + string.Format(CultureInfo.CurrentCulture, VectorConstants.FilterValueFormat, value));
            }

            return dataRow;
        }





        #endregion

        #region Sort Data

        /// <summary>
        /// Sort data table ascending or descending
        /// </summary>
        /// <param name="dataSource">data source</param>
        /// <param name="sortExpression">sort expression</param>
        /// <param name="sortDirection">sort direction</param>
        /// <returns>sorted data table</returns>
        public static DataTable SortDataTableAscendingOrDescending(DataTable dataSource, string sortExpression, string sortDirection)
        {
            if (!IsNullOrEmptyDataTable(dataSource))
            {
                if (!string.IsNullOrEmpty(sortDirection))
                {
                    if (StringManager.IsEqual(sortDirection, SortDirection.Ascending.ToString()))
                    {
                        if (sortExpression.ToUpper(CultureInfo.InvariantCulture).Contains(VectorConstants.Total))
                        {
                            dataSource = dataSource.AsEnumerable().OrderByDescending(row => row.Field<decimal>(sortExpression)).CopyToDataTable();
                        }
                        else
                        {
                            dataSource = dataSource.AsEnumerable().OrderByDescending(row => row.Field<string>(sortExpression)).CopyToDataTable();
                        }
                    }
                    else
                    {
                        if (sortExpression.ToUpper(CultureInfo.InvariantCulture).Contains(VectorConstants.Total))
                        {
                            dataSource = dataSource.AsEnumerable().OrderBy(row => row.Field<decimal>(sortExpression)).CopyToDataTable();
                        }
                        else
                        {
                            dataSource = dataSource.AsEnumerable().OrderBy(row => row.Field<string>(sortExpression)).CopyToDataTable();
                        }
                    }
                }
            }

            return dataSource;
        }

        #endregion

        #region Get Column Sum

        /// <summary>
        /// get column sum based on column index
        /// </summary>
        /// <param name="dataSource">data source</param>
        /// <param name="count">column index</param>
        /// <param name="decreaseCount">decrease Count</param>
        /// <returns>column sum</returns>
        public static decimal GetColumnsSum(DataTable dataSource, int count, int decreaseCount = 0)
        {
            decimal linqSum = (from DataRow dr in dataSource.AsEnumerable()
                               where dr.RowState != DataRowState.Deleted
                               && !dr[dataSource.Columns[count - decreaseCount]].ToString().Equals("-")
                               select string.IsNullOrEmpty(dr[dataSource.Columns[count - decreaseCount]].ToString()) ?
                               Convert.ToDecimal(0, CultureInfo.CurrentCulture) :
                               Convert.ToDecimal(dr[dataSource.Columns[count - decreaseCount]], CultureInfo.CurrentCulture)).Sum();

            return linqSum;
        }

        /// <summary>
        /// Get Columns Sum By Column Name
        /// </summary>
        /// <param name="dataSource">Data Source</param>
        /// <param name="columnName">column name</param>
        /// <returns>column sum</returns>
        public static decimal GetColumnsSumByColumnName(DataTable dataSource, string columnName)
        {
            decimal linqSum = (from DataRow dr in dataSource.AsEnumerable()
                               where dr.RowState != DataRowState.Deleted
                               && !dr[dataSource.Columns[columnName]].ToString().Equals("-")
                               select string.IsNullOrEmpty(dr[dataSource.Columns[columnName]].ToString()) ?
                               Convert.ToDecimal(0, CultureInfo.CurrentCulture) :
                               Convert.ToDecimal(dr[dataSource.Columns[columnName]], CultureInfo.CurrentCulture)).Sum();

            return linqSum;
        }

        #endregion

        #region Get Coloumn Max and Min Value

        /// <summary>
        /// Get Columns Max By Column Name
        /// </summary>
        /// <param name="dataSource">data source</param>
        /// <param name="columnName">column name</param>
        /// <returns>max in column</returns>
        public static decimal GetColumnsMaxByColumnName(DataTable dataSource, string columnName)
        {
            List<decimal> linqSum = dataSource.AsEnumerable().Select(al =>
                                        al.Field<decimal>(columnName)).Distinct().ToList();
            decimal max = linqSum.Max();
            return max;
        }

        /// <summary>
        /// Get Columns Min By Column Name
        /// </summary>
        /// <param name="dataSource">Data Source</param>
        /// <param name="columnName">column name</param>
        /// <returns>min value in column</returns>
        public static decimal GetColumnsMinByColumnName(DataTable dataSource, string columnName)
        {
            List<decimal> linqSum = dataSource.AsEnumerable().Select(al =>
                                        al.Field<decimal>(columnName)).Distinct().ToList();
            decimal min = linqSum.Min();
            return min;
        }

        #endregion

        #region Pivoting,Total row and Columns

        /// <summary>
        /// Adding Total Column and summing the values based on the Given Column Names in NameValue Collection
        /// </summary>
        /// <param name="dataSource">data Source</param>
        /// <param name="dataType">data Type</param>
        /// <param name="columns"> columns collection</param>
        /// <param name="operation">operation value</param>
        /// <param name="totalColumnHeader">total Column Header</param>
        /// <returns>Data Table</returns>
        public static DataTable AddTotalColumn(DataTable dataSource, Type dataType, NameValueCollection columns, string operation = "+", string totalColumnHeader = "Total")
        {
            //// create expression for column
            string expression = string.Empty;

            ////Get the column count
            int columnCount = columns.Keys.Count;

            ////Set the Counter
            int counter = 1;

            ////for each column in columnNames
            foreach (var str in columns.Keys)
            {
                ////if Counter not equal to  columnCount else don't append OPERATOR
                if (counter != columnCount)
                {
                    expression += string.Format(CultureInfo.CurrentCulture, FormatColumnWithNull + operation, columns.Get(str.ToString()));
                }
                else
                {
                    expression += string.Format(CultureInfo.CurrentCulture, FormatColumnWithNull, columns.Get(str.ToString()));
                }

                ////increment counter
                counter++;
            }

            //// create and add total column
            DataColumn totalColumn = new DataColumn(totalColumnHeader, dataType, expression);
            dataSource.Columns.Add(totalColumn);

            ////return the redefined data table
            return dataSource.Copy();
        }

        /// <summary>
        ///  Add total as row in the bottom of the Table for those columns in NameValueCollection
        /// </summary>
        /// <param name="dataSource">data source</param>
        /// <param name="columns">column collection</param>
        /// <param name="totalTitle">total column text</param>
        /// <param name="addTitleToColumn">Title To Column</param>
        /// <returns>data table with total row</returns>
        public static DataTable AddTotalRowByColumnNames(DataTable dataSource, NameValueCollection columns, string totalTitle = "Total", string addTitleToColumn = null)
        {
            //// Add total text give column name 
            DataRow totalRow = dataSource.NewRow();

            ////if addTitleToColumn COLUMN does not exists then don't add TOTAL 
            if (!string.IsNullOrEmpty(addTitleToColumn))
            {
                if (dataSource.Columns.Contains(addTitleToColumn))
                {
                    totalRow[addTitleToColumn] = totalTitle;
                }
            }

            ////For each column in keys calculate SUM and set to the row
            foreach (var column in columns.Keys)
            {
                ////Default value
                decimal totalValue = VectorConstants.Zero;

                ////check if the Column type is not String
                //// if (table.Columns[column.ToString()].DataType.ToString() != "System.String")
                totalValue = GetColumnsSumByColumnName(dataSource, column.ToString());
                ////float.Parse(table.Compute(SumExpressionStart + columns.Get(column.ToString()).ToString() + ClosingBracket, string.Empty).ToString());

                ////add total to the particular cell 
                totalRow[columns.Get(column.ToString()).ToString()] = totalValue;
            }

            dataSource.Rows.Add(totalRow);

            return dataSource.Copy();
        }

        /// <summary>
        /// Add total as row in the bottom of the Table for those columns in NameValueCollection
        /// </summary>
        /// <param name="dataSource">data source</param>
        /// <param name="totalTitle">total title</param>
        /// <param name="addTitleToColumn">Title To Column</param>
        /// <param name="addTitleByColumnCount">Title By Column Count</param>
        /// <param name="columnNumber">column Number</param>
        /// <param name="dataTypes">data Types</param>
        /// <returns>data table</returns>
        public static DataTable AddTotalRow(DataTable dataSource, string totalTitle = "Total", string addTitleToColumn = null, bool addTitleByColumnCount = false, int columnNumber = 0, Collection<string> dataTypes = null)
        {
            //// Add total text give column name 
            DataRow totalRow = dataSource.NewRow();

            //// if addTitleToColumn COLUMN does not exists then don't add TOTAL 
            if (!string.IsNullOrEmpty(addTitleToColumn))
            {
                if (dataSource.Columns.Contains(addTitleToColumn))
                {
                    totalRow[addTitleToColumn] = totalTitle;
                }
            }

            //// Add total by Column Number
            if (addTitleByColumnCount)
            {
                if (dataSource.Columns.Count >= columnNumber)
                {
                    totalRow[columnNumber] = totalTitle;
                }
            }

            Collection<string> columnType;
            if (dataTypes == null)
            {
                columnType = GetColumnsForSum;
            }
            else
            {
                columnType = dataTypes;
            }

            //// For each column in keys calculate SUM and set to the row
            foreach (DataColumn column in dataSource.Columns)
            {
                //// Default value
                decimal totalValue = VectorConstants.Zero;

                //// check if the Column type contains the value for sum
                if (columnType.Contains(dataSource.Columns[column.ColumnName.ToString()].DataType.ToString()))
                {
                    totalValue = GetColumnsSumByColumnName(dataSource, column.ColumnName.ToString());
                }
                //// float.Parse(table.Compute(SumExpressionStart + columns.Get(column.ToString()).ToString() + ClosingBracket, string.Empty).ToString());

                if (!StringManager.IsEqual(totalRow[column.ColumnName].ToString(), totalTitle))
                {
                    //// add total to the particular cell 
                    totalRow[column.ColumnName] = totalValue;
                }
            }

            dataSource.Rows.Add(totalRow);
            return dataSource;
        }

        /// <summary>
        /// Add Total Row To Table
        /// </summary>
        /// <param name="dataSource">data source</param>
        /// <param name="totalTitle">total title</param>
        /// <param name="columnNumber">column number</param>
        /// <returns>data table with total row</returns>
        public static DataTable AddTotalRowToTable(DataTable dataSource, string totalTitle, int columnNumber = 0)
        {
            //// Add total text give column name 
            DataRow totalRow = dataSource.NewRow();

            //// if addTitleToColumn COLUMN does not exists then don't add TOTAL 
            if (!string.IsNullOrEmpty(totalTitle))
            {
                totalRow[columnNumber] = totalTitle;
            }

            for (int count = VectorConstants.One; count < dataSource.Columns.Count; count++)
            { //// Default value
                decimal linqSum = VectorConstants.Zero;
                linqSum = GetColumnsSum(dataSource, count);
                totalRow[count] = linqSum;
            }

            dataSource.Rows.Add(totalRow);
            dataSource.AcceptChanges();

            return dataSource;
        }

        /// <summary>
        /// Add Total Row To Table
        /// </summary>
        /// <param name="dataSource">data source</param>
        /// <param name="totalColumnIndex">total Column Index</param>
        /// <param name="totalValue">total Value</param>
        /// <param name="columnIndex">column Index</param>
        /// <param name="addRowAt">add Row At</param>
        /// <returns>data table</returns>
        public static DataTable AddTotalRowToTable(DataTable dataSource, int totalColumnIndex, string totalValue, int[] columnIndex, int addRowAt = 0)
        {
            //// Add total text give column name 
            DataRow totalRow = dataSource.NewRow();

            ////if addTitleToColumn COLUMN does not exists then don't add TOTAL 
            if (dataSource.Columns.Count > totalColumnIndex)
            {
                totalRow[totalColumnIndex] = totalValue;
            }

            for (int count = VectorConstants.One; count < dataSource.Columns.Count; count++)
            { ////Default value

                if (columnIndex.Contains(count))
                {
                    decimal linqSum = VectorConstants.Zero;
                    linqSum = GetColumnsSum(dataSource, count);
                    totalRow[count] = linqSum;
                }
            }

            if (StringManager.IsNotEqual(Convert.ToString(addRowAt, CultureInfo.CurrentCulture), Convert.ToString(VectorConstants.Zero, CultureInfo.CurrentCulture)))
            {
                dataSource.Rows.InsertAt(totalRow, addRowAt);
            }
            else
            {
                dataSource.Rows.InsertAt(totalRow, dataSource.Rows.Count);
            }

            dataSource.AcceptChanges();

            return dataSource;
        }

        /// <summary>
        /// Pivot Columns based on the value
        /// </summary>
        /// <param name="mainTable">main data source</param>
        /// <param name="pivotColumnName">pivot Column Name</param>
        /// <param name="aggrigateColumn">aggregate Column</param>
        /// <param name="pivotColumn2">pivot Column Two</param>
        /// <param name="selectCols">selected columns</param>
        /// <returns>data table</returns>
        public static DataTable Pivot(
            DataTable mainTable,
            DataColumn pivotColumnName,
            DataColumn aggrigateColumn,
            DataColumn pivotColumn2 = null,
            string[] selectCols = null, string OrderByDateType = "", bool setNullsForNoData = false, bool isOrderbyRequired = true)
        {
            //if (!IsNullOrEmptyDataTable(mainTable) && mainTable.Select("" + pivotColumnName.ColumnName + " <> ''").Count() > 0)
            if (!IsNullOrEmptyDataTable(mainTable))
            {
                bool flag = false;

                if (pivotColumnName.DataType == typeof(string) && mainTable.Select("" + pivotColumnName.ColumnName + " <> ''").Count() > 0)
                    flag = true;

                else if (mainTable.Select("" + pivotColumnName.ColumnName + " is not null").Count() > 0)
                    flag = true;

                if (flag)
                {
                    DataView dv = new DataView(mainTable.Copy());
                    if (pivotColumnName.DataType == typeof(string))
                        dv.RowFilter = pivotColumnName.ColumnName + " <> ''";
                    else
                        dv.RowFilter = pivotColumnName.ColumnName + " is not null";
                    // mainTable = mainTable.Select("" + pivotColumnName.ColumnName + " <> ''").CopyToDataTable();
                    mainTable = dv.ToTable();
                }
            }

            ////Copy table
            DataTable data = mainTable.Copy();

            ////Remove the pivot Columns as we don't need them in the result table
            data.Columns.Remove(pivotColumnName.ColumnName);
            data.Columns.Remove(aggrigateColumn.ColumnName);


            if (pivotColumn2 != null)
            {
                if (data.Columns.Contains(pivotColumn2.ColumnName))
                {
                    data.Columns.Remove(pivotColumn2.ColumnName);
                }
            }

            string[] resultColumnNames = null;
            if (selectCols == null)
            {
                ////Get all the columns which should be in result table
                resultColumnNames = data.Columns.Cast<DataColumn>()
                .Select(c => c.ColumnName)
                .ToArray();
            }
            else
            {
                resultColumnNames = selectCols;
            }

            //// prepare results table with Existing Main Columns
            DataTable result = data.DefaultView.ToTable(true, resultColumnNames).Copy();
            var columns = result.Columns.Cast<DataColumn>().ToArray();
            //foreach (var clm in columns)
            //{
            //    // check column values for null 
            //    if (result.AsEnumerable().All(dr => dr.IsNull(clm)))
            //    {
            //        // remove all null value columns 
            //        result.Columns.Remove(clm);
            //        resultColumnNames = resultColumnNames.Where(val => val != clm.ToString()).ToArray();
            //    }
            //}

            result.PrimaryKey = result.Columns.Cast<DataColumn>().ToArray();

            if (pivotColumn2 != null)
            {
                if (string.IsNullOrEmpty(OrderByDateType))
                    mainTable.AsEnumerable().OrderBy(x => x[pivotColumnName.ColumnName].ToString())
                        .Select(r => r[pivotColumnName.ColumnName].ToString() + "," + r[pivotColumn2.ColumnName])
                        .Distinct().ToList()
                        .ForEach(c => result.Columns.Add(c, aggrigateColumn.DataType));
                else
                    mainTable.AsEnumerable().OrderBy(x => Convert.ToDateTime(x[pivotColumnName.ColumnName]))
                    .Select(r => r[pivotColumnName.ColumnName].ToString() + "," + r[pivotColumn2.ColumnName])
                    .Distinct().ToList()
                    .ForEach(c => result.Columns.Add(c, aggrigateColumn.DataType));

            }
            else
            {

                if (string.IsNullOrEmpty(OrderByDateType))
                {
                    if (isOrderbyRequired)
                        mainTable.AsEnumerable().OrderBy(x => x[pivotColumnName.ColumnName].ToString())
                  .Select(r => r[pivotColumnName.ColumnName].ToString())
                  .Distinct().ToList()
                  .ForEach(c => result.Columns.Add(c, aggrigateColumn.DataType));
                    else
                        mainTable.AsEnumerable()
                 .Select(r => r[pivotColumnName.ColumnName].ToString())
                 .Distinct().ToList()
                 .ForEach(c => result.Columns.Add(c, aggrigateColumn.DataType));
                }
                else
                    mainTable.AsEnumerable().OrderBy(x => Convert.ToDateTime(x[pivotColumnName.ColumnName]))
                   .Select(r => r[pivotColumnName.ColumnName].ToString())
                   .Distinct().ToList()
                   .ForEach(c => result.Columns.Add(c, aggrigateColumn.DataType));
            }

            ////assign data in each row
            foreach (DataRow row in mainTable.Rows)
            {
                //// find row to update
                DataRow aggRow = result.Rows.Find(
                    resultColumnNames
                        .Select(c => row[c])
                        .ToArray());

                for (int i = VectorConstants.Zero; i < aggRow.ItemArray.Count(); i++)
                {
                    if (string.IsNullOrEmpty(aggRow.ItemArray[i].ToString()))
                    {
                        if (setNullsForNoData)
                            aggRow[i] = DBNull.Value;
                        else
                            aggRow[i] = 0.00;
                    }
                }

                if (pivotColumn2 != null)
                {
                    ////add the value for that particular cell of the row
                    aggRow[row[pivotColumnName.ColumnName].ToString() + "," + row[pivotColumn2.ColumnName]] = row[aggrigateColumn.ColumnName] != null && row[aggrigateColumn.ColumnName] != DBNull.Value ?
                        row[aggrigateColumn.ColumnName] : VectorConstants.Zero;
                }
                else
                {
                    if (!string.IsNullOrEmpty(row[pivotColumnName.ColumnName].ToString()))
                        aggRow[row[pivotColumnName.ColumnName].ToString()] = row[aggrigateColumn.ColumnName] != null && row[aggrigateColumn.ColumnName] != DBNull.Value
                            ? row[aggrigateColumn.ColumnName] : VectorConstants.Zero;
                }
            }

            return result;
        }


        /// <summary>
        /// Pivot Columns based on the value
        /// </summary>
        /// <param name="mainTable">main data source</param>
        /// <param name="pivotColumnName">pivot Column Name</param>
        /// <param name="aggrigateColumn">aggregate Column</param>
        /// <param name="pivotColumn2">pivot Column Two</param>
        /// <param name="selectCols">selected columns</param>
        /// <returns>data table</returns>
        public static DataTable PivotVendor(
            DataTable mainTable,
            DataColumn pivotColumnName,
            DataColumn aggrigateColumn,
            DataColumn pivotColumn2 = null,
            string[] selectCols = null, string OrderByDateType = "")
        {
            if (!IsNullOrEmptyDataTable(mainTable) && mainTable.Select("" + pivotColumnName.ColumnName + " <> ''").Count() > 0)
                mainTable = mainTable.Select("" + pivotColumnName.ColumnName + " <> ''").CopyToDataTable();
            ////Copy table
            DataTable data = mainTable.Copy();

            ////Remove the pivot Columns as we don't need them in the result table
            data.Columns.Remove(pivotColumnName.ColumnName);
            data.Columns.Remove(aggrigateColumn.ColumnName);

            if (pivotColumn2 != null)
            {
                if (data.Columns.Contains(pivotColumn2.ColumnName))
                {
                    data.Columns.Remove(pivotColumn2.ColumnName);
                }
            }

            string[] resultColumnNames = null;
            if (selectCols == null)
            {
                ////Get all the columns which should be in result table
                resultColumnNames = data.Columns.Cast<DataColumn>()
                .Select(c => c.ColumnName)
                .ToArray();
            }
            else
            {
                resultColumnNames = selectCols;
            }

            //// prepare results table with Existing Main Columns
            DataTable result = data.DefaultView.ToTable(true, resultColumnNames).Copy();
            result.PrimaryKey = result.Columns.Cast<DataColumn>().ToArray();

            if (pivotColumn2 != null)
            {
                if (string.IsNullOrEmpty(OrderByDateType))
                    mainTable.AsEnumerable().OrderBy(x => x[pivotColumnName.ColumnName].ToString())
                        .Select(r => r[pivotColumnName.ColumnName].ToString() + "," + r[pivotColumn2.ColumnName])
                        .Distinct().ToList()
                        .ForEach(c => result.Columns.Add(c, aggrigateColumn.DataType));
                else
                    mainTable.AsEnumerable().OrderBy(x => Convert.ToDateTime(x[pivotColumnName.ColumnName]))
                    .Select(r => r[pivotColumnName.ColumnName].ToString() + "," + r[pivotColumn2.ColumnName])
                    .Distinct().ToList()
                    .ForEach(c => result.Columns.Add(c, aggrigateColumn.DataType));

            }
            else
            {
                if (string.IsNullOrEmpty(OrderByDateType))
                    mainTable.AsEnumerable().OrderBy(x => x[pivotColumnName.ColumnName].ToString())
              .Select(r => r[pivotColumnName.ColumnName].ToString())
              .Distinct().ToList()
              .ForEach(c => result.Columns.Add(c, aggrigateColumn.DataType));
                else
                    mainTable.AsEnumerable().OrderBy(x => Convert.ToDateTime(x[pivotColumnName.ColumnName]))
                   .Select(r => r[pivotColumnName.ColumnName].ToString())
                   .Distinct().ToList()
                   .ForEach(c => result.Columns.Add(c, aggrigateColumn.DataType));
            }

            ////assign data in each row
            foreach (DataRow row in mainTable.Rows)
            {
                //// find row to update
                DataRow aggRow = result.Rows.Find(
                    resultColumnNames
                        .Select(c => row[c])
                        .ToArray());

                for (int i = VectorConstants.Zero; i < aggRow.ItemArray.Count(); i++)
                {
                    if (string.IsNullOrEmpty(aggRow.ItemArray[i].ToString()))
                    {
                        aggRow[i] = DBNull.Value;
                    }
                }

                if (pivotColumn2 != null)
                {
                    ////add the value for that particular cell of the row
                    aggRow[row[pivotColumnName.ColumnName].ToString() + "," + row[pivotColumn2.ColumnName]] = row[aggrigateColumn.ColumnName] != null ?
                        row[aggrigateColumn.ColumnName] : "";
                }
                else
                {
                    if (!string.IsNullOrEmpty(row[pivotColumnName.ColumnName].ToString()))
                        aggRow[row[pivotColumnName.ColumnName].ToString()] = row[aggrigateColumn.ColumnName] != null ? row[aggrigateColumn.ColumnName] : VectorConstants.Zero;
                }
            }

            return result;
        }
        public static DataTable PivotWithOrderByIntType(
          DataTable mainTable,
          DataColumn pivotColumnName,
          DataColumn aggrigateColumn,
          DataColumn pivotColumn2 = null,
          string[] selectCols = null, string OrderByDateType = "", bool setNullsForNoData = false)
        {
            ////Copy table
            DataTable data = mainTable.Copy();

            ////Remove the pivot Columns as we don't need them in the result table
            data.Columns.Remove(pivotColumnName.ColumnName);
            data.Columns.Remove(aggrigateColumn.ColumnName);

            if (pivotColumn2 != null)
            {
                if (data.Columns.Contains(pivotColumn2.ColumnName))
                {
                    data.Columns.Remove(pivotColumn2.ColumnName);
                }
            }

            string[] resultColumnNames = null;
            if (selectCols == null)
            {
                ////Get all the columns which should be in result table
                resultColumnNames = data.Columns.Cast<DataColumn>()
                .Select(c => c.ColumnName)
                .ToArray();
            }
            else
            {
                resultColumnNames = selectCols;
            }

            //// prepare results table with Existing Main Columns
            DataTable result = data.DefaultView.ToTable(true, resultColumnNames).Copy();
            result.PrimaryKey = result.Columns.Cast<DataColumn>().ToArray();

            if (pivotColumn2 != null)
            {
                if (string.IsNullOrEmpty(OrderByDateType))
                    mainTable.AsEnumerable().OrderBy(x => x[pivotColumnName.ColumnName].ToString())
                        .Select(r => r[pivotColumnName.ColumnName].ToString() + "," + r[pivotColumn2.ColumnName])
                        .Distinct().ToList()
                        .ForEach(c => result.Columns.Add(c, aggrigateColumn.DataType));
                else
                    mainTable.AsEnumerable().OrderBy(x => Convert.ToInt32(x[pivotColumnName.ColumnName]))
                  .Select(r => r[pivotColumnName.ColumnName].ToString() + "," + r[pivotColumn2.ColumnName])
                  .Distinct().ToList()
                  .ForEach(c => result.Columns.Add(c, aggrigateColumn.DataType));


            }
            else
            {
                if (string.IsNullOrEmpty(OrderByDateType))
                    mainTable.AsEnumerable().OrderBy(x => Convert.ToInt32(x[pivotColumnName.ColumnName]))
              .Select(r => r[pivotColumnName.ColumnName].ToString())
              .Distinct().ToList()
              .ForEach(c => result.Columns.Add(c, aggrigateColumn.DataType));
                else
                    mainTable.AsEnumerable().OrderBy(x => Convert.ToInt32(x[pivotColumnName.ColumnName]))
                   .Select(r => r[pivotColumnName.ColumnName].ToString())
                   .Distinct().ToList()
                   .ForEach(c => result.Columns.Add(c, aggrigateColumn.DataType));
            }

            ////assign data in each row
            foreach (DataRow row in mainTable.Rows)
            {
                //// find row to update
                DataRow aggRow = result.Rows.Find(
                    resultColumnNames
                        .Select(c => row[c])
                        .ToArray());

                for (int i = VectorConstants.Zero; i < aggRow.ItemArray.Count(); i++)
                {
                    if (string.IsNullOrEmpty(aggRow.ItemArray[i].ToString()))
                    {
                        if (setNullsForNoData)
                            aggRow[i] = DBNull.Value;
                        else
                            aggRow[i] = 0.00;
                    }
                }

                if (pivotColumn2 != null)
                {
                    ////add the value for that particular cell of the row
                    aggRow[row[pivotColumnName.ColumnName].ToString() + "," + row[pivotColumn2.ColumnName]] = row[aggrigateColumn.ColumnName] != null ?
                        row[aggrigateColumn.ColumnName] : VectorConstants.Zero;
                }
                else
                {
                    aggRow[row[pivotColumnName.ColumnName].ToString()] = row[aggrigateColumn.ColumnName] != null ? row[aggrigateColumn.ColumnName] : VectorConstants.Zero;
                }
            }

            return result;
        }

        /// <summary>
        /// Split data table into two/Three based on the NameValueCollection given
        /// </summary>
        /// <param name="sourceTable">source Table</param>
        /// <param name="firstTableColumns">first Table Columns</param>
        /// <param name="secondTableColumns">second Table Columns</param>
        /// <param name="thirdTableCollection">third Table Collection</param>
        /// <param name="removeColumns">remove Columns</param>
        /// <param name="firstTableName">first Table Name</param>
        /// <param name="secondTableName">second Table Name</param>
        /// <param name="thirdTableName">third Table Name</param>
        /// <param name="removeFromExistingTable">remove From Existing Table</param>
        /// <returns>Data Set</returns>
        public static DataSet SplitMainTable(
            DataTable sourceTable,
            NameValueCollection firstTableColumns,
            NameValueCollection secondTableColumns = null,
            NameValueCollection thirdTableCollection = null,
            NameValueCollection removeColumns = null,
            string firstTableName = null,
            string secondTableName = null,
            string thirdTableName = null,
            bool removeFromExistingTable = false)
        {
            ////data set for adding tables
            DataSet dataSource = new DataSet();
            dataSource.Locale = CultureInfo.InvariantCulture;

            ////check if To split the table to multiple tables or Remove Columns from Existing table
            if (!removeFromExistingTable)
            {
                ////First split table
                DataTable firstTable = sourceTable.Copy();
                DataTable secondTable = null;
                DataTable thirdTable = null;

                ////get all unwanted columns from the list
                var columnsOne = (from DataColumn c in firstTable.Columns
                                  where firstTableColumns.Keys.OfType<string>().Where(s => firstTableColumns.Get(s) == c.ColumnName).ToList().Count == 0
                                  select c.ColumnName).ToList();

                ////remove unwanted columns
                foreach (var col in columnsOne)
                {
                    if (firstTable.Columns.Contains(col))
                    {
                        firstTable.Columns.Remove(col);
                    }
                }

                ////Check if second table needed
                if (secondTableColumns != null)
                {
                    ////copy source
                    secondTable = sourceTable.Copy();

                    ////get unwanted columns
                    var columnThree = (from DataColumn c in sourceTable.Columns
                                       where secondTableColumns.Keys.OfType<string>().Where(s => secondTableColumns.Get(s) == c.ColumnName).ToList().Count == 0
                                       select c.ColumnName).ToList();

                    ////remove unwanted columns
                    foreach (var col in columnThree)
                    {
                        if (secondTable.Columns.Contains(col))
                        {
                            secondTable.Columns.Remove(col);
                        }
                    }
                }

                ////check if third table Collection is given
                if (thirdTableCollection != null)
                {
                    ////copy source
                    thirdTable = sourceTable.Copy();

                    ////get column which are not in the list
                    var columnTwo = (from DataColumn c in sourceTable.Columns
                                     where thirdTableCollection.Keys.OfType<string>().Where(s => thirdTableCollection.Get(s) == c.ColumnName).ToList().Count == 0
                                     select c.ColumnName).ToList();

                    ////remove the unwanted item
                    foreach (var col in columnTwo)
                    {
                        if (thirdTable.Columns.Contains(col))
                        {
                            thirdTable.Columns.Remove(col);
                        }
                    }
                }

                ////Set the Table Name
                if (string.IsNullOrEmpty(firstTableName))
                {
                    firstTable.TableName = "FirstTable";
                }
                else
                {
                    firstTable.TableName = firstTableName;
                }

                ////Add the table to the DatSet
                dataSource.Tables.Add(firstTable);

                ////if Second table is not null 
                if (secondTable != null)
                {
                    ////check if table name is provided
                    if (string.IsNullOrEmpty(secondTableName))
                    {
                        secondTable.TableName = "SecondTable";
                    }
                    else
                    {
                        secondTable.TableName = secondTableName;
                    }

                    ////add the second table to result dataset
                    dataSource.Tables.Add(secondTable);
                }

                if (thirdTable != null)
                {
                    ////check if table name is provided
                    if (string.IsNullOrEmpty(thirdTableName))
                    {
                        thirdTable.TableName = "ThirdTable";
                    }
                    else
                    {
                        thirdTable.TableName = thirdTableName;
                    }

                    ////add the Third table to result dataset
                    dataSource.Tables.Add(thirdTable);
                }
            }
            else
            {
                ////copy table
                DataTable firstTable = sourceTable.Copy();

                ////remove the columns
                foreach (var col in removeColumns.Keys)
                {
                    if (!string.IsNullOrEmpty(removeColumns.Get(col.ToString())))
                    {
                        if (firstTable.Columns.Contains(removeColumns.Get(col.ToString())))
                        {
                            firstTable.Columns.Remove(removeColumns.Get(col.ToString()));
                        }
                    }
                }

                ////Add table to the DataSet
                dataSource.Tables.Add(firstTable);
            }

            return dataSource;
        }

        #endregion

        #region Percentage Column

        /// <summary>
        /// Add Percentage Columns Based on the given NameValueCollection
        /// </summary>
        /// <param name="dataSource">data source</param>
        /// <param name="dataType">data type</param>
        /// <param name="columns">column collection</param>
        /// <returns>data table</returns>
        public static DataTable AddPercentColumns(DataTable dataSource, Type dataType, NameValueCollection columns)
        {
            ////Based on the given column names in Collection
            foreach (var column in columns.Keys)
            {
                ////Expression for Setting the Percentage Expression
                string expression = string.Empty;

                ////Format the Expression for PERCENTAGE = (Column/SUM(Column))*100 
                expression = OpeningBracket + string.Format(CultureInfo.CurrentCulture, FormatColumn, column.ToString())
                                            + DividedBy
                                            + SumExpressionStart
                                            + column
                                            + ClosingBracket
                                            + ClosingPercentExp;

                ////Add DataColumn based on Column Name,Data type and Expression
                DataColumn totalColumn = new DataColumn(columns.Get(column.ToString()).ToString(), dataType, expression);
                dataSource.Columns.Add(totalColumn);
            }

            ////Return Data table
            return dataSource;
        }

        /// <summary>
        /// Add Percentage Columns Based on the given NameValueCollection
        /// </summary>
        /// <param name="dataSource">data Source</param>
        /// <param name="existingColumn">existing Column</param>
        /// <param name="newColumn">new Column</param>
        /// <param name="valueType">value Type</param>
        /// <returns>data table</returns>
        public static DataTable AddPercentColumns(DataTable dataSource, string existingColumn, string newColumn, Type valueType)
        {
            if (!IsNullOrEmptyDataTable(dataSource))
            {
                dataSource.Columns.Add(newColumn, valueType);
                double value = 0.00;
                double sum = Convert.ToInt64(dataSource.Compute("SUM(" + existingColumn + ")", string.Empty), CultureInfo.CurrentCulture);

                foreach (DataRow row in dataSource.Rows)
                {
                    value = Convert.ToDouble(Convert.ToInt64(row[existingColumn], CultureInfo.CurrentCulture) * 100 / sum, CultureInfo.CurrentCulture);

                    if (StringManager.IsNotEqual(Convert.ToString(value, CultureInfo.CurrentCulture), "NAN"))
                    {
                        row[newColumn] = Convert.ToDecimal(string.Format(CultureInfo.CurrentCulture, "{0:0.00}", value), CultureInfo.CurrentCulture);
                    }
                    else
                    {
                        row[newColumn] = "0.0";
                    }
                }

                dataSource.AcceptChanges();
            }

            ////Return Data table
            return dataSource;
        }

        /// <summary>
        ///  Return group of periods[as DataTable] splitter by given period interval provided From date and To date
        /// </summary>
        /// <param name="fromDate">from Date</param>
        /// <param name="toDate">to Date</param>
        /// <param name="period">period value</param>
        /// <param name="sequenceName">sequence Name</param>
        /// <param name="addColumn">add Column</param>
        /// <param name="columnName">column Name</param>
        /// <param name="type">data type</param>
        /// <param name="characterLength">character Length</param>
        /// <returns>data table</returns>
        public static DataTable ListPeriods(
                      DateTime fromDate,
                      DateTime toDate,
                      int period,
                      string sequenceName,
                      bool addColumn = false,
                      string columnName = null,
                      string type = null,
                      int characterLength = 0, bool addAccrualGenerationDate = false)
        {
            if (StringManager.IsNotEqual(Convert.ToString(characterLength, CultureInfo.CurrentCulture), Convert.ToString(VectorConstants.Zero, CultureInfo.CurrentCulture)))
            {
                if (sequenceName.Length > characterLength)
                {
                    sequenceName = sequenceName.Substring(0, characterLength);
                }
            }

            DataTable periodData = new DataTable(Period);
            periodData.Locale = CultureInfo.InvariantCulture;

            DataColumn colIdentifier = new DataColumn(Id);
            colIdentifier.AutoIncrement = true;
            colIdentifier.AutoIncrementSeed = VectorConstants.One;
            colIdentifier.AutoIncrementStep = VectorConstants.One;
            DataColumn colPeriodName = new DataColumn(Name);
            DataColumn colFromPeriod = new DataColumn(From);
            colFromPeriod.DataType = typeof(DateTime);
            DataColumn colToPeriod = new DataColumn(To);
            colToPeriod.DataType = typeof(DateTime);


            DataColumn colNoOfDays = new DataColumn(NoOfDays);
            periodData.Columns.Add(colIdentifier);
            periodData.Columns.Add(colPeriodName);
            periodData.Columns.Add(colFromPeriod);
            periodData.Columns.Add(colToPeriod);
            periodData.Columns.Add(colNoOfDays);
            if (addAccrualGenerationDate)
            {
                DataColumn colAccrualGenDate = new DataColumn("Accrual_Generation_Date");
                colAccrualGenDate.DataType = typeof(DateTime);
                colAccrualGenDate.Expression = "To";
                periodData.Columns.Add(colAccrualGenDate);
            }
            int periodDays = ((toDate.Subtract(fromDate).Days + VectorConstants.One) / period) - VectorConstants.One;

            if (addColumn)
            {
                if (!string.IsNullOrEmpty(columnName))
                {
                    periodData.Columns.Add(columnName);
                    DataColumn dataColumn = new DataColumn();
                    dataColumn.ColumnName = "Visibility";
                    dataColumn.DataType = typeof(bool);
                    periodData.Columns.Add(dataColumn);

                    if (!string.IsNullOrEmpty(type))
                    {
                        DataColumn column = new DataColumn();
                        column.ColumnName = "Type";
                        column.DataType = typeof(bool);
                        periodData.Columns.Add(column);
                    }
                }
            }

            ////bool isLengthUpdated = false;

            DateTime previousDate = fromDate;
            if (periodDays > VectorConstants.Zero)
            {
                for (int indexDate = VectorConstants.One; indexDate <= period; indexDate++, previousDate = previousDate.AddDays(VectorConstants.One))
                {
                    DataRow eachPeriod = periodData.NewRow();
                    eachPeriod[Name] = sequenceName + VectorConstants.Underscore + Convert.ToString(indexDate, CultureInfo.CurrentCulture);
                    eachPeriod[From] = previousDate.Date;
                    previousDate = previousDate.AddDays(periodDays);
                    eachPeriod[To] = indexDate == period ? toDate.Date : previousDate.Date;
                    eachPeriod[NoOfDays] = Convert.ToString(Convert.ToDateTime(eachPeriod[To], CultureInfo.CurrentCulture).Subtract(Convert.ToDateTime(eachPeriod[From], CultureInfo.CurrentCulture)).Days, CultureInfo.CurrentCulture);

                    if (!string.IsNullOrEmpty(type))
                    {
                        if (StringManager.IsEqual(type, "Uniform"))
                        {
                            eachPeriod["Type"] = false;
                        }
                        else
                        {
                            eachPeriod["Type"] = true;
                        }
                    }

                    eachPeriod["Visibility"] = true;
                    periodData.Rows.Add(eachPeriod);
                }
            }
            else
            {
                DataRow eachPeriod = periodData.NewRow();
                eachPeriod[Name] = sequenceName;
                eachPeriod[From] = previousDate.Date;
                eachPeriod[To] = toDate.Date;
                eachPeriod[NoOfDays] = Convert.ToString(Convert.ToDateTime(eachPeriod[To], CultureInfo.CurrentCulture).Subtract(Convert.ToDateTime(eachPeriod[From], CultureInfo.CurrentCulture)).Days, CultureInfo.CurrentCulture);
                eachPeriod["Visibility"] = 1;
                periodData.Rows.Add(eachPeriod);
            }

            return periodData;
        }

        /// <summary>
        /// Add Column To Data Table
        /// </summary>
        /// <param name="dataSource">data Source</param>
        /// <param name="tableIndex">table Index</param>
        /// <param name="columnName">column Name</param>
        /// <param name="columnDataType">column Data Type</param>
        /// <param name="defaultValue">default Value</param>
        public static void AddColumnToDataTable(DataSet dataSource, int tableIndex, string columnName, Type columnDataType, string defaultValue)
        {
            DataColumn gallonsColumn = new DataColumn(columnName, columnDataType, defaultValue);

            if (dataSource.Tables.Count > tableIndex)
            {
                dataSource.Tables[tableIndex].Columns.Add(gallonsColumn);
            }
        }

        #endregion

        #region Merge

        /// <summary>
        /// Merge Data Set With Data Table by table name
        /// </summary>
        /// <param name="dataSource">data Source</param>
        /// <param name="newDataTable">Data Table</param>
        /// <param name="tableName">table Name</param>
        /// <returns>Data Set</returns>
        public static DataSet MergeDataSetWithDataTable(DataSet dataSource, DataTable newDataTable, string tableName)
        {
            DataSet oldDataSource = null;

            if (!IsNullOrEmptyDataSet(dataSource))
            {
                oldDataSource = dataSource.Copy();
            }
            else
            {
                oldDataSource = new DataSet();
                oldDataSource.Locale = CultureInfo.InvariantCulture;
            }

            if (!string.IsNullOrEmpty(tableName))
            {
                if (!IsNullOrEmptyDataTable(newDataTable))
                {
                    newDataTable.TableName = tableName;
                }
            }

            if (!IsNullOrEmptyDataSet(oldDataSource))
            {
                if (oldDataSource.Tables.Contains(tableName))
                {
                    oldDataSource.Tables.Remove(tableName);
                }
            }

            if (!IsNullOrEmptyDataTable(newDataTable))
            {
                oldDataSource.Tables.Add(newDataTable.Copy());
            }

            return oldDataSource;
        }

        #endregion

        #region Filter Table and Rows

        /// <summary>
        /// Filter Table
        /// </summary>
        /// <param name="dataSource">data Source</param>
        /// <param name="filterValue">filter Value</param>
        /// <param name="action">action value</param>
        /// <param name="skipAndTake">skip And Take</param>
        /// <param name="skipTakeValue">skip Take Value</param>
        /// <returns>Data Table</returns>
        public static DataTable FilterTable(DataTable dataSource, int filterValue, string action = "Take", string skipAndTake = "", int skipTakeValue = 0)
        {
            if (StringManager.IsEqual(action, VectorEnums.Filter.Take.ToString()) &&
                StringManager.IsNotEqual(Convert.ToString(filterValue, CultureInfo.CurrentCulture), Convert.ToString(VectorConstants.Zero, CultureInfo.CurrentCulture)))
            {
                return dataSource.Rows.OfType<DataRow>().ToList().Take(filterValue).CopyToDataTable();
            }
            else if (StringManager.IsEqual(action, VectorEnums.Filter.Skip.ToString()) &&
                     StringManager.IsNotEqual(Convert.ToString(filterValue, CultureInfo.CurrentCulture), Convert.ToString(VectorConstants.Zero, CultureInfo.CurrentCulture)))
            {
                return dataSource.Rows.OfType<DataRow>().ToList().Skip(filterValue).CopyToDataTable();
            }
            else if (StringManager.IsEqual(skipAndTake, VectorEnums.Filter.SkipAndTake.ToString()) &&
                     StringManager.IsNotEqual(Convert.ToString(filterValue, CultureInfo.CurrentCulture), Convert.ToString(VectorConstants.Zero, CultureInfo.CurrentCulture)) &&
                     StringManager.IsNotEqual(Convert.ToString(skipTakeValue, CultureInfo.CurrentCulture), Convert.ToString(VectorConstants.Zero, CultureInfo.CurrentCulture)))
            {
                return dataSource.Rows.OfType<DataRow>().ToList().Skip(filterValue).Take(skipTakeValue).CopyToDataTable();
            }
            else
            {
                return dataSource;
            }
        }

        /// <summary>
        /// Get Rows From Table By Row Range
        /// </summary>
        /// <param name="data">Data source</param>
        /// <param name="from">from value</param>
        /// <param name="to">to value</param>
        /// <param name="orderbyColumName">order by Colum Name</param>
        /// <param name="orderByColumnType">order By Column Type</param>
        /// <param name="orderBy">order By</param>
        /// <returns>Data Table</returns>
        public static DataTable GetRowsFromTableByRowRange(
                  DataTable data,
                  int from,
                  int to,
                  string orderbyColumName = null,
                  Type orderByColumnType = null,
                  string orderBy = "Ascending")
        {
            /*  1-10 , 11-20 ,  20 - max */
            DataTable orderedData = null;

            if (string.IsNullOrEmpty(orderbyColumName) && orderByColumnType != null)
            {
                orderedData = OrderDataTable(data, orderbyColumName, orderByColumnType, orderBy);
            }
            else
            {
                orderedData = data;
            }

            if (!IsNullOrEmptyDataTable(orderedData))
            {
                if (orderedData.Rows.Count >= to && orderedData.Rows.Count > from && ////To check if the Data table contains columns till to value
                    StringManager.IsNotEqual(Convert.ToString(to, CultureInfo.CurrentCulture), Convert.ToString(VectorConstants.Zero, CultureInfo.CurrentCulture)))
                {
                    return orderedData.Rows.OfType<DataRow>().Skip(from).Take(to).CopyToDataTable();
                }
                else
                {
                    return orderedData;
                }
            }
            else
            {
                return orderedData;
            }
        }

        /// <summary>
        ///  Order By Column ASC/DESC.
        /// </summary>
        /// <param name="dataSource">data Source</param>
        /// <param name="orderByColumn">order By Column</param>
        /// <param name="orderByType">order By Type</param>
        /// <param name="orderBy">order By</param>
        /// <returns>Data Table</returns>
        public static DataTable OrderDataTable(DataTable dataSource, string orderByColumn, Type orderByType, string orderBy = "Ascending")
        {
            if (dataSource.Rows.Count == 0)
                return dataSource;

            if (StringManager.IsEqual(orderBy, SortDirection.Ascending.ToString()))
            {
                if (StringManager.IsEqual(orderByType.ToString(), typeof(string).ToString()))
                {
                    return dataSource.Rows.OfType<DataRow>().OrderBy(x => x.Field<string>(orderByColumn)).CopyToDataTable();
                }
                else if (StringManager.IsEqual(orderByType.ToString(), typeof(int).ToString()))
                {
                    return dataSource.Rows.OfType<DataRow>().OrderBy(x => x.Field<int>(orderByColumn)).CopyToDataTable();
                }
                else if (StringManager.IsEqual(orderByType.ToString(), typeof(Byte).ToString()))
                {
                    return dataSource.Rows.OfType<DataRow>().OrderBy(x => x.Field<Byte>(orderByColumn)).CopyToDataTable();
                }
                else if (StringManager.IsEqual(orderByType.ToString(), typeof(decimal).ToString()))
                {
                    return dataSource.Rows.OfType<DataRow>().OrderBy(x => x.Field<decimal>(orderByColumn)).CopyToDataTable();
                }
                else
                {
                    return dataSource;
                }
            }
            else
            {
                if (StringManager.IsEqual(orderByType.ToString(), typeof(string).ToString()))
                {
                    return dataSource.Rows.OfType<DataRow>().OrderByDescending(x => x.Field<string>(orderByColumn)).CopyToDataTable();
                }
                else if (StringManager.IsEqual(orderByType.ToString(), typeof(int).ToString()))
                {
                    return dataSource.Rows.OfType<DataRow>().OrderByDescending(x => x.Field<int>(orderByColumn)).CopyToDataTable();
                }
                else if (StringManager.IsEqual(orderByType.ToString(), typeof(decimal).ToString()))
                {
                    return dataSource.Rows.OfType<DataRow>().OrderByDescending(x => x.Field<decimal>(orderByColumn)).CopyToDataTable();
                }
                else
                {
                    return dataSource;
                }
            }
        }

        public static DataTable OrderDataTable(DataTable dataSource, string orderByColumn, string orderBy2Column, Type orderByType, string orderBy = "Ascending")
        {
            if (dataSource.Rows.Count == 0)
                return dataSource;

            if (StringManager.IsEqual(orderBy, SortDirection.Ascending.ToString()))
            {
                if (StringManager.IsEqual(orderByType.ToString(), typeof(string).ToString()))
                {
                    return dataSource.Rows.OfType<DataRow>().OrderBy(x => x.Field<string>(orderByColumn)).OrderBy(x => x.Field<string>(orderBy2Column)).CopyToDataTable();
                }
                else if (StringManager.IsEqual(orderByType.ToString(), typeof(int).ToString()))
                {
                    return dataSource.Rows.OfType<DataRow>().OrderBy(x => x.Field<int>(orderByColumn)).OrderBy(x => x.Field<int>(orderBy2Column)).CopyToDataTable();
                }
                else if (StringManager.IsEqual(orderByType.ToString(), typeof(Byte).ToString()))
                {
                    return dataSource.Rows.OfType<DataRow>().OrderBy(x => x.Field<Byte>(orderByColumn)).OrderBy(x => x.Field<Byte>(orderBy2Column)).CopyToDataTable();
                }
                else if (StringManager.IsEqual(orderByType.ToString(), typeof(decimal).ToString()))
                {
                    return dataSource.Rows.OfType<DataRow>().OrderBy(x => x.Field<decimal>(orderByColumn)).OrderBy(x => x.Field<decimal>(orderBy2Column)).CopyToDataTable();
                }
                else
                {
                    return dataSource;
                }
            }
            else
            {
                if (StringManager.IsEqual(orderByType.ToString(), typeof(string).ToString()))
                {
                    return dataSource.Rows.OfType<DataRow>().OrderByDescending(x => x.Field<string>(orderByColumn)).OrderBy(x => x.Field<string>(orderBy2Column)).CopyToDataTable();
                }
                else if (StringManager.IsEqual(orderByType.ToString(), typeof(int).ToString()))
                {
                    return dataSource.Rows.OfType<DataRow>().OrderByDescending(x => x.Field<int>(orderByColumn)).OrderBy(x => x.Field<int>(orderBy2Column)).CopyToDataTable();
                }
                else if (StringManager.IsEqual(orderByType.ToString(), typeof(decimal).ToString()))
                {
                    return dataSource.Rows.OfType<DataRow>().OrderByDescending(x => x.Field<decimal>(orderByColumn)).OrderBy(x => x.Field<decimal>(orderBy2Column)).CopyToDataTable();
                }
                else
                {
                    return dataSource;
                }
            }
        }

        /// <summary>
        /// Get the Average value based on the Column Name and FileColumn Name
        /// </summary>
        /// <param name="dataSource">data Source</param>
        /// <param name="columnName">column Name</param>
        /// <param name="filterColumnName">filter Column Name</param>
        /// <returns>double value</returns>
        public static double GetTotalOfColumnUsingFilterColumn(DataTable dataSource, string columnName, string filterColumnName)
        {
            ////For storing value of each column
            double value = VectorConstants.Zero;

            ////For each data row check if the data available in FilterRow
            foreach (DataRowView drv in dataSource.AsEnumerable().Where(r => r.Field<long?>(filterColumnName).HasValue).AsDataView())
            {
                ////add value
                value = value + Convert.ToDouble(drv[columnName].ToString(), CultureInfo.CurrentCulture);
            }

            ////return average
            return value;
        }

        /// <summary>
        /// Filter Data Table based on column name / column value
        /// </summary>
        /// <param name="dataSource">Data Source </param>
        /// <param name="filterColumnName">Table Column name</param>
        /// <param name="filterValue">Column Value</param>
        /// <returns></returns>
        public static DataTable FilterDataTable(DataTable dataSource, string filterColumnName,
                                            string filterValue)
        {
            if (!IsNullOrEmptyDataTable(dataSource))
            {
                if (dataSource.Columns[filterColumnName].DataType == typeof(string))
                {
                    var list = (from c in dataSource.AsEnumerable()
                                where c.Field<string>(filterColumnName) == null ? (string.Empty).Contains(filterValue) : c.Field<string>(filterColumnName).ToUpper().Trim().Contains(filterValue.ToUpper())
                                select c).ToList();

                    if (list.Count == 0)
                        return null;
                    else
                        return list.CopyToDataTable();
                }
                else if (dataSource.Columns[filterColumnName].DataType == typeof(decimal))
                {
                    //TO Do;
                }
            }
            return null;
        }

        /// <summary>
        /// Filter Data Table based on empty column name / column value
        /// </summary>
        /// <param name="dataSource">Data Source </param>
        /// <param name="filterColumnName">Table Column name</param>
        /// <returns></returns>
        public static DataTable FilterDataTableByEmptyColumn(DataTable dataSource, string filterColumnName)
        {
            if (!IsNullOrEmptyDataTable(dataSource))
            {
                if (dataSource.Columns[filterColumnName].DataType == typeof(string))
                {
                    var list = dataSource.Select(filterColumnName + " <> ''");

                    return list.CopyToDataTable();
                }
                else if (dataSource.Columns[filterColumnName].DataType == typeof(decimal))
                {
                    //TO Do;
                }
            }
            return null;
        }

        /// <summary>
        /// Filter Data Table based on column name / column value
        /// </summary>
        /// <param name="dataSource">Data Source </param>
        /// <param name="filterColumnName">Table Column name</param>
        /// <param name="filterValue">Column Value</param>
        /// <returns></returns>
        public static DataView FilterDataView(DataTable dataSource, string filterColumnName,
                                    string filterValue)
        {
            if (!IsNullOrEmptyDataTable(dataSource))
            {
                if (dataSource.Columns[filterColumnName].DataType != typeof(string))
                {
                    var list = (from c in dataSource.AsEnumerable()
                                where c.Field<string>(filterColumnName) == null ? (string.Empty).Contains(filterValue) : c.Field<string>(filterColumnName).ToUpper().Trim().Contains(filterValue.ToUpper())
                                select c).ToList();

                    if (list.Count == 0)
                        return null;
                    else
                        return list.CopyToDataTable().DefaultView;
                }
                else if (dataSource.Columns[filterColumnName].DataType != typeof(decimal))
                {
                    //TO Do;
                }
            }
            return null;
        }

        public static bool ContainColumn(DataTable table, string columnName)
        {
            DataColumnCollection columns = table.Columns;
            return columns.Contains(columnName);
        }

        /// <summary>
        ///  Filter source data based upon quick header search and return data
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="txtControl"></param>
        /// <param name="controlPlaceHolderText"></param>
        /// <param name="columnName"></param>
        /// <param name="columnDataType"></param>
        /// <returns></returns>
        public static DataTable GridViewQuickSearch(DataTable dataSource, TextBox txtControl, string controlPlaceHolderText, string columnName)
        {
            if (txtControl != null)
            {
                if (!DataManager.IsNullOrEmptyDataTable(dataSource))
                {
                    if (!string.IsNullOrEmpty(txtControl.Text) && StringManager.IsNotEqual(txtControl.Text, controlPlaceHolderText))
                    {
                        return FilterDataTable(dataSource, columnName, txtControl.Text.Trim());
                    }
                }
            }
            return null;
        }

        #endregion

        #region  Rows : swap , Remove , find

        /// <summary>
        /// Find Row By Index
        /// </summary>
        /// <param name="dataSource">data Source</param>
        /// <param name="columnName">column Name</param>
        /// <param name="filter">filter value</param>
        /// <returns>row index</returns>
        public static int FindRowByIndex(DataTable dataSource, string columnName, string filter)
        {
            int index = VectorConstants.MinusOne;

            if (!IsNullOrEmptyDataTable(dataSource))
            {
                DataRow[] dataRow = null;
                dataRow = dataSource.Select(columnName + " like '" + filter + "%'");
                if (dataRow.Length > VectorConstants.Zero)
                {
                    index = dataSource.Rows.IndexOf(dataRow[VectorConstants.Zero]);
                }
            }

            return index;
        }

        /// <summary>
        /// Find Row By Filter
        /// </summary>
        /// <param name="dataSource">data Source</param>
        /// <param name="columnName">column Name</param>
        /// <param name="filter">filter value</param>
        /// <returns>Data Row</returns>
        public static DataRow FindRowByFilter(DataTable dataSource, string columnName, string filter)
        {
            DataRow[] dataRow = null;
            if (!IsNullOrEmptyDataTable(dataSource))
            {
                dataRow = dataSource.Select(columnName + " like '" + filter + "%'");
                if (dataRow.Length > VectorConstants.Zero)
                {
                    return dataRow[VectorConstants.Zero];
                }
            }

            return null;
        }

        /// <summary>
        /// Remove Row By Index
        /// </summary>
        /// <param name="dataSource">data Source</param>
        /// <param name="rowIndex">row Index</param>
        /// <returns>Data Table</returns>
        public static DataTable RemoveRowByIndex(DataTable dataSource, int rowIndex)
        {
            if (!IsNullOrEmptyDataTable(dataSource))
            {
                dataSource.Rows[rowIndex].Delete();
            }

            return dataSource;
        }

        /// <summary>
        /// Remove Row by data row
        /// </summary>
        /// <param name="dataSource">data Source</param>
        /// <param name="dataRow">data Row</param>
        /// <returns>data table</returns>
        public static DataTable RemoveRow(DataTable dataSource, DataRow dataRow)
        {
            if (!IsNullOrEmptyDataTable(dataSource))
            {
                dataSource.Rows.Remove(dataRow);
            }

            return dataSource;
        }

        /// <summary>
        /// Insert Row By Index
        /// </summary>
        /// <param name="dataSource">data Source</param>
        /// <param name="dataRow">data Row</param>
        /// <param name="rowIndex">row Index</param>
        /// <returns>data table</returns>
        public static DataTable InsertRowByIndex(DataTable dataSource, DataRow dataRow, int rowIndex)
        {
            if (!IsNullOrEmptyDataTable(dataSource))
            {
                dataSource.Rows.InsertAt(dataRow, rowIndex);
            }

            return dataSource;
        }

        /// <summary>
        /// Swap Row
        /// </summary>
        /// <param name="dataSource">data Source</param>
        /// <param name="columnName">column Name</param>
        /// <param name="filter">filter value</param>
        /// <returns>data table</returns>
        public static DataTable SwapRow(DataTable dataSource, string columnName, string filter)
        {
            /*Find Row , Remove Row and Insert row */
            DataRow filteredRow = FindRowByFilter(dataSource, columnName, filter);
            if (filteredRow != null)
            {
                ////DataRow copyOfFilteredRow = drFilteredRow;
                dataSource = RemoveRow(dataSource, filteredRow);
                ////dataTable = InsertRowByIndex(dataTable, copyOfFilteredRow, addRowAt);
            }

            return dataSource;
        }

        #endregion

        #endregion

        /// <summary>
        /// Get Month ,Year and Month name-Year using FromDate and ToDate
        /// </summary>
        /// <param name="fromDate">from Date</param>
        /// <param name="toDate">to Date</param>
        /// <returns>data table</returns>
        public static DataTable GetTableMonthAndYear(DateTime fromDate, DateTime toDate)
        {
            DataTable dataSource = new DataTable();
            dataSource.Locale = CultureInfo.InvariantCulture;
            dataSource.Columns.Add(VectorEnums.Column.Month.ToString(), typeof(long));
            dataSource.Columns.Add(VectorEnums.Column.Year.ToString(), typeof(long));
            dataSource.Columns.Add(VectorEnums.Column.MonthYear.ToString(), typeof(string));

            for (var date = fromDate.Date; date <= toDate; date = date.AddMonths(VectorConstants.One))
            {
                DataRow rowDate = dataSource.NewRow();
                rowDate[VectorEnums.Column.Month.ToString()] = date.Month;
                rowDate[VectorEnums.Column.Year.ToString()] = date.Year;
                rowDate[VectorEnums.Column.MonthYear.ToString()] = DateManager.GetMonthName(date.Month).Substring(VectorConstants.Zero, VectorConstants.Three) +
                                                                "," + Convert.ToString(date.Year, CultureInfo.CurrentCulture).Substring(VectorConstants.Two);
                dataSource.Rows.Add(rowDate);
            }

            return dataSource;
        }

        /// <summary>
        /// Get Column Index based on Data table Column Name
        /// </summary>
        /// <param name="dataSource">data Source</param>
        /// <param name="fieldName">field Name</param>
        /// <returns>column index</returns>
        public static int GetDataTableColumnIndex(DataTable dataSource, string fieldName, bool ignoreSpace = true)
        {
            int columnCount = dataSource.Columns.Count;
            for (int i = VectorConstants.Zero; i < columnCount; i++)
            {
                string columnName = dataSource.Columns[i].ColumnName.ToString();

                if (ignoreSpace)
                {
                    ////Assuming accessing happens at data level, e.g with data field's name
                    if (columnName.ToUpper(CultureInfo.InvariantCulture).Replace(" ", string.Empty) == fieldName.ToUpper(CultureInfo.InvariantCulture).Replace(" ", string.Empty))
                    {
                        return i;
                    }
                }
                else
                {
                    if (columnName.ToUpper(CultureInfo.InvariantCulture) == fieldName.ToUpper(CultureInfo.InvariantCulture))
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        /// <summary>
        /// Replace from value with to value in data column from value can have multiple values separated by | e.g. String.empty | 0 
        /// </summary>
        /// <param name="data">data source</param>
        /// <param name="columnName">column name</param>
        /// <param name="fromValue">from value</param>
        /// <param name="toValue">to value</param>
        /// <returns>data table</returns>
        private static DataTable ReplaceValuesinDataColumn(DataTable data, string columnName, string fromValue, string toValue)
        {
            foreach (DataColumn dataColumn in data.Columns)
            {
                if (dataColumn.ColumnName.Equals(columnName))
                {
                    foreach (DataRow dr in data.Rows)
                    {
                        if (!fromValue.Contains(VectorConstants.Pipe))
                        {
                            if (dr[dataColumn.ColumnName].ToString().Replace("&nbsp;", string.Empty).Equals(fromValue))
                            {
                                dr[dataColumn.ColumnName] = toValue;
                            }
                        }
                        else
                        {
                            string[] splitFromValue = fromValue.Split(VectorConstants.Pipe);
                            foreach (string value in splitFromValue)
                            {
                                if (dr[dataColumn.ColumnName].ToString().Replace("&nbsp;", string.Empty).Equals(value))
                                {
                                    dr[dataColumn.ColumnName] = toValue;
                                }
                            }
                        }
                    }
                }
            }

            return data;
        }

        /// <summary>
        /// Converting XML string To DataSet
        /// </summary>
        /// <param name="xmlvalue">Xml string</param>
        /// <returns>Data set</returns>
        public static DataSet ConvertXMLToDataSet(string xmlvalue)
        {
            DataSet xmlDataSet = new DataSet();
            StringReader stringReader = new StringReader(xmlvalue);
            xmlDataSet.ReadXml(stringReader);
            return xmlDataSet;
        }

        #region Convert List to data set

        public static DataSet ListToDataSet<T>(this IList<T> list)
        {
            Type elementType = typeof(T);
            DataSet ds = new DataSet();
            DataTable t = new DataTable();
            ds.Tables.Add(t);

            //add a column to table for each public property on T
            foreach (var propInfo in elementType.GetProperties())
            {
                Type ColType = Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType;

                t.Columns.Add(propInfo.Name, ColType);
            }

            //go through each property on T and add each value to the table
            foreach (T item in list)
            {
                DataRow row = t.NewRow();

                foreach (var propInfo in elementType.GetProperties())
                {
                    row[propInfo.Name] = propInfo.GetValue(item, null) ?? DBNull.Value;
                }

                t.Rows.Add(row);
            }

            return ds;
        }

        /// <summary>
        /// Find Row By Filter
        /// </summary>
        /// <param name="dataSource">data Source</param>
        /// <param name="columnName">column Name</param>
        /// <param name="filter">filter value</param>
        /// <returns>Data Row</returns>
        public static string GetRowByColumnFilter(DataTable dataSource, string columnName, string filter, string resultcolumnName)
        {
            int rowindex = FindRowIndexInDataTable(dataSource, columnName, filter);
            if (rowindex != -1)
            {
                return dataSource.Rows[rowindex][resultcolumnName].ToString();
            }
            return null;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="StringDatatable"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static string GetDatatableToStringwithCotes(DataTable StringDatatable, string columnName)
        {
            string value = string.Empty;
            if (!IsNullOrEmptyDataTable(StringDatatable) && IsDataTableHasColumn(StringDatatable, columnName))
            {
                foreach (DataRow item in StringDatatable.Rows)
                {
                    value += "'" + item[columnName] + "',";
                }
                return value.TrimEnd(',');
            }
            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <param name="columnaName"></param>
        /// <param name="columnValue"></param>
        /// <param name="outPutcolumnName"></param>
        /// <returns></returns>
        public static string GetColumnValuefromTable(DataTable table, string columnaName, string columnValue, string outPutcolumnName)
        {
            var Value = table.AsEnumerable()
                             .Where(p => p.Field<string>(columnaName) == columnValue)
                             .Select(p => p.Field<string>(outPutcolumnName))
                             .FirstOrDefault();

            return Convert.ToString(Value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <param name="columnaName"></param>
        /// <param name="columnValue"></param>
        /// <param name="outPutcolumnName"></param>
        /// <returns></returns>
        public static string GetColumnValuefromTableForInt(DataTable table, string columnaName, string columnValue, string outPutcolumnName)
        {
            var Value = (table.AsEnumerable()
                             .Where(p => p.Field<Int64>(columnaName) == Convert.ToInt64(columnValue))
                             .Select(p => p.Field<string>(outPutcolumnName).ToString())).FirstOrDefault().ToString();

            return Convert.ToString(Value);
        }

        public static DataTable MergeDataTables(DataTable firstTable, DataTable secondTable, string primaryKeyColumnName)
        {
            DataTable dtMerged = new DataTable();
            if (ValidateColumnForMerge(firstTable, secondTable, primaryKeyColumnName))
            {
                firstTable.PrimaryKey = new DataColumn[] { firstTable.Columns[primaryKeyColumnName] };
                secondTable.PrimaryKey = new DataColumn[] { secondTable.Columns[primaryKeyColumnName] };
                dtMerged = firstTable.Copy();
                dtMerged.Merge(secondTable, false, MissingSchemaAction.Add);
                dtMerged.AcceptChanges();

                return dtMerged;
            }
            return firstTable;
        }

        private static bool ValidateColumnForMerge(DataTable firstTable, DataTable secondTable, string columnName)
        {
            if (StringManager.IsEqual(GetDataTableDataType(firstTable, columnName), GetDataTableDataType(secondTable, columnName)))
                return true;
            else
                return false;
        }

        public static string GetDataTableDataType(DataTable firstTable, string columnName)
        {
            if (IsDataTableHasColumn(firstTable, columnName))
            {
                return firstTable.Columns[columnName].DataType.ToString();
            }

            return null;
        }

        public static DataTable AppendDataTables(DataSet sourceTables, List<DataTableMetaData> tableSeqInfo, bool addEmptyForeachTable = false)
        {
            DataTable target = new DataTable();
            int maxTableCount = tableSeqInfo.Select(x => x.SequenceNo).Max();
            if (sourceTables.Tables.Count >= maxTableCount)
            {
                int maxColumnCount = sourceTables.Tables.Cast<DataTable>().Select(tbl => tbl.Columns.Count).Max();
                for (int indxCol = 0; indxCol < maxColumnCount; indxCol++)
                {
                    target.Columns.Add(indxCol.ToString());
                }
                foreach (DataTableMetaData md in tableSeqInfo.OrderBy(x => x.SequenceNo).ToList())
                {
                    if (md.SNo < maxTableCount)
                    {
                        DataTable currentDT = sourceTables.Tables[md.SNo];
                        if (!md.SkipColumns)
                        {
                            DataRow newColRow = target.NewRow();
                            for (int indxCol = 0; indxCol < currentDT.Columns.Count; indxCol++)
                            {
                                newColRow[indxCol] = currentDT.Columns[indxCol].ColumnName;
                            }
                            target.Rows.Add(newColRow);
                        }

                        foreach (DataRow eachRow in currentDT.Rows)
                        {
                            DataRow newRow = target.NewRow();
                            for (int indxCol = 0; indxCol < currentDT.Columns.Count; indxCol++)
                            {
                                newRow[indxCol] = Convert.ToString(eachRow[indxCol]);
                            }
                            target.Rows.Add(newRow);
                        }
                        if (addEmptyForeachTable)
                            target.Rows.Add(target.NewRow());
                    }
                }
            }
            return target;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <param name="columnaName"></param>
        /// <param name="columnValue"></param>
        /// <param name="outPutcolumnName"></param>
        /// <returns></returns>
        public static string GetOffileColumnValuefromTable(IEnumerable<DataRow> table, string columnaName, string columnValue, string outPutcolumnName)
        {
            bool contains = table.Any(p => p.Field<string>(columnaName) == columnValue);
            if (contains)
            {
                var Value = table
                                 .Where(p => p.Field<string>(columnaName) == columnValue)
                                 .Select(p => p.Field<string>(outPutcolumnName))
                                 .FirstOrDefault();

                return Convert.ToString(Value);
            }
            return string.Empty;

        }
    }
    public class DataTableMetaData
    {
        public int SNo { get; set; }
        public int SequenceNo { get; set; }
        public bool SkipColumns { get; set; }
    }
}

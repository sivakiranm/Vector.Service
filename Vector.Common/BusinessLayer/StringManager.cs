using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
//using ProFramework.ProEnums;
using System.Collections.Generic;
using System.Collections.Specialized;
using static Vector.Common.BusinessLayer.VectorEnums;

namespace Vector.Common.BusinessLayer
{
    public static class StringManager
    {
        #region Constants



        #endregion

        #region Enum
        public static string dtFilterNegotiator = "Role like '%Negotiator%'";
        public static string dtFilterAccountManager = "Role like'%Account Manager%'";
        public static string Role = "Role";
        public static string ColumnNameSalesPerson = "SALEPERSONNAME";
        public static string ColumnNameResourseName = "ResourceName";
        public static string RoleNameSalesPerson = "Sales Person";
        public static string RoleNameAccountManager = "Account Manager";
        public static string dtFilterSalesPerson = "Role like'%Sales Person%'";
        #endregion

        #region String

        #region Compare

        /// <summary>
        /// To compare two strings and return true or false
        /// </summary>
        /// <param name="text"></param>
        /// <param name="texttoCompare"></param>
        /// <returns></returns>
        public static bool IsEqual(string str1, string str2)
        {
            if (string.Compare(str1, str2, StringComparison.OrdinalIgnoreCase) == VectorConstants.Zero)
                return true;
            else
                return false;
        }

        /// <summary>
        /// To compare two strings and return true or false
        /// </summary>
        /// <param name="text"></param>
        /// <param name="texttoCompare"></param>
        /// <returns></returns>
        public static bool IsNotEqual(string str1, string str2)
        {
            if (string.Compare(str1, str2, StringComparison.OrdinalIgnoreCase) != VectorConstants.Zero)
                return true;
            else
                return false;
        }


        #endregion

        #region String Formating


        public static string StringToPascelCase(string strTitle)
        {
            return Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(strTitle.ToLower(CultureInfo.CurrentCulture));
        }

        public static string ReplaceDateTimeFormat(string format, string date = null)
        {
            string result = string.Empty;
            DateTime dtdate;
            if (IsEqual(date, string.Empty))
                dtdate = DateTime.Now;
            else
                dtdate = Convert.ToDateTime(date, CultureInfo.CurrentCulture);

            //01292013121212 MM DD YY HH MM SS  
            if (format.Contains(Format.MMDDYYYYHHMMSS.ToString()))
            {
                //result = format.Replace(Format.MMDDYYYYHHMMSS.ToString(),
                //                        dtdate.ToString(Format.MMddyyyyHHmmssffff.ToString(), CultureInfo.CurrentCulture));

                result = format.Replace(Format.MMDDYYYY.ToString(),
                                            dtdate.ToString(VectorConstants.MMddyyyy.ToString(), CultureInfo.CurrentCulture));
                result = result.Replace(Format.HHMMSS.ToString(),
                                        dtdate.ToString(VectorConstants.HHmmss.ToString(), CultureInfo.CurrentCulture));
            }
            else if (format.Contains(Format.YYYYMMDD_HHMMSS.ToString()))
            {
                result = format.Replace(Format.YYYYMMDD.ToString(),
                                        dtdate.ToString(VectorConstants.yyyyMMdd.ToString(), CultureInfo.CurrentCulture));
                result = result.Replace(Format.HHMMSS.ToString(),
                                        dtdate.ToString(VectorConstants.HHmmss.ToString(), CultureInfo.CurrentCulture));
            }
            else if (format.Contains(Format.YYYYMMDDHHMMSS.ToString()))
            {
                result = format.Replace(Format.YYYYMMDD.ToString(),
                                        dtdate.ToString(VectorConstants.yyyyMMdd.ToString(), CultureInfo.CurrentCulture));
                result = result.Replace(Format.HHMMSS.ToString(),
                                        dtdate.ToString(VectorConstants.HHmmss.ToString(), CultureInfo.CurrentCulture));
            }
            else if (format.Contains(VectorConstants.YYMMDD.ToUpper()) && !format.Contains(VectorConstants.YYYYMMDD.ToUpper()))
                result = format.Replace(VectorConstants.YYMMDD.ToUpper(),
                                        dtdate.ToString(VectorConstants.YYMMDD, CultureInfo.CurrentCulture));


            else if (format.Contains(Format.MMDDYY.ToString()) && !format.Contains(Format.MMDDYYYY.ToString()))
                result = format.Replace(Format.MMDDYY.ToString(),
                                        dtdate.ToString(VectorConstants.MMddyy, CultureInfo.CurrentCulture));

            else if (format.Contains(VectorConstants.DDMMYY.ToUpper()) && !format.Contains(Format.DDMMYYYY.ToString()))
                result = format.Replace(Format.ddMMyy.ToString().ToUpper(),
                                        dtdate.ToString(Format.ddMMyy.ToString(), CultureInfo.CurrentCulture));

            else if (format.Contains(VectorConstants.YYDDMM.ToUpper()) && !format.Contains(Format.YYYYDDMM.ToString()))
                result = format.Replace(VectorConstants.YYDDMM.ToUpper(),
                                        dtdate.ToString(VectorConstants.YYDDMM, CultureInfo.CurrentCulture));

            else if (format.Contains(VectorConstants.MMDotDDdotYY.ToUpper()) && !format.Contains(VectorConstants.MMdotDDdotYYYY.ToUpper()))
                result = format.Replace(VectorConstants.MMDotDDdotYY.ToUpper(),
                                        dtdate.ToString(VectorConstants.MMDotDDdotYY, CultureInfo.CurrentCulture));
            else if (format.Contains(VectorConstants.DDdotMMdotYY.ToUpper()) && !format.Contains(VectorConstants.DDdotMMdotYYYY.ToUpper()))
                result = format.Replace(VectorConstants.DDdotMMdotYY.ToUpper(),
                                        dtdate.ToString(VectorConstants.DDdotMMdotYY, CultureInfo.CurrentCulture));
            else if (format.Contains(VectorConstants.YYdotDDdotMM.ToUpper()) && !format.Contains(VectorConstants.YYYYdotDDdotMM.ToUpper()))
                result = format.Replace(VectorConstants.YYdotDDdotMM.ToUpper(),
                                        dtdate.ToString(VectorConstants.YYdotDDdotMM, CultureInfo.CurrentCulture));
            else if (format.Contains(VectorConstants.YYdotMMdotDD.ToUpper()) && !format.Contains(VectorConstants.YYYYdotMMdotDD.ToUpper()))
                result = format.Replace(VectorConstants.YYdotMMdotDD.ToUpper(),
                                        dtdate.ToString(VectorConstants.YYdotMMdotDD, CultureInfo.CurrentCulture));

            else if (format.Contains(VectorConstants.MMdotDDdotYYYY.ToUpper()))
                result = format.Replace(VectorConstants.MMdotDDdotYYYY.ToUpper(),
                                        dtdate.ToString(VectorConstants.MMdotDDdotYYYY, CultureInfo.CurrentCulture));
            else if (format.Contains(VectorConstants.DDdotMMdotYYYY.ToUpper()))
                result = format.Replace(VectorConstants.DDdotMMdotYYYY.ToUpper(),
                                        dtdate.ToString(VectorConstants.DDdotMMdotYYYY, CultureInfo.CurrentCulture));
            else if (format.Contains(VectorConstants.YYYYdotDDdotMM.ToUpper()))
                result = format.Replace(VectorConstants.YYYYdotDDdotMM.ToUpper(),
                                        dtdate.ToString(VectorConstants.YYYYdotDDdotMM, CultureInfo.CurrentCulture));
            else if (format.Contains(VectorConstants.YYYYdotMMdotDD.ToUpper()))
                result = format.Replace(VectorConstants.YYYYdotMMdotDD.ToUpper(),
                                        dtdate.ToString(VectorConstants.YYYYdotMMdotDD, CultureInfo.CurrentCulture));



            else if (format.Contains(VectorConstants.MMdotYYYYdotDD.ToUpper()))
                result = format.Replace(VectorConstants.MMdotYYYYdotDD.ToUpper(),
                                        dtdate.ToString(VectorConstants.MMdotYYYYdotDD, CultureInfo.CurrentCulture));
            else if (format.Contains(VectorConstants.DDdotYYYYdotMM.ToUpper()))
                result = format.Replace(VectorConstants.DDdotYYYYdotMM.ToUpper(),
                                        dtdate.ToString(VectorConstants.DDdotYYYYdotMM, CultureInfo.CurrentCulture));


            else if (format.Contains(Format.MMDDYYYY_HHMMSS.ToString()))
            {
                result = format.Replace(Format.MMDDYYYY.ToString(),
                                       dtdate.ToString(VectorConstants.MMddyyyy.ToString(), CultureInfo.CurrentCulture));
                result = result.Replace(Format.HHMMSS.ToString(),
                                        dtdate.ToString(VectorConstants.HHmmss.ToString(), CultureInfo.CurrentCulture));
            }
            //01292013 MMDDYYYY
            else if (format.Contains(Format.MMDDYYYY.ToString()))
                result = format.Replace(Format.MMDDYYYY.ToString(),
                                        dtdate.ToString(Format.MMddyyyy.ToString(), CultureInfo.CurrentCulture));
            else if (format.Contains(Format.DDMMYYYY.ToString()))
                result = format.Replace(Format.DDMMYYYY.ToString(),
                                        dtdate.ToString(VectorConstants.ddMMyyyy, CultureInfo.CurrentCulture));
            else if (format.Contains(Format.YYYYDDMM.ToString()))
                result = format.Replace(Format.YYYYDDMM.ToString(),
                                        dtdate.ToString(VectorConstants.yyyyddMM, CultureInfo.CurrentCulture));
            else if (format.Contains(Format.yyyyMMdd.ToString().ToUpper()))
                result = format.Replace(Format.yyyyMMdd.ToString().ToUpper(),
                                        dtdate.ToString(Format.yyyyMMdd.ToString(), CultureInfo.CurrentCulture));
            else if (format.Contains(Format.MMYYYY.ToString()))
                result = format.Replace(Format.MMYYYY.ToString(),
                                        dtdate.ToString(VectorConstants.MMyyyy.ToString(), CultureInfo.CurrentCulture));
            else if (format.Contains(Format.DD_MM_YY.ToString()))
            {
                string dateFormat = dtdate.ToString(VectorConstants.dd.ToString(), CultureInfo.CurrentCulture) + VectorConstants.CharUnderscore + dtdate.ToString(VectorConstants.MM.ToString(),
                    CultureInfo.CurrentCulture) + VectorConstants.CharUnderscore + dtdate.ToString(VectorConstants.yy.ToString(), CultureInfo.CurrentCulture);
                result = format.Replace(Format.DD_MM_YY.ToString(), dateFormat);
            }

            else
                result = format;

            return result;
        }

        /// <summary>
        /// Clean and Compress String of Control Characters
        /// </summary>
        /// <param name="source">String to be cleaned and compressed</param>
        /// <returns>String without control characters, spaces squeezed to one</returns>
        public static string StringCompress(object source)
        {
            return (Regex.Replace(Regex.Replace((source ?? "").ToString(), "[^\x20-\xff]", " "), "\\s+", " ").Trim());
        }

        /// <summary>
        /// Gets the string value.
        /// </summary>
        /// <param name="value">The object.</param>
        /// <returns></returns>
        public static string GetStringValue(object value)
        {
            return (GetStr(value, ""));
        }

        /// <summary>
        /// Replace Special Characters with Hexa Decimal values
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ReplaceSpecialCharactersWithHexa(string value)
        {
            value = value.Replace("&", "%26amp;");
            value = value.Replace("\t", string.Empty);
            value = value.Replace("\n", string.Empty);
            return value;
        }



        /// <summary>
        /// Gets the double value from a string value.
        /// </summary>
        /// <param name="val">The string value.</param>
        /// <returns>Double value or zero for error.</returns>
        public static double GetDbl(object val)
        {
            return (GetDbl(val, 0.0));
        }

        /// <summary>
        /// Gets the double value from a string value.
        /// </summary>
        /// <param name="val">The string value.</param>
        /// <param name="default">The default value.</param>
        /// <returns>Double value or default for error.</returns>
        public static double GetDbl(object val, double defaultVal)
        {
            //try
            ////{
            double dblval = defaultVal;
            return (((val == null) || (val == DBNull.Value) ||
                string.IsNullOrEmpty(val.ToString().Trim())) ?
                defaultVal : (double.TryParse(val.ToString().Trim(), out dblval) ? dblval : defaultVal));
            //}
            //catch
            //{
            //    return (defaultVal);
            //}
        }

        /// <summary>
        /// Gets the double value from a string value.
        /// </summary>
        /// <param name="val">The string value.</param>
        /// <param name="default">The default value.</param>
        /// <returns>Double value or default for non-positive or error.</returns>
        public static double GetDbl2(object val, double defaultVal)
        {
            //try
            //{
            double dblval = defaultVal;
            return (((val == null) || (val == DBNull.Value) || string.IsNullOrEmpty(val.ToString().Trim())) ? defaultVal : (double.TryParse(val.ToString().Trim(), out dblval) ? ((dblval <= 0.0) ? defaultVal : dblval) : defaultVal));
            //}
            //catch
            //{
            //    return (defaultVal);
            //}
        }

        /// <summary>
        /// Gets the integer value from a string value.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static Int64 GetInt64(object val)
        {
            return (GetNumberValue(val, 0));
        }

        /// <summary>
        /// Gets the integer value from a string value.
        /// </summary>
        /// <param name="val"></param>
        /// <param name="defaultVal"></param>
        /// <returns></returns>
        public static Int64 GetInt64(object val, Int64 defaultVal)
        {
            //try
            //{
            Int64 intval = defaultVal;
            return (((val == null) || (val == DBNull.Value) || string.IsNullOrEmpty(val.ToString().Trim())) ? defaultVal : (Int64.TryParse(val.ToString().Trim(), out intval) ? intval : defaultVal));
            //}
            //catch
            //{
            //    return (defaultVal);
            //}
        }

        /// <summary>
        /// Gets the integer value from a string value.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <returns>Integer value or zero for error.</returns>
        public static int GetNumberValue(object value)
        {
            return (GetNumberValue(value, 0));
        }

        /// <summary>
        /// Gets the integer value from a string value.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <param name="default">The default value.</param>
        /// <returns>Integer value or zero for error.</returns>
        public static int GetNumberValue(object value, int defaultValue)
        {
            //try
            //{
            int intval = defaultValue;
            return (((value == null) || (value == DBNull.Value) ||
                string.IsNullOrEmpty(value.ToString().Trim())) ?
                defaultValue : (int.TryParse(value.ToString().Trim(), out intval) ? intval : defaultValue));
            //}
            //catch
            //{
            //    return (defaultVal);
            //}
        }

        /// <summary>
        /// Gets the string value.
        /// </summary>
        /// <param name="val">The value.</param>
        /// <param name="default">The default value.</param>
        /// <returns>value or default value</returns>
        public static string GetStr(object val, string defaultVal)
        {
            //try
            //{
            return ((((val == null) || (val == DBNull.Value)
                                    || string.IsNullOrEmpty(val.ToString().Trim())) ?
                                    defaultVal : val.ToString()).Trim());
            //}
            //catch
            //{
            //}
            //return (defaultVal.Trim());
        }

        /// <summary>
        /// Gets the string value.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string GetStr(object val)
        {
            return GetStr(val, "");
        }

        /// <summary>
        /// Proper Case String
        /// </summary>
        /// <param name="val">String to be Proper Cased</param>
        /// <returns>String Proper Cased</returns>
        public static string ProperCase(object val)
        {
            //try
            //{
            return (((val == null) || (val == DBNull.Value) ||
                string.IsNullOrEmpty(val.ToString().Trim())) ? "" :
                Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Regex.Replace(val.ToString().Trim(),
                "[^\x20-\xff]", " ").Trim().ToUpper(CultureInfo.InvariantCulture)));
            //}
            //catch
            //{
            //    return (val.ToString().Trim());
            //}
        }

        /// <summary>
        /// Proper Case String or return default value
        /// </summary>
        /// <param name="val">String to be Proper Cased</param>
        /// <param name="default">The default value.</param>
        /// <returns>String Proper Cased</returns>
        public static string PropCase(object val, string defaultVal)
        {
            return (ProperCase(GetStr(val, defaultVal)));
        }

        /// <summary>
        /// Converts string to TitleCase
        /// Optional:containsCap = true discards converting text to Lower case
        /// </summary>
        /// <param name="value"></param>
        /// <param name="containsCap"></param>
        /// <returns></returns>
        public static string ConvertToTitleCase(string value, bool containsCap = false)
        {
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            return ti.ToTitleCase(containsCap ? value : value.ToLower());
        }

        #region Split

        public static string SplitAndGetValue(string textToSplit, int index, char value)
        {
            string[] result = null;

            if (value != null)
                result = textToSplit.Split(value);

            if (result.Count() >= index)
                return result[index];
            else
                return string.Empty;
        }

        /// <summary>
        /// Split the String based on Character and get all the Values in String Array
        /// </summary>
        /// <param name="textToSpit"></param>
        /// <param name="splitValue"></param>
        /// <returns></returns>
        public static string[] SplitAndGetValues(string textToSpit, char splitValue)
        {
            string[] splitValues = null;

            if (!string.IsNullOrEmpty(textToSpit))
                splitValues = textToSpit.Split(splitValue);

            return splitValues;
        }

        public static bool IsValueExist(string valueToCheck, string[] valueCollection)
        {
            if (valueCollection.Length > 0)
            {
                foreach (string item in valueCollection)
                {
                    if (IsEqual(valueToCheck, item))
                        return true;
                }
            }

            return false;
        }

        #endregion



        #endregion

        #region Remove Special Characters

        public static string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
        }

        /// <summary>
        /// Skip char from string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string EscapeLikeValue(string value)
        {
            // Escape the single quote ' by doubling it to ''. Escape * % [ ] characters by wrapping in []. e.g. 
            StringBuilder sb = new StringBuilder(value.Length);
            for (int i = 0; i < value.Length; i++)
            {
                char c = value[i];
                switch (c)
                {
                    case '~':
                    case '(':
                    case ')':
                    case '#':
                    case '\\':
                    case '/':
                    case '=':
                    case '>':
                    case '<':
                    case '+':
                    case '-':
                    case '*':
                    case '%':
                    case '&':
                    case '|':
                    case '^':
                    case '"':
                    case ']':
                    case '[':
                        sb.Append("[").Append(c).Append("]");
                        break;
                    case '\'':
                        sb.Append("''");
                        break;
                    default:
                        sb.Append(c);
                        break;
                }
            }
            return sb.ToString();
        }

        public static string EscapeSpecailCharactersInHTML(string str)
        {
            str = str.Replace("&", "&amp;");
            str = str.Replace(">", "&gt;");
            str = str.Replace("<", "&lt;");
            str = str.Replace("\"", "&quot;");
            str = str.Replace("'", "&#039;");
            return str;
        }


        public static string EscapeSpecailCharactersReverse(string str)
        {
            str = str.Replace("&amp;", "&");
            str = str.Replace("&gt;", ">");
            str = str.Replace("&lt;", "<");
            str = str.Replace("&quot;", "\"");
            str = str.Replace("&#039;", "'");
            return str;
        }

        public static string ReplaceWithDoubleCotes(string value, char delimiter)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(value)) // && value.Contains(delimiter)
            {
                foreach (string item in value.Split(delimiter))
                {
                    result = string.IsNullOrEmpty(result) ? "'" + item + "'," : result + "'" + item + "',";
                }
            }
            return result.TrimEnd(delimiter);
        }



        #endregion



        #region Collections Methods

        /// <summary>
        /// remove duplicates from strin array and return array
        /// Move it to string manager
        /// </summary>
        /// <param name="myList"></param>
        /// <returns></returns>
        public static string[] RemoveDuplicates(string[] myList)
        {
            return myList.Distinct().ToArray();
        }

        /// <summary>
        /// checking collection contains key or not.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsNullOrEmptyCollectionKey(NameValueCollection collection, string key)
        {
            if (collection != null && collection.Count > 0)
                return collection.AllKeys.Contains(key);

            return false;
        }

        /// <summary>
        /// Get string format based on symbol 
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetStringFormatBasedOnKey(NameValueCollection collection, string keyValue)
        {
            if (collection != null && collection.Count > 0)
            {
                foreach (string key in collection.Keys)
                {
                    if (IsEqual(keyValue, key.ToString().Trim()))
                        return GetFormatString((string)collection[key].Trim());
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Get string format based on symbol 
        /// </summary>
        /// <param name="formatSymbol"></param>
        /// <returns></returns>
        private static string GetFormatString(string formatSymbol)
        {
            string format = string.Empty;
            switch (formatSymbol)
            {
                case "$":
                    format = "{0:c2}";
                    break;
                case "%":
                    format = "{0:p1}";
                    break;
                default:
                    break;
            }
            return format;
        }

        /// <summary>
        /// Get string value based on key 
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public static string GetKeyValueOnKey(NameValueCollection collection, string keyValue)
        {
            if (collection != null && collection.Count > 0)
            {
                foreach (string key in collection.Keys)
                {
                    if (IsEqual(keyValue, key.ToString().Trim()))
                        return (string)collection[key].Trim();
                }
            }

            return string.Empty;
        }

        #endregion

        #region ResponsePost

        public static string GenerateResponsePost(string url, string tokenXML)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<html>");
            sb.AppendFormat(@"<body onload='document.forms[""form""].submit()'>");
            sb.AppendFormat("<form name='form' action='{0}' method='post'>", url);

            //add hidden key to hold authenticate XML and make sure that hidden name should be "AuthXml"
            // encode XML before assigning to hidden field else it will error out as it contains < or > symbols
            sb.AppendFormat("<input type='hidden' name='AuthXml' value='{0}'>", tokenXML);
            sb.Append("</form>");
            sb.Append("</body>");
            sb.Append("</html>");
            return sb.ToString();
        }

        #endregion

        #region Email String   

        public static string ReplaceSpecialCharactersFromEmail(string emails)
        {
            if (!string.IsNullOrEmpty(emails))
            {
                emails = Regex.Replace(emails, @"\s+", "");
            }
            return emails;
        }

        #endregion

        #region Camel Case 
        // Convert the string to camel case.
        public static string ToCamelCase(string value)
        {
            // If there are 0 or 1 characters, just return the string.
            if (value == null || value.Length < 2)
                return value;

            // Split the string into words.
            string[] words = value.ToLower().Split(
                new char[] { },
                StringSplitOptions.RemoveEmptyEntries);

            // Combine the words.
            string result = words[0];
            for (int i = 1; i < words.Length; i++)
            {
                result +=
                    words[i].Substring(0, 1).ToUpper() +
                    words[i].Substring(1);
            }

            return result;
        }
        #endregion

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Vector.Common.BusinessLayer
{
    public static class Common
    {
        private const string QueryKeyFormat = "(#{0}#)";
        public static string EncryptDecryptKey = "vEct0R98";
        public static string PrepareXML(List<int> values, string root, string child, string subChild)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<" + root + ">");
            for (int i = 0; i < values.Count; i++)
            {
                sb.Append("<" + child + ">");
                sb.Append("<" + subChild + ">");
                sb.Append(values[i].ToString());
                sb.Append("</" + subChild + ">");
                sb.Append("</" + child + ">");
            }
            sb.Append("</" + root + ">");
            return sb.ToString();
        }

        public static string ReplaceValuesInString(NameValueCollection dynamicValues, string dataString)
        {
            string selectStatement = dataString;
            if (dynamicValues != null)
            {
                foreach (var eachValue in dynamicValues.Keys)
                {
                    if (selectStatement.Contains(eachValue.ToString()))
                    {
                        string value = (dynamicValues.GetValues(eachValue.ToString()) == null) ? "" : dynamicValues.GetValues(eachValue.ToString()).SingleOrDefault();
                        selectStatement = selectStatement.Replace(eachValue.ToString(), value);
                    }
                }
            }
            return selectStatement;
        }

        /// <summary>
        /// Serialize an object into an XML string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeObject<T>(T obj)
        {
            string xmlString = null;
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
                    xs.Serialize(xmlTextWriter, obj);
                    xmlString = UTF8ByteArrayToString(((MemoryStream)xmlTextWriter.BaseStream).ToArray());
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return xmlString;
        }

        /// <summary>
        /// To convert a Byte Array of Unicode values (UTF-8 encoded) to a complete String.
        /// </summary>
        /// <param name="characters">Unicode Byte Array to be converted to String</param>
        /// <returns>String converted from Unicode Byte Array</returns>
        private static string UTF8ByteArrayToString(byte[] characters)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            string constructedString = encoding.GetString(characters);
            return (constructedString);
        }

        private static NameValueCollection GetNameValueCollectionFromTable(DataTable data, string action)
        {
            var objEmailDataCollection = new NameValueCollection();
            if (!DataManager.IsNullOrEmptyDataTable(data))
            {
                foreach (DataColumn item in data.Columns)
                {
                    objEmailDataCollection.Add(string.Format(CultureInfo.CurrentCulture, QueryKeyFormat, item.ColumnName),
                                                             Convert.ToString(data.Rows[0][item.ColumnName]));
                }
            }
            return objEmailDataCollection;
        }

        public static string Decrypt(string password, int keyLength)
        {
            string key = password.Substring(0, keyLength);
            string actualPwd = password.Substring(key.Length);
            byte[] cipherBytes = Convert.FromBase64String(actualPwd.Replace(" ", "+"));
            using (Aes encryptor = Aes.Create())
            {
                var salt = cipherBytes.Take(16).ToArray();
                var iv = cipherBytes.Skip(16).Take(16).ToArray();
                var encrypted = cipherBytes.Skip(32).ToArray();
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptDecryptKey, salt, 100);
                encryptor.Key = pdb.GetBytes(32);
                encryptor.Padding = PaddingMode.PKCS7;
                encryptor.Mode = CipherMode.CBC;
                encryptor.IV = iv;
                using (MemoryStream ms = new MemoryStream(encrypted))
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        using (var reader = new StreamReader(cs, Encoding.UTF8))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }

    public static class VectorTextLogger
    {
        public static string GetExceptionDetails(int i, Exception ex)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(i.ToString() + "Exception:" + ex.Message);
            sb.AppendLine("StackTrace:" + ex.StackTrace);
            if (ex.InnerException != null)
            {
                sb.AppendLine(GetExceptionDetails(++i, ex.InnerException));
            }
            return sb.ToString();
        }

        public static void LogErrortoFile(string errorText, string clientCode = "", string loginId = "", string url = "")
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Client:" + clientCode);
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("User:" + loginId);
            sb.Append(Environment.NewLine);
            sb.Append("On :" + DateTime.Now.ToShortDateString() + " at " + DateTime.Now.ToShortTimeString());
            sb.Append(Environment.NewLine);
            sb.Append("URL :" + url);
            sb.Append(Environment.NewLine);
            sb.Append("Error:" + errorText);
            WriteToErrorLogFile(sb.ToString());
        }

        private static void WriteToErrorLogFile(string logText)
        {
            StreamWriter objStreamWriter = default(StreamWriter);
            try
            {
                string path = ConfigurationManager.AppSettings["ErrorLogFilePath"].ToString();
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path = path + "\\ErrorLog-" + DateTime.Now.ToString("MMMM", CultureInfo.CurrentCulture) + "-" + DateTime.Now.Year + ".txt";
                objStreamWriter = File.AppendText(path);
                objStreamWriter.WriteLine(logText);
                objStreamWriter.WriteLine("------------------------------------------");
                objStreamWriter.Close();
            }
            catch
            {
            }
        }
    }
}

 
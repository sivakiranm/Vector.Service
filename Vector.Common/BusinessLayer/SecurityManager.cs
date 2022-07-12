
namespace Vector.Common.BusinessLayer
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Linq;
    using System.Net;
    using System.Net.Configuration;
    using System.Web;

    public static class SecurityManager
    {
        #region Constants

        private const string PickPassword = "abcdefghijkmnopqrstuvwxyzABCDEFGH­JKLMNOPQRSTUVWXYZ0123456789!@$?";
        private const string DashBoard = "DashBoard";
        private const string MailSettings = "mailSettings/";
        #endregion

        #region Configuration

        public static string GetConfigValue(string key, string defaultValue)
        {
            return (ConfigurationManager.AppSettings[key] ?? defaultValue).ToString();
        }

        public static string GetConfigValue(string key)
        {
            return GetConfigValue(key, string.Empty);
        }

        public static SmtpSection GetSmtpMailSection(string smtpType = "DefaultMail")
        {
            return (SmtpSection)ConfigurationManager.GetSection(MailSettings + smtpType);
        }

        #endregion

        #region Security Methods

        /// <summary>
        /// Decodes the HTML.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <returns>HTML string expanded</returns>
        public static string DecodeHtml(string html)
        {
            return (html.Replace("&apos;", "'").Replace("&quot;", "\"").Replace("&lt;", "<")
                .Replace("&gt;", ">").Replace("&nbsp;", " ").Replace("&amp;", "&").Trim());
        }

        /// <summary>
        /// Encodes the HTML.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <returns>HTML string expanded</returns>
        public static string EncodeHtml(string html)
        {
            return (html.Replace("'", "&apos;").Replace("\"", "&quot;").Replace("<", "&lt;")
                .Replace(">", "&gt;").Replace(" ", "&nbsp;").Replace("&", "&amp;").Trim());
        }

        /// <summary>
        /// get Connection string from web config.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConnectionString(string key)
        {
            return (ConfigurationManager.ConnectionStrings[key]).ToString();
        }

        /// <summary>
        /// Creates the random password.
        /// </summary>
        /// <param name="passwordLength">Length of the password.</param>
        /// <returns>random password</returns>
        public static string CreateRandomPassword(int passwordLength)
        {
            string _allowedChars = PickPassword;
            char[] chars = new char[passwordLength];
            Random rnd = new Random();
            //for (int inx = ConstantMgr.Zero; (inx < passwordLength); inx++)
            //{
            //    chars[inx] = _allowedChars[StringManager.GetNumberValue(rnd.Next(_allowedChars.Length))];
            //}
            return (new string(chars));
        }

        /// <summary>
        /// Creates the random numbers.
        /// </summary>
        /// <param name="passwordLength">Length of the password.</param>
        /// <returns>random password</returns>
        public static string CreateRandomNumbers(int passwordLength)
        {
            string digitLength = "D" + Convert.ToString(passwordLength);
            Random rnd = new Random();
            return rnd.Next(0, 1000000).ToString(digitLength);
        }

        #endregion

        #region Get Ip

        public static string GetIPAddress
        {
            get
            {
                string strHostName = string.Empty;
                strHostName = System.Net.Dns.GetHostName();
                IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);
                IPAddress[] addr = ipEntry.AddressList;
                return addr[addr.Length - 1].ToString();
            }
        }

        #endregion

    }
}

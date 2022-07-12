using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Vector.Common.BusinessLayer;

namespace Vector.WindowsServiceManager
{
    public class ServiceManagerCommon
    {
        private const string FileDirectory = "FileDirectory";
        private const string LogoPath = "/Images/Vector.png";
        private const string EmailTo = "EmailTo";
        private const string EmailCC = "EmailCC";
        private const string EmailBCC = "EmailBCC";
        private const string EmailStopTo = "EmailStopTo";
        private const string EmailStopCC = "EmailStopCC";
        private const string EmailStopBCC = "EmailStopBCC";
        private const string EmailFrom = "EmailFrom";

        public static void WriteExceptionToLogFile(Exception errorMessage, string message = null)
        {
            string path = "/Errors/" + DateManager.GetMonthName(Convert.ToInt32(DateTime.Today.Month.ToString())) + "-" + DateTime.Now.Year.ToString() + ".txt";
            path = AppDomain.CurrentDomain.BaseDirectory.Replace(SecurityManager.GetConfigValue(FileDirectory).ToString(), "") + path;
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
            using (StreamWriter sw = File.AppendText(path))
            {
                string errorText = "Log Entry Date: " + DateTime.Now.ToString(CultureInfo.InvariantCulture) + sw.NewLine;
                if (message == null)
                {
                    errorText += "Error Event: " + ((System.Reflection.MemberInfo)(errorMessage.TargetSite)).Name + sw.NewLine;
                    errorText += "Layer: " + errorMessage.Source + sw.NewLine;
                    errorText += "Error Code: " + Convert.ToInt32(errorMessage.Message.GetTypeCode()) + sw.NewLine;
                    errorText += "Error Message: " + errorMessage.Message;
                }
                else
                {
                    errorText += "Start Message: " + message;
                }
                sw.WriteLine(errorText);
                sw.WriteLine("_________________________________________________________________________________________________");
                sw.Flush();
                sw.Close();
            }
        }

        public static void SendServiceErrorEmail(Exception errorMessage, string clientCode = "", string serviceName = "")
        {
            string Subject = "Vector Console Service Error Notification";
                        
            string ImagePath = AppDomain.CurrentDomain.BaseDirectory.Replace(SecurityManager.GetConfigValue(FileDirectory).ToString(), "") + LogoPath;
                        
            EmailManager.SendEmailWithMutipleAttachments(SecurityManager.GetConfigValue(EmailStopTo), SecurityManager.GetConfigValue(EmailFrom), Subject,
                                                        GenerateErrorEmialBody(errorMessage, clientCode, serviceName), SecurityManager.GetConfigValue(EmailStopCC),
                                                        SecurityManager.GetConfigValue(EmailStopBCC), null, string.Empty, logoPath: ImagePath);            
        }

        private static string GenerateErrorEmialBody(Exception errorMessage, string clientCode = "", string serviceName = "")
        {
            StringBuilder emailBody = new StringBuilder();
            emailBody.Append("<div font-family:Trebuchet MS;font-weight:100;><table width='100%' height='182' border='0' cellpadding='5' cellspacing='1' bgcolor='#006699'style='font-size:11px;'>");
            emailBody.Append("<tr> <td bgcolor='#FFFFFF'><p style='text-align: left;'><img src=\"cid:companylogo\" width='190'height='61'/></p> Hi, <br /><br />");
            emailBody.Append(" Error occurred while running the service. For Client : ");
            emailBody.Append(clientCode);
            emailBody.Append(" Service Name : ");
            emailBody.Append(serviceName);
            emailBody.Append(" Time : ");
            emailBody.Append(DateManager.GetTimeWithZone);
            emailBody.Append(".Please Look into this.<br><br />");
            emailBody.Append("Log Entry Date: ");
            emailBody.Append(DateTime.Now.ToString(CultureInfo.InvariantCulture));
            emailBody.Append("<br><br />");
            emailBody.Append("Error Event: ");
            emailBody.Append(errorMessage == null ? "" : ((System.Reflection.MemberInfo)(errorMessage.TargetSite)).Name);
            emailBody.Append("<br><br />");
            emailBody.Append("Layer: ");
            emailBody.Append(errorMessage == null ? "" : errorMessage.Source);
            emailBody.Append("<br><br />");
            emailBody.Append("Error Code: ");
            emailBody.Append(errorMessage == null ? "" : Convert.ToInt32(errorMessage.Message.GetTypeCode()).ToString());
            emailBody.Append("<br><br />");
            emailBody.Append("Error Message: ");
            emailBody.Append(errorMessage == null ? "" : errorMessage.Message);
            emailBody.Append("<br><br />");
            emailBody.Append(" Regards,<br />");
            emailBody.Append(" <b>Administrator </b><br />");
            emailBody.Append(" <span style='color:#5098B8;FONT-WEIGHT: bold;'>Team Vector</span>");
            emailBody.Append(" </td></tr></table></div>");
            return emailBody.ToString();
        }        
    }
}

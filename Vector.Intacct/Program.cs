using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vector.Intacct.BusinessLogic;
using Vector.Intacct.IntacctUI;

namespace Vector.Intacct
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool instanceCountOne = false;

            using (Mutex mtex = new Mutex(true, "VectorIntacct", out instanceCountOne))
            {
                if (instanceCountOne)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    string appVersion = string.Empty;
                    appVersion = Convert.ToString(ConfigurationManager.AppSettings["AppVersion"]);
                    SecurityContext.Instance.AppVersion = appVersion;
                    Application.Run(new Login());
                }
                else
                {
                    ShowAlert("Information", "An application instance is already running");
                }
            }
        }

        private static void ShowAlert(string title, string mesg, string type = "Alert")
        {
            frmAlert objfrmAlert = new frmAlert(title, mesg, type);
            objfrmAlert.ShowDialog();
            objfrmAlert.Dispose();
        }
    }
}

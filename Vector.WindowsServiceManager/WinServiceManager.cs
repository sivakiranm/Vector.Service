using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Xml.Linq;
using Vector.Common.BusinessLayer;

namespace Vector.WindowsServiceManager
{
    partial class WinServiceManager : ServiceBase
    {
        public System.Timers.Timer DailyServiceTimer = new System.Timers.Timer();
        public System.Timers.Timer MonthlyServiceTimer = new System.Timers.Timer();

        XDocument configurationDocument;
        DataTable serviceDetails;
        public WinServiceManager()
        {
            try
            {
                configurationDocument = ServiceConfiguration.ReadConfigurationXML();
                serviceDetails = ServiceConfiguration.GetServiceDetailsFromXML(configurationDocument);
                //ServiceConfiguration.RunService();
                InitializeComponent();
            }
            catch (Exception ex)
            {
                ServiceManagerCommon.WriteExceptionToLogFile(ex);
            }
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                DailyServiceTimer.Interval = Convert.ToInt64(SecurityManager.GetConfigValue(VectorEnums.ConfigValue.DailyServiceIntervel.ToString()));
                DailyServiceTimer.Elapsed += new ElapsedEventHandler(DailyServiceTimer_Elapsed);
                DailyServiceTimer.Enabled = true;

                MonthlyServiceTimer.Interval = Convert.ToInt64(SecurityManager.GetConfigValue(VectorEnums.ConfigValue.MonthlyServiceIntervel.ToString()));
                MonthlyServiceTimer.Elapsed += new ElapsedEventHandler(MonthlyServiceTimer_Elapsed);
                MonthlyServiceTimer.Enabled = true;
            }
            catch (Exception ex)
            {
                ServiceManagerCommon.WriteExceptionToLogFile(ex);
            }
        }

        public void DailyServiceTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (!DataManager.IsNullOrEmptyDataTable(serviceDetails))
                {
                    ServiceConfiguration.ExecuteDailyServices(configurationDocument, serviceDetails, e.SignalTime);

                }
            }
            catch (Exception ex)
            {
                ServiceManagerCommon.WriteExceptionToLogFile(ex);
            }
        }

        private void MonthlyServiceTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (!DataManager.IsNullOrEmptyDataTable(serviceDetails))
                {

                    //ServiceConfiguration.ExecuteMonthlyServices(configurationDocument, serviceDetails, e.SignalTime);
                }
            }
            catch (Exception ex)
            {
                ServiceManagerCommon.WriteExceptionToLogFile(ex);
            }
        }

        protected override void OnStop()
        {
            try
            {
                DailyServiceTimer.Stop();
                DailyServiceTimer.Dispose();

                MonthlyServiceTimer.Stop();
                MonthlyServiceTimer.Dispose();
            }
            catch (Exception ex)
            {
                ServiceManagerCommon.WriteExceptionToLogFile(ex);
                ServiceManagerCommon.SendServiceErrorEmail(ex);
            }
        }
    }
}

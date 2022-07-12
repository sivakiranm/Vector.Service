using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using Vector.Common.BusinessLayer;
using Vector.Intacct.APIAccess;
using Vector.Intacct.BusinessLogic;
using Vector.Intacct.Entities;

namespace Vector.Intacct.IntacctUI
{
    public partial class frmIntacct : Form
    {
        public volatile int TotalProcesses = 0;
        public volatile int CompletedProcesses = 0;

        string logFilePath = string.Empty;
        DataTable dtTemplate = new DataTable();
        StringBuilder logFileContent = new StringBuilder();
        public const string invoiceDtls = "Invoice Details";
        public const string UploadeWarning = "Please Upload Valid Excel Template";
        string ArPaymentLogfilepath = string.Empty;
        string filexmlpath = "\\Files\\XML\\ARPayment_Column.xml";

        string stringCustomer = string.Empty;
        string stringProperty = string.Empty;
        string stringHauler = string.Empty;
        string stringInvoice = string.Empty;
        string stringArPayments = string.Empty;
        string stringRepairInvoice = string.Empty;
        string stringMarkDuplicateInvoice = string.Empty;
        string stringDownloadPayments = string.Empty;


        string headerDetails = string.Empty;
        List<function> objFunctionList = new List<function>();

        Stopwatch timer = new Stopwatch();
        public frmIntacct()
        {
            InitializeComponent();
        }

        #region Upload or Download

        #region Events      

        private void btnCheckAll_Click(object sender, EventArgs e)
        {
            try
            {
                CheckUnCheck(true);
            }
            catch (Exception ex)
            {
                ShowAlert("Error", ex.Message);
            }
        }

        private void btnCheckNone_Click(object sender, EventArgs e)
        {
            try
            {
                CheckUnCheck(false);
            }
            catch (Exception ex)
            {
                ShowAlert("Error", ex.Message);
            }
        }

        private void btnLogs_Click(object sender, EventArgs e)
        {
            try
            {
                if (Directory.Exists(txtLogFilePath.Text))
                {
                    System.Diagnostics.Process.Start(txtLogFilePath.Text);
                }
                else
                    ShowAlert("Error", Constants.LogFileNotFound);
            }
            catch (Exception ex)
            {
                ShowAlert("Error", ex.Message);
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                //To Log Report like how many update, to be updated and etc
                var objReport = new List<Vector.Intacct.Entities.Report>();

                if (ValidateDates())
                {
                    StartReport(objReport);
                }
            }
            catch (Exception ex)
            {
                ShowAlert("Error", ex.Message);

                EnableDisableAllControls(true);

                // set initial image
                this.pbProgress.Image = null;

                ErrorLog.GenerateErrorDetails(ex, string.Empty, string.Empty, string.Empty, Constants.Technical);
            }
        }

        private void lnkViewLog_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                OpenLatestFileInLogFiles(txtLogFilePath.Text, logFilePath);
            }
            catch (Exception ex)
            {
                ShowAlert("Error", ex.Message);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Start report with 
        /// </summary>
        /// <param name="objReport"></param>
        private void StartReportProcess(List<Report> objReport)
        {
            // set initial image
            this.pbProgress.Image = Properties.Resources.Animation;

            //To Log in Log file Run Time
            timer.Start();
            EnableDisableAllControls(false);

            logFilePath = BusinessLogic.Common.GenerateLogFilePath(txtLogFilePath.Text);

            headerDetails = GetLogHeader(txtFromDate.Text.Trim(), txtToDate.Text.Trim(),
                                    EnumMgr.Desc(rbUpdate.Checked ? EnumManager.ProcessOptions.Update : EnumManager.ProcessOptions.Compare),
                                     logFilePath);


            //check if the process got completed
            CompletedProcesses = Constants.Zero;

            //Total Processes (Hauler + property + etc)
            TotalProcesses = GetTotalProcessCount();

            //Create Thread that will run asynchronously for generating report
            Thread thread = new Thread(() => GenerateReport(objReport));

            //run Thread 
            thread.IsBackground = true;
            thread.Start();
        }

        /// <summary>
        /// Generate Report for each seleced process
        /// </summary>
        /// <param name="objReport"></param>
        public void GenerateReport(List<Report> objReport)
        {
            using (ConnectionEntity objConnection = new ConnectionEntity())
            {
                objConnection.UserId = SecurityManager.GetConfigValue(Constants.UserName);
                objConnection.UserPassword = SecurityManager.GetConfigValue(Constants.Password);
                objConnection.SenderId = SecurityManager.GetConfigValue(Constants.SenderId);
                objConnection.SenderPassword = SecurityManager.GetConfigValue(Constants.SenderPassword);
                objConnection.CompanyId = SecurityManager.GetConfigValue(Constants.CompanyId);
                objConnection.IntacctApiUrl = SecurityManager.GetConfigValue(Constants.ApiPath);

                IntacctLogin objLogin = IntacctConnector.EstablishIntacctConnection(objConnection);

                //Sync Customer
                if (cbCustomer.Checked)
                    RunReport(EnumManager.IntacctType.Customer, objReport, objLogin);

                //Sync Property
                if (cbProperty.Checked)
                    RunReport(EnumManager.IntacctType.Project, objReport, objLogin);

                //Sync Invoice
                if (cbInvoice.Checked)
                    RunReport(EnumManager.IntacctType.ArInvoice, objReport, objLogin);

                //Payment Reverse Sync
                if (cbReversSync.Checked)
                    RunReport(EnumManager.IntacctType.arpayment, objReport, objLogin);
            }
        }

        /// <summary>
        /// Run the report.
        /// </summary>
        /// <param name="intacctDataType"></param>
        /// <param name="objReport"></param>
        /// <param name="objLogin"></param>
        private void RunReport(EnumManager.IntacctType intacctDataType, List<Report> objReport, IntacctLogin objLogin)
        {
            using (SelectionEntity objSelectionEntity = new SelectionEntity())
            {
                {   //Run Report for each type like Invocie/Hauler/Invoice and Property. This is common method for all
                    objFunctionList.Clear();

                    using (SelectionEntity objSelection = new SelectionEntity())
                    {
                        objSelection.FromDate = txtFromDate.Text.Trim();
                        objSelection.ToDate = txtToDate.Text.Trim();
                        objSelection.IntactType = intacctDataType;
                        objSelection.OperationType = rbUpdate.Checked ? EnumManager.ProcessOptions.Update : EnumManager.ProcessOptions.Compare;
                        objSelection.LoginObject = objLogin;
                        objSelection.lblCustomerToProcess = lblCRowsToProcess;
                        objSelection.lblCustomersProcessed = lblCRowsprocessed;
                        objSelection.lblCustomersRemaning = lblCRowsRemaining;
                        objSelection.lblPropertiesToProcess = lblPRowsToProcess;
                        objSelection.lblPropertiesProcessed = lblPRowsprocessed;
                        objSelection.lblPropertiesRemaning = lblPRowsRemaining;

                        objSelection.lblInvoiceToProcess = lblIRowsToProcess;
                        objSelection.lblInvoiceProcessed = lblIRowsprocessed;
                        objSelection.lblInvoiceRemaning = lblIRowsRemaining;

                        objSelection.lblArPaymentToProcess = lblPayRowsToProcess;
                        objSelection.lblArPaymentProcessed = lblPayRowsprocessed;
                        objSelection.lblArPaymentRemaning = lblPayRowsRemaining;
                        objSelection.isBulk = rbBulk.Checked ? true : false;

                        if (intacctDataType == EnumManager.IntacctType.arpayment)
                        {
                            ReportProgress(objSelection.IntactType, ARPaymentReverseSync(objLogin, objSelection));
                        }
                        else
                        {
                            ReportProgress(objSelection.IntactType, IntacctBusinessLogic.RunProcessForEachType(objSelection, objFunctionList));
                        }
                    }
                }
            }

        }

        private string ARPaymentReverseSync(IntacctLogin session, SelectionEntity objSelectionEntity)
        {
            string paymentLogfielContent = string.Empty;
            try
            {
                using (ConnectionEntity objConnection = new ConnectionEntity())
                {
                    objConnection.UserId = SecurityManager.GetConfigValue(Constants.UserName);
                    objConnection.UserPassword = SecurityManager.GetConfigValue(Constants.Password);
                    objConnection.SenderId = SecurityManager.GetConfigValue(Constants.SenderId);
                    objConnection.SenderPassword = SecurityManager.GetConfigValue(Constants.SenderPassword);
                    objConnection.CompanyId = SecurityManager.GetConfigValue(Constants.CompanyId);
                    objConnection.IntacctApiUrl = SecurityManager.GetConfigValue(Constants.ApiPath);

                    IntacctLogin objLogin = IntacctConnector.EstablishIntacctConnection(objConnection);
                    DataSet dsInvoice = new DataSet();

                    if (!string.IsNullOrEmpty(txtFromDate.ToString()) && !string.IsNullOrEmpty(txtToDate.ToString()))
                    {
                        string PaymentheaderDetails = string.Empty;

                        dsInvoice = SyncEachRecord.GetInvoiceDetailsByQuery(objLogin, txtFromDate.Text, txtToDate.Text);

                        if (!string.IsNullOrEmpty(txtLogFilePath.Text))
                        {
                            logFilePath = BusinessLogic.Common.GenerateLogFilePath(txtLogFilePath.Text);

                            PaymentheaderDetails = GetLogHeader(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), "Reverse Sync", logFilePath);

                            if (dsInvoice != null && dsInvoice.Tables.Count > 0)
                            {
                                paymentLogfielContent = SyncEachRecord.PaymentReverseSync(dsInvoice, logFilePath, PaymentheaderDetails, session, objSelectionEntity);

                            }
                            else
                            {
                                //ShowAlert("Information", Constants.NoDataFoundIntacct);
                                ErrorLog.LogIntacctExceptions(string.Empty, SecurityContext.Instance.LogInUserId,
                                                                                                SecurityContext.Instance.LogInPassword,
                                                                                                Constants.ArEntity,
                                                                                                string.Empty,
                                                                                                Constants.ArEntity,
                                                                                               Constants.NoDataFoundIntacct,
                                                                                                objConnection.SenderId,
                                                                                                SecurityManager.GetIPAddress.ToString(),
                                                                                                Constants.FunctionalError,
                                                                                                string.Empty);
                            }
                        }
                        else
                        {
                            ShowAlert("Validation", "Please Select filepath to Save LogFile");
                        }
                    }
                    else
                    {
                        ShowAlert("Validation", "Please Select From and To Date Range to Reverse Sync the Invoices");
                    }

                }

            }
            catch (Exception ex)
            {
                ShowAlert("Error", ex.Message);

                ErrorLog.GenerateErrorDetails(ex, string.Empty, string.Empty, string.Empty, "Technical");
                ErrorLog.LogIntacctExceptions(string.Empty, SecurityContext.Instance.LogInUserId,
                                                                               SecurityContext.Instance.LogInPassword,
                                                                               "AREntity",
                                                                               string.Empty,
                                                                               "AREntity",
                                                                               ex.Message,
                                                                               string.Empty,
                                                                               SecurityManager.GetIPAddress.ToString(),
                                                                               Constants.TechnicalError,
                                                                               string.Empty);

            }
            return paymentLogfielContent;
        }

        private void ReportProgress(EnumManager.IntacctType IntacctDataType, string logReportString)
        {
            CompletedProcesses++;

            FillResultStrings(IntacctDataType, logReportString);

            if (CompletedProcesses == TotalProcesses)
            {
                this.BeginInvoke(new Action(() =>
                {
                    EndReportGeneration();
                }));
            }
        }

        private void FillResultStrings(EnumManager.IntacctType IntacctDataType, string logReportString)
        {
            switch (IntacctDataType)
            {
                case EnumManager.IntacctType.Customer:
                    stringCustomer = logReportString;
                    break;

                case EnumManager.IntacctType.Project:
                    stringProperty = logReportString;
                    break;

                case EnumManager.IntacctType.ArInvoice:
                    stringInvoice = logReportString;
                    break;

                case EnumManager.IntacctType.arpayment:
                    stringArPayments = logReportString;
                    break;
            }
        }

        private void EndReportGeneration()
        {
            SaveTextToFile(logFilePath, GenerateFinalLog().ToString());

            EnableDisableAllControls(true);
            this.pbProgress.Image = null;
        }

        private StringBuilder GenerateFinalLog()
        {
            StringBuilder finalLog = new StringBuilder();
            finalLog.Append(headerDetails);
            finalLog.Append(stringCustomer);
            finalLog.Append(stringProperty);
            finalLog.Append(stringHauler);
            finalLog.Append(stringInvoice);
            finalLog.Append(stringArPayments);
            finalLog.Append(stringRepairInvoice);
            finalLog.Append(stringMarkDuplicateInvoice);
            finalLog.Append(stringDownloadPayments);
            finalLog.AppendLine();
            finalLog.AppendLine(Constants.DashLine);
            timer.Stop();
            finalLog.AppendLine(Constants.RunTime + timer.Elapsed.ToString());
            finalLog.AppendLine(Constants.DashLine);
            timer.Reset();
            return finalLog;
        }

        public void SaveTextToFile(string logFilePath, string strData)
        {
            using (FileStream fs = new FileStream(logFilePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(strData);
                }
            }
        }

        /// <summary>
        /// Get number of processes checked i.e property + hauler + Client + Invoice (Total 4)
        /// property + Hauler --> Total 2 etc
        /// </summary>
        /// <returns></returns>
        private int GetTotalProcessCount()
        {
            int count = Constants.Zero;

            foreach (Control control in this.gbProcess.Controls)
            {
                if (control is CheckBox)
                    if (((CheckBox)control).Checked)
                        count++;
            }
            return count;
        }


        public string GetLogHeader(string fromDate, string toDate, string processOption, string logFilePath)
        {
            return string.Format(Constants.LogHeader, processOption, System.DateTime.Now.ToString(), string.Empty,
                                 string.Empty, logFilePath, string.Empty, fromDate, toDate).ToString();
        }


        /// <summary>
        /// Check if Log file path 
        /// </summary>
        /// <param name="objReport"></param>
        private void StartReport(List<Report> objReport)
        {
            //Check if File Path Exists
            string result = CheckForServiceAndLogFilePath();
            if (string.IsNullOrEmpty(result))
                StartReportProcess(objReport);
            else
                ShowAlert("Validation", result);
        }

        /// <summary>
        /// Check for log file path and file exists 
        /// check if service exists
        /// </summary>
        /// <returns></returns>
        private string CheckForServiceAndLogFilePath()
        {
            //check for log File
            if (Directory.Exists(txtLogFilePath.Text))
            {
                if (cbCustomer.Checked || cbProperty.Checked || cbInvoice.Checked || cbReversSync.Checked)
                    return Constants.Empty;
                else
                    return Constants.CheckIntacctDataTypeMessage;
            }
            return Constants.SettingsNotFound;
        }

        private bool ValidateDates()
        {
            if (DateTime.Compare(txtFromDate.Value.Date, txtToDate.Value.Date) > 0)
            {
                ShowAlert("Validation", "Begin date should not be later than End date");
                return false;
            }
            else if (DateTime.Compare(txtToDate.Value.Date, DateTime.Now.Date) > 0)
            {
                ShowAlert("Validation", "End date should not be earlier than Current Date");
                return false;
            }
            else
                return true;
        }

        private void EnableDisableAllControls(bool status)
        {
            cbCustomer.Enabled = status;
            cbProperty.Enabled = status;
            cbInvoice.Enabled = status;
            btnCheckAll.Enabled = status;
            btnCheckNone.Enabled = status;
            rbCompare.Enabled = status;
            rbUpdate.Enabled = status;
            txtFromDate.Enabled = status;
            txtToDate.Enabled = status;
            btnLogs.Enabled = status;
            lblLogOut.Enabled = status;
            lnkViewLog.Enabled = status;
            btnGo.Enabled = status;
            cbReversSync.Enabled = status;

            stringCustomer = string.Empty;
            stringProperty = string.Empty;
            stringHauler = string.Empty;
            stringInvoice = string.Empty;
            stringArPayments = string.Empty;
            stringRepairInvoice = string.Empty;
            stringMarkDuplicateInvoice = string.Empty;

            if (!status)
            {
                SetRowProcessCounts(Constants.Zero.ToString(), Constants.Zero.ToString(), Constants.Zero.ToString(),
                                        lblCRowsToProcess, lblCRowsprocessed, lblCRowsRemaining);

                SetRowProcessCounts(Constants.Zero.ToString(), Constants.Zero.ToString(), Constants.Zero.ToString(),
                                        lblPRowsToProcess, lblPRowsprocessed, lblPRowsRemaining);

                SetRowProcessCounts(Constants.Zero.ToString(), Constants.Zero.ToString(), Constants.Zero.ToString(),
                                         lblIRowsToProcess, lblIRowsprocessed, lblIRowsRemaining);

                SetRowProcessCounts(Constants.Zero.ToString(), Constants.Zero.ToString(), Constants.Zero.ToString(),
                                         lblPayRowsToProcess, lblPayRowsprocessed, lblPayRowsRemaining);

            }
        }

        /// <summary>
        /// Generate count for the UI panel to show the report process inorder avoid idle page behaviour.
        /// </summary>
        /// <param name="rowsToProcess"></param>
        /// <param name="rowsProcessed"></param>
        /// <param name="rowsRemaining"></param>
        /// <param name="lblRowsToProcess"></param>
        /// <param name="lblRowsprocessed"></param>
        /// <param name="lblRowsRemaining"></param>
        private void SetRowProcessCounts(string rowsToProcess, string rowsProcessed, string rowsRemaining,
                               Label lblRowsToProcess, Label lblRowsprocessed, Label lblRowsRemaining)
        {
            lblRowsToProcess.Text = rowsToProcess;
            lblRowsprocessed.Text = rowsProcessed;
            lblRowsRemaining.Text = rowsRemaining;
            lblRowsToProcess.Refresh();
            lblRowsprocessed.Refresh();
            lblRowsRemaining.Refresh();
        }

        public void OpenLatestFileInLogFiles(string txtLogFilePath, string logFilePath)
        {
            var latestFile = Directory
                    .EnumerateFiles(txtLogFilePath, Constants.TxtFiles, SearchOption.TopDirectoryOnly)
                    .Select(path => new
                    {
                        Path = path,
                        Date = DateManager.GetDateTimeInCustomFormat(Path.GetFileNameWithoutExtension(path).
                                Split(Constants.CharUnderscore).Last(), newformat: Constants.DateFormat)
                    })
                    .OrderByDescending(df => df.Date)
                    .Select(df => df.Path)
                    .FirstOrDefault();

            if (logFilePath != null && (File.Exists(logFilePath)))
                System.Diagnostics.Process.Start(logFilePath);
            else
                ShowAlert("Error", Constants.LogFileNotFound);
        }

        private void CheckUnCheck(bool status)
        {
            foreach (Control control in this.gbProcess.Controls)
            {
                if (control is CheckBox)
                    ((CheckBox)control).Checked = status;
            }
        }

        #endregion
        #endregion

        #region AR Payments
        private void btnARPayment_Click(object sender, EventArgs e)
        {
            try
            {
                ExcelUpload.Filter = Constants.UploadExcelType;

                if (ExcelUpload.ShowDialog() == DialogResult.OK)
                {
                    txtARPayment.Text = ExcelUpload.FileName;

                    if (!string.IsNullOrEmpty(txtARPayment.Text))
                    {
                        try
                        {
                            dtTemplate = GenerateDataTable(txtARPayment.Text.Trim());

                            if (dtTemplate != null && dtTemplate.Rows.Count > 0)
                            {
                                btnARPaymentUpload.Enabled = true;
                                btnARPaymentUpload.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
                            }
                        }
                        catch (Exception ex)
                        {
                            txtARPayment.Text = string.Empty;
                            ShowAlert("Error", ex.ToString());

                            logFileContent.AppendLine(Constants.Error + ex.Message + Environment.NewLine + Constants.SkippingRemaining + invoiceDtls);
                            ErrorLog.GenerateErrorDetails(ex, string.Empty, string.Empty, string.Empty, Constants.TechnicalError);
                        }
                    }
                }
                else
                {
                    ShowAlert("Error", UploadeWarning);
                }
            }
            catch (Exception ex)
            {
                ShowAlert("Error", ex.Message);
            }
        }

        Stopwatch timerPayment = new Stopwatch();
        private void btnARPaymentUpload_Click(object sender, EventArgs e)
        {
            try
            {
                this.pbARPayment.Image = Properties.Resources.Animation;

                //To Log in Log file Run Time
                timerPayment.Start();

                if (!string.IsNullOrEmpty(txtARPayment.Text))
                {
                    // set a default file name
                    if (!string.IsNullOrEmpty(txtLogFilePath.Text))
                    {
                        ArPaymentLogfilepath = BusinessLogic.Common.GenerateLogFilePath(txtLogFilePath.Text);

                        using (ConnectionEntity objConnection = new ConnectionEntity())
                        {
                            objConnection.UserId = SecurityManager.GetConfigValue(Constants.UserName);
                            objConnection.UserPassword = SecurityManager.GetConfigValue(Constants.Password);
                            objConnection.SenderId = SecurityManager.GetConfigValue(Constants.SenderId);
                            objConnection.SenderPassword = SecurityManager.GetConfigValue(Constants.SenderPassword);
                            objConnection.CompanyId = SecurityManager.GetConfigValue(Constants.CompanyId);
                            objConnection.IntacctApiUrl = SecurityManager.GetConfigValue(Constants.ApiPath);

                            using (SyncEachRecord arpayments = new SyncEachRecord())
                            {
                                ArPaymentLogfilepath = BusinessLogic.Common.GenerateLogFilePath(txtLogFile.Text);

                                if (dtTemplate != null && dtTemplate.Rows.Count > 0 && ValidateDataTable(dtTemplate))
                                {
                                    if (arpayments.RunProcessForEachRecordForTemplate(dtTemplate, IntacctConnector.EstablishIntacctConnection(objConnection), ArPaymentLogfilepath))
                                    {
                                        ShowAlert("Success", Constants.ArPaymentsUpdated);
                                    }
                                    else
                                    {
                                        ShowAlert("Error", Constants.NoCustomersOrInvoices);
                                    }
                                    txtARPayment.Text = string.Empty;
                                    timerPayment.Stop();
                                    timerPayment.Reset();

                                }
                            }
                        }
                    }
                    else
                    {
                        ShowAlert("Validation", Constants.PleaseSelectFilePath);
                    }
                }
                else
                {
                    ShowAlert("Validation", Constants.PleaseSelectOnlyExcelFile);
                }

                btnARPaymentUpload.Enabled = false;
                btnARPaymentUpload.ForeColor = Color.Yellow;
                this.pbARPayment.Image = null;
            }
            catch (Exception ex)
            {
                ShowAlert("Error", ex.Message);

                this.pbARPayment.Image = null;

                ErrorLog.GenerateErrorDetails(ex, Constants.ArEntity, string.Empty, string.Empty, Constants.TechnicalError);
                ErrorLog.LogIntacctExceptions(string.Empty, SecurityContext.Instance.LogInUserId,
                                                                                                SecurityContext.Instance.LogInPassword,
                                                                                                Constants.ArEntity,
                                                                                                string.Empty,
                                                                                                Constants.ArEntity,
                                                                                                ex.Message,
                                                                                                string.Empty,
                                                                                                SecurityManager.GetIPAddress.ToString(),
                                                                                                Constants.TechnicalError,
                                                                                                string.Empty);
            }
        }

        private void btnLogFilePath_Click(object sender, EventArgs e)
        {
            try
            {
                if (ARPymntBrosweDialog.ShowDialog() == DialogResult.OK)
                {
                    txtLogFile.Text = ARPymntBrosweDialog.SelectedPath;
                }
            }
            catch (Exception ex)
            {
                ShowAlert("Error", ex.Message);
            }
        }

        #region Methods

        private bool ValidateDataTable(DataTable dtTemplate)
        {
            try
            {
                DataSet ds = new DataSet();

                string xmlPath = AppDomain.CurrentDomain.BaseDirectory.Replace(Constants.ReplacePath, string.Empty).ToString()
                                                        + filexmlpath;

                ds.ReadXml(xmlPath);
                if (ds.Tables[0] != null && ds.Tables[0].Columns.Count > 0)
                {
                    DataTable dt = ds.Tables[0];

                    int colCount = dt.Columns.Count;
                    if (dt.Rows[0] != null)
                    {
                        DataRow row = dt.Rows[0];

                        for (int i = 0; i < colCount; i++)
                        {
                            if (!string.Equals(dtTemplate.Columns[i].ColumnName, row[i].ToString()))
                            {
                                ShowAlert("Validation", UploadeWarning);
                                txtARPayment.Text = string.Empty;
                                return false;
                            }
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                ShowAlert("Error", ex.Message);

                ErrorLog.GenerateErrorDetails(ex, Constants.ArEntity, string.Empty, string.Empty, Constants.TechnicalError);
                ErrorLog.LogIntacctExceptions(string.Empty, SecurityContext.Instance.LogInUserId,
                                                                                                SecurityContext.Instance.LogInPassword,
                                                                                                Constants.ArEntity,
                                                                                                string.Empty,
                                                                                                Constants.ArEntity,
                                                                                                ex.Message,
                                                                                                string.Empty,
                                                                                                SecurityManager.GetIPAddress.ToString(),
                                                                                                Constants.TechnicalError,
                                                                                                string.Empty);
            }

            return true;

        }

        private DataTable GenerateDataTable(string excelTemplate)
        {
            DataTable dt = new DataTable();
            string extenstion = string.Empty;
            ExcelLibrary Lib = new ExcelLibrary();

            if (!string.IsNullOrEmpty(excelTemplate))
            {
                extenstion = Path.GetExtension(excelTemplate);
                if (!string.IsNullOrEmpty(extenstion))
                {
                    switch (extenstion)
                    {
                        case ".xlsx":
                            dt = ExportToExcel.ReadExcel(excelTemplate, true);
                            break;
                        case ".xls":

                            dt = Lib.ReadExcel(excelTemplate, 0, 1);

                            break;
                        default:
                            break;
                    }

                }

            }
            return dt;
        }

        #endregion

        #endregion

        #region Settings

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtLogFilePath.Text))
                    UpdateCompanyAndLogFilePathInXMLFile();
                else
                    ShowAlert("Error", Constants.SettingsFillDetailsError);
            }
            catch (Exception ex)
            {
                ShowAlert("Error", ex.Message);
            }
        }

        private void btnSelectLogFilePath_Click(object sender, EventArgs e)
        {
            try
            {
                if (BrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    txtLogFilePath.Text = BrowserDialog.SelectedPath;
                }
            }
            catch (Exception ex)
            {
                ShowAlert("Error", ex.Message);
            }
        }

        private void UpdateCompanyAndLogFilePathInXMLFile()
        {
            if (Directory.Exists(txtLogFilePath.Text))
            {
                string xmlPath = GetConfigurationFilePath();
                if (File.Exists(xmlPath))
                {
                    XmlDocument xml = new XmlDocument();
                    xml.Load(xmlPath);
                    XmlNodeList nodes = xml.SelectNodes(EnumManager.XMLNodes.Configuration.ToString());
                    foreach (XmlElement element in nodes)
                    {
                        XmlManager.UpdateNodeValue(element, EnumManager.XMLNodes.LogFilePath.ToString(), txtLogFilePath.Text.Trim());
                        XmlManager.UpdateNodeValue(element, EnumManager.XMLNodes.ServicePath.ToString(), txtServicePath.Text.Trim());
                        xml.Save(xmlPath);
                    }
                    lblSettingsResult.Text = Constants.SettingUpdateSuccess;

                    //Update Path for ArPayment LoFile
                    txtLogFile.Text = txtLogFilePath.Text.ToString();
                }
                else
                    ShowAlert("Error", Constants.ConfigurationNotFound);
            }
            else
                ShowAlert("Error", Constants.LogFileNotFoundError);
        }

        private static string GetConfigurationFilePath()
        {
            return AppDomain.CurrentDomain.BaseDirectory.Replace(Constants.ReplacePath, string.Empty).ToString()
                                                      + EnumMgr.Desc(EnumManager.XMLNodes.ConfigurationXML);
        }

        #endregion

        #region Reverse Sync

        private void btnUpdateIntacctId_Click(object sender, EventArgs e)
        {
            try
            {
                using (SelectionEntity objSelection = new SelectionEntity())
                {
                    if (!String.IsNullOrEmpty(txtInputId.Text))
                    {
                        btnUpdateIntacctId.Enabled = false;
                        string entityId = Convert.ToString(txtInputId.Text);
                        using (ConnectionEntity objConnection = new ConnectionEntity())
                        {
                            objConnection.UserId = SecurityManager.GetConfigValue(Constants.UserName);
                            objConnection.UserPassword = SecurityManager.GetConfigValue(Constants.Password);
                            objConnection.SenderId = SecurityManager.GetConfigValue(Constants.SenderId);
                            objConnection.SenderPassword = SecurityManager.GetConfigValue(Constants.SenderPassword);
                            objConnection.CompanyId = SecurityManager.GetConfigValue(Constants.CompanyId);
                            objConnection.IntacctApiUrl = SecurityManager.GetConfigValue(Constants.ApiPath);

                            //ProRefuseServiceResponse res = new ProRefuseServiceResponse();
                            IntacctLogin objLogin = IntacctConnector.EstablishIntacctConnection(objConnection);
                            objSelection.LoginObject = objLogin;

                            //Sync Customer
                            if (rbnCustomerIntacctIdSync.Checked)
                            {
                                //Check for Customer in Intacct and if exists update that back to database.
                                objSelection.IntactType = EnumManager.IntacctType.Customer;
                                string customerString = IntacctBusinessLogic.CheckForEntityInIntacct(entityId, objSelection);
                                if (string.IsNullOrEmpty(customerString))
                                {
                                    lblStatusMessage.Visible = true;
                                    lblStatusMessage.Text = "Invalid Customer Entered";
                                    btnUpdateIntacctId.Enabled = true;
                                }
                                else
                                {
                                    //Update the same customerId into Database back as intacct id.                                                                
                                    var res = (VectorResponse<object>)IntacctBusinessLogic.UpdateIntacctIdInRefuse(objSelection.IntactType, entityId, entityId);
                                    if (StringManager.IsEqual(res.Response.ToString(), Constants.One))
                                    {
                                        lblStatusMessage.Visible = true;
                                        lblStatusMessage.Text = Constants.UpdatedSuccessfully;
                                        btnUpdateIntacctId.Enabled = true;
                                    }
                                    else if (StringManager.IsEqual(res.Response.ToString(), Constants.MinusOne))
                                    {
                                        lblStatusMessage.Visible = true;
                                        lblStatusMessage.Text = Constants.CannotUpdateIntacctId;
                                        btnUpdateIntacctId.Enabled = true;
                                    }

                                }
                            }

                            //Sync Property
                            if (rbnProjectIntacctIdSync.Checked)
                            {
                                //Check for Project in Intacct and if exists update that back to database.
                                objSelection.IntactType = EnumManager.IntacctType.Project;
                                string projectString = IntacctBusinessLogic.CheckForEntityInIntacct(entityId, objSelection);
                                if (string.IsNullOrEmpty(projectString))
                                {
                                    lblStatusMessage.Visible = true;
                                    lblStatusMessage.Text = "Invalid Property Entered";
                                    btnUpdateIntacctId.Enabled = true;
                                }
                                else
                                {
                                    //Update the same projectid, into Database back as intacct id.                                
                                    var res = (VectorResponse<object>)IntacctBusinessLogic.UpdateIntacctIdInRefuse(objSelection.IntactType, entityId, entityId);
                                    if (StringManager.IsEqual(res.Response.ToString(), Constants.One))
                                    {
                                        lblStatusMessage.Visible = true;
                                        lblStatusMessage.Text = Constants.UpdatedSuccessfully;
                                        btnUpdateIntacctId.Enabled = true;
                                    }
                                    else if (StringManager.IsEqual(res.Response.ToString(), Constants.MinusOne))
                                    {
                                        lblStatusMessage.Visible = true;
                                        lblStatusMessage.Text = Constants.CannotUpdateIntacctId;
                                        btnUpdateIntacctId.Enabled = true;
                                    }
                                }
                            }

                            //Sync Invoice
                            if (rbnInvoiceIntacctIdSync.Checked)
                            {
                                //Check for Invoice in Intacct and if exists update that back to database.
                                objSelection.IntactType = EnumManager.IntacctType.ArInvoice;
                                string invocieKey = IntacctBusinessLogic.CheckForEntityInIntacct(entityId, objSelection);
                                if (string.IsNullOrEmpty(invocieKey))
                                {
                                    lblStatusMessage.Visible = true;
                                    lblStatusMessage.Text = "Invalid Invoice Entered";
                                    btnUpdateIntacctId.Enabled = true;
                                }
                                else
                                {
                                    //Update the same Invoice key into Database back as intacct id.
                                    var res = (VectorResponse<object>)IntacctBusinessLogic.UpdateIntacctIdInRefuse(objSelection.IntactType, entityId, invocieKey);
                                    if (StringManager.IsEqual(res.Response.ToString(), Constants.One))
                                    {
                                        lblStatusMessage.Visible = true;
                                        lblStatusMessage.Text = Constants.UpdatedSuccessfully;
                                        btnUpdateIntacctId.Enabled = true;
                                    }
                                    else if (StringManager.IsEqual(res.Response.ToString(), Constants.MinusOne))
                                    {
                                        lblStatusMessage.Visible = true;
                                        lblStatusMessage.Text = Constants.CannotUpdateIntacctId;
                                        btnUpdateIntacctId.Enabled = true;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        lblStatusMessage.Visible = true;
                        lblStatusMessage.Text = "Please Select Value";
                        btnUpdateIntacctId.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowAlert("Error", ex.Message);
            }
        }

        #endregion

        #region Common
        private void lblLogOut_Click(object sender, EventArgs e)
        {
            try
            {
                LogOff();
            }
            catch (Exception ex)
            {
                ShowAlert("Error", ex.Message);
            }
        }

        private void LoadProgress(bool isLoading)
        {
            if (isLoading)
                Cursor = Cursors.WaitCursor;
            else
                Cursor = Cursors.Default;
        }

        private void ShowAlert(string title, string mesg, string type = "Alert")
        {
            frmAlert objfrmAlert = new frmAlert(title, mesg, type);
            if (objfrmAlert.ShowDialog(this) == DialogResult.OK)
                LoadProgress(false);

            objfrmAlert.Dispose();
        }

        private void LogOff()
        {
            SecurityContext.Instance.LogInUserId = string.Empty;
            SecurityContext.Instance.LogInPassword = string.Empty;
            this.Close();
            Login loginPage = new Login();
            loginPage.Show();
        }

        #endregion


    }
}

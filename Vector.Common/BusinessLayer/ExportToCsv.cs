using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Vector.Common.BusinessLayer
{
    public class ExportToCsv : DisposeLogic
    {
        #region Constants
        static readonly object _object = new object();
        //Numbers
        private int asyncCSVWriteCounter = 0;
        private const int One = 1;
        private const int Two = 2;
        private const int Zero = 0;
        private const int maxLength = 50;
        private const string DirectoryPath = "~/ReportFiles";
        private const string CSVExtension = ".csv";
        private const string ZipExtention = ".zip";
        private const string FilderSeparator = "\\";
        private const string UnderScore = "_";
        private const string BackSlash = "/";
        private const string Comma = ",";
        private const string Dot = ".";
        private const string CSVContentType = "text/csv";
        private const string ZipContentType = "application/zip";
        private const string ResponseHeaderName = "Content-Disposition";
        private const string ResponseHeaderValue = "attachment;Filename=\"{0}\"";
        private const string Empty = "";
        private const string Space = " ";
        private const string ShortageOfDimensions = "File has dimension values mismatch actual dimensions";
        private const string JavascriptMessageFormat = "{0}\\n";
        private const string ExceededMaxLength = "Data in following fields exceeded maximum length({0}) \\n";
        private const string EmptyFields = "Data in following fields is invalid/empty({0}) \\n";
        private const string Column1 = "Column1";
        private const string ZipTempPath = "~/ReportFiles/Download/ZipTemp/";
        private const string ZipEmailTempPath = "~/ReportFiles/Email/ZipTemp/";
        private const string ZipTemp = "ZipTemp";
        private const string CSVFolderName = "CSV Files";
        private const string FileName = "filename=";
        private const string CSVHeaderInfo = "Your ZIP file {0}contains the following files:{1}{1} ";
        private const string ReadMe = "README.txt";
        private const string DownLoad = "Download";
        private const string Email = "Email";

        #endregion

        #region enum

        private enum ReportFormat
        {
            csv,
            pdf,
            rtf
        }

        #endregion

        #region CSV File Genration

        /// <summary>
        /// Generate CSV File with data table asynchronously
        /// </summary>
        /// <param name="dataCsv"></param>
        /// <param name="filePath"></param>
        /// <param name="export"></param>
        /// <param name="format"></param>
        /// <param name="columnFormat"></param>
        /// <param name="service"></param>
        /// <param name="delimiter"></param>
        /// <param name="removeUnusedColumns"></param>
        /// <param name="ColumnSeprationDelemiter"></param>
        /// <param name="isHeaderRequired"></param>
        /// <param name="isDoubleQuoteRequired"></param>
        /// <param name="isEncodingUtf8Required"></param>
        /// <param name="isAppendDataToExistingFile"></param>
        /// <param name="completedFileNamePath"></param>
        /// <param name="totalRequests"></param>
        /// <param name="deleteFile"></param>
        /// <returns></returns>
        public async Task GenerateCsvFileAsync(DataTable dataCsv, string filePath, bool export = false, bool format = false,
                                           NameValueCollection columnFormat = null, bool service = false, String delimiter = ",",
                                           bool removeUnusedColumns = false, string ColumnSeprationDelemiter = ",", bool isHeaderRequired = true,
                                           bool isDoubleQuoteRequired = true, bool isEncodingUtf8Required = false, bool isAppendDataToExistingFile = false,
                                           string completedFileNamePath = "", int totalRequests = 0, bool deleteFile = false)
        {
            await Task.Run(() =>
            {
                lock (_object)
                {
                    if (deleteFile)
                    {
                        if (File.Exists(filePath))
                            File.Delete(filePath);

                        return;
                    }

                    if (isAppendDataToExistingFile && !File.Exists(filePath))
                    {
                        return;
                    }

                    asyncCSVWriteCounter++;
                    bool isLastReq = (asyncCSVWriteCounter == totalRequests);
                    if (removeUnusedColumns)
                    {
                        if (columnFormat != null)
                        {
                            var unUsedCols = dataCsv.Columns.Cast<DataColumn>().Where(column => columnFormat.AllKeys.Contains(column.ColumnName)).Select(eachCol => eachCol.ColumnName).ToArray();
                            dataCsv = DataManager.DeleteColumn(dataCsv, unUsedCols);
                        }

                    }
                    if (format && columnFormat != null)
                        dataCsv = DataManager.FormatDataTable(dataCsv, columnFormat, false);

                    StringBuilder sb = new StringBuilder();

                    if (isHeaderRequired)
                        sb.AppendLine(DataManager.GetColumnNames(dataCsv, delimiter, isDoubleQuoteRequired));

                    // get data table data separated by comma
                    if (service)
                    {
                        if (isAppendDataToExistingFile)
                            File.AppendAllText((filePath), DataManager.ConvertDataToCsv(dataCsv, sb, ColumnSeprationDelemiter, isDoubleQuoteRequired));
                        else
                            File.WriteAllText((filePath), DataManager.ConvertDataToCsv(dataCsv, sb, ColumnSeprationDelemiter, isDoubleQuoteRequired));

                    }

                    else
                        try
                        {
                            if (isAppendDataToExistingFile)
                            {
                                if (filePath.StartsWith(VectorConstants.Tild.ToString()))
                                    File.AppendAllText(HttpContext.Current.Server.MapPath(filePath), DataManager.ConvertDataToCsv(dataCsv, sb, ColumnSeprationDelemiter, isDoubleQuoteRequired));
                                else
                                    File.AppendAllText(filePath, DataManager.ConvertDataToCsv(dataCsv, sb, ColumnSeprationDelemiter, isDoubleQuoteRequired));
                            }
                            else
                            {
                                if (filePath.StartsWith(VectorConstants.Tild.ToString()))
                                    File.WriteAllText(HttpContext.Current.Server.MapPath(filePath), DataManager.ConvertDataToCsv(dataCsv, sb, ColumnSeprationDelemiter, isDoubleQuoteRequired));
                                else
                                    File.WriteAllText(filePath, DataManager.ConvertDataToCsv(dataCsv, sb, ColumnSeprationDelemiter, isDoubleQuoteRequired));
                            }

                            if (isLastReq && !string.IsNullOrEmpty(completedFileNamePath))
                            {
                                File.Move(filePath, completedFileNamePath);
                                File.Delete(filePath);
                            }

                        }
                        catch (Exception ex)
                        {

                        }

                    //transmit File
                    if (export)
                        TransmitCsvFile(filePath, isEncodingUtf8Required);
                }
            });
        }

        public static byte[] GenerateCsvFile(DataSet dataCsv, string filePath, bool export = false, bool format = false,
                                           NameValueCollection columnFormat = null, bool service = false, String delimiter = ",",
                                           bool removeUnusedColumns = false, string ColumnSeprationDelemiter = ",", bool isHeaderRequired = true,
                                           bool isDoubleQuoteRequired = true, bool isEncodingUtf8Required = false)
        {
            StringBuilder sbCSVData = new StringBuilder();

            for (int i = 0; i < dataCsv.Tables.Count; i++)
            {
                StringBuilder sb = new StringBuilder();
                DataTable dtData = new DataTable();
                dtData = dataCsv.Tables[i].Copy();
                if (removeUnusedColumns)
                {
                    if (columnFormat != null)
                    {
                        var unUsedCols = dtData.Columns.Cast<DataColumn>().Where(column => columnFormat.AllKeys.Contains(column.ColumnName)).Select(eachCol => eachCol.ColumnName).ToArray();
                        dtData = DataManager.DeleteColumn(dtData, unUsedCols);
                    }

                }
                if (format && columnFormat != null)
                    dtData = DataManager.FormatDataTable(dtData, columnFormat, false);
                
                if (isHeaderRequired)
                    sb.AppendLine(DataManager.GetColumnNames(dtData, delimiter, isDoubleQuoteRequired));

                sbCSVData.AppendLine(DataManager.ConvertDataToCsv(dtData, sb, ColumnSeprationDelemiter, isDoubleQuoteRequired));
            }



            // get data table data separated by comma
            if (service)
                File.WriteAllText((filePath), RemoveSpace(sbCSVData.ToString()));
            else
                try
                {
                    if (filePath.StartsWith(VectorConstants.Tild.ToString()))
                        File.WriteAllText(HttpContext.Current.Server.MapPath(filePath), RemoveSpace(sbCSVData.ToString()));
                    else
                        File.WriteAllText(filePath, RemoveSpace(sbCSVData.ToString()));
                }
                catch (Exception ex)
                {

                }


            byte[] bytes = null;
            try
            {
                bytes = System.IO.File.ReadAllBytes(filePath);
            }
            catch (Exception ex)
            {

            }
            return bytes;
        }

        public static byte[] GenerateCsvFile(DataTable dataCsv, string filePath, bool export = false, bool format = false,
                                          NameValueCollection columnFormat = null, bool service = false, String delimiter = ",",
                                          bool removeUnusedColumns = false, string ColumnSeprationDelemiter = ",", bool isHeaderRequired = true,
                                          bool isDoubleQuoteRequired = true, bool isEncodingUtf8Required = false)
        {
            if (removeUnusedColumns)
            {
                if (columnFormat != null)
                {
                    var unUsedCols = dataCsv.Columns.Cast<DataColumn>().Where(column => columnFormat.AllKeys.Contains(column.ColumnName)).Select(eachCol => eachCol.ColumnName).ToArray();
                    dataCsv = DataManager.DeleteColumn(dataCsv, unUsedCols);
                }

            }
            if (format && columnFormat != null)
                dataCsv = DataManager.FormatDataTable(dataCsv, columnFormat, false);

            StringBuilder sb = new StringBuilder();

            if (isHeaderRequired)
                sb.AppendLine(DataManager.GetColumnNames(dataCsv, delimiter, isDoubleQuoteRequired));

            // get data table data separated by comma
            if (service)
                File.WriteAllText((filePath), DataManager.ConvertDataToCsv(dataCsv, sb, ColumnSeprationDelemiter, isDoubleQuoteRequired));
            else
                try
                {
                    if (filePath.StartsWith(VectorConstants.Tild.ToString()))
                        File.WriteAllText(HttpContext.Current.Server.MapPath(filePath), DataManager.ConvertDataToCsv(dataCsv, sb, ColumnSeprationDelemiter, isDoubleQuoteRequired));
                    else
                        File.WriteAllText(filePath, DataManager.ConvertDataToCsv(dataCsv, sb, ColumnSeprationDelemiter, isDoubleQuoteRequired));
                }
                catch (Exception ex)
                {

                }

            //transmit File
            if (export)
                TransmitCsvFile(filePath, isEncodingUtf8Required);

            byte[] bytes = null;
            try
            {
                bytes = System.IO.File.ReadAllBytes(filePath);
            }
            catch (Exception ex)
            {

            }
            return bytes;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="filePath"></param>
        /// <param name="export"></param>
        /// <param name="format"></param>
        /// <param name="columnFormat"></param>
        /// <param name="delimiter"></param>
        /// <param name="removeUnusedColumns"></param>
        /// <param name="ColumnSeprationDelemiter"></param>
        /// <param name="isHeaderRequired"></param>
        /// <param name="isDoubleQuoteRequired"></param>
        /// <param name="isEncodingUtf8Required"></param>
        /// <param name="rowEndDelimeter"></param>
        public static void GenerateDatFile(DataTable data, string filePath, bool export = false, bool format = false,
                                          NameValueCollection columnFormat = null, String delimiter = ",",
                                          bool removeUnusedColumns = false, string ColumnSeprationDelemiter = ",", bool isHeaderRequired = true,
                                          bool isDoubleQuoteRequired = true, bool isEncodingUtf8Required = false, string rowEndDelimeter = "")
        {
            if (removeUnusedColumns)
            {
                if (columnFormat != null)
                {
                    var unUsedCols = data.Columns.Cast<DataColumn>().Where(column => columnFormat.AllKeys.Contains(column.ColumnName)).Select(eachCol => eachCol.ColumnName).ToArray();
                    data = DataManager.DeleteColumn(data, unUsedCols);
                }

            }
            if (format && columnFormat != null)
                data = DataManager.FormatDataTable(data, columnFormat, false);

            StringBuilder sb = new StringBuilder();

            if (isHeaderRequired)
                sb.AppendLine(DataManager.GetColumnNames(data, delimiter, isDoubleQuoteRequired));

            try
            {
                if (filePath.StartsWith(VectorConstants.Tild.ToString()))
                    File.WriteAllText(HttpContext.Current.Server.MapPath(filePath), DataManager.ConvertDataToCsv(data, sb, ColumnSeprationDelemiter, isDoubleQuoteRequired, rowEndDelimeter: rowEndDelimeter));
                else
                    File.WriteAllText(filePath, DataManager.ConvertDataToCsv(data, sb, ColumnSeprationDelemiter, isDoubleQuoteRequired, rowEndDelimeter: rowEndDelimeter));
            }
            catch (Exception ex)
            {

            }
        }

        private static string RemoveSpace(string st)
        {
            String final = "";

            char[] b = new char[] { '\r', '\n' };
            String[] lines = st.Split(b, StringSplitOptions.RemoveEmptyEntries);
            foreach (String s in lines)
            {
                if (!String.IsNullOrWhiteSpace(s))
                {
                    final += s;
                    final += Environment.NewLine;
                }
            }

            return final;

        }

        /// <summary>
        /// Generate CSV File with data set
        /// </summary>
        /// <param name="dataCsv"></param>
        /// <param name="filePath"></param>
        /// <param name="export"></param>
        /// <param name="format"></param>
        /// <param name="columnFormat"></param>
        public static void GenerateCsvFile(DataSet dataCsv, string filePath, bool export = false, bool format = false, NameValueCollection columnFormat = null, bool service = false)
        {
            if (!DataManager.IsNullOrEmptyDataSet(dataCsv))
                GenerateCsvFile(dataCsv.Tables[0], filePath, export, format, columnFormat, service);

            #region "Commented Code For GenerateCSVFile"

            //public void GenerateCSVFile(DataSet dataCSV, string filePath, bool export = false, bool format = false, NameValueCollection columnFormat = null)
            //{
            //    CreateDirectory(filePath);

            //    string zipTempPath = ZipTempPath;

            //    if (format && columnFormat != null)
            //        dataCSV = CommonUtility.FormatDataSet(dataCSV, columnFormat);

            //    StringBuilder sb = new StringBuilder();
            //    int i = 0;
            //    string str = filePath.Replace(ProFramework.SessionMgr.ClientCode.ToString(), ZipTemp);
            //    foreach (DataTable table in dataCSV.Tables)
            //    {
            //        //Generates new CSV files
            //        sb = new StringBuilder();
            //        sb.AppendLine(CommonUtility.GetColumnNames(table, Comma));
            //        File.WriteAllText(HttpContext.Current.Server.MapPath(str.Replace(ZipExtention, Empty + i.ToString() + CSVExtension)), CommonUtility.ConvertDataToCSV(table, sb));
            //        i++;
            //    }

            //    string[] fileList = null;

            //    //Export to ZIP
            //    if (filePath.Contains(DownLoad))
            //    {
            //        DownloadZipFiles(zipTempPath, filePath, export: export);
            //        fileList = Directory.GetFiles(HttpContext.Current.Server.MapPath(zipTempPath));
            //    }
            //    else if (filePath.Contains(Email))
            //    {
            //        DownloadZipFiles(ZipEmailTempPath, filePath, export: export);
            //        fileList = Directory.GetFiles(HttpContext.Current.Server.MapPath(ZipEmailTempPath));
            //    }

            //    //Delete files from the folder 
            //    foreach (string fileName in fileList) { File.Delete(fileName); }
            //}

            #endregion

        }


        /// <summary>
        /// Creates directory if doesn't exist
        /// </summary>
        /// <param name="filePath"></param>
        public static void CreateDirectory(string filePath)
        {
            if (filePath.Contains(DownLoad))
            {
                if (!Directory.Exists(HttpContext.Current.Server.MapPath(ZipTempPath)))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(ZipTempPath));
                }
            }
            else if (filePath.Contains(Email))
            {
                if (!Directory.Exists(HttpContext.Current.Server.MapPath(ZipEmailTempPath)))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(ZipEmailTempPath));
                }
            }
        }

        /// <summary>
        /// Downloads CSV files in the format of ZIP
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="saveFolderPath"></param>
        /// <param name="pswd"></param>
        /// <param name="folderNameToSave"></param>
        public static void DownloadZipFiles(string folderPath, string saveFolderPath, string folderNameToSave = null, bool export = false)
        {
            var response = HttpContext.Current.Response;
            string saveFolder = CSVFolderName;

            if (!string.IsNullOrEmpty(folderNameToSave))
                saveFolder = folderNameToSave;

            response.ContentType = ZipContentType;
            response.AddHeader(ResponseHeaderName, FileName + Path.GetFileName(saveFolderPath));
            // Zip the contents of the selected files
            using (var zip = new ZipFile())
            {
                // Construct the contents of the README.txt file that will be included in this ZIP
                var readMeMessage = string.Format(CultureInfo.CurrentCulture, CSVHeaderInfo, string.Empty, Environment.NewLine);

                // Add the checked files to the ZIP
                // Process the list of files found in the directory. 
                string[] fileEntries = Directory.GetFiles(HttpContext.Current.Server.MapPath(folderPath));
                foreach (string fileName in fileEntries)
                {
                    // Record the file that was included in readMeMessage
                    readMeMessage += string.Concat("\t* ", Path.GetFileName(fileName), Environment.NewLine);

                    // Now add the file to the ZIP (use a value of "" as the second parameter to put the files in the "root" folder)
                    zip.AddFile(fileName, saveFolder);
                }

                // Add the README.txt file to the ZIP
                zip.AddEntry(ReadMe, readMeMessage, Encoding.ASCII);

                var saveToFilename = HttpContext.Current.Server.MapPath(saveFolderPath);
                zip.Save(saveToFilename);

                if (export)
                    TransmitZipFile(saveToFilename);
            }
        }

        /// <summary>
        /// Writing csv file to given path 
        /// </summary>
        /// <param name="filePath"></param>
        private static void TransmitCsvFile(string filePath, bool isEncodingUtf8Required = false)
        {
            var response = HttpContext.Current.Response;

            response.ClearHeaders();
            response.ContentType = CSVContentType;
            response.Clear();
            response.AppendHeader(ResponseHeaderName, (string.Format(CultureInfo.CurrentCulture, ResponseHeaderValue, Path.GetFileName(filePath))));
            if (isEncodingUtf8Required)
            {
                response.ContentEncoding = Encoding.UTF8;
                response.BinaryWrite(Encoding.UTF8.GetPreamble());
            }
            //response.AddHeader(ResponseHeaderName, "attachment; filename=\"" + Path.GetFileName(filePath) + "\"");
            response.TransmitFile(filePath);
            if (File.Exists(filePath))
                FileManager.DeleteFileAsynchronous(filePath);
            response.Flush();
            response.End();
        }

        /// <summary>
        /// Writing zip file to given path 
        /// </summary>
        /// <param name="filePath"></param>
        public static void TransmitZipFile(string filePath)
        {
            var response = HttpContext.Current.Response;
            response.ClearHeaders();
            response.ContentType = CSVContentType;
            response.Clear();
            response.AppendHeader(ResponseHeaderName, (string.Format(CultureInfo.CurrentCulture, ResponseHeaderValue, Path.GetFileName(filePath))));
            response.TransmitFile(filePath);
            response.Flush();
            response.End();
        }

        #endregion
    }
}


namespace Vector.Common.BusinessLayer
{
    using ICSharpCode.SharpZipLib.Zip;
    using System;
    using System.Collections;
    using System.Configuration;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Hosting;
    using System.Web.UI.WebControls;
    using static Vector.Common.BusinessLayer.VectorEnums;

    public static class FileManager
    {
        #region Constants

        private const string InvalidFileCharacters = @"\/:*?<>| ";
        private const string ZipTempPath = "~/ReportFiles/Download/ZipTemp/";
        private const string ForwardSlash = "\\";
        private const string ZipContentType = "application/zip";
        private const string ResponseHeaderValue = "attachment;Filename=\"{0}\"";
        #endregion

        #region Enum

        #endregion

        #region File

        #region File Meothods

        /// <summary>
        /// Gets the Folder Path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetFilePath(string path, bool isRawPathRequired = false)
        {
            if (path.StartsWith(VectorConstants.Tild.ToString()))
                return HostingEnvironment.MapPath(path);

            if (isRawPathRequired)
                return path;
            else
                return SecurityManager.GetConfigValue("FileServerPath") + path;
        }

        /// <summary>
        /// Here files will be uploaded asynchronously
        /// </summary>
        /// <param name="request">File details</param>
        /// <param name="folderPath">Folder path</param>
        /// <param name="fileNameWithExtn">File name with extension</param>
        /// <param name="userId">User Id</param>
        /// <returns></returns>
        public async static Task<string> UploadFiles(HttpRequestMessage request, string folderPath = "", string fileNameWithExtn = "",
                                                    string userId = "", bool isFullPath = false)
        {
            string fileName = string.Empty;
            string filePath = string.Empty;

            var uploadPath = GetFilePath(String.IsNullOrEmpty(folderPath) ? "UploadedFiles" : folderPath, isFullPath);
            // HttpContext.Current.Server.MapPath(String.IsNullOrEmpty(folderPath) ? UploadedFiles : folderPath);
            bool existsFolder = Directory.Exists(uploadPath);
            if (!existsFolder)
                Directory.CreateDirectory(uploadPath);

            if (!request.Content.IsMimeMultipartContent())
            {
                string task = request.CreateResponse(HttpStatusCode.UnsupportedMediaType).ToString();
                return task;
            }
            try
            {
                var streamProvider = new MultipartFormDataStreamProvider(uploadPath);
                Stream reqStream = request.Content.ReadAsStreamAsync().Result;
                MemoryStream tempStream = new MemoryStream();
                reqStream.CopyTo(tempStream);

                tempStream.Seek(0, SeekOrigin.End);
                StreamWriter writer = new StreamWriter(tempStream);
                writer.WriteLine();
                writer.Flush();
                tempStream.Position = 0;

                StreamContent streamContent = new StreamContent(tempStream);
                foreach (var header in request.Content.Headers)
                {
                    streamContent.Headers.Add(header.Key, header.Value);
                }

                // Read the form data and return an async task.
                await streamContent.ReadAsMultipartAsync(streamProvider);

                //Saving with the file name
                foreach (MultipartFileData fileData in streamProvider.FileData)
                {
                    if (string.IsNullOrEmpty(fileData.Headers.ContentDisposition.FileName))
                    {
                        return request.CreateResponse(HttpStatusCode.NotAcceptable).ToString();
                    }

                    fileName = fileData.Headers.ContentDisposition.FileName;

                    if (fileName.StartsWith(VectorConstants.FrontSlashTrim) && fileName.EndsWith(VectorConstants.FrontSlashTrim))
                    {
                        fileName = fileName.Trim(VectorConstants.DoubleQuotes);
                    }
                    if (fileName.Contains(VectorConstants.ContainsAtRateFront) || fileName.Contains(VectorConstants.ContainsAtRateEnd))
                    {
                        fileName = Path.GetFileName(fileName);
                    }

                    File.Move(fileData.LocalFileName, Path.Combine(uploadPath, String.IsNullOrEmpty(fileNameWithExtn) ? fileName : fileNameWithExtn));
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.ToString().TrimEnd() == VectorConstants.FileExistExceptionMessage)
                {
                    return VectorConstants.FileExist;
                }
                return request.CreateResponse(HttpStatusCode.InternalServerError).ToString();
            }

            filePath = Path.GetFullPath(uploadPath + VectorConstants.AppendSlash + (String.IsNullOrEmpty(fileNameWithExtn) ? fileName : fileNameWithExtn));
            return filePath;
        }

        public static bool ImageExists(string name, HttpContext http, string path)
        {
            string dir = string.Empty;
            dir = path;
            DirectoryInfo objDF = new DirectoryInfo(dir);
            if (objDF.Exists)
            {
                if (File.Exists(dir + "\\" + name))
                {
                    http.Response.ClearHeaders();
                    http.Response.Clear();
                    http.Response.AppendHeader("Content-Disposition", ("attachment;Filename=" + (name)));
                    http.Response.TransmitFile(dir + "\\" + name);
                    http.Response.End();
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        public static void DeleteFileAsynchronous(string path)
        {
            AsynchronousDelete deleteFunction = new AsynchronousDelete(DeleteFile);
            deleteFunction.BeginInvoke(path, VectorConstants.Zero, new AsyncCallback(GetResultByCallback), null);
        }

        public static void DeleteFile(string path, int count)
        {
            if (!string.IsNullOrEmpty(path))
            {
                if (File.Exists(path))
                {
                    if (count < VectorConstants.Five)
                    {
                        if (!IsFileInUse(path))
                            File.Delete(path);
                        else
                        {
                            System.Threading.Thread.Sleep(1000);
                            DeleteFile(path, count++);
                        }
                    }
                }
            }

        }

        public static bool DeleteFile(string fileName, string filePath, bool isDeleteFodler = true)
        {
            //filePath = GetFilePath(filePath);
            string fileNameWithPath = HttpContext.Current.Server.MapPath(filePath + fileName);

            if (!string.IsNullOrEmpty(fileNameWithPath))
            {
                if ((File.Exists(fileNameWithPath)))
                {
                    File.Delete(fileNameWithPath);
                }
            }
            return true;
        }

        public static bool MoveFiles(string tempFolderPath, string parentFolderPath,bool isMapPath = false,bool isDeleteFodler = true)
        {
            try
            {
                if (isMapPath) { 
                    tempFolderPath = HttpContext.Current.Server.MapPath(tempFolderPath);
                parentFolderPath = HttpContext.Current.Server.MapPath(parentFolderPath);
                     }
                if (!Directory.Exists(parentFolderPath)) {
                    CreateDirectory(parentFolderPath);
                  }
                if (Directory.Exists(tempFolderPath) && Directory.Exists(parentFolderPath))
                {
                    foreach (var file in Directory.GetFiles(tempFolderPath))
                    {
                        // File.Copy(file, Path.Combine(parentFolderPath, Path.GetFileName(file)),1);

                        if(File.Exists(Path.Combine(parentFolderPath, Path.GetFileName(file))))
                        {
                            if (!Directory.Exists(Path.Combine(parentFolderPath + "/BackUp/")))
                            {
                                CreateDirectory(Path.Combine(parentFolderPath + "/BackUp/"));
                            }

                            File.Move(Path.Combine(parentFolderPath, Path.GetFileName(file)), Path.Combine(parentFolderPath + "/BackUp/", Path.GetFileName(file)));
                        }

                          File.Move(file, Path.Combine(parentFolderPath, Path.GetFileName(file)));  
                    }
                        


                    if(isDeleteFodler)
                    if (Directory.Exists(tempFolderPath))
                        Directory.Delete(tempFolderPath, true);

                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void DeleteFolderAsynchronous(string path)
        {
            AsynchronousDelete deleteFunction = new AsynchronousDelete(DeleteFolder);
            deleteFunction.BeginInvoke(path, VectorConstants.Zero, new AsyncCallback(GetResultByCallback), null);
        }

        public static void DeleteFolder(string path, int count)
        {
            if (!string.IsNullOrEmpty(path))
            {
                System.Threading.Thread.Sleep(2000);

                if (Directory.Exists(path))
                {
                    //Directory.GetAccessControl(path, System.Security.AccessControl.AccessControlSections.All);
                    Directory.Delete(path, true);

                }
            }

        }

        public static void GetResultByCallback(IAsyncResult asyncResult)
        {
        }

        private static bool IsFileInUse(string fileFullPath)
        {
            //    try
            //    {
            //if this does not throw exception then the file is not use by another program             
            using (FileStream fileStream = File.OpenWrite(fileFullPath))
            {
                if (fileStream == null)
                    return true;
            }
            return false;
            //}
            //catch
            //{
            //    return true;
            //}
        }

        /// <summary>
        /// Uploading the image to folder entered by Partner and client 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="fileName"></param>
        /// <param name="serverPath"></param>
        public static void UploadImage(FileUpload file, string fileName, string serverPath)
        {
            // Set constant values
            if (!file.Equals(null))
            {
                file.SaveAs(serverPath + fileName);
            }

        }

        public static void RenameFile(string oldFileFullpath, string newFileFullpath)
        {
            if (File.Exists(oldFileFullpath))
            {
                File.Move(oldFileFullpath, newFileFullpath);
                File.Delete(oldFileFullpath);
            }
        }

        /// <summary>
        /// Returns file names from given folder that comply to given filters
        /// </summary>
        /// <param name="sourceFolder">Folder with files to retrieve</param>
        /// <param name="filter">Multiple file filters separated by | character *.aspx | *.ascx | *.html </param>
        /// <param name="searchOption">File.IO.SearchOption, 
        /// could be AllDirectories or TopDirectoryOnly</param>
        /// <returns>Array of FileInfo objects that presents collection of file names that 
        /// meet given filter</returns>
        public static string[] GetFiles(string sourceFolder, string filter, SearchOption searchOption)
        {
            // ArrayList will hold all file names
            ArrayList alFiles = new ArrayList();

            // Create an array of filter string
            string[] MultipleFilters = filter.Split('|');

            // for each filter find mathing file names
            foreach (string FileFilter in MultipleFilters)
            {
                // add found file names to array list
                alFiles.AddRange(Directory.GetFiles(sourceFolder, FileFilter, searchOption));
            }

            // returns string array of relevant file names
            return (string[])alFiles.ToArray(typeof(string));
        }


        #endregion

        #region Directory

        /// <summary>
        /// Create New Directory
        /// </summary>
        /// <param name="DirPath"></param>
        public static void CreateDirectory(string DirPath)
        {
            DirectoryInfo objDF = new DirectoryInfo(DirPath);
            if (!objDF.Exists)
                objDF.Create();
            objDF = null;
        }

        #endregion

        #region File validations

        /// <summary>
        /// check for valid file url
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsFileUri(string path)
        {
            path.StartsWith("FILE:", StringComparison.OrdinalIgnoreCase);
            return true;
        }


        /// <summary>
        /// Remove Special Chars from string
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string ConvertToValidFileName(string fileName)
        {
            return string.Join(string.Empty, fileName.Split(InvalidFileCharacters.ToCharArray()));
        }

        public static bool IsFileExists(string dictionary)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(dictionary))
            {
                if (Directory.Exists(dictionary))
                {
                    if (File.Exists(dictionary))
                        result = true;
                }
                else
                    result = true;

            }
            else
                result = false;

            return result;
        }

        /// <summary>
        /// Check file control has file and content length
        /// </summary>
        /// <param name="fuFile"></param>
        /// <returns></returns>
        public static bool IsFileValid(FileUpload fuFile)
        {
            if (fuFile.HasFile)
            {
                if (fuFile.FileContent.Length > VectorConstants.Zero)
                {
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// Delete file for the given folder 
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="fileExtension"></param>
        public static void DeleteFiles(string folderPath, string fileExtension)
        {
            //get all *.png files and Delete them
            foreach (string strFile in System.IO.Directory.GetFiles(folderPath, fileExtension))
            {
                System.IO.File.Delete(strFile);
            }

        }

        /// <summary>
        /// validates the File extensions
        /// </summary>
        /// <param name="fuFile">FileUpload object</param>
        /// <param name="extensions">String array of accepted extensions</param>
        /// <returns></returns>
        public static bool IsValidFileExtension(FileUpload fuFile, string[] extensions)
        {
            if (IsFileValid(fuFile))
            {
                string FileEx = Path.GetExtension(fuFile.FileName).Replace(VectorConstants.Dot, string.Empty);
                return extensions.Any(ext => (StringManager.IsEqual(ext, FileEx)));
            }
            return false;
        }

        public static bool IsValidFileExtension(FileUpload fuFile, string extension)
        {
            if (FileManager.IsFileValid(fuFile))
            {
                string FileEx = Path.GetExtension(fuFile.FileName).Replace(VectorConstants.Dot, string.Empty);
                if (StringManager.IsEqual(FileEx.ToUpper(CultureInfo.InvariantCulture).Trim(), extension.ToUpper(CultureInfo.InvariantCulture).Trim()))
                    return true;
            }
            return false;
        }


        #endregion

        #region Generate File Path with File Name


        public static string GenerateFilePathForWindowsService(string filePath, string clientCode, string title, string extension)
        {
            string reportName = StringManager.RemoveSpecialCharacters(title);
            filePath = CheckPathExistsForWindowsService(filePath, reportName, clientCode, extension);
            return filePath;
        }

        public static string CreateSaveFile(string filePath, string fileName = null, bool slashIncluded = false)
        {

            if (!string.IsNullOrEmpty(fileName))
            {
                if (!slashIncluded)
                    //Check Uploads folder exists if not then create a new folder
                    filePath = filePath + ForwardSlash + fileName;
                else

                    filePath = filePath + fileName;
            }

            return filePath;
        }


        /// <summary>
        /// create Folder with ClientCode and Generate report Path.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="reportName"></param>
        /// <returns></returns>
        public static string CheckPathExists(string path, string reportName, string clientCode, string extension)
        {
            string downloadPath = path + clientCode;
            //check if folder exists 
            bool IsExists = Directory.Exists(HttpContext.Current.Server.MapPath(downloadPath));

            //if not exists create folder and send path
            if (!IsExists)
                System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath(downloadPath));

            //append The Folder path with FielName
            string filePath = downloadPath + VectorConstants.Backslash + reportName + VectorConstants.Underscore + DateManager.GetTimestamp(DateTime.Now) + "." + extension;
            return filePath;
        }

        /// <summary>
        /// create Folder with ClientCode and Generate report Path.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="reportName"></param>
        /// <returns></returns>
        public static string CheckPathExistsForWindowsService(string path, string reportName, string clientCode, string extension)
        {
            string downloadPath = path + clientCode;
            //check if folder exists 
            bool IsExists = Directory.Exists((downloadPath));

            //if not exists create folder and send path
            if (!IsExists)
                System.IO.Directory.CreateDirectory((downloadPath));

            //append The Folder path with FielName
            string filePath = downloadPath + VectorConstants.Backslash + reportName + VectorConstants.Underscore + DateManager.GetTimestamp(DateTime.Now) + "." + extension;
            return filePath;
        }

        public static string GenerateFileName(FileUpload file)
        {
            return DateManager.GetTimestamp(DateTime.Now) + VectorConstants.Underscore + file.FileName;
        }

        #endregion

        #region Generate File Path

        public static string GenerateReportFilePath(string downloadPathKey, string ext, string fileNameKey = "Report")
        {
            int sno = 1;
            StringBuilder filePathBuilder = new StringBuilder(downloadPathKey);
            filePathBuilder.Append("//");
            filePathBuilder.Append(fileNameKey);
            filePathBuilder.Append("_");
            filePathBuilder.Append(DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace("AM", "").Replace("PM", "").Replace(" ", ""));
            filePathBuilder.Append("_");
            while (File.Exists(filePathBuilder.ToString() + sno + "." + ext))
            {
                sno++;
            }
            filePathBuilder.Append(sno);
            filePathBuilder.Append(".");
            filePathBuilder.Append(ext);
            return filePathBuilder.ToString();
        }

        public static string GenerateReportFileVirtualPath(string downloadPathKey, string ext, string fileNameKey = "Report")
        {
            int sno = 1;
            StringBuilder filePathBuilder = new StringBuilder(HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings[downloadPathKey].ToString()));
            StringBuilder reportName = new StringBuilder("//");
            reportName.Append(fileNameKey);
            reportName.Append("_");
            reportName.Append(DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace("AM", "").Replace("PM", "").Replace(" ", ""));
            reportName.Append("_");
            while (File.Exists(filePathBuilder.ToString() + reportName + sno + "." + ext))
            {
                sno++;
            }
            reportName.Append(sno);
            reportName.Append(".");
            reportName.Append(ext);
            return ConfigurationManager.AppSettings[downloadPathKey].ToString() + reportName.ToString();
        }

        #endregion

        #region Delegates

        private delegate void AsynchronousDelete(string path, int count);

        #endregion

        #region Zip

        /// <summary>
        /// Zip Multiple files
        /// </summary>
        /// <param name="zipFile"></param>
        /// <param name="filePath"></param>
        public static void Zip(string zipFile, FileInfo[] filePath)
        {
            ZipOutputStream zipOut = new ZipOutputStream(File.Create(zipFile));
            foreach (FileInfo strFilName in filePath)
            {
                if (strFilName != null)
                {
                    ZipEntry entry = new ZipEntry(strFilName.Name);
                    FileStream sReader = File.OpenRead(strFilName.FullName);
                    byte[] buff = new byte[Convert.ToInt32(sReader.Length)];
                    sReader.Read(buff, 0, (int)sReader.Length);
                    entry.DateTime = strFilName.LastWriteTime;
                    entry.Size = sReader.Length;
                    sReader.Close();
                    zipOut.PutNextEntry(entry);
                    zipOut.Write(buff, 0, buff.Length);
                }
            }
            zipOut.CloseEntry();
            zipOut.Finish();
            zipOut.Close();

        }

        /// <summary>
        /// Will delete all the Files in the Given folder path
        /// </summary>
        public static void DeleteZipFileInZipFolder(string tempFolder = ZipTempPath)
        {
            string[] fileList = Directory.GetFiles(HttpContext.Current.Server.MapPath(tempFolder));
            foreach (string fileName in fileList) { File.Delete(fileName); }
        }

        /// <summary>
        /// Writing zip file to given path 
        /// </summary>
        /// <param name="filePath"></param>
        public static void TransmitZipFile(string filePath)
        {
            var response = HttpContext.Current.Response;
            response.ClearHeaders();
            response.ContentType = ZipContentType;
            response.Clear();
            response.AppendHeader("Content-Disposition", (string.Format(CultureInfo.CurrentCulture, ResponseHeaderValue, Path.GetFileName(filePath))));
            response.TransmitFile(filePath);
            response.Flush();
            response.End();
        }

        public static string CleanupFileName(string fileName)
        {
            //Special Characters Not Allowed: ~ " # % & * : < > ? / \ { | }      
            if (!string.IsNullOrEmpty(fileName))
            {
                //Reg-ex to Replace the Special Character
                fileName = Regex.Replace(fileName, @"[~#'%&*:<>?/\{|}\n]", "");

                if (fileName.Contains("\""))
                {
                    fileName = fileName.Replace("\"", "");
                }

                if (fileName.StartsWith(".", StringComparison.OrdinalIgnoreCase) || fileName.EndsWith(".", StringComparison.OrdinalIgnoreCase))
                {
                    fileName = fileName.TrimStart(new char[] { '.' });
                }
                if (fileName.IndexOf("..", StringComparison.OrdinalIgnoreCase) > -1)
                {
                    fileName = fileName.Replace("..", "");
                }
                fileName = fileName.Replace("/n", string.Empty);
            }
            return fileName;
        }

        public static string ZipFolder(string zipFileName, string sourceFile)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceFile);
            FileInfo[] flist = dir.GetFiles();
            ZipOutputStream zipOut = new ZipOutputStream(File.Create(zipFileName));
            foreach (FileInfo strFilName in flist)
            {
                ZipEntry entry = new ZipEntry(strFilName.Name);
                FileStream sReader = File.OpenRead(strFilName.FullName);
                byte[] buff = new byte[Convert.ToInt32(sReader.Length)];
                sReader.Read(buff, 0, (int)sReader.Length);
                entry.DateTime = strFilName.LastWriteTime;
                entry.Size = sReader.Length;
                sReader.Close();
                zipOut.PutNextEntry(entry);
                zipOut.Write(buff, 0, buff.Length);

            }
            if (flist.Length > 0)
            {
                zipOut.CloseEntry();
            }

            zipOut.Finish();
            zipOut.Close();
            return zipFileName;
        }

        #endregion

        #region Report Generator

        public static string GetFolderPathForReportGenerator(string timeStamp, string userID, string reportName, string clientCode, string fileExtension)
        {
            string folderPath = (GetApplicationPathForWindows(ConfigValue.FileDirectory.ToString()) +
                SecurityManager.GetConfigValue(VectorConstants.DownloadPathKey) + clientCode +
                VectorConstants.DoubleSlash.ToString() + userID + VectorConstants.DoubleSlash.ToString() +
                timeStamp + VectorConstants.DoubleSlash.ToString() + StringManager.RemoveSpecialCharacters(reportName) +
                VectorConstants.DoubleSlash.ToString() + fileExtension.ToUpper() + VectorConstants.DoubleSlash.ToString()).Replace("/", "\\").ToString();

            //string folderPath = ("D:" + SecurityManager.GetConfigValue(VectorConstants.DownloadPathKey) + clientCode +
            //    VectorConstants.DoubleSlash.ToString() + userID + VectorConstants.DoubleSlash.ToString() +
            //    timeStamp + VectorConstants.DoubleSlash.ToString() + StringManager.RemoveSpecialCharacters(reportName) +
            //    VectorConstants.DoubleSlash.ToString() + fileExtension.ToUpper() + VectorConstants.DoubleSlash.ToString()).Replace("/", "\\").ToString();

            //if not exists create folder and send path
            if (!Directory.Exists(folderPath))
                System.IO.Directory.CreateDirectory(folderPath);

            return folderPath;
        }

        public static string GenerateReportFileName(string serialNumber, string period, string reportName, string clientCode,
                                                    string fromMonth, string fromYear, string toMonth, string toYear,
                                                    string reportBy, string fileExtension = "Pdf")
        {
            StringBuilder fileName = new StringBuilder();
            fileName.Append(serialNumber.ToString());
            fileName.Append(VectorConstants.Underscore);
            fileName.Append(period);
            fileName.Append(VectorConstants.Underscore);
            fileName.Append(StringManager.RemoveSpecialCharacters(reportName));
            fileName.Append(VectorConstants.Underscore);
            fileName.Append(clientCode);
            fileName.Append(VectorConstants.Underscore);
            fileName.Append(fromMonth);
            fileName.Append(VectorConstants.Underscore);
            fileName.Append(fromYear);
            fileName.Append(VectorConstants.Underscore);
            fileName.Append(toMonth);
            fileName.Append(VectorConstants.Underscore);
            fileName.Append(toYear);
            fileName.Append(VectorConstants.Underscore);
            fileName.Append(StringManager.RemoveSpecialCharacters(reportBy));
            fileName.Append(VectorConstants.Underscore);
            fileName.Append(DateManager.GetTimestamp(DateTime.Now));
            fileName.Append(VectorConstants.Dot);
            fileName.Append(fileExtension);
            return fileName.ToString();
        }

        public static string GenerateReportFileName(string serialNumber, string period, string reportName, string clientCode,
                                            string year, string reportBy, string fileExtension = "Pdf")
        {
            StringBuilder fileName = new StringBuilder();
            fileName.Append(serialNumber.ToString());
            fileName.Append(VectorConstants.Underscore);
            fileName.Append(period);
            fileName.Append(VectorConstants.Underscore);
            fileName.Append(StringManager.RemoveSpecialCharacters(reportName));
            fileName.Append(VectorConstants.Underscore);
            fileName.Append(clientCode);
            fileName.Append(VectorConstants.Underscore);
            fileName.Append(year);
            fileName.Append(VectorConstants.Underscore);
            fileName.Append(StringManager.RemoveSpecialCharacters(reportBy));
            fileName.Append(VectorConstants.Underscore);
            fileName.Append(DateManager.GetTimestamp(DateTime.Now));
            fileName.Append(VectorConstants.Dot);
            fileName.Append(fileExtension);
            return fileName.ToString();
        }

        public static string GetApplicationPathForWindows(string BaseDirectoryReplaceValue = "FileDirectory")
        {
            return AppDomain.CurrentDomain.BaseDirectory.Replace(SecurityManager.GetConfigValue(BaseDirectoryReplaceValue).ToString(), string.Empty).ToString();
        }

        public static string GetFolderPathForReportGeneratorCharts(string timeStamp, string userID, string reportName, string clientCode)
        {
            string folderPath = (GetApplicationPathForWindows(ConfigValue.FileDirectory.ToString()) +
                SecurityManager.GetConfigValue(VectorConstants.DownloadPathKey) + clientCode +
                VectorConstants.DoubleSlash.ToString() + userID + VectorConstants.DoubleSlash.ToString() +
                timeStamp + VectorConstants.DoubleSlash.ToString() + StringManager.RemoveSpecialCharacters(reportName) +
                VectorConstants.DoubleSlash.ToString() + "Charts" + VectorConstants.DoubleSlash.ToString()).Replace("/", "\\").ToString();

            //string folderPath = ("D:" + SecurityManager.GetConfigValue(VectorConstants.DownloadPathKey) + clientCode +
            //    VectorConstants.DoubleSlash.ToString() + userID + VectorConstants.DoubleSlash.ToString() +
            //    timeStamp + VectorConstants.DoubleSlash.ToString() + StringManager.RemoveSpecialCharacters(reportName) +
            //    VectorConstants.DoubleSlash.ToString() + "Charts" + VectorConstants.DoubleSlash.ToString()).Replace("/", "\\").ToString();

            //if not exists create folder and send path
            if (!Directory.Exists(folderPath))
                System.IO.Directory.CreateDirectory(folderPath);

            return folderPath;
        }

        public static string GenerateReportChartName(string reportName, string fileExtension = ".Jpeg")
        {
            StringBuilder chartName = new StringBuilder();
            chartName.Append(StringManager.RemoveSpecialCharacters(reportName));
            chartName.Append(VectorConstants.Underscore);
            chartName.Append(DateManager.GetTimestamp(DateTime.Now));
            chartName.Append(fileExtension);
            return chartName.ToString();
        }

        #endregion

        #region Download File

        /// <summary>
        /// Download file from any file server or web server to destination folder 
        /// </summary>
        /// <param name="soruceURL"> http://proimage.prokarmabpo.com/etest/SCANNEDBILLS/2013/H00001.tif </param>
        /// <param name="destinationFolder">C:\DestinationFolder\H00001.tif</param>
        public static void DownloadFiles(string[] soruceURL, string destinationFolder)
        {

            foreach (var file in soruceURL)
            {
                try
                {
                    using (WebClient client = new WebClient())
                    {
                        Uri uri = new Uri(file);
                        client.DownloadFile(uri, destinationFolder + Path.GetFileName(uri.OriginalString));
                    }
                }
                catch (Exception ex)
                {
                    continue;
                }
            }


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceFileNames"></param>
        /// <param name="strZipFilePath"></param>
        /// <param name="strZipFileName"></param>
        /// <returns></returns>
        public static string DownloadandZipImages(string[] sourceFileNames, string strZipFilePath, string strZipFileName)
        {
            sourceFileNames = StringManager.RemoveDuplicates(sourceFileNames);

            string DirPath = strZipFilePath;
            CreateDirectory(DirPath);
            DirPath += "\\";

            DownloadFiles(sourceFileNames, DirPath);
            string strDownloadZipFilePath = ZipFolder(strZipFilePath + "\\" + strZipFileName + ".zip", DirPath);
            if (!string.IsNullOrEmpty(strDownloadZipFilePath))
            {
                TransmitZipFile(strDownloadZipFilePath);
            }

            return string.Empty;
        }

        #endregion


        #endregion
    }
}

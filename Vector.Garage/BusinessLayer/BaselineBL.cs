using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Vector.Common.BusinessLayer;
using Vector.Common.DataLayer;
using Vector.Common.Entities;
using Vector.Garage.DataLayer;
using Vector.Garage.Entities;

namespace Vector.Garage.BusinessLayer
{
    public class BaselineBL : DisposeLogic
    {
        private VectorDataConnection objVectorDB;
        public BaselineBL(VectorDataConnection objVectorCon)
        {
            this.objVectorDB = objVectorCon;
        }
        public VectorResponse<object> VectorManageBaseLineSupportFilesBL(BaseLineSupportFiles objBaselineSupportFiles)
        {
            using (var objBaselineDL = new BaselineDL(objVectorDB))
            {
                XDocument baselineDocs = new XDocument(
                     new XElement("ROOT",
                     from baselineDoc in objBaselineSupportFiles.BaseLineSupportingFilesDocuments
                     select new XElement("File",
                                 new XElement("BaselineSupportingFilesInfoId", baselineDoc.BaselineId),
                                 new XElement("BaselineSupportingFileId", baselineDoc.BaselineId),
                                 new XElement("BaselineSupportingFileDetailInfoId", baselineDoc.BaselineId),
                                 new XElement("BaselineId", baselineDoc.BaselineId),
                                 new XElement("FileName", baselineDoc.FileName),
                                 new XElement("FilePath", baselineDoc.FilePath),
                                 new XElement("FileType", baselineDoc.FileType)

                     )));
                var result = objBaselineDL.VectorManageBaseLineSupportFilesDL(objBaselineSupportFiles, baselineDocs.ToString());
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }

        public VectorResponse<object> GetBaseLineSupportFiles(BaselineSupportingFilesInfoSearch objBaselineSupportingFilesInfoSearch)
        {
            using (var objBaselineDL = new BaselineDL(objVectorDB))
            {
                var result = objBaselineDL.GetBaseLineSupportFiles(objBaselineSupportingFilesInfoSearch);

                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }
        public VectorResponse<object> VectorManageBaseLineInfoBL(BaseLineInfo objBaseLineInfo)
        {
            using (var objBaselineDL = new BaselineDL(objVectorDB))
            {

                var result = objBaselineDL.VectorManageBaseLineInfoDL(objBaseLineInfo);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }

        public VectorResponse<object> GetBaseLineInfoBL(BaselineInfoSearch objBaselineInfoSearch)
        {
            using (var objBaselineDL = new BaselineDL(objVectorDB))
            {
                var result = objBaselineDL.GetBaseLineInfoDL(objBaselineInfoSearch);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }

        public VectorResponse<object> GetBaseLineMainInfoBL(BaselineInfoSearch objBaselineInfoSearch)
        {
            using (var objBaselineDL = new BaselineDL(objVectorDB))
            {
                var result = objBaselineDL.GetBaseLineMainInfoDL(objBaselineInfoSearch);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }

        public VectorResponse<object> VectorManageBaseLineMainInfoBL(BaseLineMainInfo objBaseLineMainInfo)
        {
            using (var objBaselineDL = new BaselineDL(objVectorDB))
            {
                XDocument baselineDocs = new XDocument(
                     new XElement("ROOT",
                     from clientDoc in objBaseLineMainInfo.Documents
                     select new XElement("document",
                                 new XElement("BaselineDocumentId", clientDoc.BaselineDocumentId),
                                 new XElement("DocumentName", clientDoc.DocumentName),
                                 new XElement("DocumentPath", clientDoc.DocumentPath),
                                 new XElement("Type", clientDoc.Type)
                     )));

                var result = objBaselineDL.VectorManageBaseLineMainInfoDL(objBaseLineMainInfo, baselineDocs.ToString());
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }

        public VectorResponse<object> VectorGetMapCatalogLineItemInfoBL(MapCatalogSearch objMapCatalogSearch)
        {
            using (var objBaselineDL = new BaselineDL(objVectorDB))
            {

                var result = objBaselineDL.VectorGetMapCatalogLineItemInfoDL(objMapCatalogSearch);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    if (string.Equals("GetMapCatalogLineItemInfo", objMapCatalogSearch.Action))
                    {
                        result.Tables[VectorConstants.Zero].TableName = "PropertyInfo";
                        result.Tables[VectorConstants.One].TableName = "VendorInfo";
                        result.Tables[VectorConstants.Two].TableName = "BaselineInfo";
                        result.Tables[VectorConstants.Three].TableName = "VendorContactsInfo";
                        result.Tables[VectorConstants.Four].TableName = "lineItemsInfo";
                        result.Tables[VectorConstants.Five].TableName = "propertyDocumentInfo";
                        result.Tables[VectorConstants.Six].TableName = "baselineDocumentInfo";
                        result.Tables[VectorConstants.Seven].TableName = "activityInfo";
                    }

                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }

        public VectorResponse<object> VectorManageMapCatalogLineItemInfoBL(MapCatalog objMapCatalog)
        {
            using (var objBaselineDL = new BaselineDL(objVectorDB))
            {
                var result = objBaselineDL.VectorManageMapCatalogLineItemInfoDL(objMapCatalog);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }
        public VectorResponse<object> VectorGetApproveBaseLineInfoBL(ApproveBaseLineInfoSearch objApproveBaseLineInfoSearch)
        {
            using (var objBaselineDL = new BaselineDL(objVectorDB))
            {

                var result = objBaselineDL.VectorGetApproveBaseLineInfoDL(objApproveBaseLineInfoSearch);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }
        public VectorResponse<object> VectorManageApproveBaseLineInfoBL(ApproveBaseLineInfo objApproveBaseLineInfo)
        {
            using (var objBaselineDL = new BaselineDL(objVectorDB))
            {
                XDocument baselineDocs = new XDocument(
                     new XElement("ROOT",
                     from baselineDoc in objApproveBaseLineInfo.ApproveBaseLineDocuments
                     select new XElement("document",
                                 new XElement("ApproveBaselineInfoDocumentId", baselineDoc.ApproveBaselineInfoDocumentId),
                                 new XElement("ApproveBaselineInfoId", baselineDoc.ApproveBaselineInfoId),
                                 new XElement("BaselineDocumentId", baselineDoc.BaselineDocumentId),
                                 new XElement("DocumentName", baselineDoc.DocumentName),
                                 new XElement("DocumentPath", baselineDoc.DocumentPath),
                                 new XElement("Type", "BaselineInvoice")
                     )));
                var result = objBaselineDL.VectorManageApproveBaseLineInfoDL(objApproveBaseLineInfo, baselineDocs.ToString());
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }

        public VectorResponse<object> VectorSearchBaselines(SearchBaseline objSearchBaseline, Int64 userId)
        {
            using (var objBaselineDL = new BaselineDL(objVectorDB))
            {
                var result = objBaselineDL.VectorSearchBaselines(objSearchBaseline, userId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }

        public VectorResponse<object> VectorGetBaseLineInformation(string baseLineId, Int64 userId)
        {
            using (var objBaselineDL = new BaselineDL(objVectorDB))
            {
                var result = objBaselineDL.VectorGetBaseLineInformation(baseLineId, userId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }

        public VectorResponse<object> VectorAddBaseline(Baseline objBaseline, Int64 userId)
        {
            using (var objBaselineDL = new BaselineDL(objVectorDB))
            {
                int fileCount = 0;
                int uploadedFilesCount = 0;

                if (!objBaseline.Action.ToUpper().Equals("ADDBASELINE") && !objBaseline.Action.ToUpper().Equals("ADDBASELINEFROMTASK"))
                {
                    if (objBaseline.Documents != null)
                    {
                        fileCount = objBaseline.Documents.Count();
                        string fileDictionaryPath = ConfigurationManager.AppSettings["FileServerPath"].ToString() + "\\Baseline\\" + objBaseline.BaselineNo + "\\BaseLineInvoice";
                        string fileDictionaryPathTemp = ConfigurationManager.AppSettings["FileServerTempPath"].ToString();
                        if (!Directory.Exists(fileDictionaryPath))
                        {
                            Directory.CreateDirectory(fileDictionaryPath);
                        }


                        foreach (var fil in objBaseline.Documents)
                        {
                            if (File.Exists(fileDictionaryPathTemp + "//" + objBaseline.TempFolderName + "//" + fil.fileName))
                            {
                                uploadedFilesCount = uploadedFilesCount + 1;
                                File.Move(fileDictionaryPathTemp + "//" + objBaseline.TempFolderName + "//" + fil.fileName, fileDictionaryPath + "//" + fil.fileName);
                            }
                        }
                    }
                }


                if (fileCount != uploadedFilesCount)
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "Unable to " } };
                }
                else
                {
                    var result = objBaselineDL.VectorAddBaseline(objBaseline, userId);

                    if (DataValidationLayer.isDataSetNotNull(result))
                    {
                        if (objBaseline.Action.ToUpper().Equals("ADDBASELINE") || objBaseline.Action.ToUpper().Equals("ADDBASELINEFROMTASK"))
                        {
                            string baselienNo = (from C in result.Tables[0].AsEnumerable()
                                                 select C.Field<string>("BaselineNo")).FirstOrDefault().ToString();
                            bool isSuccess = (from C in result.Tables[0].AsEnumerable()
                                              select C.Field<bool>("Result")).FirstOrDefault();

                            if (isSuccess)
                                if (objBaseline.Documents != null)
                                {
                                    fileCount = objBaseline.Documents.Count();
                                    string fileDictionaryPath = ConfigurationManager.AppSettings["FileServerPath"].ToString() + "\\Baseline\\" + baselienNo + "\\BaseLineInvoice";
                                    string fileDictionaryPathTemp = ConfigurationManager.AppSettings["FileServerTempPath"].ToString();
                                    if (!Directory.Exists(fileDictionaryPath))
                                    {
                                        Directory.CreateDirectory(fileDictionaryPath);
                                    }


                                    foreach (var fil in objBaseline.Documents)
                                    {
                                        if (File.Exists(fileDictionaryPathTemp + "//" + objBaseline.TempFolderName + "//" + fil.fileName))
                                        {
                                            uploadedFilesCount = uploadedFilesCount + 1;
                                            File.Move(fileDictionaryPathTemp + "//" + objBaseline.TempFolderName + "//" + fil.fileName, fileDictionaryPath + "//" + fil.fileName);
                                        }
                                    }
                                }
                        }


                        return new VectorResponse<object>() { ResponseData = result };
                    }
                    else
                    {
                        return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                    }
                }
            }
        }


        public VectorResponse<object> GetBaseLineDetails(BaselineInfoSearch objBaselineInfoSearch)
        {
            using (var objBaselineDL = new BaselineDL(objVectorDB))
            {
                var result = objBaselineDL.GetBaseLineDetails(objBaselineInfoSearch);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }


        public VectorResponse<object> MangeBaselineLineItemState(LineitemInfo ObjbaselineLineitemInfo,Int64 userId)
        {
            using (var objBaselineDL = new BaselineDL(objVectorDB))
            {
                var result = objBaselineDL.MangageLineItemState(ObjbaselineLineitemInfo, userId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }

        public VectorResponse<object> GetNegotiations(string action, string propertyId, Int64 userID)
        {
            using (var objBaselineDL = new BaselineDL(objVectorDB))
            {
                var result = objBaselineDL.GetNegotiations(action,propertyId,userID);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }
    }
}

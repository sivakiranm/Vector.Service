using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Vector.Common.BusinessLayer;
using Vector.Common.DataLayer;
using Vector.UserManagement.Entities;

namespace Vector.UserManagement.DataLayer
{
    public class UserManagementDL : DisposeLogic
    {
        private Database objVectorConnection;
        private VectorDataConnection objVectorDB;

        public UserManagementDL(VectorDataConnection objVectorDB)
        {
            this.objVectorDB = objVectorDB;
        }

        private Database GetVectorDBInstance()
        {
            return objVectorDB.GetVectorDBConnection();
        }

        public DataSet VectorGetUserManagementInfoDL(VectorGetUserManagementInfo objVectorGetUserManagementInfo)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetUserManagementInfo.ToString(),
                objVectorGetUserManagementInfo.Action,
                objVectorGetUserManagementInfo.Id,
                objVectorGetUserManagementInfo.SearchValue1,
                objVectorGetUserManagementInfo.SearchValue2,
                objVectorGetUserManagementInfo.SearchValue3,
                objVectorGetUserManagementInfo.SearchValue4,
                objVectorGetUserManagementInfo.UserId);
        }

        public DataSet GetVactorMasterDataDL(VactorMasterData objVactorMasterData)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.GetVactorMasterData.ToString(),
                objVactorMasterData.Action,
                objVactorMasterData.UserId,
                objVactorMasterData.ActionId,
                objVactorMasterData.Status,
                "");
        }

        public DataSet VectorGetWorkManifestInfoDL(VectorGetWorkManifestInfo objVectorGetWorkManifestInfo)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetWorkManifestInfo.ToString(),
                objVectorGetWorkManifestInfo.Action,
                objVectorGetWorkManifestInfo.UserId,
                objVectorGetWorkManifestInfo.Id,
                objVectorGetWorkManifestInfo.SearchText1,
                objVectorGetWorkManifestInfo.SearchText2);
        }

        public DataSet VectorGetToolsFeaturesInfoDL(int personaId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetToolsFeaturesInfo.ToString(),
                VectorEnums.Actions.GetFeaturesByPerosna.ToString(),
                personaId);
        }

        public DataSet VectorManageWorkManifestInfoDL(VectorManageWorkManifestInfo objVectorManageWorkManifestInfo)
        {
            objVectorConnection = GetVectorDBInstance();
            List<int> features = new List<int>();
            string xmlElements = null;
            if (objVectorManageWorkManifestInfo.Features != null && objVectorManageWorkManifestInfo.Features.Count > VectorConstants.Zero)
            {
                features = objVectorManageWorkManifestInfo.Features.Select(i => i.FeatureID).ToList();
                xmlElements = Common.BusinessLayer.Common.PrepareXML(features,
                                                VectorConstants.ROOT,
                                                VectorConstants.Features,
                                                VectorConstants.FeatureId);
            }
            var res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageWorkManifestInfo.ToString(),
                        objVectorManageWorkManifestInfo.Action,
                        xmlElements,
                        objVectorManageWorkManifestInfo.UserId,
                        objVectorManageWorkManifestInfo.CurrentUserId);
            return res;
        }

        public DataSet VectorManagePersonaFeaturesDL(VectorManagePersonaFeatures objVectorManagePersonaFeatures)
        {
            objVectorConnection = GetVectorDBInstance();
            List<int> features = new List<int>();
            string xmlElements = null;
            if (objVectorManagePersonaFeatures.Features != null && objVectorManagePersonaFeatures.Features.Count > VectorConstants.Zero)
            {
                features = objVectorManagePersonaFeatures.Features.Select(i => i.FeatureID).ToList();
                xmlElements = Vector.Common.BusinessLayer.Common.PrepareXML(features,
                                VectorConstants.ROOT,
                                VectorConstants.Features,
                                VectorConstants.FeatureId);
            }
            var res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManagePersonaFeatures.ToString(),
                                objVectorManagePersonaFeatures.Action,
                                xmlElements,
                                objVectorManagePersonaFeatures.PersonaMasterId,
                                objVectorManagePersonaFeatures.CurrentUserId);
            return res;
        }


        public DataSet VectorManageUserInfoDL(VectorMangerUserInfo objVectorMangerUserInfo)
        {
            objVectorConnection = GetVectorDBInstance();
            int isExecuted = VectorConstants.Zero;
            string message = string.Empty;

            var res = VectorUserClientsProperties(objVectorMangerUserInfo, objVectorMangerUserInfo.Action, string.Empty);

            if (res.Tables[VectorConstants.Zero] != null)
            {
                isExecuted = Convert.ToInt32(res.Tables[VectorConstants.Zero].Rows[VectorConstants.Zero][VectorConstants.Zero]);
                message = Convert.ToString(res.Tables[VectorConstants.Zero].Rows[VectorConstants.Zero][VectorConstants.One]);

                res.Tables[VectorConstants.Zero].Rows[VectorConstants.Zero][VectorConstants.One] = message.Split(VectorConstants.Cap)[VectorConstants.Zero].Clone();
            }
            if (isExecuted == VectorConstants.One && !string.Equals(objVectorMangerUserInfo.Action, VectorConstants.InActiveUser))
            {
                objVectorMangerUserInfo.UserId = Convert.ToInt32(message.Split(VectorConstants.Cap)[VectorConstants.One]);
                List<int> clients = new List<int>();
                string xmlElements = null;
                if (objVectorMangerUserInfo.Clients != null && objVectorMangerUserInfo.Clients.Count > VectorConstants.Zero)
                {
                    clients = objVectorMangerUserInfo.Clients.Select(i => i.ClientId).ToList();
                    xmlElements = Vector.Common.BusinessLayer.Common.PrepareXML(clients,
                        VectorConstants.ROOT,
                        VectorConstants.Clients,
                        VectorConstants.ClientId);
                }

                VectorUserClientsProperties(objVectorMangerUserInfo,
                            string.Equals(objVectorMangerUserInfo.Action, VectorConstants.CreateUser)
                                            ? VectorConstants.AssignClientsToUser
                                            : VectorConstants.UpdateClientsToUser,
                            xmlElements);
                List<int> properties = new List<int>();
                if (objVectorMangerUserInfo.Properties != null && objVectorMangerUserInfo.Properties.Count > VectorConstants.Zero)
                {
                    properties = objVectorMangerUserInfo.Properties.Select(i => i.PropertyId).ToList();
                    xmlElements = Vector.Common.BusinessLayer.Common.PrepareXML(properties,
                        VectorConstants.ROOT,
                        VectorConstants.Properties,
                        VectorConstants.PropertyId);
                }

                VectorUserClientsProperties(objVectorMangerUserInfo,
                        string.Equals(objVectorMangerUserInfo.Action, VectorConstants.CreateUser)
                                        ? VectorConstants.AssignPropertiesToUser
                                        : VectorConstants.UpdatePropertiesToUser,
                        xmlElements);
            }
            return res;
        }

        private DataSet VectorUserClientsProperties(VectorMangerUserInfo objVectorMangerUserInfo, string action, string xmlElements)
        {
            var res = new DataSet();

            res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageUserInfo.ToString(),
                                                    action,
                                                    xmlElements,
                                                    objVectorMangerUserInfo.UserId,
                                                    objVectorMangerUserInfo.CurrentUserId,
                                                    objVectorMangerUserInfo.LoginId,
                                                    objVectorMangerUserInfo.FirstName,
                                                    objVectorMangerUserInfo.LastName,
                                                    objVectorMangerUserInfo.Email,
                                                    objVectorMangerUserInfo.PhoneNbr,
                                                    objVectorMangerUserInfo.TimeZone,
                                                    objVectorMangerUserInfo.UserPersona,
                                                    objVectorMangerUserInfo.UserPersonaId,
                                                    objVectorMangerUserInfo.AppIds,
                                                    objVectorMangerUserInfo.CommIds,
                                                    objVectorMangerUserInfo.SecondPhone,
                                                    objVectorMangerUserInfo.DepartmentId,
                                                    objVectorMangerUserInfo.Country,
                                                    objVectorMangerUserInfo.UserType,
                                                    objVectorMangerUserInfo.assignTaskAccess,
                                                    objVectorMangerUserInfo.reporterUserId);

            return res;
        }

        public DataSet GetUserAssigendFeatures(Int64 userId, string menuType)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetFeatures.ToString(),
                VectorEnums.Actions.UserFeatures.ToString(),
                userId, menuType);
        }

        public DataSet GetUserProfileInfoDL(Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetUserManagementInfo.ToString(),
                VectorEnums.Actions.GetUserProfileDetails.ToString(), 0, "", "", "", "", userId);
        }

        public DataSet VectorManageUserProfileInfoDL(VectorMangerUserInfo objVectorMangerUserInfo)
        {
            var res = new DataSet();
            objVectorConnection = GetVectorDBInstance();
            res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageUserProfileInfo.ToString(),
                                                    objVectorMangerUserInfo.UserId,
                                                    objVectorMangerUserInfo.FirstName,
                                                    objVectorMangerUserInfo.LastName,
                                                    objVectorMangerUserInfo.Email,
                                                    objVectorMangerUserInfo.PhoneNbr,
                                                    objVectorMangerUserInfo.TimeZone,
                                                    objVectorMangerUserInfo.SecondPhone,
                                                    objVectorMangerUserInfo.Country,
                                                    //objVectorMangerUserInfo.Password,
                                                    objVectorMangerUserInfo.ProfilePic,
                                                    objVectorMangerUserInfo.ringCentralExtension,
                                                    objVectorMangerUserInfo.ringCentralUserName,
                                                    objVectorMangerUserInfo.ringCentralPassword);

            return res;
        }

        public DataSet GetResources(string action, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetResources.ToString(),
                action,
                userId);
        }


        public DataSet ManageResources(ManageResources objManageResources, Int64 userId)
        {
            var res = new DataSet();
            objVectorConnection = GetVectorDBInstance();
            res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageResources.ToString(),
                                                    objManageResources.Action,
                                                    objManageResources.InvendoryIds,
                                                    objManageResources.ExistingResourceId,
                                                    objManageResources.NewResourceId,
                                                    objManageResources.ResourceType,
                                                    userId
                                                  );

            return res;
        }

        public DataSet LogFeatureNavigations(UserFeatureNavigationLogDetail objUserFeatureLogDetail, string action, string loginUser)
        {
            var res = new DataSet();
            objVectorConnection = GetVectorDBInstance();
            res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.ManageUserFeatureNavigationLog.ToString(),
                                                    action,
                                                    objUserFeatureLogDetail.previousFeatureId,
                                                    objUserFeatureLogDetail.currentFeatureId,
                                                    objUserFeatureLogDetail.previousUrl,
                                                    objUserFeatureLogDetail.currentUrl,
                                                    loginUser
                                                  );

            return res;
        }

        internal DataSet LogUserDetails(UserLogDetail objUserLogDetail,string action,string loginId)
        {
            var res = new DataSet();
            objVectorConnection = GetVectorDBInstance();
            res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.ManageUserLog.ToString(),
                                                    action,
                                                    //objUserLogDetail.Name,
                                                    //objUserLogDetail.Email,
                                                    //objUserLogDetail.UserType,
                                                    //objUserLogDetail.UserRole,
                                                    objUserLogDetail.SourceIP,
                                                    objUserLogDetail.BrowserName,
                                                    objUserLogDetail.Platform,
                                                    objUserLogDetail.Version,
                                                    objUserLogDetail.LoginType,
                                                    objUserLogDetail.City,
                                                    objUserLogDetail.State,
                                                    objUserLogDetail.Zip,
                                                    objUserLogDetail.Country,
                                                    objUserLogDetail.Latitude,
                                                    objUserLogDetail.Longitude,
                                                    objUserLogDetail.TimeZone,
                                                    loginId);
            return res;
        }
    }
}

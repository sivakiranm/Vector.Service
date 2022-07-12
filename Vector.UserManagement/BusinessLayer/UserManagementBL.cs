using System;
using System.Collections.Generic;
using System.Data;
using Vector.Common.BusinessLayer;
using Vector.Common.DataLayer;
using Vector.Common.Entities;
using Vector.UserManagement.DataLayer;
using Vector.UserManagement.Entities;

namespace Vector.UserManagement.BusinessLayer
{
    public class UserManagementBL : DisposeLogic
    {
        private VectorDataConnection objVectorDB;
        public UserManagementBL(VectorDataConnection objVectorCon)
        {
            this.objVectorDB = objVectorCon;
        }

        public VectorResponse<object> VectorGetUserManagementInfoBl(VectorGetUserManagementInfo objVectorGetUserManagementInfo)
        {
            using (var objUserManageDL = new UserManagementDL(objVectorDB))
            {
                var result = objUserManageDL.VectorGetUserManagementInfoDL(objVectorGetUserManagementInfo);
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

        public VectorResponse<object> VectorGetToolsFeaturesInfoBL(int personaId)
        {
            using (var objUserManageDL = new UserManagementDL(objVectorDB))
            {
                var result = objUserManageDL.VectorGetToolsFeaturesInfoDL(personaId);
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

        public VectorResponse<object> GetVactorMasterDataBL(VactorMasterData objVactorMasterData)
        {
            using (var objUserManageDL = new UserManagementDL(objVectorDB))
            {
                var result = objUserManageDL.GetVactorMasterDataDL(objVactorMasterData);
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

        public VectorResponse<object> VectorGetWorkManifestInfoBL(VectorGetWorkManifestInfo objVectorGetWorkManifestInfo)
        {
            using (var objUserManageDL = new UserManagementDL(objVectorDB))
            {
                var result = objUserManageDL.VectorGetWorkManifestInfoDL(objVectorGetWorkManifestInfo);
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

        public VectorResponse<object> VectorManageWorkManifestInfoBL(VectorManageWorkManifestInfo objVectorManageWorkManifestInfoDL)
        {
            using (var objUserManageDL = new UserManagementDL(objVectorDB))
            {
                var result = objUserManageDL.VectorManageWorkManifestInfoDL(objVectorManageWorkManifestInfoDL);
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

        public VectorResponse<object> VectorManagePersonaFeaturesBL(VectorManagePersonaFeatures objVectorManagePersonaFeatures)
        {
            using (var objUserManageDL = new UserManagementDL(objVectorDB))
            {
                var result = objUserManageDL.VectorManagePersonaFeaturesDL(objVectorManagePersonaFeatures);
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

        public VectorResponse<object> VectorManageUserInfoBL(VectorMangerUserInfo objVectorMangerUserInfo)
        {
            using (var objUserManageDL = new UserManagementDL(objVectorDB))
            {
                var result = objUserManageDL.VectorManageUserInfoDL(objVectorMangerUserInfo);
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

        public VectorResponse<object> GetUserAssigendFeatures(Int64 userId, string menuType)
        {
            using (var objUserManageDL = new UserManagementDL(objVectorDB))
            {
                DataSet result = objUserManageDL.GetUserAssigendFeatures(userId, menuType);

                UserFeatureAccess objUserFeatureAccess = new UserFeatureAccess();
                List<MainMenu> objmainmenuList = new List<MainMenu>();
                //TODO : Check If Table Exits and Validation of Table & Move To Common method
                if (result != null)
                {
                    foreach (DataRow dr in result.Tables[VectorConstants.Zero].Rows)
                    {
                        MainMenu objMainMenu = new MainMenu();
                        int MainMenuId = Convert.ToInt32(dr["MainMenuId"]);

                        List<Menu> objMMenu = new List<Menu>();
                        objMainMenu.MainMenuId = Convert.ToInt32(dr["MainMenuId"]);
                        objMainMenu.MainMenuName = Convert.ToString(dr["MainMenuName"]);
                        objMainMenu.MainMenuRoutePath = Convert.ToString(dr["MainMenuRoutePath"]);
                        objMainMenu.MainMenuType = Convert.ToString(dr["MainMenuType"]);

                        //TODO : Check If Table Exits and Validation of Table
                        foreach (DataRow drMenu in result.Tables[VectorConstants.One].Rows)
                        {
                            int MMainMenuId = Convert.ToInt32(drMenu["MainMenuId"]);
                            int MenuId = Convert.ToInt32(drMenu["MenuId"]);
                            if (MainMenuId == MMainMenuId)
                            {
                                Menu objMenu = new Menu();
                                objMenu.MenuId = MenuId;
                                objMenu.MenuName = Convert.ToString(drMenu["MenuName"]);
                                objMenu.MenuRoutePath = Convert.ToString(drMenu["MenuRoutePath"]);
                                objMenu.MenuType = Convert.ToString(drMenu["MenuType"]);

                                List<SubMenu> objsubMenuList = new List<SubMenu>();

                                foreach (DataRow drSubMenu in result.Tables[VectorConstants.Two].Rows)
                                {
                                    int SMenuId = Convert.ToInt32(drSubMenu["MenuId"]);
                                    int subMenuId = Convert.ToInt32(drSubMenu["SubMenuId"]);

                                    if (SMenuId == MenuId)
                                    {
                                        SubMenu objSubMenu = new SubMenu();
                                        objSubMenu.SubMenuId = Convert.ToInt32(drSubMenu["SubMenuId"]);
                                        objSubMenu.SubMenuName = Convert.ToString(drSubMenu["SubMenuName"]);
                                        objSubMenu.SubMenuRoutePath = Convert.ToString(drSubMenu["SubMenuRoutePath"]);
                                        objSubMenu.SubMenuType = Convert.ToString(drSubMenu["SubMenuType"]);

                                        List<Feature> objFeauresList = new List<Feature>();
                                        foreach (DataRow frFeature in result.Tables[VectorConstants.Three].Rows)
                                        {

                                            int FsubMenuId = Convert.ToInt32(frFeature["SubMenuId"]);

                                            if (subMenuId == FsubMenuId)
                                            {
                                                Feature objFeature = new Feature();
                                                objFeature.FeatureId = Convert.ToInt32(frFeature["FeatureId"]);
                                                objFeature.FeatureName = Convert.ToString(frFeature["FeatureName"]);
                                                objFeature.FeatureRoutePath = Convert.ToString(frFeature["FeatureRoutePath"]);
                                                objFeature.FeatureType = Convert.ToString(frFeature["FeatureType"]);
                                                objFeature.IsPinned = Convert.ToBoolean(frFeature["IsPinned"]);
                                                objFeature.Icon = Convert.ToString(frFeature["Icon"]);
                                                objFeature.FeatureDescription = Convert.ToString(frFeature["FeatureDescription"]);
                                                objFeature.FeatureClass = Convert.ToString(frFeature["FeatureClass"]);
                                                objFeauresList.Add(objFeature);
                                            }

                                        }
                                        objSubMenu.Features = objFeauresList;
                                        objsubMenuList.Add(objSubMenu);
                                    }
                                }

                                objMenu.SubMenu = objsubMenuList;
                                objMMenu.Add(objMenu);
                            }
                        }
                        objMainMenu.Menu = objMMenu;
                        objmainmenuList.Add(objMainMenu);
                    }


                    objUserFeatureAccess.MenuItems = objmainmenuList;


                }
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    return new VectorResponse<object>() { ResponseData = objUserFeatureAccess };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };
                }
            }
        }

        public VectorResponse<object> GetUserProfileInfoBL(Int64 userId)
        {
            using (var objUserManageDL = new UserManagementDL(objVectorDB))
            {
                var result = objUserManageDL.GetUserProfileInfoDL(userId);
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

        public VectorResponse<object> VectorManageUserProfileInfoBL(VectorMangerUserInfo objVectorMangerUserInfo)
        {
            using (var objUserManageDL = new UserManagementDL(objVectorDB))
            {
                var result = objUserManageDL.VectorManageUserProfileInfoDL(objVectorMangerUserInfo);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "Error occured. Please contact Administrator" } };
                }
            }
        }


        public VectorResponse<object> GetResources(string action, Int64 userId)
        {
            using (var objUserManageDL = new UserManagementDL(objVectorDB))
            {
                var result = objUserManageDL.GetResources(action, userId);
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



        public VectorResponse<object> ManageResources(ManageResources objManageResources, Int64 userId)
        {
            using (var objUserManageDL = new UserManagementDL(objVectorDB))
            {
                var result = objUserManageDL.ManageResources(objManageResources, userId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "Error occured. Please contact Administrator" } };
                }
            }
        }

        public VectorResponse<object> LogFeatureNavigations(UserFeatureNavigationLogDetail objUserFeatureLogDetail, string loginId, string action)
        {
            using (var objUserManageDL = new UserManagementDL(objVectorDB))
            {
                var result = objUserManageDL.LogFeatureNavigations(objUserFeatureLogDetail, action, loginId);
                if (!DataManager.IsNullOrEmptyDataSet(result))
                {
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "Error occured. Please contact Administrator" } };
                }
            }
        }

        public VectorResponse<object> LogUserDetails(UserLogDetail objUserLogDetail, string action,string loginId)
        {
            using (var objUserManageDL = new UserManagementDL(objVectorDB))
            {
                var result = objUserManageDL.LogUserDetails(objUserLogDetail,action, loginId);
                if (!DataManager.IsNullOrEmptyDataSet(result))
                {
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "Error occured. Please contact Administrator" } };
                }
            }
        }

    }
}

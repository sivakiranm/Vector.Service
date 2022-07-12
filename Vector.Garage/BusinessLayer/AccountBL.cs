using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vector.Common.BusinessLayer;
using Vector.Common.DataLayer;
using Vector.Common.Entities;
using Vector.Garage.DataLayer;
using Vector.Garage.Entities;

namespace Vector.Garage.BusinessLayer
{
    public class AccountBL : DisposeLogic
    {
        private VectorDataConnection objVectorDB;
        public AccountBL(VectorDataConnection objVectorCon)
        {
            this.objVectorDB = objVectorCon;
        }
     
        public VectorResponse<object> ManageAccountInfo(AccountInfo objAccountInfo)
        {
            using (var objAccountDL = new AccountDL(objVectorDB))
            { 
                var result = objAccountDL.ManageAccountInfo(objAccountInfo);
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

        public VectorResponse<object> GetAccountInfo(AccountInfoSearch objAccountInfoSearch)
        {
            using (var objAccountDL = new AccountDL(objVectorDB))
            {
                var result = objAccountDL.GetAccountInfo(objAccountInfoSearch);
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


        public VectorResponse<object> ManageAccountRemitInfo(AccountRemitInfo objAccountInfo)
        {
            using (var objAccountDL = new AccountDL(objVectorDB))
            {
                var result = objAccountDL.ManageAccountRemitInfo(objAccountInfo);
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

        public VectorResponse<object> GetAccountRemitInfo(AccountRemitInfoSearch objAccountInfoSearch)
        {
            using (var objAccountDL = new AccountDL(objVectorDB))
            {
                var result = objAccountDL.GetAccountRemitInfo(objAccountInfoSearch);
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


        public VectorResponse<object> VectorSearchAccounts(SearchEntity SearchEntity, Int64 userId)
        {
            using (var objAccountDL = new AccountDL(objVectorDB))
            {
                var result = objAccountDL.VectorSearchAccounts(SearchEntity, userId);
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

        public VectorResponse<object> GetAccountLineitemInfo(AccountLineItemInfo SearchEntity, Int64 userId)
        {
            using (var objAccountDL = new AccountDL(objVectorDB))
            {
                var result = objAccountDL.GetAccountLineitemInfo(SearchEntity, userId);
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

        public VectorResponse<object> ManageAccountLineItem(UpdateAccountLineitemInfo SearchEntity, Int64 userId)
        {
            using (var objAccountDL = new AccountDL(objVectorDB))
            {
                var result = objAccountDL.ManageAccountLineItem(SearchEntity, userId);
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

        public VectorResponse<object> GetAccountLineitems(string action, Int64 accountid, Int64 userId)
        {
            using (var objAccountDL = new AccountDL(objVectorDB))
            {
                var result = objAccountDL.GetAccountLineitems(action, accountid,userId);
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

        public VectorResponse<object> GetAccountComments(AccountComments accountComment, Int64 userId)
        {
            using (var objAccountDL = new AccountDL(objVectorDB))
            {
                var result = objAccountDL.GetAccountComments(accountComment, userId);
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


        public VectorResponse<object> ArchiveAccountVerificationRecord(ArchiveAccountVericationRecord objArchiveAccountVericationRecord, Int64 userId)
        {
            using (var objAccountDL = new AccountDL(objVectorDB))
            {
                var result = objAccountDL.ArchiveAccountVerificationRecord(objArchiveAccountVericationRecord, userId);
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


        public VectorResponse<object> GetAccountListReport(SearchEntity objSearchEntity, Int64 userId)
        {
            using (var objAccountDL = new AccountDL(objVectorDB))
            {
                var result = objAccountDL.GetAccountListReport(objSearchEntity, userId);
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

        public VectorResponse<object> GetMasterDataTrueUpPanelData(SearchEntity SearchEntity, Int64 userId)
        {
            using (var objAccountDL = new AccountDL(objVectorDB))
            {
                var result = objAccountDL.GetMasterDataTrueUpPanelData(SearchEntity, userId);
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



        public VectorResponse<object> ManageMasterTrueUpDataLineitem(UpdateAccountLineitemInfo SearchEntity, Int64 userId)
        {
            using (var objAccountDL = new AccountDL(objVectorDB))
            {
                var result = objAccountDL.ManageMasterTrueUpDataLineitem(SearchEntity, userId);
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


        public VectorResponse<object> VectorGetMasterDataTrueupAccountLineitemInfo(AccountLineItemInfo SearchEntity, Int64 userId)
        {
            using (var objAccountDL = new AccountDL(objVectorDB))
            {
                var result = objAccountDL.VectorGetMasterDataTrueupAccountLineitemInfo(SearchEntity, userId);
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

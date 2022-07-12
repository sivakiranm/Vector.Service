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
    public class OwnershipTransferBL : DisposeLogic
    {
        private VectorDataConnection objVectorDB;
        public OwnershipTransferBL(VectorDataConnection objVectorCon)
        {
            this.objVectorDB = objVectorCon;
        }
        public VectorResponse<object> VectorManageInitiateOwnerShipTransferInfo(OwnerShipTransferEntity objInitiateOwnershipTransfer,Int64 userID)
        {
            using (var objOwnershipTransferDL = new OwnershipTransferDL(objVectorDB))
            {
                var result = objOwnershipTransferDL.VectorManageInitiateOwnerShipTransferInfo(objInitiateOwnershipTransfer,userID);
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

        public VectorResponse<object> GetOwnershipTransfers(string Action)
        {
            using (var objOwnershipTransferDL = new OwnershipTransferDL(objVectorDB))
            {
                var result = objOwnershipTransferDL.GetOwnershipTransfers(Action);
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

        public VectorResponse<object> VectorManageOwnershipTransferClientApproval(OTClientApprovalRequest objOTClientApprovalRequest)
        {
            using (var objOwnershipTransferDL = new OwnershipTransferDL(objVectorDB))
            {
                var result = objOwnershipTransferDL.VectorManageOwnershipTransferClientApproval(objOTClientApprovalRequest);
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

        public VectorResponse<object> VectorManageOTApproveClientApproval(OTClientApprovalRequest objOTClientApprovalRequest)
        {
            using (var objOwnershipTransferDL = new OwnershipTransferDL(objVectorDB))
            {
                var result = objOwnershipTransferDL.VectorManageOTApproveClientApproval(objOTClientApprovalRequest);
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

        public VectorResponse<object> VectorManageOwnershipTransferLogEmail(OwnershipTransferLogEmail objOwnershipTransferLogEmail)
        {
            using (var objOwnershipTransferDL = new OwnershipTransferDL(objVectorDB))
            {
                var result = objOwnershipTransferDL.VectorManageOwnershipTransferLogEmail(objOwnershipTransferLogEmail);
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

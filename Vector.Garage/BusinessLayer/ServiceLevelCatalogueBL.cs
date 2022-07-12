using Vector.Common.BusinessLayer;
using Vector.Common.DataLayer;
using Vector.Common.Entities;
using Vector.Garage.DataLayer;
using Vector.Garage.Entities;

namespace Vector.Garage.BusinessLayer
{
    public class ServiceLevelCatalogueBL : DisposeLogic
    {
        private VectorDataConnection objVectorDB;
        public ServiceLevelCatalogueBL(VectorDataConnection objVectorCon)
        {
            this.objVectorDB = objVectorCon;
        }

        public VectorResponse<object> GetServiceLevelCatalogue(ServiceLevelCatalogueSearch objServiceLevelCatalogueSearch)
        {
            using (var objServiceLevelCatalogueDL = new ServiceLevelCatalogueDL(objVectorDB))
            {
                var result = objServiceLevelCatalogueDL.GetServiceLevelCatalogue(objServiceLevelCatalogueSearch);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[VectorConstants.Zero].TableName = "ServiceCatalogItems";
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }

        public VectorResponse<object> VectorGetServiceRequestInfoBL(VectorServiceRequestSearch objVectorServiceRequestSearch)
        {
            using (var objServiceLevelCatalogueDL = new ServiceLevelCatalogueDL(objVectorDB))
            {
                var result = objServiceLevelCatalogueDL.VectorGetServiceRequestInfoDL(objVectorServiceRequestSearch);
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


        public VectorResponse<object> ManageServiceLevelCatalogue(ServiceLevelCatalogue objServiceLevelCatalogue)
        {
            using (var objServiceLevelCatalogueDL = new ServiceLevelCatalogueDL(objVectorDB))
            {
                var result = objServiceLevelCatalogueDL.ManageServiceLevelCatalogue(objServiceLevelCatalogue);
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

        public VectorResponse<object> ManageServiceRequestInfo(ServiceRequest objServiceRequest)
        {
            using (var objServiceLevelCatalogueDL = new ServiceLevelCatalogueDL(objVectorDB))
            {
                var result = objServiceLevelCatalogueDL.ManageServiceRequestInfo(objServiceRequest);
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

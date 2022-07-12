using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Data;
using Vector.Common.BusinessLayer;
using Vector.Common.DataLayer;
using Vector.Garage.Entities;

namespace Vector.Garage.DataLayer
{
    public class ServiceLevelCatalogueDL : DisposeLogic
    {
        private Database objVectorConnection;
        private VectorDataConnection objVectorDB;

        DataSet dsData = new DataSet();

        public ServiceLevelCatalogueDL(VectorDataConnection objVectorDB)
        {
            this.objVectorDB = objVectorDB;
        }

        private Database GetVectorDBInstance()
        {
            return objVectorDB.GetVectorDBConnection();
        }

        public DataSet GetServiceLevelCatalogue(ServiceLevelCatalogueSearch objServiceLevelCatalogueSearch)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetServiceLevelCatalog.ToString(),
               objServiceLevelCatalogueSearch.Action,
               objServiceLevelCatalogueSearch.ServiceCatalogueId,
               objServiceLevelCatalogueSearch.ServiceCatalogueInfoId,
               objServiceLevelCatalogueSearch.TaskId);
        }

        public DataSet VectorGetServiceRequestInfoDL(VectorServiceRequestSearch objVectorServiceRequestSearch)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetServiceRequestInfo.ToString(),
               objVectorServiceRequestSearch.Action,
               objVectorServiceRequestSearch.ServiceRequestId,
               objVectorServiceRequestSearch.ServiceRequestInfoId,
               objVectorServiceRequestSearch.TaskId);
        }

        public DataSet ManageServiceLevelCatalogue(ServiceLevelCatalogue objServiceLevelCatalogue)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageServiceLevelCatalog.ToString(),
                                                  objServiceLevelCatalogue.Action,
                                                  objServiceLevelCatalogue.ServiceCatalogueId,
                                                  objServiceLevelCatalogue.ServiceCatalogueInfoId,
                                                  objServiceLevelCatalogue.TaskId,
                                                  objServiceLevelCatalogue.ServiceCategoryId,
                                                  objServiceLevelCatalogue.EquipmentTypeId,
                                                  objServiceLevelCatalogue.Size,
                                                  objServiceLevelCatalogue.VectorUnitId,
                                                  objServiceLevelCatalogue.WasteTypeId,
                                                  objServiceLevelCatalogue.IsTaxable,
                                                  objServiceLevelCatalogue.IsSurchargeable,
                                                  objServiceLevelCatalogue.ReportingVectorUnitId,
                                                  objServiceLevelCatalogue.Weight,
                                                  objServiceLevelCatalogue.Toonage,
                                                  objServiceLevelCatalogue.ServiceName,
                                                  objServiceLevelCatalogue.ServiceNo,
                                                  objServiceLevelCatalogue.QuantityBehaviour,
                                                  objServiceLevelCatalogue.ServiceFrequency,
                                                  objServiceLevelCatalogue.Comments,
                                                  objServiceLevelCatalogue.UserId,
                                                  objServiceLevelCatalogue.IsFromTask,
                                                  objServiceLevelCatalogue.SaveOrComplete);
            return res;
        }

        public DataSet ManageServiceRequestInfo(ServiceRequest objServiceRequest)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageServiceRequestInfo.ToString(),
                                                 objServiceRequest.action,
                                                objServiceRequest.serviceRequestId,
                                                objServiceRequest.requestId,
                                                objServiceRequest.requestName,
                                                objServiceRequest.clientName,
                                                objServiceRequest.clientId,
                                                 objServiceRequest.propertyName,
                                                objServiceRequest.propertyId,

                                                objServiceRequest.propertyAddress,
                                                objServiceRequest.city,
                                                objServiceRequest.state,
                                                objServiceRequest.zip,
                                               
                                                objServiceRequest.accountId,
                                                objServiceRequest.lineItemsIds,

                                                objServiceRequest.VendorId,
                                                objServiceRequest.vendorName,
                                                objServiceRequest.serviceDescription!=null ? objServiceRequest.serviceDescription.Trim().TrimEnd(','):"",
                                                objServiceRequest.reqName,
                                                objServiceRequest.reqEmail,
                                                objServiceRequest.reqPhone,

                                                objServiceRequest.propertyContactId,
                                                objServiceRequest.primarycontact,
                                                objServiceRequest.email,
                                                objServiceRequest.phone,
                                                objServiceRequest.cause,
                                               (objServiceRequest.invoiceDate),

                                                objServiceRequest.aging,
                                                objServiceRequest.invoiceId,
                                                objServiceRequest.invoice,

                                                (objServiceRequest.paymentDate),
                                                objServiceRequest.paymentStatus,
                                                objServiceRequest.paymentReference,
                                                objServiceRequest.vectorContractVersion,
                                                objServiceRequest.scheduleServiceDate,
                                                objServiceRequest.vendorContact,
                                                objServiceRequest.vendorEmail,
                                                objServiceRequest.vendorPhone,


                                                objServiceRequest.serviceType,
                                                objServiceRequest.serviceCompleted,
                                                objServiceRequest.reOpenDate, 
                                                objServiceRequest.ae,
                                                objServiceRequest.taskid,
                                                objServiceRequest.comments,
                                                objServiceRequest.userid,
                                                objServiceRequest.IsFromTask,
                                                objServiceRequest.saveorComplete
                                              );
            return res;
        }
    }
}

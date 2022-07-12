using System;
using System.Collections.Generic;
using System.Text;

namespace Vector.Garage.Entities
{
    public class VectorServiceRequestSearch
    {
        public string Action { get; set; }
        public Int64 ServiceRequestId { get; set; }
        public Int64 ServiceRequestInfoId { get; set; }
        public Int64 TaskId { get; set; }

    }

    public class ServiceLevelCatalogueSearch
    {
        public string Action { get; set; }
        public Int64 BaselineId { get; set; }
        public Int64 ServiceCatalogueId { get; set; }
        public Int64 ServiceCatalogueInfoId { get; set; }
        public Int64 TaskId { get; set; }

    }

    public class ServiceLevelCatalogue
    {
        public string Action { get; set; }
        public long ServiceCatalogueId { get; set; }
        public long ServiceCatalogueInfoId { get; set; }
        public long TaskId { get; set; }
        public long ServiceCategoryId { get; set; }
        public long EquipmentTypeId { get; set; }
        public decimal Size { get; set; }
        public long VectorUnitId { get; set; }
        public long WasteTypeId { get; set; }
        public bool IsTaxable { get; set; }
        public bool IsSurchargeable { get; set; }
        public long ReportingVectorUnitId { get; set; }
        public decimal Weight { get; set; }
        public decimal Toonage { get; set; }
        public string ServiceName { get; set; }
        public string ServiceNo { get; set; }
        public string QuantityBehaviour { get; set; }
        public string ServiceFrequency { get; set; }
        public string Comments { get; set; }
        public long UserId { get; set; }
        public bool IsFromTask { get; set; }
        public string SaveOrComplete { get; set; }
    }

    public class ServiceRequest
    {
        public string action { get; set; }
        public int serviceRequestId { get; set; }
        public int requestId { get; set; }
        public string requestName { get; set; }
        public string propertyName { get; set; }
        public int propertyId { get; set; }
        public string clientName { get; set; }
        public int clientId { get; set; }
        public int VendorId { get; set; }
        public Int64 accountId { get; set; }
        public string lineItemsIds { get; set; }
        public string propertyAddress { get; set; }
        public string vendorName { get; set; } 
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string serviceDescription { get; set; }
        public int propertyContactId { get; set; }
        public string primarycontact { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string reqName { get; set; }
        public string reqEmail { get; set; }
        public string reqPhone { get; set; }
        public string cause { get; set; }
        public string ae { get; set; }
        public int invoiceId { get; set; }
        public string invoice { get; set; }
        public DateTime? invoiceDate { get; set; }
        public DateTime? paymentDate { get; set; }
        public string aging { get; set; }
        public string paymentReference { get; set; }
        public string paymentStatus { get; set; }
        public string vectorContractVersion { get; set; }
        public string comments { get; set; }
        public string serviceType { get; set; }
        public int taskid { get; set; }
        public int userid { get; set; }
        public string saveorComplete { get; set; }
        public string serviceCompleted { get; set; }
        public DateTime? scheduleServiceDate { get; set; }
        public DateTime? reOpenDate { get; set; }
        public string vendorContact { get; set; }
        public string vendorEmail { get; set; }
        public string vendorPhone { get; set; }
        public bool IsFromTask { get; set; }
    }
}

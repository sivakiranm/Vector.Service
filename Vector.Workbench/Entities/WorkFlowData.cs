using System;
using System.Collections.Generic;
using System.Text;

namespace Vector.Workbench.Entities
{
    public class WorkFlowData
    {
        public string CreatedBy { get; set; }
        public Flow FlowData { get; set; }
        public List<FlowDetail> FlowDetails { get; set; }
        public List<FlowDetailValue> FlowDetailValues { get; set; }
    }
    public class Flow
    {
        public int FlowId { get; set; }
        public string FlowName { get; set; }
        public string FlowDescription { get; set; }
        public string FlowStatus { get; set; }
        public string FlowCategory { get; set; }

        public string GreenresponseTime { get; set; }
        public string BlueresponseTime { get; set; }
        public string RedresponseTime { get; set; }
        public string GreenEmail { get; set; }
        public string BlueEmail { get; set; }
        public string RedEmail { get; set; }
    }
    public class FlowDetail
    {
        public int TaskOrEventeId { get; set; }
        public string TaskOrEventName { get; set; }
        public int SequenceNo { get; set; }
        public string FlowValue { get; set; }
        public int ParentFlowDetailsId { get; set; }
        public bool IsEventDriven { get; set; }
        public string FlowType { get; set; }

        public string EventValue1 { get; set; }
        public string EventValue2 { get; set; }
        public string EventValue3 { get; set; }
        public string EventValue4 { get; set; }
        public string EventValue5 { get; set; }
        public string EventValue6 { get; set; }

        public string UniqueEventDetailId { get; set; }
        public string ParentId { get; set; }
    }

    public class FlowDetailValue
    {
        public int ObjectId { get; set; }
        public string ObjectName { get; set; }
        public int ObjectDetailsId { get; set; }
        public string ObjectDetailsName { get; set; }
        public string ObjectValue { get; set; }
        public string Operator { get; set; }
        public string Condition { get; set; }
    }
    public class WorkManifestData
    {
        public string CreatedBy { get; set; }
        public Manifest ManifestData { get; set; }
        public List<ManifestDetail> ManifestDetails { get; set; }
        public List<ManifestDetailValue> ManifestDetailValues { get; set; }
    }
    public class Manifest
    {
        public int WorkManifestId { get; set; }
        public string ManifestName { get; set; }
        public string ManifestInitiater { get; set; }
        public string InitiatorType { get; set; }
        public string WorkManifestStatus { get; set; }

        public string GreenresponseTime { get; set; }
        public string BlueresponseTime { get; set; }
        public string RedresponseTime { get; set; }
        public string GreenEmail { get; set; }
        public string BlueEmail { get; set; }
        public string RedEmail { get; set; }
    }
    public class ManifestDetail
    {
        public int EventOrFlowTypeId { get; set; }
        public string EventOrFlowTypeName { get; set; }
        public int SequenceNo { get; set; }
        public string FlowValue { get; set; }
        public int ParentFlowDetailsId { get; set; }
        public bool IsEventDriven { get; set; }

        public string ManifestType { get; set; }


        public string EventValue1 { get; set; }
        public string EventValue2 { get; set; }
        public string EventValue3 { get; set; }
        public string EventValue4 { get; set; }
        public string EventValue5 { get; set; }
        public string EventValue6 { get; set; }
        public string UniqueEventDetailId { get; set; }
        public string ParentId { get; set; }

        public bool IsStartTrigger { get; set; }
        public bool IsEndTrigger { get; set; }
    }
    public class ManifestDetailValue
    {
        public int FlowId { get; set; }
        public int ObjectId { get; set; }
        public string ObjectName { get; set; }
        public string ObjectTableName { get; set; }
        public string ObjectColumnName { get; set; }
        public string ObjectValue { get; set; }
        public string Operator { get; set; }
        public string Condition { get; set; }
    }
}

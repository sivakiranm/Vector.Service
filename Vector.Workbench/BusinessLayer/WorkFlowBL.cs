using System.Linq;
using System.Xml.Linq;
using Vector.Common.BusinessLayer;
using Vector.Common.DataLayer;
using Vector.Common.Entities;
using Vector.Workbench.DataLayer;
using Vector.Workbench.Entities;

namespace Vector.Workbench.BusinessLayer
{
    public class WorkFlowBL : DisposeLogic
    {
        private VectorDataConnection objVectorDB;
        public WorkFlowBL(VectorDataConnection objVectorCon)
        {
            this.objVectorDB = objVectorCon;
        }



        public VectorResponse<object> GetWorkFLows(string action, int flowId)
        {
            using (var workFlowDL = new WorkFlowDL(objVectorDB))
            {
                var result = workFlowDL.GetWorkFLows(action, flowId);
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

        public VectorResponse<object> GetManifests(string action, int manifestId)
        {
            using (var workFlowDL = new WorkFlowDL(objVectorDB))
            {
                var result = workFlowDL.GetManifests(action, manifestId);
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

        public VectorResponse<object> GetTasks(int categoryId)
        {
            using (var workFlowDL = new WorkFlowDL(objVectorDB))
            {
                var result = workFlowDL.GetTasks(categoryId);
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

        public VectorResponse<object> GetEvents()
        {
            using (var workFlowDL = new WorkFlowDL(objVectorDB))
            {
                var result = workFlowDL.GetEvents();
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

        public VectorResponse<object> AddWorkFlow(WorkFlowData workFlowData)
        {
            using (var workFlowDL = new WorkFlowDL(objVectorDB))
            {

                XDocument flowDetails = new XDocument(
                    new XElement("ROOT",
                    from flowDet in workFlowData.FlowDetails
                    select new XElement("FlowDetails",
                    new XElement("TaskOrEventeId", flowDet.TaskOrEventeId),
                    new XElement("TaskOrEventName", flowDet.TaskOrEventName),
                    new XElement("SequenceNo", flowDet.SequenceNo),
                    new XElement("FlowValue", flowDet.FlowValue),
                    new XElement("ParentFlowDetailsId", flowDet.ParentFlowDetailsId),
                    new XElement("IsEventDriven", flowDet.IsEventDriven),
                    new XElement("FlowType", flowDet.FlowType),
                    new XElement("EventValue1", flowDet.EventValue1),
                    new XElement("EventValue2", flowDet.EventValue2),
                    new XElement("EventValue3", flowDet.EventValue3),
                    new XElement("EventValue4", flowDet.EventValue4),
                    new XElement("EventValue5", flowDet.EventValue5),
                    new XElement("EventValue6", flowDet.EventValue6),
                    new XElement("UniqueEventDetailId", flowDet.UniqueEventDetailId),
                    new XElement("ParentId", flowDet.ParentId)
                    )));

                //XDocument flowDetailValues = new XDocument(
                //   new XElement("ROOT",
                //   from flowDet in workFlowData.FlowDetailValues
                //   select new XElement("FlowDetailsValue",
                //   new XElement("ObjectId", flowDet.ObjectId),
                //   new XElement("ObjectName", flowDet.ObjectName),
                //   new XElement("ObjectDetailsId", flowDet.ObjectDetailsId),
                //   new XElement("ObjectDetailsName", flowDet.ObjectDetailsName),
                //   new XElement("ObjectValue", flowDet.ObjectValue),
                //   new XElement("Operator", flowDet.Operator),
                //   new XElement("Condition", flowDet.Condition)
                //   )));
                Flow flowData = workFlowData.FlowData;

                var result = workFlowDL.AddWorkFlow(workFlowData.CreatedBy, flowData.FlowName, flowData.FlowCategory, flowData.FlowDescription, flowData.FlowStatus, flowDetails.ToString(), "",
                    flowData.GreenresponseTime, flowData.BlueresponseTime, flowData.RedresponseTime, flowData.GreenEmail, flowData.BlueEmail, flowData.RedEmail);
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
        public VectorResponse<object> UpdateWorkFlow(WorkFlowData workFlowData)
        {
            using (var workFlowDL = new WorkFlowDL(objVectorDB))
            {

                XDocument flowDetails = new XDocument(
                    new XElement("ROOT",
                    from flowDet in workFlowData.FlowDetails
                    select new XElement("FlowDetails",
                    new XElement("TaskOrEventeId", flowDet.TaskOrEventeId),
                    new XElement("TaskOrEventName", flowDet.TaskOrEventName),
                    new XElement("SequenceNo", flowDet.SequenceNo),
                    new XElement("FlowValue", flowDet.FlowValue),
                    new XElement("ParentFlowDetailsId", flowDet.ParentFlowDetailsId),
                    new XElement("IsEventDriven", flowDet.IsEventDriven),
                    new XElement("FlowType", flowDet.FlowType),
                    new XElement("EventValue1", flowDet.EventValue1),
                    new XElement("EventValue2", flowDet.EventValue2),
                    new XElement("EventValue3", flowDet.EventValue3),
                    new XElement("EventValue4", flowDet.EventValue4),
                    new XElement("EventValue5", flowDet.EventValue5),
                    new XElement("EventValue6", flowDet.EventValue6),
                    new XElement("UniqueEventDetailId", flowDet.UniqueEventDetailId),
                    new XElement("ParentId", flowDet.ParentId)
                    )));
                Flow flowData = workFlowData.FlowData;

                var result = workFlowDL.UpdateWorkFlow(workFlowData.CreatedBy, flowData.FlowId, flowData.FlowStatus, flowDetails.ToString(),
                    flowData.GreenresponseTime, flowData.BlueresponseTime, flowData.RedresponseTime, flowData.GreenEmail, flowData.BlueEmail, flowData.RedEmail);
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
        public VectorResponse<object> AddWorkManifest(WorkManifestData workManifestData)
        {
            using (var workFlowDL = new WorkFlowDL(objVectorDB))
            {

                XDocument manifestDeatails = new XDocument(
                    new XElement("ROOT",
                    from flowDet in workManifestData.ManifestDetails
                    select new XElement("ManifestDeatails",
                    new XElement("EventOrFlowTypeId", flowDet.EventOrFlowTypeId),
                    new XElement("EventOrFlowTypeName", flowDet.EventOrFlowTypeName),
                    new XElement("SequenceNo", flowDet.SequenceNo),
                    new XElement("FlowValue", flowDet.FlowValue),
                    new XElement("ParentFlowDetailsId", flowDet.ParentFlowDetailsId),
                    new XElement("IsEventDriven", flowDet.IsEventDriven),
                 new XElement("ManifestType", flowDet.ManifestType),
                    new XElement("EventValue1", flowDet.EventValue1),
            new XElement("EventValue2", flowDet.EventValue2),
            new XElement("EventValue3", flowDet.EventValue3),
            new XElement("EventValue4", flowDet.EventValue4),
            new XElement("EventValue5", flowDet.EventValue5),
            new XElement("EventValue6", flowDet.EventValue6),
                    new XElement("UniqueEventDetailId", flowDet.UniqueEventDetailId),
                    new XElement("ParentId", flowDet.ParentId),
                    new XElement("IsStartTrigger", flowDet.IsStartTrigger),
                    new XElement("IsEndTrigger", flowDet.IsEndTrigger)
                    )));

                //XDocument manifestDetailValues = new XDocument(
                //   new XElement("ROOT",
                //   from flowDet in workManifestData.ManifestDetailValues
                //   select new XElement("ManifestDeatailValues",
                //   new XElement("ObjectId", flowDet.ObjectId),
                //   new XElement("ObjectName", flowDet.ObjectName),
                //   new XElement("ObjectTableName", flowDet.ObjectTableName),
                //   new XElement("ObjectColumnName", flowDet.ObjectColumnName),
                //   new XElement("ObjectValue", flowDet.ObjectValue),
                //   new XElement("Operator", flowDet.Operator),
                //   new XElement("Condition", flowDet.Condition)
                //   )));
                Manifest manifestData = workManifestData.ManifestData;

                var result = workFlowDL.AddWorkManifest(workManifestData.CreatedBy, manifestData.ManifestName, manifestData.ManifestInitiater, manifestData.InitiatorType, manifestData.WorkManifestStatus, manifestDeatails.ToString(), null,
                    manifestData.GreenresponseTime, manifestData.BlueresponseTime, manifestData.RedresponseTime, manifestData.GreenEmail, manifestData.BlueEmail, manifestData.RedEmail
                    );
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
        public VectorResponse<object> UpdateWorkManifest(WorkManifestData workManifestData)
        {
            using (var workFlowDL = new WorkFlowDL(objVectorDB))
            {

                XDocument manifestDeatails = new XDocument(
                 new XElement("ROOT",
                 from flowDet in workManifestData.ManifestDetails
                 select new XElement("ManifestDeatails",
                 new XElement("EventOrFlowTypeId", flowDet.EventOrFlowTypeId),
                 new XElement("EventOrFlowTypeName", flowDet.EventOrFlowTypeName),
                 new XElement("SequenceNo", flowDet.SequenceNo),
                 new XElement("FlowValue", flowDet.FlowValue),
                 new XElement("ParentFlowDetailsId", flowDet.ParentFlowDetailsId),
                 new XElement("IsEventDriven", flowDet.IsEventDriven),
                 new XElement("ManifestType", flowDet.ManifestType),
                    new XElement("EventValue1", flowDet.EventValue1),
            new XElement("EventValue2", flowDet.EventValue2),
            new XElement("EventValue3", flowDet.EventValue3),
            new XElement("EventValue4", flowDet.EventValue4),
            new XElement("EventValue5", flowDet.EventValue5),
            new XElement("EventValue6", flowDet.EventValue6),
                    new XElement("UniqueEventDetailId", flowDet.UniqueEventDetailId),
                    new XElement("ParentId", flowDet.ParentId),
                    new XElement("IsStartTrigger", flowDet.IsStartTrigger),
                    new XElement("IsEndTrigger", flowDet.IsEndTrigger)
                 )));
                Manifest manifestData = workManifestData.ManifestData;

                var result = workFlowDL.UpdateWorkManifest(workManifestData.CreatedBy, manifestData.WorkManifestId, manifestData.ManifestName, manifestData.ManifestInitiater, manifestData.InitiatorType, manifestData.WorkManifestStatus, manifestDeatails.ToString(), null);
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
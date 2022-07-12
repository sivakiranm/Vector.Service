using Microsoft.Practices.EnterpriseLibrary.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Vector.Common.BusinessLayer;
using Vector.Common.DataLayer;

namespace Vector.Workbench.DataLayer
{
    public class WorkFlowDL : DisposeLogic
    {
        private Database objVectorConnection;
        private VectorDataConnection objVectorDB;

        public WorkFlowDL(VectorDataConnection objVectorDB)
        {
            this.objVectorDB = objVectorDB;
        }

        private Database GetVectorDBInstance()
        {
            return objVectorDB.GetVectorDBConnection();
        }

        public DataSet GetManifests(string action, int manifestId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("VectorGetWorkFlows", action, manifestId);


        }
        public DataSet GetWorkFLows(string action, int flowId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("VectorGetWorkFlows", action, flowId);



            //            string json = @"{
            //  'table': [
            //    {
            //      'flowName': 'Flow 1',
            //      'flowCategory': 'Client',
            //      'flowStatus':'Commissioned'
            //    },
            //   {
            //      'flowName': 'Flow 2',
            //      'flowCategory': 'Property',
            //      'flowStatus':'Commissioned'
            //    },
            //{
            //      'flowName': 'Flow 3',
            //      'flowCategory': 'Client',
            //      'flowStatus':'Commissioned'
            //    },
            //{
            //      'flowName': 'Flow 4',
            //      'flowCategory': 'Property',
            //      'flowStatus':'Commissioned'
            //    },
            //{
            //      'flowName': 'Flow 5',
            //      'flowCategory': 'Client',
            //      'flowStatus':'Commissioned'
            //    },
            //{
            //      'flowName': 'Flow 6',
            //      'flowCategory': 'Property',
            //      'flowStatus':'Commissioned'
            //    }
            //  ],
            // 'table1': [
            //    {
            //      'flowName': 'Flow 7',
            //      'flowCategory': 'Client',
            //      'flowStatus':'Approved'
            //    },
            //   {
            //      'flowName': 'Flow 8',
            //      'flowCategory': 'Property',
            //      'flowStatus':'Rejected'
            //    },
            //{
            //      'flowName': 'Flow 9',
            //      'flowCategory': 'Client',
            //      'flowStatus':'Approved'
            //    },
            //{
            //      'flowName': 'Flow 10',
            //      'flowCategory': 'Property',
            //      'flowStatus':'Rejected'
            //    },
            //{
            //      'flowName': 'Flow 11',
            //      'flowCategory': 'Client',
            //      'flowStatus':'Approved'
            //    },
            //{
            //      'flowName': 'Flow 12',
            //      'flowCategory': 'Property',
            //      'flowStatus':'Rejected'
            //    }
            //  ]
            //}";

            //            DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(json);

            // return dataSet;
        }

        public DataSet GetTasks(int categoryId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("GetVactorMasterData", "GetTaksMaster", 1, 1, 1,"");

        }

        public DataSet GetEvents()
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("GetVactorMasterData", "GetEventsMaster", 1, 1, 1,"");

        }
        public DataSet AddWorkFlow(string createdBy, string flowName, string flowCategory, string flowDescription, string flowStatus, string xmlFlowDetails, string xmlFlowDetailValues, string GreenresponseTime, string BlueresponseTime, string RedresponseTime, string GreenEmail, string BlueEmail, string RedEmail)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("AddWorkFlow", "CreateFlow", createdBy, 0, flowName, flowCategory, flowDescription, flowStatus, xmlFlowDetails, null, GreenresponseTime, BlueresponseTime, RedresponseTime, GreenEmail, BlueEmail, RedEmail);

        }
        public DataSet UpdateWorkFlow(string createdBy, int flowId, string flowStatus, string xmlFlowDetails, string GreenresponseTime, string BlueresponseTime, string RedresponseTime, string GreenEmail, string BlueEmail, string RedEmail)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("AddWorkFlow", "UpdateFlow", createdBy, flowId, string.Empty, string.Empty, string.Empty, flowStatus, xmlFlowDetails, null, GreenresponseTime, BlueresponseTime, RedresponseTime, GreenEmail, BlueEmail, RedEmail);

        }
        public DataSet AddWorkManifest(string createdBy, string manifestName, string manifestInitiater, string initiatorType, string workManifestStatus, string manifestDetailsXml, string manifestDetailValuesXml, string GreenresponseTime, string BlueresponseTime, string RedresponseTime, string GreenEmail, string BlueEmail, string RedEmail)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("AddWorkManifest", "CreateManifest", createdBy, 0, manifestName, manifestInitiater, initiatorType, workManifestStatus, manifestDetailsXml, manifestDetailValuesXml, GreenresponseTime, BlueresponseTime, RedresponseTime, GreenEmail, BlueEmail, RedEmail);

        }


        public DataSet UpdateWorkManifest(string createdBy, int manifestId, string manifestName, string manifestInitiater, string initiatorType, string workManifestStatus, string manifestDetailsXml, string manifestDetailValuesXml)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("AddWorkManifest", "UpdateManifest", createdBy, manifestId, manifestName, manifestInitiater, initiatorType, workManifestStatus, manifestDetailsXml, manifestDetailValuesXml,
                "", "", "", "", "", "");

        }
    }
}
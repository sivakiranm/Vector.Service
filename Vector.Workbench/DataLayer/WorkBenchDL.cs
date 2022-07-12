using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Data;
using Vector.Common.BusinessLayer;
using Vector.Common.DataLayer;
using Vector.Workbench.Entities;

namespace Vector.Workbench.DataLayer
{

    public class WorkBenchDL : DisposeLogic
    {
        private Database objVectorConnection;
        private VectorDataConnection objVectorDB;

        public WorkBenchDL(VectorDataConnection objVectorDB)
        {
            this.objVectorDB = objVectorDB;
        }

        private Database GetVectorDBInstance()
        {
            return objVectorDB.GetVectorDBConnection();
        }
        public DataSet GetTakStatusMaster()
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("GetVactorMasterData", "GetTaskStatusMaster", 1, 1, 0, "");

        }
        public DataSet GetTaskTypes()
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("GetVactorMasterData", "GetTaskType", 1, 1, 0, "");

        }
        public DataSet GetUsersSearch(string userName)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("VectorGetUserManagementInfo", "GetUsersSearch", 0, userName, string.Empty, string.Empty, string.Empty, 1);

        }
        public DataSet GetManifest(string manifestName)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("VectorGetWorkManifestInfo", "GetComissionedManifests", 1, 0, manifestName, string.Empty);

        }
        public DataSet GetFlows(Int64 manifestId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("VectorGetWorkManifestInfo", "GetFlowsByManifest", 1, manifestId, string.Empty, string.Empty);

        }
        public DataSet GetTasks(Int64 flowId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("VectorGetWorkManifestInfo", "GetTasksByFlowId", 1, flowId, string.Empty, string.Empty);

        }

        public DataSet AddTicketTask(TicketTask ticketTask)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageTicketAndTasks.ToString(), "CreateTask", ticketTask.TicketId, 0, ticketTask.Subject,
                ticketTask.Description, ticketTask.DueDate, ticketTask.CreatedUserId, 1, string.Empty, ticketTask.AssignedToUserId, 0, string.Empty, 0,
                ticketTask.ManifestId, ticketTask.FlowId, ticketTask.FlowDetailsId, string.Empty, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value,
                DBNull.Value, DBNull.Value, DBNull.Value, ticketTask.manifestDetailsId, VectorConstants.Zero,string.Empty,VectorConstants.Zero,VectorConstants.Zero,string.Empty,string.Empty,null);
        }

        public DataSet ManageTicket(Ticket objTicket)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageTicketAndTasks.ToString(), "CreateTicket",
                objTicket.TicketId,
                0,
                objTicket.ticketSubject,
                objTicket.Description,
                objTicket.DueDate,
                objTicket.CreatedUserId,
                1, string.Empty, objTicket.AssignedToUserId, 0, string.Empty, DBNull.Value, 0, 0, 0, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value,
                DBNull.Value, DBNull.Value, DBNull.Value,null, objTicket.ticketSubCategoryId, objTicket.ticketType, objTicket.ticketETA, objTicket.ticketPriority, objTicket.instruction,
                objTicket.irpUniqueCode,null);
        }

        public DataSet VectorAutoTaskCreationService(int taskId, bool isLogicYesNo)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("VectorAutoTaskCreationService", "", taskId, isLogicYesNo);
        }

        public DataSet GetPreviousTasks(Int64 taskId, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("VectorGetPreviousTasksList", taskId, userId);

        }
        public DataSet GetPreviousTaskInfo(string type, Int64 taskId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("VectorGetPreviousTaskInfo", type, taskId);

        }

        public DataSet GetKeyStatsInfo(Int64 Userid)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("VectorKeyStatsInfo", Userid);

        }
        public DataSet ManageTicket(Ticket objTicket, string supportFileDocs, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            DataSet res = objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageTicketAndTasks.ToString(),
                 "CreateTicket",
                objTicket.TicketId,
                0,
                objTicket.ticketSubject,
                objTicket.Description,
                objTicket.DueDate,
                userId,
                1, string.Empty, objTicket.AssignedToUserId, objTicket.ticketCategoryId, string.Empty, DBNull.Value, 0, 0, 0, supportFileDocs,
                objTicket.ClientId, objTicket.PropertyId, objTicket.AccountId, objTicket.VendorId,objTicket.requestorName,objTicket.requestorEmail,objTicket.requesterUserId,
                null,objTicket.ticketSubCategoryId, objTicket. ticketType, objTicket.ticketETA, objTicket.ticketPriority,objTicket.instruction,
                objTicket.irpUniqueCode,objTicket.contractLineitemId);
            return res;
        }

        //public DataSet GetTicketInfo(TicketInfo objTicketInfo)
        //{
        //    objVectorConnection = GetVectorDBInstance();
        //    return objVectorConnection.ExecuteDataSet("VectorGetPreviousTaskInfo", objTicketInfo.);

        //}

        public DataSet GetAllUserTasks(Int64 ticketId, string type, Int64 userId, Int64 manifestId, Int64 flowdetailId, string categoryType, string categoryId, string assignedUsers)
        {
            objVectorConnection = GetVectorDBInstance();
                return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetTasksInfo.ToString(),
                                        type,
                                        ticketId,
                                        userId,
                                        manifestId,
                                        flowdetailId,
                                        categoryType,
                                        categoryId,
                                        assignedUsers
                                        );

        }

        public DataSet GetBackLogTasks(Int64 ticketId, string type, Int64 userId, Int64 manifestId, Int64 flowdetailId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetBackLogTasks.ToString(),
                                        type,
                                        ticketId,
                                        userId,
                                        manifestId,
                                        flowdetailId);

        }

        public DataSet ManageMyQueue(MyQueue objMyQueue, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageMyQueue.ToString(),
                                        objMyQueue.Action,
                                        objMyQueue.taskid,
                                        objMyQueue.ticketId,
                                        (objMyQueue.type != null ? objMyQueue.type : ""),
                                        userId);

        }

        public DataSet GetMyQueue(MyQueue objMyQueue, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetMyQueue.ToString(),
                                        objMyQueue.Action,
                                        userId);

        }

        public DataSet ManageTaskInfo(Tasks objTasks, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageTasks.ToString(),
                                        objTasks.Action,
                                        objTasks.TicketId,
                                        objTasks.TaskSubject,
                                        objTasks.TaskDescription,
                                        objTasks.DueDate,
                                        objTasks.AssignedToUserId,
                                        objTasks.ManifestId,
                                        objTasks.FlowId,
                                        objTasks.FlowDetailsId,
                                        objTasks.TaskId,
                                        userId,
                                        objTasks.existingTaskId);

        }

        internal DataSet UpdateTicketRequestorDetails(string ticketId, string requestorName, string requestorEmail, string messageId, long userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageTicketRequestorDetails.ToString(),
                                        ticketId,
                                        requestorName,
                                        requestorEmail,
                                        messageId,
                                        userId);
        }
    }
}

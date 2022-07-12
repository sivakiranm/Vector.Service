using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Data;
using Vector.Common.BusinessLayer;
using Vector.Common.DataLayer;
using Vector.Common.Entities;
using Vector.Workbench.Entities;

namespace Vector.Workbench.DataLayer
{
    public class TicketsDL  : DisposeLogic
    {
        #region Constructor
         
        private Database objVectorConnection;
        private VectorDataConnection objVectorDB;

        public TicketsDL(VectorDataConnection objVectorDB)
        {
            this.objVectorDB = objVectorDB;
        }

        private Database GetVectorDBInstance()
        {
            return objVectorDB.GetVectorDBConnection();
        }

        #endregion

        public DataSet GetMessageBoxInfo(string action,Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("VectorGetMessageBoxInfo", action, userId);
        }


        public DataSet GetTicketDetails(TicketInfo objTicketInfo, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("VectorGetTicketInfo",
                    objTicketInfo.Action,
                    objTicketInfo.TicketId,
                    userId);
        }

        public DataSet ManageTicketDetails(TicketInfo objTicketInfo,Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("VectorManageTicketInfo",
                    objTicketInfo.Action,
                    objTicketInfo.TicketId,
                    objTicketInfo.TicketStatus,
                     objTicketInfo.TicketStatusId, 
                          objTicketInfo.assignedUserId,
                         objTicketInfo.comments,
                          objTicketInfo.DueDate,
                          objTicketInfo.ticketCategoryId,
                           objTicketInfo.Tags,
                            objTicketInfo.ticketDocuments,
                            userId,
                            objTicketInfo.ClientId,
                            objTicketInfo.PropertyId,
                            objTicketInfo.AccountId,
                            objTicketInfo.VendorId,
                            objTicketInfo.ticketSubCategoryId
                    );
        }

        public DataSet ManageTaskInfo(TicketInfo objTicketInfo, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("VectorManageTicketInfo",
                    objTicketInfo.Action,
                    objTicketInfo.TicketId,
                    objTicketInfo.TicketStatus,
                     objTicketInfo.TicketStatusId,
                          objTicketInfo.assignedUserId,
                         objTicketInfo.comments,
                          objTicketInfo.DueDate,
                          objTicketInfo.ticketCategoryId,
                           objTicketInfo.Tags,
                            objTicketInfo.ticketDocuments,
                           userId,
                            DBNull.Value,
                            DBNull.Value,
                            DBNull.Value,
                            DBNull.Value,
                            objTicketInfo.ticketSubCategoryId
                    );
        }

        public DataSet GetTicketEmailDetails(Int64 TicketId, Int64 UserId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("VectorGetTicketInfo",
                    "GetTicketEmailDetails", TicketId,UserId);
        }


        public DataSet ManageUserAssignment(AssignTo objAssignTo, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageAssignments.ToString(),
                    objAssignTo.Action,
                    objAssignTo.UserId,
                    objAssignTo.SelectedIds,
                    objAssignTo.uniqueId,
                    userId);
        }

        public DataSet ManageCommonTask(CommonTask objCommonTask, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageCommonTask.ToString(),
                    objCommonTask.Action,
                    objCommonTask.InventoryType,
                    objCommonTask.InventoryId,
                    objCommonTask.InventoryValue,
                    objCommonTask.TaskId,
                    objCommonTask.Comments,
                    objCommonTask.IsFurtherAction,
                    userId);
        }

        public DataSet GetCommonTaskInfo(string action, Int64 taskId, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorGetCommonTaskInfo.ToString(),
                    action,
                    taskId,
                    userId);
        }
            
        public DataSet ManageEmailTicket(Common.BusinessLayer.EmailProcessManager.MailDetail objMailDetail, Int64 userId, bool isEmailRead,string documentList,Int64 newTicketId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet(VectorEnums.StoredProcedures.VectorManageEmailTicket.ToString(),
                    objMailDetail.Action,
                    objMailDetail.ChangeKey,
                    objMailDetail.ConversationId,
                    objMailDetail.EWSChangeKey,
                    objMailDetail.InternetMessageId,
                    objMailDetail.Subject,
                    objMailDetail.UniqueKey,
                    objMailDetail.UIDL,
                    objMailDetail.BodyText,
                    objMailDetail.Body,
                    isEmailRead,
                    userId,
                    documentList,
                    newTicketId);
        }
    }
}   

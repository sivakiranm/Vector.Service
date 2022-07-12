using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Data;
using Vector.Common.BusinessLayer;
using Vector.Common.DataLayer;

namespace Vector.Garage.DataLayer
{

    public class ThreeSixtyDegreesDL : DisposeLogic
    {
        private Database objVectorConnection;
        private VectorDataConnection objVectorDB;
        public ThreeSixtyDegreesDL(VectorDataConnection objVectorDB)
        {
            this.objVectorDB = objVectorDB;
        }

        private Database GetVectorDBInstance()
        {
            return objVectorDB.GetVectorDBConnection();
        }

        public DataSet GetClientInformation(string action, string clientName, Int64 clientId, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("VectorGetClientThreeSixty", action, clientId, clientName,userId);

        }
        public DataSet GetClientSearch(string clientName,Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("VectorGetClientThreeSixty", "ClientSearch", 0, clientName, userId);

        }
        public DataSet GetClientProperties(string clientName, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("VectorGetClientThreeSixty", "ClientProperties", 0, clientName, userId);

        }
        public DataSet GetClientMetrics(string clientName, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("VectorGetClientThreeSixty", "clientMetrics", 0, clientName, userId);

        }

        public DataSet GetBaselineInfo(string action, int baselineId, string baselineNo)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("VectorGetBaselineThreeSixtyView",
                action,
                baselineId,
                baselineNo
                );
        }
    

        public DataSet GetPropertyInfo(string action, int propertyId, string propertyNo)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("VectorGetPropertyThreeSisty",
                action,
                propertyId,
                propertyNo
                );
        }

        public DataSet GetHaulerInfo(string action, int haulerId, string haulerNo)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("VectorGetHaulerThreeSixty",
                action,
                haulerId,
                haulerNo
                );
        }
       

        public DataSet GetAccountInfo(string action, int accountId, string accountNo)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("VectorGetAccountThreeSixty",
                action,
                accountId,
                accountNo
                );
        }

        public DataSet SavingsCostInfo(string searchBy, Int64 searchId, string searchText, string year, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("VectorAnalyticsSavingsInfo",
                searchBy,
                searchId,
                searchText,
                year,
                userId
                );
        }

        public DataSet GetBackLogTickets(string searchBy, Int64 taskId, Int64 ticketId, string userType, string ticketStatus, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("VectorGetTicketAndTasksInfo",
                searchBy,
                taskId,
                ticketId,
                userType,
                ticketStatus,
                userId
                );
        }

        public DataSet GetAdvancedBackLogTicketsSearch(string searchBy, Int64 taskId, Int64 ticketId, string userType, string ticketStatus, Int64 userId,Int64? reportingUserId
            , string categoryType, string categoryId, string assignedUsers)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("VectorGetBackLogTickets",
                searchBy,
                taskId,
                ticketId,
                userType,
                ticketStatus,
                userId,
                reportingUserId,
                categoryType,
                categoryId,
                assignedUsers
                );
        }

        public DataSet GetAdvancedArchiveTicketsSearch(string searchBy, Int64 taskId, Int64 ticketId, string userType, string ticketStatus, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("VectorGetArchivedTickets",
                searchBy,
                taskId,
                ticketId,
                userType,
                ticketStatus,
                userId
                );
        }


        public DataSet GetTicketAndTaskInfo(string searchBy, Int64 taskId, Int64 ticketId, string userType, string ticketStatus, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("VectorGetTicketAndTasksInfo",
                searchBy,
                taskId,
                ticketId,
                userType,
                ticketStatus,
                userId
                );
        }


        public DataSet GetTasksRelatedToTicket(Int64 ticketId, Int64 userId)
        {
            objVectorConnection = GetVectorDBInstance();
            return objVectorConnection.ExecuteDataSet("VectorGetTasksRelatedToTicket",  
                ticketId, 
                userId
                );
        }
    }
}
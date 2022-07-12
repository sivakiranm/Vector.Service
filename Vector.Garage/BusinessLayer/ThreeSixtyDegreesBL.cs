using System;
using System.Data;
using Vector.Common.BusinessLayer;
using Vector.Common.DataLayer;
using Vector.Common.Entities;
using Vector.Garage.DataLayer;

namespace Vector.Garage.BusinessLayer
{

    public class ThreeSixtyDegreesBL : DisposeLogic
    {
        private VectorDataConnection objVectorDB;
        public ThreeSixtyDegreesBL(VectorDataConnection objVectorCon)
        {
            this.objVectorDB = objVectorCon;
        }

        public VectorResponse<object> GetClientInformation(string action,string clientName,Int64 clientId,Int64 userId)
        {
            using (var objThreeSixtyDegreesDL = new ThreeSixtyDegreesDL(objVectorDB))
            {
                var result = objThreeSixtyDegreesDL.GetClientInformation(action,clientName, clientId, userId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {

                    DataSet clientInformation = result;

                    if (action.Equals("ClientInfo"))
                    {
                        clientInformation.Tables[0].TableName = "clientDetails";
                        clientInformation.Tables[1].TableName = "clientContracts";
                        clientInformation.Tables[2].TableName = "salesPersons";
                        clientInformation.Tables[3].TableName = "accountManagers";
                        clientInformation.Tables[4].TableName = "negotiators";
                    }
                    else if (action.Equals("clientMetrics"))
                    {
                        clientInformation.Tables[0].TableName = "clientMetrics";
                        clientInformation.Tables[1].TableName = "clientMetricsDetails";
                    }
                    else if (action.Equals("ClientProperties"))
                    {
                        clientInformation.Tables[0].TableName = "clientProperties";
                    }

                    return new VectorResponse<object>() { ResponseData = clientInformation };

                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }

        public VectorResponse<object> GetClientProperties(string clientName, Int64 userId)
        {
            using (var objThreeSixtyDegreesDL = new ThreeSixtyDegreesDL(objVectorDB))
            {
                var result = objThreeSixtyDegreesDL.GetClientProperties(clientName, userId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    DataSet clientProperties = result;
                    clientProperties.Tables[0].TableName = "clientProperties";
                    return new VectorResponse<object>() { ResponseData = clientProperties };

                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }
       
        public VectorResponse<object> GetClientSearch(string clientName, Int64 userId)
        {
            using (var objThreeSixtyDegreesDL = new ThreeSixtyDegreesDL(objVectorDB))
            {
                var result = objThreeSixtyDegreesDL.GetClientSearch(clientName, userId);
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
        
        public VectorResponse<object> GetBaselineInfo(string action, int baselineId, string baselineNo)
        {
            using (var objThreeSixtyDegreesDL = new ThreeSixtyDegreesDL(objVectorDB))
            {
                var result = objThreeSixtyDegreesDL.GetBaselineInfo(action, baselineId, baselineNo);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    if (action.ToLower().Equals("baselineitems"))
                    {
                        result.Tables[0].TableName = "lineitems"; 
                        result.Tables[1].TableName = "surcharges";
                        result.Tables[2].TableName = "taxes";
                    }
                    else if(action.ToLower().Equals("baselineinfo"))
                    {
                        result.Tables[0].TableName = "Summary";
                        result.Tables[1].TableName = "HaulerContacts";
                    }
                    return new VectorResponse<object>() { ResponseData = result };

                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }

        public VectorResponse<object> GetPropertyInfo(string action, int propertyId, string propertyNo)
        {
            using (var objThreeSixtyDegreesDL = new ThreeSixtyDegreesDL(objVectorDB))
            {
                var result = objThreeSixtyDegreesDL.GetPropertyInfo(action, propertyId, propertyNo);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    if (action.ToLower().Equals("propertyinfo"))
                    {
                        result.Tables[0].TableName = "summary";
                        result.Tables[1].TableName = "contacts";
                    }
                    else if (action.ToLower().Equals("propertylineitems"))
                    {
                        result.Tables[0].TableName = "lineItems";

                    }
                    else if (action.ToLower().Equals("propertyinvoices"))
                    {
                        result.Tables[0].TableName = "invoices";
                    }

                    return new VectorResponse<object>() { ResponseData = result };

                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }

        public VectorResponse<object> GetHaulerInfo(string action, int propertyId, string propertyNo)
        {
            using (var objThreeSixtyDegreesDL = new ThreeSixtyDegreesDL(objVectorDB))
            {
                var result = objThreeSixtyDegreesDL.GetHaulerInfo(action, propertyId, propertyNo);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    if (action.ToLower().Equals("haulerinfo"))
                    {
                        result.Tables[0].TableName = "summary";
                        result.Tables[1].TableName = "contacts";
                    }
                    else if (action.ToLower().Equals("haulerservicesandrates"))
                    {
                        result.Tables[0].TableName = "serviceRates";
                    }
                    else if (action.ToLower().Equals("haulernegotiationhistory"))
                    {
                        result.Tables[0].TableName = "negotiationHistory";
                    }
                    else if (action.ToLower().Equals("hauleracctcredentials")) 
                    {
                        result.Tables[0].TableName = "accountCredentials";
                    }

                    return new VectorResponse<object>() { ResponseData = result };

                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }

        public VectorResponse<object> GetAccountInfo(string action, int accountId, string accountNo)
        {
            using (var objThreeSixtyDegreesDL = new ThreeSixtyDegreesDL(objVectorDB))
            {
                var result = objThreeSixtyDegreesDL.GetAccountInfo(action, accountId, accountNo);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    if (action.ToLower().Equals("accountinfo"))
                    {
                        result.Tables[0].TableName = "summary";
                        result.Tables[1].TableName = "contacts";
                    }
                    else if (action.ToLower().Equals("acctlineitems"))
                    {
                        result.Tables[0].TableName = "lineitems";
                        result.Tables[1].TableName = "exampted";
                        result.Tables[2].TableName = "surcharges";
                        result.Tables[3].TableName = "taxes";
                    }
                    else if (action.ToLower().Equals("accountinvoices"))
                    {
                        result.Tables[0].TableName = "Invoices";
                    }

                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }

        public VectorResponse<object> SavingsCostInfo(string searchBy, Int64 searchId, string searchText, string year, Int64 userId)
        {
            using (var objThreeSixtyDegreesDL = new ThreeSixtyDegreesDL(objVectorDB))
            {
                var result = objThreeSixtyDegreesDL.SavingsCostInfo(searchBy, searchId, searchText, year, userId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[0].TableName = "summary";
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }

        public VectorResponse<object> GetBackLogTickets(string searchBy, Int64 taskId, Int64 ticketId, string userType, string ticketStatus, Int64 userId)
        {
            using (var objThreeSixtyDegreesDL = new ThreeSixtyDegreesDL(objVectorDB))
            {
                var result = objThreeSixtyDegreesDL.GetBackLogTickets(searchBy,
                taskId,
                ticketId,
                userType,
                ticketStatus,
                userId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[0].TableName = "summary";

                    if (searchBy.ToLower().Equals("getticketdetails"))
                        result.Tables[1].TableName = "activityLog";

                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }

        public VectorResponse<object> GetAdvancedBackLogTicketsSearch(string searchBy, Int64 taskId, Int64 ticketId, string userType, string ticketStatus, Int64 userId,Int64? reportingUserId
            , string categoryType, string categoryId, string assignedUsers)
        {
            using (var objThreeSixtyDegreesDL = new ThreeSixtyDegreesDL(objVectorDB))
            {
                var result = objThreeSixtyDegreesDL.GetAdvancedBackLogTicketsSearch(searchBy,
                taskId,
                ticketId,
                userType,
                ticketStatus,
                userId, reportingUserId , categoryType, categoryId, assignedUsers);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[0].TableName = "summary";
                     
                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }

        public VectorResponse<object> GetAdvancedArchivedTicketsSearch(string searchBy, Int64 taskId, Int64 ticketId, string userType, string ticketStatus, Int64 userId)
        {
            using (var objThreeSixtyDegreesDL = new ThreeSixtyDegreesDL(objVectorDB))
            {
                var result = objThreeSixtyDegreesDL.GetAdvancedArchiveTicketsSearch(searchBy,
                taskId,
                ticketId,
                userType,
                ticketStatus,
                userId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[0].TableName = "summary";

                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }

        public VectorResponse<object> GetTasksRelatedToTicket(Int64 taskid,Int64 userId)
        {
            using (var objThreeSixtyDegreesDL = new ThreeSixtyDegreesDL(objVectorDB))
            {
                var result = objThreeSixtyDegreesDL.GetTasksRelatedToTicket(taskid, userId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[0].TableName = "tasks";

                    return new VectorResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new VectorResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }

        public VectorResponse<object> GetTicketAndTaskInfo(string action, Int64 taskId, Int64 ticketId, string userType, string ticketStatus, Int64 userId)
        {
            using (var objThreeSixtyDegreesDL = new ThreeSixtyDegreesDL(objVectorDB))
            {
                var result = objThreeSixtyDegreesDL.GetTicketAndTaskInfo(action,
                taskId,
                ticketId,
                userType,
                ticketStatus,
                userId);
                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    result.Tables[0].TableName = "summary";

                    if (action.ToUpper().Equals("GETBACKLOGINFO"))
                        result.Tables[1].TableName = "backlogs";

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

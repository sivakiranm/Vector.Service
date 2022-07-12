using System.Data;

namespace Vector.Master.DataLayer
{
    //All Tickets Related Actions should be added to this layer
    public class TicketsDA
    {
        public DataSet GetTickets(int userId, string userName, string ticketStatus)
        {
            DataSet dsData = PrepareDataForTickets();
            return dsData;
        }

        #region Data Preparation

        private DataSet PrepareDataForTickets()
        {
            DataSet dsDAta = new DataSet("Tickets");
            DataTable dtData = new DataTable("Tickets");
            return dsDAta;
        }

        #endregion

    }
}

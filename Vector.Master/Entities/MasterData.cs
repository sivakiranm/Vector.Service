using System;
using System.Collections.Generic;
using System.Text;

namespace Vector.Master.Entities
{
    public class MasterData
    {
        public class VactorMasterDataSearch
        {
            public string Action { get; set; }
            public Int64 Id { get; set; }
            public string SearchText { get; set; } 
            public Int64? UserId { get; set; }

        }

        public class VectorMasterData
        {
            public string Action { get; set; }
            public Int64 Id { get; set; }
            public Boolean Status { get; set; }
            public string commaSperatedIds { get; set; }
        }

        public class VectorInventoryDataSearch
        {
            public string Action { get; set; }
            public Int64 UserId { get; set; }
            public Boolean Status { get; set; }
            public string ActionId { get; set; }
            public string CommaSperatedIds { get; set; }
        }

        public class DocumentInfo
        {
            public string Action { get; set; }
            public Int64 Id { get; set; }
            public string Type { get; set; }
          
        }

        public class ProcessUpdateEntity
        {
            public string Action { get; set; }
            public Int64? TicketId { get; set; }
            public Int64? InvoiceId { get; set; }
            public Int64? TaskId { get; set; }

        }
    }
   
}

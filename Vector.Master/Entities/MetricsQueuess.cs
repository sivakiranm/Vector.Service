using System;
using System.Collections.Generic;
using System.Text;

namespace Vector.Master.Entities
{

    public class MeticsQueues
    {
        public List<MetricsQueues> MeticsQueuesInfo { get; set; }
    }


   public class MetricsQueues
    {
        public string Title { get; set; } 

        public string Rank { get; set; }

        public string Id { get; set; }

        public string Assignedto { get; set; }

        public string Subject { get; set; }

        public string Statusdesc { get; set; }

        public string Duedate { get; set; }

        public string Status { get; set; }

        public string AverageFRT { get; set; }
         
        public string AverageHandingTime { get; set;  }
         
        public string OfTicektsHandled { get; set; }

        public  string MyFRTVsOverallOrg { get; set; }

        public string MyAHTVsOverallOrg { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Vector.Dashboard.Entities
{
    public class Dashboard
    {

        public Int64 dashboardId { get; set; }
        public bool isPinned { get; set; }
        public string dashboardIcon { get; set; }
        public Int64 userId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string widgets { get; set; }
        public string layout { get; set; }
        public string dashboardColor { get; set; }

    }

    public class WorkforceUsers
    {
        public string workforceUsers { get; set; }

    }
}

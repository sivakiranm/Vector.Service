using System;
using System.Collections.Generic;
using System.Text;

namespace Vector.Dashboard.Entities
{

    public class Dashboards
    {
        public List<DashboardInfo> userDashboards { get; set; }

    }

    public class DashboardInfo
    {

        public Int64 dashboardId { get; set; }
        public string dashboardTitle { get; set; }
        public string colorCode { get; set; }
        public string dashboardFormat { get; set; }
        public string icon { get; set; }
        public bool isActive { get; set; }
        public bool isPinned { get; set; }
        public List<Widget> widgets { get; set; }

    }

    public class Widgets
    {
        public List<Widget> WidgetsInfo { get; set; }
    }

    public class Widget
    {
        public Int64 widgetId { get; set; }
        public string widgetTitle { get; set; }
    }

}

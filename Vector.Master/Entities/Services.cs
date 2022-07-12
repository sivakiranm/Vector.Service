using System;
using System.Collections.Generic;
using System.Text;

namespace Vector.Master.Entities
{

    public class ServicesInfo
    {
        public List<Services> ServicesInformation { get; set; }
    }


   public class Services
    {
        public string PropertyID { get; set; }
        public string Client { get; set; }

        public string Property { get; set; }

        public string Address { get; set; }

        public string CityName { get; set; }

        public string MyServices { get; set; }

        public string PickupSchedule { get; set; }

        public string HolidaySchedule { get; set; }



        public string Title { get; set; }

        public string value { get; set; }

        public string Totaltickets { get; set; }

        public string Ticketsthismonth { get; set; }

        public string  AverageFRT { get; set; }

        public string  AverageAHT { get; set; }

        public string Tickets { get; set; }

        public string AvgAHTmonth { get; set; }

        public string AvgFRTmonth { get; set; }
      
    }
}

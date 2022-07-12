using System;
using System.Collections.Generic;
using Vector.Master.Entities;

namespace Vector.Master.BusinessLayer
{
    public  class MetricsQueuesbl
    {
        public MeticsQueues GetMetrics()
        {
            MeticsQueues MeticsQueuesInformation = null;
            List<MetricsQueues> MeticsQueuesInfo = null;
            MetricsQueues MetricsQueuesProperties = null;

            try
            {

                MeticsQueuesInformation = new MeticsQueues();
                MeticsQueuesInfo = new List<MetricsQueues>();

                MetricsQueuesProperties = new MetricsQueues();

                MetricsQueuesProperties.Title = "Rank";
                MetricsQueuesProperties.Rank = "3";
                MetricsQueuesProperties.AverageFRT = "03:43 HH:MM";
                MetricsQueuesProperties.AverageHandingTime = "06:25 HH:MM";
                MetricsQueuesProperties.OfTicektsHandled = "386";
                MetricsQueuesProperties.MyFRTVsOverallOrg = "03:43 Vs 04:23 HH:MM";
                MetricsQueuesProperties.MyAHTVsOverallOrg = "06:25 Vs 08:12 HH:MM";

               MeticsQueuesInfo.Add(MetricsQueuesProperties);

                MeticsQueuesInformation.MeticsQueuesInfo = MeticsQueuesInfo;

            }
            catch (Exception)
            {

                throw;
            }

            return MeticsQueuesInformation;

        }


        public MeticsQueues GetQueuesOpentickets( string Userid, string Username,string  SearchInfo)
        {
            MeticsQueues MeticsQueuesInformation = null;
            List<MetricsQueues> MeticsQueuesInfo = null;
            MetricsQueues MetricsQueuesProperties = null;

            try
            {

                MeticsQueuesInformation = new MeticsQueues();
                MeticsQueuesInfo = new List<MetricsQueues>();

                MetricsQueuesProperties = new MetricsQueues();

                MetricsQueuesProperties.Id = "#0545";
                MetricsQueuesProperties.Assignedto = "Rosemary Juarez";
                MetricsQueuesProperties.Subject = "Setup account template";
                MetricsQueuesProperties.Statusdesc = "Data Assurance team working";
                MetricsQueuesProperties.Duedate = "01-16-2021";
                MetricsQueuesProperties.Status = "Open";
                MeticsQueuesInfo.Add(MetricsQueuesProperties);

                MetricsQueuesProperties.Id = "#0675";
                MetricsQueuesProperties.Assignedto = "Julian Jimenez";
                MetricsQueuesProperties.Subject = "Add new property with client Car Toys";
                MetricsQueuesProperties.Statusdesc = "Client Support team working";
                MetricsQueuesProperties.Duedate = "1/16/2021";
                MetricsQueuesProperties.Status = "Open";
                MeticsQueuesInfo.Add(MetricsQueuesProperties);

                MetricsQueuesProperties.Id = "#0578";
                MetricsQueuesProperties.Assignedto = "Danielle Lizotte";
                MetricsQueuesProperties.Subject = "Complete Negotiation";
                MetricsQueuesProperties.Statusdesc = "Contract Management team working";
                MetricsQueuesProperties.Duedate = "01-21-2021";
                MetricsQueuesProperties.Status = "In process";
                MeticsQueuesInfo.Add(MetricsQueuesProperties);

                MetricsQueuesProperties.Id = "#0588";
                MetricsQueuesProperties.Assignedto = "Cyndie Wessel";
                MetricsQueuesProperties.Subject = "Transmit Contract";
                MetricsQueuesProperties.Statusdesc = "Contract Management team working";
                MetricsQueuesProperties.Duedate = "01-16-2021";
                MetricsQueuesProperties.Status = "In process";
                MeticsQueuesInfo.Add(MetricsQueuesProperties);


                MetricsQueuesProperties.Id = "#0897";
                MetricsQueuesProperties.Assignedto = "Barb Reese";
                MetricsQueuesProperties.Subject = "Create Funding Statement";
                MetricsQueuesProperties.Statusdesc = "Data Assurance team working";
                MetricsQueuesProperties.Duedate = "01-16-2021";
                MetricsQueuesProperties.Status = "In process";
                MeticsQueuesInfo.Add(MetricsQueuesProperties);

                MetricsQueuesProperties.Id = "#0785";
                MetricsQueuesProperties.Assignedto = "Ashley Frost";
                MetricsQueuesProperties.Subject = "Add Admin fee to Accout template";
                MetricsQueuesProperties.Statusdesc = "Contract Management team working";
                MetricsQueuesProperties.Duedate = "01-14-2021";
                MetricsQueuesProperties.Status = "Resolved";
                MeticsQueuesInfo.Add(MetricsQueuesProperties);


                MetricsQueuesProperties.Id = "#0945";
                MetricsQueuesProperties.Assignedto = "Brehanna Lopez";
                MetricsQueuesProperties.Subject = "Finalize Funding Statement";
                MetricsQueuesProperties.Statusdesc = "Funding Statement Finalized";
                MetricsQueuesProperties.Duedate = "01-16-2021";
                MetricsQueuesProperties.Status = "Resolved";
                MeticsQueuesInfo.Add(MetricsQueuesProperties);


                MetricsQueuesProperties.Id = "#0235";
                MetricsQueuesProperties.Assignedto = "Barb Reese";
                MetricsQueuesProperties.Subject = "Setup account template";
                MetricsQueuesProperties.Statusdesc = "Data Assurance team working";
                MetricsQueuesProperties.Duedate = "01-16-2021";
                MetricsQueuesProperties.Status = "Resolved";
                MeticsQueuesInfo.Add(MetricsQueuesProperties);

                MetricsQueuesProperties.Id = "#0235";
                MetricsQueuesProperties.Assignedto = "Sharlene.Allen";
                MetricsQueuesProperties.Subject = "Add Purchase Card Contact";
                MetricsQueuesProperties.Statusdesc = "Vendor Management team working";
                MetricsQueuesProperties.Duedate = "01-16-2021";
                MetricsQueuesProperties.Status = "Abandoned";
                MeticsQueuesInfo.Add(MetricsQueuesProperties);

                MetricsQueuesProperties.Id = "#0235";
                MetricsQueuesProperties.Assignedto = "Kerrisha Lewis";
                MetricsQueuesProperties.Subject = "Share the Monthly bill summary report";
                MetricsQueuesProperties.Statusdesc = "Dumpster Added";
                MetricsQueuesProperties.Duedate = "01-16-2021";
                MetricsQueuesProperties.Status = "Completed";
                MeticsQueuesInfo.Add(MetricsQueuesProperties);

                MeticsQueuesInformation.MeticsQueuesInfo = MeticsQueuesInfo;

            }
            catch (Exception)
            {

                throw;
            }

            return MeticsQueuesInformation;

        }

        public MeticsQueues GetQueuesResolvedtickets( string Userid, string Username, string Searchinfo)
        {
            MeticsQueues MeticsQueuesInformation = null;
            List<MetricsQueues> MeticsQueuesInfo = null;
            MetricsQueues MetricsQueuesProperties = null;

            try
            {

                MeticsQueuesInformation = new MeticsQueues();
                MeticsQueuesInfo = new List<MetricsQueues>();

                MetricsQueuesProperties = new MetricsQueues();

                MetricsQueuesProperties.Id = "#0545";
                MetricsQueuesProperties.Assignedto = "Rosemary Juarez";
                MetricsQueuesProperties.Subject = "Vendor Management team working";
                MetricsQueuesProperties.Statusdesc = "Data Assurance team working";
                MetricsQueuesProperties.Duedate = "01-16-2021";
                MetricsQueuesProperties.Status = "Open";
                MeticsQueuesInfo.Add(MetricsQueuesProperties);

                MetricsQueuesProperties.Id = "#0675";
                MetricsQueuesProperties.Assignedto = "Julian Jimenez";
                MetricsQueuesProperties.Subject = "Abandoned";
                MetricsQueuesProperties.Statusdesc = "Client Support team working";
                MetricsQueuesProperties.Duedate = "1/16/2021";
                MetricsQueuesProperties.Status = "Open";
                MeticsQueuesInfo.Add(MetricsQueuesProperties);

                MetricsQueuesProperties.Id = "#0578";
                MetricsQueuesProperties.Assignedto = "Danielle Lizotte";
                MetricsQueuesProperties.Subject = "Complete Negotiation";
                MetricsQueuesProperties.Statusdesc = "Contract Management team working";
                MetricsQueuesProperties.Duedate = "01-21-2021";
                MetricsQueuesProperties.Status = "In process";
                MeticsQueuesInfo.Add(MetricsQueuesProperties);

                MetricsQueuesProperties.Id = "#0588";
                MetricsQueuesProperties.Assignedto = "Dumpster Added";
                MetricsQueuesProperties.Subject = "Transmit Contract";
                MetricsQueuesProperties.Statusdesc = "Contract Management team working";
                MetricsQueuesProperties.Duedate = "01-16-2021";
                MetricsQueuesProperties.Status = "Resolved";
                MeticsQueuesInfo.Add(MetricsQueuesProperties);


                MetricsQueuesProperties.Id = "#0897";
                MetricsQueuesProperties.Assignedto = "Lineitem Added";
                MetricsQueuesProperties.Subject = "Create Funding Statement";
                MetricsQueuesProperties.Statusdesc = "Data Assurance team working";
                MetricsQueuesProperties.Duedate = "01-16-2021";
                MetricsQueuesProperties.Status = "Resolved";
                MeticsQueuesInfo.Add(MetricsQueuesProperties);

                MetricsQueuesProperties.Id = "#0785";
                MetricsQueuesProperties.Assignedto = "Ashley Frost";
                MetricsQueuesProperties.Subject = "Add Admin fee to Accout template";
                MetricsQueuesProperties.Statusdesc = "Contract Management team working";
                MetricsQueuesProperties.Duedate = "01-14-2021";
                MetricsQueuesProperties.Status = "Resolved";
                MeticsQueuesInfo.Add(MetricsQueuesProperties);


                MetricsQueuesProperties.Id = "#0945";
                MetricsQueuesProperties.Assignedto = "Brehanna Lopez";
                MetricsQueuesProperties.Subject = "Finalize Funding Statement";
                MetricsQueuesProperties.Statusdesc = "Funding Statement Finalized";
                MetricsQueuesProperties.Duedate = "01-16-2021";
                MetricsQueuesProperties.Status = "Resolved";
                MeticsQueuesInfo.Add(MetricsQueuesProperties);


                MetricsQueuesProperties.Id = "#0235";
                MetricsQueuesProperties.Assignedto = "Barb Reese";
                MetricsQueuesProperties.Subject = "Setup account template";
                MetricsQueuesProperties.Statusdesc = "Data Assurance team working";
                MetricsQueuesProperties.Duedate = "01-16-2021";
                MetricsQueuesProperties.Status = "Resolved";
                MeticsQueuesInfo.Add(MetricsQueuesProperties);

                MetricsQueuesProperties.Id = "#0235";
                MetricsQueuesProperties.Assignedto = "Sharlene.Allen";
                MetricsQueuesProperties.Subject = "Add Purchase Card Contact";
                MetricsQueuesProperties.Statusdesc = "Vendor Management team working";
                MetricsQueuesProperties.Duedate = "01-16-2021";
                MetricsQueuesProperties.Status = "Abandoned";
                MeticsQueuesInfo.Add(MetricsQueuesProperties);

                MetricsQueuesProperties.Id = "#0235";
                MetricsQueuesProperties.Assignedto = "Kerrisha Lewis";
                MetricsQueuesProperties.Subject = "Share the Monthly bill summary report";
                MetricsQueuesProperties.Statusdesc = "Dumpster Added";
                MetricsQueuesProperties.Duedate = "01-16-2021";
                MetricsQueuesProperties.Status = "Completed";
                MeticsQueuesInfo.Add(MetricsQueuesProperties);

                MeticsQueuesInformation.MeticsQueuesInfo = MeticsQueuesInfo;

            }
            catch (Exception)
            {

                throw;
            }

            return MeticsQueuesInformation;

        }

    }
}

using System;
using System.Collections.Generic;
using Vector.Master.Entities;

namespace Vector.Master.BusinessLayer
{
    public   class Servicesbl
    {
        public ServicesInfo GetServicesProperty()
        {

            ServicesInfo ServicesInformation = null;
            List<Services> Servicesinfo = null;
            Services ServiceProperties = null;
            try
            {

                ServicesInformation = new ServicesInfo();
                Servicesinfo = new List<Services>();
                ServiceProperties = new Services();

                ServiceProperties.PropertyID = "P19634";
                ServiceProperties.Property = "Brant Rock Apartments";
                ServiceProperties.Client = "Oro Capital Advisors";
                ServiceProperties.Address = "11011 Harts Road";
                ServiceProperties.CityName = "Jacksonville";
                ServiceProperties.MyServices = "8 yd Dumpster SW, Pickup";
                ServiceProperties.PickupSchedule = "Mon,Tue,Thu,Sat";
                ServiceProperties.HolidaySchedule = "01-20-2021 (Martin Luther King Jr. Day)";
                Servicesinfo.Add(ServiceProperties);

                ServiceProperties.PropertyID = "P19636";
                ServiceProperties.Property = "Briar Club Apartments";
                ServiceProperties.Client = "Oro Capital Advisors";
                ServiceProperties.Address = "12906 Brant Rock Drive";
                ServiceProperties.CityName = "Houston";
                ServiceProperties.MyServices = "8 yd Dumpster SW, Pickup";
                ServiceProperties.PickupSchedule = "Mon,Tue,Thu,Sat";
                ServiceProperties.HolidaySchedule = "01-20-2021 (Martin Luther King Jr. Day)";
                Servicesinfo.Add(ServiceProperties);

                ServiceProperties.PropertyID = "P22200";
                ServiceProperties.Property = "Cinnamon Trails - Oro";
                ServiceProperties.Client = "Oro Capital Advisors";
                ServiceProperties.Address = "6355 Briar Patch Ln";
                ServiceProperties.CityName = "Memphis";
                ServiceProperties.MyServices = "8 yd Dumpster SW, Pickup";
                ServiceProperties.PickupSchedule = "Mon,Tue,Thu,Sat";
                ServiceProperties.HolidaySchedule = "01-20-2021 (Martin Luther King Jr. Day)";
                Servicesinfo.Add(ServiceProperties);

                ServiceProperties.PropertyID = "P22777";
                ServiceProperties.Property = "Columns Apartments";
                ServiceProperties.Client = "Oro Capital Advisors";
                ServiceProperties.Address = "3251 Knight Trails Cir,";
                ServiceProperties.CityName = "Memphis";
                ServiceProperties.MyServices = "";
                ServiceProperties.PickupSchedule = "";
                ServiceProperties.HolidaySchedule = "01-20-2021 (Martin Luther King Jr. Day)";
                Servicesinfo.Add(ServiceProperties);

                ServiceProperties.PropertyID = "P19949";
                ServiceProperties.Property = "Commerce Park Apartments";
                ServiceProperties.Client = "Oro Capital Advisors";
                ServiceProperties.Address = "333 Laurina St";
                ServiceProperties.CityName = "Memphis";
                ServiceProperties.MyServices = "30 yd Roll Off SW, Haul";
                ServiceProperties.PickupSchedule = "Mon,Tue,Thu,Sat";
                ServiceProperties.HolidaySchedule = "01-20-2021 (Martin Luther King Jr. Day)";
                Servicesinfo.Add(ServiceProperties);

                ServiceProperties.PropertyID = "P19635";
                ServiceProperties.Property = "Cross Creek Apartments - Oro Capital";
                ServiceProperties.Client = "Oro Capital Advisors";
                ServiceProperties.Address = "15330 Ella Boulevard";
                ServiceProperties.CityName = "Jacksonville";
                ServiceProperties.MyServices = "30 yd Roll Off SW, Haul";
                ServiceProperties.PickupSchedule = "";
                ServiceProperties.HolidaySchedule = "";
                Servicesinfo.Add(ServiceProperties);

                ServiceProperties.PropertyID = "456787";
                ServiceProperties.Property = "Cross Creek Apartments - Oro Capital";
                ServiceProperties.Client = "Oro Capital Advisors";
                ServiceProperties.Address = "1441 Manotak Ave";
                ServiceProperties.CityName = "Jacksonville";
                ServiceProperties.MyServices = "30 yd Roll Off SW, Haul";
                ServiceProperties.PickupSchedule = "Mon,Tue,Thu,Sat";
                ServiceProperties.HolidaySchedule = "";
                Servicesinfo.Add(ServiceProperties);

                ServiceProperties.PropertyID = "77644";
                ServiceProperties.Property = "Hampton Greens Apartments";
                ServiceProperties.Client = "Oro Capital Advisors";
                ServiceProperties.Address = "1441 Manotak Ave";
                ServiceProperties.CityName = "Dallas";
                ServiceProperties.MyServices = "30 yd Roll Off SW, Haul";
                ServiceProperties.PickupSchedule = "";
                ServiceProperties.HolidaySchedule = "";
                Servicesinfo.Add(ServiceProperties);

                ServiceProperties.PropertyID = "P567895";
                ServiceProperties.Property = "Heron Walk Apartments";
                ServiceProperties.Client = "Oro Capital Advisors";
                ServiceProperties.Address = "1441 Manotak Ave";
                ServiceProperties.CityName = "Dallas";
                ServiceProperties.MyServices = "30 yd Roll Off SW, Haul";
                ServiceProperties.PickupSchedule = "Mon,Tue,Thu,Sat";
                ServiceProperties.HolidaySchedule = "";
                Servicesinfo.Add(ServiceProperties);

                ServiceProperties.PropertyID = "P89098";
                ServiceProperties.Property = "Langtry Village Apartments";
                ServiceProperties.Client = "Oro Capital Advisors";
                ServiceProperties.Address = "400 Powers Avenue";
                ServiceProperties.CityName = "Columbus";
                ServiceProperties.MyServices = "30 yd Roll Off SW, Haul";
                ServiceProperties.PickupSchedule = "Mon,Tue,Thu,Sat";
                ServiceProperties.HolidaySchedule = "";
                Servicesinfo.Add(ServiceProperties);

                ServiceProperties.PropertyID = "P678907";
                ServiceProperties.Property = "ORO Island Club SPE Owner";
                ServiceProperties.Client = "Oro Capital Advisors";
                ServiceProperties.Address = "1565 Interstate 35 Business";
                ServiceProperties.CityName = "Columbus";
                ServiceProperties.MyServices = "";
                ServiceProperties.PickupSchedule = "";
                ServiceProperties.HolidaySchedule = "";
                Servicesinfo.Add(ServiceProperties);

                ServiceProperties.PropertyID = "P56789";
                ServiceProperties.Property = "ORO Island Club SPE Owner";
                ServiceProperties.Client = "Oro Capital Advisors";
                ServiceProperties.Address = "2225 Montego Blvd.";
                ServiceProperties.CityName = "Columbus";
                ServiceProperties.MyServices = "06 yd - Extra Pickup";
                ServiceProperties.PickupSchedule = "Mon,Tue,Thu,Sat";
                ServiceProperties.HolidaySchedule = "";
                Servicesinfo.Add(ServiceProperties);

                ServiceProperties.PropertyID = "P25678";
                ServiceProperties.Property = "ORO Island Club SPE Owner";
                ServiceProperties.Client = "Oro Capital Advisors";
                ServiceProperties.Address = "Columbus";
                ServiceProperties.CityName = "Dublin";
                ServiceProperties.MyServices = "";
                ServiceProperties.PickupSchedule = "";
                ServiceProperties.HolidaySchedule = "";
                Servicesinfo.Add(ServiceProperties);

                ServiceProperties.PropertyID = "P25190";
                ServiceProperties.Property = "River Trace Apartments";
                ServiceProperties.Client = "Oro Capital Advisors";
                ServiceProperties.Address = "3970 Brelsford Ln.";
                ServiceProperties.CityName = "Dublin";
                ServiceProperties.MyServices = "06 yd - Extra Pickup";
                ServiceProperties.PickupSchedule = "Mon,Tue,Thu,Sat";
                ServiceProperties.HolidaySchedule = "";
                Servicesinfo.Add(ServiceProperties);

                ServiceProperties.PropertyID = "P25157";
                ServiceProperties.Property = "River Trace Apartments";
                ServiceProperties.Client = "Oro Capital Advisors";
                ServiceProperties.Address = "3970 Brelsford Lane";
                ServiceProperties.CityName = "Dublin";
                ServiceProperties.MyServices = "";
                ServiceProperties.PickupSchedule = "";
                ServiceProperties.HolidaySchedule = "01-20-2021 (Martin Luther King Jr. Day)";
                Servicesinfo.Add(ServiceProperties);

                ServiceProperties.PropertyID = "P25136";
                ServiceProperties.Property = "River Trace Apartments";
                ServiceProperties.Client = "Oro Capital Advisors";
                ServiceProperties.Address = "5350 Silverthorne Rd.";
                ServiceProperties.CityName = "Dublin";
                ServiceProperties.MyServices = "06 yd - Extra Pickup";
                ServiceProperties.PickupSchedule = "Mon,Tue,Thu,Sat";
                ServiceProperties.HolidaySchedule = "01-20-2021 (Martin Luther King Jr. Day)";
                Servicesinfo.Add(ServiceProperties);

                ServiceProperties.PropertyID = "P25198";
                ServiceProperties.Property = "River Trace Apartments";
                ServiceProperties.Client = "Oro Capital Advisors";
                ServiceProperties.Address = "Dublin";
                ServiceProperties.CityName = "Dublin";
                ServiceProperties.MyServices = "";
                ServiceProperties.PickupSchedule = "";
                ServiceProperties.HolidaySchedule = "01-20-2021 (Martin Luther King Jr. Day)";
                Servicesinfo.Add(ServiceProperties);

                ServiceProperties.PropertyID = "P25156";
                ServiceProperties.Property = "River Trace Apartments";
                ServiceProperties.Client = "Oro Capital Advisors";
                ServiceProperties.Address = "300 Springboro";
                ServiceProperties.CityName = "Westerville";
                ServiceProperties.MyServices = "6 yd Dumpster SW, Pickup";
                ServiceProperties.PickupSchedule = "Mon,Tue,Thu,Sat";
                ServiceProperties.HolidaySchedule = "01-20-2021 (Martin Luther King Jr. Day)";
                Servicesinfo.Add(ServiceProperties);

                ServiceProperties.PropertyID = "P25167";
                ServiceProperties.Property = "River Trace Apartments";
                ServiceProperties.Client = "Oro Capital Advisors";
                ServiceProperties.Address = "2165 E River Trace Rd";
                ServiceProperties.CityName = "Westerville";
                ServiceProperties.MyServices = "6 yd Dumpster SW, Pickup";
                ServiceProperties.PickupSchedule = "Mon,Tue,Thu,Sat";
                ServiceProperties.HolidaySchedule = "01-20-2021 (Martin Luther King Jr. Day)";
                Servicesinfo.Add(ServiceProperties);

                ServiceProperties.PropertyID = "P25136";
                ServiceProperties.Property = "Ansley at Harts Road Apartments";
                ServiceProperties.Client = "Oro Capital Advisors";
                ServiceProperties.Address = "301 Caravan Circle";
                ServiceProperties.CityName = "Westerville";
                ServiceProperties.MyServices = "6 yd Dumpster SW, Pickup";
                ServiceProperties.PickupSchedule = "Mon,Tue,Thu,Sat";
                ServiceProperties.HolidaySchedule = "01-20-2021 (Martin Luther King Jr. Day)";
                Servicesinfo.Add(ServiceProperties);

                ServicesInformation.ServicesInformation = Servicesinfo;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return ServicesInformation;
        }



        public ServicesInfo GetServicesSummaryProperty(string month , string year )
        {

            ServicesInfo ServicesInformation = null;
            List<Services> Servicesinfo = null;
            Services ServiceProperties = null;
            try
            {

                ServicesInformation = new ServicesInfo();
                Servicesinfo = new List<Services>();
                ServiceProperties = new Services();



              
                ServiceProperties.Tickets= "10";
                ServiceProperties.AverageFRT = "40HH:MM";
                ServiceProperties.AverageAHT = "22HH:MM";
                ServiceProperties.AvgAHTmonth = "28";
                ServiceProperties.AvgFRTmonth = "2";
                ServiceProperties.Ticketsthismonth = "5";
               
                Servicesinfo.Add(ServiceProperties);

                ServicesInformation.ServicesInformation = Servicesinfo;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return ServicesInformation;
        }


        public ServicesInfo GetThreesixtydegreeviewProperty()
        {

            ServicesInfo ServicesInformation = null;
            List<Services> Servicesinfo = null;
            Services ServiceProperties = null;
            try
            {

                ServicesInformation = new ServicesInfo();
                Servicesinfo = new List<Services>();
                ServiceProperties = new Services();



                ServiceProperties.Title = "Total # of Tickets";
                ServiceProperties.value = "238";
                Servicesinfo.Add(ServiceProperties);

                ServiceProperties.Title = "Average FRT";
                ServiceProperties.value = "05:56 HH:MM";
                Servicesinfo.Add(ServiceProperties);

                ServiceProperties.Title = "Average AHT";
                ServiceProperties.value = "04:48 HH:MM";
                Servicesinfo.Add(ServiceProperties);


                ServiceProperties.Title = "Tickets This Month";
                ServiceProperties.value = "26";
                Servicesinfo.Add(ServiceProperties);

                ServiceProperties.Title = "Average AHT this month";
                ServiceProperties.value = "06:48 HH:MM";
                Servicesinfo.Add(ServiceProperties);

                ServiceProperties.Title = "Average FRT this Month";
                ServiceProperties.value = "05:16 HH:MM";
                Servicesinfo.Add(ServiceProperties);


                ServicesInformation.ServicesInformation = Servicesinfo;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return ServicesInformation;
        }















    }
}

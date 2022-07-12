using System;
using System.Collections.Generic;
using Vector.Master.Entities;

namespace Vector.Master.BusinessLayer
{
    public class CallstoactionBL
    {
        public CallstoactionInfo GetCallstoactionToDoInfoProperty(string Userid ,string  Username , string Searchinfo)
        {    

                  CallstoactionInfo Callstoactioninformation = null;
                  List<Callstoaction> CallstoactionInfo = null;
                  Callstoaction Callstoactoinpropperties = null;
        try{

                Callstoactioninformation = new CallstoactionInfo();
                CallstoactionInfo = new List<Callstoaction>();
                 Callstoactoinpropperties = new Callstoaction();


                Callstoactoinpropperties.Id = "#0391";
                Callstoactoinpropperties.Createduser = "Marc Savas";
                Callstoactoinpropperties.Description = "Approve Invoice";
                Callstoactoinpropperties.Duedate = "12/1/2021";
                Callstoactoinpropperties.Status = "Open";
                CallstoactionInfo.Add(Callstoactoinpropperties);

                Callstoactoinpropperties.Id = "#0421";
                Callstoactoinpropperties.Createduser = "Patrica John";
                Callstoactoinpropperties.Description = "Approve Funding Statement";
                Callstoactoinpropperties.Duedate = "01-15-2021";
                Callstoactoinpropperties.Status = "Open";
                CallstoactionInfo.Add(Callstoactoinpropperties);

                Callstoactoinpropperties.Id = "#0426";
                Callstoactoinpropperties.Createduser = "Sarah Smith";
                Callstoactoinpropperties.Description = "Extend Due Date Request";
                Callstoactoinpropperties.Duedate = "01-15-2021";
                Callstoactoinpropperties.Status = "Open";
                CallstoactionInfo.Add(Callstoactoinpropperties);

                Callstoactoinpropperties.Id = "#0526";
                Callstoactoinpropperties.Createduser = "Kristin Garrett";
                Callstoactoinpropperties.Description = "Approve Overage Charge";
                Callstoactoinpropperties.Duedate = "01-15-2021";
                Callstoactoinpropperties.Status = "Open";
                CallstoactionInfo.Add(Callstoactoinpropperties);

                Callstoactoinpropperties.Id = "#0542";
                Callstoactoinpropperties.Createduser = "Elizabeth Sakla";
                Callstoactoinpropperties.Description = "Approve Contract";
                Callstoactoinpropperties.Duedate = "01-15-2021";
                Callstoactoinpropperties.Status = "Closed";
                CallstoactionInfo.Add(Callstoactoinpropperties);

                Callstoactoinpropperties.Id = "#0195";
                Callstoactoinpropperties.Createduser = "Angie Gabel";
                Callstoactoinpropperties.Description = "Monthly Review Meting";
                Callstoactoinpropperties.Duedate = "01-15-2021";
                Callstoactoinpropperties.Status = "Closed";
                CallstoactionInfo.Add(Callstoactoinpropperties);

                Callstoactoinpropperties.Id = "#0321";
                Callstoactoinpropperties.Createduser = "Michele Llamas";
                Callstoactoinpropperties.Description = "Approve Invoice";
                Callstoactoinpropperties.Duedate = "01-15-2021";
                Callstoactoinpropperties.Status = "Closed";
                CallstoactionInfo.Add(Callstoactoinpropperties);

                Callstoactoinpropperties.Id = "#265";
                Callstoactoinpropperties.Createduser = "Patrica John";
                Callstoactoinpropperties.Description = "Approve Invoice";
                Callstoactoinpropperties.Duedate = "01-15-2021";
                Callstoactoinpropperties.Status = "Closed";
                CallstoactionInfo.Add(Callstoactoinpropperties);

                Callstoactioninformation.CallstoactionInformation = CallstoactionInfo;

            }
            catch (Exception ex)
            {

                throw ex;
          }
    return Callstoactioninformation;
    }

        public CallstoactionInfo GetCallstoactionCompletedInfoProperty(string Userid, string Username, string Searchinfo)
        {

            CallstoactionInfo Callstoactioninformation = null;
            List<Callstoaction> CallstoactionInfo = null;
            Callstoaction Callstoactoinpropperties = null;
            try
            {

                Callstoactioninformation = new CallstoactionInfo();
                CallstoactionInfo = new List<Callstoaction>();
                Callstoactoinpropperties = new Callstoaction();


                Callstoactoinpropperties.Id = "#0391";
                Callstoactoinpropperties.Createduser = "Patrica John";
                Callstoactoinpropperties.Description = "Monthly Review Meeting";
                Callstoactoinpropperties.Duedate = "12/1/2021";
                Callstoactoinpropperties.Status = "Open";
                CallstoactionInfo.Add(Callstoactoinpropperties);

                Callstoactoinpropperties.Id = "#0421";
                Callstoactoinpropperties.Createduser = "Patrica John";
                Callstoactoinpropperties.Description = "Approve Funding Statement";
                Callstoactoinpropperties.Duedate = "01-15-2021";
                Callstoactoinpropperties.Status = "Open";
                CallstoactionInfo.Add(Callstoactoinpropperties);

                Callstoactoinpropperties.Id = "#0426";
                Callstoactoinpropperties.Createduser = "Sarah Smith";
                Callstoactoinpropperties.Description = "Extend Due Date Request";
                Callstoactoinpropperties.Duedate = "01-15-2021";
                Callstoactoinpropperties.Status = "Open";
                CallstoactionInfo.Add(Callstoactoinpropperties);

                Callstoactoinpropperties.Id = "#0526";
                Callstoactoinpropperties.Createduser = "Kristin Garrett";
                Callstoactoinpropperties.Description = "Approve Overage Charge";
                Callstoactoinpropperties.Duedate = "01-15-2021";
                Callstoactoinpropperties.Status = "Open";
                CallstoactionInfo.Add(Callstoactoinpropperties);

                Callstoactoinpropperties.Id = "#0542";
                Callstoactoinpropperties.Createduser = "Elizabeth Sakla";
                Callstoactoinpropperties.Description = "Approve Contract";
                Callstoactoinpropperties.Duedate = "01-15-2021";
                Callstoactoinpropperties.Status = "Closed";
                CallstoactionInfo.Add(Callstoactoinpropperties);

                Callstoactoinpropperties.Id = "#0195";
                Callstoactoinpropperties.Createduser = "Angie Gabel";
                Callstoactoinpropperties.Description = "Monthly Review Meting";
                Callstoactoinpropperties.Duedate = "01-15-2021";
                Callstoactoinpropperties.Status = "Closed";
                CallstoactionInfo.Add(Callstoactoinpropperties);

                Callstoactoinpropperties.Id = "#0321";
                Callstoactoinpropperties.Createduser = "Michele Llamas";
                Callstoactoinpropperties.Description = "Approve Invoice";
                Callstoactoinpropperties.Duedate = "01-15-2021";
                Callstoactoinpropperties.Status = "Closed";
                CallstoactionInfo.Add(Callstoactoinpropperties);

                Callstoactoinpropperties.Id = "#265";
                Callstoactoinpropperties.Createduser = "Patrica John";
                Callstoactoinpropperties.Description = "Approve Invoice";
                Callstoactoinpropperties.Duedate = "01-15-2021";
                Callstoactoinpropperties.Status = "Closed";
                CallstoactionInfo.Add(Callstoactoinpropperties);

                Callstoactioninformation.CallstoactionInformation = CallstoactionInfo;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Callstoactioninformation;
        }

    }
}

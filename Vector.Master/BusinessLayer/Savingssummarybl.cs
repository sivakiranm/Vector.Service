using System;
using System.Collections.Generic;
using Vector.Master.Entities;

namespace Vector.Master.BusinessLayer
{
    public  class Savingssummarybl
    {
        public Savingssummaryinfo GetSavingsaummaryProperty()
        {

            Savingssummaryinfo SavingsaummaryInformation = null;
            List<Savingssummary> Savingssummaryinformation = null;
            Savingssummary Savingssummaryperties = null;
            try
            {

                SavingsaummaryInformation = new Savingssummaryinfo();
                Savingssummaryinformation = new List<Savingssummary>();


                Savingssummaryperties = new Savingssummary();
                Savingssummaryperties.Totalsavings = "44,338.98";
                Savingssummaryperties.Savingsthismnth = "3,786.98";
                Savingssummaryinformation.Add(Savingssummaryperties);

                SavingsaummaryInformation.Savingssummaryinformation = Savingssummaryinformation;
            } 
            catch (Exception ex)
            {

                throw ex;
            }
            return SavingsaummaryInformation;
        }


    }
}

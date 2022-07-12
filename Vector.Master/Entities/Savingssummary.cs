using System;
using System.Collections.Generic;
using System.Text;

namespace Vector.Master.Entities
{
  public   class Savingssummary
    {
        public string Totalsavings { get; set; }

        public string Savingsthismnth { get; set; }
    }

    public class Savingssummaryinfo
    {
        public List<Savingssummary> Savingssummaryinformation { get; set; }
    }
}

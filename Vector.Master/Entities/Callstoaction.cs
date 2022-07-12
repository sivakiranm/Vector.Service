using System;
using System.Collections.Generic;
using System.Text;

namespace Vector.Master.Entities
{

    public class CallstoactionInfo
    {
        public List<Callstoaction> CallstoactionInformation { get; set; }
    }


    public class Callstoaction
    {
        public string Id { get; set; }
        public string Createduser { get; set; }
        public string Description { get; set; }
        public string Duedate { get; set; }
        public string Status { get; set; }
    }
}

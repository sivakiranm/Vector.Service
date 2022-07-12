using System;
using System.Collections.Generic;
using System.Text;

namespace Vector.Master.Entities
{
    public class bottomfiveproperties
    {
        public string PropertyName { get; set; }
        public string Amount { get; set; }
    }

    public class bottomfivepropertiesInformation
    {
        public List<bottomfiveproperties> bottomfivepropertiesInfo { get; set; }
    }
}

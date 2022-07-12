using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vector.Garage.Entities
{ 
    public class ReportEntity
    {
        public string Action { get; set; }
        public Int64? ManifestId { get; set; }
        public string ManifestName { get; set; }
        public string FeatureName { get; set; }
        public Int64 FeatureId { get; set; }
        public Int64 UserId { get; set; } 
    }

    public class ConsoleEntity
    {
        public string Action { get; set; }
        public Int64? ManifestId { get; set; }
        public string ManifestName { get; set; }
        public string FeatureName { get; set; }
        public Int64 FeatureId { get; set; }
        public Int64 UserId { get; set; }
    }

}

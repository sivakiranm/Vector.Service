using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vector.Dashboard.Entities
{
   public class QuickLinks
    {
        public string Action { get; set; }
        public int? FeatureId { get; set; }
        public Int64? ManifestId { get; set; }
        public string Type { get; set; }
        public Int64? UserId { get; set; }

    }
}

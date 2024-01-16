using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crime_analysis_reporting_system.entity
{
    public class Evidence
    {
        public int EvidenceID { get; set; }

        public string Description { get; set; }

        public string LocationFound { get; set; }

        public int IncidentID { get; set; }
    }
}
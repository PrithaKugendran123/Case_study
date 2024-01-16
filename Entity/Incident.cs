using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crime_analysis_reporting_system.entity
{
    public class Incident
    {
        public int IncidentID { get; set; }

        public string IncidentType { get; set; }

        public string IncidentDate { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public int VictimID { get; set; }

        public int SuspectID { get; set; }
    }

}
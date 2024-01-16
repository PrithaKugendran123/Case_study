using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crime_analysis_reporting_system.entity
{
    public class Report
    {
        public int ReportID { get; set; }

        public int IncidentID { get; set; }

        public int ReportingOfficer { get; set; }

        public DateTime ReportDate { get; set; }

        public string ReportDetails { get; set; }

        public string Status { get; set; }

    }
}

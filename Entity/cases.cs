using Crime_analysis_reporting_system.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crime_Analysis_and_Reporting_System.Entity
{
    
        public class Case
        {
            public int caseId { get; set; }
            public string caseDescription { get; set; }

            public int IncidentID { get; set; }

        }
    
}

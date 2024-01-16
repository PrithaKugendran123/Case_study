using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crime_analysis_reporting_system.entity
{
    public class Officer
    {
        public int OfficerID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string BadgeNumber { get; set; }

        public string Rank { get; set; }

        public string ContactInformation { get; set; }

        public int AgencyID { get; set; }
    }
}

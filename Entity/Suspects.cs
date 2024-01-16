using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crime_analysis_reporting_system.entity
{
    public class Suspect
    {
        public int SuspectID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; }

        public string PhoneNumber { get; set; }
    }
}

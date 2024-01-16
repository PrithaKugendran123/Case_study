using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crime_Analysis_and_Reporting_System.Exceptions
{
    public class IncidentNumberNotFoundException : Exception
    {
        public IncidentNumberNotFoundException() : base("Incident number not found in the database.")
        {
        }

       
    }
}

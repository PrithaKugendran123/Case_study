using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Crime_Analysis_and_Reporting_System;
using Crime_analysis_reporting_system.entity;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Data.SqlClient;
using Crime_Analysis_and_Reporting_System.Entity;


namespace Crime_analysis_reporting_system.Dao
{
     public interface ICrimeAnalysisService
    {


        bool CreateIncident(Incident incident);

        bool UpdateIncidentStatus(int incidentId, String status);

       List<Incident> GeTIncidentsInDateRange(string startDate, string endDate);

        List<Incident> SearchIncidents(string IncidentType);
        

       List< Report> GenerateIncidentReport(String IncidentType);

        bool CreateCase(Case newCases, Incident incident);

        List<Case> GetCaseDetails(int caseId);

        bool UpdateCaseDetails(int caseID,string caseDescription);

       List<Case> GetAllCases();
        
    }

    

    
}


    

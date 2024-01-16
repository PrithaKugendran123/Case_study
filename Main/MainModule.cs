using Crime_Analysis_and_Reporting_System.Dao;
using Crime_Analysis_and_Reporting_System.Entity;
using Crime_Analysis_and_Reporting_System.Exceptions;
using Crime_analysis_reporting_system.Dao;
using Crime_analysis_reporting_system.entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crime_analysis_reporting_system.Main
{
    public class MainModule
    {
       
        static void Main(string[] args)
        {
            try {
                while (true)
                {
                    Console.WriteLine("=======================================");
                    Console.WriteLine("Crime analysis and reporting system");
                    Console.WriteLine("=======================================");
                    Console.WriteLine("Select an option:");
                    Console.WriteLine("1. Create a new incident");
                    Console.WriteLine("2. Update an incident status");
                    Console.WriteLine("3. Get incidents in a date range");
                    Console.WriteLine("4. Search incidents");
                    Console.WriteLine("5. Generate incident reports");
                    Console.WriteLine("6. Create case");
                    Console.WriteLine("7. Get Case detials");
                    Console.WriteLine("8. Update  case details");
                    Console.WriteLine("9. Get all the cases");
                    Console.WriteLine("10. Exit");

                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            CreateIncident();
                            break;
                        case "2":
                            UpdateIncidentStatus();
                            break;
                        case "3":
                            GeTIncidentsInDateRange();
                            break;
                        case "4":
                            SearchIncidents();
                            break;
                        case "5":
                            GenerateIncidentReport();
                            break;
                        case "6":
                            CreateCase();
                            break;
                        case "7":
                            GetCaseDetails();
                            break;
                        case "8":
                            UpdateCaseDetails();
                            break;
                        case "9":
                            GetAllCases();
                            break;
                        case "10":
                            Console.WriteLine("Exiting...");
                            return;
                        default:
                            Console.WriteLine("Invalid selection, please try again.");
                            break;
                    }
                }
            }
            catch (IncidentNumberNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }


        public static void CreateIncident()
        {
            ICrimeAnalysisService crimeAnalysisService = new CrimeAnalysisServiceImpl();

            try
            {
                Console.WriteLine("Enter the IncidentId, incident type, date, location, description, status, victimId, and suspect id to add into the Database");

                Incident incident = new Incident();
                incident.IncidentID = Convert.ToInt32(Console.ReadLine());

                incident.IncidentType = Console.ReadLine();
                incident.IncidentDate = Console.ReadLine();
                incident.Location = Console.ReadLine();
                incident.Description = Console.ReadLine();
                incident.Status = Console.ReadLine();
                incident.VictimID = Convert.ToInt32(Console.ReadLine());
                incident.SuspectID = Convert.ToInt32(Console.ReadLine());

                crimeAnalysisService.CreateIncident(incident);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while creating the incident: {ex.Message}");
            }
        }

        public static void UpdateIncidentStatus()
        {
            ICrimeAnalysisService crimeAnalysisService = new CrimeAnalysisServiceImpl();

            try
            {
                Console.WriteLine("Enter the incident status and incident ID");
                Incident incident = new Incident();
                incident.Status = Console.ReadLine();
                incident.IncidentID = Convert.ToInt32(Console.ReadLine());

                crimeAnalysisService.UpdateIncidentStatus(incident.IncidentID, incident.Status);
                Console.WriteLine("Incident status updated successfully.");
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Invalid input format. Please  enter a valid number for the incident ID.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while updating the incident status: {ex.Message}");
            }
        }

        public static List<Incident> GeTIncidentsInDateRange()
        {
            ICrimeAnalysisService crimeAnalysisService = new CrimeAnalysisServiceImpl();
            Console.WriteLine("Enter the start Date and End Date");

            string StartDate = Console.ReadLine();
            string EndDate = Console.ReadLine();
            List<Incident> incidents = crimeAnalysisService.GeTIncidentsInDateRange(StartDate, EndDate);

            foreach (Incident inc in incidents)
            {
                Console.WriteLine($"\nIncident ID: {inc.IncidentID}");
                Console.WriteLine($"Type: {inc.IncidentType}");
                Console.WriteLine($"Date: {inc.IncidentDate}");
                Console.WriteLine($"Location: {inc.Location}");
                Console.WriteLine($"Description: {inc.Description}");
                Console.WriteLine($"Status: {inc.Status}");
                Console.WriteLine($"Victim ID: {inc.VictimID}");
                Console.WriteLine($"Suspect ID: {inc.SuspectID}");
            }

            return incidents;


        }
        public static List<Incident> SearchIncidents()
        {
            ICrimeAnalysisService crimeAnalysisService = new CrimeAnalysisServiceImpl();

            Console.WriteLine("Enter the incident type");

            string IncidentType = Console.ReadLine();

            List<Incident> incidents = crimeAnalysisService.SearchIncidents(IncidentType);
            foreach (Incident incident in incidents)
            {
                Console.WriteLine($" Incident ID: {incident.IncidentID}\n Incident type: {incident.IncidentType}\n Incident date: {incident.IncidentDate}\n Incident Location: {incident.Location}\n Incident description: {incident.Description}\n Incident status: {incident.Status}\n Incident Victim ID:  {incident.VictimID}\n Incident SuspectID: {incident.SuspectID}");
            }
            return incidents;

        }
        public static List<Report> GenerateIncidentReport()
        {
            
            ICrimeAnalysisService crimeAnalysisService = new CrimeAnalysisServiceImpl();
            Console.WriteLine("enter the incident type");
            string IncidentType = Console.ReadLine();
            List<Report> reports = crimeAnalysisService.GenerateIncidentReport(IncidentType);
            foreach (Report report in reports)
            {
                Console.WriteLine($"Report ID: {report.ReportID}\nIncident ID: {report.IncidentID}\nReporting Officer: {report.ReportingOfficer}\nReport date: {report.ReportDate}\nReport details: {report.ReportDetails}\nReport status: {report.Status}");
            }
            return reports;
        }
        public static void CreateCase()
        {
            ICrimeAnalysisService crimeAnalysisService = new CrimeAnalysisServiceImpl();
            
            Console.WriteLine("enter the caseID, caseDescription");
            Case cases = new Case();
            cases.caseId = Convert.ToInt32(Console.ReadLine());
            cases.caseDescription = Console.ReadLine();
            Console.WriteLine("Enter the IncidentId, incident type,date,location,description,status,victimId and suspect id to add into the Database");

            Incident incident = new Incident();
            incident.IncidentID = Convert.ToInt32(Console.ReadLine());

            incident.IncidentType = Console.ReadLine();
            incident.IncidentDate = Console.ReadLine();
            incident.Location = Console.ReadLine();
            incident.Description = Console.ReadLine();
            incident.Status = Console.ReadLine();
            incident.VictimID = Convert.ToInt32(Console.ReadLine());
            incident.SuspectID = Convert.ToInt32(Console.ReadLine());
            crimeAnalysisService.CreateCase(cases,incident);
        }

       

        public static void UpdateCaseDetails()
        {
            ICrimeAnalysisService crimeAnalysisService = new CrimeAnalysisServiceImpl();
            Case cases = new Case();
            Console.WriteLine("Enter the case ID where the description need to be updated");
            cases.caseId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the case description for that ID");
            cases.caseDescription= Console.ReadLine();
            crimeAnalysisService.UpdateCaseDetails(cases.caseId, cases.caseDescription);

        }
        

       
        public static void GetAllCases()
        {
            try
            {
                ICrimeAnalysisService crimeAnalysisService = new CrimeAnalysisServiceImpl();
                List<Case> cases = crimeAnalysisService.GetAllCases();

                if (cases != null && cases.Count > 0)
                {
                    foreach (Case c in cases)
                    {
                        Console.WriteLine($"Case ID: {c.caseId},Case description: {c.caseDescription}, Incident ID: {c.IncidentID}");
                    }
                }
                else
                {
                    Console.WriteLine("No cases found.");
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
       
        public static void GetCaseDetails()
        {
            ICrimeAnalysisService crimeAnalysisService = new CrimeAnalysisServiceImpl();
            Console.WriteLine("enter the case id");

            int caseId = Convert.ToInt32(Console.ReadLine());
            List<Case> cases = crimeAnalysisService.GetCaseDetails(caseId);
            foreach(Case c in cases)
            {
                Console.WriteLine($"Case ID: {c.caseId},\nCase Description: {c.caseDescription},\nIncident ID: {c.caseId}");
            }

        }
       





}



}


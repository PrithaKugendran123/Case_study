using Crime_analysis_reporting_system.Dao;
using Crime_analysis_reporting_system.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crime_analysis_reporting_system.Main;
using Crime_Analysis_and_Reporting_System.Util;
using System.Data.Common;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.Data;
using Crime_Analysis_and_Reporting_System.Entity;
using Crime_Analysis_and_Reporting_System.Exceptions;

namespace Crime_Analysis_and_Reporting_System.Dao
{

   public class CrimeAnalysisServiceImpl : ICrimeAnalysisService
    {
        SqlConnection connection;
        public bool CreateIncident(Incident incident)
        {

            bool result = false;
            try
            {
                using (connection = DBConnection.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand($"insert into victims(VictimID) values ({incident.VictimID});\r\ninsert into Suspects(SuspectID) values ({incident.SuspectID})" +
                        $"\nINSERT INTO Incidents (IncidentID, IncidentType, IncidentDate, Location, Description, Status, VictimID, SuspectID)\r\nVALUES ({incident.IncidentID}, '{incident.IncidentType}', '{incident.IncidentDate}', '{incident.Location}', '{incident.Description}', '{incident.Status}', {incident.VictimID}, {incident.SuspectID});", connection);
                    connection.Open();
                    int rowAffected = cmd.ExecuteNonQuery();
                    if (rowAffected > 0)
                    {
                        result = true;
                        Console.WriteLine("successfully created");
                    }
                    else
                    {
                        Console.WriteLine("failed to create");
                    }
                }
            }
            catch(SqlException e)
            {
                Console.WriteLine("the given id is already present",e.Message);
            }

            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);

            }

            return result;

        }
        public bool UpdateIncidentStatus(int incidentId, string status)
        {
            bool result = false;
            try
            {
                SqlConnection connection = DBConnection.GetConnection();
                SqlCommand cmd = new SqlCommand($"update Incidents set status = '{status}' where IncidentID = {incidentId}", connection);

                connection.Open();

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    result = true;
                    Console.WriteLine("Update successful");
                }
                else
                {
                    throw new IncidentNumberNotFoundException();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating incident status: {ex.Message}");
            }
            return result;
        }


        public List<Incident> GeTIncidentsInDateRange(string startDate, string endDate)
        {
            List<Incident> incidents = new List<Incident>();
            try
            {
                using (connection = DBConnection.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = $"SELECT * FROM Incidents WHERE IncidentDate BETWEEN '{startDate}' AND '{endDate}'";

                        connection.Open();


                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                incidents.Add(new Incident()
                                {
                                    IncidentID = (int)dr[0],
                                    IncidentType = dr[1].ToString(),
                                    IncidentDate = dr[2].ToString(),
                                    Location = dr[3].ToString(),
                                    Description = dr[4].ToString(),
                                    Status = dr[5].ToString(),
                                    VictimID = (int)dr[6],
                                    SuspectID = (int)dr[7]
                                });
                            }
                            if (incidents.Count == 0)
                            {
                                Console.WriteLine("No records found between this date range");
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return incidents;
        }

        public List<Incident> SearchIncidents(String IncidentType)
        {
            List<Incident> incidents = new List<Incident>();
            try
            {
                using (connection = DBConnection.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = $"select*from incidents where incidentType ='{IncidentType}'";
                    connection.Open();

                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        incidents.Add(new Incident()
                        {
                            IncidentID = (int)dr[0],
                            IncidentType = dr[1].ToString(),
                            IncidentDate = dr[2].ToString(),
                            Location = dr[3].ToString(),
                            Description = dr[4].ToString(),
                            Status = dr[5].ToString(),
                            VictimID = (int)dr[6],
                            SuspectID = (int)dr[7]
                        });
                    }
                    dr.Close();
                    if (incidents.Count == 0)
                    {
                        Console.WriteLine("No such Incident type found. Try again please");
                    }

                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return incidents;

        }
        public List<Report> GenerateIncidentReport(String IncidentType)
        {
            List<Report> reports = new List<Report>();

            using (connection = DBConnection.GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"select*from reports join Incidents on reports.IncidentID=Incidents.IncidentID where IncidentType='{IncidentType}'";
                connection.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        reports.Add(new Report()
                        {

                            ReportID = Convert.ToInt32(dr["ReportID"]),
                            IncidentID = Convert.ToInt32(dr["IncidentID"]),
                            ReportingOfficer = Convert.ToInt32(dr["ReportingOfficer"]),
                            ReportDate = Convert.ToDateTime(dr["ReportDate"]),
                            ReportDetails = Convert.ToString(dr["ReportDetails"]),
                            Status = Convert.ToString(dr["Status"])
                        });

                    }
                    if (reports.Count == 0)
                    {
                        Console.WriteLine("No such Incident type is found, try again please");
                    }
                    dr.Close();
                    connection.Close();
                    return reports;
                }

            }

        }
        bool ICrimeAnalysisService.CreateCase(Case newCases, Incident incident)
        {
            bool result = false;
            try
            {
                using (connection = DBConnection.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand($"insert into Victims(VictimID) values ({incident.VictimID})" +
                        $"insert into Suspects(SuspectID) values ({incident.SuspectID})  insert into Incidents  values({incident.IncidentID}, '{incident.IncidentType} ', '{incident.IncidentDate} ', '{incident.Location} ', '{incident.Description} ', '{incident.Status} ', {incident.VictimID}, {incident.SuspectID})" +
                        $"insert into cases values ({newCases.caseId},'{newCases.caseDescription}',{incident.IncidentID})"
                       , connection);
                    connection.Open();
                    int rowAffected = cmd.ExecuteNonQuery();
                    if (rowAffected > 0)
                    {
                        result = true;
                        Console.WriteLine("successfully created");
                    }
                    else
                    {
                        Console.WriteLine("failed to create");
                    }
                }
            }

            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            return result;
        }




        List<Case> ICrimeAnalysisService.GetCaseDetails(int caseId)
        {

            List<Case> cases = new List<Case>();

            using (connection = DBConnection.GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"select*from cases where caseId={caseId}";
                connection.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cases.Add(new Case()
                    {
                        caseId = (int)dr[0],
                        caseDescription = dr[1].ToString()



                    });

                   
                }dr.Close();
                if (cases.Count == 0)
                {
                    Console.WriteLine("No such case ID found,try again please");
                }
                return cases;

            }
        }





        public bool UpdateCaseDetails(int caseId, string caseDescription)
        {
            bool result = false;
            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {


                    using (SqlCommand cmd = new SqlCommand($"update cases set caseDescription = '{caseDescription}'where caseID = {caseId}", connection))
                    {
                        connection.Open();

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            result = true;
                            Console.WriteLine("Update successful");
                        }
                        else
                        {
                            result = false;
                            Console.WriteLine("This case ID is not found");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating case status: {ex.Message}");

            }
            return result;
        }
        


  


       
      


        List<Case> ICrimeAnalysisService.GetAllCases()
        {
            List<Case> cases = new List<Case>();

            using (var connection = DBConnection.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM cases;", connection);
                try
                {
                    connection.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            cases.Add(new Case()
                            {
                                caseId = (int)dr[0],
                                caseDescription = dr[1].ToString(),
                                IncidentID = (int)dr[2]
                            });
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);

                }
            }

            return cases;
        }

      
    }



    }


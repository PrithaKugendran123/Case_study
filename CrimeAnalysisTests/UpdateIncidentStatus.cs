using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Crime_analysis_reporting_system.entity;
using Crime_Analysis_and_Reporting_System.Dao;
using Crime_analysis_reporting_system.Dao;

namespace CrimeManagementNew.Test
{
    [TestFixture]
    public class CrimeAnalysisServiceTests
    {
        private ICrimeAnalysisService crimeAnalysisService;

        [SetUp]
        public void Setup()
        {
            crimeAnalysisService = new CrimeAnalysisServiceImpl();
        }

        [Test]
        public void UpdateIncidentStatusValid()
        {
            // Arrange
            int incidentId = 6;
            string status = "Closed";

            // Act
            bool result = crimeAnalysisService.UpdateIncidentStatus(incidentId, status);

            // Assert
            Assert.IsTrue(result, "UpdateIncidentStatus should return true for a valid value.");
        }

        [Test]
        public void UpdateIncidentStatusInvalid()
        {
            // Arrange
            int incidentId = -1;
            string status = "Closed";

            // Act
            bool result = crimeAnalysisService.UpdateIncidentStatus(incidentId, status);

            // Assert
            Assert.IsFalse(result, "UpdateIncidentStatus should return false for an invalid value.");
        }

    }
}

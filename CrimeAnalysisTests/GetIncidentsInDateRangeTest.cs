using System;
using System.Collections.Generic;
using Crime_Analysis_and_Reporting_System.Dao;
using Crime_analysis_reporting_system.Dao;
using Crime_analysis_reporting_system.entity;
using System.Data.SqlClient;
using NUnit.Framework;

namespace CrimeManagementNew.GetIncidentsInDateRange
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
        public void GetIncidentsInDateRange()
        {

            DateTime startDate = DateTime.Now.AddDays(10);
            DateTime endDate = DateTime.Now;

            IEnumerable<Incident> result = crimeAnalysisService.GeTIncidentsInDateRange(startDate, endDate);

            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }
    }
}

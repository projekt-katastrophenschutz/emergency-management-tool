// <copyright file="EndToEndTest.cs" company="FT Software">
// Copyright (c) 2016. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace BSA.Core.Tests
{
    using BSA.Core.Services;
    using Database.Management;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class EndToEndTest
    {
        [TestMethod]
        public void TestDatabaseServices()
        {
            using (var databaseContext = new DatabaseContext())
            {
                databaseContext.Database.Migrate();
            }

            var jobService = new DatabaseJobService();
            var jobs = jobService.GetAllJobs();
        }
    }
}
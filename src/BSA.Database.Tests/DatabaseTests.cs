// <copyright file="DatabaseTests.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace BSA.Database.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using BSA.Database.Entities;
    using BSA.Database.Management;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    
    [TestClass]
    public class DatabaseTests
    {
        [TestInitialize]
        public void TestStartup()
        {
            using (var databaseContext = new DatabaseContext())
            {
                databaseContext.Database.Migrate();

                if (databaseContext.Forces.Any())
                {
                    throw new NotSupportedException("Die Datenbank muss derzeit noch händisch gesichert und gelöscht werden, um Datenverlust zu vermeiden.");
                }
                
                databaseContext.Forces.Add(SampleData.SampleForces.SampleForce1);
                databaseContext.Forces.Add(SampleData.SampleForces.SampleForce2);
                databaseContext.Forces.Add(SampleData.SampleForces.SampleForce3);
                databaseContext.SaveChanges();
            }
        }

        [TestMethod]
        public async Task CreateLogs()
        {
            // Create a new log entry
            DbLogEntry logEntry = new DbLogEntry
            {
                JsonContent = "Test value",
                Timestamp = DateTime.Now
            };

            DbLogEntry lastLogEntryBeforeCommit;

            // Commit new log entry to database
            using (var databaseContext = new DatabaseContext())
            {
                lastLogEntryBeforeCommit = await databaseContext.LogEntries.LastOrDefaultAsync();
                databaseContext.LogEntries.Add(logEntry);
                databaseContext.SaveChanges();
            }
            
            // Retrieve newly created log entry
            using (var databaseContext = new DatabaseContext())
            {
                var lastLogEntry = await databaseContext.LogEntries.LastAsync();
                Assert.IsNotNull(lastLogEntry);

                if (lastLogEntryBeforeCommit == null)
                {
                    Assert.AreEqual(1, lastLogEntry.Id, "Default id does not match the expected default value (=1)");
                }
                else
                {
                    Assert.AreEqual(++lastLogEntryBeforeCommit.Id, lastLogEntry.Id, "Auto increment failed; expected a single increment for Id");
                }
            }
        }

        [TestMethod]
        public async Task TestComplexObjectPersistence()
        {
            var dbJob = SampleData.SampleJobs.SampleJob1;
            DbJob lastJobBeforeAdd = null;

            using (var databaseContext = new DatabaseContext())
            {
                if (databaseContext.Jobs != null && databaseContext.Jobs.Any())
                    lastJobBeforeAdd = await databaseContext.Jobs.LastOrDefaultAsync();

                databaseContext.Jobs.Add(dbJob);
                await databaseContext.SaveChangesAsync();
            }

            using (var databaseContext = new DatabaseContext())
            {
                // ToDo: Remove temporary DbSet iterations due to a bug in EF Core 1.0 RC2
                foreach (var force in databaseContext.Forces)
                {
                }

                foreach (var location in databaseContext.JobLocations)
                {
                }

                foreach (var relation in databaseContext.ForceJobRelations)
                {
                }

                foreach (var job in databaseContext.Jobs)
                {
                }

                var lastJobAfterAdd = await databaseContext.Jobs.LastOrDefaultAsync();
                Assert.AreEqual(lastJobAfterAdd.Forces?.Count, dbJob.Forces.Count);
                Assert.AreEqual(lastJobAfterAdd.Location.Id, dbJob.Location.Id);
            }
        }
    }
}
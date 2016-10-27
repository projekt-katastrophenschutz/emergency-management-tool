// <copyright file="DatabaseJobService.cs" company="FT Software">
// Copyright (c) 2016. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace BSA.Core.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using BSA.Core.Messages;
    using BSA.Core.Models;
    using BSA.Database.Management;
    using BSA.Database.Utils;
    using GalaSoft.MvvmLight.Messaging;
    using Microsoft.EntityFrameworkCore;

    public class DatabaseJobService : IJobService
    {
        public List<Job> GetAllJobs()
        {
            using (var databaseContext = new DatabaseContext())
            {
                databaseContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                var jobs = databaseContext.Jobs.AsNoTracking()
                    .Include(job => job.Location)
                    .Include(job => job.Location.History)
                    .Include(job => job.History)
                    .Include(job => job.Forces)
                    .ToList();

                return jobs.Select(x => new Job(x)).ToList();
            }
        }

        public void SaveJob(Job job)
        {
            using (var databaseContext = new DatabaseContext())
            {
                databaseContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                databaseContext.UpdateOrAddDatabaseEntry(databaseContext.Jobs, job);
            }

            // ToDo: Proper state handling. Who sets the IsNew flag to false?
            if (job.IsNew)
            {
                job.IsNew = false;
                Messenger.Default.Send(new JobAddedMessage(job));
            }
            else
            {
                Messenger.Default.Send(new JobUpdatedMessage(job));
            }

            this.SendHistoryItemMessage(job);
        }

        public void DeleteJob(Job job)
        {
            this.DeleteJob(job, true);
        }

        public void DeleteJob(Job job, bool sendHistory)
        {
            using (var databaseContext = new DatabaseContext())
            {
                databaseContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                databaseContext.RemoveDatabaseEntry(databaseContext.Jobs, job.Id);
            }

            Messenger.Default.Send(new JobDeletedMessage(job));
            if (sendHistory)
            {
                this.SendHistoryItemMessage(job);
            }
        }

        public void ClearData()
        {
            this.GetAllJobs().ForEach(job => this.DeleteJob(job, false));
        }

        private void SendHistoryItemMessage(Job job)
        {
            var lastHistoryItem = job.History.LastOrDefault();
            if (lastHistoryItem != null)
            {
                Messenger.Default.Send(new HistoryItemMessage(lastHistoryItem, job.Id, HistoryItemType.Job));
            }
        }
    }
}
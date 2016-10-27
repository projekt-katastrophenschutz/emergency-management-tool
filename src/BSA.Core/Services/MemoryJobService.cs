// <copyright file="MemoryJobService.cs" company="FT Software">
// Copyright (c) 2016. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace BSA.Core.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using BSA.Core.Messages;
    using BSA.Core.Models;
    using GalaSoft.MvvmLight.Messaging;

    public class MemoryJobService : IJobService
    {
        private readonly List<Job> jobs = new List<Job>();

        public List<Job> GetAllJobs()
        {
            return this.jobs;
        }

        public void SaveJob(Job job)
        {
            var existingJob = this.jobs.SingleOrDefault(j => j.Id == job.Id);
            if (existingJob != null)
            {
                this.jobs.Remove(existingJob);
            }

            this.jobs.Add(job);

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
            this.jobs.Remove(job);
            Messenger.Default.Send(new JobDeletedMessage(job));
            this.SendHistoryItemMessage(job);
        }

        public void ClearData()
        {
            this.jobs.Clear();
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
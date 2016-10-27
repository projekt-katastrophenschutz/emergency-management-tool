// <copyright file="ConnectionJobService.cs" company="FT Software">
// Copyright (c) 2016. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace BSA.Core.Services
{
    using System.Collections.Generic;
    using BSA.Core.Models;
    using BSA.Core.Network;

    public class ConnectionJobService : IJobService
    {
        private readonly IJobService jobService;

        public ConnectionJobService(IJobService jobService)
        {
            this.jobService = jobService;
        }

        public void DeleteJob(Job job)
        {
            if (BsaContext.GetUserRole() != ApplicationRole.Standalone)
            {
                CommunicationProxy.SendJobDeleted(job);
            }

            if (BsaContext.GetUserRole() != ApplicationRole.Client)
            {
                this.DeleteJobLocal(job);
            }
            
        }

        public void ClearData()
        {
            this.jobService.ClearData();
        }

        public List<Job> GetAllJobs()
        {
            return this.GetAllJobsLocal();
        }

        public void SaveJob(Job job)
        {
            if (BsaContext.GetUserRole() != ApplicationRole.Standalone)
            {
                CommunicationProxy.SendJobChange(job);
            }

            if (BsaContext.GetUserRole() != ApplicationRole.Client)
            {
                this.SaveJobLocal(job);
            }
        }

        public List<Job> GetAllJobsLocal()
        {
            return this.jobService.GetAllJobs();
        }

        public void SaveJobLocal(Job job)
        {
            this.jobService.SaveJob(job);
        }

        public void DeleteJobLocal(Job job)
        {
            this.jobService.DeleteJob(job);
        }
    }
}
// <copyright file="SampleDataJobService.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace BSA.Core.Services
{
    using System;
    using System.Collections.Generic;
    using BSA.Core.Models;
    using BSA.Database.SampleData;

    public class SampleDataJobService : IJobService
    {
        public List<Job> GetAllJobs()
        {
            return new List<Job>
            {
                new Job(SampleJobs.SampleJob1)
            };
        }

        public void SaveJob(Job job)
        {
            throw new NotImplementedException();
        }

        public void DeleteJob(Job job)
        {
            throw new NotImplementedException();
        }

        public void ClearData()
        {
            throw new NotImplementedException();
        }
    }
}
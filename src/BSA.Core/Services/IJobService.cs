// <copyright file="IJobService.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace BSA.Core.Services
{
    using System.Collections.Generic;
    using BSA.Core.Models;

    public interface IJobService
    {
        List<Job> GetAllJobs();

        void SaveJob(Job job);

        void DeleteJob(Job job);

        void ClearData();
    }
}
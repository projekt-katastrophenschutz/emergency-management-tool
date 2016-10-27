// <copyright file="SampleDataForcesService.cs" company="FT Software">
// Copyright (c) 2016. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace BSA.Core.Services
{
    using System;
    using System.Collections.Generic;
    using BSA.Core.Models;
    using BSA.Database.SampleData;

    public class SampleDataForcesService : IForcesService
    {
        public List<Force> GetAllForces()
        {
            return new List<Force>
            {
                new Force(SampleForces.SampleForce1),
                new Force(SampleForces.SampleForce2),
                new Force(SampleForces.SampleForce3)
            };
        }

        public void SaveForces(Force forces)
        {
            throw new NotImplementedException();
        }

        public void DeleteForces(Force forces)
        {
            throw new NotImplementedException();
        }

        public List<Job> GetJobsForForces(Force forces)
        {
            throw new NotImplementedException();
        }

        public void ClearData()
        {
            throw new NotImplementedException();
        }
    }
}
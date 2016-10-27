// <copyright file="IForcesService.cs" company="FT Software">
// Copyright (c) 2016. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace BSA.Core.Services
{
    using System.Collections.Generic;
    using BSA.Core.Models;

    public interface IForcesService
    {
        List<Force> GetAllForces();

        void SaveForces(Force forces);

        void DeleteForces(Force forces);

        List<Job> GetJobsForForces(Force forces);

        void ClearData();
    }
}
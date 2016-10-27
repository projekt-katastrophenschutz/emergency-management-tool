// <copyright file="ConnectionForcesService.cs" company="FT Software">
// Copyright (c) 2016. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace BSA.Core.Services
{
    using System.Collections.Generic;
    using BSA.Core.Models;
    using BSA.Core.Network;

    public class ConnectionForcesService : IForcesService
    {
        private readonly IForcesService forcesService;

        public ConnectionForcesService(IForcesService forcesService)
        {
            this.forcesService = forcesService;
        }

        public void DeleteForces(Force forces)
        {
            if (BsaContext.GetUserRole() != ApplicationRole.Standalone)
            {
                CommunicationProxy.SendForceDeleted(forces);
            }

            if (BsaContext.GetUserRole() != ApplicationRole.Client)
            {
                this.DeleteForcesLocal(forces);
            }
        }

        public List<Force> GetAllForces()
        {
            return this.GetAllForcesLocal();
        }

        public List<Job> GetJobsForForces(Force forces)
        {
            return this.GetJobsForForcesLocal(forces);
        }

        public void ClearData()
        {
            this.forcesService.ClearData();
        }

        public void SaveForces(Force forces)
        {
            if (BsaContext.GetUserRole() != ApplicationRole.Standalone)
            {
                CommunicationProxy.SendForceChanged(forces);
            }

            if (BsaContext.GetUserRole() != ApplicationRole.Client)
            {
                this.SaveForcesLocal(forces);
            }

        }

        public void DeleteForcesLocal(Force forces)
        {
            this.forcesService.DeleteForces(forces);
        }

        public List<Force> GetAllForcesLocal()
        {
            return this.forcesService.GetAllForces();
        }

        public List<Job> GetJobsForForcesLocal(Force forces)
        {
            return this.forcesService.GetJobsForForces(forces);
        }

        public void SaveForcesLocal(Force forces)
        {
            this.forcesService.SaveForces(forces);
        }
    }
}
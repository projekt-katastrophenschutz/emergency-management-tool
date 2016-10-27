// <copyright file="DatabaseForcesService.cs" company="FT Software">
// Copyright (c) 2016. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace BSA.Core.Services
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using BSA.Core.Messages;
    using BSA.Core.Models;
    using BSA.Core.Utils;
    using BSA.Database.Entities;
    using BSA.Database.Management;
    using BSA.Database.Utils;
    using GalaSoft.MvvmLight.Messaging;
    using Microsoft.EntityFrameworkCore;

    public class DatabaseForcesService : IForcesService
    {
        public List<Force> GetAllForces()
        {
            using (var databaseContext = new DatabaseContext())
            {
                var forces = databaseContext.Forces.AsNoTracking()
                    //.Include(force => force.Leader)
                    //.Include(force => force.Leader.History)
                    .Include(force => force.Jobs)
                    .Include(force => force.Persons)
                    .Include(force => force.History)
                    .ToList();

                return forces.Select(x => new Force(x)).ToList();
            }
        }

        public void SaveForces(Force force)
        {
            using (var databaseContext = new DatabaseContext())
                databaseContext.UpdateOrAddDatabaseEntry(databaseContext.Forces, force);

            // ToDo: Proper state handling. Who sets the IsNew flag to false?
            if (force.IsNew)
            {
                force.IsNew = false;
                Messenger.Default.Send(new ForceAddedMessage(force));
            }
            else
            {
                Messenger.Default.Send(new ForceUpdatedMessage(force));
            }

            this.SendHistoryItemMessage(force);
        }

        public void DeleteForces(Force force)
        {
            this.DeleteForces(force, true);
        }

        public void DeleteForces(Force force, bool sendHistory)
        {
            using (var databaseContext = new DatabaseContext())
                databaseContext.RemoveDatabaseEntry(databaseContext.Forces, force.Id);

            Messenger.Default.Send(new ForceDeletedMessage(force));
            if (sendHistory)
            {
                this.SendHistoryItemMessage(force);
            }
        }

        [SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
        public List<Job> GetJobsForForces(Force force)
        {
            // ToDo: Investigate LINQ select issues with EF Core 1.0 RC2
            using (var databaseContext = new DatabaseContext())
            {
                databaseContext.TempDatabaseIteration();
                var resultList = new List<Job>();
                foreach (var job in databaseContext.Jobs)
                {
                    if (job.Forces != null)
                    {
                        foreach (var forceJobRelation in job.Forces)
                        {
                            if (forceJobRelation.ForceId == force.Id)
                            {
                                resultList.Add(new Job(job));
                            }
                        }
                    }
                }

                ////foreach (var dbForce in databaseContext.Forces)
                ////{
                ////    if (dbForce.Id == force.Id)
                ////    {
                ////        foreach (var forceJobRelation in dbForce.Jobs)
                ////        {
                ////            resultList.Add(new Job(forceJobRelation.Job));
                ////        }
                ////    }
                ////}

                return resultList;

                //return databaseContext.Jobs
                //    .Where(job => job.Forces.Exists(y => y.Id == forces.Id))
                //    .Select(dbJob => new Job(dbJob)).ToList();
            }
        }

        public void ClearData()
        {
            this.GetAllForces().ForEach(force => this.DeleteForces(force, false));
        }

        private void SendHistoryItemMessage(Force force)
        {
            var lastHistoryItem = force.History.LastOrDefault();
            if (lastHistoryItem != null)
            {
                Messenger.Default.Send(new HistoryItemMessage(lastHistoryItem, force.Id, HistoryItemType.Forces));
            }
        }
    }
}
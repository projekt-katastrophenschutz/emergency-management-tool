// <copyright file="MemoryForcesService.cs" company="FT Software">
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
    using System;
    public class MemoryForcesService : IForcesService
    {
        private readonly List<Force> forces = new List<Force>();
        private readonly IJobService jobService;

        public MemoryForcesService(IJobService jobService)
        {
            this.jobService = jobService;
            this.InitializeSampleData();
        }

        public List<Force> GetAllForces()
        {
            return this.forces;
        }

        public void SaveForces(Force force)
        {
            var existingForce = this.forces.SingleOrDefault(f => f.Id == force.Id);
            if (existingForce != null)
            {
                this.forces.Remove(existingForce);
            }

            this.forces.Add(force);

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
            this.forces.Remove(force);
            Messenger.Default.Send(new ForceDeletedMessage(force));
            this.SendHistoryItemMessage(force);
        }

        public List<Job> GetJobsForForces(Force force)
        {
            return this.jobService.GetAllJobs()
                .Where(job => job.Forces != null)
                .Where(job => job.Forces.Contains(force.Id)).ToList();
        }

        public void ClearData()
        {
            this.forces.Clear();
        }

        private void SendHistoryItemMessage(Force force)
        {
            var lastHistoryItem = force.History.LastOrDefault();
            if (lastHistoryItem != null)
            {
                Messenger.Default.Send(new HistoryItemMessage(lastHistoryItem, force.Id, HistoryItemType.Forces));
            }
        }

        private void InitializeSampleData()
        {
            this.forces.Add(
                new Force()
                {
                    Id = Guid.NewGuid(),
                    History = new List<HistoryItem>(),
                    IsNew = false,
                    Leader = new Person()
                    {
                        Prename = "Hans",
                        Surname = "Müller"
                    },
                    Name = "Florian Musterhausen 40/1",
                    RadioName = "Florian Musterhausen 40/1",
                    Vehicle = "LF16/12 (MU-AB 123)"
                });
            this.forces.Add(
                new Force()
                {
                    Id = Guid.NewGuid(),
                    History = new List<HistoryItem>(),
                    IsNew = false,
                    Leader = new Person()
                    {
                        Prename = "Gustav",
                        Surname = "Steinmeier"
                    },
                    Name = "Florian Musterhausen 31/1",
                    RadioName = "Florian Musterhausen 31/1",
                    Vehicle = "DLA (K) 18-12 (MU-BA 234)"
                });
            this.forces.Add(
                new Force()
                {
                    Id = Guid.NewGuid(),
                    History = new List<HistoryItem>(),
                    IsNew = false,
                    Leader = new Person()
                    {
                        Prename = "Jürgen",
                        Surname = "Taube"
                    },
                    Name = "Florian Musterhausen 61/1",
                    RadioName = "Florian Musterhausen 61/1",
                    Vehicle = "Rüstwagen (MU-CD 345)"
                });
            this.forces.Add(
                new Force()
                {
                    Id = Guid.NewGuid(),
                    History = new List<HistoryItem>(),
                    IsNew = false,
                    Leader = new Person()
                    {
                        Prename = "Torben",
                        Surname = "Poll"
                    },
                    Name = "Florian Neustadt 40/1",
                    RadioName = "Florian Neustadt 40/1",
                    Vehicle = "LF16/12 (NEU-AB 123)"
                });
            this.forces.Add(
                new Force()
                {
                    Id = Guid.NewGuid(),
                    History = new List<HistoryItem>(),
                    IsNew = false,
                    Leader = new Person()
                    {
                        Prename = "Tim",
                        Surname = "Rügen"
                    },
                    Name = "Florian Neustadt 31/1",
                    RadioName = "Florian Neustadt 31/1",
                    Vehicle = "DLA (K) 18-12 (NEU-BA 234)"
                });
            this.forces.Add(
                new Force()
                {
                    Id = Guid.NewGuid(),
                    History = new List<HistoryItem>(),
                    IsNew = false,
                    Leader = new Person()
                    {
                        Prename = "Norbert",
                        Surname = "Amsel"
                    },
                    Name = "Florian Neustadt 61/1",
                    RadioName = "Florian Neustadt 61/1",
                    Vehicle = "Rüstwagen (NEU-CD 345)"
                });
            this.forces.Add(
                new Force()
                {
                    Id = Guid.NewGuid(),
                    History = new List<HistoryItem>(),
                    IsNew = false,
                    Leader = new Person()
                    {
                        Prename = "Detlef",
                        Surname = "Rieger"
                    },
                    Name = "Florian Obermusterhausen 40/1",
                    RadioName = "Florian Obermusterhausen 40/1",
                    Vehicle = "LF16/12 (OMU-AB 123)"
                });
            this.forces.Add(
                new Force()
                {
                    Id = Guid.NewGuid(),
                    History = new List<HistoryItem>(),
                    IsNew = false,
                    Leader = new Person()
                    {
                        Prename = "Otto",
                        Surname = "Lermig"
                    },
                    Name = "Florian Obermusterhausen 31/1",
                    RadioName = "Florian Obermusterhausen 31/1",
                    Vehicle = "DLA (K) 18-12 (OMU-BA 234)"
                });
            this.forces.Add(
                new Force()
                {
                    Id = Guid.NewGuid(),
                    History = new List<HistoryItem>(),
                    IsNew = false,
                    Leader = new Person()
                    {
                        Prename = "Xaver",
                        Surname = "Wahn"
                    },
                    Name = "Florian Obermusterhausen 61/1",
                    RadioName = "Florian Obermusterhausen 61/1",
                    Vehicle = "Rüstwagen (OMU-CD 345)"
                });
        }
    }
}
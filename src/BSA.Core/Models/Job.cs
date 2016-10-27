// <copyright file="Job.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace BSA.Core.Models
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using BSA.Core.Utils;
    using BSA.Database.Entities;

    public class Job : EditableModelBase<Job>
    {
        private string communicationtool;
        private DateTime date;
        private string description;
        private List<Guid> forces;
        private bool injuredPerson;
        private JobPriority jobPriority;
        private JobState jobStatus;
        private string jobType;
        private JobLocation location;
        private string messenger;
        private string name;
        private int numberInjuredPerson;
        private string organization;

        public Job()
            : base(true)
        {
            this.History = new List<HistoryItem>();
            this.Id = Guid.NewGuid();
            this.Forces = new List<Guid>();
            this.Location = new JobLocation();
        }

        public Job(DbJob databaseEntity)
            : base(false)
        {
            Mapper.Map(databaseEntity, this);
            // ToDo: Investigate proper mapping of list of forces; Check type convertion (e.g. DbJobLocation -> JobLocation or int to enum)
        }

        /// <summary>
        ///     Auftragsname
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.Set(ref this.name, value); }
        }

        /// <summary>
        ///     Eingansdatum und Zeit
        /// </summary>
        public DateTime Date
        {
            get { return this.date; }
            set { this.Set(ref this.date, value); }
        }

        /// <summary>
        ///     Melder
        /// </summary>
        public string Messenger
        {
            get { return this.messenger; }
            set { this.Set(ref this.messenger, value); }
        }

        /// <summary>
        ///     zugehörige Organisation
        /// </summary>
        public string Organization
        {
            get { return this.organization; }
            set { this.Set(ref this.organization, value); }
        }

        /// <summary>
        ///     Ortsangaben des Auftrags
        /// </summary>
        public JobLocation Location
        {
            get { return this.location; }
            set { this.Set(ref this.location, value); }
        }

        /// <summary>
        ///     Auftragstyp, Schadensereignis
        /// </summary>
        public string JobType
        {
            get { return this.jobType; }
            set { this.Set(ref this.jobType, value); }
        }

        /// <summary>
        ///     gibt es verletzte Personen
        /// </summary>
        public bool InjuredPerson
        {
            get { return this.injuredPerson; }
            set { this.Set(ref this.injuredPerson, value); }
        }

        /// <summary>
        ///     Anzahl verletzte Personen
        /// </summary>
        public int NumberInjuredPerson
        {
            get { return this.numberInjuredPerson; }
            set { this.Set(ref this.numberInjuredPerson, value); }
        }

        /// <summary>
        ///     Kommunikationsmittel
        /// </summary>
        public string Communicationtool
        {
            get { return this.communicationtool; }
            set { this.Set(ref this.communicationtool, value); }
        }

        /// <summary>
        ///     Beschreibung, Meldung
        /// </summary>
        public string Description
        {
            get { return this.description; }
            set { this.Set(ref this.description, value); }
        }

        /// <summary>
        ///     Priorität des Auftrags
        /// </summary>
        public JobPriority JobPriority
        {
            get { return this.jobPriority; }
            set { this.Set(ref this.jobPriority, value); }
        }

        /// <summary>
        ///     Offen, in Bearbeitung, beendet
        /// </summary>
        public JobState JobStatus
        {
            get { return this.jobStatus; }
            set { this.Set(ref this.jobStatus, value); }
        }

        /// <summary>
        ///     Zugeteilte Einsatzkräfte (bei offenen Aufträgen leer)
        /// </summary>
        public List<Guid> Forces
        {
            get { return this.forces; }
            set { this.Set(ref this.forces, value); }
        }
    }
}
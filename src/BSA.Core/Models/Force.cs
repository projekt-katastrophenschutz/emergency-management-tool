// <copyright file="Forces.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

using System.Collections.ObjectModel;

namespace BSA.Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using BSA.Core.Utils;
    using BSA.Database.Entities;

    public class Force : EditableModelBase<Force>
    {
        private Person leader;
        private string name;
        private string notes;
        private ObservableCollection<Person> persons;
        private string radioName;
        private string vehicle;

        public Force()
            : base(true)
        {
            this.Id = Guid.NewGuid();
            this.Persons = new ObservableCollection<Person>();
            this.History = new List<HistoryItem>();
        }

        public Force(DbForce databaseEntity)
            : base(false)
        {

            Mapper.Map(databaseEntity, this);
            this.Leader = this.Persons?.SingleOrDefault(p => p.Id == databaseEntity.LeaderId) ?? null;
        }

        /// <summary>
        ///     Einsatzkraftname
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.Set(ref this.name, value); }
        }

        /// <summary>
        ///     Funkname
        /// </summary>
        public string RadioName
        {
            get { return this.radioName; }
            set { this.Set(ref this.radioName, value); }
        }

        /// <summary>
        ///     Einsatzkraftführer
        /// </summary>
        public Person Leader
        {
            get { return this.leader; }
            set { this.Set(ref this.leader, value); }
        }

        /// <summary>
        ///     Einsatzfahrzeug
        /// </summary>
        public string Vehicle
        {
            get { return this.vehicle; }
            set { this.Set(ref this.vehicle, value); }
        }

        /// <summary>
        ///     Anmerkungen
        /// </summary>
        public string Notes
        {
            get { return this.notes; }
            set { this.Set(ref this.notes, value); }
        }

        /// <summary>
        ///     zugeteilte Personen
        /// </summary>
        public ObservableCollection<Person> Persons
        {
            get { return this.persons; }
            set { this.Set(ref this.persons, value); }
        }
    }
}
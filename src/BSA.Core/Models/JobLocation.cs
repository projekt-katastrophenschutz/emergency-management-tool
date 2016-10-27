// <copyright file="JobLocation.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace BSA.Core.Models
{
    using System;
    using AutoMapper;
    using BSA.Core.Utils;
    using BSA.Database.Entities;

    public class JobLocation : EditableModelBase<JobLocation>
    {
        private string additionalDescription;
        private string city;
        private string floorLevel;
        private string houseNumber;
        private string street;
        private string zipCode;

        public JobLocation()
            : base(true)
        {
            this.Id = Guid.NewGuid();
        }

        public JobLocation(DbJobLocation databaseEntity)
            : base(false)
        {
            Mapper.Map(databaseEntity, this);
        }

        /// <summary>
        ///     Straße
        /// </summary>
        public string Street
        {
            get { return this.street; }
            set { this.Set(ref this.street, value); }
        }

        /// <summary>
        ///     Hausnummer
        /// </summary>
        public string HouseNumber
        {
            get { return this.houseNumber; }
            set { this.Set(ref this.houseNumber, value); }
        }

        /// <summary>
        ///     Postleitzahl
        /// </summary>
        public string ZipCode
        {
            get { return this.zipCode; }
            set { this.Set(ref this.zipCode, value); }
        }

        /// <summary>
        ///     Stockwerk
        /// </summary>
        public string FloorLevel
        {
            get { return this.floorLevel; }
            set { this.Set(ref this.floorLevel, value); }
        }

        /// <summary>
        ///     Stadt
        /// </summary>
        public string City
        {
            get { return this.city; }
            set { this.Set(ref this.city, value); }
        }

        /// <summary>
        ///     Location related additional / alternative description ("Third tree of the street" or something)
        /// </summary>
        public string AdditionalDescription
        {
            get { return this.additionalDescription; }
            set { this.Set(ref this.additionalDescription, value); }
        }

        public override string ToString()
        {
            return $"{this.Street} {this.HouseNumber} {this.ZipCode} {this.City}";
        }
    }
}
// <copyright file="Person.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace BSA.Core.Models
{
    using System;
    using AutoMapper;
    using BSA.Core.Utils;
    using BSA.Database.Entities;

    public class Person : EditableModelBase<Person>
    {
        private string additional;
        private DateTime birthDate;
        private string place;
        private string prename;
        private string street;
        private string surname;
        private string zipCode;
        private string radioCallName;
        private string employer;
        private string phoneNumber;
        private string relativesDetails;

        public Person()
            : base(true)
        {
            this.Id = Guid.NewGuid();
        }

        public Person(DbPerson databaseEntity)
            : base(false)
        {
            Mapper.Map(databaseEntity, this);
        }

        /// <summary>
        ///     Vorname
        /// </summary>
        public string Prename
        {
            get { return this.prename; }
            set { this.Set(ref this.prename, value); }
        }

        /// <summary>
        ///     Nachname
        /// </summary>
        public string Surname
        {
            get { return this.surname; }
            set { this.Set(ref this.surname, value); }
        }

        /// <summary>
        ///     Geburtsdatum
        /// </summary>
        public DateTime BirthDate
        {
            get { return this.birthDate; }
            set { this.Set(ref this.birthDate, value); }
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
        ///     Postleitzahl
        /// </summary>
        public string ZipCode
        {
            get { return this.zipCode; }
            set { this.Set(ref this.zipCode, value); }
        }

        /// <summary>
        ///     Ort
        /// </summary>
        public string Place
        {
            get { return this.place; }
            set { this.Set(ref this.place, value); }
        }

        /// <summary>
        ///     Zusatzinformationen-Sonstiges
        /// </summary>
        public string Additional
        {
            get { return this.additional; }
            set { this.Set(ref this.additional, value); }
        }

        /// <summary>
        ///     Funkrufname
        /// </summary>
        public string RadioCallName
        {
            get { return this.radioCallName; }
            set { this.Set(ref this.radioCallName, value); }
        }

        /// <summary>
        ///     Arbeitgeber
        /// </summary>
        public string Employer
        {
            get { return this.employer; }
            set { this.Set(ref this.employer, value); }
        }

        /// <summary>
        ///     Telefonnummer
        /// </summary>
        public string PhoneNumber
        {
            get { return this.phoneNumber; }
            set { this.Set(ref this.phoneNumber, value); }
        }

        /// <summary>
        ///     Infos zu Verwandten / Ansprechpartnern
        /// </summary>
        public string RelativesDetails
        {
            get { return this.relativesDetails; }
            set { this.Set(ref this.relativesDetails, value); }
        }

        public override string ToString()
        {
            return $"{this.Prename} {this.Surname}";
        }
    }
}
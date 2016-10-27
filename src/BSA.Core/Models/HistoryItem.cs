// <copyright file="HistoryItem.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace BSA.Core.Models
{
    using System;
    using AutoMapper;
    using BSA.Database.Entities;
    using GalaSoft.MvvmLight;
    using Utils;
    public class HistoryItem : ObservableObject 
    {
        private string changes;
        private Guid id;
        private DateTime timestamp;
        private string user;

        public HistoryItem(string user = "", string changes = "")
        {
            this.Id = Guid.NewGuid();
            this.Timestamp = DateTime.Now;
            this.User = user;
            this.Changes = changes;
        }
        
        /// <summary>
        ///     Gets or sets an unique entity id.
        /// </summary>
        public Guid Id
        {
            get { return this.id; }
            set { this.Set(ref this.id, value); }
        }

        /// <summary>
        ///     Gets or sets the date/time of the change.
        /// </summary>
        public DateTime Timestamp
        {
            get { return this.timestamp; }
            set { this.Set(ref this.timestamp, value); }
        }

        /// <summary>
        ///     Gets or sets the name of the person who made the change.
        /// </summary>
        public string User
        {
            get { return this.user; }
            set { this.Set(ref this.user, value); }
        }

        /// <summary>
        ///     Gets or sets the description of the change.
        /// </summary>
        public string Changes
        {
            get { return this.changes; }
            set { this.Set(ref this.changes, value); }
        }
    }
}
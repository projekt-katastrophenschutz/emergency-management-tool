// <copyright file="DbForceJobRelation.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace BSA.Database.Entities
{
    using System;

    public class DbForceJobRelation : DbEntityBase
    {
        public Guid JobId { get; set; }
        public DbJob Job { get; set; }

        public Guid ForceId { get; set; }
        public DbForce Force { get; set; }
    }
}
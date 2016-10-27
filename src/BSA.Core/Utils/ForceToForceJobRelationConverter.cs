// <copyright file="ForceToForceJobRelationConverter.cs" company="FT Software">
// Copyright (c) 2016. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace BSA.Core.Utils
{
    using System;
    using AutoMapper;
    using BSA.Core.Models;
    using BSA.Database.Entities;

    internal class ForceToForceJobRelationConverter : ITypeConverter<Guid, DbForceJobRelation>
    {
        public DbForceJobRelation Convert(ResolutionContext context)
        {
            var force = (Guid) context.SourceValue;
            return new DbForceJobRelation
            {
                ForceId = force
            };
        }
    }
}
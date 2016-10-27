// <copyright file="ForceJobRelationToForceConverter.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace BSA.Core.Utils
{
    using System;
    using AutoMapper;
    using BSA.Core.Models;
    using BSA.Database.Entities;

    public class ForceJobRelationToForceConverter : ITypeConverter<DbForceJobRelation, Guid>
    {
        public Guid Convert(ResolutionContext context)
        {
            var relation = (DbForceJobRelation) context.SourceValue;
            return relation.ForceId;
        }
    }
}
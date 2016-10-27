// <copyright file="ForceJobRelationToJobConverter.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace BSA.Core.Utils
{
    using AutoMapper;
    using BSA.Core.Models;
    using BSA.Database.Entities;

    public class ForceJobRelationToJobConverter : ITypeConverter<DbForceJobRelation, Job>
    {
        public Job Convert(ResolutionContext context)
        {
            var relation = (DbForceJobRelation) context.SourceValue;
            return new Job(relation.Job);
        }
    }
}
// <copyright file="JobToForceJobRelationConverter.cs" company="FT Software">
// Copyright (c) 2016. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace BSA.Core.Utils
{
    using AutoMapper;
    using BSA.Core.Models;
    using BSA.Database.Entities;

    public class JobToForceJobRelationConverter : ITypeConverter<Job, DbForceJobRelation>
    {
        public DbForceJobRelation Convert(ResolutionContext context)
        {
            var job = (Job) context.SourceValue;
            return new DbForceJobRelation
            {
                JobId = job.Id
            };
        }
    }
}
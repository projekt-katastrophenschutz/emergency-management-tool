// <copyright file="AutoMapperProfile.cs" company="FT Software">
// Copyright (c) 2016. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace BSA.Core
{
    using System;
    using AutoMapper;
    using BSA.Core.Models;
    using BSA.Core.Printing.Csv.Entities;
    using BSA.Core.Utils;
    using BSA.Database.Entities;

    public class AutoMapperProfile : Profile
    {
        protected override void Configure()
        {
            // Define maps for mapping database entities to business database entities
            this.CreateMap(typeof(DbForce), typeof(Force))
                .ForMember(nameof(Force.Leader), opt => opt.ResolveUsing(
                    (source, member) => { return new Person(); }));
            this.CreateMap(typeof(DbHistoryItem), typeof(HistoryItem));
            this.CreateMap(typeof(DbJob), typeof(Job));
            this.CreateMap(typeof(DbJobLocation), typeof(JobLocation));
            this.CreateMap(typeof(DbPerson), typeof(Person));
            this.CreateMap<DbForceJobRelation, Guid>().ConvertUsing<ForceJobRelationToForceConverter>();
            this.CreateMap<DbForceJobRelation, Job>().ConvertUsing<ForceJobRelationToJobConverter>();

            // Define maps for mapping business entities to related database entities
            this.CreateMap(typeof(Force), typeof(DbForce));
            this.CreateMap(typeof(HistoryItem), typeof(DbHistoryItem));
            this.CreateMap(typeof(Job), typeof(DbJob));
            this.CreateMap(typeof(JobLocation), typeof(DbJobLocation));
            this.CreateMap(typeof(Person), typeof(DbPerson));
            this.CreateMap<Guid, DbForceJobRelation>().ConvertUsing<ForceToForceJobRelationConverter>();
            this.CreateMap<Job, DbForceJobRelation>().ConvertUsing<JobToForceJobRelationConverter>();

            this.CreateMap<Force, CsvForce>();
            this.CreateMap<Person, CsvPerson>();
            this.CreateMap<Job, CsvJob>();
            this.CreateMap<JobLocation, CsvJobLocation>();
        }
    }
}
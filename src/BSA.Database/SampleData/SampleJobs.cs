// <copyright file="Jobs.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace BSA.Database.SampleData
{
    using System;
    using System.Collections.Generic;
    using BSA.Database.Entities;

    public static class SampleJobs
    {
        private static DbJob sampleJob1;
        private static DbJob sampleJob2;
        private static DbJob sampleJob3;

        public static DbJob SampleJob1
        {
            get
            {
                if (sampleJob1 == null)
                {
                    var id = Guid.NewGuid();
                    sampleJob1 = new DbJob
                    {
                        Id = id,
                        History = new List<DbHistoryItem>(),
                        Communicationtool = "Funk",
                        Date = DateTime.Now,
                        Forces = new List<DbForceJobRelation>
                        {
                            new DbForceJobRelation
                            {
                                ForceId = SampleForces.SampleForce1.Id,
                                JobId = id,
                                Id = Guid.NewGuid()
                            }
                        },
                        InjuredPerson = false,
                        JobPriority = 0,
                        JobStatus = 0,
                        JobType = "1",
                        Location = new DbJobLocation
                        {
                            Id = Guid.NewGuid(),
                            History = new List<DbHistoryItem>(),
                            AdditionalDescription = string.Empty,
                            City = "Augsburg",
                            FloorLevel = "1",
                            HouseNumber = "13",
                            Street = "Musterstr.",
                            ZipCode = "86150"
                        },
                        Messenger = "Hans Müller",
                        Name = "Hund auf Katze auf Baum",
                        NumberInjuredPerson = 0,
                        Organization = "FF Musterhausen"
                    };
                }

                return sampleJob1;
            }
        }

        public static DbJob SampleJob2
        {
            get
            {
                if (sampleJob2 == null)
                {
                    var id = Guid.NewGuid();
                    sampleJob2 = new DbJob
                    {
                        Id = id,
                        History = new List<DbHistoryItem>(),
                        Communicationtool = "Funk",
                        Date = DateTime.Now,
                        Forces = new List<DbForceJobRelation>
                        {
                            new DbForceJobRelation
                            {
                                ForceId = SampleForces.SampleForce1.Id,
                                JobId = id,
                                Id = Guid.NewGuid()
                            },
                            new DbForceJobRelation
                            {
                                ForceId = SampleForces.SampleForce2.Id,
                                JobId = id,
                                Id = Guid.NewGuid()
                            }
                        },
                        InjuredPerson = true,
                        JobPriority = 2,
                        JobStatus = 2,
                        JobType = "Rettung",
                        Location = new DbJobLocation
                        {
                            Id = Guid.NewGuid(),
                            History = new List<DbHistoryItem>(),
                            AdditionalDescription = string.Empty,
                            City = "Augsburg",
                            FloorLevel = "2",
                            HouseNumber = "13",
                            Street = "Kuhseestr. 5",
                            ZipCode = "86150"
                        },
                        Messenger = "Hans Müller",
                        Name = "Kind am Kopf verletzt",
                        NumberInjuredPerson = 0,
                        Organization = "RotesKreuz Musterhausen"
                    };
                }

                return sampleJob2;
            }
        }


        public static DbJob SampleJob3
        {
            get
            {
                if (sampleJob3 == null)
                {
                    var id = Guid.NewGuid();
                    sampleJob3 = new DbJob
                    {
                        Id = id,
                        History = new List<DbHistoryItem>(),
                        Communicationtool = "Funk",
                        Date = DateTime.Now,
                        Forces = new List<DbForceJobRelation>
                        {
                            new DbForceJobRelation
                            {
                                ForceId = SampleForces.SampleForce2.Id,
                                JobId = id,
                                Id = Guid.NewGuid()
                            },
                            new DbForceJobRelation
                            {
                                ForceId = SampleForces.SampleForce3.Id,
                                JobId = id,
                                Id = Guid.NewGuid()
                            }
                        },
                        InjuredPerson = false,
                        JobPriority = 1,
                        JobStatus = 1,
                        JobType = "1",
                        Location = new DbJobLocation
                        {
                            Id = Guid.NewGuid(),
                            History = new List<DbHistoryItem>(),
                            AdditionalDescription = string.Empty,
                            City = "Gersthofen",
                            FloorLevel = "1",
                            HouseNumber = "2",
                            Street = "Halderstr.6",
                            ZipCode = "86386"
                        },
                        Messenger = "Roland Ruf",
                        Name = "Haus wurde überschwemmt",
                        NumberInjuredPerson = 0,
                        Organization = "Feuerwehr Mustermann "
                    };
                }

                return sampleJob3;
            }
        }
    }
}
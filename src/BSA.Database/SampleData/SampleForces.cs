// <copyright file="Force.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace BSA.Database.SampleData
{
    using System;
    using System.Collections.Generic;
    using BSA.Database.Entities;

    public static class SampleForces
    {
        private static DbForce sampleForce1;
        private static DbForce sampleForce2;
        private static DbForce sampleForce3;

        public static DbForce SampleForce1
        {
            get
            {
                if (sampleForce1 == null)
                {
                    var id = new Guid("256FD391-289E-4AA4-8E83-268056767E07");
                    sampleForce1 = new DbForce
                    {
                        Id = id,
                        History = new List<DbHistoryItem>(),
                        LeaderId = SamplePersons.SamplePerson1.Id,
                        Name = "Einsatzgruppe 1",
                        Notes = "Bedingt einsetzbar: Gesamter Trupp allergisch gegen Hunde",
                        Persons = new List<DbPerson>
                        {
                            SamplePersons.SamplePerson1,
                            SamplePersons.SamplePerson2,
                            SamplePersons.SamplePerson3
                        },
                        RadioName = "Florian 1-22-3",
                        Vehicle = "A LF 123"
                    };
                }

                return sampleForce1;
            }
        }

        public static DbForce SampleForce2
        {
            get
            {
                if (sampleForce2 == null)
                {
                    var id = new Guid("04FD4090-2F31-4A87-ADF3-AA8EC41F3114");
                    sampleForce2 = new DbForce
                    {
                        Id = id,
                        History = new List<DbHistoryItem>(),
                        LeaderId = SamplePersons.SamplePerson4.Id,
                        Name = "Einsatzgruppe 2",
                        Notes = "alle fit",
                        Persons = new List<DbPerson>
                        {
                            SamplePersons.SamplePerson4,
                            SamplePersons.SamplePerson5,
                            SamplePersons.SamplePerson6
                        },
                        RadioName = "Richard 1-76-4",
                        Vehicle = "Fahrzeug-123"
                    };
                }

                return sampleForce2;
            }
        }

        public static DbForce SampleForce3
        {
            get
            {
                if (sampleForce3 == null)
                {
                    var id = new Guid("C8398B5D-A826-497B-BD73-F44DB3ABC3D7");
                    sampleForce3 = new DbForce
                    {
                        Id = Guid.NewGuid(),
                        History = new List<DbHistoryItem>(),
                        LeaderId = SamplePersons.SamplePerson4.Id,
                        Name = "Einsatzgruppe 3",
                        Notes = "alle fit",
                        Persons = new List<DbPerson>
                        {
                            SamplePersons.SamplePerson4,
                            SamplePersons.SamplePerson5,
                            SamplePersons.SamplePerson6
                        },
                        RadioName = "Hans 1-77-8",
                        Vehicle = "Rettungswagen"
                    };
                }

                return sampleForce3;
            }
        }
    }
}
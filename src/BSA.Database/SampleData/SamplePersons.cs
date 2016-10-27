// <copyright file="Person.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace BSA.Database.SampleData
{
    using System;
    using System.Collections.Generic;
    using BSA.Database.Entities;

    public static class SamplePersons
    {
        public static DbPerson SamplePerson1 => new DbPerson
        {
            Id = Guid.NewGuid(),
            History = new List<DbHistoryItem>(),
            Additional = "Abzug um 20 Uhr aufgrund unaufschiebbaren Termins",
            BirthDate = new DateTime(1970, 8, 22),
            Place = "Augsburg",
            Prename = "Max",
            Surname = "Mustermann",
            Street = "Musterstr. 1A",
            ZipCode = 86150
        };

        public static DbPerson SamplePerson2 => new DbPerson
        {
            Id = Guid.NewGuid(),
            History = new List<DbHistoryItem>(),
            Additional = string.Empty,
            BirthDate = new DateTime(1972, 4, 13),
            Place = "Augsburg",
            Prename = "Jürgen",
            Surname = "Bratmann",
            Street = "Musterstr. 13",
            ZipCode = 86150
        };

        public static DbPerson SamplePerson3 => new DbPerson
        {
            Id = Guid.NewGuid(),
            History = new List<DbHistoryItem>(),
            Additional = string.Empty,
            BirthDate = new DateTime(1971, 12, 7),
            Place = "Augsburg",
            Prename = "Julia",
            Surname = "Musterfrau",
            Street = "Musterstr. 3",
            ZipCode = 86150
        };

        public static DbPerson SamplePerson4 => new DbPerson
        {
            Id = Guid.NewGuid(),
            History = new List<DbHistoryItem>(),
            Additional = string.Empty,
            BirthDate = new DateTime(1982, 3, 8),
            Place = "Augsburg",
            Prename = "Emil",
            Surname = "Huber",
            Street = "Bahnhofsstr. 13",
            ZipCode = 86150
        };

        public static DbPerson SamplePerson5 => new DbPerson
        {
            Id = Guid.NewGuid(),
            History = new List<DbHistoryItem>(),
            Additional = string.Empty,
            BirthDate = new DateTime(1985, 4, 7),
            Place = "Augsburg",
            Prename = "Johann",
            Surname = "Weber",
            Street = "Antonstr. 3",
            ZipCode = 86150
        };
        
        public static DbPerson SamplePerson6 => new DbPerson
        {
            Id = Guid.NewGuid(),
            History = new List<DbHistoryItem>(),
            Additional = string.Empty,
            BirthDate = new DateTime(1965, 5, 13),
            Place = "Gersthofen",
            Prename = "Kathrin",
            Surname = "Müller",
            Street = "Maxstr. 14",
            ZipCode = 86386
        };
        
        public static DbPerson SamplePerson7 => new DbPerson
        {
            Id = Guid.NewGuid(),
            History = new List<DbHistoryItem>(),
            Additional = string.Empty,
            BirthDate = new DateTime(1969, 5, 11),
            Place = "Gersthofen",
            Prename = "Stephanie",
            Surname = "Meier",
            Street = "Hauptstr. 14",
            ZipCode = 86386
        };

        public static DbPerson SamplePerson8 => new DbPerson
        {
            Id = Guid.NewGuid(),
            History = new List<DbHistoryItem>(),
            Additional = string.Empty,
            BirthDate = new DateTime(1972, 5, 8),
            Place = "Gersthofen",
            Prename = "Martin",
            Surname = "Fischer",
            Street = "Vogelstr. 14",
            ZipCode = 86386
        };
        
        public static DbPerson SamplePerson9 => new DbPerson
        {
            Id = Guid.NewGuid(),
            History = new List<DbHistoryItem>(),
            Additional = string.Empty,
            BirthDate = new DateTime(1979, 7, 5),
            Place = "Gersthofen",
            Prename = "Friedrich",
            Surname = "Blässing",
            Street = "Maxstr. 4",
            ZipCode = 86386
        };
    }
}
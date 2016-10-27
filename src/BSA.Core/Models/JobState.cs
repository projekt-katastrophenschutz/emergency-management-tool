// <copyright file="JobState.cs" company="FT Software">
// Copyright (c) 2016. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

using System;

namespace BSA.Core.Models
{
    [Flags]
    public enum JobState
    {
        NewCreated = 1, // neu geöffnet 

        InProgress = 2, // in Bearbeitung

        Ended = 4 // geschlossen
    }
}
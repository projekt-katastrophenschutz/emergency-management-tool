// <copyright file="JobType.cs" company="FT Software">
// Copyright (c) 2016. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace BSA.Core.Models
{
    public enum JobType
    {
        Recovery, // Bergung 

        Flooding, // Überschwemmung

        Hail, // Hagel

        LightningStrike, // Blitzeinschlag

        Fire, // ausgebrochenes Feuer,

        Rescue //Rettung
    }
}
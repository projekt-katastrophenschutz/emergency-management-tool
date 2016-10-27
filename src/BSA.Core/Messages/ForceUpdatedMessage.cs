// <copyright file="ForceUpdatedMessage.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace BSA.Core.Messages
{
    using BSA.Core.Models;

    public class ForceUpdatedMessage
    {
        public ForceUpdatedMessage(Force force)
        {
            this.Force = force;
        }

        public Force Force { get; set; }
    }
}
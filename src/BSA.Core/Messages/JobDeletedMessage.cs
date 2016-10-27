// <copyright file="JobDeletedMessage.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace BSA.Core.Messages
{
    using BSA.Core.Models;

    public class JobDeletedMessage
    {
        public JobDeletedMessage(Job job)
        {
            this.Job = job;
        }

        public Job Job { get; set; }
    }
}
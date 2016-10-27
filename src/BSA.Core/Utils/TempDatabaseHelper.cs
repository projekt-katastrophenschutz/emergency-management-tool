// <copyright file="TempDatabaseHelper.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace BSA.Core.Utils
{
    using BSA.Database.Management;

    public static class TempDatabaseHelper
    {
        public static void TempDatabaseIteration(this DatabaseContext databaseContext)
        {
            foreach (var force in databaseContext.Forces)
            {
            }

            foreach (var location in databaseContext.JobLocations)
            {
            }

            foreach (var relation in databaseContext.ForceJobRelations)
            {
            }

            foreach (var job in databaseContext.Jobs)
            {
            }
        }
    }
}
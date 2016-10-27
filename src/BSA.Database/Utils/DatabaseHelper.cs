// <copyright file="DatabaseHelper.cs" company="FT Software">
// Copyright (c) 2016. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace BSA.Database.Utils
{
    using System;
    using System.IO;
    using System.Linq;
    using AutoMapper;
    using BSA.Database.Entities;
    using BSA.Database.Management;
    using Microsoft.EntityFrameworkCore;

    public static class DatabaseHelper
    {
        public static void UpdateOrAddDatabaseEntry<T1, T2>(this DatabaseContext databaseContext, DbSet<T2> dbSet,
            T1 modelInstance)
            where T2 : DbEntityBase
        {
            // map the model object onto a database object
            var newDbEntry = Mapper.Map<T1, T2>(modelInstance);
            
            // Check if the database entry exists.      
            var existingEntry = dbSet.AsNoTracking().Include(x => x.History).FirstOrDefault(entry => entry.Id == newDbEntry.Id);
            // If the entry doesn't exist, add a new one. 
            // Otherwise, map the modelInstance (for example, a Job object) onto the database entry object 
            // (for example, a DbJob object) and update the old entry
            if (existingEntry == null)
            {
                dbSet.Add(newDbEntry);
            }
            else
            {
                //databaseContext.Entry(existingEntry).Context.Update(newDbEntry);
                ////foreach (var historyItem in existingEntry.History)
                ////{
                ////    databaseContext.HistoryEntries.Remove(historyItem);
                ////}

                ////databaseContext.SaveChanges();

                RemoveDatabaseEntry(databaseContext, dbSet, existingEntry.Id);
                
                dbSet.Add(newDbEntry);
            }

            databaseContext.SaveChanges();
        }

        public static void RemoveDatabaseEntry<T>(this DatabaseContext databaseContext, DbSet<T> dbSet,
            Guid entityId)
            where T : DbEntityBase
        {
            // Check if the database entry exists.      
            var existingEntry = GetFullDatabaseEntry(databaseContext, dbSet, entityId);
            // If the entry doesn't exist, do nothing
            // Otherwise, remove the entry.
            if (existingEntry == null)
            {
                return;
            }

            dbSet.Remove(existingEntry);
            databaseContext.SaveChanges();
        }

        public static T GetFullDatabaseEntry<T>(this DatabaseContext databaseContext, DbSet<T> dbSet, Guid entryId)
            where T : DbEntityBase
        {
            if (typeof(T) == typeof(DbJob))
            {
                return databaseContext.Jobs
                    .AsNoTracking()
                    .Include(job => job.Location)
                    .Include(job => job.Location.History)
                    .Include(job => job.History)
                    .Include(job => job.Forces)
                    .SingleOrDefault(job => job.Id == entryId) as T;
            }

            if (typeof(T) == typeof(DbForce))
            {
                return databaseContext.Forces
                    .AsNoTracking()
                    .Include(force => force.Jobs)
                    .Include(force => force.Persons)
                    .Include(force => force.History)
                    .SingleOrDefault(force => force.Id == entryId) as T;
            }

            return null;
        }

        public static void DeleteDatabaseFile()
        {
            var dbFile = "\\ProjektKatastrophenschutz.db";
            var dbFile1 = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + dbFile;
            var dbFile2 = "." + dbFile;
            
            if (File.Exists(dbFile1))
            {
                File.Delete(dbFile1);

                // Intialize new Database
                using (var databaseContext = new DatabaseContext())
                {
                    databaseContext.Database.Migrate();
                }
            }
            else if (File.Exists(dbFile2))
            {
                File.Delete(dbFile2);

                // Intialize new Database
                using (var databaseContext = new DatabaseContext())
                {
                    databaseContext.Database.Migrate();
                }
            }
        }
    }
}
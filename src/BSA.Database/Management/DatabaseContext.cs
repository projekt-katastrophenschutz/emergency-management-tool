// <copyright file="DatabaseContext.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace BSA.Database.Management
{
    using System;
    using System.IO;
    using BSA.Database.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;

    public class DatabaseContext : DbContext
    {
        /// <summary>
        /// Gets or sets the list of forces.
        /// </summary>
        public DbSet<DbForce> Forces { get; set; }

        /// <summary>
        /// Gets or sets the list of Force-Job relations.
        /// </summary>
        public DbSet<DbForceJobRelation> ForceJobRelations { get; set; }

        /// <summary>
        /// Gets or sets the list of jobs.
        /// </summary>
        public DbSet<DbJob> Jobs { get; set; }

        /// <summary>
        /// Gets or sets the list of job locations.
        /// </summary>
        public DbSet<DbJobLocation> JobLocations { get; set; }

        /// <summary>
        /// Gets or sets the list of database modifications.
        /// </summary>
        public DbSet<DbLogEntry> LogEntries { get; set; }

        /// <summary>
        /// Gets or sets the list of database modifications.
        /// </summary>
        public DbSet<DbHistoryItem> HistoryEntries { get; set; }

        /// <summary>
        /// Gets or sets the list of persons.
        /// </summary>
        public DbSet<DbPerson> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#if DEBUG
            string databaseFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
#else
            string databaseFolder = ".";
#endif
            
            optionsBuilder.UseSqlite($"Filename={Path.Combine(databaseFolder, "ProjektKatastrophenschutz.db")}");
            // VS2015 Local DB UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ProjektKatastrophenschutz;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Make DbEntityBase.Id required on all derived types
            modelBuilder.Entity<DbForce>()
                .Property(entity => entity.Id)
                .IsRequired();

            modelBuilder.Entity<DbHistoryItem>()
                .Property(entity => entity.Id)
                .IsRequired();

            modelBuilder.Entity<DbJob>()
                .Property(entity => entity.Id)
                .IsRequired();

            modelBuilder.Entity<DbJobLocation>()
                .Property(entity => entity.Id)
                .IsRequired();

            modelBuilder.Entity<DbLogEntry>()
                .Property(logEntry => logEntry.Id)
                .IsRequired();

            modelBuilder.Entity<DbPerson>()
                .Property(logEntry => logEntry.Id)
                .IsRequired();

            // Configure relationsships
            modelBuilder.Entity<DbJob>()
                .HasOne(job => job.Location)
                .WithOne(location => location.Job)
                .HasForeignKey<DbJobLocation>(location => location.JobId);

            modelBuilder.Entity<DbForceJobRelation>()
                .HasOne(relation => relation.Job)
                .WithMany(job => job.Forces)
                .HasForeignKey(relation => relation.JobId);

            modelBuilder.Entity<DbForceJobRelation>()
                .HasOne(relation => relation.Force)
                .WithMany(force => force.Jobs)
                .HasForeignKey(relation => relation.ForceId);

            modelBuilder.Entity<DbForce>()
                .HasMany(force => force.Persons)
                .WithOne(person => person.Force)
                .HasForeignKey(person => person.ForceId);

            // Configure history item relations
            modelBuilder.Entity<DbJob>()
                .HasMany(job => job.History)
                .WithOne(history => history.DbJob)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DbForce>()
                .HasMany(force => force.History)
                .WithOne(history => history.DbForce)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
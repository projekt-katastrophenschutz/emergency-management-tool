using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using BSA.Database.Management;

namespace BSA.Database.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20160705204752_InitialMigrationv11")]
    partial class InitialMigrationv11
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20901");

            modelBuilder.Entity("BSA.Database.Entities.DbForce", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("LeaderId");

                    b.Property<string>("Name");

                    b.Property<string>("Notes");

                    b.Property<string>("RadioName");

                    b.Property<string>("Vehicle");

                    b.HasKey("Id");

                    b.ToTable("Forces");
                });

            modelBuilder.Entity("BSA.Database.Entities.DbForceJobRelation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ForceId");

                    b.Property<Guid>("JobId");

                    b.HasKey("Id");

                    b.HasIndex("ForceId");

                    b.HasIndex("JobId");

                    b.ToTable("ForceJobRelations");
                });

            modelBuilder.Entity("BSA.Database.Entities.DbHistoryItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Changes");

                    b.Property<Guid?>("DbForceId");

                    b.Property<Guid?>("DbForceJobRelationId");

                    b.Property<Guid?>("DbJobId");

                    b.Property<Guid?>("DbJobLocationId");

                    b.Property<Guid?>("DbPersonId");

                    b.Property<DateTime>("Timestamp");

                    b.Property<string>("User");

                    b.HasKey("Id");

                    b.HasIndex("DbForceId");

                    b.HasIndex("DbForceJobRelationId");

                    b.HasIndex("DbJobId");

                    b.HasIndex("DbJobLocationId");

                    b.HasIndex("DbPersonId");

                    b.ToTable("HistoryEntries");
                });

            modelBuilder.Entity("BSA.Database.Entities.DbJob", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Communicationtool");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<bool>("InjuredPerson");

                    b.Property<int>("JobPriority");

                    b.Property<int>("JobStatus");

                    b.Property<string>("JobType");

                    b.Property<string>("Messenger");

                    b.Property<string>("Name");

                    b.Property<int>("NumberInjuredPerson");

                    b.Property<string>("Organization");

                    b.HasKey("Id");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("BSA.Database.Entities.DbJobLocation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AdditionalDescription");

                    b.Property<string>("City");

                    b.Property<string>("FloorLevel");

                    b.Property<string>("HouseNumber");

                    b.Property<Guid>("JobId");

                    b.Property<string>("Street");

                    b.Property<string>("ZipCode");

                    b.HasKey("Id");

                    b.HasIndex("JobId");

                    b.ToTable("JobLocations");
                });

            modelBuilder.Entity("BSA.Database.Entities.DbLogEntry", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("JsonContent");

                    b.Property<DateTime>("Timestamp");

                    b.HasKey("Id");

                    b.ToTable("LogEntries");
                });

            modelBuilder.Entity("BSA.Database.Entities.DbPerson", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Additional");

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("Employer");

                    b.Property<Guid>("ForceId");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("Place");

                    b.Property<string>("Prename");

                    b.Property<string>("RadioCallName");

                    b.Property<string>("RelativesDetails");

                    b.Property<string>("Street");

                    b.Property<string>("Surname");

                    b.Property<int>("ZipCode");

                    b.HasKey("Id");

                    b.HasIndex("ForceId");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("BSA.Database.Entities.DbForceJobRelation", b =>
                {
                    b.HasOne("BSA.Database.Entities.DbForce")
                        .WithMany()
                        .HasForeignKey("ForceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BSA.Database.Entities.DbJob")
                        .WithMany()
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BSA.Database.Entities.DbHistoryItem", b =>
                {
                    b.HasOne("BSA.Database.Entities.DbForce")
                        .WithMany()
                        .HasForeignKey("DbForceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BSA.Database.Entities.DbForceJobRelation")
                        .WithMany()
                        .HasForeignKey("DbForceJobRelationId");

                    b.HasOne("BSA.Database.Entities.DbJob")
                        .WithMany()
                        .HasForeignKey("DbJobId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BSA.Database.Entities.DbJobLocation")
                        .WithMany()
                        .HasForeignKey("DbJobLocationId");

                    b.HasOne("BSA.Database.Entities.DbPerson")
                        .WithMany()
                        .HasForeignKey("DbPersonId");
                });

            modelBuilder.Entity("BSA.Database.Entities.DbJobLocation", b =>
                {
                    b.HasOne("BSA.Database.Entities.DbJob")
                        .WithOne()
                        .HasForeignKey("BSA.Database.Entities.DbJobLocation", "JobId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BSA.Database.Entities.DbPerson", b =>
                {
                    b.HasOne("BSA.Database.Entities.DbForce")
                        .WithMany()
                        .HasForeignKey("ForceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}

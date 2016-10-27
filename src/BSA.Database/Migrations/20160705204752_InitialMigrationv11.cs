using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BSA.Database.Migrations
{
    public partial class InitialMigrationv11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Forces",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    LeaderId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    RadioName = table.Column<string>(nullable: true),
                    Vehicle = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Communicationtool = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    InjuredPerson = table.Column<bool>(nullable: false),
                    JobPriority = table.Column<int>(nullable: false),
                    JobStatus = table.Column<int>(nullable: false),
                    JobType = table.Column<string>(nullable: true),
                    Messenger = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    NumberInjuredPerson = table.Column<int>(nullable: false),
                    Organization = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogEntries",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Autoincrement", true),
                    JsonContent = table.Column<string>(nullable: true),
                    Timestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogEntries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Additional = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    Employer = table.Column<string>(nullable: true),
                    ForceId = table.Column<Guid>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Place = table.Column<string>(nullable: true),
                    Prename = table.Column<string>(nullable: true),
                    RadioCallName = table.Column<string>(nullable: true),
                    RelativesDetails = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    ZipCode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persons_Forces_ForceId",
                        column: x => x.ForceId,
                        principalTable: "Forces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ForceJobRelations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ForceId = table.Column<Guid>(nullable: false),
                    JobId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForceJobRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ForceJobRelations_Forces_ForceId",
                        column: x => x.ForceId,
                        principalTable: "Forces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ForceJobRelations_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobLocations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AdditionalDescription = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    FloorLevel = table.Column<string>(nullable: true),
                    HouseNumber = table.Column<string>(nullable: true),
                    JobId = table.Column<Guid>(nullable: false),
                    Street = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobLocations_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoryEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Changes = table.Column<string>(nullable: true),
                    DbForceId = table.Column<Guid>(nullable: true),
                    DbForceJobRelationId = table.Column<Guid>(nullable: true),
                    DbJobId = table.Column<Guid>(nullable: true),
                    DbJobLocationId = table.Column<Guid>(nullable: true),
                    DbPersonId = table.Column<Guid>(nullable: true),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    User = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryEntries_Forces_DbForceId",
                        column: x => x.DbForceId,
                        principalTable: "Forces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistoryEntries_ForceJobRelations_DbForceJobRelationId",
                        column: x => x.DbForceJobRelationId,
                        principalTable: "ForceJobRelations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistoryEntries_Jobs_DbJobId",
                        column: x => x.DbJobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistoryEntries_JobLocations_DbJobLocationId",
                        column: x => x.DbJobLocationId,
                        principalTable: "JobLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistoryEntries_Persons_DbPersonId",
                        column: x => x.DbPersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ForceJobRelations_ForceId",
                table: "ForceJobRelations",
                column: "ForceId");

            migrationBuilder.CreateIndex(
                name: "IX_ForceJobRelations_JobId",
                table: "ForceJobRelations",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryEntries_DbForceId",
                table: "HistoryEntries",
                column: "DbForceId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryEntries_DbForceJobRelationId",
                table: "HistoryEntries",
                column: "DbForceJobRelationId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryEntries_DbJobId",
                table: "HistoryEntries",
                column: "DbJobId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryEntries_DbJobLocationId",
                table: "HistoryEntries",
                column: "DbJobLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryEntries_DbPersonId",
                table: "HistoryEntries",
                column: "DbPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_JobLocations_JobId",
                table: "JobLocations",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_ForceId",
                table: "Persons",
                column: "ForceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoryEntries");

            migrationBuilder.DropTable(
                name: "LogEntries");

            migrationBuilder.DropTable(
                name: "ForceJobRelations");

            migrationBuilder.DropTable(
                name: "JobLocations");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Forces");
        }
    }
}

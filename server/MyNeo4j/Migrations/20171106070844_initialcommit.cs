using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MyNeo4j.Migrations
{
    public partial class initialcommit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Activityhappened",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activityhappened", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Commentlog",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commentlog", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Pmaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Organization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pmaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectM",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LeaderID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TechnologyUsed = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectM", x => x.ProjectId);
                });

            migrationBuilder.CreateTable(
                name: "Projectmember",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActAs = table.Column<int>(type: "int", nullable: false),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projectmember", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "SignalRDb",
                columns: table => new
                {
                    SignalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ConnectionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HubCode = table.Column<int>(type: "int", nullable: false),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    Online = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SignalRDb", x => x.SignalId);
                });

            migrationBuilder.CreateTable(
                name: "Socialsm",
                columns: table => new
                {
                    SignId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Socialsm", x => x.SignId);
                });

            migrationBuilder.CreateTable(
                name: "Teammemeber",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teammemeber", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "EpicDb",
                columns: table => new
                {
                    EpicId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EpicDb", x => x.EpicId);
                    table.ForeignKey(
                        name: "FK_EpicDb_ProjectM_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectM",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Productbl",
                columns: table => new
                {
                    StoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InSprintNo = table.Column<int>(type: "int", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    StoryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productbl", x => x.StoryId);
                    table.ForeignKey(
                        name: "FK_Productbl_ProjectM_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectM",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Releasepl",
                columns: table => new
                {
                    ReleasePlanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReleaseName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Releasepl", x => x.ReleasePlanId);
                    table.ForeignKey(
                        name: "FK_Releasepl_ProjectM_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectM",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sprintbl",
                columns: table => new
                {
                    SprintId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    ReleasePlanId = table.Column<int>(type: "int", nullable: false),
                    SprintGoal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SprintName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    TotalDays = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sprintbl", x => x.SprintId);
                    table.ForeignKey(
                        name: "FK_Sprintbl_ProjectM_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectM",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teammaster",
                columns: table => new
                {
                    TeamId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    TeamName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teammaster", x => x.TeamId);
                    table.ForeignKey(
                        name: "FK_Teammaster_ProjectM_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectM",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Taaskbl",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    SprintId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TaskName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taaskbl", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_Taaskbl_Sprintbl_SprintId",
                        column: x => x.SprintId,
                        principalTable: "Sprintbl",
                        principalColumn: "SprintId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Checklistbl",
                columns: table => new
                {
                    ChecklistId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ChecklistName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    TaskId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checklistbl", x => x.ChecklistId);
                    table.ForeignKey(
                        name: "FK_Checklistbl_Taaskbl_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Taaskbl",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Checklistbl_TaskId",
                table: "Checklistbl",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_EpicDb_ProjectId",
                table: "EpicDb",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Productbl_ProjectId",
                table: "Productbl",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Releasepl_ProjectId",
                table: "Releasepl",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Sprintbl_ProjectId",
                table: "Sprintbl",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Taaskbl_SprintId",
                table: "Taaskbl",
                column: "SprintId");

            migrationBuilder.CreateIndex(
                name: "IX_Teammaster_ProjectId",
                table: "Teammaster",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activityhappened");

            migrationBuilder.DropTable(
                name: "Checklistbl");

            migrationBuilder.DropTable(
                name: "Commentlog");

            migrationBuilder.DropTable(
                name: "EpicDb");

            migrationBuilder.DropTable(
                name: "Pmaster");

            migrationBuilder.DropTable(
                name: "Productbl");

            migrationBuilder.DropTable(
                name: "Projectmember");

            migrationBuilder.DropTable(
                name: "Releasepl");

            migrationBuilder.DropTable(
                name: "SignalRDb");

            migrationBuilder.DropTable(
                name: "Socialsm");

            migrationBuilder.DropTable(
                name: "Teammaster");

            migrationBuilder.DropTable(
                name: "Teammemeber");

            migrationBuilder.DropTable(
                name: "Taaskbl");

            migrationBuilder.DropTable(
                name: "Sprintbl");

            migrationBuilder.DropTable(
                name: "ProjectM");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MyNeo4j.Migrations
{
    public partial class additioofepic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Teammaster",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "TotalDays",
                table: "Sprintbl",
                type: "int",
                nullable: false,
                oldClrType: typeof(TimeSpan));

            migrationBuilder.CreateIndex(
                name: "IX_Teammaster_ProjectId",
                table: "Teammaster",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teammaster_ProjectM_ProjectId",
                table: "Teammaster",
                column: "ProjectId",
                principalTable: "ProjectM",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teammaster_ProjectM_ProjectId",
                table: "Teammaster");

            migrationBuilder.DropIndex(
                name: "IX_Teammaster_ProjectId",
                table: "Teammaster");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Teammaster");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "TotalDays",
                table: "Sprintbl",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}

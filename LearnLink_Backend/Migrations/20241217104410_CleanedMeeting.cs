﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnLink_Backend.Migrations
{
    /// <inheritdoc />
    public partial class CleanedMeeting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Meetings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InstructorId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstructorId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false),
                    StartsAt = table.Column<int>(type: "int", nullable: false),
                    EndsAt = table.Column<int>(type: "int", nullable: false),
                    AtDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meetings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meetings_Instructors_InstructorId1",
                        column: x => x.InstructorId1,
                        principalTable: "Instructors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Meetings_Students_StudentId1",
                        column: x => x.StudentId1,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_InstructorId1",
                table: "Meetings",
                column: "InstructorId1");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_StudentId1",
                table: "Meetings",
                column: "StudentId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Meetings");
        }
    }
}

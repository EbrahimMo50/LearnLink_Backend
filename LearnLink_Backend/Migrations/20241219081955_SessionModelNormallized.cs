using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnLink_Backend.Migrations
{
    /// <inheritdoc />
    public partial class SessionModelNormallized : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Instructors_InstructorId",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_InstructorId",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "InstructorId",
                table: "Sessions");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Sessions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Sessions");

            migrationBuilder.AddColumn<Guid>(
                name: "InstructorId",
                table: "Sessions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_InstructorId",
                table: "Sessions",
                column: "InstructorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Instructors_InstructorId",
                table: "Sessions",
                column: "InstructorId",
                principalTable: "Instructors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

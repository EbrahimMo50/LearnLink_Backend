using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnLink_Backend.Migrations
{
    /// <inheritdoc />
    public partial class ManyToManyRelationFixInStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Posts_PostModelId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Sessions_SessionModelId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_PostModelId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_SessionModelId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "PostModelId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "SessionModelId",
                table: "Students");

            migrationBuilder.CreateTable(
                name: "PostModelStudent",
                columns: table => new
                {
                    LikedPostsId = table.Column<int>(type: "int", nullable: false),
                    LikesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostModelStudent", x => new { x.LikedPostsId, x.LikesId });
                    table.ForeignKey(
                        name: "FK_PostModelStudent_Posts_LikedPostsId",
                        column: x => x.LikedPostsId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostModelStudent_Students_LikesId",
                        column: x => x.LikesId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SessionModelStudent",
                columns: table => new
                {
                    AttendendStudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SessionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionModelStudent", x => new { x.AttendendStudentId, x.SessionsId });
                    table.ForeignKey(
                        name: "FK_SessionModelStudent_Sessions_SessionsId",
                        column: x => x.SessionsId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SessionModelStudent_Students_AttendendStudentId",
                        column: x => x.AttendendStudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostModelStudent_LikesId",
                table: "PostModelStudent",
                column: "LikesId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionModelStudent_SessionsId",
                table: "SessionModelStudent",
                column: "SessionsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostModelStudent");

            migrationBuilder.DropTable(
                name: "SessionModelStudent");

            migrationBuilder.AddColumn<int>(
                name: "PostModelId",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SessionModelId",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_PostModelId",
                table: "Students",
                column: "PostModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_SessionModelId",
                table: "Students",
                column: "SessionModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Posts_PostModelId",
                table: "Students",
                column: "PostModelId",
                principalTable: "Posts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Sessions_SessionModelId",
                table: "Students",
                column: "SessionModelId",
                principalTable: "Sessions",
                principalColumn: "Id");
        }
    }
}

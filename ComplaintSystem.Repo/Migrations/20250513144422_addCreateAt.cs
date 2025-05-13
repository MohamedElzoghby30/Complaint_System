using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComplaintSystem.Repo.Migrations
{
    /// <inheritdoc />
    public partial class addCreateAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatAt",
                table: "Workflows",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatAt",
                table: "Ratings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatAt",
                table: "Departments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatAt",
                table: "ComplaintTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatAt",
                table: "Complaints",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatAt",
                table: "ComplaintParticipants",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatAt",
                table: "Comments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatAt",
                table: "CommentComplainers",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatAt",
                table: "Workflows");

            migrationBuilder.DropColumn(
                name: "CreatAt",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "CreatAt",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "CreatAt",
                table: "ComplaintTypes");

            migrationBuilder.DropColumn(
                name: "CreatAt",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "CreatAt",
                table: "ComplaintParticipants");

            migrationBuilder.DropColumn(
                name: "CreatAt",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CreatAt",
                table: "CommentComplainers");
        }
    }
}

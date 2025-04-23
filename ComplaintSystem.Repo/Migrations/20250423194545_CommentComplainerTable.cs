using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComplaintSystem.Repo.Migrations
{
    /// <inheritdoc />
    public partial class CommentComplainerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommentComplainers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComplaintID = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CommentText = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentComplainers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentComplainers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommentComplainers_Complaints_ComplaintID",
                        column: x => x.ComplaintID,
                        principalTable: "Complaints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentComplainers_ComplaintID",
                table: "CommentComplainers",
                column: "ComplaintID");

            migrationBuilder.CreateIndex(
                name: "IX_CommentComplainers_UserId",
                table: "CommentComplainers",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentComplainers");
        }
    }
}

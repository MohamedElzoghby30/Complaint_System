using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComplaintSystem.Repo.Migrations
{
    /// <inheritdoc />
    public partial class DeleteParticipantANDComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_ComplaintParticipants_ParticipantID",
                table: "Comments");

            migrationBuilder.DropTable(
                name: "CommentComplainers");

            migrationBuilder.DropTable(
                name: "ComplaintParticipants");

            migrationBuilder.RenameColumn(
                name: "ParticipantID",
                table: "Comments",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ParticipantID",
                table: "Comments",
                newName: "IX_Comments_UserId");

            migrationBuilder.AddColumn<int>(
                name: "ComplaintId",
                table: "Workflows",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatAt",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Workflows_ComplaintId",
                table: "Workflows",
                column: "ComplaintId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Workflows_Complaints_ComplaintId",
                table: "Workflows",
                column: "ComplaintId",
                principalTable: "Complaints",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_UserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Workflows_Complaints_ComplaintId",
                table: "Workflows");

            migrationBuilder.DropIndex(
                name: "IX_Workflows_ComplaintId",
                table: "Workflows");

            migrationBuilder.DropColumn(
                name: "ComplaintId",
                table: "Workflows");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Comments",
                newName: "ParticipantID");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                newName: "IX_Comments_ParticipantID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatAt",
                table: "Comments",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateTable(
                name: "CommentComplainers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComplaintID = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CommentText = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatAt = table.Column<DateTime>(type: "datetime2", nullable: true),
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

            migrationBuilder.CreateTable(
                name: "ComplaintParticipants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComplaintID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    WorkflowID = table.Column<int>(type: "int", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplaintParticipants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComplaintParticipants_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ComplaintParticipants_Complaints_ComplaintID",
                        column: x => x.ComplaintID,
                        principalTable: "Complaints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ComplaintParticipants_Workflows_WorkflowID",
                        column: x => x.WorkflowID,
                        principalTable: "Workflows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentComplainers_ComplaintID",
                table: "CommentComplainers",
                column: "ComplaintID");

            migrationBuilder.CreateIndex(
                name: "IX_CommentComplainers_UserId",
                table: "CommentComplainers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintParticipants_ComplaintID",
                table: "ComplaintParticipants",
                column: "ComplaintID");

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintParticipants_UserID",
                table: "ComplaintParticipants",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintParticipants_WorkflowID",
                table: "ComplaintParticipants",
                column: "WorkflowID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_ComplaintParticipants_ParticipantID",
                table: "Comments",
                column: "ParticipantID",
                principalTable: "ComplaintParticipants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

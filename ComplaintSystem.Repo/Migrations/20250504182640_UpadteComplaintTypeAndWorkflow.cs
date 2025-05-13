using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComplaintSystem.Repo.Migrations
{
    /// <inheritdoc />
    public partial class UpadteComplaintTypeAndWorkflow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workflows_AspNetRoles_RoleID",
                table: "Workflows");

            migrationBuilder.DropIndex(
                name: "IX_Workflows_RoleID",
                table: "Workflows");

            migrationBuilder.DropColumn(
                name: "RoleID",
                table: "Workflows");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleID",
                table: "Workflows",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Workflows_RoleID",
                table: "Workflows",
                column: "RoleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Workflows_AspNetRoles_RoleID",
                table: "Workflows",
                column: "RoleID",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

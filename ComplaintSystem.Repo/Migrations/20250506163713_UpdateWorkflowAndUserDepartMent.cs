using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComplaintSystem.Repo.Migrations
{
    /// <inheritdoc />
    public partial class UpdateWorkflowAndUserDepartMent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "StepOrder",
                table: "Workflows",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
            
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Workflows",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentID",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Workflows_UserId",
                table: "Workflows",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workflows_AspNetUsers_UserId",
                table: "Workflows",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workflows_AspNetUsers_UserId",
                table: "Workflows");

            migrationBuilder.DropIndex(
                name: "IX_Workflows_UserId",
                table: "Workflows");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Workflows");

            migrationBuilder.AlterColumn<int>(
                name: "StepOrder",
                table: "Workflows",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentID",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}

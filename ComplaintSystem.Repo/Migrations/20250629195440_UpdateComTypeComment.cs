using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComplaintSystem.Repo.Migrations
{
    /// <inheritdoc />
    public partial class UpdateComTypeComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsForUser",
                table: "Comments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintTypes_TypeName",
                table: "ComplaintTypes",
                column: "TypeName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ComplaintTypes_TypeName",
                table: "ComplaintTypes");

            migrationBuilder.DropColumn(
                name: "IsForUser",
                table: "Comments");
        }
    }
}

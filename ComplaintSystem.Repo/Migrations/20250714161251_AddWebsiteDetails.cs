using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComplaintSystem.Repo.Migrations
{
    /// <inheritdoc />
    public partial class AddWebsiteDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WebSiteDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Governorate = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FacebookLink = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TwitterLink = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    InstagramLink = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    LinkedInLink = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YouTubeLink = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebSiteDetails", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WebSiteDetails");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PortalShell.Migrations
{
    /// <inheritdoc />
    public partial class AddPortalLinks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PortalLinks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: false),
                    DisplayOrder = table.Column<int>(type: "INTEGER", nullable: false),
                    IsEnabled = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortalLinks", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "PortalLinks",
                columns: new[] { "Id", "DisplayOrder", "IsEnabled", "Title", "Url" },
                values: new object[,]
                {
                    { 1, 1, true, "Service Intake Demo", "#" },
                    { 2, 2, true, "Licensing Dashboard", "#" },
                    { 3, 3, true, "Application Status", "#" },
                    { 4, 4, false, "Document Requests", "#" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PortalLinks");
        }
    }
}

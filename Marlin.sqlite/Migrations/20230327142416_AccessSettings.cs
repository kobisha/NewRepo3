using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marlin.sqlite.Migrations
{
    /// <inheritdoc />
    public partial class AccessSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccessSettings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProfileID = table.Column<string>(type: "TEXT", nullable: true),

                    AccessObject = table.Column<string>(type: "TEXT", nullable: true),
                    Grant = table.Column<string>(type: "TEXT", nullable: true)

                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessSettings", x => x.ID);
                });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessSettings");
        }
    }
}

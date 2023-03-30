using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marlin.sqlite.Migrations
{
    /// <inheritdoc />
    public partial class UserSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                               name: "UserSettings",
                               columns: table => new
                               {
                                   ID = table.Column<int>(type: "INTEGER", nullable: false)
                                       .Annotation("Sqlite:Autoincrement", true),
                                   UserID = table.Column<string>(type: "TEXT", nullable: true),
                                   ProfileID = table.Column<string>(type: "TEXT", nullable: true)




                               },
                                constraints: table =>
                                {
                                    table.PrimaryKey("PK_UserSettings", x => x.ID);
                                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
              name: "UserSettings");
        }
    }
}

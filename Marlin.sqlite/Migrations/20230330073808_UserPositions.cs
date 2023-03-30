using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marlin.sqlite.Migrations
{
    /// <inheritdoc />
    public partial class UserPositions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                    name: "UserPositions",
                    columns: table => new
                    {
                        ID = table.Column<int>(type: "INTEGER", nullable: false)
                            .Annotation("Sqlite:Autoincrement", true),
                        PositionID = table.Column<string>(type: "TEXT", nullable: true),
                        PositionName = table.Column<string>(type: "TEXT", nullable: true)
                        



                    },
                     constraints: table =>
                     {
                         table.PrimaryKey("PK_UserPositions", x => x.ID);
                     });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
              name: "UserPositions");
        }
    }
}

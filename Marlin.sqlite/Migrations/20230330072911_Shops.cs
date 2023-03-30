using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marlin.sqlite.Migrations
{
    /// <inheritdoc />
    public partial class Shops : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
           name: "Shops",
           columns: table => new
           {
               ID = table.Column<int>(type: "INTEGER", nullable: false)
                   .Annotation("Sqlite:Autoincrement", true),
               AccountID = table.Column<string>(type: "TEXT", nullable: true),
               ShopID = table.Column<string>(type: "TEXT", nullable: true),
               SourceCode = table.Column<string>(type: "TEXT", nullable: true),
               Name = table.Column<string>(type: "TEXT", nullable: true),
               Description = table.Column<string>(type: "TEXT", nullable: true),
               Address = table.Column<string>(type: "TEXT", nullable: true),
               ContactPerson = table.Column<string>(type: "TEXT", nullable: true),
               ContactNumber = table.Column<string>(type: "TEXT", nullable: true),
               Email = table.Column<string>(type: "TEXT", nullable: true),
               Region = table.Column<string>(type: "TEXT", nullable: true),
               Format = table.Column<string>(type: "TEXT", nullable: true),
               GPS = table.Column<string>(type: "TEXT", nullable: true)


           },
            constraints: table =>
            {
                table.PrimaryKey("PK_Shops", x => x.ID);
            });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "Shops");
        }
    }
}

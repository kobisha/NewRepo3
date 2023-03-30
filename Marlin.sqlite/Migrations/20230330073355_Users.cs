using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marlin.sqlite.Migrations
{
    /// <inheritdoc />
    public partial class Users : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
          name: "Users",
          columns: table => new
          {
              ID = table.Column<int>(type: "INTEGER", nullable: false)
                  .Annotation("Sqlite:Autoincrement", true),
              AccountID = table.Column<string>(type: "TEXT", nullable: true),
              UserID = table.Column<string>(type: "TEXT", nullable: true),
              FirstName = table.Column<string>(type: "TEXT", nullable: true),
              LastName = table.Column<string>(type: "TEXT", nullable: true),
              ContactNumber = table.Column<string>(type: "TEXT", nullable: true),
              Email = table.Column<string>(type: "TEXT", nullable: true),
              Description = table.Column<string>(type: "TEXT", nullable: true),
              PositionInCompany = table.Column<string>(type: "TEXT", nullable: true),
              Password = table.Column<string>(type: "TEXT", nullable: true)
              


          },
           constraints: table =>
           {
               table.PrimaryKey("PK_Users", x => x.ID);
           });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
              name: "Users");
        }
    }
}

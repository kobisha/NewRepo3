using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marlin.sqlite.Migrations
{
    /// <inheritdoc />
    public partial class ErrorCodes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "ErrorCodes",
               columns: table => new
               {
                   ID = table.Column<int>(type: "INTEGER", nullable: false)
                       .Annotation("Sqlite:Autoincrement", true),
                   Code = table.Column<string>(type: "TEXT", nullable: true),

                   Description = table.Column<string>(type: "TEXT", nullable: true)
               },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorCodes", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ErrorCodes");
        }
    }
}

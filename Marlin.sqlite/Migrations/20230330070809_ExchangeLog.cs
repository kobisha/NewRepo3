using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marlin.sqlite.Migrations
{
    /// <inheritdoc />
    public partial class ExchangeLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "ExchangeLog",
               columns: table => new
               {
                   ID = table.Column<int>(type: "INTEGER", nullable: false)
                       .Annotation("Sqlite:Autoincrement", true),
                   TransactionID = table.Column<string>(type: "TEXT", nullable: true),
                   Date = table.Column<DateTime>(type: "TEXT", nullable: true),

                   MessageID = table.Column<string>(type: "TEXT", nullable: true),
                   Status = table.Column<string>(type: "TEXT", nullable: true),
                   ErrorCode = table.Column<string>(type: "TEXT", nullable: true)
               },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeLog", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExchangeLog");
        }
    }
}

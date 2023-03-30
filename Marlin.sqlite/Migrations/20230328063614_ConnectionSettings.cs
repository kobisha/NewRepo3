using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marlin.sqlite.Migrations
{
    /// <inheritdoc />
    public partial class ConnectionSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConnectionSettings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AccountID = table.Column<string>(type: "TEXT", nullable: true),

                    ConnectedAccountID = table.Column<string>(type: "TEXT", nullable: true),
                    AsBuyer = table.Column<bool>(type: "REAL", nullable: true),
                    AsSupplier = table.Column<bool>(type: "REAL", nullable: true),

                    PriceTypes = table.Column<string>(type: "TEXT", nullable: true),
                    ConnectionStatus = table.Column<string>(type: "TEXT", nullable: true)

                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectionSettings", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "ConnectionSettings");
        }
    }
}

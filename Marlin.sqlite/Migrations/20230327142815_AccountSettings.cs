using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marlin.sqlite.Migrations
{
    /// <inheritdoc />
    public partial class AccountSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountSettings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AccountID = table.Column<string>(type: "TEXT", nullable: true),

                    BuyerWS = table.Column<string>(type: "TEXT", nullable: true),
                    SupplierWS = table.Column<string>(type: "TEXT", nullable: true),
                    BillingSettings = table.Column<string>(type: "TEXT", nullable: true)

                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountSettings", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "AccountSettings");
        }
    }
}

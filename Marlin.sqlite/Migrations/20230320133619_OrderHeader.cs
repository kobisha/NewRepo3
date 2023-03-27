using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marlin.sqlite.Migrations
{
    /// <inheritdoc />
    public partial class OrderHeader : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderHeaders",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderID = table.Column<string>(type: "TEXT", nullable: true),
                    SourceID = table.Column<string>(type: "TEXT", nullable: true),
                    Date = table.Column<DateTime>(type: "DATE", nullable: true),
                    Number = table.Column<string>(type: "TEXT", nullable: true),
                    SenderID = table.Column<string>(type: "TEXT", nullable: true),
                    ReceiverID = table.Column<string>(type: "TEXT", nullable: true),
                    ShopID = table.Column<string>(type: "TEXT", nullable: true),
                    Amount = table.Column<double>(type: "DOUBLE", nullable: true),
                    StatusID = table.Column<double>(type: "DOUBLE", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderHeaders");
        }
    }
}


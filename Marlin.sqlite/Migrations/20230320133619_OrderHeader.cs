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
                    OrderID = table.Column<string>(type: "TEXT", nullable: false),
                    SourceID = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "DATE", nullable: false),
                    Number = table.Column<string>(type: "TEXT", nullable: false),
                    SenderID = table.Column<string>(type: "TEXT", nullable: false),
                    ReceiverID = table.Column<string>(type: "TEXT", nullable: false),
                    ShopID = table.Column<string>(type: "TEXT", nullable: false),
                    Amount = table.Column<double>(type: "DOUBLE", nullable: false),
                    StatusID = table.Column<double>(type: "DOUBLE", nullable: false)
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


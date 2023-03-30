using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marlin.sqlite.Migrations
{
    /// <inheritdoc />
    public partial class Invoices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
              name: "Invoices",
              columns: table => new
              {
                  ID = table.Column<int>(type: "INTEGER", nullable: false)
                      .Annotation("Sqlite:Autoincrement", true),
                  AccountID = table.Column<string>(type: "TEXT", nullable: true),
                  OrderID = table.Column<string>(type: "TEXT", nullable: true),
                  Package = table.Column<string>(type: "TEXT", nullable: true),
                  Period = table.Column<string>(type: "TEXT", nullable: true),
                  Number = table.Column<string>(type: "TEXT", nullable: true),
                  DueDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                  Amount = table.Column<double>(type: "TEXT", nullable: true),
                  Status = table.Column<string>(type: "TEXT", nullable: true)

                 
              },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Invoices", x => x.ID);
               });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invoices");
        }
    }
}

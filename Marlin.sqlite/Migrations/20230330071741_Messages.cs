using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marlin.sqlite.Migrations
{
    /// <inheritdoc />
    public partial class Messages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
             name: "Messages",
             columns: table => new
             {
                 ID = table.Column<int>(type: "INTEGER", nullable: false)
                     .Annotation("Sqlite:Autoincrement", true),
                 MessageID = table.Column<string>(type: "TEXT", nullable: true),
                 Date = table.Column<DateTime>(type: "TEXT", nullable: true),
                 SenderID = table.Column<string>(type: "TEXT", nullable: true),
                 ReceiverID = table.Column<string>(type: "TEXT", nullable: true),
                 Type = table.Column<string>(type: "TEXT", nullable: true),
                 JSONBody = table.Column<string>(type: "TEXT", nullable: true)


             },
              constraints: table =>
              {
                  table.PrimaryKey("PK_Messages", x => x.ID);
              });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "Messages");
        }
    }
}

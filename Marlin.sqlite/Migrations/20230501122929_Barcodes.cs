using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marlin.sqlite.Migrations
{
    /// <inheritdoc />
    public partial class Barcodes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Barcodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AccountID = table.Column<string>(type: "TEXT", nullable: true),
                    ProductID = table.Column<string>(type: "TEXT", nullable: true),
                    Barcode = table.Column<string>(type: "TEXT", nullable: true),
                    
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Barcodes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down (MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Barcodes");
        }
    }
}
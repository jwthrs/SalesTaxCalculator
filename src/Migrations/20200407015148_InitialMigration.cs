using Microsoft.EntityFrameworkCore.Migrations;

namespace SalesTaxCalculator.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "stateSalesTaxes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    TaxRate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stateSalesTaxes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CountyTax",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    TaxRate = table.Column<string>(nullable: true),
                    StateSalesTaxId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountyTax", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CountyTax_stateSalesTaxes_StateSalesTaxId",
                        column: x => x.StateSalesTaxId,
                        principalTable: "stateSalesTaxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CountyTax_StateSalesTaxId",
                table: "CountyTax",
                column: "StateSalesTaxId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CountyTax");

            migrationBuilder.DropTable(
                name: "stateSalesTaxes");
        }
    }
}

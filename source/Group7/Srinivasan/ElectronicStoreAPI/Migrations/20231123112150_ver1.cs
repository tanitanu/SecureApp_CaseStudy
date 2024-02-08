using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectronicStoreAPI.Migrations
{
    /// <inheritdoc />
    public partial class ver1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BrandMstr",
                columns: table => new
                {
                    BrandCode = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BrandName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandMstr", x => x.BrandCode);
                });

            migrationBuilder.CreateTable(
                name: "CategoryMstr",
                columns: table => new
                {
                    CatCode = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CatDescription = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryMstr", x => x.CatCode);
                });

            migrationBuilder.CreateTable(
                name: "ProductMstr",
                columns: table => new
                {
                    ProdModelNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ProdCatCatCode = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProdBrandBrandCode = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProdDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProdMRP = table.Column<long>(type: "bigint", nullable: true),
                    ProdPrice = table.Column<long>(type: "bigint", nullable: true),
                    ProdQty = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductMstr", x => x.ProdModelNo);
                    table.ForeignKey(
                        name: "FK_ProductMstr_BrandMstr_ProdBrandBrandCode",
                        column: x => x.ProdBrandBrandCode,
                        principalTable: "BrandMstr",
                        principalColumn: "BrandCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductMstr_CategoryMstr_ProdCatCatCode",
                        column: x => x.ProdCatCatCode,
                        principalTable: "CategoryMstr",
                        principalColumn: "CatCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductMstr_ProdBrandBrandCode",
                table: "ProductMstr",
                column: "ProdBrandBrandCode");

            migrationBuilder.CreateIndex(
                name: "IX_ProductMstr_ProdCatCatCode",
                table: "ProductMstr",
                column: "ProdCatCatCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductMstr");

            migrationBuilder.DropTable(
                name: "BrandMstr");

            migrationBuilder.DropTable(
                name: "CategoryMstr");
        }
    }
}

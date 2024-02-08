using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DXC.BlogConnect.WebAPI.Migrations
{
    public partial class FKContraintAddedUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role",
                table: "User",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Role",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_RoleId",
                table: "User");
        }
    }
}

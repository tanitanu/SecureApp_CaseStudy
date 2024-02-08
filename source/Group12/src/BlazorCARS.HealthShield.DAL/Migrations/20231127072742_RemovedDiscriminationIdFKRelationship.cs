using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorCARS.HealthShield.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RemovedDiscriminationIdFKRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hospital_UserStore_DiscriminationId",
                table: "UserStore");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipient_UserStore_DiscriminationId",
                table: "UserStore");

            migrationBuilder.DropIndex(
                name: "IX_UserStore_DiscriminationId",
                table: "UserStore");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserStore_DiscriminationId",
                table: "UserStore",
                column: "DiscriminationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hospital_UserStore_DiscriminationId",
                table: "UserStore",
                column: "DiscriminationId",
                principalTable: "Hospital",
                principalColumn: "HospitalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipient_UserStore_DiscriminationId",
                table: "UserStore",
                column: "DiscriminationId",
                principalTable: "Recipient",
                principalColumn: "RecipientId");
        }
    }
}

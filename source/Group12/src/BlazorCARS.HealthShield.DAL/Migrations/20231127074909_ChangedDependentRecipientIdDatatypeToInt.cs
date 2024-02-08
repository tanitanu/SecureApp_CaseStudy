using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorCARS.HealthShield.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangedDependentRecipientIdDatatypeToInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DependentRecipientId",
                table: "Recipient",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DependentRecipientId",
                table: "Recipient",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}

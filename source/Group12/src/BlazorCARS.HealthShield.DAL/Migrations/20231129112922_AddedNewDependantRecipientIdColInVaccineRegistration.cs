using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorCARS.HealthShield.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewDependantRecipientIdColInVaccineRegistration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DependantRecipientId",
                table: "VaccineRegistration",
                type: "int",
                nullable: false,
                defaultValue: 1)
                .Annotation("Relational:ColumnOrder", 12);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DependantRecipientId",
                table: "VaccineRegistration");
        }
    }
}

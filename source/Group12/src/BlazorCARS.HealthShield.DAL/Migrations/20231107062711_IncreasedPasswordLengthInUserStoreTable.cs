using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorCARS.HealthShield.DAL.Migrations
{
    /// <inheritdoc />
    public partial class IncreasedPasswordLengthInUserStoreTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "UserStore",
                type: "varchar(250)",
                unicode: false,
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "UserStore",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldUnicode: false,
                oldMaxLength: 250);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagement.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tasks_status__StatusStatusId",
                table: "tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tasks",
                table: "tasks");

            migrationBuilder.DropIndex(
                name: "IX_tasks__StatusStatusId",
                table: "tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_status",
                table: "status");

            migrationBuilder.DropColumn(
                name: "_StatusStatusId",
                table: "tasks");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "status");

            migrationBuilder.RenameTable(
                name: "tasks",
                newName: "Tasks");

            migrationBuilder.RenameTable(
                name: "status",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "StatusCode",
                table: "Tasks",
                newName: "StatusId");

            migrationBuilder.RenameColumn(
                name: "StatusDescription",
                table: "Status",
                newName: "Code");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Status",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks",
                column: "TaskId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Status",
                table: "Status",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_StatusId",
                table: "Tasks",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Status_StatusId",
                table: "Tasks",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Status_StatusId",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_StatusId",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Status",
                table: "Status");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Status");

            migrationBuilder.RenameTable(
                name: "Tasks",
                newName: "tasks");

            migrationBuilder.RenameTable(
                name: "Status",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "tasks",
                newName: "StatusCode");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "status",
                newName: "StatusDescription");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "tasks",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "_StatusStatusId",
                table: "tasks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "StatusId",
                table: "status",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_tasks",
                table: "tasks",
                column: "Title");

            migrationBuilder.AddPrimaryKey(
                name: "PK_status",
                table: "status",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_tasks__StatusStatusId",
                table: "tasks",
                column: "_StatusStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_status__StatusStatusId",
                table: "tasks",
                column: "_StatusStatusId",
                principalTable: "status",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

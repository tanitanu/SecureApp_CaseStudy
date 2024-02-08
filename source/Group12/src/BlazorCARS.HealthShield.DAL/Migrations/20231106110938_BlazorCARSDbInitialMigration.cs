using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlazorCARS.HealthShield.DAL.Migrations
{
    /// <inheritdoc />
    public partial class BlazorCARSDbInitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CreatedUser = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryId", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserRoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CreatedUser = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleId", x => x.UserRoleId);
                });

            migrationBuilder.CreateTable(
                name: "Vaccine",
                columns: table => new
                {
                    VaccineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgeGroup = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUser = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaccineId", x => x.VaccineId);
                });

            migrationBuilder.CreateTable(
                name: "State",
                columns: table => new
                {
                    StateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    CreatedUser = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateId", x => x.StateId);
                    table.ForeignKey(
                        name: "FK_Country_State_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "CountryId");
                });

            migrationBuilder.CreateTable(
                name: "Hospital",
                columns: table => new
                {
                    HospitalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Address1 = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Address2 = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Landmark = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    City = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    PostalCode = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    PrimaryContact = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: false),
                    SecondaryContact = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: true),
                    EmergencyContact = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: false),
                    EmailId = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Discrimination = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: true),
                    RegistrationStatus = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: false),
                    CreatedUser = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    LicenseNo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HospitalId", x => x.HospitalId);
                    table.ForeignKey(
                        name: "FK_Country_Hospital_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "CountryId");
                    table.ForeignKey(
                        name: "FK_State_Hospital_StateId",
                        column: x => x.StateId,
                        principalTable: "State",
                        principalColumn: "StateId");
                });

            migrationBuilder.CreateTable(
                name: "Recipient",
                columns: table => new
                {
                    RecipientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    AadhaarNo = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime", nullable: false),
                    Address1 = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Address2 = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Landmark = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    City = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    PostalCode = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    PrimaryContact = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: false),
                    SecondaryContact = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: true),
                    EmergencyContact = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: false),
                    EmailId = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    RelationshipType = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    DependentRecipientId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dose = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: false),
                    CreatedUser = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipientId", x => x.RecipientId);
                    table.ForeignKey(
                        name: "FK_Country_Recipient_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "CountryId");
                    table.ForeignKey(
                        name: "FK_State_Recipient_StateId",
                        column: x => x.StateId,
                        principalTable: "State",
                        principalColumn: "StateId");
                });

            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    InventoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HospitalId = table.Column<int>(type: "int", nullable: false),
                    VaccineId = table.Column<int>(type: "int", nullable: false),
                    OpenStock = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ReorderLevel = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    CreatedUser = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryId", x => x.InventoryId);
                    table.ForeignKey(
                        name: "FK_Hospital_Inventory_HospitalId",
                        column: x => x.HospitalId,
                        principalTable: "Hospital",
                        principalColumn: "HospitalId");
                    table.ForeignKey(
                        name: "FK_Vaccine_Inventory_VaccineId",
                        column: x => x.VaccineId,
                        principalTable: "Vaccine",
                        principalColumn: "VaccineId");
                });

            migrationBuilder.CreateTable(
                name: "InventoryTransaction",
                columns: table => new
                {
                    InventoryTransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HospitalId = table.Column<int>(type: "int", nullable: false),
                    VaccineId = table.Column<int>(type: "int", nullable: false),
                    ReceivedQty = table.Column<int>(type: "int", nullable: false),
                    ReceivedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    RefNo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CreatedUser = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryTransactionId", x => x.InventoryTransactionId);
                    table.ForeignKey(
                        name: "FK_Hospital_InventoryTransaction_HospitalId",
                        column: x => x.HospitalId,
                        principalTable: "Hospital",
                        principalColumn: "HospitalId");
                    table.ForeignKey(
                        name: "FK_Vaccine_InventoryTransaction_VaccineId",
                        column: x => x.VaccineId,
                        principalTable: "Vaccine",
                        principalColumn: "VaccineId");
                });

            migrationBuilder.CreateTable(
                name: "VaccineSchedule",
                columns: table => new
                {
                    VaccineScheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HospitalId = table.Column<int>(type: "int", nullable: false),
                    VaccineId = table.Column<int>(type: "int", nullable: false),
                    ScheduleDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    TimeSlot = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: false),
                    CreatedUser = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaccineScheduleId", x => x.VaccineScheduleId);
                    table.ForeignKey(
                        name: "FK_Hospital_VaccineSchedule_HospitalId",
                        column: x => x.HospitalId,
                        principalTable: "Hospital",
                        principalColumn: "HospitalId");
                    table.ForeignKey(
                        name: "FK_Vaccine_VaccineSchedule_VaccineId",
                        column: x => x.VaccineId,
                        principalTable: "Vaccine",
                        principalColumn: "VaccineId");
                });

            migrationBuilder.CreateTable(
                name: "UserStore",
                columns: table => new
                {
                    UserStoreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    DiscriminationId = table.Column<int>(type: "int", nullable: true),
                    CreatedUser = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UserRoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStorreId", x => x.UserStoreId);
                    table.ForeignKey(
                        name: "FK_Hospital_UserStore_DiscriminationId",
                        column: x => x.DiscriminationId,
                        principalTable: "Hospital",
                        principalColumn: "HospitalId");
                    table.ForeignKey(
                        name: "FK_Recipient_UserStore_DiscriminationId",
                        column: x => x.DiscriminationId,
                        principalTable: "Recipient",
                        principalColumn: "RecipientId");
                    table.ForeignKey(
                        name: "FK_UserRole_UserStore_UserRoleId",
                        column: x => x.UserRoleId,
                        principalTable: "UserRole",
                        principalColumn: "UserRoleId");
                });

            migrationBuilder.CreateTable(
                name: "VaccineRegistration",
                columns: table => new
                {
                    VaccineRegistrationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipientId = table.Column<int>(type: "int", nullable: false),
                    VaccineScheduleId = table.Column<int>(type: "int", nullable: false),
                    IsVaccinated = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    TimeSlot = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: false),
                    Dose = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: false),
                    CreatedUser = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaccineRegistrationId", x => x.VaccineRegistrationId);
                    table.ForeignKey(
                        name: "FK_Recipient_VaccineRegistration_HospitalId",
                        column: x => x.RecipientId,
                        principalTable: "Recipient",
                        principalColumn: "RecipientId");
                    table.ForeignKey(
                        name: "FK_VaccineSchedule_VaccineRegistration_VaccineScheduleId",
                        column: x => x.VaccineScheduleId,
                        principalTable: "VaccineSchedule",
                        principalColumn: "VaccineScheduleId");
                });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "CountryId", "CreatedDateTime", "CreatedUser", "Name", "UpdatedDateTime", "UpdatedUser" },
                values: new object[] { 1, new DateTime(2023, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seeded", "India", new DateTime(2023, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seeded" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "UserRoleId", "CreatedDateTime", "CreatedUser", "Name", "UpdatedDateTime", "UpdatedUser" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seeded", "Super Admin", new DateTime(2023, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seeded" },
                    { 2, new DateTime(2023, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seeded", "Admin", new DateTime(2023, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seeded" },
                    { 3, new DateTime(2023, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seeded", "User", new DateTime(2023, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seeded" }
                });

            migrationBuilder.CreateIndex(
                name: "UQ_Country_Name",
                table: "Country",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hospital_CountryId",
                table: "Hospital",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Hospital_StateId",
                table: "Hospital",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "UQ_Inventory_Name",
                table: "Hospital",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_VaccineId",
                table: "Inventory",
                column: "VaccineId");

            migrationBuilder.CreateIndex(
                name: "UQ_Inventory_HospitalId_VaccineId",
                table: "Inventory",
                columns: new[] { "HospitalId", "VaccineId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransaction_HospitalId",
                table: "InventoryTransaction",
                column: "HospitalId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransaction_VaccineId",
                table: "InventoryTransaction",
                column: "VaccineId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipient_CountryId",
                table: "Recipient",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipient_StateId",
                table: "Recipient",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "UQ_Inventory_AadhaarNo",
                table: "Recipient",
                column: "AadhaarNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_State_CountryId",
                table: "State",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "UQ_State_Name",
                table: "State",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_UserRole_Name",
                table: "UserRole",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserStore_DiscriminationId",
                table: "UserStore",
                column: "DiscriminationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserStore_UserRoleId",
                table: "UserStore",
                column: "UserRoleId");

            migrationBuilder.CreateIndex(
                name: "UQ_UserStore_UserName",
                table: "UserStore",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Vaccine_Name",
                table: "Vaccine",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VaccineRegistration_VaccineScheduleId",
                table: "VaccineRegistration",
                column: "VaccineScheduleId");

            migrationBuilder.CreateIndex(
                name: "UQ_Inventory_RecipientId_Dose",
                table: "VaccineRegistration",
                columns: new[] { "RecipientId", "Dose" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VaccineSchedule_VaccineId",
                table: "VaccineSchedule",
                column: "VaccineId");

            migrationBuilder.CreateIndex(
                name: "UQ_Inventory_HospitalId_VaccineId_ScheduleDate_TimeSlot",
                table: "VaccineSchedule",
                columns: new[] { "HospitalId", "VaccineId", "ScheduleDate", "TimeSlot" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inventory");

            migrationBuilder.DropTable(
                name: "InventoryTransaction");

            migrationBuilder.DropTable(
                name: "UserStore");

            migrationBuilder.DropTable(
                name: "VaccineRegistration");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "Recipient");

            migrationBuilder.DropTable(
                name: "VaccineSchedule");

            migrationBuilder.DropTable(
                name: "Hospital");

            migrationBuilder.DropTable(
                name: "Vaccine");

            migrationBuilder.DropTable(
                name: "State");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}

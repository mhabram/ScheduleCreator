using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ScheduleCreator.EntityFramework.Migrations
{
    public partial class CompleteRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Schedules",
                table: "Schedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "FreeWorkingDays",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Shift",
                table: "Employees");

            migrationBuilder.AlterColumn<short>(
                name: "WorkingDays",
                table: "Schedules",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<short>(
                name: "Month",
                table: "Schedules",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ScheduleId",
                table: "Schedules",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "Shift",
                table: "Schedules",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte>(
                name: "Year",
                table: "Schedules",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Employees",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Employees",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Employees",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "PreferencesId",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Schedules",
                table: "Schedules",
                column: "ScheduleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "EmployeeId");

            migrationBuilder.CreateTable(
                name: "EmployeeSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(nullable: false),
                    ScheduleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeSchedules_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeSchedules_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Preferences",
                columns: table => new
                {
                    PreferencesId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FreeWorkingDays = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preferences", x => x.PreferencesId);
                });

            migrationBuilder.CreateTable(
                name: "Dates",
                columns: table => new
                {
                    DateId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FreeDayChosen = table.Column<DateTime>(nullable: false),
                    PreferencesId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dates", x => x.DateId);
                    table.ForeignKey(
                        name: "FK_Dates_Preferences_PreferencesId",
                        column: x => x.PreferencesId,
                        principalTable: "Preferences",
                        principalColumn: "PreferencesId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PreferencesId",
                table: "Employees",
                column: "PreferencesId",
                unique: true,
                filter: "[PreferencesId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Dates_PreferencesId",
                table: "Dates",
                column: "PreferencesId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSchedules_EmployeeId",
                table: "EmployeeSchedules",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSchedules_ScheduleId",
                table: "EmployeeSchedules",
                column: "ScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Preferences_PreferencesId",
                table: "Employees",
                column: "PreferencesId",
                principalTable: "Preferences",
                principalColumn: "PreferencesId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Preferences_PreferencesId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "Dates");

            migrationBuilder.DropTable(
                name: "EmployeeSchedules");

            migrationBuilder.DropTable(
                name: "Preferences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Schedules",
                table: "Schedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_PreferencesId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ScheduleId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Shift",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PreferencesId",
                table: "Employees");

            migrationBuilder.AlterColumn<int>(
                name: "WorkingDays",
                table: "Schedules",
                type: "int",
                nullable: false,
                oldClrType: typeof(short));

            migrationBuilder.AlterColumn<int>(
                name: "Month",
                table: "Schedules",
                type: "int",
                nullable: false,
                oldClrType: typeof(short));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Schedules",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "FreeWorkingDays",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Shift",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Schedules",
                table: "Schedules",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "Id");
        }
    }
}

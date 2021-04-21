using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ScheduleCreator.EntityFramework.Migrations
{
    public partial class UnusedModelsRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Days_Weeks_WeekId",
                table: "Days");

            migrationBuilder.DropTable(
                name: "Dates");

            migrationBuilder.DropTable(
                name: "EmployeeSchedules");

            migrationBuilder.DropTable(
                name: "Weeks");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.RenameColumn(
                name: "WeekId",
                table: "Days",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Days_WeekId",
                table: "Days",
                newName: "IX_Days_EmployeeId");

            migrationBuilder.AddColumn<string>(
                name: "MonthId",
                table: "Days",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PreferenceDays",
                columns: table => new
                {
                    PreferenceDayId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FreeDayChosen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PreferencesId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreferenceDays", x => x.PreferenceDayId);
                    table.ForeignKey(
                        name: "FK_PreferenceDays_Preferences_PreferencesId",
                        column: x => x.PreferencesId,
                        principalTable: "Preferences",
                        principalColumn: "PreferencesId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PreferenceDays_PreferencesId",
                table: "PreferenceDays",
                column: "PreferencesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Days_Employees_EmployeeId",
                table: "Days",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Days_Employees_EmployeeId",
                table: "Days");

            migrationBuilder.DropTable(
                name: "PreferenceDays");

            migrationBuilder.DropColumn(
                name: "MonthId",
                table: "Days");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Days",
                newName: "WeekId");

            migrationBuilder.RenameIndex(
                name: "IX_Days_EmployeeId",
                table: "Days",
                newName: "IX_Days_WeekId");

            migrationBuilder.CreateTable(
                name: "Dates",
                columns: table => new
                {
                    DateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FreeDayChosen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PreferencesId = table.Column<int>(type: "int", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    ScheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Month = table.Column<short>(type: "smallint", nullable: false),
                    Shift = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    WorkingDays = table.Column<short>(type: "smallint", nullable: false),
                    Year = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.ScheduleId);
                });

            migrationBuilder.CreateTable(
                name: "Weeks",
                columns: table => new
                {
                    WeekId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    InternalWeekId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weeks", x => x.WeekId);
                    table.ForeignKey(
                        name: "FK_Weeks_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    ScheduleId = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_Weeks_EmployeeId",
                table: "Weeks",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Days_Weeks_WeekId",
                table: "Days",
                column: "WeekId",
                principalTable: "Weeks",
                principalColumn: "WeekId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

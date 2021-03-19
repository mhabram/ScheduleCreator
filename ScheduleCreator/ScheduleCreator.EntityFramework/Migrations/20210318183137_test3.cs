using Microsoft.EntityFrameworkCore.Migrations;

namespace ScheduleCreator.EntityFramework.Migrations
{
    public partial class test3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Shift",
                table: "Week");

            migrationBuilder.AddColumn<string>(
                name: "Shift",
                table: "Day",
                type: "nvarchar(1)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Shift",
                table: "Day");

            migrationBuilder.AddColumn<string>(
                name: "Shift",
                table: "Week",
                type: "nvarchar(1)",
                nullable: false,
                defaultValue: "");
        }
    }
}

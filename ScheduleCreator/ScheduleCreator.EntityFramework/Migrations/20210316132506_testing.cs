using Microsoft.EntityFrameworkCore.Migrations;

namespace ScheduleCreator.EntityFramework.Migrations
{
    public partial class testing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InternalWeekId",
                table: "Week",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InternalWeekId",
                table: "Week");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace ScheduleCreator.EntityFramework.Migrations
{
    public partial class testing2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InternalId",
                table: "Preferences");

            migrationBuilder.AddColumn<string>(
                name: "InternalPreferenceId",
                table: "Preferences",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InternalPreferenceId",
                table: "Preferences");

            migrationBuilder.AddColumn<string>(
                name: "InternalId",
                table: "Preferences",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

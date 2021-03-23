using Microsoft.EntityFrameworkCore.Migrations;

namespace ScheduleCreator.EntityFramework.Migrations
{
    public partial class NewValueInDayModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkingDays",
                table: "Weeks");

            migrationBuilder.AddColumn<byte>(
                name: "AgentNumber",
                table: "Days",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgentNumber",
                table: "Days");

            migrationBuilder.AddColumn<byte>(
                name: "WorkingDays",
                table: "Weeks",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}

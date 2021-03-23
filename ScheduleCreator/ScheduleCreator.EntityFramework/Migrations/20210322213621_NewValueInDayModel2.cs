using Microsoft.EntityFrameworkCore.Migrations;

namespace ScheduleCreator.EntityFramework.Migrations
{
    public partial class NewValueInDayModel2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgentNumber",
                table: "Days");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "AgentNumber",
                table: "Days",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}

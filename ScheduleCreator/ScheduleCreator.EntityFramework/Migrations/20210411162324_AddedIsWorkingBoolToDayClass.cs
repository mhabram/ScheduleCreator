using Microsoft.EntityFrameworkCore.Migrations;

namespace ScheduleCreator.EntityFramework.Migrations
{
    public partial class AddedIsWorkingBoolToDayClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Shift",
                table: "Days",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1)");

            migrationBuilder.AddColumn<bool>(
                name: "IsWorking",
                table: "Days",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsWorking",
                table: "Days");

            migrationBuilder.AlterColumn<string>(
                name: "Shift",
                table: "Days",
                type: "nvarchar(1)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}

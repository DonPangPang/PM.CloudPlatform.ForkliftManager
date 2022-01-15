using Microsoft.EntityFrameworkCore.Migrations;

namespace PM.CloudPlatform.ForkliftManager.Apis.Migrations
{
    public partial class init_db_07 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "UseRecord",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "TerminalLoginRecord",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "TerminalBindRecord",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Terminal",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "RentalRecord",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "GpsPositionRecord",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CarType",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Maintainer",
                table: "CarMaintenanceRecord",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaintainerContent",
                table: "CarMaintenanceRecord",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaintainerTel",
                table: "CarMaintenanceRecord",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CarMaintenanceRecord",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "CarMaintenanceRecord",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Car",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "UseRecord");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "TerminalLoginRecord");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "TerminalBindRecord");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Terminal");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "RentalRecord");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "GpsPositionRecord");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CarType");

            migrationBuilder.DropColumn(
                name: "Maintainer",
                table: "CarMaintenanceRecord");

            migrationBuilder.DropColumn(
                name: "MaintainerContent",
                table: "CarMaintenanceRecord");

            migrationBuilder.DropColumn(
                name: "MaintainerTel",
                table: "CarMaintenanceRecord");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CarMaintenanceRecord");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "CarMaintenanceRecord");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Car");
        }
    }
}

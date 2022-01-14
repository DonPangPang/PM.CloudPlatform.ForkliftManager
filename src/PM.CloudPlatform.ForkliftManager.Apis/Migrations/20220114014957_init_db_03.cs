using Microsoft.EntityFrameworkCore.Migrations;

namespace PM.CloudPlatform.ForkliftManager.Apis.Migrations
{
    public partial class init_db_03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EorWLon",
                table: "GpsPositionRecord",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GpsInfoLength",
                table: "GpsPositionRecord",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GpsLocatedFunc",
                table: "GpsPositionRecord",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GpsSatelliteCount",
                table: "GpsPositionRecord",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Heading",
                table: "GpsPositionRecord",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IsGpsLocated",
                table: "GpsPositionRecord",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SorNLat",
                table: "GpsPositionRecord",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EorWLon",
                table: "GpsPositionRecord");

            migrationBuilder.DropColumn(
                name: "GpsInfoLength",
                table: "GpsPositionRecord");

            migrationBuilder.DropColumn(
                name: "GpsLocatedFunc",
                table: "GpsPositionRecord");

            migrationBuilder.DropColumn(
                name: "GpsSatelliteCount",
                table: "GpsPositionRecord");

            migrationBuilder.DropColumn(
                name: "Heading",
                table: "GpsPositionRecord");

            migrationBuilder.DropColumn(
                name: "IsGpsLocated",
                table: "GpsPositionRecord");

            migrationBuilder.DropColumn(
                name: "SorNLat",
                table: "GpsPositionRecord");
        }
    }
}

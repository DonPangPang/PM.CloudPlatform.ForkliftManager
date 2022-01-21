using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace PM.CloudPlatform.ForkliftManager.Apis.Migrations
{
    public partial class init_db_16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Point>(
                name: "Point",
                table: "GpsPositionRecord",
                type: "geometry",
                nullable: true);

            migrationBuilder.AddColumn<Polygon>(
                name: "Border",
                table: "ElectronicFence",
                type: "geometry",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Point",
                table: "GpsPositionRecord");

            migrationBuilder.DropColumn(
                name: "Border",
                table: "ElectronicFence");
        }
    }
}

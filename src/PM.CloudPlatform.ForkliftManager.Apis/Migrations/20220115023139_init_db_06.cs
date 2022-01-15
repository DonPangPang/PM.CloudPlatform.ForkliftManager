using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PM.CloudPlatform.ForkliftManager.Apis.Migrations
{
    public partial class init_db_06 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_RentalRecord_RentalRecordId",
                table: "Car");

            migrationBuilder.DropIndex(
                name: "IX_Car_RentalRecordId",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "RentalRecordId",
                table: "Car");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReturnTime",
                table: "RentalRecord",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RentalStartTime",
                table: "RentalRecord",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RentalEndTime",
                table: "RentalRecord",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<string>(
                name: "RentalEmployeeTel",
                table: "RentalRecord",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<Guid>(
                name: "TerminalId",
                table: "GpsPositionRecord",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaintenanceDateLength",
                table: "CarMaintenanceRecord",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CarRentalRecord",
                columns: table => new
                {
                    CarsId = table.Column<Guid>(type: "uuid", nullable: false),
                    RentalRecordsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarRentalRecord", x => new { x.CarsId, x.RentalRecordsId });
                    table.ForeignKey(
                        name: "FK_CarRentalRecord_Car_CarsId",
                        column: x => x.CarsId,
                        principalTable: "Car",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarRentalRecord_RentalRecord_RentalRecordsId",
                        column: x => x.RentalRecordsId,
                        principalTable: "RentalRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GpsPositionRecord_TerminalId",
                table: "GpsPositionRecord",
                column: "TerminalId");

            migrationBuilder.CreateIndex(
                name: "IX_CarRentalRecord_RentalRecordsId",
                table: "CarRentalRecord",
                column: "RentalRecordsId");

            migrationBuilder.AddForeignKey(
                name: "FK_GpsPositionRecord_Terminal_TerminalId",
                table: "GpsPositionRecord",
                column: "TerminalId",
                principalTable: "Terminal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GpsPositionRecord_Terminal_TerminalId",
                table: "GpsPositionRecord");

            migrationBuilder.DropTable(
                name: "CarRentalRecord");

            migrationBuilder.DropIndex(
                name: "IX_GpsPositionRecord_TerminalId",
                table: "GpsPositionRecord");

            migrationBuilder.DropColumn(
                name: "TerminalId",
                table: "GpsPositionRecord");

            migrationBuilder.DropColumn(
                name: "MaintenanceDateLength",
                table: "CarMaintenanceRecord");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReturnTime",
                table: "RentalRecord",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RentalStartTime",
                table: "RentalRecord",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RentalEndTime",
                table: "RentalRecord",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RentalEmployeeTel",
                table: "RentalRecord",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RentalRecordId",
                table: "Car",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Car_RentalRecordId",
                table: "Car",
                column: "RentalRecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_RentalRecord_RentalRecordId",
                table: "Car",
                column: "RentalRecordId",
                principalTable: "RentalRecord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PM.CloudPlatform.ForkliftManager.Apis.Migrations
{
    public partial class init_db_09 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Terminal_Car_CarId",
                table: "Terminal");

            migrationBuilder.DropTable(
                name: "CarRentalRecord");

            migrationBuilder.DropIndex(
                name: "IX_Terminal_CarId",
                table: "Terminal");

            migrationBuilder.AddColumn<Guid>(
                name: "CarId",
                table: "RentalRecord",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TerminalId",
                table: "Car",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RentalRecord_CarId",
                table: "RentalRecord",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Car_TerminalId",
                table: "Car",
                column: "TerminalId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Car_Terminal_TerminalId",
                table: "Car",
                column: "TerminalId",
                principalTable: "Terminal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalRecord_Car_CarId",
                table: "RentalRecord",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_Terminal_TerminalId",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalRecord_Car_CarId",
                table: "RentalRecord");

            migrationBuilder.DropIndex(
                name: "IX_RentalRecord_CarId",
                table: "RentalRecord");

            migrationBuilder.DropIndex(
                name: "IX_Car_TerminalId",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "RentalRecord");

            migrationBuilder.DropColumn(
                name: "TerminalId",
                table: "Car");

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
                name: "IX_Terminal_CarId",
                table: "Terminal",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_CarRentalRecord_RentalRecordsId",
                table: "CarRentalRecord",
                column: "RentalRecordsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Terminal_Car_CarId",
                table: "Terminal",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PM.CloudPlatform.ForkliftManager.Apis.Migrations
{
    public partial class init_db_02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentalRecord_Car_CarId",
                table: "RentalRecord");

            migrationBuilder.DropIndex(
                name: "IX_RentalRecord_CarId",
                table: "RentalRecord");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "RentalRecord");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<Guid>(
                name: "CarId",
                table: "RentalRecord",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RentalRecord_CarId",
                table: "RentalRecord",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_RentalRecord_Car_CarId",
                table: "RentalRecord",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PM.CloudPlatform.ForkliftManager.Apis.Migrations
{
    public partial class init_db_12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ElectronicFenceId",
                table: "RentalRecord",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RentalRecord_ElectronicFenceId",
                table: "RentalRecord",
                column: "ElectronicFenceId");

            migrationBuilder.AddForeignKey(
                name: "FK_RentalRecord_ElectronicFence_ElectronicFenceId",
                table: "RentalRecord",
                column: "ElectronicFenceId",
                principalTable: "ElectronicFence",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentalRecord_ElectronicFence_ElectronicFenceId",
                table: "RentalRecord");

            migrationBuilder.DropIndex(
                name: "IX_RentalRecord_ElectronicFenceId",
                table: "RentalRecord");

            migrationBuilder.DropColumn(
                name: "ElectronicFenceId",
                table: "RentalRecord");
        }
    }
}

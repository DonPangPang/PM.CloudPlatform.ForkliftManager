using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PM.CloudPlatform.ForkliftManager.Apis.Migrations
{
    public partial class init_db_19 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarMaintenanceRecord_Car_CarId",
                table: "CarMaintenanceRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_ElectronicFence_RentalCompany_RentalCompanyId",
                table: "ElectronicFence");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalRecord_RentalCompany_RentalCompanyId",
                table: "RentalRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_TerminalBindRecord_Car_CarId",
                table: "TerminalBindRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_TerminalBindRecord_Terminal_TerminalId",
                table: "TerminalBindRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_TerminalLoginRecord_Terminal_TerminalId",
                table: "TerminalLoginRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_UseRecord_Car_CarId",
                table: "UseRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_UseRecord_Terminal_TerminalId",
                table: "UseRecord");

            migrationBuilder.AlterColumn<Guid>(
                name: "TerminalId",
                table: "UseRecord",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "CarId",
                table: "UseRecord",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "TerminalId",
                table: "TerminalLoginRecord",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "TerminalId",
                table: "TerminalBindRecord",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "CarId",
                table: "TerminalBindRecord",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "RentalCompanyId",
                table: "RentalRecord",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "RentalCompanyId",
                table: "ElectronicFence",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "CarId",
                table: "CarMaintenanceRecord",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_CarMaintenanceRecord_Car_CarId",
                table: "CarMaintenanceRecord",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ElectronicFence_RentalCompany_RentalCompanyId",
                table: "ElectronicFence",
                column: "RentalCompanyId",
                principalTable: "RentalCompany",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalRecord_RentalCompany_RentalCompanyId",
                table: "RentalRecord",
                column: "RentalCompanyId",
                principalTable: "RentalCompany",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TerminalBindRecord_Car_CarId",
                table: "TerminalBindRecord",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TerminalBindRecord_Terminal_TerminalId",
                table: "TerminalBindRecord",
                column: "TerminalId",
                principalTable: "Terminal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TerminalLoginRecord_Terminal_TerminalId",
                table: "TerminalLoginRecord",
                column: "TerminalId",
                principalTable: "Terminal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UseRecord_Car_CarId",
                table: "UseRecord",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UseRecord_Terminal_TerminalId",
                table: "UseRecord",
                column: "TerminalId",
                principalTable: "Terminal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarMaintenanceRecord_Car_CarId",
                table: "CarMaintenanceRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_ElectronicFence_RentalCompany_RentalCompanyId",
                table: "ElectronicFence");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalRecord_RentalCompany_RentalCompanyId",
                table: "RentalRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_TerminalBindRecord_Car_CarId",
                table: "TerminalBindRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_TerminalBindRecord_Terminal_TerminalId",
                table: "TerminalBindRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_TerminalLoginRecord_Terminal_TerminalId",
                table: "TerminalLoginRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_UseRecord_Car_CarId",
                table: "UseRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_UseRecord_Terminal_TerminalId",
                table: "UseRecord");

            migrationBuilder.AlterColumn<Guid>(
                name: "TerminalId",
                table: "UseRecord",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CarId",
                table: "UseRecord",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TerminalId",
                table: "TerminalLoginRecord",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TerminalId",
                table: "TerminalBindRecord",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CarId",
                table: "TerminalBindRecord",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "RentalCompanyId",
                table: "RentalRecord",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "RentalCompanyId",
                table: "ElectronicFence",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CarId",
                table: "CarMaintenanceRecord",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CarMaintenanceRecord_Car_CarId",
                table: "CarMaintenanceRecord",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ElectronicFence_RentalCompany_RentalCompanyId",
                table: "ElectronicFence",
                column: "RentalCompanyId",
                principalTable: "RentalCompany",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalRecord_RentalCompany_RentalCompanyId",
                table: "RentalRecord",
                column: "RentalCompanyId",
                principalTable: "RentalCompany",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TerminalBindRecord_Car_CarId",
                table: "TerminalBindRecord",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TerminalBindRecord_Terminal_TerminalId",
                table: "TerminalBindRecord",
                column: "TerminalId",
                principalTable: "Terminal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TerminalLoginRecord_Terminal_TerminalId",
                table: "TerminalLoginRecord",
                column: "TerminalId",
                principalTable: "Terminal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UseRecord_Car_CarId",
                table: "UseRecord",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UseRecord_Terminal_TerminalId",
                table: "UseRecord",
                column: "TerminalId",
                principalTable: "Terminal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

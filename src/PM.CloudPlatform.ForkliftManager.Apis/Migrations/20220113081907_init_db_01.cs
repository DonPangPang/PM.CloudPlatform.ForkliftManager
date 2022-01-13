using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PM.CloudPlatform.ForkliftManager.Apis.Migrations
{
    public partial class init_db_01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_RentalRecord_RentalRecordId",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_Module_Role_RoleId",
                table: "Module");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_User_UserId",
                table: "Role");

            migrationBuilder.DropIndex(
                name: "IX_Role_UserId",
                table: "Role");

            migrationBuilder.DropIndex(
                name: "IX_Module_RoleId",
                table: "Module");

            migrationBuilder.DropIndex(
                name: "IX_Car_RentalRecordId",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Module");

            migrationBuilder.DropColumn(
                name: "RentalRecordId",
                table: "Car");

            migrationBuilder.AlterColumn<string>(
                name: "ReturnAckEmployeeTel",
                table: "RentalRecord",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ReturnAckEmployeeName",
                table: "RentalRecord",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<Guid>(
                name: "CarId",
                table: "RentalRecord",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Module",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "LngLats",
                table: "ElectronicFence",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ElectronicFence",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "SerialNumber",
                table: "Car",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "NumberOfMaintenance",
                table: "Car",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "LicensePlateNumber",
                table: "Car",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "LengthOfUse",
                table: "Car",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastMaintenanceOfTime",
                table: "Car",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<int>(
                name: "LastLengthOfUse",
                table: "Car",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "CarModel",
                table: "Car",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BuyTime",
                table: "Car",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<string>(
                name: "Brand",
                table: "Car",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "ModuleRole",
                columns: table => new
                {
                    ModulesId = table.Column<Guid>(type: "uuid", nullable: false),
                    RolesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleRole", x => new { x.ModulesId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_ModuleRole_Module_ModulesId",
                        column: x => x.ModulesId,
                        principalTable: "Module",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModuleRole_Role_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleUser",
                columns: table => new
                {
                    RolesId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUser", x => new { x.RolesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_RoleUser_Role_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUser_User_UsersId",
                        column: x => x.UsersId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RentalRecord_CarId",
                table: "RentalRecord",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleRole_RolesId",
                table: "ModuleRole",
                column: "RolesId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleUser_UsersId",
                table: "RoleUser",
                column: "UsersId");

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
                name: "FK_RentalRecord_Car_CarId",
                table: "RentalRecord");

            migrationBuilder.DropTable(
                name: "ModuleRole");

            migrationBuilder.DropTable(
                name: "RoleUser");

            migrationBuilder.DropIndex(
                name: "IX_RentalRecord_CarId",
                table: "RentalRecord");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "RentalRecord");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Role",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReturnAckEmployeeTel",
                table: "RentalRecord",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReturnAckEmployeeName",
                table: "RentalRecord",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Module",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RoleId",
                table: "Module",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LngLats",
                table: "ElectronicFence",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ElectronicFence",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SerialNumber",
                table: "Car",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NumberOfMaintenance",
                table: "Car",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LicensePlateNumber",
                table: "Car",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LengthOfUse",
                table: "Car",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastMaintenanceOfTime",
                table: "Car",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LastLengthOfUse",
                table: "Car",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CarModel",
                table: "Car",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "BuyTime",
                table: "Car",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Brand",
                table: "Car",
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
                name: "IX_Role_UserId",
                table: "Role",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Module_RoleId",
                table: "Module",
                column: "RoleId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Module_Role_RoleId",
                table: "Module",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Role_User_UserId",
                table: "Role",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

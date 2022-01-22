using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PM.CloudPlatform.ForkliftManager.Apis.Migrations
{
    public partial class init_db_21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlarmRecord",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TerminalId = table.Column<Guid>(type: "uuid", nullable: true),
                    IMEI = table.Column<string>(type: "text", nullable: false),
                    CarId = table.Column<Guid>(type: "uuid", nullable: true),
                    ElectronFenceId = table.Column<Guid>(type: "uuid", nullable: true),
                    ElectronicFenceId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsReturn = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreateUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreateUserName = table.Column<string>(type: "text", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifyUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifyUserName = table.Column<string>(type: "text", nullable: true),
                    EnableMark = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteMark = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlarmRecord_Car_CarId",
                        column: x => x.CarId,
                        principalTable: "Car",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlarmRecord_ElectronicFence_ElectronicFenceId",
                        column: x => x.ElectronicFenceId,
                        principalTable: "ElectronicFence",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlarmRecord_Terminal_TerminalId",
                        column: x => x.TerminalId,
                        principalTable: "Terminal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlarmRecord_CarId",
                table: "AlarmRecord",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmRecord_ElectronicFenceId",
                table: "AlarmRecord",
                column: "ElectronicFenceId");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmRecord_TerminalId",
                table: "AlarmRecord",
                column: "TerminalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlarmRecord");
        }
    }
}

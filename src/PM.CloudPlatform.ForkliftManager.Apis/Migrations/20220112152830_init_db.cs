using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PM.CloudPlatform.ForkliftManager.Apis.Migrations
{
    public partial class init_db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreateUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateUserName = table.Column<string>(type: "text", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifyUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifyUserName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GpsPositionRecord",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Lon = table.Column<decimal>(type: "numeric", nullable: false),
                    Lat = table.Column<decimal>(type: "numeric", nullable: false),
                    Speed = table.Column<byte>(type: "smallint", nullable: false),
                    MCC = table.Column<int>(type: "integer", nullable: false),
                    MNC = table.Column<byte>(type: "smallint", nullable: false),
                    LAC = table.Column<int>(type: "integer", nullable: false),
                    CellId = table.Column<int>(type: "integer", nullable: false),
                    AccState = table.Column<byte>(type: "smallint", nullable: false),
                    DataReportingMode = table.Column<byte>(type: "smallint", nullable: false),
                    GpsRealTimeHeadIn = table.Column<int>(type: "integer", nullable: false),
                    Mileage = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreateUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateUserName = table.Column<string>(type: "text", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifyUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifyUserName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GpsPositionRecord", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RentalCompany",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Contact = table.Column<string>(type: "text", nullable: true),
                    Tel = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreateUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateUserName = table.Column<string>(type: "text", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifyUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifyUserName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalCompany", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreateUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateUserName = table.Column<string>(type: "text", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifyUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifyUserName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ElectronicFence",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RentalCompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    LngLats = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreateUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateUserName = table.Column<string>(type: "text", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifyUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifyUserName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectronicFence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElectronicFence_RentalCompany_RentalCompanyId",
                        column: x => x.RentalCompanyId,
                        principalTable: "RentalCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RentalRecord",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RentalCompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    RentalEmployeeName = table.Column<string>(type: "text", nullable: true),
                    RentalEmployeeTel = table.Column<string>(type: "text", nullable: true),
                    RentalStartTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    RentalEndTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsReturn = table.Column<bool>(type: "boolean", nullable: false),
                    ReturnTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ReturnAckEmployeeName = table.Column<string>(type: "text", nullable: true),
                    ReturnAckEmployeeTel = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreateUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateUserName = table.Column<string>(type: "text", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifyUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifyUserName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentalRecord_RentalCompany_RentalCompanyId",
                        column: x => x.RentalCompanyId,
                        principalTable: "RentalCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Module",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Url = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreateUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateUserName = table.Column<string>(type: "text", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifyUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifyUserName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Module", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Module_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Tel = table.Column<string>(type: "text", nullable: true),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreateUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateUserName = table.Column<string>(type: "text", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifyUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifyUserName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Car",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LicensePlateNumber = table.Column<string>(type: "text", nullable: true),
                    Brand = table.Column<string>(type: "text", nullable: true),
                    SerialNumber = table.Column<string>(type: "text", nullable: true),
                    CarModel = table.Column<string>(type: "text", nullable: true),
                    CarTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    BuyTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LengthOfUse = table.Column<int>(type: "integer", nullable: false),
                    NumberOfMaintenance = table.Column<int>(type: "integer", nullable: false),
                    LastMaintenanceOfTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastLengthOfUse = table.Column<int>(type: "integer", nullable: false),
                    ElectronicFenceId = table.Column<Guid>(type: "uuid", nullable: true),
                    RentalCompanyId = table.Column<Guid>(type: "uuid", nullable: true),
                    RentalRecordId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreateUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateUserName = table.Column<string>(type: "text", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifyUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifyUserName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Car", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Car_CarType_CarTypeId",
                        column: x => x.CarTypeId,
                        principalTable: "CarType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Car_ElectronicFence_ElectronicFenceId",
                        column: x => x.ElectronicFenceId,
                        principalTable: "ElectronicFence",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Car_RentalCompany_RentalCompanyId",
                        column: x => x.RentalCompanyId,
                        principalTable: "RentalCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Car_RentalRecord_RentalRecordId",
                        column: x => x.RentalRecordId,
                        principalTable: "RentalRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Terminal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IMEI = table.Column<string>(type: "text", nullable: true),
                    CarId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreateUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateUserName = table.Column<string>(type: "text", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifyUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifyUserName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terminal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Terminal_Car_CarId",
                        column: x => x.CarId,
                        principalTable: "Car",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UseRecord",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CarId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LengthOfTime = table.Column<int>(type: "integer", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreateUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateUserName = table.Column<string>(type: "text", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifyUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifyUserName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UseRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UseRecord_Car_CarId",
                        column: x => x.CarId,
                        principalTable: "Car",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TerminalBindRecord",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TerminalId = table.Column<Guid>(type: "uuid", nullable: false),
                    CarId = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreateUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateUserName = table.Column<string>(type: "text", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifyUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifyUserName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TerminalBindRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TerminalBindRecord_Car_CarId",
                        column: x => x.CarId,
                        principalTable: "Car",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TerminalBindRecord_Terminal_TerminalId",
                        column: x => x.TerminalId,
                        principalTable: "Terminal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TerminalLoginRecord",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TerminalId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreateUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateUserName = table.Column<string>(type: "text", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifyUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifyUserName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TerminalLoginRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TerminalLoginRecord_Terminal_TerminalId",
                        column: x => x.TerminalId,
                        principalTable: "Terminal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Car_CarTypeId",
                table: "Car",
                column: "CarTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Car_ElectronicFenceId",
                table: "Car",
                column: "ElectronicFenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Car_RentalCompanyId",
                table: "Car",
                column: "RentalCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Car_RentalRecordId",
                table: "Car",
                column: "RentalRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicFence_RentalCompanyId",
                table: "ElectronicFence",
                column: "RentalCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Module_RoleId",
                table: "Module",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalRecord_RentalCompanyId",
                table: "RentalRecord",
                column: "RentalCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Terminal_CarId",
                table: "Terminal",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_TerminalBindRecord_CarId",
                table: "TerminalBindRecord",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_TerminalBindRecord_TerminalId",
                table: "TerminalBindRecord",
                column: "TerminalId");

            migrationBuilder.CreateIndex(
                name: "IX_TerminalLoginRecord_TerminalId",
                table: "TerminalLoginRecord",
                column: "TerminalId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UseRecord_CarId",
                table: "UseRecord",
                column: "CarId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GpsPositionRecord");

            migrationBuilder.DropTable(
                name: "Module");

            migrationBuilder.DropTable(
                name: "TerminalBindRecord");

            migrationBuilder.DropTable(
                name: "TerminalLoginRecord");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "UseRecord");

            migrationBuilder.DropTable(
                name: "Terminal");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Car");

            migrationBuilder.DropTable(
                name: "CarType");

            migrationBuilder.DropTable(
                name: "ElectronicFence");

            migrationBuilder.DropTable(
                name: "RentalRecord");

            migrationBuilder.DropTable(
                name: "RentalCompany");
        }
    }
}

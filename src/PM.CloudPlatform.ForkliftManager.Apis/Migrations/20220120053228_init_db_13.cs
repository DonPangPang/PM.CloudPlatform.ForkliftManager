using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PM.CloudPlatform.ForkliftManager.Apis.Migrations
{
    public partial class init_db_13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TerminalId",
                table: "UseRecord",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_UseRecord_TerminalId",
                table: "UseRecord",
                column: "TerminalId");

            migrationBuilder.AddForeignKey(
                name: "FK_UseRecord_Terminal_TerminalId",
                table: "UseRecord",
                column: "TerminalId",
                principalTable: "Terminal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UseRecord_Terminal_TerminalId",
                table: "UseRecord");

            migrationBuilder.DropIndex(
                name: "IX_UseRecord_TerminalId",
                table: "UseRecord");

            migrationBuilder.DropColumn(
                name: "TerminalId",
                table: "UseRecord");
        }
    }
}

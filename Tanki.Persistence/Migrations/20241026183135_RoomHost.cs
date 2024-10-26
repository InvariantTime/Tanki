using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tanki.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RoomHost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "HostId",
                table: "Rooms",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_HostId",
                table: "Rooms",
                column: "HostId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Users_HostId",
                table: "Rooms",
                column: "HostId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Users_HostId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_HostId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "HostId",
                table: "Rooms");
        }
    }
}

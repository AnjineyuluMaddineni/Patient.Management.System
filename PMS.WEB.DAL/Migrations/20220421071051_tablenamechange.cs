using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PMS.WEB.DAL.Migrations
{
    public partial class tablenamechange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_notifications_AspNetUsers_AppUserId",
                table: "notifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_notifications",
                table: "notifications");

            migrationBuilder.RenameTable(
                name: "notifications",
                newName: "Notifications");

            migrationBuilder.RenameIndex(
                name: "IX_notifications_AppUserId",
                table: "Notifications",
                newName: "IX_Notifications_AppUserId");

            migrationBuilder.AlterColumn<Guid>(
                name: "MId",
                table: "Medications",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("ab0f114a-14d8-4026-8093-36ba9fcb7924"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValue: new Guid("3c1692a5-950e-483f-b9de-73b7d04d0c35"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_AppUserId",
                table: "Notifications",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_AppUserId",
                table: "Notifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications");

            migrationBuilder.RenameTable(
                name: "Notifications",
                newName: "notifications");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_AppUserId",
                table: "notifications",
                newName: "IX_notifications_AppUserId");

            migrationBuilder.AlterColumn<Guid>(
                name: "MId",
                table: "Medications",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("3c1692a5-950e-483f-b9de-73b7d04d0c35"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValue: new Guid("ab0f114a-14d8-4026-8093-36ba9fcb7924"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_notifications",
                table: "notifications",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_notifications_AspNetUsers_AppUserId",
                table: "notifications",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

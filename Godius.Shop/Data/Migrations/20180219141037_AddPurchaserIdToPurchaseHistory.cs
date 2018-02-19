using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Godius.Shop.Data.Migrations
{
    public partial class AddPurchaserIdToPurchaseHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseHistory_AspNetUsers_PurchaserId",
                table: "PurchaseHistory");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseHistory_PurchaserId",
                table: "PurchaseHistory");

            migrationBuilder.AlterColumn<Guid>(
                name: "PurchaserId",
                table: "PurchaseHistory",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PurchaserId1",
                table: "PurchaseHistory",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseHistory_PurchaserId1",
                table: "PurchaseHistory",
                column: "PurchaserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseHistory_AspNetUsers_PurchaserId1",
                table: "PurchaseHistory",
                column: "PurchaserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseHistory_AspNetUsers_PurchaserId1",
                table: "PurchaseHistory");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseHistory_PurchaserId1",
                table: "PurchaseHistory");

            migrationBuilder.DropColumn(
                name: "PurchaserId1",
                table: "PurchaseHistory");

            migrationBuilder.AlterColumn<string>(
                name: "PurchaserId",
                table: "PurchaseHistory",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseHistory_PurchaserId",
                table: "PurchaseHistory",
                column: "PurchaserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseHistory_AspNetUsers_PurchaserId",
                table: "PurchaseHistory",
                column: "PurchaserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

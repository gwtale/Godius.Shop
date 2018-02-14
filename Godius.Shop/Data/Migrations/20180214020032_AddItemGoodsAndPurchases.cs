using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Godius.Shop.Data.Migrations
{
    public partial class AddItemGoodsAndPurchases : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_PurchaseHistory_ByPurchaseId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseHistory_Goods_ItemId",
                table: "PurchaseHistory");

            migrationBuilder.DropIndex(
                name: "IX_Items_ByPurchaseId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ByPurchaseId",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "PurchaseHistory",
                newName: "GoodsId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseHistory_ItemId",
                table: "PurchaseHistory",
                newName: "IX_PurchaseHistory_GoodsId");

            migrationBuilder.RenameColumn(
                name: "UpgradeOption",
                table: "Items",
                newName: "Weight");

            migrationBuilder.RenameColumn(
                name: "UpgradeDurability",
                table: "Items",
                newName: "WC");

            migrationBuilder.RenameColumn(
                name: "DefaultOption",
                table: "Items",
                newName: "HC");

            migrationBuilder.RenameColumn(
                name: "DefaultDurability",
                table: "Items",
                newName: "Durability");

            migrationBuilder.AddColumn<Guid>(
                name: "ResultItemId",
                table: "PurchaseHistory",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "AC",
                table: "Items",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DC",
                table: "Items",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Items",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ItemGoods",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    GoodsId = table.Column<Guid>(nullable: false),
                    ItemId = table.Column<Guid>(nullable: false),
                    Probability = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemGoods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemGoods_Goods_GoodsId",
                        column: x => x.GoodsId,
                        principalTable: "Goods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemGoods_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResultItemGoods",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ItemGoodsId = table.Column<Guid>(nullable: false),
                    PurchaseId = table.Column<Guid>(nullable: false),
                    UpgradeDurability = table.Column<int>(nullable: false),
                    UpgradeOption = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultItemGoods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResultItemGoods_ItemGoods_ItemGoodsId",
                        column: x => x.ItemGoodsId,
                        principalTable: "ItemGoods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResultItemGoods_PurchaseHistory_PurchaseId",
                        column: x => x.PurchaseId,
                        principalTable: "PurchaseHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemGoods_GoodsId",
                table: "ItemGoods",
                column: "GoodsId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGoods_ItemId",
                table: "ItemGoods",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultItemGoods_ItemGoodsId",
                table: "ResultItemGoods",
                column: "ItemGoodsId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultItemGoods_PurchaseId",
                table: "ResultItemGoods",
                column: "PurchaseId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseHistory_Goods_GoodsId",
                table: "PurchaseHistory",
                column: "GoodsId",
                principalTable: "Goods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseHistory_Goods_GoodsId",
                table: "PurchaseHistory");

            migrationBuilder.DropTable(
                name: "ResultItemGoods");

            migrationBuilder.DropTable(
                name: "ItemGoods");

            migrationBuilder.DropColumn(
                name: "ResultItemId",
                table: "PurchaseHistory");

            migrationBuilder.DropColumn(
                name: "AC",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "DC",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "GoodsId",
                table: "PurchaseHistory",
                newName: "ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseHistory_GoodsId",
                table: "PurchaseHistory",
                newName: "IX_PurchaseHistory_ItemId");

            migrationBuilder.RenameColumn(
                name: "Weight",
                table: "Items",
                newName: "UpgradeOption");

            migrationBuilder.RenameColumn(
                name: "WC",
                table: "Items",
                newName: "UpgradeDurability");

            migrationBuilder.RenameColumn(
                name: "HC",
                table: "Items",
                newName: "DefaultOption");

            migrationBuilder.RenameColumn(
                name: "Durability",
                table: "Items",
                newName: "DefaultDurability");

            migrationBuilder.AddColumn<Guid>(
                name: "ByPurchaseId",
                table: "Items",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_ByPurchaseId",
                table: "Items",
                column: "ByPurchaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_PurchaseHistory_ByPurchaseId",
                table: "Items",
                column: "ByPurchaseId",
                principalTable: "PurchaseHistory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseHistory_Goods_ItemId",
                table: "PurchaseHistory",
                column: "ItemId",
                principalTable: "Goods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

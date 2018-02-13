using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Godius.Shop.Data.Migrations
{
    public partial class AddCategoryId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goods_GoodsCategories_CategoryId",
                table: "Goods");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "Goods",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Goods_GoodsCategories_CategoryId",
                table: "Goods",
                column: "CategoryId",
                principalTable: "GoodsCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goods_GoodsCategories_CategoryId",
                table: "Goods");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "Goods",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_Goods_GoodsCategories_CategoryId",
                table: "Goods",
                column: "CategoryId",
                principalTable: "GoodsCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

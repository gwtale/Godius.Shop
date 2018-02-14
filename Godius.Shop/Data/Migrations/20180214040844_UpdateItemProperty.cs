using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Godius.Shop.Data.Migrations
{
    public partial class UpdateItemProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Weight",
                table: "Items",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "WC",
                table: "Items",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "HC",
                table: "Items",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "Durability",
                table: "Items",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "DC",
                table: "Items",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "AC",
                table: "Items",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Weight",
                table: "Items",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "WC",
                table: "Items",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HC",
                table: "Items",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Durability",
                table: "Items",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DC",
                table: "Items",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AC",
                table: "Items",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}

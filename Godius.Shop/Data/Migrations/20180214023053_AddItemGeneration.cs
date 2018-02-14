using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Godius.Shop.Data.Migrations
{
    public partial class AddItemGeneration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Generation",
                table: "Items",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Generation",
                table: "Items");
        }
    }
}

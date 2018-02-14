using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Godius.Shop.Data.Migrations
{
    public partial class UpdateTypeOfGeneration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Generation",
                table: "Items",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Generation",
                table: "Items",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}

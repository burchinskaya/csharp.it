﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace csharp_it.Migrations
{
    public partial class WalletMoneyColumnmWasAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "WalletMoney",
                table: "AspNetUsers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WalletMoney",
                table: "AspNetUsers");
        }
    }
}

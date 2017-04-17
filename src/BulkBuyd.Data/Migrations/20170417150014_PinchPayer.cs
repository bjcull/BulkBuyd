using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BulkBuyd.Data.Migrations
{
    public partial class PinchPayer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountBsb",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AccountName",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "AccountNumber",
                table: "AspNetUsers",
                newName: "PinchPayerId");

            migrationBuilder.AddColumn<string>(
                name: "PinchPayerId",
                table: "Pledges",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PinchPayerId",
                table: "Pledges");

            migrationBuilder.RenameColumn(
                name: "PinchPayerId",
                table: "AspNetUsers",
                newName: "AccountNumber");

            migrationBuilder.AddColumn<string>(
                name: "AccountBsb",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountName",
                table: "AspNetUsers",
                nullable: true);
        }
    }
}

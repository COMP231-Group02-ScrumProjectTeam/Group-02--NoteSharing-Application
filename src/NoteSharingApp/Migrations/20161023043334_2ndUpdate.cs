using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NoteSharingApp.Migrations
{
    public partial class _2ndUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Enrollement_year",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Semester",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Enrollement_year",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Semester",
                table: "Users");
        }
    }
}

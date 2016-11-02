using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NoteSharingApp.Migrations
{
    public partial class _3ndUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "College_org",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmailId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Occupation",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "College",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "User_name",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "College",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "User_name",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "College_org",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmailId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Occupation",
                table: "Users",
                nullable: true);
        }
    }
}

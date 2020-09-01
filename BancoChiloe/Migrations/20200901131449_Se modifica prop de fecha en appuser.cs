using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BancoChiloe.Migrations
{
    public partial class Semodificapropdefechaenappuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UltimoAcceso",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UltimoAcceso",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}

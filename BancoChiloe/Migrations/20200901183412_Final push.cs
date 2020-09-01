using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BancoChiloe.Migrations
{
    public partial class Finalpush : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaApertura",
                table: "Cuentas",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "FechaApertura",
                table: "Cuentas",
                nullable: false,
                oldClrType: typeof(DateTime));
        }
    }
}

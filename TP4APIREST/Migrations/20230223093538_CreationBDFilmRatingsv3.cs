using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TP4APIREST.Migrations
{
    public partial class CreationBDFilmRatingsv3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "utl_datecreation",
                table: "t_e_utilisateur_utl",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "utl_datecreation",
                table: "t_e_utilisateur_utl",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "now()");
        }
    }
}

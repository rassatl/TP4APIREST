using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TP4APIREST.Migrations
{
    public partial class CreationBDFilmRatingsv5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "ck_not_note",
                table: "t_j_notation_not",
                sql: "not_note between 0 and 5");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "ck_not_note",
                table: "t_j_notation_not");
        }
    }
}

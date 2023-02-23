using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TP4APIREST.Migrations
{
    public partial class CreationBDFilmRatings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "t_e_film_flm",
                columns: table => new
                {
                    flm_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    flm_titre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    flm_resume = table.Column<string>(type: "text", nullable: true),
                    flm_datesortie = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    flm_duree = table.Column<decimal>(type: "numeric", nullable: true),
                    flm_genre = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_film_flm", x => x.flm_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_utilisateur_utl",
                columns: table => new
                {
                    utl_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    utl_nom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    utl_prenom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    utl_mobile = table.Column<string>(type: "char(10)", nullable: true),
                    utl_mail = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    utl_pwd = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    utl_rue = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    utl_cp = table.Column<string>(type: "char(5)", nullable: true),
                    utl_ville = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    utl_pays = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    utl_latitude = table.Column<double>(type: "double precision", nullable: true),
                    utl_longitude = table.Column<double>(type: "double precision", nullable: true),
                    utl_datecreation = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_utilisateur_utl", x => x.utl_id);
                });

            migrationBuilder.CreateTable(
                name: "t_j_notation_not",
                columns: table => new
                {
                    utl_id = table.Column<int>(type: "integer", nullable: false),
                    flm_id = table.Column<int>(type: "integer", nullable: false),
                    not_note = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_j_notation_not", x => new { x.utl_id, x.flm_id });
                    table.ForeignKey(
                        name: "FK_t_j_notation_not_t_e_film_flm_flm_id",
                        column: x => x.flm_id,
                        principalTable: "t_e_film_flm",
                        principalColumn: "flm_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_j_notation_not_t_e_utilisateur_utl_utl_id",
                        column: x => x.utl_id,
                        principalTable: "t_e_utilisateur_utl",
                        principalColumn: "utl_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_j_notation_not_flm_id",
                table: "t_j_notation_not",
                column: "flm_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_j_notation_not");

            migrationBuilder.DropTable(
                name: "t_e_film_flm");

            migrationBuilder.DropTable(
                name: "t_e_utilisateur_utl");
        }
    }
}

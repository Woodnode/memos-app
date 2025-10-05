using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MemosWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AjoutIndexUniqueCompteNomUtilisateur : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Compte",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NomUtilisateur = table.Column<string>(type: "TEXT", nullable: false),
                    MotDePasse = table.Column<string>(type: "TEXT", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    DateDerniereConnexion = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compte", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Memo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Titre = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    IdCompte = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Memo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Memo_Compte_IdCompte",
                        column: x => x.IdCompte,
                        principalTable: "Compte",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Compte_NomUtilisateur",
                table: "Compte",
                column: "NomUtilisateur",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Memo_IdCompte",
                table: "Memo",
                column: "IdCompte");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Memo");

            migrationBuilder.DropTable(
                name: "Compte");
        }
    }
}

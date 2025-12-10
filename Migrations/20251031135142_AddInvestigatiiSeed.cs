using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimPim.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddInvestigatiiSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComenziInvestigatii",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PatientId = table.Column<int>(type: "INTEGER", nullable: false),
                    InvestigatieId = table.Column<int>(type: "INTEGER", nullable: false),
                    CodInvestigatie = table.Column<string>(type: "TEXT", nullable: false),
                    DenumireInvestigatie = table.Column<string>(type: "TEXT", nullable: false),
                    DataComanda = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComenziInvestigatii", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Investigatii",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Cod = table.Column<string>(type: "TEXT", nullable: false),
                    Denumire = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investigatii", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RezultateInvestigatii",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ComandaId = table.Column<int>(type: "INTEGER", nullable: false),
                    CodParametru = table.Column<string>(type: "TEXT", nullable: false),
                    DenumireParametru = table.Column<string>(type: "TEXT", nullable: false),
                    Valoare = table.Column<string>(type: "TEXT", nullable: false),
                    Unitate = table.Column<string>(type: "TEXT", nullable: false),
                    Interpretare = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RezultateInvestigatii", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RezultateInvestigatii_ComenziInvestigatii_ComandaId",
                        column: x => x.ComandaId,
                        principalTable: "ComenziInvestigatii",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParametriInvestigatii",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InvestigatieId = table.Column<int>(type: "INTEGER", nullable: false),
                    CodParametru = table.Column<string>(type: "TEXT", nullable: false),
                    Denumire = table.Column<string>(type: "TEXT", nullable: false),
                    Unitate = table.Column<string>(type: "TEXT", nullable: false),
                    ValoareMin = table.Column<double>(type: "REAL", nullable: true),
                    ValoareMax = table.Column<double>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParametriInvestigatii", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParametriInvestigatii_Investigatii_InvestigatieId",
                        column: x => x.InvestigatieId,
                        principalTable: "Investigatii",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParametriInvestigatii_InvestigatieId",
                table: "ParametriInvestigatii",
                column: "InvestigatieId");

            migrationBuilder.CreateIndex(
                name: "IX_RezultateInvestigatii_ComandaId",
                table: "RezultateInvestigatii",
                column: "ComandaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParametriInvestigatii");

            migrationBuilder.DropTable(
                name: "RezultateInvestigatii");

            migrationBuilder.DropTable(
                name: "Investigatii");

            migrationBuilder.DropTable(
                name: "ComenziInvestigatii");
        }
    }
}

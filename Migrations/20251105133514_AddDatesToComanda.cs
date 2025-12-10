using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimPim.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddDatesToComanda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataInvestigatie",
                table: "ComenziInvestigatii",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataRezultate",
                table: "ComenziInvestigatii",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataInvestigatie",
                table: "ComenziInvestigatii");

            migrationBuilder.DropColumn(
                name: "DataRezultate",
                table: "ComenziInvestigatii");
        }
    }
}

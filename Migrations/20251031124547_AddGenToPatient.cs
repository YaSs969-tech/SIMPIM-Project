using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimPim.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddGenToPatient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gen",
                table: "Patients",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gen",
                table: "Patients");
        }
    }
}

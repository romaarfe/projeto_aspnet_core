using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebRPGCreation.Migrations
{
    /// <inheritdoc />
    public partial class models2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "Equipamento",
                newName: "Quantidade");

            migrationBuilder.RenameColumn(
                name: "Dano",
                table: "Equipamento",
                newName: "Ataque");

            migrationBuilder.AddColumn<string>(
                name: "Moeda",
                table: "Personagem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataFim",
                table: "Grupo",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataInicio",
                table: "Grupo",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Moeda",
                table: "Personagem");

            migrationBuilder.DropColumn(
                name: "DataFim",
                table: "Grupo");

            migrationBuilder.DropColumn(
                name: "DataInicio",
                table: "Grupo");

            migrationBuilder.RenameColumn(
                name: "Quantidade",
                table: "Equipamento",
                newName: "Descricao");

            migrationBuilder.RenameColumn(
                name: "Ataque",
                table: "Equipamento",
                newName: "Dano");
        }
    }
}

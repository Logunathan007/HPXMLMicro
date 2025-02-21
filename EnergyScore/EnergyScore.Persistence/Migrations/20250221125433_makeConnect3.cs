using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnergyScore.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class makeConnect3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Walls_Insulations_InsulationsId",
                table: "Walls");

            migrationBuilder.DropIndex(
                name: "IX_Windows_WallId",
                table: "Windows");

            migrationBuilder.DropIndex(
                name: "IX_Walls_InsulationsId",
                table: "Walls");

            migrationBuilder.DropColumn(
                name: "InsulationsId",
                table: "Walls");

            migrationBuilder.AddColumn<Guid>(
                name: "WallId",
                table: "Insulations",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Windows_WallId",
                table: "Windows",
                column: "WallId");

            migrationBuilder.CreateIndex(
                name: "IX_Insulations_WallId",
                table: "Insulations",
                column: "WallId");

            migrationBuilder.AddForeignKey(
                name: "FK_Insulations_Walls_WallId",
                table: "Insulations",
                column: "WallId",
                principalTable: "Walls",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Insulations_Walls_WallId",
                table: "Insulations");

            migrationBuilder.DropIndex(
                name: "IX_Windows_WallId",
                table: "Windows");

            migrationBuilder.DropIndex(
                name: "IX_Insulations_WallId",
                table: "Insulations");

            migrationBuilder.DropColumn(
                name: "WallId",
                table: "Insulations");

            migrationBuilder.AddColumn<Guid>(
                name: "InsulationsId",
                table: "Walls",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Windows_WallId",
                table: "Windows",
                column: "WallId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Walls_InsulationsId",
                table: "Walls",
                column: "InsulationsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Walls_Insulations_InsulationsId",
                table: "Walls",
                column: "InsulationsId",
                principalTable: "Insulations",
                principalColumn: "Id");
        }
    }
}

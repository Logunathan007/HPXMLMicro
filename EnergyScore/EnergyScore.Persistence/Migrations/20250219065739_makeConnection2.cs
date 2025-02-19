using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnergyScore.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class makeConnection2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skylights_Roofs_RoofId",
                table: "Skylights");

            migrationBuilder.AlterColumn<Guid>(
                name: "RoofId",
                table: "Skylights",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Skylights_Roofs_RoofId",
                table: "Skylights",
                column: "RoofId",
                principalTable: "Roofs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skylights_Roofs_RoofId",
                table: "Skylights");

            migrationBuilder.AlterColumn<Guid>(
                name: "RoofId",
                table: "Skylights",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Skylights_Roofs_RoofId",
                table: "Skylights",
                column: "RoofId",
                principalTable: "Roofs",
                principalColumn: "Id");
        }
    }
}

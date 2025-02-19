using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnergyScore.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class makeConnection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Skylights",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Area = table.Column<double>(type: "double precision", nullable: false),
                    SHGC = table.Column<double>(type: "double precision", nullable: false),
                    UFactor = table.Column<double>(type: "double precision", nullable: false),
                    RoofId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skylights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skylights_Roofs_RoofId",
                        column: x => x.RoofId,
                        principalTable: "Roofs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Skylights_RoofId",
                table: "Skylights",
                column: "RoofId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Skylights");
        }
    }
}

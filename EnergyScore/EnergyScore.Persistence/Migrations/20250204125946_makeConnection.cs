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
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Address",
                newName: "AddressId");

            migrationBuilder.CreateTable(
                name: "About",
                columns: table => new
                {
                    AboutId = table.Column<Guid>(type: "uuid", nullable: false),
                    residentialFacilityType = table.Column<string>(type: "text", nullable: false),
                    yearBuilt = table.Column<int>(type: "integer", nullable: false),
                    numberofBedrooms = table.Column<int>(type: "integer", nullable: false),
                    numberofConditionedFloorsAboveGrade = table.Column<int>(type: "integer", nullable: false),
                    averageCeilingHeight = table.Column<int>(type: "integer", nullable: false),
                    conditionedBuildingVolume = table.Column<int>(type: "integer", nullable: false),
                    conditionedFloorArea = table.Column<int>(type: "integer", nullable: false),
                    azimuthOfFrontOfHome = table.Column<int>(type: "integer", nullable: false),
                    orientationOfFrontOfHome = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_About", x => x.AboutId);
                });

            migrationBuilder.CreateTable(
                name: "AirInfiltrationMeasurement",
                columns: table => new
                {
                    AirInfiltrationMeasurementId = table.Column<Guid>(type: "uuid", nullable: false),
                    HousePressure = table.Column<int>(type: "integer", nullable: false),
                    UnitofMeasure = table.Column<string>(type: "text", nullable: false),
                    AirLeakage = table.Column<int>(type: "integer", nullable: false),
                    LeakinessDescription = table.Column<string>(type: "text", nullable: false),
                    AboutId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirInfiltrationMeasurement", x => x.AirInfiltrationMeasurementId);
                    table.ForeignKey(
                        name: "FK_AirInfiltrationMeasurement_About_AboutId",
                        column: x => x.AboutId,
                        principalTable: "About",
                        principalColumn: "AboutId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AirInfiltrationMeasurement_AboutId",
                table: "AirInfiltrationMeasurement",
                column: "AboutId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AirInfiltrationMeasurement");

            migrationBuilder.DropTable(
                name: "About");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "Address",
                newName: "Id");
        }
    }
}

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
                name: "Abouts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ResidentialFacilityType = table.Column<string>(type: "text", nullable: false),
                    YearBuilt = table.Column<int>(type: "integer", nullable: false),
                    NumberofBedrooms = table.Column<int>(type: "integer", nullable: false),
                    NumberofConditionedFloorsAboveGrade = table.Column<double>(type: "double precision", nullable: false),
                    AverageCeilingHeight = table.Column<double>(type: "double precision", nullable: false),
                    ConditionedBuildingVolume = table.Column<double>(type: "double precision", nullable: false),
                    ManufacturedHomeSections = table.Column<string>(type: "text", nullable: false),
                    ConditionedFloorArea = table.Column<double>(type: "double precision", nullable: false),
                    AzimuthOfFrontOfHome = table.Column<int>(type: "integer", nullable: false),
                    OrientationOfFrontOfHome = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abouts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Addresss",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Address1 = table.Column<string>(type: "text", nullable: false),
                    Address2 = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    State = table.Column<string>(type: "text", nullable: false),
                    Zipcode = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresss", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ZoneFloors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZoneFloors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AirInfiltrationMeasurements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    HousePressure = table.Column<double>(type: "double precision", nullable: false),
                    UnitofMeasure = table.Column<string>(type: "text", nullable: false),
                    AirLeakage = table.Column<double>(type: "double precision", nullable: false),
                    LeakinessDescription = table.Column<string>(type: "text", nullable: false),
                    AboutId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirInfiltrationMeasurements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AirInfiltrationMeasurements_Abouts_AboutId",
                        column: x => x.AboutId,
                        principalTable: "Abouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Buildings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AboutId = table.Column<Guid>(type: "uuid", nullable: true),
                    AddressId = table.Column<Guid>(type: "uuid", nullable: false),
                    ZoneFloorId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buildings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Buildings_Abouts_AboutId",
                        column: x => x.AboutId,
                        principalTable: "Abouts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Buildings_Addresss_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresss",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Buildings_ZoneFloors_ZoneFloorId",
                        column: x => x.ZoneFloorId,
                        principalTable: "ZoneFloors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Foundations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FoundationName = table.Column<string>(type: "text", nullable: false),
                    FoundationType = table.Column<string>(type: "text", nullable: false),
                    ZoneFloorId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foundations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Foundations_ZoneFloors_ZoneFloorId",
                        column: x => x.ZoneFloorId,
                        principalTable: "ZoneFloors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FoundationTypeDynamicOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Finished = table.Column<bool>(type: "boolean", nullable: true),
                    Conditioned = table.Column<bool>(type: "boolean", nullable: true),
                    Vented = table.Column<bool>(type: "boolean", nullable: true),
                    FoundationId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoundationTypeDynamicOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoundationTypeDynamicOptions_Foundations_FoundationId",
                        column: x => x.FoundationId,
                        principalTable: "Foundations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoundationWalls",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FoundationWallName = table.Column<string>(type: "text", nullable: false),
                    Area = table.Column<double>(type: "double precision", nullable: false),
                    FoundationId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoundationWalls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoundationWalls_Foundations_FoundationId",
                        column: x => x.FoundationId,
                        principalTable: "Foundations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FrameFloors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FrameFloorName = table.Column<string>(type: "text", nullable: false),
                    Area = table.Column<double>(type: "double precision", nullable: false),
                    FoundationId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrameFloors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FrameFloors_Foundations_FoundationId",
                        column: x => x.FoundationId,
                        principalTable: "Foundations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Slabs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SlabName = table.Column<string>(type: "text", nullable: false),
                    ExposedPerimeter = table.Column<double>(type: "double precision", nullable: false),
                    FoundationId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slabs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Slabs_Foundations_FoundationId",
                        column: x => x.FoundationId,
                        principalTable: "Foundations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Insulations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NominalRValue = table.Column<double>(type: "double precision", nullable: false),
                    AssemblyEffectiveRValue = table.Column<double>(type: "double precision", nullable: false),
                    FrameFloorId = table.Column<Guid>(type: "uuid", nullable: true),
                    FoundationWallId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insulations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Insulations_FoundationWalls_FoundationWallId",
                        column: x => x.FoundationWallId,
                        principalTable: "FoundationWalls",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Insulations_FrameFloors_FrameFloorId",
                        column: x => x.FrameFloorId,
                        principalTable: "FrameFloors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PerimeterInsulations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NominalRValue = table.Column<double>(type: "double precision", nullable: false),
                    AssemblyEffectiveRValue = table.Column<double>(type: "double precision", nullable: false),
                    SlabId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerimeterInsulations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerimeterInsulations_Slabs_SlabId",
                        column: x => x.SlabId,
                        principalTable: "Slabs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AirInfiltrationMeasurements_AboutId",
                table: "AirInfiltrationMeasurements",
                column: "AboutId");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_AboutId",
                table: "Buildings",
                column: "AboutId");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_AddressId",
                table: "Buildings",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_ZoneFloorId",
                table: "Buildings",
                column: "ZoneFloorId");

            migrationBuilder.CreateIndex(
                name: "IX_Foundations_ZoneFloorId",
                table: "Foundations",
                column: "ZoneFloorId");

            migrationBuilder.CreateIndex(
                name: "IX_FoundationTypeDynamicOptions_FoundationId",
                table: "FoundationTypeDynamicOptions",
                column: "FoundationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FoundationWalls_FoundationId",
                table: "FoundationWalls",
                column: "FoundationId");

            migrationBuilder.CreateIndex(
                name: "IX_FrameFloors_FoundationId",
                table: "FrameFloors",
                column: "FoundationId");

            migrationBuilder.CreateIndex(
                name: "IX_Insulations_FoundationWallId",
                table: "Insulations",
                column: "FoundationWallId");

            migrationBuilder.CreateIndex(
                name: "IX_Insulations_FrameFloorId",
                table: "Insulations",
                column: "FrameFloorId");

            migrationBuilder.CreateIndex(
                name: "IX_PerimeterInsulations_SlabId",
                table: "PerimeterInsulations",
                column: "SlabId");

            migrationBuilder.CreateIndex(
                name: "IX_Slabs_FoundationId",
                table: "Slabs",
                column: "FoundationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AirInfiltrationMeasurements");

            migrationBuilder.DropTable(
                name: "Buildings");

            migrationBuilder.DropTable(
                name: "FoundationTypeDynamicOptions");

            migrationBuilder.DropTable(
                name: "Insulations");

            migrationBuilder.DropTable(
                name: "PerimeterInsulations");

            migrationBuilder.DropTable(
                name: "Abouts");

            migrationBuilder.DropTable(
                name: "Addresss");

            migrationBuilder.DropTable(
                name: "FoundationWalls");

            migrationBuilder.DropTable(
                name: "FrameFloors");

            migrationBuilder.DropTable(
                name: "Slabs");

            migrationBuilder.DropTable(
                name: "Foundations");

            migrationBuilder.DropTable(
                name: "ZoneFloors");
        }
    }
}

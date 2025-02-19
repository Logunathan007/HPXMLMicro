using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnergyScore.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
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
                    ManufacturedHomeSections = table.Column<string>(type: "text", nullable: true),
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
                name: "ZoneRoofs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZoneRoofs", x => x.Id);
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
                    ZoneFloorId = table.Column<Guid>(type: "uuid", nullable: true),
                    ZoneRoofId = table.Column<Guid>(type: "uuid", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_Buildings_ZoneRoofs_ZoneRoofId",
                        column: x => x.ZoneRoofId,
                        principalTable: "ZoneRoofs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Attics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AtticName = table.Column<string>(type: "text", nullable: false),
                    AtticType = table.Column<string>(type: "text", nullable: false),
                    ZoneRoofId = table.Column<Guid>(type: "uuid", nullable: false),
                    BuildingId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attics_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attics_ZoneRoofs_ZoneRoofId",
                        column: x => x.ZoneRoofId,
                        principalTable: "ZoneRoofs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Foundations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FoundationName = table.Column<string>(type: "text", nullable: false),
                    FoundationType = table.Column<string>(type: "text", nullable: false),
                    ZoneFloorId = table.Column<Guid>(type: "uuid", nullable: false),
                    BuildingId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foundations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Foundations_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Foundations_ZoneFloors_ZoneFloorId",
                        column: x => x.ZoneFloorId,
                        principalTable: "ZoneFloors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AtticTypeDynamicOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Vented = table.Column<bool>(type: "boolean", nullable: true),
                    Conditioned = table.Column<bool>(type: "boolean", nullable: true),
                    CapeCod = table.Column<bool>(type: "boolean", nullable: true),
                    AtticId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtticTypeDynamicOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AtticTypeDynamicOptions_Attics_AtticId",
                        column: x => x.AtticId,
                        principalTable: "Attics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Roofs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoofName = table.Column<string>(type: "text", nullable: false),
                    RoofType = table.Column<string>(type: "text", nullable: false),
                    RoofColor = table.Column<string>(type: "text", nullable: false),
                    Area = table.Column<double>(type: "double precision", nullable: false),
                    RadiantBarrier = table.Column<bool>(type: "boolean", nullable: false),
                    SolarAbsorptance = table.Column<double>(type: "double precision", nullable: false),
                    AtticId = table.Column<Guid>(type: "uuid", nullable: false),
                    BuildingId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roofs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roofs_Attics_AtticId",
                        column: x => x.AtticId,
                        principalTable: "Attics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Roofs_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Walls",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WallName = table.Column<string>(type: "text", nullable: false),
                    AtticWallType = table.Column<string>(type: "text", nullable: false),
                    Area = table.Column<double>(type: "double precision", nullable: false),
                    AtticId = table.Column<Guid>(type: "uuid", nullable: false),
                    BuildingId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Walls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Walls_Attics_AtticId",
                        column: x => x.AtticId,
                        principalTable: "Attics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Walls_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    FoundationId = table.Column<Guid>(type: "uuid", nullable: false),
                    BuildingId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoundationWalls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoundationWalls_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    FoundationId = table.Column<Guid>(type: "uuid", nullable: true),
                    AtticId = table.Column<Guid>(type: "uuid", nullable: true),
                    BuildingId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrameFloors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FrameFloors_Attics_AtticId",
                        column: x => x.AtticId,
                        principalTable: "Attics",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FrameFloors_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FrameFloors_Foundations_FoundationId",
                        column: x => x.FoundationId,
                        principalTable: "Foundations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Slabs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SlabName = table.Column<string>(type: "text", nullable: false),
                    ExposedPerimeter = table.Column<double>(type: "double precision", nullable: false),
                    FoundationId = table.Column<Guid>(type: "uuid", nullable: true),
                    BuildingId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slabs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Slabs_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    FoundationWallId = table.Column<Guid>(type: "uuid", nullable: true),
                    FrameFloorId = table.Column<Guid>(type: "uuid", nullable: true),
                    RoofId = table.Column<Guid>(type: "uuid", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_Insulations_Roofs_RoofId",
                        column: x => x.RoofId,
                        principalTable: "Roofs",
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
                name: "IX_Attics_BuildingId",
                table: "Attics",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_Attics_ZoneRoofId",
                table: "Attics",
                column: "ZoneRoofId");

            migrationBuilder.CreateIndex(
                name: "IX_AtticTypeDynamicOptions_AtticId",
                table: "AtticTypeDynamicOptions",
                column: "AtticId",
                unique: true);

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
                name: "IX_Buildings_ZoneRoofId",
                table: "Buildings",
                column: "ZoneRoofId");

            migrationBuilder.CreateIndex(
                name: "IX_Foundations_BuildingId",
                table: "Foundations",
                column: "BuildingId");

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
                name: "IX_FoundationWalls_BuildingId",
                table: "FoundationWalls",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_FoundationWalls_FoundationId",
                table: "FoundationWalls",
                column: "FoundationId");

            migrationBuilder.CreateIndex(
                name: "IX_FrameFloors_AtticId",
                table: "FrameFloors",
                column: "AtticId");

            migrationBuilder.CreateIndex(
                name: "IX_FrameFloors_BuildingId",
                table: "FrameFloors",
                column: "BuildingId");

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
                name: "IX_Insulations_RoofId",
                table: "Insulations",
                column: "RoofId");

            migrationBuilder.CreateIndex(
                name: "IX_PerimeterInsulations_SlabId",
                table: "PerimeterInsulations",
                column: "SlabId");

            migrationBuilder.CreateIndex(
                name: "IX_Roofs_AtticId",
                table: "Roofs",
                column: "AtticId");

            migrationBuilder.CreateIndex(
                name: "IX_Roofs_BuildingId",
                table: "Roofs",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_Slabs_BuildingId",
                table: "Slabs",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_Slabs_FoundationId",
                table: "Slabs",
                column: "FoundationId");

            migrationBuilder.CreateIndex(
                name: "IX_Walls_AtticId",
                table: "Walls",
                column: "AtticId");

            migrationBuilder.CreateIndex(
                name: "IX_Walls_BuildingId",
                table: "Walls",
                column: "BuildingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AirInfiltrationMeasurements");

            migrationBuilder.DropTable(
                name: "AtticTypeDynamicOptions");

            migrationBuilder.DropTable(
                name: "FoundationTypeDynamicOptions");

            migrationBuilder.DropTable(
                name: "Insulations");

            migrationBuilder.DropTable(
                name: "PerimeterInsulations");

            migrationBuilder.DropTable(
                name: "Walls");

            migrationBuilder.DropTable(
                name: "FoundationWalls");

            migrationBuilder.DropTable(
                name: "FrameFloors");

            migrationBuilder.DropTable(
                name: "Roofs");

            migrationBuilder.DropTable(
                name: "Slabs");

            migrationBuilder.DropTable(
                name: "Attics");

            migrationBuilder.DropTable(
                name: "Foundations");

            migrationBuilder.DropTable(
                name: "Buildings");

            migrationBuilder.DropTable(
                name: "Abouts");

            migrationBuilder.DropTable(
                name: "Addresss");

            migrationBuilder.DropTable(
                name: "ZoneFloors");

            migrationBuilder.DropTable(
                name: "ZoneRoofs");
        }
    }
}

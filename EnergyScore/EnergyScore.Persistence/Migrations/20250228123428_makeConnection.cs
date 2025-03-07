﻿using System;
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
                    AverageCeilingHeight = table.Column<double>(type: "double precision", nullable: true),
                    ConditionedBuildingVolume = table.Column<double>(type: "double precision", nullable: true),
                    ManufacturedHomeSections = table.Column<string>(type: "text", nullable: true),
                    ConditionedFloorArea = table.Column<double>(type: "double precision", nullable: true),
                    AzimuthOfFrontOfHome = table.Column<int>(type: "integer", nullable: true),
                    OrientationOfFrontOfHome = table.Column<string>(type: "text", nullable: true),
                    Comments = table.Column<string>(type: "text", nullable: true)
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
                name: "DistributionSystems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistributionSystems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HVACPlants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HVACPlants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Photovoltaics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photovoltaics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WaterHeatings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaterHeatings", x => x.Id);
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
                name: "ZoneWalls",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZoneWalls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AirInfiltrationMeasurements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    HousePressure = table.Column<double>(type: "double precision", nullable: true),
                    UnitofMeasure = table.Column<string>(type: "text", nullable: true),
                    AirLeakage = table.Column<double>(type: "double precision", nullable: true),
                    LeakinessDescription = table.Column<string>(type: "text", nullable: true),
                    AirSealing = table.Column<bool>(type: "boolean", nullable: true),
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
                    ZoneRoofId = table.Column<Guid>(type: "uuid", nullable: true),
                    ZoneWallId = table.Column<Guid>(type: "uuid", nullable: true),
                    DistributionSystemsId = table.Column<Guid>(type: "uuid", nullable: true),
                    HVACPlantId = table.Column<Guid>(type: "uuid", nullable: true),
                    WaterHeatingId = table.Column<Guid>(type: "uuid", nullable: true),
                    PhotovoltaicsId = table.Column<Guid>(type: "uuid", nullable: true)
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
                        name: "FK_Buildings_DistributionSystems_DistributionSystemsId",
                        column: x => x.DistributionSystemsId,
                        principalTable: "DistributionSystems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Buildings_HVACPlants_HVACPlantId",
                        column: x => x.HVACPlantId,
                        principalTable: "HVACPlants",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Buildings_Photovoltaics_PhotovoltaicsId",
                        column: x => x.PhotovoltaicsId,
                        principalTable: "Photovoltaics",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Buildings_WaterHeatings_WaterHeatingId",
                        column: x => x.WaterHeatingId,
                        principalTable: "WaterHeatings",
                        principalColumn: "Id");
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
                    table.ForeignKey(
                        name: "FK_Buildings_ZoneWalls_ZoneWallId",
                        column: x => x.ZoneWallId,
                        principalTable: "ZoneWalls",
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
                name: "DistributionSystem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DistributionSystemName = table.Column<string>(type: "text", nullable: false),
                    LeakinessObservedVisualInspection = table.Column<string>(type: "text", nullable: false),
                    DuctSystemSealed = table.Column<bool>(type: "boolean", nullable: false),
                    DuctType = table.Column<string>(type: "text", nullable: true),
                    Units = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<double>(type: "double precision", nullable: false),
                    TotalOrToOutside = table.Column<string>(type: "text", nullable: false),
                    DistributionSystemsId = table.Column<Guid>(type: "uuid", nullable: false),
                    BuildingId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistributionSystem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DistributionSystem_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DistributionSystem_DistributionSystems_DistributionSystemsId",
                        column: x => x.DistributionSystemsId,
                        principalTable: "DistributionSystems",
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
                name: "HeatPumps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    HeatPumpName = table.Column<string>(type: "text", nullable: false),
                    FractionHeatLoadServed = table.Column<double>(type: "double precision", nullable: false),
                    HeatingCapacity17F = table.Column<double>(type: "double precision", nullable: false),
                    HeatingCapacity = table.Column<double>(type: "double precision", nullable: false),
                    FractionCoolLoadServed = table.Column<double>(type: "double precision", nullable: false),
                    CoolingCapacity = table.Column<double>(type: "double precision", nullable: false),
                    HeatPumpType = table.Column<string>(type: "text", nullable: false),
                    AnnualHeatingEfficiencyUnits = table.Column<string>(type: "text", nullable: false),
                    AnnualHeatingEfficiencyValue = table.Column<double>(type: "double precision", nullable: false),
                    AnnualCoolingEfficiencyUnits = table.Column<string>(type: "text", nullable: false),
                    AnnualCoolingEfficiencyValue = table.Column<double>(type: "double precision", nullable: false),
                    ModelYear = table.Column<int>(type: "integer", nullable: false),
                    YearInstalled = table.Column<int>(type: "integer", nullable: false),
                    FloorAreaServed = table.Column<double>(type: "double precision", nullable: false),
                    BuildingId = table.Column<Guid>(type: "uuid", nullable: false),
                    HVACPlantId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeatPumps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HeatPumps_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HeatPumps_HVACPlants_HVACPlantId",
                        column: x => x.HVACPlantId,
                        principalTable: "HVACPlants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PVSystems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PVSystemName = table.Column<string>(type: "text", nullable: false),
                    MaxPowerOutput = table.Column<double>(type: "double precision", nullable: false),
                    CollectorArea = table.Column<double>(type: "double precision", nullable: false),
                    NumberOfPanels = table.Column<int>(type: "integer", nullable: false),
                    YearInverterManufactured = table.Column<int>(type: "integer", nullable: false),
                    YearModulesManufactured = table.Column<int>(type: "integer", nullable: false),
                    ArrayAzimuth = table.Column<double>(type: "double precision", nullable: false),
                    ArrayOrientation = table.Column<string>(type: "text", nullable: false),
                    ArrayTilt = table.Column<double>(type: "double precision", nullable: false),
                    BuildingId = table.Column<Guid>(type: "uuid", nullable: false),
                    PhotovoltaicsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PVSystems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PVSystems_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PVSystems_Photovoltaics_PhotovoltaicsId",
                        column: x => x.PhotovoltaicsId,
                        principalTable: "Photovoltaics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WaterHeatingSystems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    HeatingSystemName = table.Column<string>(type: "text", nullable: false),
                    FractionDHWLoadServed = table.Column<double>(type: "double precision", nullable: false),
                    WaterHeaterType = table.Column<string>(type: "text", nullable: false),
                    FuelType = table.Column<string>(type: "text", nullable: false),
                    EnergyFactor = table.Column<double>(type: "double precision", nullable: false),
                    UniformEnergyFactor = table.Column<double>(type: "double precision", nullable: false),
                    ModelYear = table.Column<int>(type: "integer", nullable: false),
                    YearInstalled = table.Column<int>(type: "integer", nullable: false),
                    BuildingId = table.Column<Guid>(type: "uuid", nullable: false),
                    WaterHeatingId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaterHeatingSystems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WaterHeatingSystems_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WaterHeatingSystems_WaterHeatings_WaterHeatingId",
                        column: x => x.WaterHeatingId,
                        principalTable: "WaterHeatings",
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
                    AtticWallType = table.Column<string>(type: "text", nullable: true),
                    Area = table.Column<double>(type: "double precision", nullable: false),
                    ExteriorAdjacentTo = table.Column<string>(type: "text", nullable: true),
                    WallType = table.Column<string>(type: "text", nullable: true),
                    Siding = table.Column<string>(type: "text", nullable: true),
                    Azimuth = table.Column<int>(type: "integer", nullable: true),
                    Orientation = table.Column<string>(type: "text", nullable: true),
                    AtticId = table.Column<Guid>(type: "uuid", nullable: true),
                    BuildingId = table.Column<Guid>(type: "uuid", nullable: false),
                    ZoneWallId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Walls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Walls_Attics_AtticId",
                        column: x => x.AtticId,
                        principalTable: "Attics",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Walls_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Walls_ZoneWalls_ZoneWallId",
                        column: x => x.ZoneWallId,
                        principalTable: "ZoneWalls",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CoolingSystems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CoolingSystemName = table.Column<string>(type: "text", nullable: false),
                    FractionCoolLoadServed = table.Column<double>(type: "double precision", nullable: false),
                    CoolingCapacity = table.Column<double>(type: "double precision", nullable: false),
                    CoolingSystemType = table.Column<string>(type: "text", nullable: false),
                    AnnualCoolingEfficiencyUnits = table.Column<string>(type: "text", nullable: false),
                    AnnualCoolingEfficiencyValue = table.Column<double>(type: "double precision", nullable: false),
                    ModelYear = table.Column<int>(type: "integer", nullable: false),
                    YearInstalled = table.Column<int>(type: "integer", nullable: false),
                    FloorAreaServed = table.Column<double>(type: "double precision", nullable: false),
                    DistributionSystemId = table.Column<Guid>(type: "uuid", nullable: false),
                    BuildingId = table.Column<Guid>(type: "uuid", nullable: false),
                    HVACPlantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoolingSystems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoolingSystems_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoolingSystems_DistributionSystem_DistributionSystemId",
                        column: x => x.DistributionSystemId,
                        principalTable: "DistributionSystem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoolingSystems_HVACPlants_HVACPlantId",
                        column: x => x.HVACPlantId,
                        principalTable: "HVACPlants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HeatingSystems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    HeatingSystemName = table.Column<string>(type: "text", nullable: false),
                    FractionHeatLoadServed = table.Column<double>(type: "double precision", nullable: false),
                    HeatingCapacity = table.Column<double>(type: "double precision", nullable: false),
                    HeatingSystemFuel = table.Column<string>(type: "text", nullable: false),
                    HeatingSystemType = table.Column<string>(type: "text", nullable: false),
                    AnnualHeatingEfficiencyUnits = table.Column<string>(type: "text", nullable: false),
                    AnnualHeatingEfficiencyValue = table.Column<double>(type: "double precision", nullable: false),
                    ModelYear = table.Column<int>(type: "integer", nullable: false),
                    YearInstalled = table.Column<int>(type: "integer", nullable: false),
                    FloorAreaServed = table.Column<double>(type: "double precision", nullable: false),
                    DistributionSystemId = table.Column<Guid>(type: "uuid", nullable: false),
                    BuildingId = table.Column<Guid>(type: "uuid", nullable: false),
                    HVACPlantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeatingSystems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HeatingSystems_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HeatingSystems_DistributionSystem_DistributionSystemId",
                        column: x => x.DistributionSystemId,
                        principalTable: "DistributionSystem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HeatingSystems_HVACPlants_HVACPlantId",
                        column: x => x.HVACPlantId,
                        principalTable: "HVACPlants",
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
                name: "Skylights",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Area = table.Column<double>(type: "double precision", nullable: false),
                    SHGC = table.Column<double>(type: "double precision", nullable: false),
                    UFactor = table.Column<double>(type: "double precision", nullable: false),
                    RoofId = table.Column<Guid>(type: "uuid", nullable: true),
                    BuildingId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skylights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skylights_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Skylights_Roofs_RoofId",
                        column: x => x.RoofId,
                        principalTable: "Roofs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WallsTypeDynamicOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExpandedPolystyreneSheathing = table.Column<bool>(type: "boolean", nullable: true),
                    OptimumValueEngineering = table.Column<bool>(type: "boolean", nullable: true),
                    FramingType = table.Column<string>(type: "text", nullable: true),
                    Staggered = table.Column<bool>(type: "boolean", nullable: true),
                    WallId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WallsTypeDynamicOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WallsTypeDynamicOptions_Walls_WallId",
                        column: x => x.WallId,
                        principalTable: "Walls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Windows",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Area = table.Column<double>(type: "double precision", nullable: false),
                    SHGC = table.Column<double>(type: "double precision", nullable: false),
                    UFactor = table.Column<double>(type: "double precision", nullable: false),
                    FrameType = table.Column<string>(type: "text", nullable: false),
                    GlassType = table.Column<string>(type: "text", nullable: false),
                    GlassLayers = table.Column<string>(type: "text", nullable: false),
                    GasFill = table.Column<string>(type: "text", nullable: false),
                    Orientation = table.Column<string>(type: "text", nullable: false),
                    Azimuth = table.Column<int>(type: "integer", nullable: false),
                    WallId = table.Column<Guid>(type: "uuid", nullable: false),
                    BuildingId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Windows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Windows_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Windows_Walls_WallId",
                        column: x => x.WallId,
                        principalTable: "Walls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Insulations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NominalRValue = table.Column<double>(type: "double precision", nullable: false),
                    AssemblyEffectiveRValue = table.Column<double>(type: "double precision", nullable: false),
                    InstallationType = table.Column<string>(type: "text", nullable: true),
                    InsulationMaterial = table.Column<string>(type: "text", nullable: true),
                    FoundationWallId = table.Column<Guid>(type: "uuid", nullable: true),
                    FrameFloorId = table.Column<Guid>(type: "uuid", nullable: true),
                    RoofId = table.Column<Guid>(type: "uuid", nullable: true),
                    WallId = table.Column<Guid>(type: "uuid", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_Insulations_Walls_WallId",
                        column: x => x.WallId,
                        principalTable: "Walls",
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

            migrationBuilder.CreateTable(
                name: "FrameTypeDynamicOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ThermalBreak = table.Column<bool>(type: "boolean", nullable: true),
                    WindowId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrameTypeDynamicOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FrameTypeDynamicOptions_Windows_WindowId",
                        column: x => x.WindowId,
                        principalTable: "Windows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InsulationMaterialDynamicOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Batt = table.Column<string>(type: "text", nullable: true),
                    LooseFill = table.Column<string>(type: "text", nullable: true),
                    Rigit = table.Column<string>(type: "text", nullable: true),
                    SprayFoam = table.Column<string>(type: "text", nullable: true),
                    InsulationId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsulationMaterialDynamicOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InsulationMaterialDynamicOptions_Insulations_InsulationId",
                        column: x => x.InsulationId,
                        principalTable: "Insulations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Ducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DuctInsulationRValue = table.Column<double>(type: "double precision", nullable: false),
                    DuctInsulationThickness = table.Column<double>(type: "double precision", nullable: false),
                    DuctInsulationMaterial = table.Column<string>(type: "text", nullable: false),
                    DuctInsulationMaterialDynamicOptionsId = table.Column<Guid>(type: "uuid", nullable: false),
                    DuctLocation = table.Column<string>(type: "text", nullable: false),
                    FractionDuctArea = table.Column<double>(type: "double precision", nullable: false),
                    DistributionSystemId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ducts_DistributionSystem_DistributionSystemId",
                        column: x => x.DistributionSystemId,
                        principalTable: "DistributionSystem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ducts_InsulationMaterialDynamicOptions_DuctInsulationMateri~",
                        column: x => x.DuctInsulationMaterialDynamicOptionsId,
                        principalTable: "InsulationMaterialDynamicOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_Buildings_DistributionSystemsId",
                table: "Buildings",
                column: "DistributionSystemsId");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_HVACPlantId",
                table: "Buildings",
                column: "HVACPlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_PhotovoltaicsId",
                table: "Buildings",
                column: "PhotovoltaicsId");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_WaterHeatingId",
                table: "Buildings",
                column: "WaterHeatingId");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_ZoneFloorId",
                table: "Buildings",
                column: "ZoneFloorId");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_ZoneRoofId",
                table: "Buildings",
                column: "ZoneRoofId");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_ZoneWallId",
                table: "Buildings",
                column: "ZoneWallId");

            migrationBuilder.CreateIndex(
                name: "IX_CoolingSystems_BuildingId",
                table: "CoolingSystems",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_CoolingSystems_DistributionSystemId",
                table: "CoolingSystems",
                column: "DistributionSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_CoolingSystems_HVACPlantId",
                table: "CoolingSystems",
                column: "HVACPlantId");

            migrationBuilder.CreateIndex(
                name: "IX_DistributionSystem_BuildingId",
                table: "DistributionSystem",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_DistributionSystem_DistributionSystemsId",
                table: "DistributionSystem",
                column: "DistributionSystemsId");

            migrationBuilder.CreateIndex(
                name: "IX_Ducts_DistributionSystemId",
                table: "Ducts",
                column: "DistributionSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_Ducts_DuctInsulationMaterialDynamicOptionsId",
                table: "Ducts",
                column: "DuctInsulationMaterialDynamicOptionsId");

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
                name: "IX_FrameTypeDynamicOptions_WindowId",
                table: "FrameTypeDynamicOptions",
                column: "WindowId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HeatingSystems_BuildingId",
                table: "HeatingSystems",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_HeatingSystems_DistributionSystemId",
                table: "HeatingSystems",
                column: "DistributionSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_HeatingSystems_HVACPlantId",
                table: "HeatingSystems",
                column: "HVACPlantId");

            migrationBuilder.CreateIndex(
                name: "IX_HeatPumps_BuildingId",
                table: "HeatPumps",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_HeatPumps_HVACPlantId",
                table: "HeatPumps",
                column: "HVACPlantId");

            migrationBuilder.CreateIndex(
                name: "IX_InsulationMaterialDynamicOptions_InsulationId",
                table: "InsulationMaterialDynamicOptions",
                column: "InsulationId",
                unique: true);

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
                name: "IX_Insulations_WallId",
                table: "Insulations",
                column: "WallId");

            migrationBuilder.CreateIndex(
                name: "IX_PerimeterInsulations_SlabId",
                table: "PerimeterInsulations",
                column: "SlabId");

            migrationBuilder.CreateIndex(
                name: "IX_PVSystems_BuildingId",
                table: "PVSystems",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_PVSystems_PhotovoltaicsId",
                table: "PVSystems",
                column: "PhotovoltaicsId");

            migrationBuilder.CreateIndex(
                name: "IX_Roofs_AtticId",
                table: "Roofs",
                column: "AtticId");

            migrationBuilder.CreateIndex(
                name: "IX_Roofs_BuildingId",
                table: "Roofs",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_Skylights_BuildingId",
                table: "Skylights",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_Skylights_RoofId",
                table: "Skylights",
                column: "RoofId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Walls_ZoneWallId",
                table: "Walls",
                column: "ZoneWallId");

            migrationBuilder.CreateIndex(
                name: "IX_WallsTypeDynamicOptions_WallId",
                table: "WallsTypeDynamicOptions",
                column: "WallId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WaterHeatingSystems_BuildingId",
                table: "WaterHeatingSystems",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_WaterHeatingSystems_WaterHeatingId",
                table: "WaterHeatingSystems",
                column: "WaterHeatingId");

            migrationBuilder.CreateIndex(
                name: "IX_Windows_BuildingId",
                table: "Windows",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_Windows_WallId",
                table: "Windows",
                column: "WallId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AirInfiltrationMeasurements");

            migrationBuilder.DropTable(
                name: "AtticTypeDynamicOptions");

            migrationBuilder.DropTable(
                name: "CoolingSystems");

            migrationBuilder.DropTable(
                name: "Ducts");

            migrationBuilder.DropTable(
                name: "FoundationTypeDynamicOptions");

            migrationBuilder.DropTable(
                name: "FrameTypeDynamicOptions");

            migrationBuilder.DropTable(
                name: "HeatingSystems");

            migrationBuilder.DropTable(
                name: "HeatPumps");

            migrationBuilder.DropTable(
                name: "PerimeterInsulations");

            migrationBuilder.DropTable(
                name: "PVSystems");

            migrationBuilder.DropTable(
                name: "Skylights");

            migrationBuilder.DropTable(
                name: "WallsTypeDynamicOptions");

            migrationBuilder.DropTable(
                name: "WaterHeatingSystems");

            migrationBuilder.DropTable(
                name: "InsulationMaterialDynamicOptions");

            migrationBuilder.DropTable(
                name: "Windows");

            migrationBuilder.DropTable(
                name: "DistributionSystem");

            migrationBuilder.DropTable(
                name: "Slabs");

            migrationBuilder.DropTable(
                name: "Insulations");

            migrationBuilder.DropTable(
                name: "FoundationWalls");

            migrationBuilder.DropTable(
                name: "FrameFloors");

            migrationBuilder.DropTable(
                name: "Roofs");

            migrationBuilder.DropTable(
                name: "Walls");

            migrationBuilder.DropTable(
                name: "Foundations");

            migrationBuilder.DropTable(
                name: "Attics");

            migrationBuilder.DropTable(
                name: "Buildings");

            migrationBuilder.DropTable(
                name: "Abouts");

            migrationBuilder.DropTable(
                name: "Addresss");

            migrationBuilder.DropTable(
                name: "DistributionSystems");

            migrationBuilder.DropTable(
                name: "HVACPlants");

            migrationBuilder.DropTable(
                name: "Photovoltaics");

            migrationBuilder.DropTable(
                name: "WaterHeatings");

            migrationBuilder.DropTable(
                name: "ZoneFloors");

            migrationBuilder.DropTable(
                name: "ZoneRoofs");

            migrationBuilder.DropTable(
                name: "ZoneWalls");
        }
    }
}

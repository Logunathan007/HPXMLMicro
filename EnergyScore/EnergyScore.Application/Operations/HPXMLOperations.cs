using EnergyScore.Application.Mappers.DTOS.AboutDTOS;
using EnergyScore.Application.Mappers.DTOS.AddressDTOS;
using EnergyScore.Application.Mappers.DTOS.CommonDTOS;
using EnergyScore.Application.Mappers.DTOS.DistributionSystemDTOS;
using EnergyScore.Application.Mappers.DTOS.HVACPlantDTOS;
using EnergyScore.Application.Mappers.DTOS.PhotovoltaicsDTOS;
using EnergyScore.Application.Mappers.DTOS.WaterHeating;
using EnergyScore.Application.Mappers.DTOS.ZoneFloorDTOS;
using EnergyScore.Application.Mappers.DTOS.ZoneRoofDTOS;
using EnergyScore.Application.Mappers.DTOS.ZoneWallDTOS;
using EnergyScore.Application.Templates.HPXMLs;
using EnergyScore.Application.Templates.HPXMLs.Systems;
using EnergyScore.Application.Templates.HPXMLs.ZoneFloors;
using EnergyScore.Application.Templates.HPXMLs.ZoneRoofs;
using EnergyScore.Application.Templates.HPXMLs.ZoneWalls;

namespace EnergyScore.Application.Operations
{
    public interface IHPXMLOperations
    {
        public HPXML GetHPXMLObj(Guid buildingId, AddressDTO addressDTO, AboutDTO aboutDTO);
        public List<AirInfiltrationMeasurement> AirInfiltrationMeasurementConvertor(AboutDTO aboutDTO);
    }
    public class HPXMLOperations : IHPXMLOperations
    {
        private readonly IIdConversionOpertaions _idConvertor;
        private readonly IZoneFloorOperatoins _zoneFloorOperatoins;
        private readonly IZoneRoofOperations _zoneRoofOperations;
        private readonly IZoneWallOperations _zoneWallOperations;
        private readonly IWaterHeatingOperations _waterHeatingOperations;
        private readonly IPhotovoltaicsOperations _photovoltaicsOperations;
        private readonly IHVACPlantOperations _HVACPlantOperations;
        private readonly IDistributionSystemOperations _distributionSystemOperations;
        public HPXMLOperations(IIdConversionOpertaions idConversionOpertaions,
            IZoneFloorOperatoins zoneFloorOperatoins,
            IZoneRoofOperations zoneRoofOperations,
            IZoneWallOperations zoneWallOperations,
            IWaterHeatingOperations waterHeatingOperations,
            IPhotovoltaicsOperations photovoltaicsOperations,
            IHVACPlantOperations hVACPlantOperations,
            IDistributionSystemOperations distributionSystemOperations)
        {
            _idConvertor = idConversionOpertaions;
            _zoneFloorOperatoins = zoneFloorOperatoins;
            _zoneRoofOperations = zoneRoofOperations;
            _zoneWallOperations = zoneWallOperations;
            _waterHeatingOperations = waterHeatingOperations;
            _photovoltaicsOperations = photovoltaicsOperations;
            _HVACPlantOperations = hVACPlantOperations;
            _distributionSystemOperations = distributionSystemOperations;
        }

        public HPXML GetHPXMLObj(Guid buildingId, AddressDTO addressDTO, AboutDTO aboutDTO)
        {
            IEnumerable<FoundationDTO> foundationList = _zoneFloorOperatoins.GetFoundationsByBuildingId(buildingId);
            IEnumerable<FoundationWallDTO> foundationWallList = _zoneFloorOperatoins.GetFoundationWallsByBuildingId(buildingId);
            IEnumerable<FrameFloorDTO> frameFloorList = _zoneFloorOperatoins.GetFrameFloorByBuildingId(buildingId);
            IEnumerable<SlabDTO> slabList = _zoneFloorOperatoins.GetSlabByBuildingId(buildingId);
            IEnumerable<RoofDTO> roofList = _zoneRoofOperations.GetRoofsByBuildingId(buildingId);
            IEnumerable<AtticDTO> atticList = _zoneRoofOperations.GetAtticsByBuildingId(buildingId);
            IEnumerable<WallDTO> wallList = _zoneWallOperations.GetWallsByBuildingId(buildingId);
            IEnumerable<SkylightDTO> skylightList = _zoneRoofOperations.GetSkylightsByBuildingId(buildingId);
            IEnumerable<WindowDTO> windowList = _zoneWallOperations.GetWindowByBuildingId(buildingId);
            IEnumerable<HeatingSystemDTO> heatingSystemList = _HVACPlantOperations.GetHeatingSystemsByBuildingId(buildingId);
            IEnumerable<CoolingSystemDTO> coolingSystemList = _HVACPlantOperations.GetCoolingSystemsByBuildingId(buildingId);
            IEnumerable<HeatPumpDTO> heatPumpList = _HVACPlantOperations.GetHeatPumpsByBuildingId(buildingId);
            IEnumerable<PVSystemDTO> PVSystemList = _photovoltaicsOperations.GetPVSystemsByBuildingId(buildingId);
            IEnumerable<WaterHeatingSystemDTO> waterHeatingList = _waterHeatingOperations.GetWaterHeatingSystemByBuildingId(buildingId);
            IEnumerable<DistributionSystemDTO> distributionSystemList = _distributionSystemOperations.GetDistributionSystemByBuildingId(buildingId);
            var hpxml = new HPXML
            {
                XMLTransactionHeaderInformation = new XMLTransactionHeaderInformation
                {
                    XMLType = "",
                    XMLGeneratedBy = "Inspection Depot",
                    Transaction = "create"
                },
                SoftwareInfo = new SoftwareInfo(),
                Building = new Building
                {
                    BuildingID = new BuildingID
                    {
                        Id = _idConvertor.GuidToHPXMLIDConvertor(buildingId)
                    },
                    Site = new Site
                    {
                        SiteID = new SiteID
                        {
                            Id = _idConvertor.GuidToHPXMLIDConvertor(addressDTO.Id)
                        },
                        Address = new Address
                        {
                            Address1 = addressDTO.Address1,
                            Address2 = addressDTO.Address2,
                            CityMunicipality = addressDTO.City,
                            StateCode = addressDTO.State,
                            ZipCode = addressDTO.Zipcode
                        }
                    },
                    ProjectStatus = new ProjectStatus
                    {
                        EventType = "quality assurance/monitoring",
                        Date = DateTime.Today.Date.ToString("yyyy-MM-dd")
                    },
                    BuildingDetails = new BuildingDetails
                    {
                        BuildingSummary = new BuildingSummary
                        {
                            Site = new BSSite
                            {
                                OrientationOfFrontOfHome = aboutDTO.OrientationOfFrontOfHome,
                                AzimuthOfFrontOfHome = aboutDTO.AzimuthOfFrontOfHome
                            },
                            BuildingConstruction = new BuildingConstruction
                            {
                                YearBuilt = aboutDTO.YearBuilt,
                                ResidentialFacilityType = aboutDTO.ResidentialFacilityType,
                                NumberofConditionedFloorsAboveGrade = aboutDTO.NumberofConditionedFloorsAboveGrade,
                                AverageCeilingHeight = aboutDTO.AverageCeilingHeight,
                                NumberofBedrooms = aboutDTO.NumberofBedrooms,
                                ConditionedFloorArea = aboutDTO.ConditionedFloorArea,
                                ConditionedBuildingVolume = aboutDTO.ConditionedBuildingVolume,
                                extension = aboutDTO.ManufacturedHomeSections == null ? null : new extension()
                                {
                                    ManufacturedHomeSections = aboutDTO.ManufacturedHomeSections
                                }
                            }
                        },
                        Enclosure = new Enclosure
                        {
                            AirInfiltration = new AirInfiltration
                            {
                                AirInfiltrationMeasurement = this.AirInfiltrationMeasurementConvertor(aboutDTO)
                            },
                            Attics = (atticList == null || atticList.Count() == 0) ? null : new Attics()
                            {
                                Attic = this.AtticConvertor(atticList)
                            },
                            Foundations = (foundationList == null || foundationList.Count() == 0) ? null : new Foundations()
                            {
                                Foundation = FoundationConvertor(foundationList)
                            },
                            Roofs = (roofList == null || roofList.Count() == 0) ? null : new Roofs()
                            {
                                Roof = RoofConvertor(roofList)
                            },
                            Walls = (wallList == null || wallList.Count() == 0) ? null : new Walls()
                            {
                                Wall = WallConvertor(wallList)
                            },
                            FoundationWalls = (foundationWallList == null || foundationWallList.Count() == 0) ? null : new FoundationWalls()
                            {
                                FoundationWall = this.FoundationWallConvertor(foundationWallList)
                            },
                            FrameFloors = (frameFloorList == null || frameFloorList.Count() == 0) ? null : new FrameFloors()
                            {
                                FrameFloor = this.FrameFloorConveror(frameFloorList)
                            },
                            Slabs = (slabList == null || slabList.Count() == 0) ? null : new Slabs()
                            {
                                Slab = this.SlabConvertor(slabList)
                            },
                            Windows = (windowList == null || windowList.Count() == 0) ? null : new Windows()
                            {
                                Window = this.WindowConvertor(windowList)
                            },
                            Skylights = (skylightList == null || skylightList.Count() == 0) ? null : new Skylights()
                            {
                                Skylight = this.SkyLightConvertor(skylightList)
                            }
                        },
                        Systems = new HpxmlSystems()
                        {
                            HVAC = new HVAC()
                            {
                                HVACPlant = new HVACPlant()
                                {
                                    CoolingSystem = this.CoolingSystemConvertor(coolingSystemList),
                                    HeatingSystem = this.HeatingSystemConvertor(heatingSystemList),
                                    HeatPump = this.HeatPumpConvertor(heatPumpList)
                                },
                                HVACDistribution = this.HVACDistributionConvertor(distributionSystemList)
                            },
                            WaterHeating = new WaterHeating()
                            {
                                WaterHeatingSystem = this.WaterHeatingSystemConvertor(waterHeatingList)
                            },
                            Photovoltaics = new Photovoltaics()
                            {
                                PVSystem = this.PVSystemConvertor(PVSystemList),
                            }
                        }
                    }
                }
            };
            return hpxml;
        }
        public List<Window> WindowConvertor(IEnumerable<WindowDTO> windowDTOs)
        {
            List<Window> windows = new List<Window>();
            foreach (WindowDTO item in windowDTOs)
            {
                windows.Add(new Window()
                {
                    SystemIdentifier = new SystemIdentifier()
                    {
                        Id = _idConvertor.GuidToHPXMLIDConvertor(item.Id),
                    },
                    FrameType = this.FrameTypeConvertor(item.FrameType, item.FrameTypeDynamicOptions),
                    Area = item.Area,
                    SHGC = item.SHGC,
                    UFactor = item.UFactor,
                });
            }
            return windows;
        }
        public FrameType FrameTypeConvertor(string frameType, FrameTypeDynamicOptionsDTO options)
        {
            if (frameType == null || frameType == string.Empty || options == null) return null;
            switch (frameType)
            {
                case "Aluminum":
                    return new FrameType() { Aluminum = new Aluminum() { ThermalBreak = options.ThermalBreak } };
                case "Composite":
                    return new FrameType() { Composite = new Composite() };
                case "Fiberglass":
                    return new FrameType() { Fiberglass = new Fiberglass() { } };
                case "Metal":
                    return new FrameType() { Metal = new Metal() { ThermalBreak = options.ThermalBreak } };
                case "Vinyl":
                    return new FrameType() { Vinyl = new Vinyl() { } };
                case "Wood":
                    return new FrameType() { Wood = new Wood() { } };
                case "Other":
                    return new FrameType() { Other = new Other() };
                default:
                    return null;
            }
        }
        public List<PVSystem> PVSystemConvertor(IEnumerable<PVSystemDTO> pVSystemDTOs)
        {
            List<PVSystem> PVSystems = new List<PVSystem>();
            foreach (PVSystemDTO item in pVSystemDTOs)
            {
                PVSystems.Add(new PVSystem()
                {
                    ArrayAzimuth = item.ArrayAzimuth,
                    ArrayTilt = item.ArrayTilt,
                    ArrayOrientation = item.ArrayOrientation,
                    CollectorArea = item.CollectorArea,
                    MaxPowerOutput = item.MaxPowerOutput,
                    NumberOfPanels = item.NumberOfPanels,
                    YearInverterManufactured = item.YearInverterManufactured,
                    YearModulesManufactured = item.YearModulesManufactured,
                    SystemIdentifier = new SystemIdentifier()
                    {
                        Id = _idConvertor.GuidToHPXMLIDConvertor(item.Id),
                    }
                });
            }
            return PVSystems;
        }
        public List<WaterHeatingSystem> WaterHeatingSystemConvertor(IEnumerable<WaterHeatingSystemDTO> waterHeatingSystemDTOs)
        {
            List<WaterHeatingSystem> waterHeatings = new List<WaterHeatingSystem>();
            foreach (WaterHeatingSystemDTO item in waterHeatingSystemDTOs)
            {
                waterHeatings.Add(new WaterHeatingSystem()
                {
                    SystemIdentifier = new SystemIdentifier()
                    {
                        Id = _idConvertor.GuidToHPXMLIDConvertor(item.Id),
                    },
                    EnergyFactor = item.EnergyFactor,
                    FractionDHWLoadServed = item.FractionDHWLoadServed,
                    FuelType = item.FuelType,
                    ModelYear = item.ModelYear,
                    UniformEnergyFactor = item.UniformEnergyFactor,
                    WaterHeaterType = item.WaterHeaterType,
                    YearInstalled = item.YearInstalled,
                });
            }
            return waterHeatings;
        }
        public List<CoolingSystem> CoolingSystemConvertor(IEnumerable<CoolingSystemDTO> coolingSystemDTOs)
        {
            List<CoolingSystem> coolingSystems = new List<CoolingSystem>();
            foreach (CoolingSystemDTO item in coolingSystemDTOs)
            {
                coolingSystems.Add(new CoolingSystem()
                {
                    SystemIdentifier = new SystemIdentifier()
                    {
                        Id = _idConvertor.GuidToHPXMLIDConvertor(item.Id),
                    },
                    AnnualCoolingEfficiency = new AnnualCoolingEfficiency()
                    {
                        Units = item.AnnualCoolingEfficiencyUnits,
                        Value = item.AnnualCoolingEfficiencyValue
                    },
                    CoolingCapacity = item.CoolingCapacity,
                    CoolingSystemType = item.CoolingSystemType,
                    DistributionSystem = new DistributionSystem()
                    {
                        IdRef = _idConvertor.GuidToHPXMLIDConvertor(item.DistributionSystemId)
                    },
                    FloorAreaServed = item.FloorAreaServed,
                    FractionCoolLoadServed = item.FractionCoolLoadServed,
                    ModelYear = item.ModelYear,
                    YearInstalled = item.YearInstalled,
                });
            }
            return coolingSystems;
        }
        public List<HeatingSystem> HeatingSystemConvertor(IEnumerable<HeatingSystemDTO> heatingSystemDTOs)
        {
            List<HeatingSystem> HeatingSystems = new List<HeatingSystem>();
            foreach (HeatingSystemDTO item in heatingSystemDTOs)
            {
                HeatingSystems.Add(new HeatingSystem()
                {
                    SystemIdentifier = new SystemIdentifier()
                    {
                        Id = _idConvertor.GuidToHPXMLIDConvertor(item.Id)
                    },
                    FloorAreaServed = item.FloorAreaServed,
                    AnnualHeatingEfficiency = new AnnualHeatingEfficiency()
                    {
                        Units = item.AnnualHeatingEfficiencyUnits,
                        Value = item.AnnualHeatingEfficiencyValue
                    },
                    DistributionSystem = new DistributionSystem()
                    {
                        IdRef = _idConvertor.GuidToHPXMLIDConvertor(item.DistributionSystemId)
                    },
                    FractionHeatLoadServed = item.FractionHeatLoadServed,
                    HeatingCapacity = item.HeatingCapacity,
                    HeatingSystemFuel = item.HeatingSystemFuel,
                    HeatingSystemType = item.HeatingSystemType,
                    ModelYear = item.ModelYear,
                    YearInstalled = item.YearInstalled,
                });
            }
            return HeatingSystems;
        }
        public List<HeatPump> HeatPumpConvertor(IEnumerable<HeatPumpDTO> heatPumpDTOs)
        {
            List<HeatPump> heatPumps = new List<HeatPump>();
            foreach (HeatPumpDTO item in heatPumpDTOs)
            {
                heatPumps.Add(new HeatPump()
                {
                    AnnualCoolingEfficiency = new AnnualCoolingEfficiency()
                    {
                        Units = item.AnnualCoolingEfficiencyUnits,
                        Value = item.AnnualCoolingEfficiencyValue,
                    },
                    AnnualHeatingEfficiency = new AnnualHeatingEfficiency()
                    {
                        Units = item.AnnualHeatingEfficiencyUnits,
                        Value = item.AnnualCoolingEfficiencyValue,
                    },
                    CoolingCapacity = item.CoolingCapacity,
                    FloorAreaServed = item.FloorAreaServed,
                    FractionCoolLoadServed = item.FractionCoolLoadServed,
                    FractionHeatLoadServed = item.FractionHeatLoadServed,
                    HeatingCapacity = item.HeatingCapacity,
                    HeatingCapacity17F = item.HeatingCapacity17F,
                    HeatPumpType = item.HeatPumpType,
                    ModelYear = item.ModelYear,
                    YearInstalled = item.YearInstalled,
                    SystemIdentifier = new SystemIdentifier()
                    {
                        Id = _idConvertor.GuidToHPXMLIDConvertor(item.Id),
                    }
                });
            }
            return heatPumps;
        }

        public List<HVACDistribution> HVACDistributionConvertor(IEnumerable<DistributionSystemDTO> distributionSystemDTOs)
        {
            List<HVACDistribution> distributionSystems = new List<HVACDistribution>();
            foreach (DistributionSystemDTO item in distributionSystemDTOs)
            {
                distributionSystems.Add(new HVACDistribution()
                {
                    SystemIdentifier = new SystemIdentifier
                    {
                        Id = _idConvertor.GuidToHPXMLIDConvertor(item.Id),
                    },
                    DistributionSystemType = new DistributionSystemType()
                    {
                        AirDistribution = new AirDistribution()
                        {
                            DuctLeakageMeasurement = new DuctLeakageMeasurement()
                            {
                                DuctType = item.DuctType,
                                LeakinessObservedVisualInspection = item.LeakinessObservedVisualInspection,
                                DuctLeakage = new DuctLeakage()
                                {
                                    TotalOrToOutside = item.TotalOrToOutside,
                                    Units = item.Units,
                                    Value = item.Value
                                }
                            },
                            Ducts = item.Ducts.Select(duct => new Ducts()
                            {
                                DuctInsulationMaterial = this.InsulationMaterialDynamicOpionsConvertor(duct.DuctInsulationMaterial, duct.DuctInsulationMaterialDynamicOptions),
                                DuctInsulationRValue = duct.DuctInsulationRValue,
                                DuctInsulationThickness = duct.DuctInsulationThickness,
                                DuctLocation = duct.DuctLocation,
                                FractionDuctArea = duct.FractionDuctArea,
                            }).ToList(),
                        }
                    }
                });
            }
            return distributionSystems;
        }
        public List<AirInfiltrationMeasurement> AirInfiltrationMeasurementConvertor(AboutDTO aboutDTO)
        {
            List<AirInfiltrationMeasurement> airInFilMeasure = new List<AirInfiltrationMeasurement>();
            foreach (var item in aboutDTO.AirInfiltrationMeasurements)
            {
                airInFilMeasure.Add(new AirInfiltrationMeasurement
                {
                    SystemIdentifier = new SystemIdentifier
                    {
                        Id = _idConvertor.GuidToHPXMLIDConvertor(item.Id),
                    },
                    HousePressure = item.HousePressure,
                    LeakinessDescription = item.LeakinessDescription,
                    BuildingAirLeakage = new BuildingAirLeakage()
                    {
                        UnitofMeasure = item.UnitofMeasure,
                        AirLeakage = item.AirLeakage
                    }
                });
            }
            return airInFilMeasure;
        }
        public List<Skylight> SkyLightConvertor(IEnumerable<SkylightDTO> skylightList)
        {
            List<Skylight> skylights = new List<Skylight>();
            foreach (SkylightDTO skylight in skylightList)
            {
                skylights.Add(new Skylight()
                {
                    SystemIdentifier = new SystemIdentifier()
                    {
                        Id = _idConvertor.GuidToHPXMLIDConvertor(skylight.Id)
                    },
                    Area = skylight.Area,
                    UFactor = skylight.UFactor,
                    SHGC = skylight.SHGC,
                    AttachedToRoof = (skylight?.Roof == null) ? null : new AttachedToRoof()
                    {
                        IdRef = _idConvertor.GuidToHPXMLIDConvertor(skylight.Roof.Id)
                    }
                });
            }
            return skylights;
        }
        public List<Wall> WallConvertor(IEnumerable<WallDTO> wallDTO)
        {
            List<Wall> walls = new List<Wall>();
            foreach (WallDTO wall in wallDTO)
            {
                walls.Add(new Wall()
                {
                    SystemIdentifier = new SystemIdentifier()
                    {
                        Id = _idConvertor.GuidToHPXMLIDConvertor(wall.Id)
                    },
                    AtticWallType = wall.AtticWallType,
                    Area = wall.Area,
                    Insulation = (wall.Insulation == null) ? null : new Insulation()
                    {
                        SystemIdentifier = new SystemIdentifier
                        {
                            Id = _idConvertor.GuidToHPXMLIDConvertor(wall.Insulation.Id)
                        },
                        AssemblyEffectiveRValue = wall.Insulation.AssemblyEffectiveRValue,
                        Layer = wall.Insulation.Layers.Count() == 0 ? null : wall.Insulation.Layers.Select
                        (obj =>
                            new Layer
                            {
                                NominalRValue = obj.NominalRValue,
                                InstallationType = obj.InstallationType,
                                InsulationMaterial = InsulationMaterialDynamicOpionsConvertor(obj.InsulationMaterial, obj.InsulationMaterialDynamicOptions)
                            }).ToList()
                    }
                });
            }
            return walls;
        }
        public List<Roof> RoofConvertor(IEnumerable<RoofDTO> roofDTO)
        {
            List<Roof> roofs = new List<Roof>();
            foreach (var roof in roofDTO)
            {
                roofs.Add(new Roof()
                {
                    SystemIdentifier = new SystemIdentifier
                    {
                        Id = _idConvertor.GuidToHPXMLIDConvertor(roof.Id)
                    },
                    Area = roof.Area,
                    RoofType = roof.RoofType,
                    RoofColor = roof.RoofColor,
                    SolarAbsorptance = roof.SolarAbsorptance,
                    RadiantBarrier = roof.RadiantBarrier,
                });
            }
            return roofs;
        }
        public List<Attic> AtticConvertor(IEnumerable<AtticDTO> atticDTO)
        {
            List<Attic> attics = new List<Attic>();
            foreach (var attic in atticDTO)
            {
                attics.Add(new Attic()
                {
                    SystemIdentifier = new SystemIdentifier
                    {
                        Id = _idConvertor.GuidToHPXMLIDConvertor(attic.Id)
                    },
                    AtticType = GetAtticTypeDynamicOption(attic.AtticType, attic.AtticTypeDynamicOptions),
                    AttachedToRoof = attic.Roofs.Select(obj => new AttachedToRoof()
                    {
                        IdRef = _idConvertor.GuidToHPXMLIDConvertor(obj.Id)
                    }).ToList(),
                    AttachedToWall = attic.Walls.Select(obj => new AttachedToWall()
                    {
                        IdRef = _idConvertor.GuidToHPXMLIDConvertor(obj.Id)
                    }).ToList(),
                    AttachedToFrameFloor = attic.FrameFloors.Select(obj => new AttachedToFrameFloor()
                    {
                        IdRef = _idConvertor.GuidToHPXMLIDConvertor(obj.Id)
                    }).ToList()
                });
            }
            return attics;
        }
        public InsulationMaterial InsulationMaterialDynamicOpionsConvertor(string type, InsulationMaterialDynamicOptionsDTO options)
        {
            if (options == null) return null;
            switch (type)
            {
                case "Batt":
                    return new InsulationMaterial() { Batt = options.Batt };
                case "LooseFill":
                    return new InsulationMaterial() { LooseFill = options.LooseFill };
                case "Rigid":
                    return new InsulationMaterial() { Rigid = options.Rigid };
                case "SprayFoam":
                    return new InsulationMaterial() { SprayFoam = options.SprayFoam };
                case "Other":
                    return new InsulationMaterial() { Other = new Others() };
                case "None":
                    return new InsulationMaterial() { None = new None() };
                case "Unknown":
                    return new InsulationMaterial() { Unknown = new Unknown() };
                default:
                    return new InsulationMaterial();
            }
        }
        public AtticType GetAtticTypeDynamicOption(string type, AtticTypeDynamicOptionDTO options)
        {
            switch (type)
            {
                case "Attic":
                    return new AtticType()
                    {
                        Attic = new AtticTypes()
                        {
                            CapeCod = options.CapeCod,
                            Conditioned = options.Conditioned,
                            Vented = options.Vented,
                        }
                    };
                case "CathedralCeiling":
                    return new AtticType()
                    {
                        CathedralCeiling = ""
                    };
                case "FlatRoof":
                    return new AtticType()
                    {
                        FlatRoof = ""
                    };
                case "BelowApartment":
                    return new AtticType()
                    {
                        BelowApartment = ""
                    };
                default:
                    return new AtticType();
            }
        }
        public List<Foundation> FoundationConvertor(IEnumerable<FoundationDTO> foundationDTO)
        {
            List<Foundation> foundations = new List<Foundation>();
            foreach (var foundation in foundationDTO)
            {
                foundations.Add(new Foundation
                {
                    SystemIdentifier = new SystemIdentifier
                    {
                        Id = _idConvertor.GuidToHPXMLIDConvertor(foundation.Id)
                    },
                    FoundationType = this.GetFoundationType(foundation.FoundationType, foundation.FoundationTypeDynamicOptions),

                    AttachedToFoundationWall = foundation.FoundationWalls.Select(obj => new AttachedToFoundationWall
                    {
                        IdRef = _idConvertor.GuidToHPXMLIDConvertor(obj.Id)
                    }).ToList(),

                    AttachedToFrameFloor = foundation.FrameFloors.Select(obj => new AttachedToFrameFloor
                    {
                        IdRef = _idConvertor.GuidToHPXMLIDConvertor(obj.Id)
                    }).ToList(),

                    AttachedToSlab = foundation.Slabs.Select(obj => new AttachedToSlab
                    {
                        IdRef = _idConvertor.GuidToHPXMLIDConvertor(obj.Id)
                    }).ToList()
                });
            }
            return foundations;
        }
        public FoundationType GetFoundationType(string foundationType, FoundationTypeDynamicOptionDTO options)
        {
            switch (foundationType)
            {
                case "SlabOnGrade":
                    return new FoundationType() { SlabOnGrade = new SlabOnGrade() };
                case "Crawlspace":
                    return new FoundationType() { Crawlspace = new Crawlspace() { Conditioned = options.Conditioned, Vented = options.Vented } };
                case "Basement":
                    return new FoundationType() { Basement = new Basement() { Conditioned = options.Conditioned, Finished = options.Finished } };
                case "Garage":
                    return new FoundationType() { Garage = new Garage() { Conditioned = options.Conditioned } };
                case "AboveApartment":
                    return new FoundationType() { AboveApartment = new AboveApartment() };
                case "Ambient":
                    return new FoundationType() { Ambient = new Ambient() };
                default:
                    return new FoundationType();
            }
        }
        public List<FoundationWall> FoundationWallConvertor(IEnumerable<FoundationWallDTO> foundationWallDTO)
        {
            List<FoundationWall> foundationWalls = new List<FoundationWall>();
            foreach (var foundationWall in foundationWallDTO)
            {
                foundationWalls.Add(new FoundationWall
                {
                    SystemIdentifier = new SystemIdentifier
                    {
                        Id = _idConvertor.GuidToHPXMLIDConvertor(foundationWall.Id)
                    },
                    Area = foundationWall.Area,
                    Insulation = (foundationWall.Insulation==null)? null : new Insulation()
                    {
                        AssemblyEffectiveRValue = foundationWall.Insulation.AssemblyEffectiveRValue,
                        SystemIdentifier = new SystemIdentifier
                        {
                            Id = _idConvertor.GuidToHPXMLIDConvertor(foundationWall.Insulation.Id)
                        },
                        Layer = foundationWall.Insulation.Layers.Count() == 0 ? null : foundationWall.Insulation.Layers.Where(obj => obj.InstallationType == "Exterior" || obj.InstallationType == "Interior") .Select(obj => new Layer
                        {
                            NominalRValue = obj.NominalRValue,
                            InstallationType = obj.InstallationType,
                            InsulationMaterial = InsulationMaterialDynamicOpionsConvertor(obj.InsulationMaterial, obj.InsulationMaterialDynamicOptions)
                        }).ToList()
                    } 
                });
            }
            return foundationWalls;
        }
        public List<FrameFloor> FrameFloorConveror(IEnumerable<FrameFloorDTO> frameFloorDTOs)
        {
            List<FrameFloor> floors = new List<FrameFloor>();
            foreach (var floor in frameFloorDTOs)
            {
                floors.Add(new FrameFloor
                {
                    SystemIdentifier = new SystemIdentifier
                    {
                        Id = _idConvertor.GuidToHPXMLIDConvertor(floor.Id)
                    },
                    Area = floor.Area,
                    Insulation = (floor.Insulation == null) ? null : new Insulation
                    {
                        SystemIdentifier = new SystemIdentifier
                        {
                            Id = _idConvertor.GuidToHPXMLIDConvertor(floor.Insulation.Id)
                        },
                        AssemblyEffectiveRValue = floor.Insulation.AssemblyEffectiveRValue,
                        Layer = floor.Insulation.Layers.Select(obj => new Layer
                        {
                            InstallationType = obj.InstallationType,
                            InsulationMaterial = InsulationMaterialDynamicOpionsConvertor(obj.InsulationMaterial, obj.InsulationMaterialDynamicOptions),
                            NominalRValue = obj.NominalRValue
                        }).ToList()
                    }
                });
            }
            return floors;
        }
        public List<Slab> SlabConvertor(IEnumerable<SlabDTO> slabDTO)
        {
            if (slabDTO == null) return null;
            List<Slab> slabs = new List<Slab>();
            foreach (var slab in slabDTO)
            {
                slabs.Add(new Slab
                {
                    SystemIdentifier = new SystemIdentifier
                    {
                        Id = _idConvertor.GuidToHPXMLIDConvertor(slab.Id)
                    },
                    ExposedPerimeter = slab.ExposedPerimeter,
                    PerimeterInsulation = (slab.PerimeterInsulation == null) ? null : new PerimeterInsulation
                    {
                        SystemIdentifier = new SystemIdentifier
                        {
                            Id = _idConvertor.GuidToHPXMLIDConvertor(slab.PerimeterInsulation.Id)
                        },
                        AssemblyEffectiveRValue = slab.PerimeterInsulation.AssemblyEffectiveRValue,
                        Layer = slab.PerimeterInsulation.Layers.Select(obj => new Layer
                        {
                            InstallationType = obj.InstallationType,
                            InsulationMaterial = InsulationMaterialDynamicOpionsConvertor(obj.InsulationMaterial, obj.InsulationMaterialDynamicOptions),
                            NominalRValue = obj.NominalRValue
                        }).ToList()
                    }
                });
            }
            return slabs;
        }

    }
}


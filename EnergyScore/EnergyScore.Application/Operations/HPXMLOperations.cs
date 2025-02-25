using EnergyScore.Application.Mappers.DTOS.AboutDTOS;
using EnergyScore.Application.Mappers.DTOS.AddressDTOS;
using EnergyScore.Application.Mappers.DTOS.CommonDTOS;
using EnergyScore.Application.Mappers.DTOS.ZoneFloorDTOS;
using EnergyScore.Application.Mappers.DTOS.ZoneRoofDTOS;
using EnergyScore.Application.Templates.HPXMLs;
using EnergyScore.Application.Templates.HPXMLs.ZoneFloors;
using EnergyScore.Application.Templates.HPXMLs.ZoneRoofs;

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
        public HPXMLOperations(IIdConversionOpertaions idConversionOpertaions,
            IZoneFloorOperatoins zoneFloorOperatoins,
            IZoneRoofOperations zoneRoofOperations,
            IZoneWallOperations zoneWallOperations)
        {
            _idConvertor = idConversionOpertaions;
            _zoneFloorOperatoins = zoneFloorOperatoins;
            _zoneRoofOperations = zoneRoofOperations;
            _zoneWallOperations = zoneWallOperations;
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
                            Skylights = (skylightList == null || skylightList.Count() == 0) ? null : new Skylights()
                            {
                                Skylight = this.SkyLightConvertor(skylightList)
                            }
                        }
                    }
                }
            };
            return hpxml;
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
                    Insulations = wall.Insulations.Select(obj => new Insulation
                    {
                        SystemIdentifier = new SystemIdentifier
                        {
                            Id = _idConvertor.GuidToHPXMLIDConvertor(obj.Id)
                        },
                        AssemblyEffectiveRValue = obj.AssemblyEffectiveRValue,
                        Layer = new Layer
                        {
                            NominalRValue = obj.NominalRValue,
                            InstallationType = obj.InstallationType,
                            InsulationMaterial = InsulationMaterialDynamicOpions(obj.InsulationMaterial,obj.InsulationMaterialDynamicOptions)

                        }
                    }).ToList()
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
        public InsulationMaterial InsulationMaterialDynamicOpions(string type, InsulationMaterialDynamicOptionsDTO options)
        {
            switch (type)
            {
                case "Batt":
                    return new InsulationMaterial() { Batt = options.Batt };
                case "LooseFill":
                    return new InsulationMaterial() { LooseFill = options.LooseFill };
                case "Rigid":
                    return new InsulationMaterial() { Rigid = options.Rigit };
                case "SprayFoam":
                    return new InsulationMaterial() { SprayFoam = new SprayFoam() };
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
                    Insulation = foundationWall.Insulations.Select(obj => new Insulation
                    {
                        SystemIdentifier = new SystemIdentifier
                        {
                            Id = _idConvertor.GuidToHPXMLIDConvertor(obj.Id)
                        },
                        AssemblyEffectiveRValue = obj.AssemblyEffectiveRValue,
                        Layer = new Layer
                        {
                            NominalRValue = obj.NominalRValue
                        }
                    }).ToList()
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
                    Insulation = floor.Insulations.Select(obj => new Insulation
                    {
                        SystemIdentifier = new SystemIdentifier
                        {
                            Id = _idConvertor.GuidToHPXMLIDConvertor(obj.Id)
                        },
                        AssemblyEffectiveRValue = obj.AssemblyEffectiveRValue,
                        Layer = new Layer
                        {
                            NominalRValue = obj.NominalRValue,
                        }
                    }).ToList()
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
                    PerimeterInsulation = slab.PerimeterInsulations.Select(obj => new PerimeterInsulation
                    {
                        SystemIdentifier = new SystemIdentifier
                        {
                            Id = _idConvertor.GuidToHPXMLIDConvertor(obj.Id)
                        },
                        AssemblyEffectiveRValue = obj.AssemblyEffectiveRValue,
                        Layer = new Layer
                        {
                            NominalRValue = obj.AssemblyEffectiveRValue
                        }
                    }).ToList()
                });
            }
            return slabs;
        }

    }
}


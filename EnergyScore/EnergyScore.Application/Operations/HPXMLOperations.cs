using EnergyScore.Application.Mappers.DTOS.AboutDTOS;
using EnergyScore.Application.Mappers.DTOS.AddressDTOS;
using EnergyScore.Application.Mappers.DTOS.ZoneFloorDTOS;
using EnergyScore.Application.Templates.HPXMLs;
using EnergyScore.Application.Templates.HPXMLs.ZoneFloors;

namespace EnergyScore.Application.Operations
{
    public interface IHPXMLOperations
    {
        public HPXML GetHPXMLObj(Guid buildingId, AddressDTO addressDTO, AboutDTO aboutDTO,ZoneFloorDTO zoneFloorDTO);
        public List<AirInfiltrationMeasurement> AirInfiltrationMeasurementConvertor(AboutDTO aboutDTO);
    }
    public class HPXMLOperations : IHPXMLOperations
    {
        IIdConversionOpertaions _idConvertor;
        public HPXMLOperations(IIdConversionOpertaions idConversionOpertaions)
        {
            _idConvertor = idConversionOpertaions;
        }

        public List<AirInfiltrationMeasurement>  AirInfiltrationMeasurementConvertor(AboutDTO aboutDTO)
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
        public HPXML GetHPXMLObj(Guid buildingId, AddressDTO addressDTO, AboutDTO aboutDTO, ZoneFloorDTO zoneFloorDTO)
        {
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
                            Foundations = (this.FoundationConvertor(zoneFloorDTO).Count==0)?null: (zoneFloorDTO?.Foundations?.Count == 0 || zoneFloorDTO?.Foundations == null) ? null : new Foundations()
                            {
                                Foundation = this.FoundationConvertor(zoneFloorDTO) 
                            },
                            FoundationWalls = (this.FoundationWallConvertor(zoneFloorDTO).Count == 0)?null:new FoundationWalls()
                            {
                                FoundationWall = this.FoundationWallConvertor(zoneFloorDTO)
                            },
                            FrameFloors = (this.FrameFloorConveror(zoneFloorDTO).Count == 0) ? null : new FrameFloors()
                            {
                                FrameFloor = this.FrameFloorConveror(zoneFloorDTO)
                            },
                            Slabs = (this.SlabConvertor(zoneFloorDTO).Count == 0) ? null : new Slabs()
                            {
                                Slab = this.SlabConvertor(zoneFloorDTO)
                            }
                        }
                    }
                }
            };
            return hpxml;
        }
        public FoundationType GetFoundationType (string foundationType,FoundationTypeDynamicOptionDTO options)
        {
            switch (foundationType)
            {
                case "SlabOnGrade":
                    return new FoundationType() { SlabOnGrade = new SlabOnGrade() };
                case "Crawlspace":
                    return new FoundationType() { Crawlspace = new Crawlspace() { Conditioned=options.Conditioned, Vented=options.Vented } };
                case "Basement":
                    return new FoundationType() { Basement = new Basement() { Conditioned=options.Conditioned, Finished=options.Finished } };
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
        public List<Foundation> FoundationConvertor(ZoneFloorDTO zoneFloorDTO)
        {
            if(zoneFloorDTO == null) return null;
            var foundations = new List<Foundation>(); 
            foreach (var foundation in zoneFloorDTO?.Foundations) {
                foundations.Add(new Foundation
                {
                    SystemIdentifier = new SystemIdentifier
                    {
                        Id = _idConvertor.GuidToHPXMLIDConvertor(foundation.Id)
                    },
                    FoundationType = this.GetFoundationType(foundation.FoundationType,foundation.FoundationTypeDynamicOptions),
                    
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
        
        public List<FoundationWall> FoundationWallConvertor(ZoneFloorDTO zoneFloorDTO)
        {
            if(zoneFloorDTO == null) return null;
            var foundationWalls = new List<FoundationWall>();
            foreach (var foundation in zoneFloorDTO?.Foundations)
            {
                foreach (var foundationWall in foundation?.FoundationWalls)
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
            }
            return foundationWalls;
        }

        public List<Slab> SlabConvertor(ZoneFloorDTO zoneFloorDTO)
        {
            if (zoneFloorDTO == null) return null;
            var slabs = new List<Slab>();
            foreach (var foundation in zoneFloorDTO?.Foundations)
            {
                foreach (var slab in foundation?.Slabs)
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
            }
            return slabs;
        }
        public List<FrameFloor> FrameFloorConveror(ZoneFloorDTO zoneFloorDTO)
        {
            if (zoneFloorDTO == null) return null;
            var frames = new List<FrameFloor>();
            foreach (var foundation in zoneFloorDTO?.Foundations)
            {
                foreach (var frame in foundation?.FrameFloors)
                {
                    frames.Add(new FrameFloor
                    {
                        SystemIdentifier = new SystemIdentifier
                        {
                            Id = _idConvertor.GuidToHPXMLIDConvertor(frame.Id)
                        },
                        Area = frame.Area,
                        Insulation = frame.Insulations.Select(obj => new Insulation
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
            }
            return frames;
        }
    }
}


using AutoMapper;
using WebApi.DTO;
using WebApi.ViewModel;

namespace WebApi.Model
{
    public class PropertyProfileMapper : Profile
    {
        public PropertyProfileMapper()
        {
            CreateMap<PropertyDTO, Property>()
                .ForMember(x => x.Improvements, y => y.MapFrom(z =>
                        z.SelectedBasic.Where(a => !string.IsNullOrWhiteSpace(a))
                        .Select(a => new ImprovementToProperty()
                        {
                            ImprovementId = Int32.Parse(a),
                            PropertyId = z.PropertyId
                        })));

            CreateMap<Property, PropertyDTO>();
            CreateMap<Property, PropertyMainDTO>();
            CreateMap<PropertyMainDTO, Property>();
            CreateMap<Province, ProvinceDTO>();
            CreateMap<ProvinceDTO,Province>();
            CreateMap<Station, StationDTO>();
            CreateMap<StationDTO,Station>();
            CreateMap<City, CityDTO>();
            CreateMap<CityDTO, City>();
            CreateMap<Owner, OwnerDTO>();
            CreateMap<OwnerDTO, Owner>();
            CreateMap<Project, ProjectDTO>();
            CreateMap<ProjectDTO, Project>();
            CreateMap<Property, PropertyAddressDTO>();
            CreateMap<PropertyAddressDTO, Property>();
            CreateMap<Improvement, ImprovementDTO>();
            CreateMap<ImprovementDTO, Improvement>();
            CreateMap<Property, PropertyImprovmentDTO>().ForMember(x => x.ImprovmentIds, y => y.MapFrom(x => x.Improvements.Select(z => z.Id).ToList()));
            CreateMap<PropertyImprovmentDTO, Property>();
            CreateMap<TypePropertyDTO,TypeProperty>();
            CreateMap<CompanyDTO, Company>();
        }
    }
}

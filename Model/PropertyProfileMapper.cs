using AutoMapper;
using WebApi.ViewModel;

namespace WebApi.Model
{
    public class PropertyProfileMapper : Profile
    {
        public PropertyProfileMapper()
        {
            CreateMap<PropertyViewModel, Property>()
                .ForMember(x => x.Improvements, y => y.MapFrom(z =>
                        z.SelectedBasic.Where(a => !string.IsNullOrWhiteSpace(a))
                        .Select(a => new ImprovementToProperty()
                        {
                            ImprovementId = Int32.Parse(a),
                            PropertyId = z.PropertyId
                        })));

            CreateMap<Property, PropertyViewModel>();

            CreateMap<Property, PropertyMainViewModel>();
            CreateMap<PropertyMainViewModel, Property>();

            CreateMap<Property, PropertyAddressViewModel>();
            CreateMap<PropertyAddressViewModel, Property>();

            CreateMap<Property, PropertyImprovmentViewModel>().ForMember(x => x.ImprovmentIds, y => y.MapFrom(x => x.Improvements.Select(z => z.Id).ToList()));
            CreateMap<PropertyImprovmentViewModel, Property>();
        }
    }
}

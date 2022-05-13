namespace WebApi.Service
{
    public interface IPropertyService
    {
        string GetPrypertyNameById(int propertyid);
    }

    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;
        public PropertyService(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public string GetPrypertyNameById(int propertyid)
        {
            return _propertyRepository.GetPrypertyNameById(propertyid);
        }
    }
}

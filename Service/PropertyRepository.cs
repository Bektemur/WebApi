using WebApi.Database;
namespace WebApi.Service
{
    public interface IPropertyRepository
    {
        string GetPrypertyNameById(int propertyid);
    }

    public class PropertyRepository : IPropertyRepository
    {
        private readonly ApplicationDbContext _context;
        public PropertyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public string GetPrypertyNameById(int propertyid)
        {
            return _context.Properties.Where(v => v.PropertyId == propertyid).Select(v => v.Name).FirstOrDefault();
        }
    }
}

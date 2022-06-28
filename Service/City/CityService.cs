using AutoMapper;
using WebApi.Database;
using WebApi.ViewModel;
using WebApi.Model;

namespace WebApi.Service.CityService
{
    public class CityService : ICityService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CityService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task AddCity(CityDTO city)
        {
            var cities = _mapper.Map<City>(city);
            _context.Add(cities);
            await _context.SaveChangesAsync();
        }

        public Task<CityDTO> GetById(int studentId)
        {
            throw new NotImplementedException();
        }

        public Task<List<CityDTO>> GetCityList()
        {
            throw new NotImplementedException();
        }

        public Task RemoveCity(int studentId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCity(CityDTO studentEntity)
        {
            throw new NotImplementedException();
        }
    }
}

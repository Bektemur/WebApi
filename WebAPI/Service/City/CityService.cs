using AutoMapper;
using WebApi.Database;
using WebApi.ViewModel;
using WebApi.Model;
using Microsoft.EntityFrameworkCore;

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

        public async Task<CityDTO> GetById(int cityId)
        {
            var cityData = await _context.City.FirstOrDefaultAsync(z => z.Id == cityId);
            var cities = _mapper.Map<CityDTO>(cityData);
            return cities;
        }

        public async Task<List<CityDTO>> GetCityList()
        {
            var cityList = await _context.City.ToListAsync();
            var cities = _mapper.Map<List<CityDTO>>(cityList);
            return cities;
        }

        public async Task RemoveCity(int cityId)
        {
            var city = await _context.City.FirstOrDefaultAsync(z => z.Id== cityId);
            if (city != null)
            {
                _context.City.Remove(city);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateCity(CityDTO city)
        {
            _context.Entry(city).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}

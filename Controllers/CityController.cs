using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Database;
using WebApi.Model;
using WebApi.ViewModel;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController: ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CityController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("AddCity")]
        public async Task<IActionResult> AddCity(CityViewModel city)
        {
            if (city == null) 
                return BadRequest();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CityViewModel, City>());
            var mapper = new Mapper(config);
            var cities = mapper.Map<CityViewModel, City>(city);
            _context.City.Add(cities);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}

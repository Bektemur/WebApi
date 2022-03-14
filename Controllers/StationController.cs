using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Database;
using WebApi.Model;
using WebApi.ViewModel;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public StationController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("AddStation")]
        public async Task<IActionResult> AddStation(StationViewModel station)
        {
            if (station == null)
                return BadRequest();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<StationViewModel, Station>());
            var mapper = new Mapper(config);
            var stations = mapper.Map<StationViewModel, Station>(station);
            _context.Stations.Add(stations);
            await _context.SaveChangesAsync();
            return Ok();
        }


    }
}

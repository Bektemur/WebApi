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
        private readonly IMapper _mapper;
        public StationController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("GetAllStation")]
        public IActionResult GetAllStation()
        {
            return Ok(_context.Stations.ToList());
        }
        [HttpGet]
        [Route("GetStationById")]
        public IActionResult GetStationById(int id)
        {
            return Ok(_context.Stations.Where(c=>c.StationId == id).ToList());
        }
        [HttpGet]
        [Route("GetStationByStationTypeId")]
        public IActionResult GetStationByStationTypeId(int id)
        {
            return Ok(_context.Stations.Where(c => c.StationType == (StationType)id).ToList());
        }
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(StationViewModel station)
        {
            if (station == null)
                return BadRequest();
            var stations = _mapper.Map<Station>(station);
            _context.Stations.Add(stations);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Update(int id, StationViewModel station)
        {
            var entity = _context.Stations.Where(v => v.StationId == id).FirstOrDefault();
            if (entity == null)
                return BadRequest("Station with Id = " + id + " not found");
            entity.Name = station.Name;
            entity.StationType = (StationType)station.StationType;
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = _context.Stations.Where(v => v.StationId == id).FirstOrDefault();
            if (entity == null)
                return BadRequest("Station with Id = " + id + " not found");
            _context.Stations.Remove(entity);
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}

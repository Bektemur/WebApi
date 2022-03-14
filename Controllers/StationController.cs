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
        public async Task<IActionResult> GetAllStation()
        {
            return Ok(_context.Stations.ToList());
        }
        [HttpGet]
        [Route("GetStationById")]
        public async Task<IActionResult> GetStationById(int id)
        {
            return Ok(_context.Stations.Where(c=>c.StationId == id).ToList());
        }
        [HttpGet]
        [Route("GetStationByStationTypeId")]
        public async Task<IActionResult> GetStationByStationTypeId(int id)
        {
            return Ok(_context.Stations.Where(c => c.StationType == (StationType)id).ToList());
        }

    }
}

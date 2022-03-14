using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Database;
using WebApi.Model;
using WebApi.ViewModel;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvinceController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ProvinceController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(ProvinceViewModel province)
        {
            if (province == null)
                return BadRequest();
            var provinces = _mapper.Map<Province>(province);
            _context.Province.Add(provinces);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPut]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Update(int id, ProvinceViewModel province)
        {
            var entity = _context.Province.Where(v => v.StationId == id).FirstOrDefault();
            if (entity == null)
                return BadRequest("City with Id = " + id + " not found");
            var provinces = _mapper.Map<Province>(province);
            _context.Province.Update(provinces);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = _context.Stations.Where(v => v.StationId == id).FirstOrDefault();
            if (entity == null)
                return BadRequest("City with Id = " + id + " not found");
            return Ok(entity);
        }
    }
}

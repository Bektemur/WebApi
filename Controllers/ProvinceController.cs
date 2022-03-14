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
        [Route("AddProvince")]
        public async Task<IActionResult> AddProvince(ProvinceViewModel province)
        {
            if (province == null)
                return BadRequest();
            var provinces = _mapper.Map<Province>(province);
            _context.Add(provinces);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}

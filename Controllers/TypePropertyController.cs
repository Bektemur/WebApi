using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Database;
using WebApi.Model;
using WebApi.ViewModel;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypePropertyController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        public TypePropertyController(IMapper mapper, ApplicationDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(TypePropertyViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            var entity = _mapper.Map<TypeProperty>(model);
            _context.TypeProperties.Add(entity);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}

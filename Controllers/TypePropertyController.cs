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
        public async Task<IActionResult> Create(TypePropertyDTO model)
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
        [HttpPut]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Update(int id, TypePropertyDTO typeProperty)
        {
            var entity = _context.TypeProperties.Where(v => v.TypePropertyId == id).FirstOrDefault();
            if (entity == null)
                return BadRequest("Type property with Id = " + id + " not found");
            entity.Name = typeProperty.Name;
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = _context.TypeProperties.Where(v => v.TypePropertyId == id).FirstOrDefault();
            if (entity == null)
                return NotFound("Type properties with Id = " + id + " not found");
            _context.TypeProperties.Remove(entity);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        [Route("GetTypePropertyId")]
        public IActionResult GetTypePropertyId(int id)
        {
            var typeProperty = _context.TypeProperties.Where(v => v.TypePropertyId == id).FirstOrDefault();
            if (typeProperty == null)
            {
                return NotFound("Type property with Id = " + id + " not found");
            }
            return Ok(typeProperty);
        }
        [HttpGet]
        [Route("GetAll")]
        public ActionResult GetAll()
        {
            return Ok(_context.TypeProperties.ToList());
        }
    }
}

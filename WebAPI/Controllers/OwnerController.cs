using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Database;
using WebApi.DTO;
using WebApi.Model;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public OwnerController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(OwnerDTO owner)
        {
            if (owner == null)
                return BadRequest();
            var owners = _mapper.Map<Owner>(owner);
            _context.Owners.Add(owners);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPut]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Update(int id, OwnerDTO owner)
        {
            var entity = _context.Owners.Where(v => v.OwnerId== id).FirstOrDefault();
            if (entity == null)
                return BadRequest("City with Id = " + id + " not found");
            entity.OwnerName = owner.OwnerName;
            entity.OwnerEmail= owner.OwnerEmail;
            entity.OwnerTelephone = owner.OwnerTelephone;
            _context.Owners.Update(entity);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = _context.Owners.Where(v => v.OwnerId == id).FirstOrDefault();
            if (entity == null)
                return BadRequest("City with Id = " + id + " not found");
            _context.Owners.Remove(entity);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        [Route("GetAll")]
        public ActionResult GetAll()
        {
            return Ok(_context.Owners.ToList());
        }
    }
}

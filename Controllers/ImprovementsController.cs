using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Database;
using WebApi.DTO;
using WebApi.Model;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImprovementsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ImprovementsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("AddImprovments")]
        public async Task<IActionResult> Create(ImprovementDTO improvement)
        {
            var improvements = _mapper.Map<Improvement>(improvement);
            _context.Add(improvements);
            await _context.SaveChangesAsync();
           return Ok();
        }
        [HttpPut]
        [Route("EditImprovments")]
        public async Task<IActionResult> Edit(int id, Improvement improvement)
        {
            if (id != improvement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(improvement);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImprovementExists(improvement.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return BadRequest();
        }
        [HttpDelete]
        [Route("DeleteImprovments")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var improvement = await _context.Improvements.FindAsync(id);
            _context.Improvements.Remove(improvement);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        [Route("GetAllImprovments")]
        public IActionResult GetAllImprovments()
        {
            return Ok(_context.Improvements.ToList());
        }

        [HttpGet]
        [Route("GetImprovementById")]
        public IActionResult GetCompanyById(int id)
        {
            var improvement = _context.Improvements.FirstOrDefault(v => v.Id == id);
            if (improvement == null)
            {
                return NotFound("Improvement with Id = " + id + " not found");
            }
            return Ok(improvement);
        }

        private bool ImprovementExists(int id)
        {
            return _context.Improvements.Any(e => e.Id == id);
        }
    }
}

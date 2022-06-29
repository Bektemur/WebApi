using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Database;
using WebApi.DTO;
using WebApi.Model;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CompanyController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(CompanyDTO company)
        {
            if (company == null)
                return BadRequest();
            var companies = _mapper.Map<Company>(company);
            _context.Companies.Add(companies);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPut]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Update(int id, CompanyDTO company)
        {
            var entity = _context.Companies.Where(v => v.CompanyId == id).FirstOrDefault();
            if (entity == null)
                return BadRequest("Company with Id = " + id + " not found");
            entity.CompanyName = company.CompanyName;
            entity.Email = company.Email;
            entity.Telephone = company.Telephone;
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = _context.Companies.Where(v => v.CompanyId == id).FirstOrDefault();
            if (entity == null)
                return NotFound("Companies with Id = " + id + " not found");
            _context.Companies.Remove(entity);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        [Route("GetAllCompanies")]
        public IActionResult GetAllCompanies()
        {
            return Ok(_context.Companies.ToList());
        }
        [HttpGet]
        [Route("GetCompanyById")]
        public IActionResult GetCompanyById(int id)
        {
            var company = _context.Companies.FirstOrDefault(v => v.CompanyId == id);
            if (company == null)
            {
                return NotFound("Companies with Id = " + id + " not found");
            }
            return Ok(company);
        }
    }
}

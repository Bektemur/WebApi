using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Database;
using WebApi.Model;
using WebApi.ViewModel;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController: ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CityController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(CityDTO city)
        {
            if (city == null) 
                return BadRequest();
            var cities = _mapper.Map<City>(city);
            _context.Add(cities);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPut]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Update(int id, CityDTO city)
        {
            var entity = _context.City.Where(v=>v.Id == id).FirstOrDefault();
            if (entity == null)
                return BadRequest("City with Id = "+id+ " not found");
            entity.Name = city.Name;
            entity.ProvinceId = city.ProvinceId;
            _context.City.Update(entity);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = _context.City.Where(v => v.Id == id).FirstOrDefault();
            if (entity == null)
                return BadRequest("City with Id = " + id + " not found");
            _context.City.Remove(entity);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_context.City.ToList());
        }
        [HttpGet]
        [Route("Get/{id}")]
        public IActionResult GetById(int id)
        {
            var entity = _context.City.Where(v => v.Id == id).FirstOrDefault();
            if (entity == null)
                return BadRequest("City with Id = " + id + " not found");
            return  Ok(entity);
        }


    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Database;
using WebApi.Model;
using WebApi.Service.CityService;
using WebApi.ViewModel;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController: ControllerBase
    {
        private readonly ICityService _cityService;
        private readonly ApplicationDbContext _context;

        public CityController(ICityService cityService, ApplicationDbContext context)
        {
            _cityService = cityService;
            _context = context;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(CityDTO city)
        {
            if (city == null) 
                return BadRequest();
            await _cityService.AddCity(city);
            return Ok();
        }
        [HttpPut]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Update(int id, CityDTO city)
        {
            var entity = _context.City.Where(v=>v.Id == id).FirstOrDefault();
            if (entity == null)
                return BadRequest("City with Id = "+id+ " not found");
            await _cityService.UpdateCity(city);
            return Ok();
        }
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = _context.City.Where(v => v.Id == id).FirstOrDefault();
            if (entity == null)
                return BadRequest("City with Id = " + id + " not found");
            await _cityService.RemoveCity(id);
            return Ok();
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _cityService.GetCityList());
        }
        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = _context.City.Where(v => v.Id == id).FirstOrDefault();
            if (entity == null)
                return BadRequest("City with Id = " + id + " not found");
            return  Ok(await _cityService.GetById(id));
        }


    }
}

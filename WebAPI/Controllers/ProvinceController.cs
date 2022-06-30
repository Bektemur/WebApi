using Microsoft.AspNetCore.Mvc;
using WebApi.Database;
using WebApi.Service.ProvinceService;
using WebApi.ViewModel;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvinceController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IProvinceService _provinceService;
        public ProvinceController(ApplicationDbContext context, IProvinceService provinceService)
        {
            _context = context;
            _provinceService = provinceService;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(ProvinceDTO province)
        {
            if (province == null)
                return BadRequest();
            await _provinceService.AddProvince(province);
            return Ok();
        }
        [HttpPut]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Update(int id, ProvinceDTO province)
        {
            var entity = _context.Province.Where(v => v.ProvinceId == id).FirstOrDefault();
            if (entity == null)
                return BadRequest("Province with Id = " + id + " not found");
            await _provinceService.UpdateProvince(province);
            return Ok();
        }

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = _context.Province.Where(v => v.ProvinceId == id).FirstOrDefault();
            if (entity == null)
                return BadRequest("Province with Id = " + id + " not found");
            return Ok(await _provinceService.GetById(id));
        }
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = _context.Province.Where(v => v.ProvinceId == id).FirstOrDefault();
            if (entity == null)
                return BadRequest("Province with Id = " + id + " not found");
            await _provinceService.RemoveProvince(id);
            return Ok();
        }
    }
}

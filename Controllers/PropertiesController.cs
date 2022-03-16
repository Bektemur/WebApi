using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApi.Database;
using WebApi.Model;
using WebApi.ViewModel;

namespace WebApi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public PropertiesController(UserManager<ApplicationUser> userManager, IConfiguration configuration, ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _userManager = userManager;
            _configuration = configuration;
            _context = applicationDbContext;
            _mapper = mapper;
        }
        [HttpPost]
        [Route("AddProperty")]
        public async Task<IActionResult> AddProperties([FromBody] PropertyMainDTO model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            model.UpdateDate = DateTime.UtcNow;
            //model.ApplicationUserId = GetCurrentUserId();
            var newOne = _mapper.Map<Property>(model);
            await _context.Properties.AddAsync(newOne);
            await _context.SaveChangesAsync();
            return Ok(new Response { Status = "Success", Message = "Property Created Successfully" });
        }

        private string GetCurrentUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}

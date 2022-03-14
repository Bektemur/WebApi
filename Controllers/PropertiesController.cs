using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApi.Database;
using WebApi.Model;
using WebApi.ViewModel;

namespace WebApi.Controllers
{
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
        public async Task<IActionResult> AddProperties([FromBody] PropertyMainViewModel model)
        {
            
                model.UpdateDate = DateTime.UtcNow;
                var previous = await _context.Properties.AsNoTracking().FirstOrDefaultAsync(x => x.PropertyId == model.PropertyId);
                if (previous != null)
                {
                    _mapper.Map<PropertyMainViewModel, Property>(model, previous);
                    if (string.IsNullOrWhiteSpace(model.ApplicationUserId))
                    {
                        model.ApplicationUserId = GetCurrentUserId();
                    }
                    previous.UpdateDate = DateTime.UtcNow;
                    _context.Properties.Update(previous);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var newOne = _mapper.Map<Property>(model);
                    await _context.Properties.AddAsync(newOne);
                    await _context.SaveChangesAsync();
                    model.PropertyId = newOne.PropertyId;
                }
                return Ok(new Response { Status = "Success", Message = "User Created Successfully" });
        }

        private string GetCurrentUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}

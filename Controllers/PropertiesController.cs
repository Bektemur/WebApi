using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Model;

namespace WebApi.Controllers
{
    public class PropertiesController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        public PropertiesController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
    }
}

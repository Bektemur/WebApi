using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Authentication;
using WebApi.Database;
using WebApi.Interface;
using WebApi.Model;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMailService _mailService;
        private readonly ApplicationDbContext _context;
        public AuthenticationController (UserManager<ApplicationUser> userManager, IConfiguration configuration, IMailService mailService, ApplicationDbContext context)
        {
            _userManager = userManager;
            _configuration = configuration;
            _mailService = mailService;
            _context = context;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            var userExist = await _userManager.FindByNameAsync(registerModel.UserName);
            if (userExist != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User with this username already exists, please select another username" });
            ApplicationUser user = new ApplicationUser()
            {
                Email = registerModel.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerModel.UserName,
                PasswordHash = registerModel.Password,
                PhoneNumber = registerModel.Phone
            };
            var result = await _userManager.CreateAsync(user, registerModel.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = result.Errors.FirstOrDefault().Description });
            return Ok(new Response { Status = "Success", Message = "User Created Successfully"});
        }

        [HttpPost]
        [Route("Login")]
        public async Task <IActionResult> Login ([FromBody] Login login)
        {
            var user = await _userManager.FindByNameAsync(login.Username);
            var checkPassword = await _userManager.CheckPasswordAsync(user, login.Password);
            if (user != null && checkPassword)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(6),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    User = user.UserName,
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPassword model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Email not found" });
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, model.Password);
            var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = token }, protocol: HttpContext.Request.Scheme);
            MailRequest message = new MailRequest();
            message.Body = callbackUrl +" ";
            message.ToEmail = model.Email;
            message.Subject = "test";
            await _mailService.SendEmailAsync(message);
            //await emailService.SendEmailAsync(model.Email, "Reset Password",
            // $"Для сброса пароля пройдите по ссылке: <a href='{callbackUrl}'>link</a>");
            if (result.Succeeded)
            {
                return Ok(new Response { Status = "Success", Message = "Reset Password Confirmation" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = result.Errors.FirstOrDefault().Code });
            }
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPassword forgotPassword)
        {
            var user = await _userManager.FindByEmailAsync(forgotPassword.Email);
            if (user == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Email not found" });
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callback = Url.Action(nameof(ResetPassword), "Account", new { token, email = user.Email }, Request.Scheme);
            var message = new MailRequest()
            {
                ToEmail = user.Email,
                Body = callback,
                Subject = "Reset password token",
            };
            await _mailService.SendEmailAsync(message);
            return Ok(new Response { Status = "Success", Message = "Reset password success" });
        }
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        [Route("GetAllUser")]
        public IActionResult GetAllUser()
        {
            return Ok(_context.Users.ToList());
        }
    }
}

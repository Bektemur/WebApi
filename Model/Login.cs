using System.ComponentModel.DataAnnotations;

namespace WebApi
{
    public class Login
    {
        [Required(ErrorMessage = "UserName is required")]
        public string Username { get; set; }
        [Required(ErrorMessage ="Password is required")]
        public string Password { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace WebApi.Authentication
{
    public class RegisterModel
    {
        [Required (ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }
        public string Code { get; set; }
    }
}

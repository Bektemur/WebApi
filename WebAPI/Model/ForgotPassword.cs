using System.ComponentModel.DataAnnotations;

namespace WebApi.Model
{
    public class ForgotPassword
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}

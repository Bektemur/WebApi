using System.ComponentModel.DataAnnotations;
using WebApi.Model;

namespace WebApi.DTO
{
    public class ImprovementDTO
    {
        public ImprovementType Type { get; set; }
        [Required]
        public string Name { get; set; }
    }
}

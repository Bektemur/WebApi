using System.ComponentModel.DataAnnotations;

namespace WebApi.Model
{
    public class MultipleFilesModel
    {
        [Required(ErrorMessage = "Please select files")]
        public IFormFile[] Files { get; set; }
    }
}

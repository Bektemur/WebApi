using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Model
{
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public virtual List<Property> Properties { get; set; }

    }
}

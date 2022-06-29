using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Model
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ProvinceId { get; set; }
        [ForeignKey("ProvinceId")]
        public Province Province { get; set; }
        public ICollection<Property> Properties { get; set; }

    }
}

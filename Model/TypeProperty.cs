using System.ComponentModel.DataAnnotations;

namespace WebApi.Model
{
    public class TypeProperty
    {
        [Key]
        public int TypePropertyId { get; set; }
        public string Name { get; set; }
        public List<Property> Properties { get; set; }

    }
}

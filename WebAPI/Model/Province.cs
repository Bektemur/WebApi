using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Model
{
    public class Province
    {
        [Key]
        public int ProvinceId { get; set; }
        public string Name { get; set; }
        [ForeignKey("StationId")]
        public int? StationId { get; set; }
        public Station Station { get; set; }
        public ICollection<City> Cities { get; set; }

    }
}

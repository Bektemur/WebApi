using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Model
{
    public class Station
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StationId { get; set; }
        public string Name { get; set; }
        public StationType StationType { get; set; }
        //public string PlaceName { get; set; }
        //public string GeoLong { get; set; }
        //public string GeoLat { get; set; }
    }
    public enum StationType
    {
        [Display(Name = "BTS Silom")]
        BTSSilom,
        [Display(Name = "BTS Sum")]
        BTSSuk,
        [Display(Name = "MRT")]
        MRT,
    }

}

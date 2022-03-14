using System.ComponentModel.DataAnnotations;

namespace WebApi.ViewModel
{
    public class StationViewModel
    {
        public string Name { get; set; }
        public StationType StationType { get; set; }
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

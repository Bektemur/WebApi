using System.ComponentModel.DataAnnotations;
using WebApi.Model;

namespace WebApi.ViewModel
{
    public class PropertyMainDTO
    {
        public int PropertyId { get; set; }
        public string ExternalPropertyId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public PublishingState PublishingState { get; set; }
        public string Description { get; set; }
        public string DescriptionThai { get; set; }
        public bool ForRent { get; set; }
        public bool ForSale { get; set; }
        [Required]
        public double PriceSale { get; set; } = 0;
        [Required]
        public double PriceRent { get; set; } = 0;
        public double PriceSaleSqm { get; set; } = 0;

        public double LivingArea { get; set; }
        public double LandArea { get; set; }
        public int Bathrooms { get; set; } = 0;
        [Required]
        public int Bedrooms { get; set; } = 0;
        public int Parking { get; set; } = 0;
        public string Note { get; set; }

        public DateTime PublicDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string ApplicationUserId { get; set; }

        public int TypePropertyId { get; set; }
        public int? ProjectId { get; set; }
        public int? StationId { get; set; }
        public int? CityId { get; set; }
        public int? OwnerId { get; set; }
        public int? CompanyId { get; set; }
    }
}

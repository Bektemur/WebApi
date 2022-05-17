using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Model
{
    public class Property
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PropertyId { get; set; }
        public string ExternalPropertyId { get; set; }
        public string Name { get; set; }
        public PublishingState PublishingState { get; set; }
        public string Description { get; set; }
        public string DescriptionThai { get; set; }
        public bool ForRent { get; set; }
        public bool ForSale { get; set; }
        public double PriceSale { get; set; } = 0;
        public double PriceSaleSqm { get; set; } = 0;
        public double PriceRent { get; set; } = 0;

        public double LivingArea { get; set; }
        public double LandArea { get; set; }
        public int Bathrooms { get; set; } = 0;
        public int Bedrooms { get; set; } = 0;
        public int Parking { get; set; } = 0;

        public DateTime PublicDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string ApplicationUserId { get; set; }

        public int TypePropertyId { get; set; }
        public int? ProjectId { get; set; }
        public int? StationId { get; set; }
        public int? CityId { get; set; }
        public int? OwnerId { get; set; }
        public int? CompanyId { get; set; }
        public string HouseNumber { get; set; }
        public string Street { get; set; }
        public string SubDistrict { get; set; }
        public string District { get; set; }
        public string ZipCode { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Note { get; set; }

        [ForeignKey("TypePropertyId")]
        public virtual TypeProperty TypeProperties { get; set; }
        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }
        [ForeignKey("StationId")]
        public virtual Station Station { get; set; }
        [ForeignKey("CityId")]
        public virtual City City { get; set; }
        [ForeignKey("OwnerId")]
        public virtual Owner Owner { get; set; }
        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<FileOnFileSystemModel> FileSystemModels { get; set; }
        public virtual ICollection<ImprovementToProperty> Improvements { get; set; }
    }
    public enum PublishingState
    {
        Draft = 0,
        Published = 1,
        Booked = 2,
        Rented = 3,
        Sold = 4,
        Disabled = 5
    }

}

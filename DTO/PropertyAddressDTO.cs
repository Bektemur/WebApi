namespace WebApi.ViewModel
{
    public class PropertyAddressDTO
    {
        public int PropertyId { get; set; }
        public string HouseNumber { get; set; }
        public string Street { get; set; }
        public string SubDistrict { get; set; }
        public string District { get; set; }
        public string ZipCode { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}

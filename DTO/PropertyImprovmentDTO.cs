namespace WebApi.ViewModel
{
    public class PropertyImprovmentDTO
    {
        public int PropertyId { get; set; }
        public List<int> ImprovmentIds { get; set; } = new List<int>();
    }
}

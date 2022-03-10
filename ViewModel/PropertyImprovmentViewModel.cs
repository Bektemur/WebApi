namespace WebApi.ViewModel
{
    public class PropertyImprovmentViewModel
    {
        public int PropertyId { get; set; }
        public List<int> ImprovmentIds { get; set; } = new List<int>();
    }
}

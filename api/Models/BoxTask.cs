namespace api.Models
{
    public class BoxTask
    {
        //[JsonIgnore]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Complete { get; set; }
        public string Address { get; set; }
    }
}
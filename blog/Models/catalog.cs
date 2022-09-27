namespace blog.Models
{
    public class catalog
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public ICollection<news> news { get; set; }
    }
}

namespace EatvardAPI.Models
{
    public class Restaurant
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Address { get; set; }

        public string? City { get; set; }
    }
}

using SF.Models;

namespace SF.Model
{
    public class Home : BaseEntity
    {
        public string? Address { get; set; }
        public decimal Price { get; set; }
        public int ZipCode { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Comments { get; set; }
        public string? ImageUrl { get; set; }

    }
}

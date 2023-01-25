using Bazart.Models.Model;

namespace Bazart.Models.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public List<Product> Products { get; set; }
    }
}
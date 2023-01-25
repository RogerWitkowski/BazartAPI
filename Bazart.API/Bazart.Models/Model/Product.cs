using Bazart.Models.Model;

namespace Bazart.Models.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsForSell { get; set; }
        public string ImageUrl { get; set; }
        public string CreatedById { get; set; }
        public User CreatedBy { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
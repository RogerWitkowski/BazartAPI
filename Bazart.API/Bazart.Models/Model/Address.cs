using Bazart.Models.Model;

namespace Bazart.Models.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int HouseOrFlatNumber { get; set; }
        public string PostalCode { get; set; }
        public User User { get; set; }
    }
}
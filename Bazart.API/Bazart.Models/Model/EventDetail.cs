using Bazart.Models.Model;

namespace Bazart.Models.Models
{
    public class EventDetail
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int HouseOrFlatNumber { get; set; }
        public string PostalCode { get; set; }
        public string ImageUrl { get; set; }
        public Event Event { get; set; }
        public int EventId { get; set; }
    }
}
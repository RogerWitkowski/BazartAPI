using Microsoft.AspNetCore.Identity;

namespace Bazart.Models.Model
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Nationality { get; set; }

        public IEnumerable<Product> Products { get; set; }

        public IEnumerable<Event> Events { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
    }
}
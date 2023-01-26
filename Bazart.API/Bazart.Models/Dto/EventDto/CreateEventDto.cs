using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazart.Models.Dto.EventDto
{
    public class CreateEventDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int HouseOrFlatNumber { get; set; }
        public string PostalCode { get; set; }
        public string ImageUrl { get; set; }
        public int CreatedById { get; set; }
    }
}
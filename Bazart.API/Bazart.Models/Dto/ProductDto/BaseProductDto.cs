using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazart.Models.Dto.ProductDto
{
    public abstract class BaseProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsForSell { get; set; }
        public string ImageUrl { get; set; }
        public string CategoryName { get; set; }
    }
}
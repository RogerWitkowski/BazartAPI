using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazart.Models.Dto.ProductDto
{
    public class ProductDto : BaseProductDto
    {
        public int Id { get; set; }

        public string CreatedBy { get; set; }
    }
}
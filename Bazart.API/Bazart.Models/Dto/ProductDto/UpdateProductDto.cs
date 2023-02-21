using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazart.Models.Dto.ProductDto
{
    public class UpdateProductDto : BaseProductDto
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }
    }
}
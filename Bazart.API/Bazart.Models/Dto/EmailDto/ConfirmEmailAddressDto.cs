using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazart.Models.Dto.EmailDto
{
    public class ConfirmEmailAddressDto
    {
        public string UserId { get; set; }
        public string Token { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazart.Models.Dto.EventDto
{
    public class CreateEventDto : BaseEventDto
    {
        public int CreatedById { get; set; }
    }
}
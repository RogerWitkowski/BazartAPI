using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazart.Models.Dto.EventDto
{
    public class EventDto : BaseEventDto
    {
        public int Id { get; set; }

        public string CreatedBy { get; set; }
    }
}
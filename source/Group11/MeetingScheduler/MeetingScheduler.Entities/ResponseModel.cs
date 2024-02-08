using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingScheduler.Entities
{
    public class ResponseModel
    {
        public bool? Status { get; set; }
        public string Role { get; set; }
        public string EmailId { get; set; }
        public string? Message { get; set; }

    }
}

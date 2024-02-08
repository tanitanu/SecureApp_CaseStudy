using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingScheduler.Entities
{
    public class UserManagerResponse
    {
        public string Message { get; set; } = string.Empty; 
        public bool IsSuccess { get; set; }
        //public IEnumerable<string> Errors { get; set; } = new List<string>();
        public DateTime? ExpireDate { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }
    }
}

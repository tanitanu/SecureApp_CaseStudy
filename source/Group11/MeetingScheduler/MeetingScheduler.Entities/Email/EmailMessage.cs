using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingScheduler.Entities.Email
{
    public class EmailMessage
    {
        public string Subject { get; set; } 

        public string Body { get; set; }

       public string To { get; set; }

        public EmailMessage(string to, string subject ,string body) 
        {
            To = to;
            Subject = subject;  
            Body = body;
        }   
    }
}

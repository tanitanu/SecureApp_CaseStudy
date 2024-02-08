/*
* Created By: Prabu Elavarasan
*/
using System.Security.Principal;

namespace DXC.BlogConnect.WebAPI.Models.Domain
{
    public class Subscriber
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }
    }
}

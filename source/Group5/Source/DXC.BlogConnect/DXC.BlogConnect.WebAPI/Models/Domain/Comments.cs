/*
* Created By: Prabu Elavarasan
*/
namespace DXC.BlogConnect.WebAPI.Models.Domain
{
    public class Comments:BaseDomain
    {
        public int Id { get; set; }
        public int ComUserId { get; set; }
        public int PostId { get; set; }
        public string Comment { get; set; }
    }
}

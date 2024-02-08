using System.Security.Principal;
/*
* Created By: Prabu Elavarasan
*/
namespace DXC.BlogConnect.WebAPI.Models.Domain
{
    public class Likes:BaseDomain
    {
        public int Id { get; set; }
        public int LikedUserId { get; set; }
        public int PostId { get; set; }

    }
}

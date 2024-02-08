/*
* Created By: Prabu Elavarasan
*/
namespace DXC.BlogConnect.WebAPI.Models.Domain
{
    public class Post:BaseDomain
    {
        public int Id { get; set; } 
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ThumbnailUrl { get; set; }
        public string ShortDescription { get; set; }
        public DateTime PublishedDate { get; set; }

        public ICollection<Likes> Likes { get; set; }
        public ICollection<Comments> Comments { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace DXC.BlogConnect.WebAPI.Models.DTO
{
    /*
    Created by Prabu Elavarasan
    */
    public class AddBlogPostDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string ThumbnailUrl { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        [Required]
        public DateTime PublishedDate { get; set; } = DateTime.Now;
    }
}

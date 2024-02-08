using System.ComponentModel.DataAnnotations;

namespace DXC.BlogConnect.WebAPI.Models.DTO
{
    /*
    Created by Prabu Elavarasan
    */
    public class BlogPostAddDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string ThumbnailUrl { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        
        public DateTime? PublishedDate { get; set; } 

        public string UserName { get; set; } = null!;
    }
}

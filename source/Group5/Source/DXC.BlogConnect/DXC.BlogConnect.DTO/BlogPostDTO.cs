using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXC.BlogConnect.DTO
{
    public class BlogPostDTO
    {
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

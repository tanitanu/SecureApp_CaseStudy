using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DiscussionForumUserInterface.Models
{
    public class CreateUpdateDiscussionModel
    {
        [ValidateNever]
        public string? QuestionId { get; set; } 
        [Required(ErrorMessage = "Title is required")]
        [DataType(DataType.Text)]
        public string Title { get; set; } = null!;
        [Required(ErrorMessage = "Content is required")]
        [DataType(DataType.Html)]
        public string? Content { get; set; }
        [Required(ErrorMessage = "Category is required")]
        public string CategoryId { get; set; } = null!;
       
        public string? Status { get; set; }

    }
    public class Catgory
    {
        public string? CategoryId { get; set; }

        public string? Category { get; set; }
    }


}

namespace DXC.BlogConnect.WebAPI.Models.DTO
{
    /*
    Created by Prabu Elavarasan
    */
    public class AddBlogCommentDTO
    {
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public int UserId { get; set; }
    }
}

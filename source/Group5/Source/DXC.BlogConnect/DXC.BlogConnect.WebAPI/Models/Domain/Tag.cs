namespace DXC.BlogConnect.WebAPI.Models.Domain
{
    /*Created by Prabu Elavarasan*/
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BlogPostId { get; set; }
    }
}

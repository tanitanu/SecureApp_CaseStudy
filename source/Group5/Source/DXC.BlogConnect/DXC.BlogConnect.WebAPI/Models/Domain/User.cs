namespace DXC.BlogConnect.WebAPI.Models.Domain
{
    /*
 * Created By: Kishore
 */
    public class User : BaseDomain
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
}

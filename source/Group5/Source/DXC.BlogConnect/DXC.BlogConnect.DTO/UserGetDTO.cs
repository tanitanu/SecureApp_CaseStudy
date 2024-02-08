namespace DXC.BlogConnect.DTO
{
    /*
* Created By: Kishore
*/
    public class UserGetDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
        public string EmailId { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;
    }
}

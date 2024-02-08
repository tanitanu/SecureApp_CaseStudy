using System.ComponentModel.DataAnnotations;

namespace BlazorCARS.HealthShield.WebApp.Model.DTO
{
    public class UserDTO
    {
        [Required(ErrorMessage = "UserName is required")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}

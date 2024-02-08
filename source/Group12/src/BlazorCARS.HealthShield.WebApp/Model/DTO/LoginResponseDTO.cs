namespace BlazorCARS.HealthShield.WebApp.Model.DTO
{
    public class LoginResponseDTO
    {
            public string UserName { get; set; }
            public string UserRole { get; set; }
            public int? DiscriminationId { get; set; }
            public string Token { get; set; }
        
    }
}

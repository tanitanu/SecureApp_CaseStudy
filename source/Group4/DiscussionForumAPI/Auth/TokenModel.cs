namespace DiscussionForumAPI.Auth
{
    /// <summary Author = Kirti Garg>
    /// We can create a class “TokenModel” which will be used to pass access token and refresh token into the refresh 
    /// method of the authenticate controller.
    /// </summary>
    public class TokenModel
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}

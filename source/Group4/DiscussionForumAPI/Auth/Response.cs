namespace DiscussionForumAPI.Auth
{
    /// <summary Author = Kirti Garg>
    /// We can create a class “Response” for returning the response value after user registration and user login.
    /// It will also return error messages if the request fails.
    /// </summary>
    public class Response
    {
        public string? Status { get; set; }
        public string? Message { get; set; }
    }
}

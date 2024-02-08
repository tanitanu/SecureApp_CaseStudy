using DiscussionForumAPI.Helper;

namespace DiscussionForumAPI.Contracts
{
    /// <summary Author = Kirti Garg>
    /// This is interface that contains all the methods related to mail.
    /// </summary>
    public interface IMail
    {
        bool SendMailAsync(MailData mailData);
    }
}

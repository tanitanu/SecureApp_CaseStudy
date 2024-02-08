namespace DiscussionForumUserInterface.Models
{
    public class UsersReportedModel
    {
        public string ReporterUserId { get; set; }
        public string RespondentUserId { get; set; }
        public string ReporterUserEmail { get; set; }
        public string ReporterUserName { get; set; }
        public string RespondentUserEmail { get; set; }

        public string RespondentUserName { get; set; }
        public string Title { get; set; }

        public bool IsDelete { get; set; }
    }
}

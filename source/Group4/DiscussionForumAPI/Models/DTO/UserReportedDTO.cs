namespace DiscussionForumAPI.Models.DTO
{
    public class UserReportedDTO
    {
        public string ReporterUserId { get; set; }
        public string RespondentUserId { get; set; }
        public string ReporterUserEmail { get; set; }
        public string ReporterUserName { get; set; }
        public string RespondentUserEmail { get; set; }

        public string RespondentUserName { get; set; }

        public bool IsDelete { get; set; }

        public string Title { get; set; }



    }
}

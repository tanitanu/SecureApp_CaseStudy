using System;
using System.Collections.Generic;

namespace DiscussionForumAPI.Models;

public partial class DiscussionForumUserReported
{
    public int Id { get; set; }

    public string ReportId { get; set; } = null!;

    public string QuestionId { get; set; } = null!;

    public string ReporterUserId { get; set; } = null!;

    public string RespondentUserId { get; set; } = null!;

    public bool? IsDelete { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual AspNetUser? CreatedByNavigation { get; set; }

    public virtual DiscussionForumQuestion Question { get; set; } = null!;

    public virtual AspNetUser ReporterUser { get; set; } = null!;

    public virtual AspNetUser RespondentUser { get; set; } = null!;

    public virtual AspNetUser? UpdatedByNavigation { get; set; }
}

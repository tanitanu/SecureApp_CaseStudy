using System;
using System.Collections.Generic;

namespace DiscussionForumAPI.Models;

public partial class DiscussionForumQuestion
{
    public int Id { get; set; }

    public string QuestionId { get; set; } = null!;

    public string CategoryId { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? Content { get; set; }

    public bool? IsDelete { get; set; }

    public string? Status { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual ICollection<DiscussionForumAnswer> DiscussionForumAnswers { get; set; } = new List<DiscussionForumAnswer>();

    public virtual ICollection<DiscussionForumLikesDislike> DiscussionForumLikesDislikes { get; set; } = new List<DiscussionForumLikesDislike>();

    public virtual ICollection<DiscussionForumUserReported> DiscussionForumUserReporteds { get; set; } = new List<DiscussionForumUserReported>();
}

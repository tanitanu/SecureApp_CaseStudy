using System;
using System.Collections.Generic;

namespace DiscussionForumAPI.Models;

public partial class DiscussionForumLikesDislike
{
    public int Id { get; set; }

    public string LikeDislikeId { get; set; } = null!;

    public string QuestionId { get; set; } = null!;

    public bool? Like { get; set; }

    public bool? Dislike { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual AspNetUser? CreatedByNavigation { get; set; }

    public virtual DiscussionForumQuestion Question { get; set; } = null!;

    public virtual AspNetUser? UpdatedByNavigation { get; set; }
}

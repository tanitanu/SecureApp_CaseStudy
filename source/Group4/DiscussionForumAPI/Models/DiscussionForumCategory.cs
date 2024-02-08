using System;
using System.Collections.Generic;

namespace DiscussionForumAPI.Models;

public partial class DiscussionForumCategory
{
    public int Id { get; set; }

    public string CategoryId { get; set; } = null!;

    public string Category { get; set; } = null!;

    public bool? IsDeleted { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual AspNetUser? CreatedByNavigation { get; set; }

    public virtual AspNetUser? UpdatedByNavigation { get; set; }
}

using System;
using System.Collections.Generic;

namespace DiscussionForumAPI.Models;

public partial class DisussionForumUserTwoFactorCode
{
    public int Id { get; set; }

    public string UserTwoFactorCodeId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string? Code { get; set; }

    public DateTime? CreatedOn { get; set; }

    public virtual AspNetUser User { get; set; } = null!;
}

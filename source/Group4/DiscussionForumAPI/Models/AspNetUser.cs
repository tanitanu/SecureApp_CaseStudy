using System;
using System.Collections.Generic;

namespace DiscussionForumAPI.Models;

public partial class AspNetUser
{
    public string Id { get; set; } = null!;

    public string? RefreshToken { get; set; }

    public DateTime RefreshTokenExpiryTime { get; set; }

    public string? UserName { get; set; }

    public string? NormalizedUserName { get; set; }

    public string? Email { get; set; }

    public string? NormalizedEmail { get; set; }

    public bool EmailConfirmed { get; set; }

    public string? PasswordHash { get; set; }

    public bool? IsActive { get; set; }

    public string? SecurityStamp { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public string? PhoneNumber { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public DateTimeOffset? LockoutEnd { get; set; }

    public bool LockoutEnabled { get; set; }

    public int AccessFailedCount { get; set; }

    public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; } = new List<AspNetUserClaim>();

    public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; } = new List<AspNetUserLogin>();

    public virtual ICollection<AspNetUserToken> AspNetUserTokens { get; set; } = new List<AspNetUserToken>();

    public virtual ICollection<DiscussionForumAnswer> DiscussionForumAnswerCreatedByNavigations { get; set; } = new List<DiscussionForumAnswer>();

    public virtual ICollection<DiscussionForumAnswer> DiscussionForumAnswerUpdatedByNavigations { get; set; } = new List<DiscussionForumAnswer>();

    public virtual ICollection<DiscussionForumCategory> DiscussionForumCategoryCreatedByNavigations { get; set; } = new List<DiscussionForumCategory>();

    public virtual ICollection<DiscussionForumCategory> DiscussionForumCategoryUpdatedByNavigations { get; set; } = new List<DiscussionForumCategory>();

    public virtual ICollection<DiscussionForumLikesDislike> DiscussionForumLikesDislikeCreatedByNavigations { get; set; } = new List<DiscussionForumLikesDislike>();

    public virtual ICollection<DiscussionForumLikesDislike> DiscussionForumLikesDislikeUpdatedByNavigations { get; set; } = new List<DiscussionForumLikesDislike>();

    public virtual ICollection<DiscussionForumUserReported> DiscussionForumUserReportedCreatedByNavigations { get; set; } = new List<DiscussionForumUserReported>();

    public virtual ICollection<DiscussionForumUserReported> DiscussionForumUserReportedReporterUsers { get; set; } = new List<DiscussionForumUserReported>();

    public virtual ICollection<DiscussionForumUserReported> DiscussionForumUserReportedRespondentUsers { get; set; } = new List<DiscussionForumUserReported>();

    public virtual ICollection<DiscussionForumUserReported> DiscussionForumUserReportedUpdatedByNavigations { get; set; } = new List<DiscussionForumUserReported>();

    public virtual ICollection<DisussionForumUserTwoFactorCode> DisussionForumUserTwoFactorCodes { get; set; } = new List<DisussionForumUserTwoFactorCode>();

    public virtual ICollection<AspNetRole> Roles { get; set; } = new List<AspNetRole>();
}

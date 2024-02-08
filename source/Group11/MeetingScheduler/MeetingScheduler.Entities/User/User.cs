using System;
using System.Collections.Generic;

namespace MeetingScheduler.Entities
{

    public partial class User
    {
        public int Id { get; set; }

        public string Email { get; set; } = null!;

        public byte[] PasswordHash { get; set; } = null!;

        public byte[] PasswordSalt { get; set; } = null!;

        public string? VerificationToken { get; set; }

        public string? ResetToken { get; set; }

        public DateTime? ResetTokenExpired { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? LastUpdtUserId { get; set; }

        public DateTime? LastUpdtTs { get; set; }

        public int? RoleId { get; set; }
    }
}

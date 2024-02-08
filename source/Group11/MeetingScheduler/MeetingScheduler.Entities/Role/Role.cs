using System;
using System.Collections.Generic;

namespace MeetingScheduler.Entities
{
    public partial class Role
    {
        public int? RoleId { get; set; }

        public string? RoleName { get; set; }

        public string? LastUpdtUserId { get; set; }

        public DateTime? LastUpdtTs { get; set; }

    }
}

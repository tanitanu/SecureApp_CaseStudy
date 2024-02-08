using System;
using System.Collections.Generic;

namespace MeetingScheduler.Entities
{

    public partial class Resource
    {
        public int ResourceId { get; set; }

        public int MeetingId { get; set; }

        public string? ResourceEmailId { get; set; }

        public string? LastUpdtId { get; set; }

        public DateTime? LastUpdtTs { get; set; }
    }
}

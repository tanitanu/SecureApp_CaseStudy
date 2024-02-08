using System;
using System.Collections.Generic;

namespace MeetingScheduler.Entities
{


    public partial class Meeting
    {
        public int MeetingId { get; set; }

        public string? Subject { get; set; }

        public string? Location { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public string? Description { get; set; }

        public bool? IsAllDay { get; set; }

        public string? RecurrenceRule { get; set; }

        public string? RecurrenceException { get; set; }

        public int? RecurrenceId { get; set; }

        public int MeetingCreatedUserId { get; set; }

        public string? LastUpdtId { get; set; }

        public DateTime? LastUpdtTs { get; set; }       
    }
}

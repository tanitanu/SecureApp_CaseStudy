using MeetingScheduler.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingScheduler.Model
{
    public interface IMeetingHandler
    {
        Task<IEnumerable<MeetingDTO>> GetAllMeetings();
        Task<UserManagerResponse> CreateMeeting(MeetingDTO createMeetingRequest);
        Task<UserManagerResponse> DeleteMeeting(int meetingId);
        Task<UserManagerResponse> UpdateMeeting(MeetingDTO meetingRequest);
        Task<IEnumerable<MeetingDTO>> GetUserMeetings(int userId);
    }
}

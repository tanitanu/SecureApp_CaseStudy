using MeetingScheduler.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingScheduler.DAL
{
    public interface IMeetingDAL
    {
        Task<IEnumerable<MeetingDTO>> GetAllMeetings();
        Task<UserManagerResponse> CreateMeeting(MeetingDTO createMeetingRequest);
        Task<UserManagerResponse> DeleteMeeting(int meetingId);
        Task<UserManagerResponse> UpdateMeeting(MeetingDTO meeting);
        Task<IEnumerable<MeetingDTO>> GetUserMeetings(int userId);

        void GetExpiredAndDeleteMeetings();
    }
}

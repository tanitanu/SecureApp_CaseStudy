using MeetingScheduler.Entities;

namespace MeetingScheduler.Data
{
    public interface IMeetingService
    {
        List<MeetingDTO?> GetAllMeetings();
        UserManagerResponse? CreateNewMeeting(MeetingDTO meetings);
        UserManagerResponse? EditMeeting(MeetingDTO meetings);

        List<MeetingDTO?> GetUserMeetings(int userId);
        UserManagerResponse? DeleteMeeting(int meetingId);
    }
}

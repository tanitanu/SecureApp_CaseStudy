using MeetingScheduler.Entities;

namespace MeetingScheduler.Data
{
    public interface IReportService
    {

      
        Task<List<MeetingDTO>> GetMonthlyMeetings(int userId, DateTime customMonth,string rolename);
        Task<List<MeetingDTO>> GetWeeklyMeetings(int userId, DateTime customStartDate, string rolename);

    }
}

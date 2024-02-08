using MeetingScheduler.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingScheduler.DAL
{
    public interface IReportsDAL
    {      
        Task<IEnumerable<MeetingDTO>> GetMonthlyMeetings(int userId, DateTime customMonth, string roleName);
        Task<IEnumerable<MeetingDTO>> GetWeeklyMeetings(int userId, DateTime customStartDate,string roleName);
    }
}

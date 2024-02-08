using MeetingScheduler.DAL;
using MeetingScheduler.DAL;
using MeetingScheduler.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingScheduler.Model
{
    public class ReportsHandler : BaseDAL, IReportsHandler
    {
        private readonly IReportsDAL reportsDAL; //Report dal object
        /// <summary>
        /// Reports Handler constructor
        /// </summary>
        public ReportsHandler()
        {
            reportsDAL = new ReportsDAL();
        }


        /// <summary>
        /// Get Monthly Meetings
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="customMonth"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public Task<IEnumerable<MeetingDTO>> GetMonthlyMeetings(int userId, DateTime customMonth, string roleName)
        {
            try
            {
                return reportsDAL.GetMonthlyMeetings(userId, customMonth,roleName);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// Get Weekly Meetings
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="customStartDate"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public Task<IEnumerable<MeetingDTO>> GetWeeklyMeetings(int userId, DateTime customStartDate, string roleName)
        {
            try
            {
                return reportsDAL.GetWeeklyMeetings(userId, customStartDate,roleName);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

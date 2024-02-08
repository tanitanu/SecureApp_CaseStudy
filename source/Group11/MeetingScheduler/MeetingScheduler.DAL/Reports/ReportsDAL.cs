using MeetingScheduler.DAL.Models;
using MeetingScheduler.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MeetingScheduler.DAL
{
    public class ReportsDAL : BaseDAL, IReportsDAL
    {
        public ReportsDAL() { }

        /// <summary>
        /// Get Weekly Meetings
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="customStartDate"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public async Task<IEnumerable<MeetingDTO>> GetWeeklyMeetings(int userId,
                                                                        DateTime customStartDate,string roleName)
        {
            try
            {
                using (var context = new MeetingSchedulerContext())
                {
                    // Normalize the customStartDate to include only the date part (without time).
                 DateTime startOfWeek = DateTime.Today.AddDays(
                     (int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek -
                                                             (int)DateTime.Today.DayOfWeek);

                    // Calculate the end date, which is 7 days (a week) after the customStartDate.
                    DateTime endDate = startOfWeek.AddDays(6);

                    var _meetings = await context.Meetings.ToListAsync();
                    var _resources = await context.Resources.ToListAsync();
                    var _users=await context.Users.ToListAsync();
                    if (roleName.ToLower() == "admin")
                    {
                        var _userMeetings = GetMeetingDetails(startOfWeek, endDate, _meetings, 
                                            _resources, _users);
                        return _userMeetings;
                    }
                    else
                    {
                        var _userMeetings = from _meeting in _meetings
                                            join _resource in _resources
                                                 on _meeting.MeetingId equals _resource.MeetingId
                                            join _user in _users
                                                 on _resource.ResourceEmailId equals _user.Email into userDetails
                                                    from userDetail in userDetails.DefaultIfEmpty()
                                            where (_meeting.MeetingCreatedUserId == userId
                                           && _meeting.StartTime!.Value.Date >= customStartDate && 
                                              _meeting.StartTime.Value.Date < endDate)
                                            select new MeetingDTO
                                            {
                                                MeetingId = _meeting.MeetingId,
                                                UserName = userDetail != null ? userDetail.FirstName + " "
                                                                  + userDetail.LastName : string.Empty,
                                                Location = _meeting.Location,
                                                Subject = _meeting.Subject,
                                                LastUpdatedUserId = _meeting.LastUpdtId!,
                                                StartTime = _meeting.StartTime,
                                                EndTime = _meeting.EndTime,
                                                Description = _meeting.Description,
                                                RecurrenceException = _meeting.RecurrenceException,
                                                RecurrenceId = _meeting.RecurrenceId,
                                                IsAllDay = _meeting.IsAllDay,
                                                RecurrenceRule = _meeting.RecurrenceRule,
                                                emailAddresses = _resource.ResourceEmailId
                                            };
                        return _userMeetings;
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get Meeting Details
        /// </summary>
        /// <param name="customStartDate"></param>
        /// <param name="endDate"></param>
        /// <param name="_meetings"></param>
        /// <param name="_resources"></param>
        /// <param name="_users"></param>
        /// <returns></returns>
        private IEnumerable<MeetingDTO> GetMeetingDetails(DateTime customStartDate, DateTime endDate, 
                            List<Meeting> _meetings, List<Resource> _resources,List<User> _users)
        {
            var _userMeetings = from _meeting in _meetings
                                join _resource in _resources
                                     on _meeting.MeetingId equals _resource.MeetingId
                                join _user in _users
                                     on _resource.ResourceEmailId equals _user.Email into userDetails
                                from userDetail in userDetails.DefaultIfEmpty()
                                where (_meeting.StartTime!.Value.Date >= customStartDate &&
                                  _meeting.StartTime.Value.Date <= endDate)
                                select new MeetingDTO
                                {
                                    MeetingId = _meeting.MeetingId,
                                    UserName = userDetail != null ? userDetail.FirstName + " "
                                                        + userDetail.LastName : string.Empty,
                                    Location = _meeting.Location,
                                    Subject = _meeting.Subject,
                                    LastUpdatedUserId = _meeting.LastUpdtId!,
                                    StartTime = _meeting.StartTime,
                                    EndTime = _meeting.EndTime,
                                    Description = _meeting.Description,
                                    RecurrenceException = _meeting.RecurrenceException,
                                    RecurrenceId = _meeting.RecurrenceId,
                                    IsAllDay = _meeting.IsAllDay,
                                    RecurrenceRule = _meeting.RecurrenceRule,
                                    emailAddresses = _resource.ResourceEmailId
                                };

            return _userMeetings;
        }

        /// <summary>
        /// Get Monthly Meetings
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="customMonth"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public async Task<IEnumerable<MeetingDTO>> GetMonthlyMeetings(int userId,
                                                                        DateTime customMonth, string roleName)
        {
            try
            {
                using (var context = new MeetingSchedulerContext())
                {                   

                    // Calculate the start and end of the custom month.
                    DateTime startOfMonth = new DateTime(customMonth.Year, customMonth.Month, 1);
                    DateTime endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);                 
                    var _meetings = await context.Meetings.ToListAsync();
                    var _resources = await context.Resources.ToListAsync();
                    var _users = await context.Users.ToListAsync();
                    if (roleName.ToLower() == "admin")
                    {
                        var _userMeetings = GetMeetingDetails(startOfMonth, endOfMonth, _meetings,
                                            _resources, _users);
                        return _userMeetings;
                    }
                    {
                        var _userMeetings = from _meeting in _meetings
                                            join _resource in _resources
                                                 on _meeting.MeetingId equals _resource.MeetingId
                                            join _user in _users
                                                 on _resource.ResourceEmailId equals _user.Email into userDetails
                                            from userDetail in userDetails.DefaultIfEmpty()
                                            where (_meeting.MeetingCreatedUserId == userId
                                           && _meeting.StartTime!.Value.Date >= startOfMonth &&
                                              _meeting.StartTime.Value.Date <= endOfMonth)
                                            select new MeetingDTO
                                            {
                                                MeetingId = _meeting.MeetingId,
                                                UserName = userDetail != null ? userDetail.FirstName + " "
                                                              + userDetail.LastName : string.Empty,
                                                Location = _meeting.Location,
                                                Subject = _meeting.Subject,
                                                LastUpdatedUserId = _meeting.LastUpdtId!,
                                                StartTime = _meeting.StartTime,
                                                EndTime = _meeting.EndTime,
                                                Description = _meeting.Description,
                                                RecurrenceException = _meeting.RecurrenceException,
                                                RecurrenceId = _meeting.RecurrenceId,
                                                IsAllDay = _meeting.IsAllDay,
                                                RecurrenceRule = _meeting.RecurrenceRule,
                                                emailAddresses = _resource.ResourceEmailId
                                            };

                        return _userMeetings;
                    }

                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}

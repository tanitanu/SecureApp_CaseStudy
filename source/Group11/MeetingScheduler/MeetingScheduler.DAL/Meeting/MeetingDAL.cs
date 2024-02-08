using MeetingScheduler.Common;
using MeetingScheduler.DAL.Models;
using MeetingScheduler.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MeetingScheduler.DAL
{
    public class MeetingDAL : IMeetingDAL
    {
        #region Messages
        private const string MEETING_CREATED_SUCCESS = "Meeting created Succesfully";
        private const string MEETING_CREATED_NOT_CREATED = "Meeting is not created";
        private const string UPDATE_MEETING_SUCCESS= "Meeting Details has been successfully updated";
        private const string UPDATE_MEETING_ERR = "Sorry, Something went wrong. Meeting is not Updated";
        private const string DELETE_MEETING_NOT_FOUND = "Meeting not found";
        private const string DELETE_MEETING_SUCCESS = "Meeting deleted successfully";
        private const string DELETE_MEETING_NOT_DELETED = "Meeting is not deleted";
        #endregion
        public MeetingDAL()
        {


        }

        /// <summary>
        /// Get all meetings
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<MeetingDTO>> GetAllMeetings()
        {
            try
            {
               
                using (var context = new MeetingSchedulerContext())
                { 
                    var _meetings = await context.Meetings.ToListAsync();
                    var _resources = await context.Resources.ToListAsync();
                    var _allMeetings = from _meeting in _meetings
                                       join _resource in _resources
                                            on _meeting.MeetingId equals _resource.MeetingId
                                       select new MeetingDTO
                                       {
                                           MeetingId = _meeting.MeetingId,
                                           Location = _meeting.Location,
                                           Subject = _meeting.Subject,
                                           MeetingCreatedUserId = _meeting.MeetingCreatedUserId,
                                           StartTime = _meeting.StartTime,
                                           EndTime = _meeting.EndTime,
                                           Description = _meeting.Description,
                                           RecurrenceException = _meeting.RecurrenceException,
                                           RecurrenceId = _meeting.RecurrenceId,
                                           IsAllDay = _meeting.IsAllDay,
                                           RecurrenceRule = _meeting.RecurrenceRule,
                                           emailAddresses = _resource.ResourceEmailId
                                       };

                    return _allMeetings;
                }
            }

            catch
            {
                throw;
            }

        }

        /// <summary>
        /// Get User Meetings by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<MeetingDTO>> GetUserMeetings(int userId)
        {
            try
            {
                using (var context = new MeetingSchedulerContext())
                {
                    var _meetings = await context.Meetings.ToListAsync();
                    var _resources = await context.Resources.ToListAsync();
                    var _userMeetings = from _meeting in _meetings
                                        join _resource in _resources
                                             on _meeting.MeetingId equals _resource.MeetingId
                                        where _meeting.MeetingCreatedUserId == userId
                                        select new MeetingDTO
                                        {
                                            MeetingId = _meeting.MeetingId,
                                            Location = _meeting.Location,
                                            Subject = _meeting.Subject,
                                            MeetingCreatedUserId = _meeting.MeetingCreatedUserId,
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

            catch
            {
                throw;
            }

        }
        /// <summary>
        /// Create new meeting
        /// </summary>
        /// <param name="createMeetingRequest"></param>
        /// <returns></returns>
        public async Task<UserManagerResponse> CreateMeeting(MeetingDTO createMeetingRequest)
        {
            try
            {
                using (var context = new MeetingSchedulerContext())
                {

                    CreateMeeting(createMeetingRequest, context);

                    var result = await context.SaveChangesAsync();

                    if (result > 0)
                    {
                        return new UserManagerResponse
                        {
                            Message = MEETING_CREATED_SUCCESS,
                            IsSuccess = true,
                        };
                    }

                    return new UserManagerResponse
                    {
                        Message = MEETING_CREATED_NOT_CREATED,
                        IsSuccess = false,
                    };
                }
            }
            catch (Exception ex)
            {
                return new UserManagerResponse
                {
                    Message = ex.Message,
                    IsSuccess = false,
                };
            }
        }

        /// <summary>
        /// Create new meeting
        /// </summary>
        /// <param name="createMeetingRequest"></param>
        /// <param name="context"></param>
        private static void CreateMeeting(MeetingDTO createMeetingRequest, MeetingSchedulerContext context)
        {
            try
            {
                if(createMeetingRequest != null)
                {
                    int meetingMaxId = context.Meetings.DefaultIfEmpty().Max(m => m == null ? 0 : m.MeetingId);


                    var meeting = new Meeting
                    {
                        MeetingId = meetingMaxId + 1,
                        Subject = createMeetingRequest.Subject,
                        Location = createMeetingRequest.Location,
                        StartTime = createMeetingRequest.StartTime,
                        EndTime = createMeetingRequest.EndTime,
                        Description = createMeetingRequest.Description,
                        IsAllDay = createMeetingRequest.IsAllDay,
                        RecurrenceRule = createMeetingRequest.RecurrenceRule,
                        RecurrenceException = createMeetingRequest.RecurrenceException,
                        MeetingCreatedUserId = createMeetingRequest.MeetingCreatedUserId,
                        LastUpdtId = createMeetingRequest.LastUpdatedUserId,
                        LastUpdtTs = DateTime.Now,
                    };

                    context.Meetings.Add(meeting);


                    int resourceId = 0;
                    List<string> emailIds = createMeetingRequest.emailAddresses!.Split(';').ToList();
                    emailIds = emailIds.Distinct().ToList();
                    Resource? _resource;
                    resourceId = context.Resources.DefaultIfEmpty().Max(r => r == null ? 0 : r.ResourceId);
                    foreach (string email in emailIds)
                    {
                        if (!string.IsNullOrWhiteSpace(email))
                        {
                            resourceId = resourceId + 1;
                            _resource = new Resource
                            {
                                ResourceId = resourceId,
                                MeetingId = meeting.MeetingId,
                                ResourceEmailId = email,
                                LastUpdtId = meeting.LastUpdtId,
                                LastUpdtTs = DateTime.Now
                            };
                            context.Resources.Add(_resource);
                        }
                    }

                    Helper.SendMeetingEmail(emailIds, createMeetingRequest);
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get expired meetings
        /// </summary>
        public void GetExpiredAndDeleteMeetings()
        {
            try
            {
                var context = new MeetingSchedulerContext();
                var _meetings = context.Meetings.ToList();
                var _resources = context.Resources.ToList();
                var expiredMeetings = from _meeting in _meetings
                                      join _resource in _resources
                                           on _meeting.MeetingId equals _resource.MeetingId
                                      where _meeting.EndTime < DateTime.Now
                                      select new MeetingDTO
                                      {
                                          MeetingId = _meeting.MeetingId,
                                          Location = _meeting.Location,
                                          Subject = _meeting.Subject,
                                          MeetingCreatedUserId = _meeting.MeetingCreatedUserId,
                                          StartTime = _meeting.StartTime,
                                          EndTime = _meeting.EndTime,
                                          Description = _meeting.Description,
                                          RecurrenceException = _meeting.RecurrenceException,
                                          RecurrenceId = _meeting.RecurrenceId,
                                          IsAllDay = _meeting.IsAllDay,
                                          RecurrenceRule = _meeting.RecurrenceRule,
                                          emailAddresses = _resource.ResourceEmailId
                                      };




                foreach (var item in expiredMeetings)
                {

                    var deleteMeeting = context.Meetings.FirstOrDefault(u => u.MeetingId == item.MeetingId);

                    var deleteResource = context.Resources.FirstOrDefault(r => r.MeetingId == item.MeetingId);

                    if (deleteMeeting != null && deleteResource != null)
                    {
                        context.Meetings.Remove(deleteMeeting);
                        context.Resources.Remove(deleteResource);
                    }


                }

                context.SaveChanges();

            }

            catch
            {
                throw;
            }

        }

        /// <summary>
        /// Update meeting
        /// </summary>
        /// <param name="meetingRequest"></param>
        /// <returns></returns>
        public async Task<UserManagerResponse> UpdateMeeting(MeetingDTO meetingRequest)
        {
            try
            {

                using (var context = new MeetingSchedulerContext())
                {
                    var meeting = await context.Meetings.FirstOrDefaultAsync(u => u.MeetingId == meetingRequest.MeetingId);
                    var resource = await context.Resources.FirstOrDefaultAsync(r => r.MeetingId == meetingRequest.MeetingId);

                    if(meeting!=null  && resource != null)
                    {
                        context.Meetings.Remove(meeting);
                        context.Resources.Remove(resource);
                    }
                    
                    var result = await context.SaveChangesAsync();

                    if (result > 0)
                    {
                        CreateMeeting(meetingRequest, context);

                        result = await context.SaveChangesAsync();

                        if (result > 0)
                        {
                            return new UserManagerResponse
                            {
                                Message = UPDATE_MEETING_SUCCESS,
                                IsSuccess = true,
                            };
                        }
                    }
                    return new UserManagerResponse
                    {
                        Message = UPDATE_MEETING_ERR,
                        IsSuccess = false,
                    };
                }
            }
            catch (Exception ex)
            {
                return new UserManagerResponse
                {
                    Message = ex.Message,
                    IsSuccess = false,
                };
            }

        }

        /// <summary>
        /// Delete meeting
        /// </summary>
        /// <param name="meetingId"></param>
        /// <returns></returns>
        public async Task<UserManagerResponse> DeleteMeeting(int meetingId)
        {
            try
            {
                using (var context = new MeetingSchedulerContext())
                {

                    var meeting = await context.Meetings.FirstOrDefaultAsync(u => u.MeetingId == meetingId);
                    var resource = await context.Resources.FirstOrDefaultAsync(r => r.MeetingId == meetingId);

                    var _resources = context.Resources.Where(r => r.MeetingId == meetingId);
                    List<string> emailids=new List<string>();
                    foreach(Resource _rsource in _resources)
                    {
                        if(!string.IsNullOrEmpty(_rsource.ResourceEmailId))
                        emailids.Add(_rsource.ResourceEmailId);
                    }
                                          

                    if (meeting != null && resource != null)
                    {
                        context.Meetings.Remove(meeting);
                        context.Resources.Remove(resource);
                    }

                    var result = await context.SaveChangesAsync();

                    if (meeting == null)
                    {
                        return new UserManagerResponse
                        {
                            Message = DELETE_MEETING_NOT_FOUND,
                            IsSuccess = false,

                        };
                    }

                    if (result > 0)
                    {
                        Helper.SendDeleteMeetingEmail(emailids, meeting);
                        return new UserManagerResponse
                        {
                            Message = DELETE_MEETING_SUCCESS,
                            IsSuccess = true,
                        }; 
                    }

                    return new UserManagerResponse
                    {
                        Message = DELETE_MEETING_NOT_DELETED,
                        IsSuccess = true,
                    };
                }
            }

            catch (Exception ex)
            {
                return new UserManagerResponse
                {
                    Message = ex.Message,
                    IsSuccess = false,
                };
            }
        }
    }
}

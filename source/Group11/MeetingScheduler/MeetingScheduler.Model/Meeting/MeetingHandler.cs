using MeetingScheduler.DAL;
using MeetingScheduler.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingScheduler.Model
{
    public class MeetingHandler:IMeetingHandler
    {
        private readonly IMeetingDAL _meetingDAl; //meeting dal object

        /// <summary>
        /// Meeting Handler constructor
        /// </summary>
        /// <param name="meetingDAL"></param>
        public MeetingHandler(IMeetingDAL meetingDAL)
        {
            _meetingDAl = meetingDAL;
        }

        /// <summary>
        /// Get All Meetings
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<MeetingDTO>> GetAllMeetings()
        {
            try
            {
                return await _meetingDAl.GetAllMeetings();
            }
            catch
            {

                throw ;
            }
        }

        /// <summary>
        /// Get User Meetings
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<MeetingDTO>> GetUserMeetings(int userId)
        {
            try
            {
                return await _meetingDAl.GetUserMeetings(userId);
            }
            catch
            {

                throw;
            }
        }

        /// <summary>
        /// Create Meeting
        /// </summary>
        /// <param name="createMeetingRequest"></param>
        /// <returns></returns>
        public async Task<UserManagerResponse> CreateMeeting(MeetingDTO createMeetingRequest)
        {
            try
            {
                return await _meetingDAl.CreateMeeting(createMeetingRequest);
            }
            catch
            {

                throw;
            }
        }
        /// <summary>
        /// Update Meeting
        /// </summary>
        /// <param name="meetingRequest"></param>
        /// <returns></returns>
        public async Task<UserManagerResponse> UpdateMeeting(MeetingDTO meetingRequest)
        {
            try
            {
                return await _meetingDAl.UpdateMeeting(meetingRequest);
            }
            catch
            {

                throw;
            }
        }

        /// <summary>
        /// Delete Meeting
        /// </summary>
        /// <param name="meetingId"></param>
        /// <returns></returns>
        public async Task<UserManagerResponse> DeleteMeeting(int meetingId)
        {
            try
            {
                return await _meetingDAl.DeleteMeeting(meetingId);
            }
            catch
            {

                throw;
            }
        }

    }

}


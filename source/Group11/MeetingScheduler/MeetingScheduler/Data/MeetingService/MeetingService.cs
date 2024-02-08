using MeetingScheduler.Common;
using MeetingScheduler.Entities;
using Newtonsoft.Json;
using System.Net;


namespace MeetingScheduler.Data
{
    public class MeetingService:IMeetingService
    {
        private readonly HttpClient _httpClient;// http client object to call web api methods
        private readonly ILogger<RoleService> _logger; //logger object

        /// <summary>
        /// Meeting Service constructor
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="logger"></param>
        public MeetingService(HttpClient httpClient, ILogger<RoleService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }
        /// <summary>
        /// Get All Meetings
        /// </summary>
        /// <returns></returns>
        public List<MeetingDTO?> GetAllMeetings()
        {
            List<MeetingDTO?> _meetingDTO=new List<MeetingDTO?>();
            _logger.LogInformation(Utility.GetStartMessage("GetAllMeetings"));
            try
            {               
                var response = _httpClient.GetAsync("/api/Meeting/GetAllMeetings").Result;
                if (response != null && response.StatusCode == HttpStatusCode.OK)
                {
                   var _meetings = JsonConvert.DeserializeObject<List<MeetingDTO>?>
                                                (response.Content.ReadAsStringAsync().Result);
                    if(_meetings != null)
                    {
                        var meetingIds = _meetings.GroupBy(m => m.MeetingId).Select(m => m.FirstOrDefault());
                        
                        foreach (MeetingDTO? meetingid in meetingIds)
                        {
                            if(meetingid != null)
                            {
                                var emailAddresses = _meetings.Where(m => m.MeetingId == meetingid.MeetingId).Select(m => m.emailAddresses);
                                string emailid = string.Empty;
                                foreach (string? email in emailAddresses)
                                {
                                    if (!string.IsNullOrWhiteSpace(email))
                                    {
                                        emailid = string.IsNullOrWhiteSpace(emailid) ? email : emailid + ';' + email;
                                    }
                                }
                                meetingid.emailAddresses = emailid;
                                _meetingDTO.Add(meetingid);
                            }
                            
                        }
                    }
                    
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(Utility.GetExceptionMessage("GetAllMeetings") + " \n" + ex.ToString());
            }
            _logger.LogInformation(Utility.GetEndMessage("GetAllMeetings"));
            return _meetingDTO;
        }

        /// <summary>
        /// Get User Meetings
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<MeetingDTO?> GetUserMeetings(int userId)
        {
            List<MeetingDTO?> _meetingDTO = new List<MeetingDTO?>();
            _logger.LogInformation(Utility.GetStartMessage("GetUserMeetings"));
            try
            {
                var response = _httpClient.GetAsync($"/api/Meeting/GetUserMeetings/{userId}").Result;
                if (response != null && response.StatusCode == HttpStatusCode.OK)
                {
                    var _meetings = JsonConvert.DeserializeObject<List<MeetingDTO>?>
                                                 (response.Content.ReadAsStringAsync().Result);
                    if (_meetings != null)
                    {
                        var meetingIds = _meetings.GroupBy(m => m.MeetingId).Select(m => m.FirstOrDefault());
                        foreach (MeetingDTO? meetingid in meetingIds)
                        {
                            if(meetingid != null)
                            {
                                var emailAddresses = _meetings.Where(m => m.MeetingId == meetingid.MeetingId).Select(m => m.emailAddresses);
                                string emailid = string.Empty;
                                foreach (string? email in emailAddresses)
                                {
                                    if (!string.IsNullOrWhiteSpace(email))
                                    {
                                        emailid = string.IsNullOrWhiteSpace(emailid) ? email : emailid + ';' + email;
                                    }
                                }
                                meetingid.emailAddresses = emailid;
                                _meetingDTO.Add(meetingid);
                            }
                            
                        }
                    }
                    
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(Utility.GetExceptionMessage("GetUserMeetings") + " \n" + ex.ToString());
            }
            _logger.LogInformation(Utility.GetEndMessage("GetUserMeetings"));
            return _meetingDTO;
        }

        /// <summary>
        /// Create New Meeting
        /// </summary>
        /// <param name="meetings"></param>
        /// <returns></returns>
        public UserManagerResponse? CreateNewMeeting(MeetingDTO meetings)
        {
            UserManagerResponse? createMeetingResponse = new UserManagerResponse();
            try
            {
                _logger.LogInformation(Utility.GetStartMessage("CreateNewMeeting"));
                var response = _httpClient.PostAsJsonAsync($"/api/Meeting/CreateMeeting", meetings).Result;
                if (response != null && response.StatusCode == HttpStatusCode.OK)
                {
                    createMeetingResponse = JsonConvert.DeserializeObject<UserManagerResponse?>
                                                (response.Content.ReadAsStringAsync().Result);
                }
                _logger.LogInformation(Utility.GetEndMessage("CreateNewMeeting"));
            }
            catch (Exception ex)
            {
                _logger.LogError(Utility.GetExceptionMessage("CreateNewMeeting") + " \n" + ex.ToString());
            }

            return createMeetingResponse;
        }

        /// <summary>
        /// Edit Meeting
        /// </summary>
        /// <param name="meetings"></param>
        /// <returns></returns>
        public UserManagerResponse? EditMeeting(MeetingDTO meetings)
        {
            UserManagerResponse? createMeetingResponse = new UserManagerResponse();
            try
            {
                _logger.LogInformation(Utility.GetStartMessage("EditMeeting"));
                var response =  _httpClient.PostAsJsonAsync($"/api/Meeting/UpdateMeetingDetails", meetings).Result;
                if (response != null && response.StatusCode == HttpStatusCode.OK)
                {
                    createMeetingResponse = JsonConvert.DeserializeObject<UserManagerResponse?>
                                                (response.Content.ReadAsStringAsync().Result);
                }
                _logger.LogInformation(Utility.GetEndMessage("EditMeeting"));
            }
            catch (Exception ex)
            {
                _logger.LogError(Utility.GetExceptionMessage("EditMeeting") + " \n" + ex.ToString());
            }

            return createMeetingResponse;
        }

        /// <summary>
        /// Delete Meeting
        /// </summary>
        /// <param name="meetingId"></param>
        /// <returns></returns>
        public UserManagerResponse? DeleteMeeting(int meetingId)
        {
            UserManagerResponse? createMeetingResponse = new UserManagerResponse();
            try
            {
                _logger.LogInformation(Utility.GetStartMessage("DeleteMeeting"));
                var response = _httpClient.DeleteAsync($"/api/Meeting/DeleteMeeting/" + meetingId).Result;
                if (response != null && response.StatusCode == HttpStatusCode.OK)
                {
                    createMeetingResponse = JsonConvert.DeserializeObject<UserManagerResponse?>
                                                (response.Content.ReadAsStringAsync().Result);
                }
                _logger.LogInformation(Utility.GetEndMessage("DeleteMeeting"));
            }
            catch (Exception ex)
            {
                _logger.LogError(Utility.GetExceptionMessage("DeleteMeeting") + " \n" + ex.ToString());
            }

            return createMeetingResponse;
        }

    }
}

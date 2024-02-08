namespace MeetingScheduler.Common
{
    public static class Constants
    {
        #region MeetingController
        public const string GETALLMEETINGS_START= "Started GetAllMeetings controller Get";
        public const string GETALLMEETINGS_END = "End GetAllMeetings controller Get";
        public const string GETALLMEETINGS_EXCEPTION="Exception in Get GetAllMeetings controller";

        public const string GETUSERMEETINGS_START = "Started GetUserMeetings controller Get";
        public const string GETUSERMEETINGS_END = "End GetUserMeetings controller Get";
        public const string GETUSERMEETINGS_EXCEPTION = "Exception in Get GetUserMeetings controller";

        public const string CREATEMEETING_START = "Started CreateMeeting controller Post";
        public const string CREATEMEETING_END = "End CreateMeeting controller Get";
        public const string CREATEMEETING_EXCEPTION = "Exception in Post CreateMeeting controller";

        public const string UPDATEMEETING_START = "Started UpdateMeetingDetails controller";
        public const string UPDATEMEETING_END = "End UpdateMeetingDetails controller";
        public const string UPADTEMEETING_EXCEPTION = "Exception in UpdateMeetingDetails controller";

        public const string DELETEMEETING_START = "Started DeleteMeeting controller";
        public const string DELETEMEETING_END = "End DeleteMeeting controller";
        public const string DELETEMEETING_EXCEPTION = "Exception in DeleteMeeting controller";

        #endregion

        #region ReportsController
        public const string GETWEEKLYMEETINGS_START = "Started Reports controller GetWeeklyMeetings";
        public const string GETWEEKLYMEETINGS_END = "End Reports controller GetWeeklyMeetings";
        public const string GETWEEKLYMEETINGS_EXCEPTION = "Exception in Reports controller GetWeeklyMeetings";

        public const string GETMONTHLY_START = "Started Reports controller GetMonthlyMeetings";
        public const string GETMONTHLY_END = "End Reports controller GetMonthlyMeetings";
        public const string GETMONTHLY_EXCEPTION = "Exception in Reports controller GetMonthlyMeetings";
        #endregion

        #region RoleController
        public const string GETROLE_START = "Started GetRole controller Post";
        public const string GETROLE_END = "End GetRole controller Get";
        public const string GETROLE_EXCEPTION = "Exception in Get role controller";

        public const string GETROLES_START = "Started GetRoles controller Post";
        public const string GETROLES_END = "End GetRoles controller Get";
        public const string GETROLES_EXCEPTION = "Exception in GetRoles controller";

        #endregion

        #region ResourceController
        public const string CREATERESOURCES_START = "Started CreateResources controller Post";
        public const string CREATERESOURCES_END = "End CreateResources controller Post";
        public const string CREATERESOURCES_EXCEPTION = "Exception in CreateResources controller";

        public const string GETRESOURCESBYMEETING_START = "Started GetResourcesByMeeting controller GET";
        public const string GETRESOURCESBYMEETING_END = "End GetResourcesByMeeting controller Get";
        public const string GETRESOURCESBYMEETING_EXCEPTION = "Exception in GetResourcesByMeeting controller";

        public const string UPDATERESOURCEDETAILS_START = "Started UpdateResourceDetails controller put";
        public const string UPDATERESOURCEDETAILS_END = "End UpdateResourceDetails controller put";
        public const string UPDATERESOURCEDETAILS_EXCEPTION = "Exception in UpdateResourceDetails controller";

        public const string GETRESOURCEDETAILS_START = "Started GetResourceDetails controller Get";
        public const string GETRESOURCEDETAILS_END = "End GetResourceDetails controller Get";
        public const string GETRESOURCEDETAILS_EXCEPTION = "Exception in GetResourceDetails controller Get";

        public const string DELETERESOURCE_START = "Started DeleteResource controller Delete";
        public const string DELETERESOURCE_END = "End DeleteResource controller Delete";
        public const string DELETERESOURCE_EXCEPTION = "Exception in DeleteResource controller Delete";


        #endregion

        #region UserController
        public const string REGISTER_START = "Started register controller Post";
        public const string REGISTER_END = "End register controller Post";
        public const string REGISTER_EXCEPTION = "Exception in  register controller Post";

        public const string LOGIN_START = "Started login controller Post";
        public const string LOGIN_END = "End login controller Post";
        public const string LOGIN_EXCEPTION = "Exception in  login controller Post";

        public const string FORGOT_START = "Started forgot password controller Post";
        public const string FORGOT_END = "End forgot password controller Post";
        public const string FORGOT_EXCEPTION = "Exception in forgot password controller Post";

        public const string RESET_START = "Started reset password controller Post";
        public const string RESET_END = "End reset password controller Post";
        public const string RESET_EXCEPTION = "Exception in   reset password controller Post";

        public const string GETUSERS_START = "Started GetUsers controller get";
        public const string GETUSERS_END = "End forgot GetUsers controller get";
        public const string GETUSERS_EXCEPTION = "Exception in   GetUsers  controller get";

        public const string UPDATEDROLE_START = "Started UpdatedRole controller Post";
        public const string UPDATEDROLE_END = "End UpdatedRole controller Post";
        public const string UPDATEDROLE_EXCEPTION = "Exception in   UpdatedRole controller Post";

        #endregion
        
    }
}
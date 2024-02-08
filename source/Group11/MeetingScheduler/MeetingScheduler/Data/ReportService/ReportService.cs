using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MeetingScheduler.Data;
using MeetingScheduler.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Win32;
using Newtonsoft.Json;
using Serilog;



public class ReportService : IReportService
{
    private readonly HttpClient _httpClient;// http client object to call web api methods
    private readonly ILogger<ReportService> _logger;// Logger object

    #region 
    private const string GETMONTHLYMEETINGS_FAILED = "Failed to retrieve GetMonthlyMeetings by userId.";
    private const string GETMONTHLYMEETINGS_ERR= "Error while retrieving GetMonthlyMeetings by userId";
    private const string GETWEEKLYMEETINGS_FAILED = "Failed to retrieve GetWeeklyMeetings by userId.";
    private const string GETWEEKLYMEETINGS_ERR = "Error while retrieving GetWeeklyMeetings by userId";
    #endregion
    /// <summary>
    /// Report Service constructor
    /// </summary>
    /// <param name="httpClient"></param>
    /// <param name="logger"></param>
    public ReportService(HttpClient httpClient, ILogger<ReportService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }



    /// <summary>
    /// Get Monthly Meetings
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="customMonth"></param>
    /// <param name="rolename"></param>
    /// <returns></returns>
    public async Task<List<MeetingDTO>> GetMonthlyMeetings(int userId, DateTime customMonth, string rolename)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/Reports/GetMonthlyMeetings?userId={userId}&customMonth={customMonth}&roleName={rolename}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var meetings = JsonConvert.DeserializeObject<List<MeetingDTO>>(content);
                if(meetings != null)
                {
                    return meetings;
                }
                return null!;
            }
            else
            {
                _logger.LogError(GETMONTHLYMEETINGS_FAILED);
                return new List<MeetingDTO>();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(GETMONTHLYMEETINGS_ERR);
            return new List<MeetingDTO>();
        }

    }

    /// <summary>
    /// Get Weekly Meetings
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="customStartDate"></param>
    /// <param name="rolename"></param>
    /// <returns></returns>
    public async Task<List<MeetingDTO>> GetWeeklyMeetings(int userId, DateTime customStartDate, string rolename)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/Reports/GetWeeklyMeetings?userId={userId}&customStartDate={customStartDate}&roleName={rolename}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var meetings = JsonConvert.DeserializeObject<List<MeetingDTO>>(content);
                if (meetings != null)
                {
                    return meetings;
                }
                return null!;
            }
            else
            {
                _logger.LogError(GETWEEKLYMEETINGS_FAILED);
                return new List<MeetingDTO>();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(GETWEEKLYMEETINGS_ERR);
            return new List<MeetingDTO>();
        }
    }

}

using DiscussionForumAPI.Models;
using DiscussionForumAPI.Models.DTO;

namespace DiscussionForumAPI.Contracts
{
    public interface IReport
    {
        Task<DiscussionForumReportDTO?> GetAsync(string status, string reportType, string fromDate, string? toDate);
    }
}

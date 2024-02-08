using DiscussionForumAPI.Contracts;
using DiscussionForumAPI.Models;
using DiscussionForumAPI.Models.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DiscussionForumAPI.Service
{
    public class ReportService : IReport
    {
        private readonly DiscussionForumContext _dbContext;
        public ReportService(DiscussionForumContext dbContext)
        {
             _dbContext = dbContext;
        }
        public async Task<DiscussionForumReportDTO?> GetAsync(string status, string reportType, string fromDate, string? toDate)
        {
            DiscussionForumReportDTO questionDetails = new DiscussionForumReportDTO();
            questionDetails.QuestionReportDetails = new List<QuestionReportDetails>();
            questionDetails.TopContributor = new List<TopContributor>();
            using (var con = (Microsoft.Data.SqlClient.SqlConnection)_dbContext.Database.GetDbConnection())
            {
                try
                {

                    con.Open();
                    SqlCommand cmd = new SqlCommand("[DiscussionForum].[usp_Reports]", con);
                    cmd.Parameters.AddWithValue("@OpCode", 100);
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@reportType", reportType);
                    cmd.Parameters.AddWithValue("@fromDate", fromDate);
                    cmd.Parameters.AddWithValue("@toDate", toDate);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        if (status != "Top")
                        {
                            QuestionReportDetails detail = new QuestionReportDetails
                            {
                                QuestionId = reader["Question Id"].ToString(),
                                Title = reader["Title"].ToString(),
                                CategoryName = reader["Category Name"].ToString(),
                                Status = reader["Status"].ToString(),
                                LikeCount = Convert.ToInt32(reader["Like Count"].ToString()),
                                DislikeCount = Convert.ToInt32(reader["Dislike Count"].ToString()),
                                CreatedBy = reader["Created By"].ToString(),
                                DateCreation = Convert.ToDateTime(reader["Date Creation"].ToString()),
                                CreatedByName = reader["User Name"].ToString(),
                                Delete = Convert.ToBoolean(reader["Delete"].ToString()),
                                Role = reader["Role"].ToString(),

                            };
                            questionDetails.QuestionReportDetails.Add(detail);
                        }
                        else
                        {
                            TopContributor topContributor = new TopContributor
                            {
                                UserName = reader["User Name"].ToString(),
                                AnswerCount = Convert.ToInt32(reader["Answer Count"].ToString()),
                            };
                            questionDetails.TopContributor.Add(topContributor);
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return questionDetails;
        }
    }
}

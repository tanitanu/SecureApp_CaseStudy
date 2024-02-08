using DiscussionForumAPI.Contracts;
using DiscussionForumAPI.Models;
using DiscussionForumAPI.Models.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DiscussionForumAPI.Service
{
    public class UserReportedService : IUserReported
    {
        private readonly DiscussionForumContext _dbContext;
        public UserReportedService(DiscussionForumContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<DiscussionForumUserReported?> CreateUpdateAsync(DiscussionForumUserReported user)
        {
            var existingUserReported = await _dbContext.DiscussionForumUserReporteds.FirstOrDefaultAsync(x => x.QuestionId == user.QuestionId && x.ReporterUserId == user.ReporterUserId && x.RespondentUserId == user.RespondentUserId);
            if (existingUserReported == null)
            {
                user.ReportId = Guid.NewGuid().ToString();
                user.UpdatedBy = null;
                await _dbContext.DiscussionForumUserReporteds.AddAsync(user);
                await _dbContext.SaveChangesAsync();
                return user;
            }
            else
            {
                existingUserReported.ReporterUserId = user.ReporterUserId;
                existingUserReported.RespondentUserId = user.RespondentUserId;
                existingUserReported.UpdatedBy = user.UpdatedBy;
                await _dbContext.SaveChangesAsync();
                return existingUserReported;
            }
        }

        public async Task<List<UserReportedDTO>> GetAllReportedUsersAsync()
        {
            List<UserReportedDTO> userReportList = new List<UserReportedDTO>();
            using (var con = (Microsoft.Data.SqlClient.SqlConnection)_dbContext.Database.GetDbConnection())
            {
                try
                {

                    con.Open();
                    SqlCommand cmd = new SqlCommand("[DiscussionForum].[usp_UserReported]", con);
                    cmd.Parameters.AddWithValue("@OpCode", 100);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        UserReportedDTO user = new UserReportedDTO
                        {
                            ReporterUserId = reader["ReporterUserId"].ToString(),
                            RespondentUserId = reader["RespondentUserId"].ToString(),
                            ReporterUserEmail = reader["ReporterUserEmail"].ToString(),
                            RespondentUserEmail = reader["RespondentUserEmail"].ToString(),
                            ReporterUserName = reader["ReporterUserName"].ToString(),
                            RespondentUserName = reader["RespondentUserName"].ToString(),
                            IsDelete = Convert.ToBoolean(reader["IsDelete"].ToString()),
                            Title = reader["Title"].ToString(),
                        };
                        userReportList.Add(user);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return userReportList;
        }

        public async Task<DiscussionForumUserReported?> GetByIdAsync(string reportUserId)
        {
            return await _dbContext.DiscussionForumUserReporteds.FirstOrDefaultAsync(x => x.ReportId == reportUserId);
        }
    }
}

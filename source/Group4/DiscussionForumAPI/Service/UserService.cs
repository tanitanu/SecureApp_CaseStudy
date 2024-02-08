using DiscussionForumAPI.Contracts;
using DiscussionForumAPI.Models;
using DiscussionForumAPI.Models.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DiscussionForumAPI.Service
{
    public class UserService : IUser
    {
        private readonly DiscussionForumContext _dbContext;
        public UserService(DiscussionForumContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AspNetUser?> DeleteAsync(string userId)
        {
            var existingUser = await _dbContext.AspNetUsers.FirstOrDefaultAsync(x => x.Id == userId);

            if (existingUser == null)
            {
                return null;
            }

            existingUser.IsActive = false;
            await _dbContext.SaveChangesAsync();
            return existingUser;
        }

        public async Task<List<UsersDTO>> GetAllUsersAsync()
        {
            List<UsersDTO> userList = new List<UsersDTO>();
            using (var con = (Microsoft.Data.SqlClient.SqlConnection)_dbContext.Database.GetDbConnection())
            {
                try
                {

                    con.Open();
                    SqlCommand cmd = new SqlCommand("[DiscussionForum].[usp_UserDetails]", con);
                    cmd.Parameters.AddWithValue("@OpCode", 100);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        UsersDTO user = new UsersDTO
                        {
                            Id = reader["Id"].ToString(),
                            Email = reader["Email"].ToString(),
                            UserName = reader["UserName"].ToString(),
                            Role = reader["Role"].ToString(),
                            IsActive = Convert.ToBoolean(reader["IsActive"].ToString()),
                        };
                        userList.Add(user);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return userList;
        }
    }
}

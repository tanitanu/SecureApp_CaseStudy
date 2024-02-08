using DiscussionForumAPI.Contracts;
using DiscussionForumAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DiscussionForumAPI.Service
{
    public class QuestionService : IQuestion
    {
        private readonly DiscussionForumContext _dbContext;
        public QuestionService(DiscussionForumContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<DiscussionForumQuestion?> CreateAsync(DiscussionForumQuestion question)
        {
            var existingQuestion = await _dbContext.DiscussionForumQuestions.FirstOrDefaultAsync(x => x.Title == question.Title && x.CreatedBy == question.CreatedBy && x.CategoryId == question.CategoryId && x.Status == "Open");
            if (existingQuestion == null)
            {
                question.QuestionId = Guid.NewGuid().ToString();
                await _dbContext.DiscussionForumQuestions.AddAsync(question);
                await _dbContext.SaveChangesAsync();
                return question;
            }
            else
            {
                question = new DiscussionForumQuestion();
                return question;
            }

        }

        public async Task<DiscussionForumQuestion?> DeleteAsync(string questionId, string? userId)
        {
            var existingQuestion = await _dbContext.DiscussionForumQuestions.FirstOrDefaultAsync(x => x.QuestionId == questionId);

            if (existingQuestion == null)
            {
                return null;
            }

            existingQuestion.IsDelete = true;
            existingQuestion.UpdatedBy = userId;
            await _dbContext.SaveChangesAsync();
            return existingQuestion;
        }

        public async Task<List<QuestionDetails?>> GetAllAsync()
        {
            List<QuestionDetails?> questionDetails = new List<QuestionDetails?>();
            using (var con = (Microsoft.Data.SqlClient.SqlConnection)_dbContext.Database.GetDbConnection())
            {
                try
                {

                    con.Open();
                    SqlCommand cmd = new SqlCommand("[DiscussionForum].[usp_QuestionAnswerDetails]", con);
                    cmd.Parameters.AddWithValue("@OpCode", 100);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        QuestionDetails detail = new QuestionDetails
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
                        questionDetails.Add(detail);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return questionDetails;
        }

        public async Task<List<DiscussionForumCategory>> GetAllCategories()
        {
            return await _dbContext.DiscussionForumCategories.Where(x=> x.IsDeleted == false).ToListAsync();
        }

        public async Task<DiscussionForumQuestion?> GetByIdAsync(string questionId)
        {
            return await _dbContext.DiscussionForumQuestions.FirstOrDefaultAsync(x => x.QuestionId == questionId);
        }

        public async Task<QuestionAnswerDetails> GetQuestionAnswerDetails(string questionId, string userId)
        {
            QuestionAnswerDetails questionAnswerDetails = new QuestionAnswerDetails();
            questionAnswerDetails.answerDetails = new List<AnswerDetails?>();
            using (var con = (Microsoft.Data.SqlClient.SqlConnection)_dbContext.Database.GetDbConnection())
            {
                try
                {

                    con.Open();
                    SqlCommand cmd = new SqlCommand("[DiscussionForum].[usp_QuestionAnswerDetails]", con);
                    cmd.Parameters.AddWithValue("@OpCode", 101);
                    cmd.Parameters.AddWithValue("@QuestionId", questionId);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            questionAnswerDetails.QuestionId = reader["Question Id"].ToString();
                            questionAnswerDetails.Title = reader["Title"].ToString();
                            questionAnswerDetails.Content = reader["Content"].ToString();
                            questionAnswerDetails.CategoryName = reader["Category Name"].ToString();
                            questionAnswerDetails.Status = reader["Status"].ToString();
                            questionAnswerDetails.LikeCount = Convert.ToInt32(reader["Like Count"].ToString());
                            questionAnswerDetails.DislikeCount = Convert.ToInt32(reader["Dislike Count"].ToString());
                            questionAnswerDetails.QuestionCreatedBy = reader["Created By"].ToString();
                            questionAnswerDetails.QuestionDateCreation = Convert.ToDateTime(reader["Date Creation"].ToString());
                            questionAnswerDetails.QuestionCreatedByName = reader["Question Created By Name"].ToString();
                            questionAnswerDetails.QuestionIsDelete = Convert.ToBoolean(reader["Question Is Delete"].ToString());
                            questionAnswerDetails.Like = Convert.ToBoolean(reader["Like"].ToString());
                            questionAnswerDetails.Dislike = Convert.ToBoolean(reader["Dislike"].ToString());
                            if (reader["Answer ID"].ToString() != string.Empty)
                            {
                                AnswerDetails answerDetail = new AnswerDetails
                                {
                                    AnswerID = reader["Answer ID"].ToString(),
                                    Answer = reader["Answer"].ToString(),
                                    AnswerIsDelete = Convert.ToBoolean(reader["Answer Is Delete"].ToString()),
                                    AnswerCreatedBy = reader["Created By Id"].ToString(),
                                    AnswerCreatedByName = reader["Answer Created By Name"].ToString(),
                                    AnswerDateCreation = Convert.ToDateTime(reader["Answer Date Creation"].ToString())
                                };
                                questionAnswerDetails.answerDetails.Add(answerDetail);
                            }

                        }
                        reader.NextResult();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return questionAnswerDetails;
        }

        public async Task<DiscussionForumQuestion?> UpdateAsync(string questionId, DiscussionForumQuestion question)
        {
            var existingQuestion = await _dbContext.DiscussionForumQuestions.FirstOrDefaultAsync(x => x.QuestionId == questionId);

            if (existingQuestion == null)
            {
                return null;
            }

            existingQuestion.CategoryId = question.CategoryId;
            existingQuestion.Title = question.Title;
            existingQuestion.Status = question.Status;
            existingQuestion.Content = question.Content;
            existingQuestion.UpdatedBy = question.UpdatedBy;
            await _dbContext.SaveChangesAsync();
            return existingQuestion;
        }

        public async Task<DiscussionForumQuestion?> UpdateAsync(string questionId, string? userId)
        {
            var existingQuestion = await _dbContext.DiscussionForumQuestions.FirstOrDefaultAsync(x => x.QuestionId == questionId);

            if (existingQuestion == null)
            {
                return null;
            }

            existingQuestion.Status = "Close";
            existingQuestion.UpdatedBy = userId;
            await _dbContext.SaveChangesAsync();
            return existingQuestion;
        }
    }
}

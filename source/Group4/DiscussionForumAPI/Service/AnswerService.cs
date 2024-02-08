using DiscussionForumAPI.Contracts;
using DiscussionForumAPI.Helper;
using DiscussionForumAPI.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

namespace DiscussionForumAPI.Service
{
    public class AnswerService : IAnswer
    {
        private readonly DiscussionForumContext _dbContext;
        private readonly IMail _mail;
        public AnswerService(DiscussionForumContext dbContext, IMail mail)
        {
            _dbContext = dbContext;
            _mail = mail;
        }
        public async Task<DiscussionForumAnswer?> CreateAsync(DiscussionForumAnswer answer)
        {
            var existingAnswer = await _dbContext.DiscussionForumAnswers.FirstOrDefaultAsync(x => x.Answer == answer.Answer && x.CreatedBy == answer.CreatedBy && x.QuestionId == answer.QuestionId);
            if (existingAnswer == null)
            {  
                answer.AnswerId = Guid.NewGuid().ToString();
                await _dbContext.DiscussionForumAnswers.AddAsync(answer);
                await _dbContext.SaveChangesAsync();
                var existingQuestion = await _dbContext.DiscussionForumQuestions.FirstOrDefaultAsync(x => x.QuestionId == answer.QuestionId);
                var user = await _dbContext.AspNetUsers.FirstOrDefaultAsync(x => x.Id == existingQuestion.CreatedBy);
                string subject = existingQuestion.Title;
                string body = "<div style=\"padding:20px; background - color: rgb(255, 255, 255);\"><div style=\"color: rgb(0, 0, 0); text - align: left;\"><h3 style=\"margin: 1rem 0\">" + existingQuestion.Content + "</h3><p style=\"padding - bottom: 16px\">Answered By: " + _dbContext.AspNetUsers.Where(x => x.Id == answer.CreatedBy).Select(c => c.UserName).FirstOrDefault() + "</p><p style=\"padding - bottom: 16px\">" +
                    "<p style=\"padding-bottom: 16px\">" + answer.Answer + "</p><p style=\"padding-bottom: 16px\">Thanks,<br>The ConvoVerse Team</p></div></div>";
                MailData mailData = new MailData();
                mailData.EmailToId = user.Email;
                mailData.EmailToName = user.UserName;
                mailData.EmailSubject = subject;
                mailData.EmailBody = body;
                bool isSuccessful = _mail.SendMailAsync(mailData);
                return answer;
            }
            else
            {
                answer = new DiscussionForumAnswer();
                return answer;
            }
        }

        public async Task<DiscussionForumAnswer?> DeleteAsync(string answerId, string? userId)
        {
            var existingAnswer = await _dbContext.DiscussionForumAnswers.FirstOrDefaultAsync(x => x.AnswerId == answerId);

            if (existingAnswer == null)
            {
                return null;
            }

            existingAnswer.IsDelete = true;
            existingAnswer.UpdatedBy = userId;
            await _dbContext.SaveChangesAsync();
            return existingAnswer;
        }

        public async Task<DiscussionForumAnswer?> GetByIdAsync(string answerId)
        {
            return await _dbContext.DiscussionForumAnswers.FirstOrDefaultAsync(x => x.AnswerId == answerId);
        }

        public async Task<QuestionAnswerDetails?> GetQuesAnsByIdAsync(string questionId, string answerId)
        {
            QuestionAnswerDetails questionAnswerDetails = new QuestionAnswerDetails();
            questionAnswerDetails.answerDetails = new List<AnswerDetails?>();
            using (var con = (Microsoft.Data.SqlClient.SqlConnection)_dbContext.Database.GetDbConnection())
            {
                try
                {

                    con.Open();
                    SqlCommand cmd = new SqlCommand("[DiscussionForum].[usp_QuestionAnswerDetails]", con);
                    cmd.Parameters.AddWithValue("@OpCode", 102);
                    cmd.Parameters.AddWithValue("@QuestionId", questionId);
                    cmd.Parameters.AddWithValue("@AnswerId", answerId);
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

        public async Task<DiscussionForumAnswer?> UpdateAsync(string answerId, DiscussionForumAnswer answer)
        {
            var existingAnswer = await _dbContext.DiscussionForumAnswers.FirstOrDefaultAsync(x => x.AnswerId == answerId);

            if (existingAnswer == null)
            {
                return null;
            }

            existingAnswer.Answer = answer.Answer;
            existingAnswer.UpdatedBy = answer.UpdatedBy;
            await _dbContext.SaveChangesAsync();
            var existingQuestion = await _dbContext.DiscussionForumQuestions.FirstOrDefaultAsync(x => x.QuestionId == answer.QuestionId);
            var user = await _dbContext.AspNetUsers.FirstOrDefaultAsync(x => x.Id == existingQuestion.CreatedBy);
            string subject = existingQuestion.Title;
            string body = "<div style=\"padding:20px; background - color: rgb(255, 255, 255);\"><div style=\"color: rgb(0, 0, 0); text - align: left;\"><h3 style=\"margin: 1rem 0\">" + existingQuestion.Content + "</h3><p style=\"padding - bottom: 16px\">Answered By: " + _dbContext.AspNetUsers.Where(x => x.Id == answer.UpdatedBy).Select(c => c.UserName).FirstOrDefault() + "</p><p style=\"padding - bottom: 16px\">" +
                "<p style=\"padding-bottom: 16px\">" + answer.Answer + "</p><p style=\"padding-bottom: 16px\">Thanks,<br>The ConvoVerse Team</p></div></div>";
            MailData mailData = new MailData();
            mailData.EmailToId = user.Email;
            mailData.EmailToName = user.UserName;
            mailData.EmailSubject = subject;
            mailData.EmailBody = body;
            bool isSuccessful = _mail.SendMailAsync(mailData);
            return existingAnswer;
        }
    }
}

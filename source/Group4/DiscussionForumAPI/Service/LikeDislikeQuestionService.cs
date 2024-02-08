using DiscussionForumAPI.Contracts;
using DiscussionForumAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DiscussionForumAPI.Service
{
    public class LikeDislikeQuestionService : ILikeDislikeQuestion
    {
        private readonly DiscussionForumContext _dbContext;
        public LikeDislikeQuestionService(DiscussionForumContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<DiscussionForumLikesDislike> CreateUpdateAsync(DiscussionForumLikesDislike likeDislikeQuestion)
        {
            var existingLikeDislike = await _dbContext.DiscussionForumLikesDislikes.FirstOrDefaultAsync(x => x.CreatedBy == likeDislikeQuestion.CreatedBy && x.QuestionId == likeDislikeQuestion.QuestionId);
            if (existingLikeDislike == null)
            {
                likeDislikeQuestion.LikeDislikeId = Guid.NewGuid().ToString();
                likeDislikeQuestion.UpdatedBy = null;
                await _dbContext.DiscussionForumLikesDislikes.AddAsync(likeDislikeQuestion);
                await _dbContext.SaveChangesAsync();
                return likeDislikeQuestion;
            }
            else
            {
                if(likeDislikeQuestion.Like == true && existingLikeDislike.Dislike == true)
                {
                    likeDislikeQuestion.Dislike = false;
                }
                if (likeDislikeQuestion.Dislike == true && existingLikeDislike.Dislike == true)
                {
                    likeDislikeQuestion.Like = false;
                }
                existingLikeDislike.Like = likeDislikeQuestion.Like;
                existingLikeDislike.Dislike = likeDislikeQuestion.Dislike;
                existingLikeDislike.UpdatedBy = likeDislikeQuestion.UpdatedBy;
                await _dbContext.SaveChangesAsync();
                return existingLikeDislike;
            }
        }

        public async Task<DiscussionForumLikesDislike?> GetByIdAsync(string likeDislikeQuestionId)
        {
            return await _dbContext.DiscussionForumLikesDislikes.FirstOrDefaultAsync(x => x.LikeDislikeId == likeDislikeQuestionId);
        }
    }
}

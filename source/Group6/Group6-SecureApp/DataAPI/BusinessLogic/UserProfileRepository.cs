using DataAPI.Interfaces;
using DataAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace DataAPI.BusinessLogic
{
    public class UserProfileRepository : RepositoryBase<UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

    }
}

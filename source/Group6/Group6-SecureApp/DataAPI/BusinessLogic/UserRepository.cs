using DataAPI.Interfaces;
using DataAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace DataAPI.BusinessLogic
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext): base(repositoryContext)
        {
        }
    }
}

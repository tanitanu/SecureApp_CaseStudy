using BlazorCARS.HealthShield.DAL.Data;
using BlazorCARS.HealthShield.DAL.Entity;
using BlazorCARS.HealthShield.DAL.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
  Created by JAYaseelan
 */
namespace BlazorCARS.HealthShield.DAL.Repository
{
    public class UserStoreRepository : BaseRepository<UserStore>, IUserStoreRepository
    {
        private readonly BlazorCARSDBContext _blazorDbContext;
        public UserStoreRepository(BlazorCARSDBContext blazorDbContext) : base(blazorDbContext)
        {
            _blazorDbContext = blazorDbContext;
        }

        public async Task<UserStore> GetByUserNameAsync(string username)
        {
            return await (from us in _blazorDbContext.UserStore
                          where us.UserName.ToLower() == username.ToLower()
                          select new UserStore
                          {
                              UserStoreId = us.UserStoreId,
                              UserName = us.UserName,
                              Password = us.Password,
                              UserRoleId = us.UserRoleId,
                              DiscriminationId = us.DiscriminationId
                          }).FirstOrDefaultAsync();
        }
    }
}

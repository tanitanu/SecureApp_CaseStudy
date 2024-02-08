using DXC.BlogConnect.WebAPI.Data;
using DXC.BlogConnect.WebAPI.Repositories.Interfaces;
using System.Numerics;
/*
* Created By: Kishore
*/
namespace DXC.BlogConnect.WebAPI.Repositories
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly BlogConnectDbcontext _dbContext;
        public IUserRepository Users { get; }
        public IUserRoleRepository UserRoles { get; }
        public UnitOfWork(BlogConnectDbcontext dbContext,
                            IUserRepository userRepository, IUserRoleRepository userRoleRepository)
        {
            _dbContext = dbContext;
            Users = userRepository;
            UserRoles = userRoleRepository;
        }

      

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task RollbackAsync()
        {
            await _dbContext.DisposeAsync();
        }

        public int Save()
        {
            return _dbContext.SaveChanges();
        }
    }
}

namespace DXC.BlogConnect.WebAPI.Repositories.Interfaces
{
    /*
* Created By: Kishore
*/
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IUserRoleRepository UserRoles { get; }

        Task CommitAsync();
        Task RollbackAsync();
        int Save();
    }
}

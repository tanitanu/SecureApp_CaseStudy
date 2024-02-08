namespace DataAPI.Interfaces
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
        IUserProfileRepository UserProfile { get; }
        void Save();
    }
}

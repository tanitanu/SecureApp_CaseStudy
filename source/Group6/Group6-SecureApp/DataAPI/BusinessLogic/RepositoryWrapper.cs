using DataAPI.Interfaces;
using DataAPI.Models;

namespace DataAPI.BusinessLogic
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private IUserRepository _user;
        private IUserProfileRepository _profile;

        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_repoContext);
                }
                return _user;
            }
        }
        public IUserProfileRepository UserProfile
        {
            get
            {
                if (_profile == null)
                {
                    _profile = new UserProfileRepository(_repoContext);
                }
                return _profile;
            }
        }

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }
        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}

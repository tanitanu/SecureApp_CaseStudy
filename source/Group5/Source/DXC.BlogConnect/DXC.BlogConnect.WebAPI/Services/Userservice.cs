using DXC.BlogConnect.WebAPI.Models.Domain;
using DXC.BlogConnect.DTO;
using DXC.BlogConnect.WebAPI.Repositories.Interfaces;
using DXC.BlogConnect.WebAPI.Services.Interfaces;
/*
* Created By: Kishore
*/
namespace DXC.BlogConnect.WebAPI.Services
{
    //user service - which we inject and consume inside the user controller
    public class UserService : IUserService
    {
        public IUnitOfWork _unitOfWork;
        public IUserRepository _userRepository;

        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllUserAsync()
        {
            IEnumerable<User> entity = await _unitOfWork.Users.GetAllAsync();
            return entity;
        }

        public async Task AddUserAsync(User entity)
        {
            await _unitOfWork.Users.AddAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public void UpdateUser(User entity)
        {
            _unitOfWork.Users.Update(entity);
            _unitOfWork.CommitAsync();
        }

        public async Task<bool> DeleteUser(int userId)
        {
            if (userId > 0)
            {
                var user = await _unitOfWork.Users.GetById(userId);
                if (user != null)
                {
                    _unitOfWork.Users.Delete(user);
                    var result = _unitOfWork.Save();

                    if (result > 0)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }

        public async Task<UserGetDTO> GetUserByUserName(string userName)
        {
            var userDTO = await _unitOfWork.Users.GetUserByUserName(userName);
            return userDTO;

        }
        public async Task<User> GetUserById(int userId)
        {
            var entity = await _unitOfWork.Users.GetById(userId);
            return entity;
        }
        public bool IsUserExistByUsername(string userName)
        {
            return _userRepository.IsUserExistByUsername(userName);
        }

        public async Task<List<User>> GetUserListById(int userId)
        {
            var entity = await _unitOfWork.Users.GetById(userId);
            var list = new List<User>();
            list.Add(entity);
            return list;
        }
        public async Task<List<UserGetDTO>> GetAllUserWithUserName()
        {
            var entity = await _userRepository.GetAllUserWithUserName();
            return entity;
        }
    }
}

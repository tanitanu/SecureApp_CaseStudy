using DXC.BlogConnect.WebAPI.Models.Domain;
using DXC.BlogConnect.WebAPI.Repositories;
using DXC.BlogConnect.WebAPI.Repositories.Interfaces;
using DXC.BlogConnect.WebAPI.Services.Interfaces;

namespace DXC.BlogConnect.WebAPI.Services
{
    //user role service - which we inject and consume inside the user role controller
    public class UserRoleService:IUserRoleService
    {
        public IUnitOfWork _unitOfWork;
        public IUserRoleRepository _roleRepository;
        public UserRoleService(IUnitOfWork unitOfWork, IUserRoleRepository roleRepository)
        {
            _unitOfWork = unitOfWork;
            _roleRepository = roleRepository;   
        }

        public async Task<IEnumerable<Role>> GetAllRoleAsync()
        {
            IEnumerable<Role> entity = await _unitOfWork.UserRoles.GetAllAsync();
            return entity;
        }

        public bool IsRoleExistByRolename(string roleName)
        {
            return _roleRepository.IsRoleExistByRolename(roleName);
        }

        public async Task AddRoleAsync(Role entity)
        {
            await _unitOfWork.UserRoles.AddAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public void UpdateRole(Role entity)
        {
            _unitOfWork.UserRoles.Update(entity);
            _unitOfWork.CommitAsync();
        }

        public Role GetRoleById(int roleId)
        {
            return _roleRepository.GetRoleByID(roleId);
        }
    }
}

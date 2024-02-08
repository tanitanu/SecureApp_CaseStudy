using MeetingScheduler.Entities;
using MeetingScheduler.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MeetingScheduler.Model
{
    public class UserHandler : BaseHandler, IUserHandler
    {
        private readonly IUserDAL userDAL ;
        public UserHandler()
        {
            userDAL=new UserDAL();
        }
        public Task<UserManagerResponse> ForgotPassword(ForgetPassword forgetPassword)
        {
            try 
            {
                return userDAL.ForgotPassword(forgetPassword);
            }
            catch (Exception ex) 
            {
                throw;
            }
        }

        public Task<UserManagerResponse> Login(UserLogin userLogin)
        {
            try
            {
                return userDAL.Login(userLogin);
            }
            catch (Exception ex) 
            {

                throw; 
            }   
        }

        public Task<IEnumerable<UserVo>> GetUsers()
        {
            try
            {
                return userDAL.GetUsers();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Task<UserManagerResponse> Register(UserRegister userRegister)
        {
            try
            {
                return userDAL.Register(userRegister);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<UserManagerResponse> resetPassword(ResetPasswordRequest resetPasswordRequest)
        {
            try
            {
                return userDAL.resetPassword(resetPasswordRequest);
            }

            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<UserManagerResponse> UpdateRole(int id, Role role)
        {
            try
            {
                return userDAL.UpdateRole(id, role);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

}

using Azure.Core;
using MeetingScheduler.Common;
using MeetingScheduler.DAL.Models;
using MeetingScheduler.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace MeetingScheduler.DAL
{
    public class UserDAL : BaseDAL, IUserDAL
    {
        #region
        private const string USER_ALREADY_EXIST = "User already exists!!";
        private const string USER_SUCCESS="User registered Succesfully";
        private const string USER_NOT_CREATED = "User is not created";
        private const string USER_NOT_FOUND= "User not found";
        private const string PASSWORD_INCORRECT = "Password is incorrect";
        private const string WELCOME_APP = "Welcome to Applicaiton!!";
        private const string RESET_PASSWORD_EMAIL = "You may now reset password based on email instruction";
        private const string INVALID_TOKEN = "Invalid Token ";
        private const string PASSWORD_RESET_SUCCESS = "Password has been succesfully reset";
        private const string ROLE_UPDT_SUCCESS = "Role has been successfully updated";
        private const string VALID_USER = "Please select a valid user";

        #endregion
        public UserDAL()
        { 
        
        }

        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="userRegister"></param>
        /// <returns></returns>
        public async Task<UserManagerResponse> Register(UserRegister userRegister)
        {
            using (var context = new MeetingSchedulerContext())
            {

                if (context.Users.Any(u => u.Email == userRegister.Email))
                {
                    return new UserManagerResponse
                    {
                        Message = USER_ALREADY_EXIST,
                        IsSuccess = false,
                    };
                }

                CreatePasswordHash(userRegister.Password,
                     out byte[] passwordHash,
                     out byte[] passwordSalt);

                var user = new User
                {
                    Email = userRegister.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    VerificationToken = CreateRandomToken(),
                    FirstName = userRegister.FirstName,
                    LastName = userRegister.LastName,
                    LastUpdtUserId = userRegister.FirstName + userRegister.LastName,
                    LastUpdtTs = DateTime.Now,
                    RoleId = 2
                };

                context.Users.Add(user);

                var result = await context.SaveChangesAsync();

                if (result == 1)
                {
                    return new UserManagerResponse
                    {
                        Message = USER_SUCCESS,
                        IsSuccess = true,
                    };
                }

                return new UserManagerResponse
                {
                    Message = USER_NOT_CREATED,
                    IsSuccess = false,

                };
            }
        }

        /// <summary>
        /// Create Random token
        /// </summary>
        /// <returns></returns>
        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }

        /// <summary>
        /// Created encrypted password
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        /// <summary>
        /// Get login details
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        public async Task<UserManagerResponse> Login(UserLogin userLogin)
        {
            try
            {
                using (var context = new MeetingSchedulerContext())
                {
                    var meetingDAL = new MeetingDAL();

                    meetingDAL.GetExpiredAndDeleteMeetings();

                    var user = await context.Users.FirstOrDefaultAsync(u => u.Email == userLogin.EmailId);

                    if (user == null)
                    {
                        return new UserManagerResponse
                        {
                            Message = USER_NOT_FOUND,
                            IsSuccess = false,
                        };
                    }

                    if (!VerifyPasswordHash(userLogin.Password, user.PasswordHash, user.PasswordSalt))
                    {
                        return new UserManagerResponse
                        {
                            Message = PASSWORD_INCORRECT,
                            IsSuccess = false,

                        };
                    }

                    return new UserManagerResponse
                    {
                        Message = WELCOME_APP,
                        IsSuccess = true,
                        UserName = user.FirstName + "  " + user.LastName,
                        UserId = user.Id
                    };
                }
            }

            catch (Exception ex)
            {
                throw;
            }

        }
        
        /// <summary>
        /// Get all Users
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<UserVo>> GetUsers()
        {          
            try
            {
                using (var context = new MeetingSchedulerContext())
                {

                    var users = await context.Users.ToListAsync();

                    var _lstUsers = from user in users
                                    select new UserVo
                                    {
                                        UserId = user.Id,
                                        UserName = user.FirstName + " " + user.LastName
                                    };

                    return _lstUsers;
                }
            }

            catch (Exception ex)
            {
                throw;
            }
           
        }

        /// <summary>
        /// Verify password
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        /// <returns></returns>
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                var done= computedHash.SequenceEqual(passwordHash);
                return done;
            }
        }

        /// <summary>
        /// Forgot Password verification
        /// </summary>
        /// <param name="forgetPassword"></param>
        /// <returns></returns>
        public async Task<UserManagerResponse> ForgotPassword(ForgetPassword forgetPassword )    
            {
            try
            {
                using (var context = new MeetingSchedulerContext())
                {


                    var user = await context.Users.FirstOrDefaultAsync(u => u.Email == forgetPassword.Email);

                    if (user == null)
                    {
                        return new UserManagerResponse
                        {
                            Message = USER_NOT_FOUND,
                            IsSuccess = false,

                        };
                    }

                    user.ResetToken = CreateRandomToken();
                    user.ResetTokenExpired = DateTime.Now.AddDays(1);


                    await context.SaveChangesAsync();

                    string url = $"https://localhost:7193/resetPassword";

                    string Body = $"<p>To reset your password <a href={url}>Click here</a></p>" + $"<h1>Please use below Token to reset your password before Token is expired, {user.ResetToken}</h1>";

                    Helper.SendEmail(forgetPassword.Email, "Forget Password", Body);

                    return new UserManagerResponse
                    {
                        Message = RESET_PASSWORD_EMAIL,
                        IsSuccess = true,
                    };
                }
            }
            catch(Exception ex)
            {
                return new UserManagerResponse
                {
                    Message = ex.Message,
                    IsSuccess = false,

                };
            }

        }

        /// <summary>
        /// Rset password
        /// </summary>
        /// <param name="resetPasswordRequest"></param>
        /// <returns></returns>
        public async Task<UserManagerResponse> resetPassword(ResetPasswordRequest resetPasswordRequest)
        {
            try
            {
                using (var context = new MeetingSchedulerContext())
                {

                    var user = await context.Users.FirstOrDefaultAsync(u => u.ResetToken == resetPasswordRequest.Token);

                    if (user == null || user.ResetTokenExpired < DateTime.Now)
                    {
                        return new UserManagerResponse
                        {
                            Message = INVALID_TOKEN,
                            IsSuccess = false,

                        };
                    }

                    CreatePasswordHash(resetPasswordRequest.Password, out byte[] passwordHash, out byte[] passwordSalt);

                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                    user.ResetToken = null;
                    user.ResetTokenExpired = null;

                    await context.SaveChangesAsync();

                    return new UserManagerResponse
                    {
                        Message = PASSWORD_RESET_SUCCESS,
                        IsSuccess = true,
                    };
                }
            }
            catch(Exception ex)
            {
                return new UserManagerResponse
                {
                    Message = ex.Message,
                    IsSuccess = false,

                };
            }


        }

        /// <summary>
        /// Update role
        /// </summary>
        /// <param name="id"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<UserManagerResponse> UpdateRole(int id, Role role)
        {
            try
            {
                using (var context = new MeetingSchedulerContext())
                {
                    var data = await context.Users.FindAsync(id);
                    if (data != null)
                    {
                        data.RoleId = role.RoleId;
                        await context.SaveChangesAsync();
                        return new UserManagerResponse
                        {
                            Message = ROLE_UPDT_SUCCESS,
                            IsSuccess = true,
                        };

                    }
                    return new UserManagerResponse
                    {
                        Message = VALID_USER,
                        IsSuccess = false,
                    };
                }
            }
            catch(Exception ex)
            {
                return new UserManagerResponse
                {
                    Message = ex.Message,
                    IsSuccess = false,
                };
            }

        }



    }

}


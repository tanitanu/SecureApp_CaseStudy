using DataAPI.Interfaces;
using DataAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DataAPI.Controllers.v1
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private IRepositoryWrapper _repository;


        public UserController(ILogger<UserController> logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet("/Ping")]
        public string Ping()
        {
            _logger.LogInformation("Ping()");
            return "Hello from API. " + DateTime.Now.ToString();
        }


        [HttpGet]
        [Route("GetAllUserProfiles")]
        /// <summary>
        /// Method to get All users
        /// </summary>
        public IEnumerable<UserProfile> GetAllUserProfiles()
        {
            var profiles = _repository.UserProfile.FindAll().Include(z => z.User);
            return profiles;
        }

        [HttpGet]
        [Route("FindProfilebyProfileID")]
        /// <summary>
        /// Method to get user profile by profile id
        /// </summary>
        public UserProfile FindProfilebyProfileID(string Id)
        {
            var user = _repository.UserProfile.FindByCondition(x => x.ID.Equals(Id)).Include(z => z.User).FirstOrDefault();
            return user;
        }

        [HttpGet]
        [Route("FindProfilebyUserID")]
        /// <summary>
        /// Method to get user profile by user id
        /// </summary>
        public UserProfile FindProfilebyUserID(string Id)
        {
            var user = _repository.UserProfile.FindByCondition(x => x.User.ID.Equals(Id)).Include(z => z.User).FirstOrDefault();
            return user;
        }

        [HttpGet]
        [Route("FindUserbyUserID")]
        /// <summary>
        /// Method to get user by user id
        /// </summary>
        public User FindUserbyUserID(string Id)
        {
            var user = _repository.User.FindByCondition(x => x.ID.Equals(Id)).FirstOrDefault();
            return user;
        }

        [HttpGet]
        [Route("FindUserbyProfileID")]
        /// <summary>
        /// Method to get user by Profile id
        /// </summary>
        public UserProfile FindUserbyProfileID(string Id)
        {
            var user = _repository.UserProfile.FindByCondition(x => x.User.ID.Equals(Id)).FirstOrDefault();
            return user;
        }

        [HttpGet]
        [Route("FindUserbyUserName")]
        /// <summary>
        /// Method to get user by user name
        /// </summary>
        public User FindUserbyUserName(string name)
        {
            var user = _repository.User.FindByCondition(x => x.Name.Equals(name)).FirstOrDefault();
            return user;
        }


        [HttpGet]
        [Route("FindUserProfilebyUserName")]
        /// <summary>
        /// Method to get user profile by user name
        /// </summary>
        public UserProfile FindUserProfilebyUserName(string name)
        {
            var user = _repository.UserProfile.FindByCondition(x => x.User.Name.Equals(name)).Include(z => z.User).FirstOrDefault();
            return user;
        }     

        [HttpPut]
        [Route("EditUserProfile")]
        /// <summary>
        /// Method to update user details
        /// </summary>
        public UserProfile EditUserProfile(UserProfile profile)
        {
            try
            {
                var profileData = FindProfilebyProfileID(profile.ID);
                if (profileData != null)
                {
                    profile.UserId = profileData.UserId;
                    _repository.UserProfile.Update(profile);
                    _repository.Save();
                    return profileData;
                }
                else
                {
                    _logger.LogError("No data found. Method- UpdateUserProfile");
                    return null;
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError("Error in method - UpdateUserProfile ", ex);
                throw;
            }

        }


        [HttpDelete]
        [Route("DeleteUser")]
        /// <summary>
        /// Method to delete user 
        /// </summary>
        public bool DeleteUserProfile(string Id)
        {
            try
            {
                var profile = FindProfilebyProfileID(Id);
                if (profile != null)
                {
                    //delete both user and profile table data
                    _repository.UserProfile.Delete(profile);
                    _repository.User.Delete(profile.User);
                    _repository.Save();
                    return true;
                }
                else
                    return false;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError("Error in method - DeleteUserProfile ", ex);

                if (FindUserbyUserID(Id) == null)
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }


        [HttpPost]
        [Route("Login")]
        /// <summary>
        /// Method for user login
        /// </summary>
        public bool Login(Login user)
        {
            try
            {
                var data = _repository.UserProfile.FindByCondition(x => x.User.Name.Equals(user.Name) && x.User.Password.Equals(user.Password)).FirstOrDefault();
                if (data != null)
                    return true;
                else
                    return false;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError("Error in method - DeleteUserProfile ", ex);
                throw;
            }
        }


        [HttpPost]
        [Route("ChangePassword")]
        /// <summary>
        /// Method to change password
        /// </summary>
        public bool ChangePassword(ChangePassword data)
        {
            try
            {
                var user = _repository.User.FindByCondition(x => x.Name.Equals(data.userName) && x.Password.Equals(x.Password)).FirstOrDefault();
                if (user != null)
                {
                    user.Password = data.NewPassword;
                    _repository.User.Update(user);
                    _repository.Save();
                    return true;
                }
                else
                {
                    _logger.LogError("No data found. Method- ChangePassword");
                    return false;
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError("Error in method - ChangePassword ", ex);
                throw;
            }

        }

    }
}
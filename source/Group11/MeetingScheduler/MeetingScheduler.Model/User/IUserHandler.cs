﻿using System;
using MeetingScheduler.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MeetingScheduler.Model
{
    public interface IUserHandler
    {
        Task<UserManagerResponse> Register(UserRegister userRegister);
        Task<UserManagerResponse> Login(UserLogin userLogin);

        Task<UserManagerResponse> ForgotPassword(ForgetPassword forgetPassword);

        Task<UserManagerResponse> resetPassword(ResetPasswordRequest resetPasswordRequest);
        Task<IEnumerable<UserVo>> GetUsers();

        Task<UserManagerResponse> UpdateRole(int id, Role role);

    }
}
﻿using Daily_Status_Report_task.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Daily_Status_Report_task.Repository.IRepository
{
    public interface IUserRepository
    {
        IEnumerable<UserTable> GetAllUsers();
        bool IsUniqueUser(string email, string name);
        UserTable Authenticate(string email, string password);
        UserTable Register(string email, string password, string name, string address, int departmentId, int roleId);
        UserTable UpdateUser(UserTable userTable);
        void DeleteUser(int id);
        Task<UserTable> GetUserById(int id);
        UserTable UpdateUsers(UserTable userTable);
    }
}

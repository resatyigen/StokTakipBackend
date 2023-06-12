using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IUserService
    {
        Task<List<User>> GetAllAsync();
        Task<User> GetUserByUsernameAndPassword(string userName, string password);
        Task<User> GetAsync(int id);
        Task<User> AddAsync(User user);
        Task<bool> CheckUserName(string userName);
        Task<bool> CheckEmail(string email);
        Task<User> UpdateAsync(User user);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public Task<User> AddAsync(User user)
        {
            return _userDal.AddAsync(user);
        }

        public async Task<bool> CheckEmail(string email)
        {
            var user = await _userDal.GetAsync(x => x.Email == email);
            return user != null;
        }

        public async Task<bool> CheckUserName(string userName)
        {
            var user = await _userDal.GetAsync(x => x.UserName == userName);
            return user != null;
        }

        public Task<User> GetAsync(int id)
        {
            return _userDal.GetAsync(x => x.ID == id);
        }

        public Task<List<User>> GetAllAsync()
        {
            return _userDal.GetAllAsync();
        }

        public Task<User> GetUserByUsernameAndPassword(string userName, string password)
        {
            return _userDal.GetAsync(x => x.UserName == userName && x.Password == password);
        }

        public Task<User> UpdateAsync(User user)
        {
            return _userDal.UpdateAsync(user);
        }
    }
}
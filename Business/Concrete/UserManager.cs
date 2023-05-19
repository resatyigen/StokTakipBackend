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

        public Task<List<User>> GetAllAsync()
        {
            return _userDal.GetAllAsync();
        }
    }
}
using OpenResumeAPI.Business.Interfaces;
using OpenResumeAPI.Models;
using OpenResumeAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenResumeAPI.Business
{
    public class UserBusiness : CRUDBusiness<User, IUserRepository>, IUserBusiness
    {
        public UserBusiness(IUserRepository repository) : base(repository) { }

        public User Login(User user)
        {
            User result = repository.FindByEmail(user.Email);
            if(result == null || !result.PasswordHash.Equals(user.PasswordHash))
                result = null;           
            return result;
        }
    }
}

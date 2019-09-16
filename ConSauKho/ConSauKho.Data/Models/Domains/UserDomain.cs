using ConSauKho.Data.Models.Repositories;
using ConSauKho.Data.Models.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConSauKho.Data.Models.Domains
{
    public class UserDomain : BaseDomain
    {
        public UserDomain(IUnitOfWork uow) : base(uow)
        {

        }

        public Users GetByUsername(string username)
        {
            return uow.GetService<IUsersRepository>().FindByUsername(username);
        }

        public Users Create(Users user)
        {
            return uow.GetService<IUsersRepository>().Create(user).Entity;
        }

        public Users Login(UserLoginModel model)
        {
            return uow.GetService<IUsersRepository>().CheckLogin(model.Username, model.Password);
        }
    }
}

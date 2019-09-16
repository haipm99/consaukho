using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConSauKho.Data.Models.Repositories
{
    public partial interface IUsersRepository : IBaseRepository<Users,string>
    {
        Users FindByUsername(string username);

        Users CheckLogin(string username, string password);
    }

    public partial class UsersRepository : BaseRepository<Users,string> , IUsersRepository
    {
        public Users FindByUsername(string username)
        {
            return dbSet.FirstOrDefault(user => user.Username == username);
        }

        public Users CheckLogin(string username, string password)
        {
            return dbSet.FirstOrDefault(user => user.Username == username && user.Password == password);
        }

    }
}

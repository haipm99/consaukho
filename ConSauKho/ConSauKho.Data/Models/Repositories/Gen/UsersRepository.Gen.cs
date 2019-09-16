using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConSauKho.Data.Models;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace ConSauKho.Data.Models.Repositories
{
	public partial interface IUsersRepository : IBaseRepository<Users, string>
	{
	}
	
	public partial class UsersRepository : BaseRepository<Users, string>, IUsersRepository
	{
		public UsersRepository(DbContext context) : base(context)
		{
		}

        #region CRUD area
        public override Users FindById(string key)
		{
			var entity = dbSet.FirstOrDefault(
				e => e.Id == key);
			return entity;
		}
		
		public override async Task<Users> FindByIdAsync(string key)
		{
			var entity = await dbSet.FirstOrDefaultAsync(
				e => e.Id == key);
			return entity;
		}
        #endregion
    }
}

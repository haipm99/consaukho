using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConSauKho.Data.Models.Repositories;
using ConSauKho.Data.ViewModels;
using ConSauKho.Data.Models;
using ConSauKho.Data.Global;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ConSauKho.Data.Models.Domains
{
	public abstract partial class BaseDomain
	{
		protected readonly IUnitOfWork uow;
		
		public BaseDomain(IUnitOfWork uow)
		{
			this.uow = uow;
		}
		
	}
}

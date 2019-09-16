using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConSauKho.Data.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ConSauKho.WebApi.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IUnitOfWork _uow;

        public BaseController(IUnitOfWork uow)
        {
            _uow = uow;
        }
    }
}

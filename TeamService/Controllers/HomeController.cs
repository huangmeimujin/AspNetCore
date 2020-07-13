using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamService.Controllers
{
    public class HomeController:Controller
    {
        public IActionResult Index()
        {
            return this.Ok("Hello,Word!");
        }
    }
}

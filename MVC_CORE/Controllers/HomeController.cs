using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MVC_CORE.Data;
using MVC_CORE.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_CORE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            this.context = context;
            _logger = logger;
        }

        public IActionResult Index(string username)
        {
            if (username != null)
            {
                var v = context.users.Where(x => x.username == username).FirstAsync();
                ViewBag.userrol = v.Result.role;

            }
            ViewBag.result = context.courses.OrderByDescending(x => x.Rate).ToList();

            return View(context.Topics.ToList());
        }

       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

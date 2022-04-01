using _0330_Auth.Models;
using _0330_Auth.Models.DBEntity;
using _0330_Auth.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace _0330_Auth.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDBRepository _repository;


        public HomeController(ILogger<HomeController> logger, IDBRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [AllowAnonymous]
        public IActionResult NoAuthPage()
        {
            return View();
        }

        [Authorize]
        public IActionResult AuthPage()
        {
            var userId = int.Parse(User.Identity.Name);
            var addressData = _repository.GetAll<AddressBook>().Where(x => x.UserId == userId).ToList();
            return View(addressData);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult RolePage()
        {
            return View();
        }
    }
}

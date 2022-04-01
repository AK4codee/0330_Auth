using _0330_Auth.Models.DTO.Account;
using _0330_Auth.Models.ViewModel.Account.Data;
using _0330_Auth.Services.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace _0330_Auth.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _serevice;
        public AccountController(IAccountService service)
        {
            _serevice = service;
        }
        public IActionResult Signup()
        {
            return View();
        }

        public IActionResult Verify(int user)
        {
            _serevice.VerifyAccount(user);
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginDataModel request)
        {
            var inputDto = new LoginAccountInputDto
            {
                Account = request.Account,
                Password = request.Password,
            };

            var outputDto = _serevice.LoginAccount(inputDto);

            if (outputDto.IsSuccess)
            {
                return Redirect("/");
            }
            else
            {
                return View("Login");
            }

        }

        // 前端互動用的
        [HttpPost]
        public IActionResult FetchLogin([FromBody] LoginDataModel request)
        {
            var inputDto = new LoginAccountInputDto
            {
                Account = request.Account,
                Password = request.Password,
            };

            var outputDto = _serevice.LoginAccount(inputDto);

            return new JsonResult(outputDto);
        }

        public IActionResult UserInfo()
        {
            return PartialView("_UserInfoPartial");
        }

        public IActionResult Logout()
        {
            _serevice.LogoutAccount();
            return Redirect("/");
        }

        [HttpPost]
        public IActionResult Signup(SignupDataModel request)
        {
            var inputDto = new CreateAccountInputDto
            {
                Email = request.Email,
                Name = request.Email,
                Phone = request.Phone,
                Password = request.Password,
                PasswordCheck = request.PasswordCheck,
            };

            var outputDto = _serevice.CreateAccount(inputDto);

            if (outputDto.IsSuccess)
            {
                return View("CheckEmail");
            }
            else
            {
                return View("Signup");
            }
        }
    }
}

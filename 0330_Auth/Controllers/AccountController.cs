using _0330_Auth.Models.DTO.Account;
using _0330_Auth.Models.ViewModel.Account.Data;
using _0330_Auth.Services.Interface;
using Microsoft.AspNetCore.Mvc;

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
                return Redirect("/");
            }
            else
            {
                return View("Signup");
            }
        }
    }
}

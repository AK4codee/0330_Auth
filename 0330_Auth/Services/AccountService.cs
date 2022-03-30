using _0330_Auth.Common;
using _0330_Auth.Models.DBEntity;
using _0330_Auth.Models.DTO.Account;
using _0330_Auth.Repositories.Interface;
using _0330_Auth.Services.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace _0330_Auth.Services
{
    public class AccountService : IAccountService
    {
        private readonly IDBRepository _repository;
        private readonly IMailService _mailService;
        private readonly IHttpContextAccessor _contextAccessor;
        public AccountService(IDBRepository repository, IMailService mailService, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _mailService = mailService;
            _contextAccessor = httpContextAccessor;
        }
        public CreateAccountOutputDto CreateAccount(CreateAccountInputDto input)
        {
            var res = new CreateAccountOutputDto();
            res.IsSuccess = false;
            res.User.UserName = input.Name;
            res.User.UserEmail = input.Email;
            res.User.UserPhone = input.Phone;

            if (this.IsExistAccount(input.Email))
            {
                res.Message = "Email have already exist!";
                return res;
            }

            if(input.Password != input.PasswordCheck)
            {
                res.Message = "Password is not currected!";
                return res;
            }

            var entity = new User
            {
                Email = input.Email,
                Phone = input.Phone,
                Name = input.Name,
                Password = Encryption.SHA256Encryption(input.Password),
                IsAdmin = false,
                IsVerify = false,
            };

            var target = _repository.DBContext.Users.Add(entity);
            _repository.Save();

            //驗證
            _mailService.SendVerifyMail(target.Entity.Email, target.Entity.Id);

            res.IsSuccess = true;
            res.User.UserId = target.Entity.Id;

            return res;
        }

        public bool IsExistAccount(string email)
        {
            return _repository.GetAll<User>().Any(x => x.Email == email);
        }

        public LoginAccountOutputDto LoginAccount(LoginAccountInputDto input)
        {
            var res = new LoginAccountOutputDto();
            res.IsSuccess = false;

            if (!this.IsExistAccount(input.Account))
            {
                res.Message = "使用者不存在，請先註冊";
                return res;
            }

            var currentUser = _repository.GetAll<User>().First(x => x.Email == input.Account);
            
            if (!currentUser.IsVerify)
            {
                res.Message = "請先驗證帳號";
                return res;
            }

            if(Encryption.SHA256Encryption(input.Password) != currentUser.Password)
            {
                res.Message = "密碼錯誤";
                return res;
            }

            res.IsSuccess = true;
            res.User.UserId = currentUser.Id;
            res.User.UserName = currentUser.Name;
            res.User.UserEmail = currentUser.Email;
            res.User.UserPhone = currentUser.Phone;
            res.User.UserRole = currentUser.IsAdmin ? "Admin" : "User";

            if (res.IsSuccess)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, res.User.UserId.ToString()),
                    new Claim(ClaimTypes.Email, res.User.UserEmail),
                    new Claim(ClaimTypes.Role, res.User.UserRole),
                    new Claim("UserName", res.User.UserName)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                _contextAccessor.HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity));
            }

            return res;
        }

        public void LogoutAccount()
        {
            _contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public void VerifyAccount(int userId)
        {
            var user = _repository.GetAll<User>().First(x => x.Id == userId);
            if (!user.IsVerify)
            {
                user.IsVerify = true;

                _repository.Update<User>(user);
                _repository.Save();
            }
        }
    }
}

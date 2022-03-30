using _0330_Auth.Models.DTO.Account;

namespace _0330_Auth.Services.Interface
{
    public interface IAccountService
    {
        public CreateAccountOutputDto CreateAccount(CreateAccountInputDto input);
        public LoginAccountOutputDto LoginAccount(LoginAccountInputDto input);
        public void LogoutAccount();
        public bool IsExistAccount(string email);
        public void VerifyAccount(int userId);
    }
}

namespace _0330_Auth.Models.DTO.Account
{
    public class CreateAccountInputDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string PasswordCheck { get; set; }
    }
}

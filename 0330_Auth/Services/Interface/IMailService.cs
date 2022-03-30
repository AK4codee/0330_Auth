namespace _0330_Auth.Services.Interface
{
    public interface IMailService
    {
        public void SendVerifyMail(string mailTo, int userId);
    }
}

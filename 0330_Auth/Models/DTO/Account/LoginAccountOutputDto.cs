﻿namespace _0330_Auth.Models.DTO.Account
{
    public class LoginAccountOutputDto
    {
        public LoginAccountOutputDto()
        {
            User = new UserData();
        }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public UserData User { get; set; }
        public class UserData
        {
            public int UserId { get; set; }
            public string UserName { get; set; }
            public string UserPhone { get; set; }
            public string UserEmail { get; set; }
            public string UserRole { get; set; }
        }
    }
}

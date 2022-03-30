using System;
using System.Collections.Generic;

#nullable disable

namespace _0330_Auth.Models.DBEntity
{
    public partial class User
    {
        public User()
        {
            AddressBooks = new HashSet<AddressBook>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsVerify { get; set; }

        public virtual ICollection<AddressBook> AddressBooks { get; set; }
    }
}

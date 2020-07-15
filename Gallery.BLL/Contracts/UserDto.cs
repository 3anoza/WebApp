using System.Collections.Generic;
using Gallery.DAL.Models;

namespace Gallery.BLL.Contracts
{
    public class UserDto
    {
        public int UserId { get;  set; }
        public string UserEmail { get;  set; }
        public string Password { get;  set; }
        public List<Role> UserRole { get;  set; }
    }
}
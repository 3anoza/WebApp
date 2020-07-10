using System.Collections.Generic;
using Gallery.DAL.Models;

namespace Gallery.BLL.Contracts
{
    public class UserDto
    {
        public int UserId { get; protected set; }
        public string UserEmail { get; protected set; }
        public string Password { get; protected set; }
        public List<Role> UserRole { get; protected set; }
    }
}
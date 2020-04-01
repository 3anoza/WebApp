using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Gallery.DAL.Interfaces;
using Gallery.DAL.Models;

namespace Gallery.DAL
{
    public class DbRepository : IRepository
    {
        protected readonly UserContext Context = new UserContext();

        public DbRepository(UserContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> IsUserExistAsync(string username, string plainPassword)
        {
            return await Context.Users.AnyAsync(u =>
                u._email == username.Trim().ToLower() && u._password == plainPassword.Trim());
        }

        public async Task AddUserToDatabaseAsync(string username, string plainPassword)
        {
            Context.Users.Add(new User() {_email = username, _password = plainPassword});
            Context.SaveChanges();
        }

        public int GetPersonId(string username)
        {
            return Context.Users.Where(u => u._email == username).Select(u => u._userId).FirstOrDefault();
        }
    }
}
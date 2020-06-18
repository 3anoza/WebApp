using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Gallery.DAL.Contexts;
using Gallery.DAL.Interfaces;
using Gallery.DAL.Models;

namespace Gallery.DAL
{
    public class DbRepository : IRepository
    {
        protected readonly SqlContext Context = new SqlContext();

        public DbRepository(SqlContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> IsUserExistAsync(string username, string plainPassword)
        {
            return await Context.Users.AnyAsync(u =>
                u.Email == username.Trim().ToLower() && u.Password == plainPassword.Trim());
        }

        public async Task AddUserToDatabaseAsync(string username, string plainPassword)
        {
            Context.Users.Add(new User() {Email = username, Password = plainPassword});
            Context.SaveChanges();
        }

        public int GetPersonId(string username)
        {
            return Context.Users.Where(u => u.Email == username).Select(u => u.Id).FirstOrDefault();
        }
    }
}
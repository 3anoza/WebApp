using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Gallery.DAL.Contexts;
using Gallery.DAL.Interfaces;
using Gallery.DAL.Models;
using SqlContext = Gallery.DAL.Contexts.SqlContext;

namespace Gallery.DAL
{
    public class DbRepository : IRepository
    {
        protected readonly SqlContext Context = new SqlContext();

        public DbRepository(SqlContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> IsUserExistAsync(string userEmail, string password)
        {
            return await Context.Users.AnyAsync(u =>
                u.Email == userEmail.Trim().ToLower() && u.Password == password.Trim());
        }

        public async Task<User> FindUserAsync(string email, string password)
        {
            return await Context.Users.FirstOrDefaultAsync(u =>
                u.Email == email && u.Password == password);
        }

        public async Task AddUserToDatabaseAsync(string userEmail, string password)
        {
            Context.Users.Add(new User() {Email = userEmail, Password = password });
            Role role = Context.Roles.First(r => r.Name == "user");
            Context.Roles.Add(role);
            Context.SaveChanges();
        }

        public async Task AddAttemptToDatabaseAsync(string email, string ipAddress, bool isSuccess)
        {
            var user = await Context.Users.FirstOrDefaultAsync(p => p.Email == email);
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            Attempts attempt = new Attempts {Id = 1,
                    TimeStamp = DateTime.Now,
                    Success = isSuccess,
                    IpAddress = ipAddress,
                    User = user
            };
            Context.Attempts.Add(attempt);
            await Context.SaveChangesAsync();
        }

        public int GetPersonId(string userEmail)
        {
            return Context.Users.Where(u => u.Email == userEmail).Select(u => u.Id).FirstOrDefault();
        }

        public string GetUsersNames(int id)
        {
            return Context.Users.Where(u => u.Id == id).Select(u => u.Email).FirstOrDefault();
        }
    }
}
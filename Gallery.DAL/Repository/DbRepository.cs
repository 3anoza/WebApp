using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Gallery.DAL.Interfaces;
using Gallery.DAL.Models;
using SqlContext = Gallery.DAL.Contexts.SqlContext;

namespace Gallery.DAL.Repository
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
                u.Email == userEmail && u.Password == password);
        }

        public async Task<User> FindUserAsync(string email, string password)
        {
            return await Context.Users.FirstOrDefaultAsync(u =>
                u.Email == email && u.Password == password);
        }

        public async Task<bool> IsUserExistByEmailAsync(string email)
        {
            return await Context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await Context.Users.FirstOrDefaultAsync(u => u.Id == id);
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
            Attempts attempt = new Attempts {
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

    }
}
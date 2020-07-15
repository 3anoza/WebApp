using System.Threading.Tasks;
using Gallery.DAL.Models;

namespace Gallery.DAL.Interfaces
{
    public interface IRepository
    {
        Task<bool> IsUserExistAsync(string userEmail, string password);
        Task<User> FindUserAsync(string email, string password);
        Task<bool> IsUserExistByEmailAsync(string email);
        Task<User> GetUserByIdAsync(int id);
        Task AddUserToDatabaseAsync(string userEmail, string password);
        Task AddAttemptToDatabaseAsync(string email, string ipAddress, bool isSuccess);
        int GetPersonId(string userEmail);
        
    }
}
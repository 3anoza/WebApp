using System.Threading.Tasks;

namespace Gallery.DAL.Interfaces
{
    public interface IRepository
    {
        Task<bool> IsUserExistAsync(string username, string plainPassword);
        Task AddUserToDatabaseAsync(string username, string plainPassword);
        int GetPersonId(string username);
    }
}
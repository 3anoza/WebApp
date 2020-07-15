using System.Threading.Tasks;
using Gallery.BLL.Contracts;
using Gallery.DAL.Models;

namespace Gallery.BLL.Interfaces
{
    public interface IUserService
    {
        Task<bool> IsUserExistAsync(UserDto userDto);
        Task<UserDto> FindUserAsync(UserDto userDto);
        Task<bool> IsUserExistByEmailAsync(UserDto userDto);
        Task<UserDto> GetUserByIdAsync(int id);
        Task AddUserAsync(UserDto userDto);
        Task AddAttemptAsync(AttemptsDTO attemptsDto);
        int GetPersonId(string username);
    }
}
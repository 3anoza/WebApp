using System.Threading.Tasks;
using Gallery.BLL.Contracts;

namespace Gallery.BLL.Interfaces
{
    public interface IUserService
    {
        Task<bool> IsUserExistAsync(string username, string plainPassword);
        Task<UserDto> FindUserAsync(string username, string plainPassword);
        Task AddUserAsync(AddUserDto dto);
        int GetPersonId(string username);
    }
}
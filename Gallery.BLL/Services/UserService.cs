using System;
using System.Threading.Tasks;
using Gallery.BLL.Contracts;
using Gallery.BLL.Interfaces;
using Gallery.DAL.Interfaces;

namespace Gallery.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository repository;

        public UserService(IRepository Repository)
        {
            repository = Repository ?? throw new ArgumentNullException(nameof(Repository));
        }

        public async Task<bool> IsUserExistAsync(string username, string plainPassword)
        {
            return await repository.IsUserExistAsync(username, plainPassword);
        }

        public async Task<UserDto> FindUserAsync(string username, string plainPassword)
        {
            throw new NotImplementedException();
        }

        public async Task AddUserAsync(AddUserDto dto)
        {
           await repository.AddUserToDatabaseAsync(dto.Username, dto.PlainPassword);
        }

        public int GetPersonId(string username)
        {
            return repository.GetPersonId(username);
        }
    }
}
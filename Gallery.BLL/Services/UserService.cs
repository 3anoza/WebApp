using System;
using System.Linq;
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

        public async Task<bool> IsUserExistAsync(UserDto userDto)
        {
            return await repository.IsUserExistAsync(userDto.UserEmail, userDto.Password);
        }

        public async Task<UserDto> FindUserAsync(UserDto userDto)
        {
            var user = await repository.FindUserAsync(userDto.UserEmail, userDto.Password);
            return new UserDto
            {
                UserId = user.Id,
                UserEmail = user.Email,
                Password = user.Password,
                UserRole = user.Roles.ToList()
            };
        }

        public async Task AddUserAsync(UserDto userDto)
        {
           await repository.AddUserToDatabaseAsync(userDto.UserEmail, userDto.Password);
        }

        public async Task AddAttemptAsync(AttemptsDTO attemptsDto)
        {
            await repository.AddAttemptToDatabaseAsync
                (attemptsDto.Email, attemptsDto.IpAddress, attemptsDto.IsSuccess);
        }


        public int GetPersonId(string username)
        {
            return repository.GetPersonId(username);
        }
    }
}
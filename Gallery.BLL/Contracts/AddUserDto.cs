namespace Gallery.BLL.Contracts
{
    public class AddUserDto
    {
        public string Username { get; }
        public string PlainPassword { get; }
        public AddUserDto(string username, string plainPassword)
        {
            Username = username;
            PlainPassword = plainPassword;
        }
    }
}
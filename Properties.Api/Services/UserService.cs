using Microsoft.Extensions.Configuration;
using Properties.Api.IServices;
using Properties.Domain.Entities;

namespace Properties.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;

        public UserService(IConfiguration configuration)
        {
            _configuration = configuration;

            User user = new User()
            {
                Email = _configuration["Credentials:Email"],
                Password=_configuration["Credentials:Password"]
            };

            users = new List<User>() { user };
        }

        List<User> users;

        public bool IsUser(string email, string password) =>
            users.Where(user => user.Email == email && user.Password == password).Count() > 0;
    }
}

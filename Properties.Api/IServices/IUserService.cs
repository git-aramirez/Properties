namespace Properties.Api.IServices
{
    public interface IUserService
    {
        public bool IsUser(string email, string password);
    }
}

namespace Properties.Core.IServices
{
    public interface IUserService
    {
        public bool IsUser(string email, string password);
    }
}

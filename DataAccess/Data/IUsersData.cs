namespace DataAccess.Data;

public interface IUsersData
{
    Task<UsersModel> AuthenticateUser(LoginModel user);
    Task<bool> CreateUser(UsersModel user);
}

using System.Security.Cryptography;
using System.Text;

namespace DataAccess.Data;
public class UsersData : IUsersData
{
    private readonly ISqlAccess _db;

    public UsersData(ISqlAccess db)
    {
        _db = db;
    }

    private static string Hash(string password)
    {
        string salt = @"PSv6OGMYhEeZ7t/ZOOmKjxok4GlQweUlYwxMTjCfi+4=";
        var sha = new HMACSHA256(Encoding.UTF8.GetBytes(salt + password));
        byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
        return System.Convert.ToBase64String(hash);
    }

    public async Task<bool> CreateUser(UsersModel user)
    {
        string sql = @"INSERT INTO users (name, email, password, created_at)
                       VALUES (@Name, @Email, @Password, CURRENT_TIMESTAMP)";
        var param = new
        {
            user.Name,
            user.Email,
            Password = Hash(user.Password!)
        };
        return await _db.WriteSqlDataCheckSuccess(sql, param);
    }

    public async Task<UsersModel?> AuthenticateUser(LoginModel user)
    {
        string sql = @"SELECT id, name, email, password, created_at CreatedAt, updated_at UpdatedAt
                       FROM users
                       WHERE email = @Email";
        var result = await _db.LoadSqlData<UsersModel, dynamic>(sql, user);

        UsersModel? authenticatedUser = null;
        if (result.Count() != 0)
        {
            if (result.Single().Password == Hash(user.Password!))
            {
                authenticatedUser = result.Single();
            }
            else
            {
                authenticatedUser = new UsersModel();
            }
        }

        return authenticatedUser;
    }
}

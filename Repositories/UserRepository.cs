// using SocialMedia.Repository;
using Dapper;
using SocialMedia.Models;
using SocialMedia.Utilities;

namespace SocialMedia.Repositories;

public interface IUserRepository
{
 Task<User> Create (User item);
 Task<bool> Update (User item);

 Task<User> GetById(long UserId);
 Task<List<User>> GetList();
}

public class UserRepository : BaseRepository, IUserRepository
{
    public UserRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<User> Create(User item)
    {

        var query = $@"INSERT INTO ""{TableNames.user}""
        ( user_name, date_of_birth, mobile, email, address)
        VALUES(@UserId, @DateOfBirth, @Mobile, @Email, @Address ) RETURNING *";
        using(var con = NewConnection){

            var res = await con.QuerySingleOrDefaultAsync<User>(query,item);
            return res;
            
        }
    }



    public async Task<User> GetById(long UserId)
    {
        var query = $@"SELECT * FROM ""{TableNames.user}""
        WHERE user_id = @UserId ";
        using(var con = NewConnection)
        return await con.QuerySingleOrDefaultAsync<User>(query,
        new{
            UserId
        });
         
    }

    public async Task<List<User>> GetList()
    {
        var query = $@"SELECT * FROM ""{TableNames.user}""";
        List<User>res;
        using (var con = NewConnection)
            res = (await con.QueryAsync<User>(query)).AsList();
        return res;
    }

    



    public async Task<bool> Update(User item)
    {

        var query = $@"UPDATE ""{TableNames.user}"" SET user_name = @UserName, address = @Address
        WHERE user_id = @UserId";

        using(var con = NewConnection){

            var rowCount = await con.ExecuteAsync(query,item);
            return rowCount == 1;

        }
        
    }
}
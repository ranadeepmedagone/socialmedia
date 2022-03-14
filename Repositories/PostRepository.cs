using Dapper;
using SocialMedia.Models;
using SocialMedia.Utilities;

namespace SocialMedia.Repositories;

public interface IPostRepository
{
 Task<Post> Create (Post item);
//  Task<bool> Update (Post item);
 Task <bool> Delete (long PostId);
 Task<Post> GetById(long PostId);
 Task<List<Post>> GetList();
 Task<List<Post>> GetListOfPost (long PostId);
    Task<List<Post>> GetList(long UserId);
    // Task GetList();
}

public class PostRepository : BaseRepository, IPostRepository
{
    public PostRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<Post> Create(Post item)
    {

        var query = $@"INSERT INTO ""{TableNames.post}""
        ( post_type, date_created, date_updated, user_id)
        VALUES(@PostType, @DateCreated, @DateUpdated, @UserId) RETURNING *";
        using(var con = NewConnection){

            var res = await con.QuerySingleOrDefaultAsync<Post>(query,item);
            return res;
            
        }
    }

    public async Task<bool> Delete(long PostId )
    {
        var query = $@"DELETE FROM ""{TableNames.post}""
        WHERE Post_id = @PostId";
        using(var con = NewConnection){

            var res = await con.ExecuteAsync(query, new{PostId});
            return res > 0;

        }
    }

    public async Task<Post> GetById(long PostId)
    {
        var query = $@"SELECT * FROM ""{TableNames.post}""
        WHERE Post_id = @PostId ";
        using(var con = NewConnection)
        return await con.QuerySingleOrDefaultAsync<Post>(query,
        new{
            PostId
        });
         
    }

    public async Task<List<Post>> GetList(long UserId)
    {
        var query = $@"SELECT * FROM ""{TableNames.post}"" WHERE user_id = @user_id";

        using (var con = NewConnection)
        {
            return (await con.QueryAsync<Post>(query, new { UserId })).AsList();
        }
    }

    public async Task<List<Post>> GetList()
    {
        var query = $@"SELECT * FROM ""{TableNames.post}""";
        List<Post>res;
        using (var con = NewConnection)
            res = (await con.QueryAsync<Post>(query)).AsList();
        return res;
    }

    public async Task<List<Post>> GetListOfPost(long PostId)
    {
        var query = $@"SELECT p.* 
        FROM {TableNames.post_hash} ph
        LEFT JOIN {TableNames.post} p
         ON p. post_id = ph.post_id
        WHERE ph.hash_id = @hash_id";

        using (var con = NewConnection)
        {
            return (await con.QueryAsync<Post>(query,new {PostId})).AsList();
        }
    }
}
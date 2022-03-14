using Dapper;
using SocialMedia.Models;
using SocialMedia.Utilities;

namespace SocialMedia.Repositories;

public interface ILikeRepository
{

 Task <bool> Delete (long LikeId);
 Task<Like> Create (Like item);
 Task<Like> GetById(long LikeId);
 Task<List<Like>> GetListOfLikes(long PostId);
}

public class LikeRepository : BaseRepository, ILikeRepository
{
    public LikeRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<Like> Create(Like item)
    {

        var query = $@"INSERT INTO ""{TableNames.like}""
        ( date_created,  user_id, post_id)
        VALUES(@DateCreated,  @UserId, @PostId) RETURNING *";
        using(var con = NewConnection){

            var res = await con.QuerySingleOrDefaultAsync<Like>(query,item);
            return res;
            
        }
    }

    public async Task<bool> Delete(long LikeId )
    {
        var query = $@"DELETE FROM ""{TableNames.like}""
        WHERE like_id = @LikeId";
        using(var con = NewConnection){

            var res = await con.ExecuteAsync(query, new{LikeId});
            return res > 0;

        }
    }

    public async Task<Like> GetById(long LikeId)
    {
        var query = $@"SELECT * FROM ""{TableNames.like}""
        WHERE like_id = @LikeId ";
        using(var con = NewConnection)
        return await con.QuerySingleOrDefaultAsync<Like>(query,
        new{
            LikeId
        });
         
    }

    public async Task<List<Like>> GetListOfLikes(long PostId)
    {
        var query = $@"SELECT * FROM ""{TableNames.post}"" WHERE post_id = @post_id";

        using (var con = NewConnection)
        {
            return (await con.QueryAsync<Like>(query, new { PostId })).AsList();
        }
    }

    // public async Task<List<Like>> GetList()
    // {
    //     var query = $@"SELECT * FROM ""{TableNames.like}""";
    //     List<Like>res;
    //     using (var con = NewConnection)
    //         res = (await con.QueryAsync<Like>(query)).AsList();
    //     return res;
    // }

}
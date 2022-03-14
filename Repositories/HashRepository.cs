using Dapper;
using SocialMedia.Models;
using SocialMedia.Utilities;

namespace SocialMedia.Repositories;

public interface IHashRepository
{
 Task<Hash> Create (Hash item);

 Task <bool> Delete (long HashId);
 Task<Hash> GetById (long HashId);
 Task<List<Hash>> GetList (long HashId);

}

public class HashRepository : BaseRepository, IHashRepository
{
    public HashRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<Hash> Create(Hash item)
    {

        var query = $@"INSERT INTO ""{TableNames.hash}""
        ( hash_name)
        VALUES(@HashName) RETURNING *";
        using(var con = NewConnection){

            var res = await con.QuerySingleOrDefaultAsync<Hash>(query,item);
            return res;
            
        }
    }

    public async Task<bool> Delete(long hashId )
    {
        var query = $@"DELETE FROM ""{TableNames.hash}""
        WHERE hash_id = @HashId";
        using(var con = NewConnection){

            var res = await con.ExecuteAsync(query, new{hashId});
            return res > 0;

        }
    }

    public async Task<Hash> GetById(long hashId)
    {
        var query = $@"SELECT * FROM ""{TableNames.hash}""
        WHERE hash_id = @HashId ";
        using(var con = NewConnection)
        return await con.QuerySingleOrDefaultAsync<Hash>(query,
        new{
            hashId
        });
         
    }

    public async Task<List<Hash>> GetList(long HashId)
    {
        var query = $@"SELECT h.* 
        FROM {TableNames.post_hash} ph
        LEFT JOIN {TableNames.hash} h
         ON h. hash_id = ph.hash_id
        WHERE ph.post_id = @post_id";

        using (var con = NewConnection)
        {
            return (await con.QueryAsync<Hash>(query,new {HashId})).AsList();
        }


    }
}  
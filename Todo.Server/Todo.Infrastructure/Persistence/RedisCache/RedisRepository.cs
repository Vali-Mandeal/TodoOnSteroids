namespace Todo.Infrastructure.Persistence.RedisCache;

using StackExchange.Redis;
using System.Threading.Tasks;
using Todo.Application.Contracts.RedisCache;

public class RedisRepository : IRedisRepository
{
    private static IDatabase _database;
    public RedisRepository(IDatabase database)
    {
        _database = database;
    }

    public async Task<string> GetStringValue(string key)
    {
        var result = await _database.StringGetAsync(key);

        if (result == "null")
            return null;

        return result;
    }
        
    public async Task SetStringValue(string key, string value)
    {
        await _database.StringSetAsync(key, value);
    }

    public async Task DeleteStringValue(string key)
    {
        await _database.KeyDeleteAsync(key);
    }
}

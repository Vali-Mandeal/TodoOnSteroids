namespace Todo.Application.Contracts.RedisCache;

public interface IRedisRepository
{
    Task<string> GetStringValue(string key);
    Task SetStringValue(string key, string value);
    Task DeleteStringValue(string key);
}   

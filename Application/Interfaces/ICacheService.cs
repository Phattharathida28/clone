using System;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface ICacheService
{
    Task SetAsync<T>(string key, T value, TimeSpan expiration = default);
    Task<T> GetAsync<T>(string key);
    Task<T> GetAsync<T>(string key, Func<Task<T>> dataRetriever, TimeSpan expiration = default);
    Task RemoveAsync(string key);
    Task FlushAsync();
}

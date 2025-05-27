using Application.Interfaces;
using EasyCaching.Core;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Services;

public class CacheService : ICacheService
{
    private readonly IEasyCachingProvider _provider;
    public CacheService(IEasyCachingProvider provider) => this._provider = provider;
    public async Task SetAsync<T>(string key, T value, TimeSpan expiration = default)
    {
        if (expiration == default) expiration = TimeSpan.FromMinutes(10);

        await this._provider.SetAsync(key, value, expiration).ConfigureAwait(false);
    }
    public async Task<T> GetAsync<T>(string key) => (await this._provider.GetAsync<T>(key).ConfigureAwait(false)).Value;
    public async Task<T> GetAsync<T>(string key, Func<Task<T>> dataRetriever, TimeSpan expiration = default)
    {
        if (expiration == default) expiration = TimeSpan.FromMinutes(10);
        return (await this._provider.GetAsync<T>(key, dataRetriever, expiration).ConfigureAwait(false)).Value;
    }
    public async Task RemoveAsync(string key) => await this._provider.RemoveAsync(key).ConfigureAwait(false);

    public async Task FlushAsync() => await this._provider.FlushAsync().ConfigureAwait(false);
}

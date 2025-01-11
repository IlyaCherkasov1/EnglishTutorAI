using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace EnglishTutorAI.Application.Services;

[SingletonDependency]
public class SingleEntryCache : ISingleEntryCache
{
    private readonly IMemoryCache _cache;
    private string? _currentKey;

    public SingleEntryCache(IMemoryCache cache)
    {
        _cache = cache;
    }

    public string? Get(string key)
    {
        if (_currentKey != key)
        {
            return null;
        }

        _cache.TryGetValue(key, out string? cachedValue);

        return cachedValue;
    }

    public void Set(string key, string value)
    {
        if (_currentKey != null && _currentKey != key)
        {
            _cache.Remove(_currentKey);
        }

        _cache.Set(key, value);
        _currentKey = key;
    }
}
# EasyCaching.AspectCore.Expression
An extension library for AspectCore based EasyCaching interceptor with simple expression supported to generate cache key from method parameters.

# Usage
```csharp
public class DataDictionaryItemInput
{
    public string Name { get; set; }
    public string Val { get; set; }
}
```

```csharp
public interface IDataDictionaryService
{
    [EasyCachingAble(CacheKeyPrefix = "dict:${0}", Expiration = 604800)]
    public List<DataDictionaryOutput> GetDictionary(string name);
    
    [EasyCachingAble(CacheKeyPrefix = "dict:${0:Name}:${0:Val}", Expiration = 3600)]
    public string GetDicText(DataDictionaryItemInput input);
    
    [EasyCachingDel(CacheKeyPrefix = "dict:${0:Name}")]
    [EasyCachingDel(CacheKeyPrefix = "dict:${0:Name}:${0:Val}")]
    public Result UpdateDataDictionary(DataDictionaryUpdate vm);
}
```

# EasyCachingDelAttribute
`EasyCachingDelAttribute` is same as `EasyCachingEvictAttribute` from EasyCaching except that it can be used multiple times on single method.

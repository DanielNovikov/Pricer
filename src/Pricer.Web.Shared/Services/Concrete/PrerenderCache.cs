using Microsoft.JSInterop;
using Newtonsoft.Json;
using Pricer.Web.Shared.Services.Abstract;

namespace Pricer.Web.Shared.Services.Concrete;

public class PrerenderCache : IPrerenderCache
{
    private readonly IJSRuntime _jsRuntime;
    private IDictionary<string, string> _data;

    public PrerenderCache(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
        _data = new Dictionary<string, string>();
    }

    public async Task<TResult> GetOrAdd<TResult>(string key, Func<Task<TResult>> dataFactory)
    {
        if (IsRunningOnServer)
        {
            var dataResult = await dataFactory() ?? 
                throw new ArgumentNullException($"Unable load data for key {key}");
            
            _data[key] = JsonConvert.SerializeObject(dataResult);
            
            return dataResult;
        }
        
        _data = await _jsRuntime.InvokeAsync<Dictionary<string, string>>("prerenderCache.load");
        
        if (_data.Remove(key, out var objJson))
            return JsonConvert.DeserializeObject<TResult>(objJson);

        return await dataFactory();
    }

    public string Serialize()
    {
        return JsonConvert.SerializeObject(_data);
    }
    
    private bool IsRunningOnServer => 
        _jsRuntime.GetType().Name == "UnsupportedJavaScriptRuntime";
}
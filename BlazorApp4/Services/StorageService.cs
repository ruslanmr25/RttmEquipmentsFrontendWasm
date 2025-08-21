using System;
using Microsoft.JSInterop;

namespace BlazorApp4.Services;

public class StorageService(IJSRuntime jSRuntime)
{
    protected IJSRuntime JS = jSRuntime;

    public async Task SaveToSessionStorage(string key, string value)
    {
        await JS.InvokeVoidAsync("storageFunctions.setSessionItem", key, value);
    }

    public async Task<string?> ReadFromSessionStorage(string key)
    {
        return await JS.InvokeAsync<string>("storageFunctions.getSessionItem", key);
    }

    public async Task RemoveFromSessionStorage(string key)
    {
        await JS.InvokeVoidAsync("storageFunctions.removeSessionItem", key);
    }

    public async Task SaveToLocalStorage(string key, string value)
    {
        await JS.InvokeVoidAsync("storageFunctions.setSessionItem", key, value);
    }

    public async Task<string?> ReadFromLocalStorage(string key)
    {
        return await JS.InvokeAsync<string>("storageFunctions.getSessionItem", key);
    }

    public async Task RemoveFromLocalStorage(string key)
    {
        await JS.InvokeVoidAsync("storageFunctions.removeSessionItem", key);
    }
}

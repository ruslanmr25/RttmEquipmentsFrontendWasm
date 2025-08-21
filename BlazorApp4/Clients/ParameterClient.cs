using System;
using BlazorApp4.Models.Parameters;
using BlazorApp4.Services;

namespace BlazorApp4.Clients;

public class ParameterClient : BaseClient<Parameter>
{
    public ParameterClient(HttpClient httpClient, StorageService storageService)
        : base(httpClient, "/api/parameters", storageService) { }
}

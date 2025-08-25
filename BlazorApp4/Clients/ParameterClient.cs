using System;
using BlazorApp4.Models.Parameters;
using BlazorApp4.Services;

namespace BlazorApp4.Clients;

public class ParameterClient : BaseClient<Parameter>
{
    public ParameterClient(IHttpClientFactory factory, StorageService storageService)
        : base(factory, "/api/parameters", storageService) { }
}

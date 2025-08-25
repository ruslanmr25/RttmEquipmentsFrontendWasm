using System;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BlazorApp4.Models;
using BlazorApp4.Models.Auth;
using BlazorApp4.Models.UserModels;
using BlazorApp4.Responses;
using BlazorApp4.Services;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorApp4.Clients;

public class AuthClient(IHttpClientFactory factory, StorageService storageService)
{
    protected readonly HttpClient _client = factory.CreateClient("ApiClient");

    private readonly StorageService _storageService = storageService;

    protected readonly string loginUrl = "/api/login";

    public async Task<Response<LoginData>> Login(LoginDto loginDto)
    {
        var response = await _client.PostAsJsonAsync(loginUrl, loginDto);
        if ((int)response.StatusCode >= 500)
        {
            var serverMessage = await response.Content.ReadAsStringAsync();
            throw new ApplicationException($"Server xatosi (500+): {serverMessage}");
        }

        var result = await response.Content.ReadFromJsonAsync<Response<LoginData>>();

        result!.StatusCode = (int)response.StatusCode;
        return result!;
    }

    public async Task<Response<User>> Me()
    {
        var token = await _storageService.ReadFromLocalStorage("token");

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Bearer",
            token
        );

        var response = await _client.GetFromJsonAsync<Response<User>>("/api/me");

        return response!;
    }
}

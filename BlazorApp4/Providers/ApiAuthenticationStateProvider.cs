using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using BlazorApp4.Clients;
using BlazorApp4.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace BlazorApp4.Providers;

public class ApiAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly HttpClient _client;

    private readonly StorageService _storageService;

    private readonly AuthClient _authClient;

    public ApiAuthenticationStateProvider(
        HttpClient httpClient,
        StorageService storageService,
        AuthClient authClient
    )
    {
        _client = httpClient;
        _storageService = storageService;
        _authClient = authClient;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _storageService.ReadFromLocalStorage("token");

        if (string.IsNullOrWhiteSpace(token))
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        try
        {
#warning JWT ni public key orqali verifikatsiya qilish kerak

#warning Refresh token qo'shish kerak

            var data = ParseClaimsFromJwt(token);

            var identity = new ClaimsIdentity(
                new[]
                {
                    new Claim(ClaimTypes.Name, data!["fullName"].ToString()!),
                    new Claim("username", data["username"].ToString()!),
                    new Claim(ClaimTypes.Role, data["role"].ToString()!),
                },
                "Bearer"
            );

            var principal = new ClaimsPrincipal(identity);
            return new AuthenticationState(principal);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Nimadir xato ketti:{e}");
        }

        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    public async Task MarkUserAsAuthenticated(string token)
    {
        await _storageService.SaveToLocalStorage("token", token);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Bearer",
            token
        );
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task MarkUserAsLoggedOut()
    {
        await _storageService.RemoveFromLocalStorage("token");
        _client.DefaultRequestHeaders.Authorization = null;
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    private Dictionary<string, object>? ParseClaimsFromJwt(string jwt)
    {
        var payload = jwt.Split('.')[1];
        var jsonBytes = Convert.FromBase64String(PadBase64(payload));
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        return keyValuePairs;
    }

    private string PadBase64(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2:
                base64 += "==";
                break;
            case 3:
                base64 += "=";
                break;
        }
        return base64.Replace('-', '+').Replace('_', '/');
    }
}

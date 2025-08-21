using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BlazorApp4.Models;
using BlazorApp4.Models.Auth;
using BlazorApp4.Responses;

namespace BlazorApp4.Clients;

public class AuthClient(HttpClient httpClient)
{
    protected readonly HttpClient _client = httpClient;

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
}

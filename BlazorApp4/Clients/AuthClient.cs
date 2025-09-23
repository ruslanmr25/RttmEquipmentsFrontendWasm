using System.Net.Http.Headers;
using System.Net.Http.Json;
using BlazorApp4.Models.Auth;
using BlazorApp4.Models.UserModels;
using BlazorApp4.Responses;
using BlazorApp4.Services;

namespace BlazorApp4.Clients;

public class AuthClient(IHttpClientFactory factory, StorageService storageService)
{
    protected readonly HttpClient _client = factory.CreateClient("ApiClient");
    private readonly StorageService _storageService = storageService;

    protected readonly string loginUrl = "/api/login";
    protected readonly string registerUrl = "/api/register";
    protected readonly string logoutUrl = "/api/logout";

    public async Task<Response<LoginData>> Login(LoginDto loginDto)
    {
        try
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
        catch (HttpRequestException)
        {
            Response<LoginData> response = new();
            response.Message = "Serverga ulanishda xatolik iltimos keyinroq urinib ko'ring!";

            return response;
        }
    }

    public async Task<Response<LoginData>> Register(LoginDto loginDto)
    {
        var response = await _client.PostAsJsonAsync(registerUrl, loginDto);
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
        var response = await _client.GetFromJsonAsync<Response<User>>("/api/me");

        return response!;
    }

    public async Task Logout()
    {
        await _client.PostAsJsonAsync(logoutUrl, new { });
    }
}

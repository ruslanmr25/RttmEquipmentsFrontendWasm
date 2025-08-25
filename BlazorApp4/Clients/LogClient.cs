using System;
using System.Net.Http.Json;
using System.Text.Json;
using BlazorApp4.Models.Logs;
using BlazorApp4.Responses;
using BlazorApp4.Services;

namespace BlazorApp4.Clients;

public class LogClient(IHttpClientFactory factory, StorageService storageService)
    : BaseClient<Log>(factory, "/api/logs", storageService)
{
    public async Task<Response<Log>> GetAsync(string model, int id)
    {
     
        try
        {
            var response = await _client.GetFromJsonAsync<Response<Log>>(
                $"{this.url}/{model}/{id}"
            );
            return response ?? new Response<Log>();
        }
        catch (NotSupportedException ex)
        {
            Console.WriteLine($"Noto‘g‘ri format: {ex.Message}");
            return new Response<Log> { Success = false, Message = "Ma’lumot formatida xatolik" };
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"JSON xato: {ex.Message}");
            return new Response<Log> { Success = false, Message = "JSON o‘qishda xatolik" };
        }
    }
}

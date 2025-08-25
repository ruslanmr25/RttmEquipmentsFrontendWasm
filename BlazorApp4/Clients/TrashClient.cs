using System;
using System.Net.Http.Json;
using System.Text.Json;
using BlazorApp4.Models;
using BlazorApp4.Responses;
using BlazorApp4.Services;

namespace BlazorApp4.Clients;

public class TrashClient(IHttpClientFactory factory, StorageService storageService)
    : BaseClient<Equipment>(factory, "/api/trashes", storageService)
{
    public override async Task<Response<List<Equipment>>> GetAllAsync()
    {
        try
        {
            var response = await _client.GetFromJsonAsync<Response<List<Equipment>>>(url);
            return response ?? new Response<List<Equipment>>();
        }
        catch (NotSupportedException ex)
        {
            Console.WriteLine($"Noto‘g‘ri format: {ex.Message}");
            return new Response<List<Equipment>>
            {
                Success = false,
                Message = "Ma’lumot formatida xatolik",
            };
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"JSON xato: {ex.Message}");
            return new Response<List<Equipment>>
            {
                Success = false,
                Message = "JSON o‘qishda xatolik",
            };
        }
    }
}

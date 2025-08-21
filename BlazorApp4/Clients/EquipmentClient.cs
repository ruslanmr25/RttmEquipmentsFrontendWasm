using System;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorApp4.Models;
using BlazorApp4.Responses;
using BlazorApp4.Services;

namespace BlazorApp4.Clients;

public class EquipmentClient(HttpClient httpClient, StorageService storageService)
    : BaseClient<Equipment>(httpClient, "/api/equipments", storageService)
{
    public override async Task<Response<List<Equipment>>> GetAllAsync()
    {
        await BeforeSend();

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

    public async Task<Response<Equipment>> UploadFile(MultipartFormDataContent content)
    {
        await BeforeSend();

        HttpResponseMessage response = await _client.PostAsync("/api/upload", content);

        var result = await response.Content.ReadFromJsonAsync<Response<Equipment>>();

        result!.StatusCode = (int)response.StatusCode;

        return result!;
    }

    public async Task<Response<object>> DeleteAsync(int Id, MultipartFormDataContent content)
    {
        var response = await _client.PostAsync($"{this.url}/{Id}/delete", content);

        var result = await response.Content.ReadFromJsonAsync<Response<object>>();

        result!.Success = response.IsSuccessStatusCode;

        return result!;
    }
}

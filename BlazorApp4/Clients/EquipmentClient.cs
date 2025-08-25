using System;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorApp4.Models;
using BlazorApp4.Responses;
using BlazorApp4.Services;

namespace BlazorApp4.Clients;

public class EquipmentClient(IHttpClientFactory factory, StorageService storageService)
    : BaseClient<Equipment>(factory, "/api/equipments", storageService)
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

    public virtual async Task<Response<List<Equipment>>> GetMyEquipments<TParam>(TParam searchParam)
    {
        string query = QueryStringHelper.ToQueryString(searchParam);

        try
        {
            var response = await _client.GetFromJsonAsync<Response<List<Equipment>>>(
                $"{this.url}{query}"
            );
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

    public async Task<Response<List<Equipment>>> GetMyEquipments()
    {
        try
        {
            var response = await _client.GetFromJsonAsync<Response<List<Equipment>>>(
                "/api/my-equipments"
            );
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

    public async Task<Response<Equipment>> SendEquipmentTo(int userId, List<Equipment> equipments)
    {
        var body = new
        {
            UserId = userId,
            Equipments = equipments.Select(equipment => equipment.Id).ToArray(),
        };

        var response = await _client.PutAsJsonAsync($"{this.url}/sendto", body);

        response.EnsureSuccessStatusCode(); // 200 OK bo‘lmasa exception

        var result = await response.Content.ReadFromJsonAsync<Response<Equipment>>();

        if (result is null)
            throw new Exception("API bo‘sh javob qaytardi");

        return result;
    }
}

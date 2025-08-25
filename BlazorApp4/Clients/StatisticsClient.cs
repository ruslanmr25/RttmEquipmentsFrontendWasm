using System;
using System.Net.Http.Json;
using System.Text.Json;
using BlazorApp4.Models.Statistics;
using BlazorApp4.Responses;
using BlazorApp4.Services;

namespace BlazorApp4.Clients;

public class StatisticsClient
{
    protected readonly HttpClient _client;

    protected readonly string url;

    protected readonly StorageService _storageService;

    public StatisticsClient(IHttpClientFactory factory, StorageService storageService)
    {
        _client = factory.CreateClient("ApiClient");

        this.url = "/api/statistics";
        _storageService = storageService;
    }

    protected async Task BeforeSend()
    {
        string? token = await _storageService.ReadFromLocalStorage("token");
        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token ?? "token");
    }

    public virtual async Task<Response<StatisticData>> GetAllAsync<TParam>(TParam searchParam)
    {
        await BeforeSend();

        string query = QueryStringHelper.ToQueryString(searchParam);

        try
        {
            var response = await _client.GetFromJsonAsync<Response<StatisticData>>(
                $"{this.url}{query}"
            );
            return response ?? new Response<StatisticData>();
        }
        catch (NotSupportedException ex)
        {
            Console.WriteLine($"Noto‘g‘ri format: {ex.Message}");
            return new Response<StatisticData>
            {
                Success = false,
                Message = "Ma’lumot formatida xatolik",
            };
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"JSON xato: {ex.Message}");
            return new Response<StatisticData>
            {
                Success = false,
                Message = "JSON o‘qishda xatolik",
            };
        }
    }

    public virtual async Task<Response<StatisticData>> GetAllAsync()
    {
        await BeforeSend();

        try
        {
            var response = await _client.GetFromJsonAsync<Response<StatisticData>>(this.url);
            return response ?? new Response<StatisticData>();
        }
        catch (NotSupportedException ex)
        {
            Console.WriteLine($"Noto‘g‘ri format: {ex.Message}");
            return new Response<StatisticData>
            {
                Success = false,
                Message = "Ma’lumot formatida xatolik",
            };
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"JSON xato: {ex.Message}");
            return new Response<StatisticData>
            {
                Success = false,
                Message = "JSON o‘qishda xatolik",
            };
        }
    }
}

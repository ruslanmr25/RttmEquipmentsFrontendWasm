using System;
using System.Collections;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using BlazorApp4.Responses;
using BlazorApp4.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorApp4.Clients;

public class BaseClient<T>
{
    protected readonly HttpClient _client;

    protected readonly string url;

    protected readonly StorageService _storageService;

    public BaseClient(IHttpClientFactory factory, string url, StorageService storageService)
    {
        _client = factory.CreateClient("ApiClient");

        if (string.IsNullOrWhiteSpace(url))
        {
            throw new ArgumentException("URL qiymati required", nameof(url));
        }

        this.url = url;
        _storageService = storageService;
    }

    public virtual async Task<Response<List<T>>> GetAllAsync()
    {
        try
        {
            var response = await _client.GetFromJsonAsync<Response<List<T>>>(this.url);
            return response ?? new Response<List<T>>();
        }
        catch (NotSupportedException ex)
        {
            Console.WriteLine($"Noto‘g‘ri format: {ex.Message}");
            return new Response<List<T>>
            {
                Success = false,
                Message = "Ma’lumot formatida xatolik",
            };
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"JSON xato: {ex.Message}");
            return new Response<List<T>> { Success = false, Message = "JSON o‘qishda xatolik" };
        }
    }

    public virtual async Task<Response<List<T>>> GetAllAsync<TParam>(TParam searchParam)
    {
        string query = QueryStringHelper.ToQueryString(searchParam);

        try
        {
            var response = await _client.GetFromJsonAsync<Response<List<T>>>($"{this.url}{query}");
            return response ?? new Response<List<T>>();
        }
        catch (NotSupportedException ex)
        {
            Console.WriteLine($"Noto‘g‘ri format: {ex.Message}");
            return new Response<List<T>>
            {
                Success = false,
                Message = "Ma’lumot formatida xatolik",
            };
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"JSON xato: {ex.Message}");
            return new Response<List<T>> { Success = false, Message = "JSON o‘qishda xatolik" };
        }
    }

    public virtual async Task<Response<T>> CreateAsync<TDto>(TDto entity)
    {
        var httpResponse = await _client.PostAsJsonAsync(url, entity);

        var response = await httpResponse.Content.ReadFromJsonAsync<Response<T>>();

        return response!;
    }

    public virtual async Task<Response<T>> GetAsync(int id)
    {
        try
        {
            var response = await _client.GetFromJsonAsync<Response<T>>($"{this.url}/{id}");
            return response ?? new Response<T>();
        }
        catch (NotSupportedException ex)
        {
            Console.WriteLine($"Noto‘g‘ri format: {ex.Message}");
            return new Response<T> { Success = false, Message = "Ma’lumot formatida xatolik" };
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"JSON xato: {ex.Message}");
            return new Response<T> { Success = false, Message = "JSON o‘qishda xatolik" };
        }
    }

    public virtual async Task<Response<T>> UpdateAsync<TDto>(int id, TDto entity)
    {
        var httpResponse = await _client.PutAsJsonAsync($"{url}/{id}", entity);

        // 2. Javobni o‘qiymiz va deserializatsiya qilamiz
        var response = await httpResponse.Content.ReadFromJsonAsync<Response<T>>();

        return response!;
    }
}

public static class QueryStringHelper
{
    public static string ToQueryString<Tparam>(Tparam obj)
    {
        if (obj == null)
            return string.Empty;

        var query = new List<string>();

        foreach (
            var prop in typeof(Tparam).GetProperties( // <<< BU YER TO‘G‘RILANDI
                System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance
            )
        )
        {
            var value = prop.GetValue(obj);

            // null yoki bo‘sh string bo‘lsa, o‘tkazib yuboriladi
            if (value == null || (value is string str && string.IsNullOrWhiteSpace(str)))
                continue;

            if (value is IEnumerable enumerable && !(value is string))
            {
                foreach (var item in enumerable)
                {
                    if (item != null)
                        query.Add(
                            $"{ToCamelCase(prop.Name)}={WebUtility.UrlEncode(item.ToString())}"
                        );
                }
            }
            else
            {
                query.Add($"{ToCamelCase(prop.Name)}={WebUtility.UrlEncode(value.ToString())}");
            }
        }

        return query.Count > 0 ? "?" + string.Join("&", query) : string.Empty;
    }

    private static string ToCamelCase(string input)
    {
        if (string.IsNullOrEmpty(input) || char.IsLower(input[0]))
            return input;
        return char.ToLower(input[0]) + input.Substring(1);
    }
}

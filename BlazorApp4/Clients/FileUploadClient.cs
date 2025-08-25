using System;
using System.Net.Http.Json;
using BlazorApp4.Models.Images;
using BlazorApp4.Responses;
using BlazorApp4.Services;

namespace BlazorApp4.Clients;

public class FileUploadClient
{
    protected readonly HttpClient _client;

    protected readonly StorageService _storageService;

    protected readonly string url = "/images/upload";

    public FileUploadClient(IHttpClientFactory factory, StorageService storageService)
    {
        _client = factory.CreateClient("ApiClient");

        _storageService = storageService;
    }

    protected async Task BeforeSend()
    {
        string? token = await _storageService.ReadFromLocalStorage("token");
        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token ?? "token");
    }

    public async Task<Response<Image>> SendBase64Photo(CreateImageDto createImageDto)
    {
        await BeforeSend();
        var httpResponse = await _client.PostAsJsonAsync(url, createImageDto);

        var response = await httpResponse.Content.ReadFromJsonAsync<Response<Image>>();

        return response!;
    }

    public async Task<Response<Image>> SendPhoto(int id, MultipartFormDataContent content)
    {
        await BeforeSend();

        HttpResponseMessage response = await _client.PostAsync($"/api/images/{id}/upload", content);

        var result = await response.Content.ReadFromJsonAsync<Response<Image>>();

        result!.StatusCode = (int)response.StatusCode;

        return result!;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var response = await _client.DeleteAsync($"/api/images/{id}/delete");

        return response.IsSuccessStatusCode;
    }
}

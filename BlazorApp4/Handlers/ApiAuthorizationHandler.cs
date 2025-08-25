using System;
using BlazorApp4.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorApp4.Handlers;

public class ApiAuthorizationHandler : DelegatingHandler
{
    protected readonly StorageService _storageService;
    protected readonly ToastrService _toastrService;
    private readonly NavigationManager _navigation;

    public ApiAuthorizationHandler(
        NavigationManager navigation,
        StorageService storageService,
        ToastrService toastrService
    )
    {
        _navigation = navigation;
        _storageService = storageService;
        _toastrService = toastrService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            string? token = await _storageService.ReadFromLocalStorage("token");
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) // 401
            {
                Console.WriteLine("shu yerdan o'tdi");
                _navigation.NavigateTo("/login", false);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden) // 403
            {
                _navigation.NavigateTo("/forbidden", false);
            }

            return response;
        }
        catch (HttpRequestException ex)
        {
            // Bu joyda "Failed to fetch", "Connection refused" kabi xatolarni tutasan
            _toastrService.ShowError("Serverga ulanib bo'lmadi");

            // Masalan foydalanuvchini error sahifaga yuborish mumkin
            _navigation.NavigateTo("/server-down", false);

            // Bo'sh response qaytarish (yoki custom response yasash)
            return new HttpResponseMessage(System.Net.HttpStatusCode.ServiceUnavailable)
            {
                ReasonPhrase = "Server bilan bog‘lanib bo‘lmadi",
            };
        }
    }
}

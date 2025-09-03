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
            // Tokenni headerga qo‘shish
            string? token = await _storageService.ReadFromLocalStorage("token");
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            var response = await base.SendAsync(request, cancellationToken);

            // 🔹 Status kodlarni tekshirish
            if (!response.IsSuccessStatusCode)
            {
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.Unauthorized: // 401
                        _toastrService.ShowWarning("Avtorizatsiya kerak. Qayta login qiling.");
                        _navigation.NavigateTo("/login", forceLoad: false);
                        break;

                    case System.Net.HttpStatusCode.Forbidden: // 403
                        _toastrService.ShowError("Sizda ruxsat yo‘q.");
                        _navigation.NavigateTo("/forbidden", forceLoad: false);
                        break;

                    case System.Net.HttpStatusCode.NotFound: // 404
                        _toastrService.ShowError("Resurs topilmadi.");
                        break;

                    case System.Net.HttpStatusCode.InternalServerError: // 500
                        _toastrService.ShowError("Serverdagi xatolik.");
                        break;

                    default:
                        _toastrService.ShowInfo($"Xato status kodi: {(int)response.StatusCode}");
                        break;
                }
            }

            return response;
        }
        catch (HttpRequestException ex)
        {
            // 🔹 Brauzer tarmoq yoki CORS xatolari ("Failed to fetch")
            _toastrService.ShowError("Serverga ulanishda muammo!");

            Console.WriteLine($"HttpRequestException: {ex.Message}");

            // Xatolikdan keyin "fake" response qaytaryapmiz
            return new HttpResponseMessage(System.Net.HttpStatusCode.ServiceUnavailable)
            {
                Content = new StringContent("Serverga ulanishda muammo"),
            };
        }
        catch (TaskCanceledException ex)
        {
            // 🔹 Timeout bo‘lsa
            _toastrService.ShowError("So‘rov juda uzoq davom etdi (timeout).");
            Console.WriteLine($"TaskCanceledException: {ex.Message}");

            return new HttpResponseMessage(System.Net.HttpStatusCode.RequestTimeout)
            {
                Content = new StringContent("So‘rov vaqti tugadi"),
            };
        }
    }
}

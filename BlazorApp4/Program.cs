using BlazorApp4;
using BlazorApp4.Clients;
using BlazorApp4.Handlers;
using BlazorApp4.Providers;
using BlazorApp4.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Auth & state
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();

// Custom handler
builder.Services.AddTransient<ApiAuthorizationHandler>();

// HttpClient factory bilan sozlash
builder
    .Services.AddHttpClient(
        "ApiClient",
        client =>
        {
            client.BaseAddress = new Uri("https://equipments-api.samdu.uz/");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        }
    )
    .AddHttpMessageHandler<ApiAuthorizationHandler>();

// Default HttpClient sifatida berib qo‘yish
builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IHttpClientFactory>().CreateClient("ApiClient")
);

// Barcha client’lar shu HttpClient’dan foydalanadi
builder.Services.AddScoped<CategoryClient>();
builder.Services.AddScoped<EquipmentTypeClient>();
builder.Services.AddScoped<AuthClient>();
builder.Services.AddScoped<EquipmentClient>();
builder.Services.AddScoped<UserClient>();
builder.Services.AddScoped<DepartmentClient>();
builder.Services.AddScoped<RoomClient>();
builder.Services.AddScoped<ParameterClient>();
builder.Services.AddScoped<TrashClient>();
builder.Services.AddScoped<LogClient>();
builder.Services.AddScoped<FileUploadClient>();
builder.Services.AddScoped<StatisticsClient>();

// Boshqa servislar
builder.Services.AddScoped<CameraService>();
builder.Services.AddScoped<ToastrService>();

builder.Services.AddSingleton<AppStateService>();

// Storage service
builder.Services.AddSingleton(sp =>
{
    var js = sp.GetRequiredService<IJSRuntime>();
    return new StorageService(js);
});

await builder.Build().RunAsync();

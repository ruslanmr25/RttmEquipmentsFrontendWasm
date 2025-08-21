using BlazorApp4;
using BlazorApp4.Clients;
using BlazorApp4.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp =>
{
    var _client = new HttpClient { BaseAddress = new Uri("http://localhost:8000/") };
    _client.DefaultRequestHeaders.Add("Accept", "application/json");
    return _client;
});

builder.Services.AddScoped<CategoryClient>();
builder.Services.AddScoped<EquipmentTypeClient>();
builder.Services.AddScoped<AuthClient>();
builder.Services.AddScoped<EquipmentClient>();

builder.Services.AddScoped<CameraService>();

builder.Services.AddScoped<UserClient>();

builder.Services.AddScoped<ToastrService>();

builder.Services.AddScoped<DepartmentClient>();
builder.Services.AddScoped<RoomClient>();
builder.Services.AddScoped<ParameterClient>();
builder.Services.AddScoped<TrashClient>();
builder.Services.AddScoped<LogClient>();
builder.Services.AddScoped<FileUploadClient>();
builder.Services.AddScoped<StatisticsClient>();

builder.Services.AddSingleton(sp =>
{
    var js = sp.GetRequiredService<IJSRuntime>();
    return new StorageService(js);
});

await builder.Build().RunAsync();

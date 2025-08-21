using System;
using Microsoft.JSInterop;

namespace BlazorApp4.Services;

public class CameraService
{
    private string currentFacing = "user";

    public string? VideoId { get; set; }

    public string? CanvasId { get; set; }

    public string? ImageId { get; set; }

    private readonly IJSRuntime _js;

    public CameraService(IJSRuntime js)
    {
        _js = js;
    }

    public void Initialize(string canvasId, string imageId, string video)
    {
        CanvasId = canvasId;
        ImageId = imageId;
        VideoId = video;
    }

    public async Task StartCameraAsync()
    {
        await _js.InvokeVoidAsync("cameraFunctions.startCamera", VideoId, currentFacing);
    }

    public async Task ChangeCameraAsync()
    {
        currentFacing = currentFacing == "user" ? "environment" : "user";
     
        await StartCameraAsync();
    }

    public async Task<string> TakePhotoAsync()
    {
        return await _js.InvokeAsync<string>(
            "cameraFunctions.takePhoto",
            VideoId,
            CanvasId,
            ImageId
        );
    }

    public async Task StopCameraAsync()
    {
        await _js.InvokeVoidAsync("cameraFunctions.stopCamera", VideoId);
    }
}

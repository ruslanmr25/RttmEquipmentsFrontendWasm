using System;
using System.Text.Json;
using BlazorApp4.Models;

namespace BlazorApp4.Responses;

public class Response<T>
{
    public int StatusCode { get; set; } = 200;
    public bool Success { get; set; } = true;

    public string? Message { get; set; }
    public T? Data { get; set; }

    public Dictionary<string, JsonElement>? Errors { get; set; }
    public Meta? Meta { get; set; }
}

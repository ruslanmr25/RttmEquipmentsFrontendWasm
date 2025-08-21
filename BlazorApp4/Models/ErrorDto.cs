using System;

namespace BlazorApp4.Models;

public class Error
{
    public Dictionary<string, string[]> Errors { get; set; } = new();
}

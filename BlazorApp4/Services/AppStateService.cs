using System;
using BlazorApp4.Models;

namespace BlazorApp4.Services;

public class AppStateService
{
    public string Message { get; set; } = "salom";
    public List<Equipment>? Equipments { get; set; }
}

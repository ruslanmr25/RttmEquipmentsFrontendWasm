using System;
using BlazorApp4.Models;
using BlazorApp4.Models.Positions;

namespace BlazorApp4.Models.UserModels;

public class User
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public required string FullName { get; set; }

    public string? Phone { get; set; }

    public Role? Role { get; set; }

    public List<Permission>? Permissions { get; set; }

    public List<Equipment>? Equipments { get; set; }

    public Position? Position { get; set; }

    public int? EquipmentsCount { get; set; }
}

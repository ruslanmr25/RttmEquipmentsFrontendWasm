using System;
using BlazorApp4.Models.Rooms;
using BlazorApp4.Models.UserModels;

namespace BlazorApp4.Models.Departments;

public class Department
{
    public int? Id { get; set; }

    public string? Name { get; set; }

    public List<Room>? Rooms { get; set; }

    public User? Guard { get; set; }

    public int? EquipmentsCount { get; set; }

    public List<Equipment>? Equipments { get; set; }
    public int? RoomsCount { get; set; }
}

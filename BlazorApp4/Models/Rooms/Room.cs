using System;
using BlazorApp4.Models.Departments;

namespace BlazorApp4.Models.Rooms;

public class Room
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public Department? Department { get; set; }
    public int? EquipmentsCount { get; set; }
}

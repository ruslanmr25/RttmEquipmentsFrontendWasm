using System;
using BlazorApp4.Models.Departments;
using BlazorApp4.Models.Images;
using BlazorApp4.Models.Parameters;
using BlazorApp4.Models.Rooms;
using BlazorApp4.Models.UserModels;

namespace BlazorApp4.Models;

public class Equipment
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public long? InvertarId { get; set; }
    public User? User { get; set; }
    public EquipmentType? EquipmentType { get; set; }
    public int? RoomId { get; set; } // nullable int
    public int DepartmentId { get; set; }
    public string? MacAddress { get; set; } // nullable string

    public Department? Department { get; set; }

    public Room? Room { get; set; }

    public List<Image>? Images { get; set; }

    public File? DeletedItemFile { get; set; }

    public DateTime? DeletedAt { get; set; }

    public List<Parameter>? Parameters { get; set; }
}

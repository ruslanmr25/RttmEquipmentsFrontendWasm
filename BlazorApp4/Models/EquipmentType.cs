using System;
using BlazorApp4.Models.Parameters;

namespace BlazorApp4.Models;

public class EquipmentType
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public int? EquipmentsCount { get; set; }

    public Category? Category { get; set; }

    public List<Parameter>? Parameters { get; set; }
}

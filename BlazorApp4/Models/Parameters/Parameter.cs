using System;

namespace BlazorApp4.Models.Parameters;

public class Parameter
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public EquipmentType? EquipmentType { get; set; }
}

using System;

namespace BlazorApp4.Models.Equipments;

public class EquipmentSearchParameters
{
    public string? Name { get; set; }

    public int? InvertarId { get; set; }

    public int? UserId { get; set; }

    public int? DepartmentId { get; set; }

    public int? RoomId { get; set; }

    public int? CategoryId { get; set; }

    public int? EquipmentTypeId { get; set; }

    public List<int>? Parameters { get; set; }

    public int? Page { get; set; }
}

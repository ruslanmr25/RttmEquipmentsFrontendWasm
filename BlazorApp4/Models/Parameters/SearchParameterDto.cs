using System;

namespace BlazorApp4.Models.Parameters;

public class SearchParameterDto
{
    public string? Name { get; set; }

    public int? EquipmentTypeId { get; set; }
    public int? Page { get; set; }
}

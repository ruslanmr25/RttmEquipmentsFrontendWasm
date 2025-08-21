using System;
using BlazorApp4.Models.Divisions;

namespace BlazorApp4.Models.Positions;

public class Position
{
    public int? Id { get; set; }
    public string? Name { get; set; }

    public Division? Division { get; set; }
}

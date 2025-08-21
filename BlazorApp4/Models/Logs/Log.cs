using System;

namespace BlazorApp4.Models.Logs;

public class Log
{
    public int Id { get; set; }
    public required string Level { get; set; }
    public required string LevelName { get; set; }
    public required string Agent { get; set; }
    public required string Role { get; set; }
    public required string Model { get; set; }
    public int ModelId { get; set; }
    public required string Action { get; set; }
    public required string Message { get; set; }
    public object? Changes { get; set; } // array yoki string bo'lishi mumkin
    public object? Original { get; set; } // object yoki null bo'lishi mumkin
    public DateTime CreatedAt { get; set; }
}

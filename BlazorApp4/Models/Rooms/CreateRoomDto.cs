using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp4.Models.Rooms;

public class CreateRoomDto
{
    [
        Required(ErrorMessage = "Binoning nomi talab qilinadi"),
        MaxLength(255, ErrorMessage = "Nom 255 ta belgidan ortiq bo'la olmaydi")
    ]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Binoning nomi talab qilinadi")]
    public int DepartmentId { get; set; }
}

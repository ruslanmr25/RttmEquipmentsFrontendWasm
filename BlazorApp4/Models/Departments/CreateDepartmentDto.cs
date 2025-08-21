using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp4.Models.Departments;

public class CreateDepartmentDto
{
    [
        Required(ErrorMessage = "Binoning nomi talab qilinadi"),
        MaxLength(255, ErrorMessage = "Nom 255 ta belgidan ortiq bo'la olmaydi")
    ]
    public string Name { get; set; } = string.Empty;
}

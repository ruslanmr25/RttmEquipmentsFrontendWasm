using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp4.Models.Auth;

public class LoginDto
{
    [Required(ErrorMessage = "foydalanuvchi nomi bo'sh bo'la olmaydi"), MaxLength(255)]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Parol bo'sh bo'lmasligi kerak"), MaxLength(255)]
    public string Password { get; set; } = string.Empty;
}

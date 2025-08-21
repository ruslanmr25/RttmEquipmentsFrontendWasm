using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp4.Models.Equipments;

public class EditEquipmentDto
{
    [Required(ErrorMessage = "Uskuna nomi majburiy.")]
    [StringLength(
        100,
        MinimumLength = 2,
        ErrorMessage = "Nomi 2 dan 100 belgigacha bo‘lishi kerak."
    )]
    public string? Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Inventar raqami majburiy.")]
    [Range(1, int.MaxValue, ErrorMessage = "Inventar raqami 1 dan katta bo‘lishi kerak.")]
    public int? InvertarId { get; set; }

    //    [RegularExpression(
    //        @"^([0-9A-Fa-f]{2}([-:])){5}([0-9A-Fa-f]{2})$",
    //        ErrorMessage = "Mac manzil noto‘g‘ri formatda."
    //    )]
    public string? MacAddress { get; set; }

    [Required(ErrorMessage = "Foydalanuvchi tanlanishi kerak.")]
    [Range(1, int.MaxValue, ErrorMessage = "Foydalanuvchi ID noto‘g‘ri.")]
    public int? UserId { get; set; }

    [Required(ErrorMessage = "Uskuna turi tanlanishi kerak.")]
    [Range(1, int.MaxValue, ErrorMessage = "Uskuna turi noto‘g‘ri.")]
    public int EquipmentTypeId { get; set; }

    public List<int> Parameters { get; set; } = new();

    [Required(ErrorMessage = "Bino tanlanishi kerak.")]
    [Range(1, int.MaxValue, ErrorMessage = "Bino  noto‘g‘ri.")]
    public int DepartmentId { get; set; }

    [Required(ErrorMessage = "Xona tanlanishi kerak.")]
    [Range(1, int.MaxValue, ErrorMessage = "Xona  noto‘g‘ri.")]
    public int? RoomId { get; set; }
}

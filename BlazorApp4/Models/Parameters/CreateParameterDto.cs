using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp4.Models.Parameters;

public class CreateParameterDto
{
    [
        Required(ErrorMessage = "Parameter uchun nom talab qilinadi!"),
        MaxLength(255, ErrorMessage = "255ta  belgidan kam bulmasligi kerak")
    ]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Parameter uchun tur tanlash talab qilinadi")]
    public int? EquipmentTypeId { get; set; }
}

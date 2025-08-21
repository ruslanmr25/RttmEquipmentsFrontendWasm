using System.ComponentModel.DataAnnotations;

namespace BlazorApp4.Models.EquipmentTypes;

public class EditEquipmentTypeDto
{
    [
        Required(ErrorMessage = "'Name' maydoni bo'sh bo'lmasligi kerak"),
        MaxLength(255, ErrorMessage = "'Name' maydoni 255 belgidan ko'p bo'lolmaydi")
    ]
    public string Name { get; set; } = string.Empty;

    [Required]
    public int CategoryId { get; set; }
}

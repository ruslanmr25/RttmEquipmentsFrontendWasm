using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp4.Models.Images;

public class CreateImageDto
{
    [Required(ErrorMessage = "image bo'lishi shart")]
    public string Image { get; set; } = string.Empty;
}

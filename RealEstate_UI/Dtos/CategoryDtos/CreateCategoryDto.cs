using System.ComponentModel.DataAnnotations;

namespace RealEstate_UI.Dtos.CategoryDtos
{
    public class CreateCategoryDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        [RegularExpression("^[a-zA-Z0-9 ]+$", ErrorMessage = "Name can only contain letters, numbers, and spaces.")]
        public string? Name { get; set; }
    }
}

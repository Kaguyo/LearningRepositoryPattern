using System.ComponentModel.DataAnnotations;

namespace fullstack_repository_pattern.Domain
{
    public class Car()
    {   

        [Required(ErrorMessage = "Make is required.")]
        public string Make { get; set; } = String.Empty;

        [Required(ErrorMessage = "Model is required.")]
        public string Model { get; set; } = String.Empty;
        
        [Required(ErrorMessage = "Color is required.")]
        public string Color { get; set; } = String.Empty;
        
        [Required(ErrorMessage = "Year is required.")]
        [Range(1886, 2100, ErrorMessage = "Year must be between 1886 and 2025.")]
        public int Year { get; set; }
    }

}
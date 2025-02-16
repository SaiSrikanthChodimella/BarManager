using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarManagerAPI.Models
{
    public class MenuItem
    {
        public int Id { get; set; }

        [Required, StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        [Range(0.01, 10000, ErrorMessage = "Price must be between 0.01 and 10,000.")]
        [Column(TypeName = "decimal(10,2)")] // Controls precision in database
        public decimal Price { get; set; }

        [StringLength(300, ErrorMessage = "Image URL cannot exceed 300 characters.")]
        public string? Image { get; set; }

        [Required(ErrorMessage = "Menu Category is required.")]
        public int MenuCategoryId { get; set; } // Foreign key

        public MenuCategory? MenuCategory { get; set; }
    }
}

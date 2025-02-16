using System.ComponentModel.DataAnnotations;

namespace BarManagerAPI.Models
{
    public class MenuCategory
    {
        public int Id { get; set; }

        [Required, StringLength(100, ErrorMessage = "Category name cannot exceed 100 characters.")]
        public string Name { get; set; }

        public ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
    }

}

using System.ComponentModel.DataAnnotations;

namespace BarManagerAPI.Models
{
    public class TeamMember
    {
        public int Id { get; set; }

        [Required, StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        [StringLength(300, ErrorMessage = "Image URL cannot exceed 300 characters.")]
        public string? Image { get; set; }

        [StringLength(200, ErrorMessage = "User quote cannot exceed 200 characters.")]
        public string? UserQuote { get; set; }
    }
}

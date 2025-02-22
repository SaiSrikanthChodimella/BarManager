using System.ComponentModel.DataAnnotations;

namespace BarManagerAPI.Models
{
    public class EventItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Start date is required.")]
        public DateTime Start { get; set; }

        [Required(ErrorMessage = "End date is required.")]
        public DateTime End { get; set; }

        [Required, StringLength(100, ErrorMessage = "Event text cannot exceed 100 characters.")]
        public string Text { get; set; }
    }

}

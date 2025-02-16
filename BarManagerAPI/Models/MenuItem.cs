namespace BarManagerAPI.Models
{
    public class MenuItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Image { get; set; }

        public int MenuCategoryId { get; set; } // Foreign key property

        public MenuCategory? MenuCategory { get; set; }
    }
}

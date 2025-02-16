namespace BarManagerAPI.DTO
{
    public class MenuCategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<MenuItemDTO> MenuItems { get; set; } = new List<MenuItemDTO>(); // Simplified DTOs
    }
}
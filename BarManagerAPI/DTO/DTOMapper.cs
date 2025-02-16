using BarManagerAPI.Models;

namespace BarManagerAPI.DTO
{
    public static class DTOMapper
    {
        public static MenuItemDTO MapToMenuItemDto(this MenuItem menuItem) => new()
        {
            Id = menuItem.Id,
            Name = menuItem.Name,
            Description = menuItem.Description,
            Price = menuItem.Price,
            Image = menuItem.Image,
            MenuCategoryId = menuItem.MenuCategoryId,
            MenuCategory = new MenuCategoryDTO
            {
                Id = menuItem.MenuCategory.Id,
                Name = menuItem.MenuCategory.Name
            }
        };

        public static MenuCategoryDTO MapToMenuCategoryDto(this MenuCategory menuCategory) => new()
        {
            Id = menuCategory.Id,
            Name = menuCategory.Name,
            MenuItems = [.. menuCategory.MenuItems .Select(mi => new MenuItemDTO
                    {
                        Id = mi.Id,
                        Name = mi.Name,
                        Description = mi.Description,
                        Price = mi.Price,
                        Image = mi.Image,
                        MenuCategoryId = mi.MenuCategoryId
                    })]
        };
    }
}

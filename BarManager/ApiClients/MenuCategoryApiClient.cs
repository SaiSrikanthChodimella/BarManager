using BarManager.Models;

namespace BarManager.ApiClients
{
    public class MenuCategoryApiClient(HttpClient httpClient)
    {
        public async Task<List<MenuCategory>> GetMenuCategoriesAsync()
        {
            var menuCategory = new List<MenuCategory>();

            await foreach (var forecast in httpClient.GetFromJsonAsAsyncEnumerable<MenuCategory>("api/MenuCategory"))
            {
                if (forecast is not null)
                {
                    menuCategory.Add(forecast);
                }
            }
            return menuCategory;
        }
        public async Task AddMenuCategoryAsync(MenuCategory menuCategory) => await httpClient.PostAsJsonAsync("api/MenuCategory", menuCategory);

        public async Task UpdateMenuCategoryAsync(MenuCategory menuCategory) => await httpClient.PutAsJsonAsync($"api/MenuCategory/{menuCategory.Id}", menuCategory);

        public async Task DeleteMenuCategoryAsync(int id) => await httpClient.DeleteAsync($"api/MenuCategory/{id}");
    }
}

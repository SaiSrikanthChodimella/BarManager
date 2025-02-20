using BarManager.Models;

namespace BarManager.ApiClients
{
    public class MenuItemsApiClient(HttpClient httpClient)
    {
        public async Task<List<MenuItem>> GetMenuItemsAsync()
        {
            var menuCategory = new List<MenuItem>();

            await foreach (var forecast in httpClient.GetFromJsonAsAsyncEnumerable<MenuItem>("api/MenuItems"))
            {
                if (forecast is not null)
                {
                    menuCategory.Add(forecast);
                }
            }
            return menuCategory;
        }

        public async Task<List<MenuItem>> GetMenuItemsByCategoryIdAsync(int iD)
        {
            var menuItems = new List<MenuItem>();

            await foreach (var forecast in httpClient.GetFromJsonAsAsyncEnumerable<MenuItem>($"api/MenuItems/CategotyID/{iD}"))
            {
                if (forecast is not null)
                {
                    menuItems.Add(forecast);
                }
            }
            return menuItems;
        }

        public async Task AddMenuItemAsync(MenuItem menuCategory) => await httpClient.PostAsJsonAsync("api/MenuItems", menuCategory);

        public async Task UpdateMenuItemAsync(MenuItem menuCategory) => await httpClient.PutAsJsonAsync($"api/MenuItems/{menuCategory.Id}", menuCategory);

        public async Task DeleteMenuItemAsync(int id) => await httpClient.DeleteAsync($"api/MenuItems/{id}");
    }
}

using BarManager.Models;

namespace BarManager.ApiClients
{
    public class MenuApiClient(HttpClient httpClient)
    {
        public async Task<List<MenuCategory>> GetMenu()
        {
            var menuCategory = new List<MenuCategory>();

            await foreach (var forecast in httpClient.GetFromJsonAsAsyncEnumerable<MenuCategory>("api/TeamMembers"))
            {
                if (forecast is not null)
                {
                    menuCategory.Add(forecast);
                }
            }
            return menuCategory;
        }
    }
}

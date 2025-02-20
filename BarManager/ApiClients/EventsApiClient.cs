using BarManager.Models;

namespace BarManager.ApiClients
{
    public class EventsApiClient(HttpClient httpClient)
    {
        public async Task<List<EventItems>> GetEventsAsync()
        {
            var events = new List<EventItems>();

            await foreach (var eventItem in httpClient.GetFromJsonAsAsyncEnumerable<EventItems>("api/Events"))
            {
                if (eventItem is not null)
                {
                    events.Add(eventItem);
                }
            }

            return events;
        }

        public async Task AddEventItemsAsync(EventItems eventItems) => await httpClient.PostAsJsonAsync("api/Events", eventItems);

        public async Task UpdateEventItemsAsync(EventItems eventItems) => await httpClient.PutAsJsonAsync($"api/Events/{eventItems.Id}", eventItems);

        public async Task DeleteEventItemsAsync(int id) => await httpClient.DeleteAsync($"api/Events/{id}");
    }
}

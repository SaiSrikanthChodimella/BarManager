using BarManager.Models;

namespace BarManager.ApiClients
{
    public class EventsApiClient(HttpClient httpClient)
    {
        public async Task<List<EventItems>> GetEvents()
        {
            var events = new List<EventItems>();

            await foreach (var forecast in httpClient.GetFromJsonAsAsyncEnumerable<EventItems>("api/TeamMembers"))
            {
                if (forecast is not null)
                {
                    events.Add(forecast);
                }
            }

            return events;
        }
    }
}

using BarManager.Models;

namespace BarManager.ApiClients
{
    public class TeamMemberApiClient(HttpClient httpClient)
    {
        public async Task<List<TeamMembers>> GetTeamMember()
        {
            var  TeamMembers = new List<TeamMembers>();

            await foreach (var teamMembers in httpClient.GetFromJsonAsAsyncEnumerable<TeamMembers>("api/TeamMembers"))
            {
                if (teamMembers is not null)
                {
                    TeamMembers.Add(teamMembers);
                }
            }

            return TeamMembers;
        }

    }
}

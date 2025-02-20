using BarManager.Models;

namespace BarManager.ApiClients
{
    public class TeamMemberApiClient(HttpClient httpClient)
    {
        public async Task<List<TeamMembers>> GetTeamMembersAsync()
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

        public async Task AddTeamMemberAsync(TeamMembers teamMember) => await httpClient.PostAsJsonAsync("api/teammembers", teamMember);

        public async Task UpdateTeamMemberAsync(TeamMembers teamMember) => await httpClient.PutAsJsonAsync($"api/teammembers/{teamMember.Id}", teamMember);

        public async Task DeleteTeamMemberAsync(int id) => await httpClient.DeleteAsync($"api/teammembers/{id}");
    }
}

using BarManagerAPI.Models;
using BarManagerAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamMembersController(IUnitOfWork unitOfWork) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _unitOfWork.TeamMembersRepository.GetAllAsync();
            return Ok(items);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(TeamMember member)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _unitOfWork.TeamMembersRepository.AddAsync(member);
            await _unitOfWork.SaveAsync();

            return Created($"/api/TeamMembers/{member.Id}", member);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, TeamMember updatedTeamMember)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var teamMember = await _unitOfWork.TeamMembersRepository.GetByIdAsync(id);

            if (teamMember is null)
            {
                return NotFound();
            }

            teamMember.Name = updatedTeamMember.Name;
            teamMember.Description = updatedTeamMember.Description;
            teamMember.UserQuote = updatedTeamMember.UserQuote;
            teamMember.Image = updatedTeamMember.Image;

            _unitOfWork.TeamMembersRepository.Update(teamMember);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var teamMember = await _unitOfWork.TeamMembersRepository.GetByIdAsync(id);

            if (teamMember is null)
            {
                return NotFound();
            }

            _unitOfWork.TeamMembersRepository.Delete(teamMember);
            await _unitOfWork.SaveAsync();

            return Ok(teamMember);
        }
    }
}
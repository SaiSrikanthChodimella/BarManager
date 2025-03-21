﻿using BarManagerAPI.Models;
using BarManagerAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController(IUnitOfWork unitOfWork) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _unitOfWork.EventItemsRepository.GetAllAsync();
            return Ok(items);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(EventItem events)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _unitOfWork.EventItemsRepository.AddAsync(events);
            await _unitOfWork.SaveAsync();

            return Created($"/api/Events/{events.Id}", events);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, EventItem updatedEventItems)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (updatedEventItems.Start >= updatedEventItems.End)
            {
                return BadRequest("Start date must be before End date.");
            }
            var eventItem = await _unitOfWork.EventItemsRepository.GetByIdAsync(id);

            if (eventItem is null)
            {
                return NotFound();
            }
            if (updatedEventItems.Start >= updatedEventItems.End)
            {
                return BadRequest("Start date must be before End date.");
            }

            eventItem.Start = updatedEventItems.Start;
            eventItem.End = updatedEventItems.End;
            eventItem.Text = updatedEventItems.Text;

            _unitOfWork.EventItemsRepository.Update(eventItem);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var eventItem = await _unitOfWork.EventItemsRepository.GetByIdAsync(id);

            if (eventItem is null)
            {
                return NotFound();
            }

            _unitOfWork.EventItemsRepository.Delete(eventItem);
            await _unitOfWork.SaveAsync();

            return Ok(eventItem);
        }
    }
}
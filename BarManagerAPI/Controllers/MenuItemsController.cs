using BarManagerAPI.DTO;
using BarManagerAPI.Models;
using BarManagerAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BarManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuItemsController(IUnitOfWork unitOfWork) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _unitOfWork.MenuItemRepository.GetAllAsync(x => x.MenuCategory);
            var itemDtos = items.Select(x => x.MapToMenuItemDto()).ToList();
            return Ok(itemDtos);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MenuItem menuItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _unitOfWork.MenuItemRepository.AddAsync(menuItem);
            await _unitOfWork.SaveAsync();

            return Created($"/api/MenuItems/{menuItem.Id}", menuItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MenuItem updatedMenuItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var menuItem = await _unitOfWork.MenuItemRepository.GetByIdAsync(id);

            if (menuItem is null)
            {
                return NotFound();
            }

            menuItem.Name = updatedMenuItem.Name;
            menuItem.Description = updatedMenuItem.Description;
            menuItem.Price = updatedMenuItem.Price;
            menuItem.Image = updatedMenuItem.Image;

            _unitOfWork.MenuItemRepository.Update(menuItem);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var menuItem = await _unitOfWork.MenuItemRepository.GetByIdAsync(id);

            if (menuItem is null)
            {
                return NotFound();
            }

            _unitOfWork.MenuItemRepository.Delete(menuItem);
            await _unitOfWork.SaveAsync();

            var item = menuItem.MapToMenuItemDto();
            return Ok(item);
        }
    }
}

using BarManagerAPI.DTO;
using BarManagerAPI.Models;
using BarManagerAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuCategoryController(IUnitOfWork unitOfWork) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _unitOfWork.MenuCategoryRepository.GetAllAsync(x => x.MenuItems);
            var categoriesDtos = categories.Select(x => x.MapToMenuCategoryDto()).ToList();
            return Ok(categoriesDtos);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(MenuCategory menuCategoryItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _unitOfWork.MenuCategoryRepository.AddAsync(menuCategoryItem);
            await _unitOfWork.SaveAsync();

            return Created($"/MenuItems/{menuCategoryItem.Id}", menuCategoryItem);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, MenuCategory updatedMenuCategoryItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var menuCategory = await _unitOfWork.MenuCategoryRepository.GetByIdAsync(id);

            if (menuCategory is null)
            {
                return NotFound();
            }

            menuCategory.Id = updatedMenuCategoryItem.Id;
            menuCategory.Name = updatedMenuCategoryItem.Name;

            _unitOfWork.MenuCategoryRepository.Update(menuCategory);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var menuCategory = await _unitOfWork.MenuCategoryRepository.GetByIdAsync(id);

            if (menuCategory is null)
            {
                return NotFound();
            }
            _unitOfWork.MenuCategoryRepository.Delete(menuCategory);

            await _unitOfWork.SaveAsync();

            var categoriesDtos = menuCategory.MapToMenuCategoryDto();
            return Ok(categoriesDtos);
        }
    }
}
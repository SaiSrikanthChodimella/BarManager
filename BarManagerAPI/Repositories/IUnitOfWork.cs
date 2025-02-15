using BarManagerAPI.Models;

namespace BarManagerAPI.Repositories
{
    public interface IUnitOfWork
    {
        IGenericRepository<MenuItem> MenuItemRepository { get; }

        IGenericRepository<MenuCategory> MenuCategoryRepository { get; }

        IGenericRepository<TeamMembers> TeamMembersRepository { get; }

        IGenericRepository<EventItems> EventItemsRepository { get; }

        Task SaveAsync();
    }
}

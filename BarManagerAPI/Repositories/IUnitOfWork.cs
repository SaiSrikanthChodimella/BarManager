using BarManagerAPI.Models;

namespace BarManagerAPI.Repositories
{
    public interface IUnitOfWork
    {
        IGenericRepository<MenuItem> MenuItemRepository { get; }

        IGenericRepository<MenuCategory> MenuCategoryRepository { get; }

        IGenericRepository<TeamMember> TeamMembersRepository { get; }

        IGenericRepository<EventItem> EventItemsRepository { get; }

        IGenericRepository<User> UserRepository { get; }

        Task SaveAsync();
    }
}

using BarManagerAPI.Models;

namespace BarManagerAPI.Repositories
{
    public class UnitOfWork(BarManagerDBContext dBContext) : IUnitOfWork
    {

        private IGenericRepository<MenuItem> _menuItemRepository;
        public IGenericRepository<MenuItem> MenuItemRepository => _menuItemRepository ?? new GenericRepository<MenuItem>(dBContext);

        private IGenericRepository<MenuCategory> _menuCategoryRepository;
        public IGenericRepository<MenuCategory> MenuCategoryRepository => _menuCategoryRepository ?? new GenericRepository<MenuCategory>(dBContext);

        private IGenericRepository<TeamMember> _teamMemberRepository;
        public IGenericRepository<TeamMember> TeamMembersRepository => _teamMemberRepository ?? new GenericRepository<TeamMember>(dBContext);

        private IGenericRepository<EventItem> _eventItemsRepository;
        public IGenericRepository<EventItem> EventItemsRepository => _eventItemsRepository ?? new GenericRepository<EventItem>(dBContext);

        private IGenericRepository<User> _userRepository;
        public IGenericRepository<User> UserRepository => _userRepository ?? new GenericRepository<User>(dBContext);

        public async Task SaveAsync()
        {
            //TODO : When using Real Database use Try catch with transaction
            await dBContext.SaveChangesAsync();
        }
    }
}

using BarManagerAPI.Models;

namespace BarManagerAPI.Repositories
{
    public class UnitOfWork(BarManagerDBContext dBContext) : IUnitOfWork
    {

        private IGenericRepository<MenuItem> _menuItemRepository;
        public IGenericRepository<MenuItem> MenuItemRepository => _menuItemRepository ?? new GenericRepository<MenuItem>(dBContext);

        private IGenericRepository<MenuCategory> _menuCategoryRepository;
        public IGenericRepository<MenuCategory> MenuCategoryRepository => _menuCategoryRepository ?? new GenericRepository<MenuCategory>(dBContext);

        private IGenericRepository<TeamMembers> _teamMemberRepository;
        public IGenericRepository<TeamMembers> TeamMembersRepository => _teamMemberRepository ?? new GenericRepository<TeamMembers>(dBContext);

        private IGenericRepository<EventItems> _eventItemsRepository;
        public IGenericRepository<EventItems> EventItemsRepository => _eventItemsRepository ?? new GenericRepository<EventItems>(dBContext);

        public async Task SaveAsync()
        {
            //TODO : When using Real Database use Try catch with transaction
            await dBContext.SaveChangesAsync();
        }
    }
}

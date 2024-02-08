using BlazorCARS.HealthShield.DAL.Data;
using BlazorCARS.HealthShield.DAL.Entity;
using BlazorCARS.HealthShield.DAL.IRepository;

namespace BlazorCARS.HealthShield.DAL.Repository
{
    public class InventoryRepository : BaseRepository<Inventory>, IInventoryRepository
    {
        private readonly BlazorCARSDBContext _blazorDbContext;
        public InventoryRepository(BlazorCARSDBContext blazorDbContext) : base(blazorDbContext)
        {
            _blazorDbContext = blazorDbContext;
        }
    }
}

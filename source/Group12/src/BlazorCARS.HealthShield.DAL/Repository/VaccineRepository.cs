using BlazorCARS.HealthShield.DAL.Data;
using BlazorCARS.HealthShield.DAL.Entity;
using BlazorCARS.HealthShield.DAL.IRepository;

//Created by Shilpa Tatiparthi

namespace BlazorCARS.HealthShield.DAL.Repository
{
    public class VaccineRepository : BaseRepository<Vaccine>, IVaccineRepository
    {
        private readonly BlazorCARSDBContext _blazorDbContext;
        public VaccineRepository(BlazorCARSDBContext blazorDbContext) : base(blazorDbContext)
        {
            _blazorDbContext = blazorDbContext;
        }

    }
}

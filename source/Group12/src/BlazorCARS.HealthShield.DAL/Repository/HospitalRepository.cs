using BlazorCARS.HealthShield.DAL.Data;
using BlazorCARS.HealthShield.DAL.Entity;
using BlazorCARS.HealthShield.DAL.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
  Created by Sai Sreeja Yellampalli
 */
namespace BlazorCARS.HealthShield.DAL.Repository
{
    public class HospitalRepository : BaseRepository<Hospital>, IHospitalRepository
    {
        private readonly BlazorCARSDBContext _blazorDbContext;
        public HospitalRepository(BlazorCARSDBContext blazorDbContext) : base(blazorDbContext)
        {
            _blazorDbContext = blazorDbContext;
        }
    }
}

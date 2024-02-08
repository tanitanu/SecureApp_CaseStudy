using BlazorCARS.HealthShield.DAL.Data;
using BlazorCARS.HealthShield.DAL.Entity;
using BlazorCARS.HealthShield.DAL.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
  Created by JAYaseelan
 */
namespace BlazorCARS.HealthShield.DAL.Repository
{
    public class StateRepository : BaseRepository<State>, IStateRepository
    {
        private readonly BlazorCARSDBContext _blazorDbContext;
        public StateRepository(BlazorCARSDBContext blazorDbContext) : base(blazorDbContext)
        {
            _blazorDbContext = blazorDbContext;
        }
    }
}

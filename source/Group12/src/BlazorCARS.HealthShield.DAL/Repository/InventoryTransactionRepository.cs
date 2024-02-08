using BlazorCARS.HealthShield.DAL.Data;
using BlazorCARS.HealthShield.DAL.Entity;
using BlazorCARS.HealthShield.DAL.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCARS.HealthShield.DAL.Repository
{
    public class InventoryTransactionRepository : BaseRepository<InventoryTransaction>, IInventoryTransactionRepository
    {
        private readonly BlazorCARSDBContext _blazorDBContext;

        public InventoryTransactionRepository(BlazorCARSDBContext blazorDBContext) : base(blazorDBContext)
        {
            this._blazorDBContext = blazorDBContext;
        }

        public async Task<List<InventoryTransaction>> GetAllSync()
        {
            return await (from it in this._blazorDBContext.InventoryTransaction
                          select new InventoryTransaction
                          {
                              HospitalId = it.HospitalId,
                              InventoryTransactionId = it.InventoryTransactionId,
                              ReceivedOn = it.ReceivedOn,
                              ReceivedQty = it.ReceivedQty,
                              RefNo = it.RefNo,
                              VaccineId = it.VaccineId,
                              IsDeleted = it.IsDeleted
                          }
                          ).ToListAsync();
        }

        public async Task<List<InventoryTransaction>> GetByVaccineSync(int vaccineId)
        {
            return await (from it in this._blazorDBContext.InventoryTransaction
                          where it.VaccineId == vaccineId
                          select new InventoryTransaction
                          {
                              HospitalId = it.HospitalId,
                              InventoryTransactionId = it.InventoryTransactionId,
                              ReceivedOn = it.ReceivedOn,
                              ReceivedQty = it.ReceivedQty,
                              RefNo = it.RefNo,
                              VaccineId = it.VaccineId,
                              IsDeleted = it.IsDeleted
                          }
                          ).ToListAsync();
        }

        public async Task<List<InventoryTransaction>> GetByHospitalSync(int hospitalId)
        {
            return await (from it in this._blazorDBContext.InventoryTransaction
                          where it.HospitalId == hospitalId
                          select new InventoryTransaction
                          {
                              HospitalId = it.HospitalId,
                              InventoryTransactionId = it.InventoryTransactionId,
                              ReceivedOn = it.ReceivedOn,
                              ReceivedQty = it.ReceivedQty,
                              RefNo = it.RefNo,
                              VaccineId = it.VaccineId,
                              IsDeleted = it.IsDeleted
                          }
                          ).ToListAsync();
        }
    }
}

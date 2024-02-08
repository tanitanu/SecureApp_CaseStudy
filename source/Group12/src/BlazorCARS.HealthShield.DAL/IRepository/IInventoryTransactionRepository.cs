using BlazorCARS.HealthShield.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCARS.HealthShield.DAL.IRepository
{
    public interface IInventoryTransactionRepository : IBaseRepository<InventoryTransaction>
    {
        Task<List<InventoryTransaction>> GetAllSync();

        Task<List<InventoryTransaction>> GetByVaccineSync(int vaccineId);

       Task<List<InventoryTransaction>> GetByHospitalSync(int hospitalId);

    }
}

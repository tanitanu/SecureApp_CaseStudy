using BlazorCARS.HealthShield.DAL.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
  Created by JAYaseelan
 */
namespace BlazorCARS.HealthShield.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
        Task RollbackAsync();

        #region Domain Registration
        IUserRoleRepository UserRole { get; }
        IUserStoreRepository UserStore { get; }
        ICountryRepository Country { get; }
        IStateRepository State { get; }
        IHospitalRepository Hospital { get; }
        IInventoryRepository Inventory { get; }
        IRecipientRepository Recipient { get; }
        IVaccineRepository Vaccine { get; }
        IVaccineRegistrationRepository VaccineRegistration { get; }
        IVaccineScheduleRepository VaccineSchedule { get; }
        IInventoryTransactionRepository InventoryTransaction { get; }
        ITokenHandler TokenHandler { get; }
        #endregion
    }
}

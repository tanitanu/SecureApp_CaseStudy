using BlazorCARS.HealthShield.DAL.Data;
using BlazorCARS.HealthShield.DAL.Entity;
using BlazorCARS.HealthShield.DAL.IRepository;
using BlazorCARS.HealthShield.DAL.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

/*
  Created by JAYaseelan
 */
namespace BlazorCARS.HealthShield.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BlazorCARSDBContext _blazorDbContext;
        private readonly IConfiguration _configuration;
        public UnitOfWork(BlazorCARSDBContext blazorDbContext, IConfiguration configuration)
        {
            _blazorDbContext = blazorDbContext;
            _configuration = configuration;
        }
        public async Task CommitAsync()
        {
            await _blazorDbContext.SaveChangesAsync();
        }

        public async Task RollbackAsync()
        {
            await _blazorDbContext.DisposeAsync();
        }

        #region Domain Objects creation registration
        private IUserRoleRepository _userRole;
        private IUserStoreRepository _useUserStore;
        private ICountryRepository _country;
        private IStateRepository _state;
        private IRecipientRepository _recipient;
        private IHospitalRepository _hospital;
        private IInventoryRepository _inventory;
        private IVaccineRepository _vaccine;
        private IVaccineRegistrationRepository _vaccineRegistration;
        private IVaccineScheduleRepository _vaccineSchedule;
        private IInventoryTransactionRepository _inventoryTransaction;
        private ITokenHandler _tokenHandler;
        public IUserRoleRepository UserRole
        {
            get { return _userRole ??= new UserRoleRepository(_blazorDbContext); }
        }

        public IUserStoreRepository UserStore
        {
            get { return _useUserStore ??= new UserStoreRepository(_blazorDbContext); }
        }
        public ICountryRepository Country
        {
            get { return _country ??= new CountryRepository(_blazorDbContext); }
        }

        public IStateRepository State
        {
            get { return _state ??= new StateRepository(_blazorDbContext); }
        }

        public IHospitalRepository Hospital
        {
            get { return _hospital ??= new HospitalRepository(_blazorDbContext); }
        }

        public IInventoryRepository Inventory
        {
            get { return this._inventory ??= new InventoryRepository(_blazorDbContext); }
        }

        public IRecipientRepository Recipient
        {
            get { return _recipient ??= new RecipientRepository(_blazorDbContext); }
        }

        public IVaccineRepository Vaccine
        {
            get { return this._vaccine ??= new VaccineRepository(_blazorDbContext); }
        }

        public IVaccineRegistrationRepository VaccineRegistration
        {
            get { return this._vaccineRegistration ??= new VaccineRegistrationRepository(_blazorDbContext); }
        }

        public IVaccineScheduleRepository VaccineSchedule
        {
            get { return this._vaccineSchedule ??= new VaccineScheduleRepository(this._blazorDbContext); }
        }

        public IInventoryTransactionRepository InventoryTransaction
        {
            get { return this._inventoryTransaction ??= new InventoryTransactionRepository(this._blazorDbContext); }
        }
        public ITokenHandler TokenHandler
        {
            get { return _tokenHandler ??= new TokenHandler(_configuration); }
        }
        #endregion
    }
}

using AutoMapper;
using BlazorCARS.HealthShield.DAL.Appointment;
using BlazorCARS.HealthShield.DAL.Entity;
using BlazorCARS.HealthShield.DataObject.DTO;
using BlazorCARS.HealthShield.DataObject.DTO.Appointment;
using BlazorCARS.HealthShield.Utility.RequestValidator;
using BlazorCARS.HealthShield.WebAPI.Controllers;
/*
  Created by JAYaseelan
 */
namespace BlazorCARS.HealthShield.WebAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            #region Core maps
            CreateMap<UserRole, UserRoleDTO>().ReverseMap();
            CreateMap<UserRole, UserRoleCreateDTO>().ReverseMap();
            CreateMap<UserRole, UserRoleUpdateDTO>().ReverseMap();

            CreateMap<UserStore, UserStoreDTO>().ReverseMap();
            CreateMap<UserStore, UserStoreCreateDTO>().ReverseMap();
            CreateMap<UserStore, UserStoreUpdateDTO>().ReverseMap();

            CreateMap<Country, CountryDTO>().ReverseMap();
            CreateMap<Country, CountryCreateDTO>().ReverseMap();
            CreateMap<Country, CountryUpdateDTO>().ReverseMap();

            CreateMap<State, StateDTO>().ReverseMap();
            CreateMap<State, StateCreateDTO>().ReverseMap();
            CreateMap<State, StateUpdateDTO>().ReverseMap();

            CreateMap<Recipient, RecipientDTO>().ReverseMap();
            CreateMap<Recipient, RecipientCreateDTO>().ReverseMap();
            CreateMap<Recipient, RecipientUpdateDTO>().ReverseMap();

            CreateMap<Hospital, HospitalDTO>().ReverseMap();
            CreateMap<Hospital, HospitalCreateDTO>().ReverseMap();
            CreateMap<Hospital, HospitalUpdateDTO>().ReverseMap();

            CreateMap<Inventory, InventoryDTO>().ReverseMap();
            CreateMap<Inventory, InventoryCreateDTO>().ReverseMap();
            CreateMap<Inventory, InventoryUpdateDTO>().ReverseMap();

            CreateMap<InventoryTransaction, InventoryTransactionDTO>().ReverseMap();
            CreateMap<InventoryTransaction, InventoryTransactionCreateDTO>().ReverseMap();
            CreateMap<InventoryTransaction, InventoryTransactionUpdateDTO>().ReverseMap();

            CreateMap<Vaccine, VaccineDTO>().ReverseMap();
            CreateMap<Vaccine, VaccineCreateDTO>().ReverseMap();
            CreateMap<Vaccine, VaccineUpdateDTO>().ReverseMap();

            CreateMap<VaccineSchedule, VaccineScheduleDTO>().ReverseMap();
            CreateMap<VaccineSchedule, VaccineScheduleCreateDTO>().ReverseMap();
            CreateMap<VaccineSchedule, VaccineScheduleUpdateDTO>().ReverseMap();

            CreateMap<VaccineRegistration, VaccineRegistrationDTO>().ReverseMap();
            CreateMap<VaccineRegistration, VaccineRegistrationCreateDTO>().ReverseMap();
            CreateMap<VaccineRegistration, VaccineRegistrationUpdateDTO>().ReverseMap();

            CreateMap<Recipient, RecipientRegistryDTO>().ReverseMap();
            CreateMap<Hospital, HospitalRegistryDTO>().ReverseMap();

            CreateMap<ActiveAppointment, ActiveAppointmentDTO>().ReverseMap();
            #endregion

            #region Request Validators
            CreateMap<UserRoleRequest, UserRoleCreateDTO>().ReverseMap();
            CreateMap<UserRoleRequest, UserRoleUpdateDTO>().ReverseMap();
            
            CreateMap<UserStoreRequest, UserStoreCreateDTO>().ReverseMap();
            CreateMap<UserStoreRequest, UserStoreUpdateDTO>().ReverseMap();
            
            CreateMap<CountryRequest, CountryCreateDTO>().ReverseMap();
            CreateMap<CountryRequest, CountryUpdateDTO>().ReverseMap();
            
            CreateMap<StateRequest, StateCreateDTO>().ReverseMap();
            CreateMap<StateRequest, StateUpdateDTO>().ReverseMap();

            CreateMap<RecipientRequest, RecipientCreateDTO>().ReverseMap();
            CreateMap<RecipientRequest, RecipientUpdateDTO>().ReverseMap();

            CreateMap<HospitalRequest, HospitalCreateDTO>().ReverseMap();
            CreateMap<HospitalRequest, HospitalUpdateDTO>().ReverseMap();

            CreateMap<InventoryRequest, InventoryCreateDTO>().ReverseMap();
            CreateMap<InventoryRequest, InventoryUpdateDTO>().ReverseMap();

            CreateMap<InventoryTransactionRequest, InventoryTransactionCreateDTO>().ReverseMap();
            CreateMap<InventoryTransactionRequest, InventoryTransactionUpdateDTO>().ReverseMap();

            CreateMap<VaccineRequest, VaccineCreateDTO>().ReverseMap();
            CreateMap<VaccineRequest, VaccineUpdateDTO>().ReverseMap();

            CreateMap<VaccineScheduleRequest, VaccineScheduleCreateDTO>().ReverseMap();
            CreateMap<VaccineScheduleRequest, VaccineScheduleUpdateDTO>().ReverseMap();

            CreateMap<VaccineRegistrationRequest, VaccineRegistrationCreateDTO>().ReverseMap();
            CreateMap<VaccineRegistrationRequest, VaccineRegistrationUpdateDTO>().ReverseMap();

            CreateMap<RecipientRequest, RecipientRegistryDTO>().ReverseMap();
            CreateMap<HospitalRequest, HospitalRegistryDTO>().ReverseMap();
            #endregion
        }
    }
}

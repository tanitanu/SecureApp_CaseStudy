using BlazorCARS.HealthShield.DAL.Appointment;
using BlazorCARS.HealthShield.DAL.Data;
using BlazorCARS.HealthShield.DAL.Entity;
using BlazorCARS.HealthShield.DAL.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BlazorCARS.HealthShield.DAL.Repository
{
    public class VaccineRegistrationRepository: BaseRepository<VaccineRegistration>, IVaccineRegistrationRepository
    {
        private readonly BlazorCARSDBContext _blazorDbContext;

        public VaccineRegistrationRepository(BlazorCARSDBContext blazorDBContext):base(blazorDBContext)
        {
               this._blazorDbContext = blazorDBContext;
        }

        public async Task<IEnumerable<ActiveAppointment>> GetActiveappoinmentAsync(int id)
        {
            return await (from vr in _blazorDbContext.VaccineRegistration
                          join r in _blazorDbContext.Recipient on vr.RecipientId equals r.RecipientId
                          join vs in _blazorDbContext.VaccineSchedule on vr.VaccineScheduleId equals vs.VaccineScheduleId
                          where vr.DependantRecipientId == id 
                          && vr.IsVaccinated == false
                          && vs.ScheduleDate.Date >= DateTime.Now.Date
                          select new ActiveAppointment
                          {
                              VaccineRegistrationId = vr.VaccineRegistrationId,
                              RecipientId = vr.RecipientId,
                              RecipientName = $"{r.FirstName} {r.LastName}",
                              VaccineScheduleId = vr.VaccineScheduleId,
                              ScheduleDate = vs.ScheduleDate,
                              TimeSlot = vr.TimeSlot,
                              Dose = vr.Dose
                          }).ToListAsync();
        }
        public async Task<IEnumerable<ActiveAppointment>> GetActiveappoinmentByHospitalAsync(int id)
        {
            return await (from vr in _blazorDbContext.VaccineRegistration
                          join r in _blazorDbContext.Recipient on vr.RecipientId equals r.RecipientId
                          join vs in _blazorDbContext.VaccineSchedule on vr.VaccineScheduleId equals vs.VaccineScheduleId
                          where vs.HospitalId == id
                          && vr.IsVaccinated == false
                          && vs.ScheduleDate.Date >= DateTime.Now.Date
                          select new ActiveAppointment
                          {
                              VaccineRegistrationId = vr.VaccineRegistrationId,
                              RecipientId = vr.RecipientId,
                              RecipientName = $"{r.FirstName} {r.LastName}",
                              VaccineScheduleId = vr.VaccineScheduleId,
                              ScheduleDate = vs.ScheduleDate,
                              TimeSlot = vr.TimeSlot,
                              Dose = vr.Dose
                          }).ToListAsync();
        }
    }
}

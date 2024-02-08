using BlazorCARS.HealthShield.DAL.Data;
using BlazorCARS.HealthShield.DAL.Entity;
using BlazorCARS.HealthShield.DAL.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BlazorCARS.HealthShield.DAL.Repository
{
    public class VaccineScheduleRepository : BaseRepository<VaccineSchedule>, IVaccineScheduleRepository
    {
        private readonly BlazorCARSDBContext _blazorDBContext;

        public VaccineScheduleRepository(BlazorCARSDBContext blazorDbContext) : base(blazorDbContext)
        {
            this._blazorDBContext = blazorDbContext;
        }

        public async Task<List<VaccineSchedule>> GetScheduleByVaccine(int vaccineId)
        {
            return await (from vs in this._blazorDBContext.VaccineSchedule
                          where vs.VaccineId == vaccineId
                          select new VaccineSchedule
                          {
                              VaccineScheduleId = vs.VaccineScheduleId,
                              VaccineId = vs.VaccineId,
                              HospitalId = vs.HospitalId,
                              ScheduleDate = vs.ScheduleDate,
                              TimeSlot = vs.TimeSlot
                          }).ToListAsync();
        }

        public async Task<List<VaccineSchedule>> GetScheduleByHospital(int hospitalId)
        {
            return await (from vs in this._blazorDBContext.VaccineSchedule
                          where vs.HospitalId == hospitalId
                          select new VaccineSchedule
                          {
                              VaccineScheduleId = vs.VaccineScheduleId,
                              VaccineId = vs.VaccineId,
                              HospitalId = vs.HospitalId,
                              ScheduleDate = vs.ScheduleDate,
                              TimeSlot = vs.TimeSlot
                          }
                          ).ToListAsync();
        }
    }
}

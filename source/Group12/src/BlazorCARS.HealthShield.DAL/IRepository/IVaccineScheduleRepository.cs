using BlazorCARS.HealthShield.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCARS.HealthShield.DAL.IRepository
{
    public interface IVaccineScheduleRepository : IBaseRepository<VaccineSchedule>
    {
        Task<List<VaccineSchedule>> GetScheduleByVaccine(int vaccineId);

        Task<List<VaccineSchedule>> GetScheduleByHospital(int hospitalId);


    }
}

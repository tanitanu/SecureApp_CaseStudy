using BlazorCARS.HealthShield.DAL.Appointment;
using BlazorCARS.HealthShield.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCARS.HealthShield.DAL.IRepository
{
    public interface IVaccineRegistrationRepository : IBaseRepository<VaccineRegistration>
    {
        Task<IEnumerable<ActiveAppointment>> GetActiveappoinmentAsync(int id);
        Task<IEnumerable<ActiveAppointment>> GetActiveappoinmentByHospitalAsync(int id);
    }
}

using BlazorCARS.HealthShield.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
  Created by Sai Sreeja Yellampalli
 */
namespace BlazorCARS.HealthShield.DAL.IRepository
{
    public interface IHospitalRepository : IBaseRepository<Hospital>
    {
        //Can be extended based on business requirement
    }
}

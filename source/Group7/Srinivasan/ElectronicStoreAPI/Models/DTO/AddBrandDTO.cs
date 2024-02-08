using System.ComponentModel.DataAnnotations;

namespace ElectronicStoreAPI.Models.DTO
{
    public class AddBrandDTO
    {
        [StringLength(30)]
        public string BrandName { get; set; }   //not null
    }
}

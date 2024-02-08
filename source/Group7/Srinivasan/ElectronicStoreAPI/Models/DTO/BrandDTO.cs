using System.ComponentModel.DataAnnotations;

namespace ElectronicStoreAPI.Models.DTO
{
    //by Srinivasan
    public class BrandDTO
    {
        [Key] public Guid BrandCode { get; set; }
        [StringLength(30)]
        public string BrandName { get; set; }   //not null
    }
}

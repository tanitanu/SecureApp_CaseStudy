using System.ComponentModel.DataAnnotations;

//by Srinivasan 
namespace ElectronicStoreAPI.Models.Domain
{   
    public class Brand
    {
        [Key] public Guid BrandCode { get; set; }
        [StringLength(30)]
        public string BrandName { get; set; }   //not null
    }
}

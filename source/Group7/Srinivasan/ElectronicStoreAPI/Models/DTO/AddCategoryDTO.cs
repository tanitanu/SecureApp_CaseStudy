using System.ComponentModel.DataAnnotations;

namespace ElectronicStoreAPI.Models.DTO
{
    public class AddCategoryDTO
    {
        [StringLength(30)]
        public string CatDescription { get; set; }  //not null
    }
}

using System.ComponentModel.DataAnnotations;

namespace ElectronicStoreAPI.Models.DTO
{
    //by Srinivasan
    public class CategoryDTO
    {
        [Key] public Guid CatCode { get; set; }

        [StringLength(30)]
        public string CatDescription { get; set; }  //not null
    }
}

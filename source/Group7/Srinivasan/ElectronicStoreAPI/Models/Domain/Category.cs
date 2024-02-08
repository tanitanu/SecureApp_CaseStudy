using System.ComponentModel.DataAnnotations;

//by Srinivasan
namespace ElectronicStoreAPI.Models.Domain
{
    //Product category
    public class Category
    {
        [Key] public Guid CatCode { get; set; }

        [StringLength(30)]
        public string CatDescription { get; set; }  //not null


    }
}

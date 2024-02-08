using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

//by Srinivasan

namespace ElectronicStoreAPI.Models.Domain
{
    //[PrimaryKey(nameof(ProdCat), nameof(ProdBrand), nameof(ProdModelNo))]
    public class Product
    {
        [Column("ProdCat", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key] public Category ProdCat { get; set; }
        [Column("ProdBrand", Order = 1 )]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key] public Brand ProdBrand { get; set; }
        [StringLength(20)]
        [Column(Order =2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key] public string ProdModelNo { get; set; }
        [StringLength (50)]
        public string ProdDescription { get; set; }

        public long? ProdMRP { get; set; }
        public long? ProdPrice { get; set; }
        [DefaultValue(0)]
        public Int16 ProdQty { get; set; }       

    }
}

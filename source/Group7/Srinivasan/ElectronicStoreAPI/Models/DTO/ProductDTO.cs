using ElectronicStoreAPI.Models.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ElectronicStoreAPI.Models.DTO
{
    //by Srinivasan
    public class ProductDTO
    {
        [Column("ProdCat", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key] public Category ProdCat { get; set; }
        [Column("ProdBrand", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key] public Brand ProdBrand { get; set; }
        [StringLength(20)]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key] public string ProdModelNo { get; set; }
        [StringLength(50)]
        public string ProdDescription { get; set; }

        public long? ProdMRP { get; set; }
        public long? ProdPrice { get; set; }
        [DefaultValue(0)]
        public Int16 ProdQty { get; set; }
    }
}

namespace DXC.BlogConnect.WebAPI.Models.Domain
{
    /*
 * Created By: Kishore
 */
    public class BaseDomain
    {
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;

    }
}

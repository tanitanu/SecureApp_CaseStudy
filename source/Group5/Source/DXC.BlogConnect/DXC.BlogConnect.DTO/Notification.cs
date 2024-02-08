using DXC.BlogConnect.Utilities.Enums;

namespace DXC.BlogConnect.DTO
{
    public class Notification
    {
        public string? Message { get; set; }
        public NotificationType Type { get; set; }
    }
}

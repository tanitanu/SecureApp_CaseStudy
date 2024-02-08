using System.Net;

namespace DXC.BlogConnect.DTO
{
    /*
* Created By: Kishore
*/
    public class APIResponse<T>
    {
        public APIResponse()
        {
            ErrorMessages = new List<ErrorCode.Error>();
            Result = new List<T>();
        }

        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public IEnumerable<ErrorCode.Error> ErrorMessages { get; set; }
        public List<T> Result { get; set; }
        public int Count { get; set; }
    }
}


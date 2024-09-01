using System.Net;

namespace WebApplication_MinimalAPI.Models
{
    public class APIRespons
    {
        public APIRespons()
        {
            ErrorMessages = new List<string>();
        }

        public bool IsSuccess { get; set; }
        public Object Result { get; set; }
        public HttpStatusCode StatusCode { get; set; }


        public List<string> ErrorMessages { get; set; }
    }
}

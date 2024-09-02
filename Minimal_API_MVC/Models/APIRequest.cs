using static Minimal_API_MVC.StaticDetails;

namespace Minimal_API_MVC.Models
{
    public class APIRequest
    {
        public ApiType apiType { get; set; }

        public string Url { get; set; }

        public object Data { get; set; }
        public string AccessToken { get; set; }
    }
}

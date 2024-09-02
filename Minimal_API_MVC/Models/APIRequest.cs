using static CouponAPI.StaticDetails;

namespace CouponAPI.Models
{
    public class APIRequest
    {
        public ApiType apiType { get; set; }

        public string Url { get; set; }

        public object Data { get; set; }
        public string AccessToken { get; set; }
    }
}

using CouponAPI.Models;

namespace Minimal_API_MVC_WEB.Servicies
{
    public interface IBaseService :IDisposable
    {
        ResponseDto responseModel { get; set; }

        Task<T> SendAsync<T>(APIRequest response);
    }
}

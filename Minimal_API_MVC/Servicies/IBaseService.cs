using CouponAPI.Models;

namespace CouponAPI.Servicies
{
    public interface IBaseService : IDisposable
    {
        ResponseDto responseModel { get; set; }

        Task<T> SendAsync<T>(APIRequest response);
    }
}

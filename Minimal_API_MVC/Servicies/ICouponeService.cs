using WebApplication_MinimalAPI.Models.DTOs;

namespace Minimal_API_MVC_WEB.Servicies
{
    public interface ICouponeService
    {
        Task<T> GetAllCoupons<T>();
        Task<T> GetCouponsById<T>(int id);

        Task<T> CreateCouponAsync<T>(CouponDTO couponDTO);

        Task<T> UpdateCouponAsync<T>(CouponDTO couponDTO);

        Task<T> DeleteCouponAsync<T>(int id);
    }
}

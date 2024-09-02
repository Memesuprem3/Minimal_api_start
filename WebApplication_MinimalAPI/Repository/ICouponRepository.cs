using CouponAPI.Models;

namespace CouponAPI.Repository
{
    public interface ICouponRepository
    {
        Task<IEnumerable<Coupon>> GetAllAsync();

        Task<Coupon> GetAsync(int id);

        Task<Coupon> GetAsync(string couponName);

        Task CreateAsync(Coupon coupon);
        Task UpdateAsync(Coupon coupon);
        Task RemoveAsync(Coupon coupon);
        Task SaveAsync();

    }
}

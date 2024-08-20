
using WebApplication_MinimalAPI.Models;
namespace WebApplication_MinimalAPI.Data
{
    public class CouponStore
    {
        public static List<Coupon> couponList = new List<Coupon> 
        {
            new Coupon{ ID = 101, Name="10% OFF", Precent = 10, IsActive =true},
            new Coupon{ ID = 102, Name="20% OFF", Precent = 10, IsActive =true},
            new Coupon{ ID = 103, Name="25% OFF", Precent = 10, IsActive =true},
            new Coupon{ ID = 104, Name="35% OFF", Precent = 10, IsActive =true}
            
        };
    }
}

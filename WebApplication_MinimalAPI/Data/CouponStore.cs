﻿
using CouponAPI.Models;
namespace CouponAPI.Data
{
    public class CouponStore
    {
        public static List<Coupon> couponList = new List<Coupon> 
        {
            new Coupon{ ID = 101, Name="10% OFF", Precent = 10, IsActive =true},
            new Coupon{ ID = 102, Name="20% OFF", Precent = 20, IsActive =true},
            new Coupon{ ID = 103, Name="25% OFF", Precent = 25, IsActive =true},
            new Coupon{ ID = 104, Name="35% OFF", Precent = 35, IsActive =true}
            
        };
    }
}

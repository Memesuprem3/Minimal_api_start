﻿using CouponAPI;
using CouponAPI.Models.DTOs;
using CouponAPI.Models;

namespace CouponAPI.Servicies
{
    public class CouponService : BaseService, ICouponeService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CouponService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _httpClientFactory = clientFactory;
        }



        public async Task<T> CreateCouponAsync<T>(CouponDTO couponDTO)
        {
            return await SendAsync<T>(new APIRequest()
            {
                apiType = StaticDetails.ApiType.POST,
                Data = couponDTO,
                Url = StaticDetails.CouponApiBase + "/api/coupon",
                AccessToken = ""

            });
        }

        public async Task<T> DeleteCouponAsync<T>(int id)
        {
            return await SendAsync<T>(new APIRequest()
            {
                apiType = StaticDetails.ApiType.DELETE,
                Data = id,
                Url = StaticDetails.CouponApiBase + "/api/coupon/" + id,
                AccessToken = ""

            });
        }

        public Task<T> GetAllCoupons<T>()
        {
            return this.SendAsync<T>(new APIRequest()
            {
                apiType = StaticDetails.ApiType.GET,
                Url = StaticDetails.CouponApiBase + "/api/coupons",
                AccessToken = ""
            });
        }

        public async Task<T> GetCouponsById<T>(int id)
        {
            return await SendAsync<T>(new APIRequest()
            {
                apiType = StaticDetails.ApiType.GET,
                Url = StaticDetails.CouponApiBase + "/api/coupon/" + id,
                AccessToken = ""
            });
        }

        public async Task<T> UpdateCouponAsync<T>(CouponDTO couponDTO)
        {
            return await SendAsync<T>(new APIRequest()
            {
                apiType = StaticDetails.ApiType.PUT,
                Data = couponDTO,
                Url = StaticDetails.CouponApiBase + "/api/coupon",
                AccessToken = ""
            });
        }
    }
}

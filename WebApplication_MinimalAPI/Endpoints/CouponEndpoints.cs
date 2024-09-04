using AutoMapper;
using CouponAPI.Data;
using CouponAPI.Models;
using CouponAPI.Models.DTOs;
using CouponAPI.Repository;

namespace CouponAPI.Endpoints
{
    public static class CouponEndpoints
    {
        public static void ConfigurationCouponEndPoints(this WebApplication app)
        {
            app.MapGet("/api/coupons", GetAllCoupon).WithName("GetCoupons").Produces<APIRespons>();

            app.MapGet("/api/coupon/{id:int}", GetCoupon).WithName("GetCoupon").Produces<APIRespons>();

            app.MapPost("/api/coupon", CreateCoupon).
                WithName("CreateCoupon").
                Accepts<CouponCreateDTO>("application/json").
                Produces(201).Produces(400);

            app.MapPut("/api/coupon", UpdateCoupon).WithName("UpdateCoupone").
                Accepts<CouponUpdateDTO>("application/json").
                Produces<CouponUpdateDTO>(200).Produces(400);

            app.MapDelete("/api/coupon/{id:int}", DeleteCoupon).WithName("DeleteCoupon");
        }


        private async static Task<IResult> GetAllCoupon(ICouponRepository _couponRepo)
        {
            APIRespons response = new APIRespons();

            response.Result = await _couponRepo.GetAllAsync();
            response.IsSuccess = true;
            response.StatusCode = System.Net.HttpStatusCode.OK;

            return Results.Ok(response);
        }

        private async static Task<IResult> GetCoupon(ICouponRepository _couponRepo, int id)
        {
            APIRespons response = new APIRespons();
            response.Result = await _couponRepo.GetAsync(id);
            response.IsSuccess = true;
            response.StatusCode = System.Net.HttpStatusCode.OK;


            return Results.Ok(response);
        }


        private async static Task<IResult> CreateCoupon(ICouponRepository _couponRepo,
            IMapper _mapper, CouponCreateDTO coupon_C_DTO)
        {
            APIRespons response = new() { IsSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

            if (_couponRepo.GetAsync(coupon_C_DTO.Name).GetAwaiter().GetResult() != null)
            {
                response.ErrorMessages.Add("Coupon Name is already in use,please use another");
                return Results.BadRequest(response);
            }

            Coupon coupon = _mapper.Map<Coupon>(coupon_C_DTO);
            await _couponRepo.CreateAsync(coupon);
            await _couponRepo.SaveAsync();
            CouponDTO couponDto = _mapper.Map<CouponDTO>(coupon);


            response.Result = couponDto;
            response.IsSuccess = true;
            response.StatusCode = System.Net.HttpStatusCode.Created;

            return Results.Ok(response);
        }

        private async static Task<IResult> UpdateCoupon(ICouponRepository _couponRepo,
            IMapper _mapper, CouponUpdateDTO coupon_U_DTO)
        {
            APIRespons response = new APIRespons()
            { IsSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

            await _couponRepo.UpdateAsync(_mapper.Map<Coupon>(coupon_U_DTO));
            await _couponRepo.SaveAsync();

            response.Result = _mapper.Map<CouponUpdateDTO>(await _couponRepo.GetAsync(coupon_U_DTO.ID));
            response.IsSuccess = true;
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return Results.Ok(response);
        }

        private async static Task<IResult> DeleteCoupon(ICouponRepository _couponRepo, int id)
        {
            APIRespons response = new()
            { IsSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

            Coupon couponFromDB = await _couponRepo.GetAsync(id);

            if (couponFromDB != null)
            {
                await _couponRepo.RemoveAsync(couponFromDB);
                await _couponRepo.SaveAsync();
                response.IsSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.NoContent;
                return Results.Ok(response);
            }
            else
            {
                response.ErrorMessages.Add("Invalid ID");
                return Results.BadRequest(response);
            }
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using CouponAPI.Models;
using CouponAPI.Servicies;
using Newtonsoft.Json;
using CouponAPI.Models.DTOs;

namespace web_Coupone.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponeService _couponeService;
        public CouponController(ICouponeService couponService)
        {
            _couponeService = couponService;
        }

        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDTO> List = new List<CouponDTO>();
            var response = await _couponeService.GetAllCoupons<ResponseDto>();

            if (response != null && response.IsSuccess)
            {
                List = JsonConvert.DeserializeObject<List<CouponDTO>>(Convert.ToString(response.Result));
            }
            return View(List);
        }

        public async Task<IActionResult> Details(int id)
        {
            CouponDTO cDto = new CouponDTO();

            var response = await _couponeService.GetCouponsById<ResponseDto>(id);

            if (response != null && response.IsSuccess)
            {
                CouponDTO model = JsonConvert.DeserializeObject<CouponDTO>(Convert.ToString(response.Result));
                return View(model);
            }
            return View();
        }

        public async Task<IActionResult> CouponCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CouponCreate(CouponDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _couponeService.CreateCouponAsync<ResponseDto>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(CouponIndex));
                }
            }
            return View(model);
        }
        public async Task<IActionResult> UpdateCoupon(int couponId)
        {
            var response = await _couponeService.GetCouponsById<ResponseDto>(couponId);
            if (response != null && response.IsSuccess)
            {
                CouponDTO model = JsonConvert.DeserializeObject<CouponDTO>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCoupon(CouponDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _couponeService.UpdateCouponAsync<ResponseDto>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(CouponIndex));
                }
            }
            return View(model);
        }

        public async Task<IActionResult> DeleteCoupon(int Id)
        {
            var response = await _couponeService.DeleteCouponAsync<ResponseDto>(Id);

            if (response != null && response.IsSuccess)
            {
                CouponDTO model = JsonConvert.DeserializeObject<CouponDTO>(Convert.ToString(response.Result));

                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCoupon(CouponDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _couponeService.DeleteCouponAsync<ResponseDto>(model.ID);

                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(CouponIndex));
                }

            }
            return RedirectToAction(nameof(CouponIndex));
        }
    }
}

using FluentValidation;
using CouponAPI.Models.DTOs;

namespace CouponAPI.Validations
{
    public class CouponCreateValidation : AbstractValidator<CouponCreateDTO>
    {
        public CouponCreateValidation()
        {
            RuleFor(model => model.Name).NotEmpty();
            RuleFor(model => model.Precent).InclusiveBetween(1, 75);
        }
    }
}

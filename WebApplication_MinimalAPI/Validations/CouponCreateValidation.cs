using FluentValidation;
using WebApplication_MinimalAPI.Models.DTOs;

namespace WebApplication_MinimalAPI.Validations
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

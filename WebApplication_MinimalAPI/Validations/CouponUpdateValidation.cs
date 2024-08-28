﻿using FluentValidation;
using WebApplication_MinimalAPI.Models.DTOs;

namespace WebApplication_MinimalAPI.Validations
{
    public class CouponUpdateValidation : AbstractValidator<CouponUpdateDTO>
    {
        public CouponUpdateValidation()
        {
            RuleFor(model => model.ID).NotEmpty().GreaterThan(0);
            RuleFor(model => model.Name).NotEmpty();
            RuleFor(model => model.Precent).InclusiveBetween(1,75);
        }
    }
}

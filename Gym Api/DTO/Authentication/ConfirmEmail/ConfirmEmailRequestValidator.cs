﻿using FluentValidation;

namespace Gym_Api.DTO.Authentication.ConfirmEmail
{
    public class ConfirmEmailRequestValidator : AbstractValidator<ConfirmEmailRequest>
    {
        public ConfirmEmailRequestValidator()
        {
            RuleFor(X => X.Email)
                .NotEmpty()
                .WithMessage("Plz Add a {PropertyName}");

            RuleFor(X => X.Otp)
               .NotEmpty()
               .WithMessage("Plz Add a {PropertyName}");


        }

    }
}

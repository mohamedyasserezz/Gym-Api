﻿using FluentValidation;

namespace Gym_Api.DTO.Authentication.Login
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(X => X.Email)
                .NotEmpty()
                .WithMessage("Plz Add a {PropertyName}")
                .EmailAddress();

            RuleFor(X => X.Password)
               .NotEmpty()
               .WithMessage("Plz Add a {PropertyName}");


        }

    }
}

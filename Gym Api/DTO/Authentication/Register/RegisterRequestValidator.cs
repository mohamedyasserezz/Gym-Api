using FluentValidation;
using Gym_Api.Common.Consts;

namespace Gym_Api.DTO.Authentication.Register
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(X => X.FirstName)
                .NotEmpty()
                .WithMessage("Plz Add a {PropertyName}")
                .Length(3, 100)
                .WithMessage("{PropertyName} length should be between 3 and 100");

            RuleFor(X => X.LastName)
               .NotEmpty()
               .WithMessage("Plz Add a {PropertyName}")
               .Length(3, 100)
               .WithMessage("{PropertyName} length should be between 3 and 100");

            RuleFor(X => X.Email)
                .NotEmpty()
                .WithMessage("Plz Add a {PropertyName}")
                .EmailAddress();


            RuleFor(X => X.Password)
                .NotEmpty()
                .WithMessage("Plz Add a {PropertyName}")
                .Matches(RegexPatterns.Password)
                .WithMessage("Password should be atleast 8 digits and contains LowerCase, UpperCase, NonAlphanumeric");


        }
    }
}

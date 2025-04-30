using FluentValidation;
using Gym_Api.Common.Consts;

namespace Gym_Api.DTO.ResetPassword
{
    public class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
    {
        public ResetPasswordRequestValidator()
        {
            RuleFor(X => X.Email)
                .NotEmpty()
                .WithMessage("Plz Add a {PropertyName}")
                .EmailAddress();

            RuleFor(X => X.Code)
                .NotEmpty()
                .WithMessage("Plz Add a {PropertyName}");

            RuleFor(X => X.NewPassword)
                .NotEmpty()
                .WithMessage("Plz Add a {PropertyName}")
                .Matches(RegexPatterns.Password)
                .WithMessage("Password should be atleast 8 digits and contains LowerCase, UpperCase, NonAlphanumeric");
        }
    }
}

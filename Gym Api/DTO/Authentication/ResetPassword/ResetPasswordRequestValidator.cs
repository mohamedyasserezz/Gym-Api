using FluentValidation;
using Gym_Api.Common.Consts;

namespace Gym_Api.DTO.Authentication.ResetPassword
{
    public class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
    {
        public ResetPasswordRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Otp)
               .NotEmpty();


        }
    }
}

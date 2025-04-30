using FluentValidation;

namespace Gym_Api.DTO.Authentication.RefreshToken
{
    public class RefreshTokenRequestValidator : AbstractValidator<RefreshTokenRequest>
    {
        public RefreshTokenRequestValidator()
        {
            RuleFor(X => X.Token)
                .NotEmpty();

            RuleFor(X => X.RefreshToken)
               .NotEmpty();
        }


    }
}

using FluentValidation;

namespace Gym_Api.DTO.Authentication.ConfirmEmail
{
    public class ConfirmEmailRequestValidator : AbstractValidator<ConfirmEmailRequest>
    {
        public ConfirmEmailRequestValidator()
        {
            RuleFor(X => X.UserId)
                .NotEmpty()
                .WithMessage("Plz Add a {PropertyName}");

            RuleFor(X => X.Code)
               .NotEmpty()
               .WithMessage("Plz Add a {PropertyName}");


        }

    }
}

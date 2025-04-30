using FluentValidation;

namespace Gym_Api.DTO.Authentication.ResendConfirmationEmail
{
    public class ResendConfirmationEmailRequestValidator : AbstractValidator<ResendConfirmationEmailRequest>
    {
        public ResendConfirmationEmailRequestValidator()
        {
            RuleFor(X => X.Email)
                .NotEmpty()
                .WithMessage("Plz Add a {PropertyName}")
                .EmailAddress()
                .WithMessage("Invalid email format");
        }
    }
}

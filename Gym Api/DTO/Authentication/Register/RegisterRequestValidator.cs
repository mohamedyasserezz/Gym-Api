using FluentValidation;
using Gym_Api.Common.Consts;

namespace Gym_Api.DTO.Authentication.Register
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(X => X.FullName)
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

            RuleFor(X => X.UserName)
                .NotEmpty()
                .WithMessage("Plz Add a {PropertyName}")
                .Length(3, 100)
                .WithMessage("{PropertyName} length should be between 3 and 100")
                .Matches(RegexPatterns.Name)
                .WithMessage("{PropertyName} should only contain letters");

            RuleFor(X => X.UserType)
                .NotEmpty()
                .WithMessage("Plz Add a {PropertyName}")
                .Must(x => x == Users.Admin || x == Users.Trainee
                || x == Users.Coach || x == Users.User)
                .WithMessage("UserType should be either User or Admin or coach or trainee");
        }
    }
}

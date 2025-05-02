using FluentValidation;
using Gym_Api.Common.Consts;

namespace Gym_Api.DTO.Authentication.ResetPassword
{
    public class AssignNewPasswordValidator : AbstractValidator<AssignNewPassword>
    {
        public AssignNewPasswordValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.NewPassword)
               .NotEmpty()
               .Matches(RegexPatterns.Password)
               .WithMessage("Password should be at least 8 digits and should contains Lowercase, NonAlphanumeric and Uppercase");
        }


    }
}

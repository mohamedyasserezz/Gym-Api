using FluentValidation;

namespace Gym_Api.DTO.Authentication.ForgetPassword
{
	public class ForgetPasswordRequestValidator : AbstractValidator<ForgetPasswordRequest>
	{
		public ForgetPasswordRequestValidator()
		{
			RuleFor(X => X.Email)
				.NotEmpty()
				.WithMessage("Plz Add a {PropertyName}")
				.EmailAddress();
		}
	}
}

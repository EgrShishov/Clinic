namespace Auth.Application.Commands.SignUp
{
    public sealed class SignUpValidator :
           AbstractValidator<SignUpCommand>
    {
        public SignUpValidator()
        {
            RuleFor(command => command.Password)
                .NotEmpty().WithMessage("Please, enter the password")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters")
                .MaximumLength(15).WithMessage("Password must be no more than 15 characters")
                .When(x => x.Password.Any(char.IsDigit), ApplyConditionTo.CurrentValidator)
                .When(x => x.Password.Any(char.IsLetter), ApplyConditionTo.CurrentValidator);

            RuleFor(command => command.Email)
                .NotEmpty().WithMessage("Please, enter the email")
                .EmailAddress().WithMessage("You've entered an invalid email");

            RuleFor(x => x.ReenteredPassword)
                .NotEmpty().WithMessage("Please, reenter the password")
                .Equal(x => x.Password).WithMessage("The passwords you’ve entered don’t coincide");
        }
    }
}

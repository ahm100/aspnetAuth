

using FluentValidation;
using Application.Features.Users.Commands;
using Application.Features.Users.Validators;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Firstname).NotEmpty().WithMessage("First name is required.");
        RuleFor(x => x.Lastname).NotEmpty().WithMessage("Last name is required.");
        //RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.").EmailAddress().WithMessage("Invalid email format.");
        //RuleFor(x => x.DateOfBirth).NotEmpty().WithMessage("Date of birth is required.");
        // Add other validation rules as needed
    }
}

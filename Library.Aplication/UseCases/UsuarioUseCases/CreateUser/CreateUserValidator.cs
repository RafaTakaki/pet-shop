using FluentValidation;

namespace Library.Aplication.UseCases.UsuarioUseCases.CreateUser
{
    public class CreateUserValidator : AbstractValidator<CreateUserRequest>
    {
        // colocar validações aqui
        public CreateUserValidator()
        {
            // RuleFor(x => x.Name)
            //     .NotEmpty().WithMessage("Name is required.")
            //     .Length(2, 50).WithMessage("Name must be between 2 and 50 characters.");

            // RuleFor(x => x.Email)
            //     .NotEmpty().WithMessage("Email is required.")
            //     .EmailAddress().WithMessage("Invalid email format.");

            // RuleFor(x => x.Password)
            //     .NotEmpty().WithMessage("Password is required.")
            //     .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        }
    }
}
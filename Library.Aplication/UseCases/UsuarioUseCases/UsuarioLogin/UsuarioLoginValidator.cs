using FluentValidation;

namespace Library.Aplication.UseCases.UsuarioUseCases.UsuarioLogin
{
    public class UsuarioLoginValidator : AbstractValidator<UsuarioLoginRequest>
    {
        public UsuarioLoginValidator()
        {
            // RuleFor(x => x.Email)
            //     .NotEmpty().WithMessage("Email é obrigatório.")
            //     .EmailAddress().WithMessage("Email inválido.");

            // RuleFor(x => x.Senha)
            //     .NotEmpty().WithMessage("Senha é obrigatória.");
        }
    }
}
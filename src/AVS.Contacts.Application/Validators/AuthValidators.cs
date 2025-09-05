using AVS.Contacts.Application.Commands;
using FluentValidation;

namespace AVS.Contacts.Application.Validators;

public class SignInCommandValidator : AbstractValidator<SignInCommand>
{
    public SignInCommandValidator()
    {
        // SignInCommand não tem propriedades para validar
        // Validação será feita no serviço de autenticação
    }
}

public class SignOutCommandValidator : AbstractValidator<SignOutCommand>
{
    public SignOutCommandValidator()
    {
        // SignOutCommand não tem propriedades para validar
        // Validação será feita no serviço de autenticação
    }
}
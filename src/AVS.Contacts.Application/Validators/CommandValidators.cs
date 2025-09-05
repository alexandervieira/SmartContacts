using AVS.Contacts.Application.Commands;
using FluentValidation;

namespace AVS.Contacts.Application.Validators;

public class CreateContactCommandValidator : AbstractValidator<CreateContactCommand>
{
    public CreateContactCommandValidator()
    {
        RuleFor(x => x.Contact)
            .NotNull().WithMessage("Dados do contato são obrigatórios")
            .SetValidator(new CreateContactDtoValidator());
    }
}

public class CreateContactFromVoiceCommandValidator : AbstractValidator<CreateContactFromVoiceCommand>
{
    public CreateContactFromVoiceCommandValidator()
    {
        RuleFor(x => x.VoiceData)
            .NotNull().WithMessage("Dados de voz são obrigatórios");

        RuleFor(x => x.VoiceData.RawText)
            .NotEmpty().WithMessage("Texto de voz é obrigatório")
            .MinimumLength(10).WithMessage("Texto deve ter pelo menos 10 caracteres")
            .When(x => x.VoiceData != null);
    }
}
using AVS.Contacts.Contracts.DTOs;
using FluentValidation;

namespace AVS.Contacts.Application.Validators;

public class CreateContactDtoValidator : AbstractValidator<CreateContactDto>
{
    public CreateContactDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Nome é obrigatório")
            .MaximumLength(50).WithMessage("Nome deve ter no máximo 50 caracteres");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Sobrenome é obrigatório")
            .MaximumLength(50).WithMessage("Sobrenome deve ter no máximo 50 caracteres");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Telefone é obrigatório")
            .Matches(@"^\+?[\d\s\(\)\-]{10,15}$").WithMessage("Formato de telefone inválido");

        RuleFor(x => x.Street)
            .NotEmpty().WithMessage("Rua é obrigatória")
            .MaximumLength(100).WithMessage("Rua deve ter no máximo 100 caracteres");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("Cidade é obrigatória")
            .MaximumLength(50).WithMessage("Cidade deve ter no máximo 50 caracteres");
    }
}

public class UpdateContactDtoValidator : AbstractValidator<UpdateContactDto>
{
    public UpdateContactDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .MaximumLength(50).WithMessage("Nome deve ter no máximo 50 caracteres")
            .When(x => !string.IsNullOrEmpty(x.FirstName));

        RuleFor(x => x.LastName)
            .MaximumLength(50).WithMessage("Sobrenome deve ter no máximo 50 caracteres")
            .When(x => !string.IsNullOrEmpty(x.LastName));

        RuleFor(x => x.Phone)
            .Matches(@"^\+?[\d\s\(\)\-]{10,15}$").WithMessage("Formato de telefone inválido")
            .When(x => !string.IsNullOrEmpty(x.Phone));

        RuleFor(x => x.Street)
            .MaximumLength(100).WithMessage("Rua deve ter no máximo 100 caracteres")
            .When(x => !string.IsNullOrEmpty(x.Street));

        RuleFor(x => x.City)
            .MaximumLength(50).WithMessage("Cidade deve ter no máximo 50 caracteres")
            .When(x => !string.IsNullOrEmpty(x.City));
    }
}
using FluentValidation;
using MovimentControl.Api.Models;

namespace MovimentControl.Api.Validations
{
    public class ClientValidator : AbstractValidator<ClientInputModel>
    {
        public ClientValidator()
        { 
            RuleFor(x => x.FullName)
                .NotNull()
                .MaximumLength(30)
                .WithMessage("Nome Inválido");

            RuleFor(x => x.CPF)
                .NotNull()
                .Length(11)
                .WithMessage("CPF Inválido");

            RuleFor(x => x.RG)
                .NotNull()
                .Length(10)
                .WithMessage("RG Inválido");

            RuleFor(x => x.CEP)
                .NotNull()
                .Length(8)
                .WithMessage("CEP Inválido");

            RuleFor(x => x.Email)
                .NotNull()
                .EmailAddress()
                .WithMessage("Email Inválido");

            RuleFor(x => x.Phone)
                .NotNull()
                .Length(11)
                .WithMessage("Telefone Inválido");
            
        }
    }
}
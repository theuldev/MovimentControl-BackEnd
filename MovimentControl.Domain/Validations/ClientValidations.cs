using FluentValidation;
using MovimentControl.Domain.Models.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovimentControl.Domain.Validations
{
    public class ClientValidations : AbstractValidator<ClientInputModel>
    {
        public ClientValidations()
        {
            RuleFor(x => x.FullName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(30)
                .WithMessage("Nome Inválido");

            RuleFor(x => x.CPF)
                .NotNull()
                .NotEmpty()
                .Length(11)
                .WithMessage("CPF Inválido");

            RuleFor(x => x.RG)
                .NotNull()
                .NotEmpty()
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

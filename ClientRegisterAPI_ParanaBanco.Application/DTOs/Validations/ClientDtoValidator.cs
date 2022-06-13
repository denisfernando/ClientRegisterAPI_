using ClientRegisterAPI_ParanaBanco.Domain.Validations;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientRegisterAPI_ParanaBanco.Application.DTOs.Validations
{
    public class ClientDtoValidator : AbstractValidator<ClientDTO>
    {
         public ClientDtoValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .NotNull()
                .WithMessage("Informe o Nome completo do cliente");
            
            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .WithMessage("Informe o Email completo do cliente");

            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Informe um email válido");
        }
    }
}

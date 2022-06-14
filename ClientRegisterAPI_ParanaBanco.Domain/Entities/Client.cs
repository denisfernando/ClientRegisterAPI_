using ClientRegisterAPI_ParanaBanco.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientRegisterAPI_ParanaBanco.Domain.Entities
{
    public class Client
    {
        public int Id { get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }

        public Client(string fullName, string email)
        {
            Validation(fullName, email);
        }

        public Client(int id, string fullName, string email)
        {
            DomainValidationException.When(id < 0, "O Cliente não existe.");
            Validation(fullName, email);
        }

        private void Validation(string fullName, string email)
        {
            DomainValidationException.When(String.IsNullOrEmpty(fullName), "Informe o Nome completo do cliente.");
            DomainValidationException.When(String.IsNullOrEmpty(email), "Informe o Email do cliente.");
            DomainValidationException.When(fullName.Length > 250, "O Nome do cliente não pode conter mais de 250 caracteres.");
            DomainValidationException.When(email.Length > 250, "O Email do cliente não pode conter mais de 250 caracteres.");
            DomainValidationException.When(!UtilsValidate.IsValidEmail(email), "Informe um email válido.");

            FullName = fullName;
            Email = email;
        }
    }
}

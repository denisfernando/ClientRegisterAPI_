using ClientRegisterAPI_ParanaBanco.Domain.Entities;
using ClientRegisterAPI_ParanaBanco.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientRegisterAPI_ParanaBanco.Test.Entities
{
    public class ClientTests
    {
        [Fact]
        public void Client_Validade_Full_Name_EmptyOrNull()
        {
            //Arrange e ACT
            var result = Assert.Throws<DomainValidationException>(() => new Client(
                "",
                "denis_feernan@hotmail.com"
                ));

            //Assert
            Assert.Equal("Informe o Nome completo do cliente.", result.Message);
        }

        [Fact]
        public void Client_Validade_Full_Name_Length()
        {
            //Arrange e ACT
            var result = Assert.Throws<DomainValidationException>(() => new Client(
                "Cliente teste 11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111",
                "Cliente@teste.com"
                ));

            //Assert
            Assert.Equal("O Nome do cliente não pode conter mais de 250 caracteres.", result.Message);
        }

        [Fact]
        public void Client_Validade_Email_EmptyOrNull()
        {
            //Arrange e ACT
            var result = Assert.Throws<DomainValidationException>(() => new Client(
                "Cliente teste",
                ""
                ));

            //Assert
            Assert.Equal("Informe o Email do cliente.", result.Message);
        }

        [Fact]
        public void Client_Validade_Email_Length()
        {
            //Arrange e ACT
            var result = Assert.Throws<DomainValidationException>(() => new Client(
                "Cliente teste",
                "Cliente11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111@teste.com"
                ));

            //Assert
            Assert.Equal("O Email do cliente não pode conter mais de 250 caracteres.", result.Message);
        }

        [Fact]
        public void Client_Validade_Email_Is_Invalid()
        {
            //Arrange e ACT
            var result = Assert.Throws<DomainValidationException>(() => new Client(
                "Cliente teste",
                "Cliente@"
                ));

            //Assert
            Assert.Equal("Informe um email válido.", result.Message);
        }


        [Fact]
        public void Client_Validade_Id_Is_Invalid()
        {
            //Arrange e ACT
            var result = Assert.Throws<DomainValidationException>(() => new Client(
                -1,
                "Cliente teste",
                "cliente@teste.com"
                ));

            //Assert
            Assert.Equal("O Cliente não existe.", result.Message);
        }
    }
}

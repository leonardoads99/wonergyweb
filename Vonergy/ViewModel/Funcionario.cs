using System;
using System.ComponentModel.DataAnnotations;
using vonergyDom.ViewModel.Enumm;

namespace Vonergy.ViewModel
{
    public class Funcionario
    {

        public long Id { get; set; }

        [Required(ErrorMessage = "O campo nome é Obrigatório.")]
        [StringLength(100, ErrorMessage = "ONomedeveterentre1e100caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo cpf é Obrigatório.")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "O campo Rg é Obrigatório.")]
        public string Rg { get; set; }

        [Required(ErrorMessage = "Selecione um Orgão Expeditor.")]
        public OrgaoExpeditor OrgaoExpeditor { get; set; }

        [Required(ErrorMessage = "Selecione a data de nascimento.")]
        //[DisplayFormat(ApplyFormatInEditMode = true)]

        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O campo nome mãe é Obrigatório.")]
        public string NomeMae { get; set; }

        public string NomePai { get; set; }

        [Required(ErrorMessage = "Selecione se sexo.")]
        public Sexo Sexo { get; set; }

        [Required(ErrorMessage = "Selecione se estado civil.")]
        public EstadoCivil EstadoCivil { get; set; }

        [Required(ErrorMessage = "O campo logradouro é Obrigatório.")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "O campo bairro é Obrigatório.")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "O campo cidade é Obrigatório.")]
        public string Cidade { get; set; }

        public string Numero { get; set; }

        public string Referencia { get; set; }

        [Required(ErrorMessage = "O campo cep é Obrigatório.")]
        public string Cep { get; set; }

        [Required(ErrorMessage = "O campo uf é Obrigatório.")]
        public string Uf { get; set; }    
        
        public string Telefone { get; set; }

        [Required]
        public string Celular { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "email inválido")]
        public string Email { get; set; }
        
        public string Senha { get; set; }

        public bool ResetSenha { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vonergy.ViewModel
{
    public class Empresa
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Cnpj é Obrigatório.")]
        [Display(Name = "Cnpj")]
        public string Cnpj { get; set; }

        [Required(ErrorMessage = "O campo Razao Social é Obrigatório.")]
        [Display(Name = "RazaoSocial")]
        public string RazaoSocial { get; set; }

        [Required(ErrorMessage = "O campo Nome Fantasia é Obrigatório.")]
        [Display(Name = "NomeFantasia")]
        public string NomeFantasia { get; set; }

        [Required(ErrorMessage = "O campo status é Obrigatório.")]
        [Display(Name = "Ativo")]
        public string Ativo { get; set; }

        [Required(ErrorMessage = "O campo Logradouro é Obrigatório.")]
            [Display(Name = "Logradouro")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "O campo Bairro é Obrigatório.")]
        [Display(Name = "Bairro")]
        public string Bairro { get; set; }

        [Display(Name = "Cidade")]
        [Required(ErrorMessage = "O campo cidade é Obrigatório.")]
        public string Cidade { get; set; }

        [Display(Name = "Numero")]
        public string Numero { get; set; }

        [Display(Name = "Referencia")]
        public string Referencia { get; set; }

        [Display(Name = "Cep")]
        [Required(ErrorMessage = "O campo cep é Obrigatório.")]
        public string Cep { get; set; }

        [Display(Name = "Uf")]
        [Required(ErrorMessage = "O campo estado é Obrigatório.")]
        public string Uf { get; set; }

        [EmailAddress(ErrorMessage = "email inválido")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Telefone")]
        public string Telefone { get; set; }

        [Display(Name = "Celular")]
        [Required]
        public string Celular { get; set; }

        [Display(Name = "Site")]
        [Required]
        public string Site { get; set; }
    }
}
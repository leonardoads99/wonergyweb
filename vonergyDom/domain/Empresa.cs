using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace vonergyDom.ViewModel
{
    public class Empresa
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Cnpj é Obrigatório.")]
        public string Cnpj { get; set; }

        [Required(ErrorMessage = "O campo Razao Social é Obrigatório.")]
        public string RazaoSocial { get; set; }

        [Required(ErrorMessage = "O campo Nome Fantasia é Obrigatório.")]
        public string NomeFantasia { get; set; }

        [Required(ErrorMessage = "O campo status é Obrigatório.")]
        public string Ativo { get; set; }

        public string Logradouro { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public string Numero { get; set; }

        public string Referencia { get; set; }

        [Required(ErrorMessage = "O campo cep é Obrigatório.")]
        public string Cep { get; set; }

        public string Uf { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "email inválido")]
        public string Email { get; set; }

        public string Telefone { get; set; }

        public string Celular { get; set; }

        public string Site { get; set; }

        public  virtual IList<Funcionario> Funcionarios { get; set; }
       // public virtual long? IdFuncionario { get; set; }

        public virtual IList<Equipamentos> Equipamentos { get; set; }
        //  public virtual long? IdEquipamento { get; set; }

        public virtual IList<DispositivoIOT> DispositivoIOTs { get; set; }
    }
}

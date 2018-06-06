using System.ComponentModel.DataAnnotations.Schema;
using vonergyDom.ViewModel.Enumm;

namespace vonergyDom.ViewModel
{
    [Table("IndustriaEnergetica")]
   public class Industrias
    {

        public long Id { get; set; }
        public string Empresa { get; set; }
        public string Localizaçao { get; set; }
        public string Custo { get; set; }//constante onde fica o custo a calcular
        public DistribuidoraEletrica NomeDistribuidoraEletrica { get; set; }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}

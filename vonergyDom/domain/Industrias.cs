using System.ComponentModel.DataAnnotations.Schema;
using vonergyDom.ViewModel.Enumm;

namespace vonergyDom.ViewModel
{
    [Table("IndustriaEnergetica")]
   public class Industria
    {

        public long Id { get; set; }
        public string Empresa { get; set; }
        public string Localizaçao { get; set; }
        public string Custo { get; set; }//constante onde fica o custo a calcular
        public DistribuidoraEletrica NomeDistribuidoraEletrica { get; set; }
    }
}

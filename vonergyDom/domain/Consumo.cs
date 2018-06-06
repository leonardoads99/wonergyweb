using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace vonergyDom.ViewModel
{
    [Table("ConsumoEletrico")]
    public class Consumo
    {
        public long Id { get; set; }
        public decimal Corrente { get; set; }
        public decimal Tensao { get; set; }
        public decimal Potencia { get; set; }
        public decimal ConsumoDiario { get; set; }
        public DateTime DataRegistro { get; set; }

        
    }
}

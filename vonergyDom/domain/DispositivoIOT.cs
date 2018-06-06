using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using vonergyDom.ViewModel.Enumm;

namespace vonergyDom.ViewModel
{
    [Table("DispositivoIOT")]
    public class DispositivoIOT
    {
        public long Id { get; set; }
        public string CodigoEsp { get; set; }
        public string Nome { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Descicao { get; set; }
        private Status status;
        public Status Status { get => status; set => status = Status.Inativo; }

        public virtual IList<Consumo> Consumos { get; set; }
    }
}

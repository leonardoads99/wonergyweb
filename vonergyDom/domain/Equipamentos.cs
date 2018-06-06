using System.ComponentModel.DataAnnotations.Schema;

namespace vonergyDom.ViewModel
{
    [Table("Equipamento")]
   public class Equipamentos
    {
        public long Id { get; set; }
        public string NomeEquipamento { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public decimal PotenciaMinima { get; set; }
        public decimal PotenciaMaxima { get; set; }
        public decimal TensaoMinima { get; set; }
        public decimal TensaoMaxima { get; set; } 
        public decimal CorrentMinima { get; set; }
        public decimal CorrenteMaxima { get; set; }

    }
}

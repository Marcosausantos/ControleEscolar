using ControleEscolar.Entities.Entity;
using System;
using System.Collections.Generic;

namespace ControleEscolar.Entities.Escola
{
    public class SensoMec : IEntityBase
    {
        public int Id { get; set; }
        public string NumeroRotina { get; set; }
        public DateTime Competencia { get; set; }
        public string Descricao { get; set; }
        public DateTime DateGeracao { get; set; }
        public DateTime? DataEnvio { get; set; }
        public string NumeroProtocoloRetorno { get; set; }
        public string Status { get; set; }

        public ICollection<SensoMecItem> SensoMecItens { get; set; }

        public SensoMec()
        {
            SensoMecItens = new List<SensoMecItem>();
        }
    }
}

using ControleEscolar.Entities.Entity;

namespace ControleEscolar.Entities.Escola
{
    public class SensoMecItem : IEntityBase
    {
        public int Id { get; set; }
        public Aluno Aluno { get; set; }
        public bool Enviar { get; set; }
        public SensoMec SensoMec { get; set; }
    }
}

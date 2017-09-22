using ControleEscolar.Entities.Entity;

namespace ControleEscolar.Entities.Escola
{
    public class Municipio : IEntityBase
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int? EstadoId { get; set; }
        public Estado Estado { get; set; }         
    }
}

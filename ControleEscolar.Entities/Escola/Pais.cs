using ControleEscolar.Entities.Entity;

namespace ControleEscolar.Entities.Escola
{
    public class Pais : IEntityBase
    {
        public int Id { get; set; }
        public string Nome { get; set; }        
    }
}

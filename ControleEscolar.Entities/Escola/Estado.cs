using ControleEscolar.Entities.Entity;

namespace ControleEscolar.Entities.Escola
{
    public class Estado : IEntityBase
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public int? PaisId { get; set; }
        public Pais Pais { get; set; }  
    }
}
